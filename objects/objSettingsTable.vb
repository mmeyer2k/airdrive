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
'    Filename: objSettingsTable.vb


Public Class objSettingsTable
    Protected b_LoggingEnabled As Boolean = False
    Protected strDontShow As String = Nothing
    Protected bShowConsole As Boolean = False
    Protected bDisplayFileIcons As Boolean = True
    Protected bLoadCache As Boolean = False
    Protected bCheckForUpdates As Boolean = True
    Protected bEncryptLog As Boolean = False


#Region "Properties (saved)"

    Public Property LoggingEnabled() As Boolean
        Get
            LoggingEnabled = b_LoggingEnabled
        End Get
        Set(ByVal value As Boolean)
            b_LoggingEnabled = value
            bLogToFile = value
            frmOptions.checkLogging.Checked = value
        End Set
    End Property

    Public Property HideDiag() As String
        Get
            HideDiag = strDontShow
        End Get
        Set(ByVal value As String)
            strDontShow += value & ";"
        End Set
    End Property

    Public Property ShowConsole() As Boolean
        Get
            ShowConsole = bShowConsole
        End Get
        Set(ByVal value As Boolean)
            bShowConsole = value
        End Set
    End Property

    Public Property CheckForUpdates() As Boolean
        Get
            CheckForUpdates = bCheckForUpdates
        End Get
        Set(ByVal value As Boolean)
            bCheckForUpdates = value
        End Set
    End Property

    Public Property LoadCache() As Boolean
        Get
            LoadCache = bLoadCache
        End Get
        Set(ByVal value As Boolean)
            bLoadCache = value
        End Set
    End Property

    Public Property EncryptLog() As Boolean
        Get
            EncryptLog = bEncryptLog
        End Get
        Set(ByVal value As Boolean)
            bEncryptLog = value
            bIsLogEncryted = value
            If value Then
                LogOutput(enums.enumLogType.INFO, "Changing to encrypted logging mode.")
            Else
                LogOutput(enums.enumLogType.INFO, "Changing to normal logging mode.")
            End If
        End Set
    End Property

#End Region


#Region "Functions"

    Public Function CheckIfHide(ByVal name As String) As Boolean
        If strDontShow.Contains(name & ";") Then Return True
        Return False
    End Function

    Public Sub Save()
        Dim n As New Collection
        Dim f As String = String.Empty

        f = Application.StartupPath & "\" & strProfileName & ".sxml"

        f = SlashScrubber("\", f)

        n.Add(Me)
        HTColToXML(n, f)
    End Sub

    Public Sub New(ByRef ht As Hashtable)
        For Each item As DictionaryEntry In ht
            If item.Key = "LoggingEnabled" Then
                LoggingEnabled = item.Value
            End If

            If item.Key = "HideDiag" Then
                HideDiag = item.Value
            End If

            If item.Key = "ShowConsole" Then
                bShowConsole = item.Value
            End If

            If item.Key = "CheckForUpdates" Then
                bCheckForUpdates = item.Value
            End If

            If item.Key = "LoadCache" Then
                bLoadCache = item.Value
            End If

            If item.Key = "EncryptLog" Then
                EncryptLog = item.Value
            End If
        Next
    End Sub

    Public Sub New()

    End Sub

#End Region



End Class
