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
'    Filename: func_transfer_download.vb


Imports AirDrive.vars
Imports AirDrive.functions
Imports System.IO
Imports AirDrive.methods
Imports AirDrive.vars_IMAP
Imports AirDrive.objimap
Imports AirDrive.enums
Imports AirDrive.frmMain
Imports AirDrive.func_rcrypt
Imports System.Text

Public Module func_transfer_download

    Public Function IMAPDownload(ByRef lvi As ListViewItem) As Boolean

        'determine if lvi carries decryption flag
        Dim bEncrypted As Boolean = False

        'Pull local path name from list view item
        Dim sLocalPath As String = lvi.SubItems(0).Text.Trim

        'pull remote path name from list view item
        Dim sRemotePath As String = lvi.SubItems(2).Text.Trim

        'pull the file name from the remote path, format: (xxxxxxxx.ext)
        Dim sFileName As String = sRemotePath.Substring(sRemotePath.LastIndexOf("/") + 1)

        Dim intArc As Integer = 1
        If lvi.Tag.ToString.Contains("<arc>") AndAlso lvi.Tag.ToString.Contains("</arc>") Then
            intArc = Convert.ToInt32(TextBetween(lvi.Tag.ToString, "<arc>", "</arc>"))
        End If

        'change list view item show down that it is downloading...
        lvi.SubItems(4).Text = "Downloading"

        'check if the local file exists
        If System.IO.File.Exists(sLocalPath) Then
            'if local file already exists:

            'create now duplicate diag. windows and apply properties
            Dim frmDiag As New frmLocDup
            frmDiag.lblFname.Text = sFileName

            'create a variable to store response in
            Dim res As New Windows.Forms.DialogResult

            'show duplicate download dialog and gather user response
            'action codes to expect from dialog window:::(windows.forms.dialogresult))
            'YES - Rename
            'OK - Overwrite
            'ABORT - Skip
            res = frmDiag.ShowDialog()

            'handle file based on response
            If res = Windows.Forms.DialogResult.Abort Then
                '===SKIP

                'simply return this download as successful and move to next file
                Return True


            ElseIf res = Windows.Forms.DialogResult.Yes Then
                '===RENAME

                'get the root directory of output file
                Dim sRoot As String = sLocalPath.Substring(0, sLocalPath.LastIndexOf(chrBackslash) + 1)

                'separate file name and extension, if file has one
                'ensure that the '.' is not the last character in the name, which would cause an exception
                'also asign variables if file name has no extension
                Dim fn, fe As String
                If sFileName.Contains(chrPeriod) Then
                    'file does have an extension:

                    fn = sFileName.Substring(0, sFileName.LastIndexOf(chrPeriod))
                    fe = sFileName.Substring(sFileName.LastIndexOf(chrPeriod))
                Else
                    'file does not have an extension

                    fn = sFileName
                    fe = String.Empty
                End If

                'create integer that will increment the iteration through potential new/unique file names
                Dim i As Integer = 1

                'start finding an unused filename
                Do
                    'concatenate the new file name
                    Dim oNewOutputFileName As String = String.Format("{0}({1}){2}", fn, i, fe)

                    'create a new string to represent the full path of the hypothetical file
                    Dim oNewOutputPath As String = String.Format("{0}{1}", sRoot, oNewOutputFileName)

                    'test to see if new file name also exists in the 'root' on the local machine
                    'if it does not, then pass the new variable to the thread
                    If Not File.Exists(oNewOutputPath) Then
                        'pass new variables
                        sFileName = oNewOutputFileName
                        sLocalPath = oNewOutputPath
                        Exit Do
                    End If

                    'increment the counter
                    i += 1

                Loop


            ElseIf res = Windows.Forms.DialogResult.OK Then
                '===OVERWRITE

                Try
                    'attempt to delete existing file
                    System.IO.File.Delete(sLocalPath)

                Catch ex As Exception

                    'if exception is caught, log minor error
                    'continue with processing of this file
                    LogOutput(enumLogType.WARN, String.Format("Could not delete [{0}] in preparation for overwrite procedure. Continuing", sFileName))

                End Try
            End If
        End If


        'create variables that will store data about the first segment
        Dim sMasterFileTotalSegs As Int64 = 0
        Dim sMasterFileSize As Int64 = 0
        Dim sMasterFileMD5 As String = String.Empty
        Dim sMasterFileName As String = String.Empty

        'create the master file byte counter register
        Dim nBytes As Long = 0

        'create a minicollection to store all of the segment data required for this download
        Dim colMini As New Collection

        'enumerate the REMOTE FILE DATA collection
        For Each objSeg As objSegment In colRemoteFileData
            'load hashtable from current item
            sMasterFileName = objSeg.FilePath

            'check if there is a match with the desired filename
            If sMasterFileName = sRemotePath AndAlso objSeg.ArchiveNumber = intArc Then
                'if file does match:

                'determine the current segment number
                Dim seg As Integer = objSeg.Segment

                'check if it is the first segment
                'the first segment contains the most important information about the file
                'save this important information to be used later in the function
                If seg = 1 Then
                    sMasterFileTotalSegs = objSeg.TotalSegments
                    sMasterFileSize = objSeg.SizeTotal
                    sMasterFileMD5 = objSeg.MD5Total
                End If
                colMini.Add(objSeg, seg)

                'if it is the last segment of the archive, then exit the iteration
                If colMini.Count = sMasterFileTotalSegs Then Exit For
            End If
            'Loop
        Next

        'catch errors in file collection data
        If colMini.Count = 0 Then
            'No matching file found in entire collection
            LogOutput(enumLogType.ERROR, "Invalid filename in database. It is recommended that you refresh your file data from the IMAP servers")
            Return False
        ElseIf colMini.Count < sMasterFileTotalSegs Then
            'Incomplete segment data
            LogOutput(enumLogType.ERROR, "Invalid filename in database. It is recommended that you refresh your file data from the IMAP servers")
            Return False
        End If

        'create variable that can store the current segment number and ensure that segments are downloaded in order
        Dim iSeg As Integer = 1

        'begin iteration
        Do
            'check if the segment has gone out of range
            'return failure if out of range
            If iSeg > sMasterFileTotalSegs Then
                Return False
            End If

            'create segment registers data
            Dim suid As String = String.Empty
            Dim smd5 As String = String.Empty
            Dim email As String = String.Empty
            Dim size As Long = 0

            'Enumerate the MINICOLLECTION, find the correct segment's hashtable, and store the required information
            Dim iMini As IEnumerator = colMini.GetEnumerator
            Do While iMini.MoveNext
                Dim objSeg As objSegment = iMini.Current
                If objSeg.Segment = iSeg Then
                    smd5 = objSeg.MD5Seg
                    suid = objSeg.UID
                    email = objSeg.EmailAddress
                    size = objSeg.SizeConCat
                    bEncrypted = objSeg.IsEncrypted
                    Exit Do
                End If
            Loop

            'Invalid segment data
            If suid = String.Empty OrElse smd5 = String.Empty OrElse email = String.Empty OrElse sMasterFileName = String.Empty Then
                'if any segments are caught by this test, the whole file should be scrapped.
                Return False
            End If

            'check if the currenly logged in email account is the correct one.
            'connect to other account if not.
            'return false on failure.
            If Not CheckConnection(smIMAP, email) Then
                Return False
            End If

            'increment command value
            smIMAP.num_CommandValue += 1

            'create command string
            Dim sCommand As String = String.Format("{0}uid fetch {1} body[2]{2}", smIMAP.imap_cmd_Identifier, suid, strEOL) 'old line: smIMAP.IMAP_COMMAND_IDENTIFIER & "uid fetch " & suid & " body[2]" & strEOL

            'convert string to byte array to send into TCP stream
            Dim data As Byte() = System.Text.Encoding.ASCII.GetBytes(sCommand.ToCharArray())

            'write command to stream
            If smIMAP.b_SSL Then
                smIMAP.stream_SSL.Write(data, 0, data.Length)
            Else
                smIMAP.stream_NetStrm.Write(data, 0, data.Length)
            End If

            'create string builder so that massive files take up little RAM
            Dim sbDownload As New StringBuilder

            'create variables to store IMAP response
            Dim response As enumIMAPResponse = enumIMAPResponse.imapSuccess
            Dim sError As String = String.Empty

            Dim iCount As Integer = 0

            'begin looping through response lines
            Dim bRead As Boolean = True
            While bRead

                'create a variable to store the current line
                Dim line As String = String.Empty

                Try
                    'read line
                    line = If(smIMAP.b_SSL, smIMAP.obj_Reader.ReadLine(), smIMAP.stream_NetworkReader.ReadLine())
                Catch ex As Exception
                    'catch error, log it, return failure
                    LogOutput(enumLogType.ERROR, ex.Message)
                    Return False
                End Try

                'make sure that the read actually gathered data
                If Not line = Nothing Then
                    'append data to stringbuilder
                    sbDownload.Append(line.Trim)
                Else
                    'continue to next iteration if there is no data in line
                    'causes errors, not sure why
                    'Continue Do
                End If

                'add size to the global byte register, which calculates the rate of the transfer
                nByteRegister += line.Length

                'add size to the local byte register, which calculates the progress of this specific transfer
                nBytes += line.Trim.Length

                'calculate the progress percentage and show to user:
                If iCount Mod 4 = 0 Then
                    Dim percent As Int64 = nBytes / func_CalculateEncodedSize(sMasterFileSize) * 100
                    Dim sCurrent As String = lvi.SubItems(4).Text
                    If sCurrent.IndexOf("(" & percent & "%") = -1 AndAlso percent < 100 Then
                        lvi.SubItems(4).Text = String.Format("Download ({0}%)", percent)
                    End If
                End If

                'determine if response is final line and treat accordingly
                If line.StartsWith(smIMAP.imap_serv_res_OK) Then
                    response = enumIMAPResponse.imapSuccess
                    bRead = False
                ElseIf line.StartsWith(smIMAP.imap_serv_res_Bad) OrElse line.StartsWith(smIMAP.imap_serv_res_No) Then
                    response = enumIMAPResponse.imapFailure
                    sError = line
                    bRead = False
                Else
                    response = enumIMAPResponse.IMAP_IGNORE_RESPONSE
                End If
                LogOutput(enumLogType.IMAP, line)

                iCount += 1
            End While

            'zero the global byte register and blank the rate label on frmMain
            nByteRegister = 0
            frmMain.lblRate.Text = String.Empty

            If response = enumIMAPResponse.IMAP_IGNORE_RESPONSE OrElse response = enumIMAPResponse.imapFailure Then
                Dim sMessage As String = String.Format("Error downloading file {0} segment {1}; Server output: {2}; ({3})", sFileName, iSeg, sError, email)
                LogOutput(enumLogType.ERROR, sMessage)
                Return False
            End If

            Dim sResult As String = String.Empty
            sResult = sbDownload.ToString

            'remove the data out of the wrapper
            sResult = TextBetween(sResult, "}", ")").Trim

            Dim root As String = sLocalPath.Substring(0, sLocalPath.LastIndexOf(chrBackslash) + 1)

            If Not DirectoryWizard(root) Then
                Return False
            End If

            Dim sLocalPathTemp As String = sLocalPath & ".~tmp"

            Dim enc As New System.Security.Cryptography.MD5CryptoServiceProvider
            Dim testhash As String = ByteArrayToString(enc.ComputeHash(System.Text.ASCIIEncoding.ASCII.GetBytes(sResult)))

            If Not smd5 = testhash Then
                Dim sMessage As String = String.Format("An error occurred while downloading {0}. Segment {1} did not pass integrity check.", sFileName, iSeg)
                LogOutput(enumLogType.ERROR, sMessage)
                Return False
            End If

            'append segment to master file
            If bEncrypted Then
                Dim buff() As Byte = Nothing
                sResult = rDecrypt(sResult, strProfilePassword, buff).Trim
                ExportToFile(sLocalPathTemp, buff)
            Else
                If Not ExportToFile(sLocalPathTemp, sResult, True) Then
                    Return False
                End If
            End If

            'hide the temp file
            Dim attribute As System.IO.FileAttributes = FileAttributes.Hidden
            File.SetAttributes(sLocalPathTemp, attribute)

            'if this is the final segment of the archive...
            If iSeg = sMasterFileTotalSegs Then

                'update queue listview to processing
                lvi.SubItems(4).Text = "Processing..."
                Application.DoEvents()

                'extract the RAW base64 encoded temp file to the actual path
                If ExtractEncodedFile(sLocalPathTemp, sLocalPath, sMasterFileSize, bEncrypted) Then

                    'calculate the MD5 checksum of the decoded file
                    If Not sMasterFileMD5 = func_MD5HashFromFile(sLocalPath) Then
                        'if it does not match MD5 of original:

                        'clear status label
                        props.StatusMessage = String.Empty

                        'log error
                        LogOutput(enumLogType.ERROR, String.Format("An error occurred while building file: {0}; Completed file did not pass integrity check.", sFileName))

                        'delete temp file
                        Try
                            System.IO.File.Delete(sLocalPathTemp)
                        Catch ex As Exception
                            LogOutput(enumLogType.WARN, String.Format("Could not delete temporary file: {0}. Workstation message:  {1}", sLocalPathTemp, ex.Message))
                        End Try

                        'delete corrupt file
                        Try
                            System.IO.File.Delete(sLocalPath)
                        Catch ex As Exception
                            LogOutput(enumLogType.WARN, String.Format("Could not delete temporary file: {0}. Workstation message:  {1}", sLocalPath, ex.Message))
                        End Try

                        'return as failed download
                        Return False
                    End If


                    'delete local file upon successful decode/copy
                    Try
                        System.IO.File.Delete(sLocalPathTemp)
                    Catch ex As Exception
                        LogOutput(enumLogType.WARN, String.Format("Could not delete temporary file: {0}", sLocalPathTemp))
                    End Try

                    LogOutput(enumLogType.INFO, String.Format("Successfully downloaded: {0}", sFileName))

                    'return function as successful
                    Return True
                Else

                    LogOutput(enumLogType.ERROR, String.Format("Error decoding of file:  {0}.", sFileName))
                    Return False
                End If
            End If

            'increment counter
            iSeg += 1
        Loop

        'if execution somehow gets to this point, return failure
        LogOutput(enumLogType.ERROR, "An unhandled error occurred in download function.")
        Return False
    End Function

End Module
