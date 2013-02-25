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
'    Filename: objIcon.vb

Imports System
Imports System.Drawing
Imports System.Runtime.InteropServices
Imports AirDrive.enums

Public Class objIcon
    <Flags()> Private Enum SHGFI
        SmallIcon = &H1
        LargeIcon = &H0
        Icon = &H100
        DisplayName = &H200
        Typename = &H400
        SysIconIndex = &H4000
        UseFileAttributes = &H10
    End Enum

    Public Enum IconSize
        SmallIcon = 1
        LargeIcon = 0
    End Enum

    <StructLayout(LayoutKind.Sequential)> _
    Private Structure SHFILEINFO
        Public hIcon As IntPtr
        Public iIcon As Integer
        Public dwAttributes As Integer
        <MarshalAs(UnmanagedType.LPStr, SizeConst:=260)> Public szDisplayName As String
        <MarshalAs(UnmanagedType.LPStr, SizeConst:=80)> Public szTypeName As String

        Public Sub New(ByVal B As Boolean)
            hIcon = IntPtr.Zero
            iIcon = 0
            dwAttributes = 0
            szDisplayName = vbNullString
            szTypeName = vbNullString
        End Sub
    End Structure

    Private Declare Auto Function SHGetFileInfo Lib "shell32" (ByVal pszPath As String, ByVal dwFileAttributes As Integer, ByRef psfi As SHFILEINFO, ByVal cbFileInfo As Integer, ByVal uFlagsn As SHGFI) As Integer

    Public Function GetDefaultIcon(ByVal Path As String, Optional ByVal IconSize As IconSize = IconSize.SmallIcon, Optional ByVal SaveIconPath As String = "") As Icon
        Dim info As New SHFILEINFO(True)
        Dim cbSizeInfo As Integer = Marshal.SizeOf(info)
        Dim flags As SHGFI = SHGFI.Icon Or SHGFI.UseFileAttributes
        flags = flags + IconSize
        SHGetFileInfo(Path, 256, info, cbSizeInfo, flags)
        GetDefaultIcon = Icon.FromHandle(info.hIcon)
        If SaveIconPath <> String.Empty Then
            Dim FileStream As New IO.FileStream(SaveIconPath, IO.FileMode.Create)
            GetDefaultIcon.Save(FileStream)
            FileStream.Close()
        End If
    End Function
    Public Function ImageToIcon(ByVal SourceImage As Image) As Icon
        Dim TempBitmap As New Bitmap(SourceImage)
        ImageToIcon = Icon.FromHandle(TempBitmap.GetHicon())
        TempBitmap.Dispose()
    End Function

End Class
