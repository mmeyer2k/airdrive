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
'    Filename: objIMAP.vb

#Region "Imports"
Imports System
Imports System.Collections
Imports System.IO
Imports System.Xml
Imports System.Net.Mail
Imports AirDrive.functions
Imports AirDrive.vars
Imports AirDrive.methods
Imports System.Net.Sockets
Imports AirDrive.enums
Imports AirDrive.vars_IMAP
Imports System.Net.Security
Imports System.Security.Cryptography.X509Certificates
Imports System.Security.Authentication
#End Region

Public Class objIMAP

#Region "Global Varriables"

    'strings
    Public str_MailBoxName As String = String.Empty
    Protected str_CommandPrefix As String = RandomString(2, True)
    Protected str_UserID As String = String.Empty
    Public str_Email As String = String.Empty
    Public str_LastError As String = String.Empty
    Public str_Host As String = String.Empty
    Protected str_Password As String = String.Empty

    'booleans
    Public b_FolderSelected As Boolean = False
    Public b_LoggedIn As Boolean = False
    Public b_SSL As Boolean = True
    Protected b_Connected As Boolean = False
    Protected b_unclearedsockerror As Boolean = False

    'numbers
    Public int_UIDNext As Int32 = 0
    Public num_CommandValue As UShort = 0
    Public int_TotalMessages As Integer = 0
    Protected int_port As UShort = imap_num_DefaultPort

    'network objects
    Private tcp_Server As TcpClient
    Public stream_NetStrm As NetworkStream
    Public stream_NetworkReader As StreamReader
    Public stream_SSL As System.Net.Security.SslStream
    Public obj_Reader As StreamReader

#End Region

#Region "Properties"

    Public ReadOnly Property imap_cmd_Identifier() As String
        Get
            Return str_CommandPrefix & num_CommandValue.ToString() & chrSpace
        End Get
    End Property

    Public ReadOnly Property imap_serv_res_OK() As String
        Get
            Return imap_cmd_Identifier & imap_res_OK
        End Get
    End Property

    Public ReadOnly Property imap_serv_res_No() As String
        Get
            Return imap_cmd_Identifier & imap_res_No
        End Get
    End Property

    Public ReadOnly Property imap_serv_res_Bad() As String
        Get
            Return imap_cmd_Identifier & imap_res_Bad
        End Get
    End Property

#End Region

