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
'    Filename: func_convert.vb

Imports System.Text
Imports System.IO

Public Module func_convert

    Public Function ArrayListToString(ByRef arrList As ArrayList, Optional ByVal delimiter As String = "") As String
        Dim strwork As String = String.Empty
        For Each line In arrList
            If delimiter = String.Empty Then
                strwork = strwork & line
            Else
                strwork = strwork & delimiter & line
            End If
        Next
        Return strwork
    End Function

    Public Function Base64Encode(ByVal ref As String) As String
        Dim bytesToEncode As Byte()
        bytesToEncode = Encoding.UTF8.GetBytes(ref)
        Dim encodedText As String
        encodedText = Convert.ToBase64String(bytesToEncode)
        Return encodedText
    End Function

    Public Function Base64Decode(ByVal ref As String) As String
        Dim decodedBytes As Byte()
        decodedBytes = Convert.FromBase64String(ref)
        Dim decodedText As String
        decodedText = Encoding.UTF8.GetString(decodedBytes)
        Return decodedText
    End Function

    Public Function ByteArrayToString(ByVal arrInput() As Byte) As String
        Dim sb As New System.Text.StringBuilder(arrInput.Length)
        For i As Integer = 0 To arrInput.Length - 1
            sb.Append(arrInput(i).ToString("X2"))
        Next
        Return sb.ToString().ToLower
    End Function

    Public Function ReadMemoryStream(ByVal memStream As MemoryStream) As String

        Dim pos As Long = memStream.Position ' Remember the position so we can restore it later.
        memStream.Position = 0 ' Reset the stream otherwise you will just get an empty string.

        Dim reader As New StreamReader(memStream)
        Dim str = reader.ReadToEnd()

        memStream.Position = pos ' Reset the position so that subsequent writes are correct.

        Return str

    End Function

    Public Function ReturnNiceSize(ByVal nDecimalPlaces As Short, Optional ByVal filename As String = "", Optional ByVal bytes As Long = 0) As String
        Dim intX As Double = 0

        If bytes <> 0 Then
            intX = bytes
        Else
            If filename = Nothing Then
                Return String.Empty
            End If

            intX = GetFileSize(filename)
        End If

        Dim sPattern As String = String.Empty
        If nDecimalPlaces = 0 Then
            sPattern = "#0"
        ElseIf nDecimalPlaces = 1 Then
            sPattern = "#0.0"
        ElseIf nDecimalPlaces = 2 Then
            sPattern = "#0.00"
        ElseIf nDecimalPlaces = 3 Then
            sPattern = "#0.000"
        Else
            sPattern = "#0"
        End If

        If intX >= 1073741824 Then
            Return Format(intX / 1024 / 1024 / 1024, sPattern) & " GB"
        ElseIf intX >= 1048576 Then
            Return Format(intX / 1024 / 1024, sPattern) & " MB"
        ElseIf intX >= 1024 Then
            Return Format(intX / 1024, sPattern) & " KB"
        ElseIf intX > 1 AndAlso intX < 1024 Then
            Return Fix(intX) & " Bytes"
        ElseIf intX = 1 Then
            Return "1 Byte"
        Else
            Return "0 Bytes"
        End If
    End Function

    Public Function SlashScrubber(ByVal sSlashType As Char, ByRef sInput As String) As String
        If sSlashType = "/" Then
            sInput = Replace(sInput, "\", "/")
        ElseIf sSlashType = "\" Then
            sInput = sInput.Replace("/", "\")
        Else
            'catch input errors
        End If

        Do Until sInput.IndexOf(sSlashType.ToString & sSlashType.ToString) = -1
            sInput = sInput.Replace(sSlashType.ToString & sSlashType.ToString, sSlashType)
        Loop

        Return sInput

    End Function

End Module
