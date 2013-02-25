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
'    Filename: func_conncheck.vb

Imports AirDrive.func_calc

Public Module func_conncheck
    Public Function CheckConnection(ByRef inst_objIMAP As objIMAP, ByRef strEmail As String) As Boolean

        If inst_objIMAP.b_LoggedIn Then 'check if logged in at all
            If strEmail = inst_objIMAP.str_Email Then
                Return True
            End If
        End If

        Try
            'find and retrieve authentication data in hashtable format
            Dim objAuth As objAccount = ReturnAccountInfo(strEmail)
            If Not objAuth.Port = 0 Then
                Dim bSuccess As Boolean = inst_objIMAP.Login(objAuth.Hostname, objAuth.Port, objAuth.SSL, objAuth.Username, objAuth.Password)
                If bSuccess Then
                    objAuth.IsFunctioning = True
                    If OpenIMAPFolder(inst_objIMAP) Then
                        Return True
                    Else
                        objAuth.IsFunctioning = False
                        Return False
                    End If
                Else
                    objAuth.IsFunctioning = False
                    Return False
                End If
            Else
                Return False
            End If
        Catch ex As Exception
            Return False
        End Try
        Return False
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="nTries"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function IsWebConnectionAvailable(Optional ByVal nTries As Integer = 3) As Boolean
        Dim arrUrls As String() = {"google.com", _
                                   "microsoft.com", _
                                   "ebay.com", _
                                   "sourceforge.net" _
                                   }

        ShuffleArray(arrUrls)

        For Each t In arrUrls
            Dim objUrl As New System.Uri("http://www." & t)
            Dim objWebReq As System.Net.WebRequest
            objWebReq = System.Net.WebRequest.Create(objUrl)
            Dim objResp As System.Net.WebResponse
            objResp = Nothing
            Try
                ' Attempt to get response and return True
                objResp = objWebReq.GetResponse
                objResp.Close()
                objWebReq = Nothing
                Return True
            Catch ex As Exception
                objResp.Close()
                objWebReq = Nothing
            End Try
            nTries = nTries - 1
            If nTries = -1 Then Return False
        Next

    End Function

End Module
