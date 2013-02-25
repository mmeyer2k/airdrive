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
'    Filename: enums.vb


Public Class enums
    Public Enum CryptType
        Encrypt
        Decrypt
    End Enum

    Public Enum RemoteFileExistsResponse
        Archive
        Overwrite
        Rename
        Skip
    End Enum

    Public Enum frmMainStates
        Locked
        Unlocked
        RemoteFilesEmpty
    End Enum

    Public Enum enumIMAPResponse
        imapSuccess
        imapFailure
        IMAP_IGNORE_RESPONSE
        imapBeginAppend
    End Enum

    Public Enum enumLogType
        [INFO]
        [WARN]
        [ERROR]
        [IMAPCom]
        [IMAP]
    End Enum

    Public Enum ConsoleViewSetting
        None
        Normal
        Verbose
    End Enum

    Public Enum enumActType
        GMAIL
        Other
    End Enum

    Enum QueueActionType
        Upload
        Download
    End Enum
End Class
