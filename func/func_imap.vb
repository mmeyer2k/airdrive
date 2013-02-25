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
'    Filename: func_imap.vb

Imports AirDrive.enums
Imports AirDrive.objimap
Imports AirDrive.vars
Imports AirDrive.func_rcrypt

Public Module func_imap

    ''' <summary>
    ''' Gather toplevel mailboxes
    ''' </summary>
    ''' <param name="inst_objIMAP">Reference to objIMAP Instance</param>
    ''' <returns>false on failure</returns>
    ''' <remarks></remarks>
    Public Function OpenIMAPFolder(ByRef inst_objIMAP As objimap) As Boolean
        Dim aResult As New ArrayList 'create blank array list
        Dim eResponse As enumIMAPResponse 'create a response instance

        Dim strCommand As String = String.Format("LIST {0}{0} %", Chr(34)) 'format the command string

        eResponse = inst_objIMAP.IMAPInterface(strCommand, aResult, True, True, True)

        If eResponse = enumIMAPResponse.imapFailure OrElse eResponse = enumIMAPResponse.IMAP_IGNORE_RESPONSE OrElse eResponse = enumIMAPResponse.imapBeginAppend Then
            Return False
        End If

        Dim mbCheck As String = ArrayListToString(aResult)

        strCommand = String.Empty
        aResult.Clear()

        'find name in stack
        If mbCheck.IndexOf(strAppName) = -1 Then
            Console.WriteLine("WARN: No application folder found, creating one")

            'App mail box does not exist, create it
            strCommand = "CREATE " & strAppName
            eResponse = inst_objIMAP.IMAPInterface(strCommand, aResult, True, True, True)

            If Not eResponse = enumIMAPResponse.imapSuccess Then
                LogOutput(enumLogType.ERROR, String.Format("[OpenIMAPFolder]: Error creating IMAP folder. Last server response: {0}", smIMAP.str_LastError))
                Return False
            End If
        End If

        inst_objIMAP.SelectMailBox()
        If inst_objIMAP.b_FolderSelected Then
            Return True
        Else
            LogOutput(enumLogType.ERROR, String.Format("[OpenIMAPFolder]: Error selecting IMAP folder. Last server response: {0}", smIMAP.str_LastError))
            Return False
        End If
    End Function

    ''' <summary>
    ''' Return IMAP quota for given account
    ''' </summary>
    ''' <param name="inst_objIMAP">IMAP Instance</param>
    ''' <param name="inst_objAccount">Account object</param>
    ''' <returns>False on failure</returns>
    ''' <remarks></remarks>
    Public Function GetIMAPQuota(ByRef inst_objIMAP As objIMAP, ByRef inst_objAccount As objAccount) As Boolean

        Dim nUsedBytes, nTotalbytes As Long 'create integers to store the data in
        Dim bSuccess As Boolean = inst_objIMAP.ReturnQuota(nUsedBytes, nTotalbytes) 'use IMAP quota subroutine to populate variables

        If bSuccess Then    'check if the operation was successful

            inst_objAccount.IsFunctioning = True 'set the isFunctioning flag for the imap account object
            inst_objAccount.BytesTotal = nTotalbytes 'set total bytes
            inst_objAccount.BytesUsed = nUsedBytes 'set used bytes

            Return True

        Else   'handle nonfunctional IMAP connection
            LogOutput(enumLogType.ERROR, "Error: Attempt to retrieve quota information from server failed.")
            inst_objAccount.IsFunctioning = False
            Return False
        End If

    End Function

    Public Function IsResponeLine(ByRef sLine As String, ByRef eResponse As enumIMAPResponse, Optional ByRef sError As String = "") As Boolean
        Try
            sLine = sLine.Substring(sLine.IndexOf(chrSpace)).Trim

            If sLine.StartsWith(imap_res_OK) Then
                LogOutput(enumLogType.IMAP, sLine)
                eResponse = enumIMAPResponse.imapSuccess
                Return True
            ElseIf sLine.StartsWith(smIMAP.imap_serv_res_No) Then
                LogOutput(enumLogType.IMAP, sLine)
                sError = sLine
                eResponse = enumIMAPResponse.imapFailure
                Return True
            ElseIf sLine.StartsWith(smIMAP.imap_serv_res_Bad) Then
                LogOutput(enumLogType.IMAP, sLine)
                sError = sLine
                eResponse = enumIMAPResponse.imapFailure
                Return True
            ElseIf sLine.StartsWith(chrPlus) Then
                eResponse = enumIMAPResponse.imapBeginAppend
                LogOutput(enumLogType.IMAP, sLine)
                Return True
            Else
                eResponse = enumIMAPResponse.IMAP_IGNORE_RESPONSE
                LogOutput(enumLogType.IMAP, sLine)
                Return False
            End If
        Catch ex As Exception
            Return False
            sError = ex.Message
        End Try

    End Function

    Public Function EnumerateProperties(ByRef subLine As String, ByRef xObj As objSegment) As String

        Dim strProperties As String() = {"seg", "arc", "md5", "size", "uid", "sum", "enc", "psize"}    'array with all of the possible subject property tags

        For Each strProperty In strProperties    'loop through the tags to find matches in subject string
            Dim strTags As String() = {"<" & strProperty & ">", _
                                       "<;" & strProperty & ">"}    'create the current XML tag boundries and load into string array

            Dim strValue As String = String.Empty

            If subLine.IndexOf(strTags(0)) <> -1 AndAlso subLine.IndexOf(strTags(1)) <> -1 Then     'make sure subject string contains both tags

                strValue = TextBetween(subLine, strTags(0), strTags(1))


                If strProperty = "seg" Then
                    'seg format: <seg>1:6<;seg>     (segment one of six)
                    Dim segs As String = String.Empty 'split segment data
                    segs = strValue.Substring(strValue.IndexOf(":") + 1)    'parse total segment number
                    xObj.TotalSegments = segs 'implicit type conversion
                    strValue = strValue.Substring(0, strValue.IndexOf(":"))  'parse current segment number
                    xObj.Segment = strValue

                ElseIf strProperty = "arc" Then
                    xObj.ArchiveNumber = strValue

                ElseIf strProperty = "md5" Then
                    xObj.MD5Seg = strValue

                ElseIf strProperty = "sum" Then
                    xObj.MD5Total = strValue

                ElseIf strProperty = "size" Then
                    xObj.SizeTotal = strValue

                ElseIf strProperty = "uid" Then
                    xObj.UID = strValue

                ElseIf strProperty = "enc" Then
                    xObj.IsEncrypted = True

                ElseIf strProperty = "psize" Then
                    xObj.SizeConCat = strValue

                End If
            End If
        Next

        'only need a date value for the first segment of a file.
        'this will save room in the fXML
        'could also possibly include totalsegs, totalsize, sum
        If xObj.Segment > 1 Then
            xObj.DateValue = Nothing
        End If

        Return subLine.Substring(0, subLine.IndexOf("<")).Trim    'return subject without flags

    End Function

    Public Function ParseEnvelope(ByRef smIMAP As objIMAP, ByVal sBodyLine As String, ByRef xObj As objSegment) As Boolean
        ' remove any whitespaces at the beginning and the end
        sBodyLine = sBodyLine.Trim()

        If sBodyLine.IndexOf("(UID") = -1 Then
            LogOutput(enumLogType.ERROR, "Improperly formatted header line: " & sBodyLine)
            Return False
        End If

        Dim suid As String = TextBetween(sBodyLine, "(UID", "ENVELOPE")

        xObj.UID = Convert.ToInt32(suid)
        xObj.EmailAddress = smIMAP.str_Email

        Try
            'date
            Dim sTemp As String = String.Empty
            If Not func_ParseQuotedString(sBodyLine, sTemp) Then
                LogOutput(enumLogType.[ERROR], "Invalid Message Envelope Date.")
                Return False
            End If
            If sTemp.Length > 0 Then
                xObj.DateValue = Convert.ToDateTime(sTemp)
            End If

            ' Subject
            If Not func_ParseQuotedString(sBodyLine, sTemp) Then
                LogOutput(enumLogType.[ERROR], "Invalid Message Envelope Subject.")
                Return False
            End If
            If sTemp.Length > 0 Then
                If sTemp.Contains(str_EncodedFlag) Then   'check if the subject string has been encrypted
                    Dim c As String = sTemp.Replace(str_EncodedFlag, String.Empty)
                    c = rDecrypt(c, strProfilePassword)
                    sTemp = c & str_EncodedFlag
                End If

                'create string for subject line
                'populate this string by calling the EnumerateProperties function
                'this will populate the objects properties
                Dim sNewSubject As String = String.Empty
                sNewSubject = EnumerateProperties(sTemp, xObj)
                xObj.FilePath = sNewSubject
            End If
        Catch ex As Exception
            LogOutput(enumLogType.ERROR, ex.Message)
            Return False
        End Try
        Return True
    End Function

    Public Function func_ParseQuotedString(ByRef sBodyStruct As String, ByRef strOutputString As String) As Boolean
        strOutputString = String.Empty

        sBodyStruct = sBodyStruct.Trim()

        If sBodyStruct.IndexOf(Chr(34)) <> -1 Then
            ' extract the part within first set of quotes
            sBodyStruct = sBodyStruct.Substring(sBodyStruct.IndexOf(chrQuote) + 1)
            strOutputString = sBodyStruct.Substring(0, sBodyStruct.IndexOf(chrQuote))
            sBodyStruct = sBodyStruct.Substring(sBodyStruct.IndexOf(chrQuote) + 1)
            Return True
        End If

        If sBodyStruct.IndexOf("OK") = -1 Then
            Dim sLog As String = "Invalid Body Structure " & sBodyStruct & chrPeriod
            LogOutput(enumLogType.[ERROR], sLog)
            Return False
        End If

    End Function

End Module
