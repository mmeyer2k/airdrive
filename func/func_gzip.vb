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
'    Filename: func_gzip.vb

Imports System.IO
Imports System.IO.Compression
Imports System.Text

Public Class func_gzip
    Public Shared Function gzCompress(ByVal text As String) As String

        Dim b As Byte() = Encoding.UTF8.GetBytes(text)
        Dim ms = New MemoryStream()
        Using gzStrm = New GZipStream(ms, CompressionMode.Compress, True)
            gzStrm.Write(b, 0, b.Length)
        End Using

        ms.Position = 0

        Dim compressedData = New Byte(ms.Length - 1) {}
        ms.Read(compressedData, 0, compressedData.Length)

        Dim gzBuff = New Byte(compressedData.Length + 3) {}
        Buffer.BlockCopy(compressedData, 0, gzBuff, 4, compressedData.Length)
        Buffer.BlockCopy(BitConverter.GetBytes(b.Length), 0, gzBuff, 0, 4)
        Return Convert.ToBase64String(gzBuff)
    End Function


    Public Shared Function gzDecompress(ByVal compressedText As String, Optional ByRef sError As String = "") As String
        Try
            Dim gzBuff As Byte() = Convert.FromBase64String(compressedText)
            Using ms = New MemoryStream()
                Dim dataLength As Integer = BitConverter.ToInt32(gzBuff, 0)
                ms.Write(gzBuff, 4, gzBuff.Length - 4)

                Dim buffer = New Byte(dataLength - 1) {}

                ms.Position = 0
                Using gzStrm = New GZipStream(ms, CompressionMode.Decompress)
                    gzStrm.Read(buffer, 0, buffer.Length)
                End Using

                Return Encoding.UTF8.GetString(buffer)
            End Using
        Catch ex As Exception
            LogOutput(enums.enumLogType.ERROR, String.Format("[gzDecompress]:  {0}", ex.Message))
            sError = ex.Message
            Return String.Empty
        End Try
    End Function

End Class