#Region "Public Functions"

    Public Function DetermineCapability() As Boolean
        Dim eImapRes As enumIMAPResponse = enumIMAPResponse.imapSuccess
        Dim sResultArray As New ArrayList()
        Try
            eImapRes = IMAPInterface(imap_cmd_Capability, sResultArray, True, True, True)
            If eImapRes <> enumIMAPResponse.imapSuccess Then
                Throw New Exception("[capability]: Error occurred during CAPABILITY command. Last IMAP response was: " & str_LastError)
            End If
        Catch e As Exception
            Throw e
        End Try
        Return True
    End Function

    Public Function DisconnectStream() As Boolean
        num_CommandValue = 0
        If b_Connected Then
            If stream_NetStrm IsNot Nothing Then
                stream_NetStrm.Close()
            End If
            If stream_NetworkReader IsNot Nothing Then
                stream_NetworkReader.Close()
            End If
        End If
        Return True
    End Function

    Public Function Expunge() As Boolean
        Dim e As enumIMAPResponse
        e = IMAPInterface("EXPUNGE", New ArrayList, True, True, True)
        If e <> enumIMAPResponse.imapSuccess Then
            LogOutput(enumLogType.ERROR, "EXPUNGE command error. " & str_LastError)
            Return False
        Else
            LogOutput(enumLogType.INFO, "Successfully executed EXPUNGE command")
            Return True
        End If
    End Function

    Public Function IMAPInterface(ByVal cmd As String, _
                                   ByRef sResultArray As ArrayList, _
                                   ByVal bReadResult As Boolean, _
                                   ByVal bAppendEOL As Boolean, _
                                   ByVal bCommandPrefix As Boolean, _
                                   Optional ByRef sCopyUID As String = "") _
                                   As enumIMAPResponse

        If b_unclearedsockerror Then
            Exit Function 'forces reconnection to clear socket error
        End If

        Dim strCommand As String = cmd 'create command

        'prefix with command value if specified
        If bCommandPrefix Then
            num_CommandValue += 1
            strCommand = imap_cmd_Identifier & strCommand
        End If

        If tcp_Server IsNot Nothing Then
            tcp_Server.SendTimeout = 50000
        Else
            Return enumIMAPResponse.imapFailure
        End If

        'append EOL value if specified
        If bAppendEOL Then
            strCommand += strEOL
        End If

        Dim data As Byte() = System.Text.Encoding.ASCII.GetBytes(strCommand.ToCharArray())
        LogOutput(enumLogType.IMAPCom, strCommand)

        Try
            Do
                If b_SSL Then
                    stream_SSL.Write(data, 0, data.Length)
                Else
                    stream_NetStrm.Write(data, 0, data.Length)
                End If

                If bReadResult = False Then
                    Return enumIMAPResponse.imapSuccess
                End If

                While True
                    Dim strResult As String = String.Empty

                    If b_SSL Then
                        strResult = obj_Reader.ReadLine
                    Else
                        strResult = stream_NetworkReader.ReadLine()
                    End If

                    sResultArray.Add(strResult)

                    If strResult.StartsWith(imap_serv_res_OK) Then
                        If strResult.Contains(const_str_copyuid) Then    'gather copyUID if available. needed for deleting items.
                            Dim arrTempArray() As String = TextBetween(strResult, "[", "]").Split(chrSpace)    'this gathers the numbers in the result and moves them to an array
                            sCopyUID = arrTempArray(3)    'grab final number of the array, the copyUID
                        End If

                        LogOutput(enumLogType.IMAP, strResult)
                        Return enumIMAPResponse.imapSuccess

                    ElseIf strResult.StartsWith(imap_serv_res_No) Then
                        LogOutput(enumLogType.IMAP, strResult)
                        str_LastError = StripCommandIdentifier(strResult)
                        Return enumIMAPResponse.imapFailure

                    ElseIf strResult.StartsWith(imap_serv_res_Bad) Then
                        LogOutput(enumLogType.IMAP, strResult)
                        str_LastError = StripCommandIdentifier(strResult)
                        Return enumIMAPResponse.imapFailure

                    ElseIf strResult.StartsWith(chrPlus) Then
                        LogOutput(enumLogType.IMAP, StripCommandIdentifier(strResult))
                        Return enumIMAPResponse.imapBeginAppend

                    Else
                        LogOutput(enumLogType.IMAP, strResult)
                    End If

                End While
            Loop
        Catch e As Exception
            b_unclearedsockerror = True
            LogOutput(enumLogType.ERROR, String.Format("IMAP socket stream could not be read or written to. System message: ", e.Message))
            LogOut()
            Return enumIMAPResponse.imapFailure
        End Try

    End Function

    Public Function Login(ByVal sHost As String, _
                          ByVal nPort As UShort, _
                          ByVal bSSLEnabled As Boolean, _
                          ByVal sUserId As String, _
                          ByVal sPassword As String) _
                          As Boolean

        'create IMAP response register
        Dim imapResponse As enumIMAPResponse = enumIMAPResponse.imapSuccess

        If b_Connected Then    'check if the connection has already been made
            If b_LoggedIn Then    'check if logged in
                If str_Host = sHost AndAlso int_port = nPort Then     'check if other identifier data matches
                    If str_UserID = sUserId AndAlso str_Password = sPassword Then
                        LogOutput(enumLogType.INFO, String.Format("Attempted connection to {0} will be aborted. Connected and logged in to this account already.", str_Host))
                        Return True 'return true because connection is good
                    Else
                        LogOut()
                    End If
                Else
                    DisconnectStream()
                End If
            End If
        End If

        'set the boolean connection indicators to false as base result
        b_Connected = False
        b_LoggedIn = False

        Try
            imapResponse = Connect(sHost, nPort)    'attempt the connection to the IMAP server
            If imapResponse = enumIMAPResponse.imapSuccess Then    'if connection to IMAP server is successful
                b_Connected = True    'set connect state to true
                str_Email = sUserId    'add email value
            Else    'log error, set not functioning property, and return as fail
                LogOutput(enumLogType.ERROR, String.Format("Could not make connection to {0}:{1}. Verify internet connection and hostname.", str_Host, int_port))
                Return False
            End If
        Catch e As Exception
            LogOutput(enumLogType.ERROR, "An error occurred while connecting to server: " & e.Message)
            Return False
        End Try

        'create a new arraylist to store output
        Dim arrResultArray As New ArrayList()

        'create the command string
        Dim strCommand As String = imap_cmd_LogIn & chrSpace & sUserId & chrSpace & sPassword

        Try

            imapResponse = IMAPInterface(strCommand, arrResultArray, True, True, True)
            If imapResponse = enumIMAPResponse.imapSuccess Then
                b_LoggedIn = True
                b_unclearedsockerror = False
                str_UserID = sUserId
                str_Password = sPassword
                b_SSL = bSSLEnabled
            Else
                b_LoggedIn = False '<--bug fix. improperly reporting that EU is connected.
                LogOutput(enumLogType.ERROR, String.Format("Could not log in to {0}. Login rejected. Server response:  {1}", str_Email, str_LastError))
                Return False
            End If
        Catch e As Exception
            'log an error and return the function as failed
            LogOutput(enumLogType.ERROR, String.Format("Could not connect to server: {0}", e.Message))
            Return False
        End Try

        'log successful result
        LogOutput(enumLogType.INFO, String.Format("Logged in to {0}", str_Email))

        Return True
    End Function

    Public Function LogOut() As Boolean
        If b_LoggedIn OrElse b_Connected Then
            Dim imapResponse As enumIMAPResponse
            Dim arrResultArray As New ArrayList()
            Dim strCommand As String = imap_cmd_LogOut
            Try
                tcp_Server.SendTimeout = 50000
                imapResponse = IMAPInterface(strCommand, arrResultArray, True, True, True)
            Catch e As Exception
                DisconnectStream()
                str_LastError = e.Message
                LogOutput(enumLogType.ERROR, String.Format("Failure terminating connection; {0}", e.Message))
                Return False
            End Try
            DisconnectStream()
            b_Connected = False
            b_LoggedIn = False
            Return True
        End If
    End Function

    Public Function ReturnQuota(ByRef intUsed As Long, _
                                ByRef intTotal As Long) _
                                As Boolean

        Dim imapResponse As enumIMAPResponse = enumIMAPResponse.imapSuccess 'create blank imap response enumerator variable

        'create blank boolean variable to store the status of the request
        Dim bResult As Boolean = False

        'generate the variables to fill with quota information
        intUsed = 0
        intTotal = 0

        'check if the imap session is logged in
        If Not b_LoggedIn OrElse Not b_Connected Then
            If Not RestoreStream() Then
                LogOutput(enumLogType.ERROR, "[GetQuota]: Attempted restore of connection failed due to insufficient data. Aborting.")
                Return False
            End If
        End If

        Dim arrResultArray As New ArrayList()
        Dim strCommand As String = imap_cmd_GetQuota & chrSpace & strAppName

        Try
            imapResponse = IMAPInterface(strCommand, arrResultArray, True, True, True)
            If imapResponse = enumIMAPResponse.imapSuccess Then
                str_MailBoxName = strAppName

                Dim quotaPrefix As String = imap_res_Untagged & chrSpace
                quotaPrefix += imap_res_Quota & chrSpace

                For Each strLine As String In arrResultArray
                    If strLine.StartsWith(quotaPrefix) = True Then
                        If strLine.IndexOf(quotaPrefix) <> -1 Then
                            Dim sString = TextBetween(strLine, "(", ")")
                            Dim arrSplit() As String = sString.Split(chrSpace)
                            If arrSplit(0) = "STORAGE" Then
                                bResult = True
                                intUsed = Convert.ToInt64(arrSplit(1), 10) * 1024
                                intTotal = Convert.ToInt64(arrSplit(2), 10) * 1024
                            End If
                        End If
                    End If
                Next

                If Not bResult Then
                    LogOutput(enumLogType.ERROR, String.Format("[GetQuota]: Failure getting the quota information for the folder/mailbox on {0}.", str_Email))
                    Return False
                End If

                Dim sLogStr As String = String.Format("Quota returned for [{0}]: [used={1}] [total={2}]", str_Email, ReturnNiceSize(3, , intUsed), ReturnNiceSize(3, , intTotal))
                LogOutput(enumLogType.INFO, sLogStr)
            End If
        Catch e As Exception
            str_LastError = e.Message
            LogOutput(enumLogType.ERROR, String.Format("[GetQuota]: Unhandled exception raised when retrieving quota information:  {0}", e.Message))
            Return False
        End Try
        Return True
    End Function

    Public Function SelectMailBox(Optional ByVal mBoxName As String = Nothing) As Boolean
        If mBoxName = Nothing Then mBoxName = strAppName

        If Not b_LoggedIn Then
            If Not RestoreStream() Then
                LogOutput(enumLogType.ERROR, "Connection could not be restored for [selectfolder] function.")
                Return False
            End If
        End If

        Dim arrResultArray As New ArrayList()
        Dim strCmd As String = String.Format("{0} {1}", imap_cmd_Select, mBoxName)

        Try
            If IMAPInterface(strCmd, arrResultArray, True, True, True) = enumIMAPResponse.imapSuccess Then
                str_MailBoxName = strAppName
                b_FolderSelected = True
            Else
                If Not CreateFolder() Then
                    b_FolderSelected = False
                    Return False
                Else
                    arrResultArray.Clear()
                    If IMAPInterface(strCmd, arrResultArray, True, True, True) = enumIMAPResponse.imapSuccess Then
                        b_FolderSelected = True
                    End If
                End If
            End If
        Catch e As Exception
            LogOutput(enumLogType.ERROR, String.Format("An error occurred in [selectfolder]: {0}", e.Message))
            Return False
        End Try

        For Each strLine As String In arrResultArray
            If strLine.IndexOf(imap_res_Untagged) <> -1 Then    'If this is an unsolicited response starting with '*' then...
                If strLine.IndexOf("EXISTS") <> -1 Then
                    int_TotalMessages = Convert.ToInt32(TextBetween(strLine, "*", "EXISTS"))
                End If

                'added to grab UIDNext value
                If strLine.IndexOf("UIDNEXT") <> -1 Then
                    int_UIDNext = Convert.ToInt32(TextBetween(strLine, "OK [UIDNEXT", "]").Trim)
                End If
            End If
        Next
        Return True
    End Function

    Public Function func_UploadMessage(ByVal subject As String, _
                                       ByVal body As String, _
                                       Optional ByVal attachment As String = "", _
                                       Optional ByVal fname As String = "") As Boolean

        If Not b_Connected OrElse Not b_LoggedIn Then
            RestoreStream()
        ElseIf subject = String.Empty Then
            Exit Function
        End If

        Dim arrMsg As New ArrayList
        Dim strbound As String = RandomString(9, False) & ":::" & RandomString(5, False) & ":" & RandomString(2, False)


        Dim fDataArr As New ArrayList
        Dim bDone As Boolean = False

        'pull data from encoded temp file in chunks and add to msg arraylist
        Dim j As Long = 0

        'Copy the global const to a temp value that can be changed later
        Dim tChunk As Int64 = uChunk

        'turn data into octets
        Do Until bDone = True
            If (j + tChunk) >= attachment.Length Then
                tChunk = attachment.Length - j
                bDone = True
            End If
            fDataArr.Add(attachment.Substring(j, tChunk))
            j += tChunk
        Loop

        'add message lines to arraylist
        arrMsg.Add("Date: " & DateTime.Now & strEOL)
        arrMsg.Add("From: Me <" & str_Email & ">" & strEOL)
        arrMsg.Add("Subject: " & subject & strEOL)
        arrMsg.Add("To: Me <" & str_Email & ">" & strEOL)
        arrMsg.Add("MIME-Version: 1.0" & strEOL)
        arrMsg.Add("Content-Type: MULTIPART/mixed; BOUNDARY=" & Chr(34) & strbound & Chr(34) & strEOL)
        arrMsg.Add(strEOL)
        arrMsg.Add("--" & strbound & strEOL)
        arrMsg.Add("Content-Type: TEXT/plain; CHARSET=US-ASCII" & strEOL)
        arrMsg.Add(strEOL)
        arrMsg.Add(body)

        If attachment IsNot Nothing Then
            arrMsg.Add(strEOL)
            arrMsg.Add("--" & strbound & strEOL)
            arrMsg.Add("Content-Type: APPLICATION/octet-stream" & strEOL)
            arrMsg.Add("Content-Transfer-Encoding: BASE64" & strEOL)
            arrMsg.Add("Content-Description: " & fname & strEOL)
            arrMsg.Add("Content-Disposition: inline; filename=" & Chr(34) & fname & Chr(34) & strEOL)
            arrMsg.Add(strEOL)
            For Each line As String In fDataArr
                arrMsg.Add(line & strEOL)
            Next

        End If

        arrMsg.Add(strEOL)
        arrMsg.Add(strEOL)
        arrMsg.Add("--" & strbound & "--" & strEOL) 'final boundry line

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

                    'create new arraylist for output from GodFunction
                    Dim ArrResult As New ArrayList

                    'create response enumerator for result of final line command
                    Dim eResponse As enumIMAPResponse

                    'send final line to IMAP server
                    eResponse = smIMAP.IMAPInterface(line, ArrResult, True, True, False)

                    If InStr(ArrayListToString(ArrResult), imap_res_OK) Then
                        Return True
                    Else
                        Return False
                    End If
                Else
                    smIMAP.IMAPInterface(line, New ArrayList, False, False, False)
                End If

                nByteRegister += line.Length

                'check if there is a cancellation pending in the parent worker
                If frmMain.bgwQueueHandler.CancellationPending Then
                    LogOutput(enumLogType.WARN, String.Format("Cancelled upload of {0}", fname))
                    smIMAP.LogOut()
                    Return False
                End If

                z += 1    'incremment counter
            Next
        Else
            LogOutput(enumLogType.ERROR, String.Format("[UploadMessage]: An exception occurred while attempting to open an APPEND connection to server {0}. Last error from IMAP module: {1}", smIMAP.str_Host, smIMAP.str_LastError))
            Return False
        End If

    End Function

