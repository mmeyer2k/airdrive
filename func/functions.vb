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
'    Filename: functions.vb

Imports System.IO
Imports System.Text
Imports AirDrive.frmMain
Imports AirDrive.vars
Imports AirDrive.methods
Imports AirDrive.enums
Imports AirDrive.func_dllimport
Imports System.Text.RegularExpressions
Imports System.Security.Cryptography

Public Module functions

    Public Function func_HasNet35() As Boolean
        Try
            AppDomain.CurrentDomain.Load("System.Core, Version=3.5.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function func_LoadProfiles() As ArrayList
        Dim lDir As New System.IO.DirectoryInfo(Application.StartupPath)
        Dim jArr As New ArrayList

        For Each fItem As System.IO.FileInfo In lDir.GetFiles
            If fItem.Extension = ".axml" Then
                jArr.Add(fItem.Name)
            End If
        Next

        Return jArr
    End Function

    Public Function EmailAddressCheck(ByVal emailAddress As String) As Boolean
        Dim pattern As String = "^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$"
        Dim emailAddressMatch As Match = Regex.Match(emailAddress, pattern)
        If emailAddressMatch.Success Then
            EmailAddressCheck = True
        Else
            EmailAddressCheck = False
        End If
    End Function

    Public Function func_EnumerateDirectory(ByVal root As String)
        Dim z As String = String.Empty
        Try
            If Directory.GetFiles(root).Count = 0 Then 'check if there are any files in the directory

                Dim entry As String = z & root & chrBackslash & ";"    'add empty directory with ending backslash for easy detection
                z = SlashScrubber(chrBackslash, z)
            Else
                'find and add files in root
                For Each fileName As String In Directory.GetFiles(root)
                    z = z & fileName & ";"
                Next
            End If

            'recurse into directories
            For Each foundDir As String In Directory.GetDirectories(root)
                z = z & func_EnumerateDirectory(foundDir)
            Next
        Catch ex As Exception
            LogOutput(enumLogType.ERROR, "File enumeration error: " & ex.Message)
            z = String.Empty
        End Try

        Return z
    End Function

    Public Function TextBetween(ByRef Stream As String, ByRef item1 As String, ByRef item2 As String) As String
        Dim x, y As Integer
        TextBetween = String.Empty
        x = Stream.IndexOf(item1)
        y = Stream.IndexOf(item2, x + 1)

        x = x + item1.Length

        Try
            TextBetween = Trim(Stream.Substring(x, y - x))
        Catch ex As Exception
            ' do nothing
        End Try

        Return TextBetween
    End Function

    Public Function ConnectToAccount(ByRef smIMAP As objimap, ByVal email As String) As Boolean
        Dim xObj As objAccount = ReturnAccountInfo(email)

        Dim username, password, hostname As String

        username = xObj.Username
        password = xObj.Password
        hostname = xObj.Hostname

        Dim bssl As Boolean = xObj.SSL
        Dim port As Integer = xObj.Port

        Dim retries As Integer = 3

        Do Until retries = 0
            Dim bSuccess As Boolean = smIMAP.Login(hostname, port, bssl, username, password)
            If bSuccess = True Then
                xObj.IsFunctioning = True
                Return True
            Else
                retries -= 1
            End If
        Loop
        xObj.IsFunctioning = False
        Return False
    End Function

    Public Function SelectAccount(ByRef smIMAP As objIMAP) As Boolean
        If colAccountList.Count = 0 Then
            Return False
        End If

        'get the best accont to use and make sure it is logged it
        If smIMAP.b_LoggedIn Then
            Dim xObj As objAccount = ReturnAccountInfo(smIMAP.str_Email)
            If xObj.Quota > xObj.PercentUsed Then
                Return True
            End If
        End If

        bHiveQuotaReached = True

        For Each Item As objAccount In colAccountList
            If Item.PercentUsed < Item.Quota Then
                bHiveQuotaReached = False
                If ConnectToAccount(smIMAP, Item.EmailAddress) Then
                    Return True
                End If
            End If
        Next

        If bHiveQuotaReached = True Then
            Dim r As MsgBoxResult
            Dim msg As String = "You have reached the specified quota for your account hive. You can either add more accounts or remove unused items from the servers."
            r = MessageBox.Show(msg, "Hive full", MessageBoxButtons.OK)
            LogOutput(enumLogType.WARN, msg)
        Else
            LogOutput(enumLogType.ERROR, "[SelectAccount]: Could not log in to any accounts in hive.")
        End If

        Return False
    End Function

    Public Function ReturnAccountInfo(ByVal email As String) As objAccount
        For Each xObj As objAccount In colAccountList
            If xObj.EmailAddress = email Then Return xObj
        Next
        Return Nothing
    End Function

    Public Function RandomString(ByVal size As Integer, ByVal lowerCase As Boolean) As String
        Dim builder As New StringBuilder()
        Dim random As New Random()
        Dim ch As Char
        Dim i As Integer
        For i = 0 To size - 1
            ch = Convert.ToChar(Convert.ToInt32((26 * random.NextDouble() + 65)))
            builder.Append(ch)
        Next i
        If lowerCase Then
            Return builder.ToString().ToLower()
        End If
        Return builder.ToString()
    End Function

    Public Function StripCommandIdentifier(ByVal line As String) As String
        Try
            Return line.Substring(line.IndexOf(chrSpace)).Trim    'strip the command ID
        Catch ex As Exception
            Return line    'gather exception that could occur if the line is malformed
        End Try
    End Function

    Public Function ReturnExtention(ByVal fname As String) As String
        Dim ext As String = String.Empty
        If fname.Contains(chrPeriod) Then
            If fname.LastIndexOf(chrBackslash) > fname.LastIndexOf(chrPeriod) OrElse fname.EndsWith(chrPeriod) Then 'check that there is a period in the actual file name and that it is not the last character
                Return String.Empty
            End If
            Return fname.Substring(fname.LastIndexOf(chrPeriod) + 1).ToLower
        Else
            Return String.Empty
        End If
    End Function

End Module
