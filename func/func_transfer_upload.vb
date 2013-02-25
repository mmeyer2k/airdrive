'    AirDrive Project: Array of IMAP repositories
'    Copyright (C) 2010 Michael Meyer
'
'    This program is free software: you can redistribute it and/or modify
'    it under the terms of the GNU General Public License as published by
'    the Free Software Foundation, either version 3 of the License, or
'    (at your option) any later version.
'
'    This program is distributed in the hope that it will be useful,
'    but WITHOUT ANY WARRANTY; without even the implied warranty of
'    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
'    GNU General Public License for more details.

'    You should have received a copy of the GNU General Public License
'    along with this program.  If not, see <http://www.gnu.org/licenses/>.
'
'    Filename: func_transfer_upload.vb


Imports AirDrive.enums
Imports System.IO
Imports AirDrive.func_rcrypt
Imports AirDrive.func_convert
Imports AirDrive.functions

Public Module func_transfer_upload

    Public bNoAccount As Boolean = False

    Public Function IMAPUpload(ByRef sItem As ListViewItem) As Boolean
        If colAccountList.Count = 0 Then
            Dim r As MsgBoxResult
            r = MessageBox.Show("You do not have any remote accounts to upload to. Would you like to add one now?", String.Empty, MessageBoxButtons.YesNo)
            If r = MsgBoxResult.Yes Then
                frmActs.ShowDialog(frmMain)
                If colAccountList.Count = 0 Then
                    bNoAccount = True
                End If
            Else
                bNoAccount = True
                Return False
            End If
        End If

        Dim bRename As Boolean = False

        Dim localpath = sItem.SubItems(0).Text
        Dim remotepath = sItem.SubItems(2).Text
        Dim intSeg, intArc As Integer
        Dim bIsEncrypted As Boolean = False

        'determine if segment should be encrypted
        If sItem.Tag IsNot Nothing AndAlso sItem.Tag.ToString.Contains("encrypted;") Then
            bIsEncrypted = True
        End If

        intArc = 1
        intSeg = 0

        'search if file already exists in collection
        Dim ie As IEnumerator = colRemoteFileData.GetEnumerator
        Do While ie.MoveNext
            Dim objSeg As objSegment = ie.Current
            Dim hread As String = objSeg.FilePath

            If hread = remotepath Then
                Dim bShowDialog As Boolean = False
                Dim lMD5 As String = func_MD5HashFromFile(localpath)

                If bDupResAlways AndAlso bDupResOnlyDiff AndAlso lMD5 <> objSeg.MD5Total Then
                    bShowDialog = True
                End If

                If Not bDupResAlways Then
                    bShowDialog = True
                End If

                If bShowDialog = True Then
                    'show the duplicate file dialog box
                    Dim newFrm As New frmDupDiag
                    newFrm.lblLocalFile.Text = localpath
                    newFrm.lblRemoteFile.Text = remotepath
                    newFrm.lblRemoteSize.Text = ReturnNiceSize(2, , objSeg.SizeTotal)
                    newFrm.lblRemoteMD5.Text = objSeg.MD5Total
                    newFrm.lblLocalSize.Text = ReturnNiceSize(2, localpath)
                    newFrm.lblLocalMD5.Text = lMD5
                    If newFrm.ShowDialog() = Windows.Forms.DialogResult.Cancel Then
                        'if user chooses cancel at duplicate dialog, then assume 'SKIP':
                        LogOutput(enumLogType.INFO, String.Format("Skipping file:  {0}", hread))
                        Return True
                    End If
                End If


                If ePreviousAction = enums.RemoteFileExistsResponse.Archive Then
                    'if user chooses to archive new version
                    'scan for highest saved arc number and increment it
                    Dim t As New Collection
                    Dim i As Integer = 0
                    For Each item As objSegment In colRemoteFileData
                        If item.FilePath.ToLower = remotepath.ToLower Then
                            t.Add(item)
                        End If
                    Next
                    i = t.Count
                    i += 1
                    intArc = i

                ElseIf ePreviousAction = enums.RemoteFileExistsResponse.Overwrite Then
                    LogOutput(enumLogType.INFO, String.Format("Overwriting file:  {0}", hread))
                    colPendingDelete.Add(objSeg)
                    b_PendingDelete = True

                ElseIf ePreviousAction = RemoteFileExistsResponse.Rename Then
                    bRename = True

                ElseIf ePreviousAction = enums.RemoteFileExistsResponse.Skip Then
                    LogOutput(enumLogType.INFO, String.Format("Skipping file:  {0}", hread))
                    Return True
                End If
            End If
        Loop

        'gather info about local file and post to global size variable
        Dim fItem As String = sItem.SubItems(0).Text
        Dim fInfo As New FileInfo(fItem)
        Dim fName As String = fInfo.Name
        Dim fSize As Int64 = fInfo.Length


        If bRename Then 'if the rename action is triggered, then rename the file
            Dim fNew As String = Nothing
            Dim i As Integer = 1
            Do 'find the most suitable new name
                Dim bEscape As Boolean = True
                If fName.Contains(chrPeriod) Then
                    Dim fOldArray() As String = fName.Split(chrPeriod)
                    Dim firstword As String = String.Format("{0}({1})", fOldArray(0), i)
                    fNew = firstword
                    Dim i2 As Integer = -1
                    For Each item In fOldArray
                        i2 += 1
                        If i2 = 0 Then
                            Continue For
                        End If
                        fNew += chrPeriod & item
                    Next
                Else
                    fNew = String.Format("{0}({1})", fName, i)
                End If

                For Each seg As objSegment In colRemoteFileData
                    If seg.FileName = fNew Then
                        bEscape = False
                    End If
                Next
                i += 1
                If bEscape = True Then Exit Do
            Loop

            'apply new name
            fName = fNew

            'modify the remote path
            remotepath = remotepath.Substring(0, remotepath.LastIndexOf(chrFrontslash) + 1)
            remotepath += fName

            'modify the listview item
            sItem.SubItems(2).Text = remotepath
        End If

        Dim totalSegs As Integer = 1

        'create a block size that fits with max attachment size
        Dim EntryPoint As Integer

        'determine if file size is less than maximum and set loop to end after a single iteration
        If func_CalculateEncodedSize(fSize) < longMaxATT Then
            EntryPoint = fSize
        Else
            EntryPoint = Math.Ceiling(longMaxATT * 3 / 4)
            Do Until EntryPoint Mod 3 = 0
                EntryPoint -= 1
            Loop
            totalSegs = Math.Ceiling(func_CalculateEncodedSize(fSize) / func_CalculateEncodedSize(EntryPoint))
        End If

        'Prepare to read original file and copy to temp
        Dim fChunk As New FileStream(fItem, FileMode.Open, FileAccess.Read)
        Dim rChunk As New BinaryReader(fChunk)

        'bytecounter to report progress with
        Dim bytecounter As Integer = 0

        'create loop escape 
        Dim bFinished As Boolean = False

        Do While bFinished = False
            intSeg += 1

            'select the accout to upload to and ensure that it is logged it
            If Not SelectAccount(smIMAP) Then
                LogOutput(enumLogType.ERROR, "[func_transfer_upload]: could not connect to an IMAP server.")
                Return False
            End If

            'check if this is the last segment and terminate iteration when complete
            If intSeg = totalSegs Then
                bFinished = True
            End If

            'Dim HashSeg As New Hashtable
            Dim objSeg As New objSegment

            'calculate entrypoint
            If (intSeg * EntryPoint) > fInfo.Length Then
                EntryPoint = fInfo.Length - ((intSeg - 1) * EntryPoint)
                Do Until EntryPoint Mod 3 = 0
                    EntryPoint += 1
                Loop
                bFinished = True
            End If

            'update UI status
            props.StatusMessage = "Encoding segment..."
            Application.DoEvents()

            Dim buffer(EntryPoint) As Byte

            'read byte stream to string
            Try
                rChunk.Read(buffer, 0, EntryPoint)
            Catch ex As Exception
                LogOutput(enumLogType.ERROR, String.Format("An exception occurred when attempting to read file: {0}; Entry-point: {1}; Exception output: {2}", fItem, EntryPoint, ex.Message))
                Return False
            End Try

            'encode saved chunk and prep for transfer
            'encrypt data if specified by user
            Dim fdata As String = Nothing
            If bIsEncrypted Then
                props.StatusMessage = "Encrypting segment..."
                Application.DoEvents()
                fdata = rEncrypt(buffer, strProfilePassword)
            Else
                fdata = Convert.ToBase64String(buffer, 0, EntryPoint)
            End If

            Dim enc As New System.Security.Cryptography.MD5CryptoServiceProvider
            Dim segMD5 As String = ByteArrayToString(enc.ComputeHash(System.Text.ASCIIEncoding.ASCII.GetBytes(fdata)))

            Dim fDataArr As New ArrayList
            Dim bDone As Boolean = False

            'pull data from encoded temp file in chunks and add to msg arraylist
            Dim j As Long = 0

            'Copy the global const to a temp value that can be changed later
            Dim tChunk As Int64 = uChunk

            'turn data into octets
            Do Until bDone = True
                If (j + tChunk) >= fdata.Length Then
                    tChunk = fdata.Length - j
                    bDone = True
                End If
                fDataArr.Add(fdata.Substring(j, tChunk))
                j += tChunk
            Loop

            props.StatusMessage = String.Empty

            'create MIME boundry
            Dim strBound As String = RandomString(9, False) & ":::" & RandomString(5, False) & ":" & RandomString(2, False)

            Dim eSeg As String = Convert.ToString(intSeg)
            Do Until eSeg.Length = Convert.ToString(totalSegs).Length
                eSeg = "0" & eSeg
            Loop

            'prevent bug that sometimes assigns a value of 0 to UID and screws up downloads.
            If smIMAP.int_UIDNext = 0 OrElse smIMAP.b_FolderSelected = False Then
                If Not smIMAP.SelectMailBox() Then
                    LogOutput(enumLogType.ERROR, "[IMAPDownload]: Could not select application mailbox folder.")
                    Return False
                End If
                'check if it is still 0
                If smIMAP.int_UIDNext = 0 Then
                    LogOutput(enumLogType.ERROR, "[IMAPDownload]: Could not obtain a [UIDNEXT] value from the IMAP server.")
                    Return False
                End If
            End If

            'create body
            Dim strBody As String = "Filename: " & fName & strEOL & _
                                    "Total Size: " & ReturnNiceSize(2, , fSize) & strEOL & _
                                    "Encoded Size: " & ReturnNiceSize(2, , func_CalculateEncodedSize(fSize)) & strEOL & _
                                    "MD5 Checksum: " & func_MD5HashFromFile(fItem) & strEOL & _
                                    "Archive version: " & intArc & strEOL & _
                                    "Total Segments: " & totalSegs

            If totalSegs > 1 Then
                strBody += strEOL & strEOL & _
                            "Segment Number: " & intSeg & " of " & totalSegs & strEOL & _
                            "Segment Size: " & ReturnNiceSize(2, , fdata.Length) & strEOL & _
                            "Segment MD5: " & segMD5 & strEOL & _
                            "Date Uploaded: " & DateAndTime.Now

                strBody += strEOL & strEOL & "This email contains a partial file. It will not function properly unless merged with the rest of its archive."
            End If

            'generate ghetto xml subject line suffix
            Dim strSubject As String = remotepath & "  " & _
                                    "<arc>" & intArc.ToString & "<;arc>" & _
                                    "<seg>" & eSeg & ":" & totalSegs & "<;seg>" & _
                                    "<size>" & fSize & "<;size>" & _
                                    "<psize>" & EntryPoint & "<;psize>" & _
                                    "<uid>" & smIMAP.int_UIDNext & "<;uid>" & _
                                    "<md5>" & segMD5 & "<;md5>"

            If intSeg = 1 Then
                strSubject += "<sum>" & func_MD5HashFromFile(fItem) & "<;sum>"
            End If

            Dim strSubjectCopy As String = strSubject

            If bIsEncrypted Then
                strSubject = rEncrypt(strSubject, strProfilePassword)

                strBody = "This email contains an encrypted file. It must be decrypted before use."
                strSubject += str_EncodedFlag
                strSubjectCopy += str_EncodedFlag
                objSeg.IsEncrypted = True
            End If

            objSeg.FilePath = remotepath
            objSeg.EmailAddress = smIMAP.str_Email
            objSeg.SizeConCat = EntryPoint
            EnumerateProperties(strSubjectCopy, objSeg)

            'Create arraylist for lines of message
            Dim arrMsg As New ArrayList

            'add message lines to arraylist
            arrMsg.Add("Date: " & DateTime.Now & strEOL)
            arrMsg.Add("From: Me <" & smIMAP.str_Email & ">" & strEOL)
            arrMsg.Add("Subject: " & strSubject & strEOL)
            arrMsg.Add("To: Me <" & smIMAP.str_Email & ">" & strEOL)
            arrMsg.Add("MIME-Version: 1.0" & strEOL)
            arrMsg.Add("Content-Type: MULTIPART/mixed; BOUNDARY=" & Chr(34) & strBound & Chr(34) & strEOL)
            arrMsg.Add(strEOL)
            arrMsg.Add("--" & strBound & strEOL)
            arrMsg.Add("Content-Type: TEXT/plain; CHARSET=US-ASCII" & strEOL)
            arrMsg.Add(strEOL)
            arrMsg.Add(strBody)
            arrMsg.Add(strEOL)
            arrMsg.Add("--" & strBound & strEOL)
            arrMsg.Add("Content-Type: APPLICATION/octet-stream" & strEOL)
            arrMsg.Add("Content-Transfer-Encoding: BASE64" & strEOL)
            If bIsEncrypted Then
                'dont give away information about the files in encrypted mode.
                Dim sRand As String = RandomString(10, True)
                arrMsg.Add("Content-Description: " & sRand & strEOL)
                arrMsg.Add("Content-Disposition: inline; filename=" & Chr(34) & sRand & chrPeriod & intSeg & Chr(34) & strEOL)
            Else
                arrMsg.Add("Content-Description: " & fName & strEOL)
                If intSeg > 1 Then
                    arrMsg.Add("Content-Disposition: inline; filename=" & Chr(34) & fName & chrPeriod & intSeg & Chr(34) & strEOL)
                Else
                    arrMsg.Add("Content-Disposition: inline; filename=" & Chr(34) & fName & Chr(34) & strEOL)
                End If
            End If

            arrMsg.Add(strEOL)
            For Each line As String In fDataArr
                arrMsg.Add(line & strEOL)
            Next
            arrMsg.Add(strEOL)
            arrMsg.Add(strEOL)
            arrMsg.Add("--" & strBound & "--" & strEOL) 'final boundry line

            fDataArr.Clear()

            'calculate body size for APPEND command
            Dim nAppendLength As Integer = 0
            For Each line As String In arrMsg
                nAppendLength += line.Length
            Next

            Dim TempArray As New ArrayList
            Dim sCommand As String = "APPEND " & strAppName & " (\Seen) {" & nAppendLength.ToString & "}"
            Dim eImapResponse As enumIMAPResponse
            eImapResponse = smIMAP.IMAPInterface(sCommand, TempArray, True, True, True)

            If eImapResponse = enumIMAPResponse.imapBeginAppend Then
                Dim z As Integer = 0
                For Each line As String In arrMsg
                    If z >= arrMsg.Count - 1 Then
                        '==================
                        'read respone after final message line

                        'if this is the last seg, move progress to 100%
                        If intSeg = totalSegs Then sItem.SubItems(4).Text = String.Format("Uploading ({0}%)", "100")

                        'create new arraylist for output from GodFunction
                        Dim ArrResult As New ArrayList

                        'create response enumerator for result of final line command
                        Dim eResponse As enumIMAPResponse

                        'send final line to IMAP server
                        eResponse = smIMAP.IMAPInterface(line, ArrResult, True, True, False)

                        If InStr(ArrayListToString(ArrResult), imap_res_OK) Then
                            LogOutput(enumLogType.INFO, String.Format("Successfully uploaded: {0} segment {1} of {2}", fName, intSeg, totalSegs))

                            objSeg.DateValue = DateAndTime.Now
                            AddToCollection(colRemoteFileData, objSeg)

                            Dim ActObjUpdate As objAccount = colAccountList.Item(smIMAP.str_Email)
                            ActObjUpdate.AppendUsedBytes(func_CalculateEncodedSize(fSize))
                        Else
                            LogOutput(enumLogType.ERROR, String.Format("Could not download: {0}; Server response: {1}.", fName, ArrResult))
                            Return False
                        End If
                    Else
                        If smIMAP.IMAPInterface(line, New ArrayList, False, False, False) = enumIMAPResponse.imapFailure Then
                            Return False
                        End If
                    End If
                    bytecounter += line.Length
                    nByteRegister += line.Length

                    'progress updater
                    If Not fSize = 0 Then 'this line fixes error when processing a 0 byte file. 
                        'skip every fourth iteration to save processing power and make the upload transfer quicker
                        If z Mod 4 = 0 Then
                            Dim prog As Integer = Nothing
                            Dim iCalc As Long = Nothing
                            iCalc = bytecounter
                            If intSeg > 1 Then
                                iCalc += ((intSeg - 1) * fdata.Length)
                            End If
                            prog = Math.Floor(bytecounter / func_CalculateEncodedSize(fSize) * 100)
                            If Not prog = 100 OrElse Not sItem.SubItems(4).Text.IndexOf("100") <> -1 Then
                                If sItem.SubItems(4).Text.IndexOf("(" & prog.ToString & "%)") = -1 Then
                                    If prog <= 100 Then
                                        sItem.SubItems(4).Text = String.Format("Uploading ({0}%)", prog.ToString)
                                    End If
                                End If
                            End If
                        End If
                    End If

                    'check if there is a cancellation pending in the parent worker
                    If frmMain.bgwQueueHandler.CancellationPending Then
                        LogOutput(enumLogType.WARN, String.Format("Cancelled upload of {0}", fName))
                        smIMAP.LogOut()
                        Return False
                    End If

                    'incremment counter
                    z += 1
                Next
            Else
                LogOutput(enumLogType.ERROR, String.Format("An exception occurred while attempting to open an APPEND connection to server {0}. Last error from IMAP module: {1}", smIMAP.str_Host, smIMAP.str_LastError))
                Return False
            End If

            nByteRegister = 0

            smIMAP.SelectMailBox()
            ReDim buffer(0)
        Loop

        'return successful
        Return True
    End Function

End Module
