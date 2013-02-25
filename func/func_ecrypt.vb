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
'    Filename: func_ecrypt.vb


Public Module func_ecrypt

    Public Function ecrypt(ByVal cyphText As String, ByVal Key As String, Optional ByVal boolDecode As Boolean = False) As String
        Dim count As Integer = 1
        Dim WorkString As String = cyphText
        ecrypt = Nothing
        For Each z In Key
            If ecrypt IsNot Nothing Then
                WorkString = ecrypt
                ecrypt = Nothing
            End If
            For Each i In WorkString
                Dim wrkBuffer As Integer
                wrkBuffer = Asc(i)
                Dim InterCount As Integer = 1
                For Each j In Key
                    If boolDecode = True Then
                        wrkBuffer = wrkBuffer - Asc(j) * count * InterCount - System.Math.Floor(Key.Length * cyphText.Length / count * Asc(j))
                    Else
                        wrkBuffer = wrkBuffer + Asc(j) * count * InterCount + System.Math.Floor(Key.Length * cyphText.Length / count * Asc(j))
                    End If
                    InterCount = InterCount + 1
                    wrkBuffer = Normalize(wrkBuffer)
                Next
                ecrypt = ecrypt + Chr(Normalize(wrkBuffer))
                count = count + 1
            Next
        Next
    End Function

    Public Function Normalize(ByVal Xnumber As Integer) As Integer
        If Xnumber > 127 Then
            Do Until Xnumber < 128
                Xnumber = Xnumber - 127
            Loop
        End If
        If Xnumber < 0 Then
            Do Until Xnumber > 0
                Xnumber = Xnumber + 127
            Loop
        End If
        Normalize = Xnumber
    End Function

End Module