#End Region

#Region "Protected Functions"

    Protected Function Connect(ByVal sHost As String, ByVal nPort As Integer, Optional ByVal bSSL As Boolean = True) As enumIMAPResponse
        If sHost.Length = 0 OrElse nPort = Nothing OrElse bSSL = Nothing Then
            LogOutput(enumLogType.ERROR, "Invalid parameter passed to [connect] function, hostname can not be null. Check account information.")
            Return enumIMAPResponse.imapFailure
        End If

        num_CommandValue = 0
        Dim eImapResponse As enumIMAPResponse = enumIMAPResponse.imapSuccess
        Try
            tcp_Server = New TcpClient(sHost, nPort)
            stream_NetStrm = tcp_Server.GetStream()
            stream_NetworkReader = New StreamReader(tcp_Server.GetStream())

            If bSSL Then
                stream_SSL = New System.Net.Security.SslStream(tcp_Server.GetStream(), False, New RemoteCertificateValidationCallback(AddressOf ValidateServerCert), Nothing)
                Try
                    stream_SSL.AuthenticateAsClient(sHost)
                Catch ex As AuthenticationException
                    LogOutput(enumLogType.ERROR, String.Format("Exception occurred while creating the socket connection to [{0}:]. Aborting...", sHost, nPort))
                    tcp_Server.Close()

                    Return enumIMAPResponse.imapFailure
                End Try

                obj_Reader = New StreamReader(stream_SSL)

            End If

            Dim sResult As String = If(bSSL, obj_Reader.ReadLine(), stream_NetworkReader.ReadLine())

            If sResult.StartsWith(imap_res_serv_OK) = True Then
                LogOutput(enumLogType.INFO, String.Format("Connection to {0} was successful.", sHost))
                LogOutput(enumLogType.IMAP, sResult)
                eImapResponse = enumIMAPResponse.imapSuccess
                DetermineCapability()
            Else
                LogOutput(enumLogType.IMAP, sResult)
                eImapResponse = enumIMAPResponse.imapFailure
            End If
        Catch ex As Exception
            LogOutput(enumLogType.ERROR, String.Format("An error occurred in the [connect] function. [host={0}] [port={1}] [ssl={2}]; System message: {3}", sHost, nPort, bSSL, ex.Message))
            eImapResponse = enumIMAPResponse.imapFailure
        End Try
        str_Host = sHost
        int_port = nPort
        Return eImapResponse
    End Function

    Protected Function CreateFolder() As Boolean
        Dim e As enumIMAPResponse
        e = IMAPInterface("CREATE " & strAppName, New ArrayList, True, True, True)
        If e = enumIMAPResponse.imapSuccess Then
            LogOutput(enumLogType.INFO, String.Format("Created AirDrive folder on {0}", str_Host))
            Return True
        End If
        LogOutput(enumLogType.INFO, String.Format("[CreateFolder]: Could not create folder on {0}. Aborting...", str_Host))
        Return False
    End Function

    Protected Function RestoreStream() As Boolean

        If str_Host.Length = 0 OrElse _
           str_UserID.Length = 0 OrElse _
           str_Password.Length = 0 Then

            LogOutput(enumLogType.ERROR, "Invalid parameter[s] passed to the [restore] subroutine. Halting...")
            Return False
        End If

        Try
            b_LoggedIn = False
            Login(str_Host, int_port, int_port, str_UserID, str_Password)
            If str_MailBoxName.Length > 0 Then
                If b_FolderSelected Then
                    b_FolderSelected = False
                    SelectMailBox()
                End If
            End If
        Catch e As Exception
            LogOutput(enumLogType.ERROR, String.Format("Invalid parameters passed to the [restore] subroutine. System message {0}", e.Message))
            Return False
        End Try

        Return True
    End Function

    Protected Function ValidateServerCert(ByVal sender As Object, ByVal certificate As X509Certificate, ByVal chain As X509Chain, ByVal sslPolicyErrors__1 As SslPolicyErrors) As Boolean
        If sslPolicyErrors__1 = SslPolicyErrors.None Then
            Return True
        Else
            Return False
        End If
    End Function

#End Region

End Class

