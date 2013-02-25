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
'    Filename: objQueueThreadStarter.vb


Imports AirDrive.enums

Public Class objQueueThreadStarter

    Public QueueTypeIdentifier As QueueActionType
    Public IsDir As Boolean = False
    Public Path As String = String.Empty
    Public LocalPath As String = String.Empty 'download only

    Public Sub StartQueueThread()

    End Sub

End Class
