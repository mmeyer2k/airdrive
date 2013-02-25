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
'    Filename: func_dllimport.vb


Imports System.Runtime.InteropServices

Public Class func_dllimport
#Region "DLLImport"
    <DllImport("shell32.dll", CallingConvention:=CallingConvention.Cdecl)> Public Shared Function ExtractIcon(ByVal hIcon As IntPtr, ByVal lpszExeFileName As String, ByVal nIconIndex As Integer) As IntPtr
    End Function
    <DllImport("user32", CallingConvention:=CallingConvention.Cdecl)> Public Shared Function DestroyIcon(ByVal hIcon As IntPtr) As Boolean
    End Function
#End Region
End Class
