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
'    Filename: func_rcrypt.vb

Imports System.Security.Cryptography
Imports System.Text

Public Class func_rcrypt

    Public Shared rSalt As String = String.Empty
    Public Const rIter As Integer = 2
    Public Const rSize As Integer = 256
    Public Shared nSaltValue As String = String.Empty
    Public Shared nInitVector As String = String.Empty


    Public Shared Function rEncrypt(ByVal plainText As String, ByVal passPhrase As String, Optional ByRef bytes As Byte() = Nothing) As String

        GenerateValues(passPhrase)

        Dim initVectorBytes As Byte()
        initVectorBytes = Encoding.ASCII.GetBytes(nInitVector)

        Dim SaltBytes As Byte()
        SaltBytes = Encoding.ASCII.GetBytes(nSaltValue)

        Dim plainbytes As Byte()
        plainbytes = Encoding.UTF8.GetBytes(plainText)

        Dim password As New Rfc2898DeriveBytes(passPhrase, SaltBytes, rIter)

        Dim keyBytes As Byte()
        keyBytes = password.GetBytes(rSize / 8)

        Dim symmetricKey As RijndaelManaged
        symmetricKey = New RijndaelManaged()

        symmetricKey.Mode = CipherMode.CBC

        Dim encryptor As ICryptoTransform
        encryptor = symmetricKey.CreateEncryptor(keyBytes, initVectorBytes)

        Dim memoryStream As IO.MemoryStream
        memoryStream = New IO.MemoryStream()

        Dim cryptStream As CryptoStream
        cryptStream = New CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write)

        cryptStream.Write(plainbytes, 0, plainbytes.Length)

        cryptStream.FlushFinalBlock()

        Dim cipherTextBytes As Byte()
        cipherTextBytes = memoryStream.ToArray()

        memoryStream.Close()
        cryptStream.Close()

        Dim cipherText As String
        cipherText = Convert.ToBase64String(cipherTextBytes)

        bytes = cipherTextBytes

        rEncrypt = cipherText
    End Function

    Public Shared Function rEncrypt(ByVal plainbytes As Byte(), ByVal passPhrase As String, Optional ByRef bytes As Byte() = Nothing) As String

        GenerateValues(passPhrase)

        Dim initVectorBytes As Byte()
        initVectorBytes = Encoding.ASCII.GetBytes(nInitVector)

        Dim saltbytes As Byte()
        saltbytes = Encoding.ASCII.GetBytes(nSaltValue)

        Dim password As New Rfc2898DeriveBytes(passPhrase, saltbytes, rIter)

        Dim keyBytes As Byte()
        keyBytes = password.GetBytes(rSize / 8)

        Dim symmetricKey As RijndaelManaged
        symmetricKey = New RijndaelManaged()

        symmetricKey.Mode = CipherMode.CBC

        Dim encryptor As ICryptoTransform
        encryptor = symmetricKey.CreateEncryptor(keyBytes, initVectorBytes)

        Dim memoryStream As IO.MemoryStream
        memoryStream = New IO.MemoryStream()

        Dim cryptoStream As CryptoStream
        cryptoStream = New CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write)

        cryptoStream.Write(plainbytes, 0, plainbytes.Length)

        cryptoStream.FlushFinalBlock()

        Dim cipherTextBytes As Byte()
        cipherTextBytes = memoryStream.ToArray()

        memoryStream.Close()
        cryptoStream.Close()

        Dim cipherText As String
        cipherText = Convert.ToBase64String(cipherTextBytes)

        bytes = cipherTextBytes

        rEncrypt = cipherText
    End Function

    Public Shared Function rDecrypt(ByVal cipherText As String, ByVal passPhrase As String, Optional ByRef bytes As Byte() = Nothing) As String

        GenerateValues(passPhrase)

        Dim initVectorBytes As Byte()
        initVectorBytes = Encoding.ASCII.GetBytes(nInitVector)

        Dim saltValueBytes As Byte()
        saltValueBytes = Encoding.ASCII.GetBytes(nSaltValue)

        Dim cipherTextBytes As Byte()
        cipherTextBytes = Convert.FromBase64String(cipherText)

        Dim password As New Rfc2898DeriveBytes(passPhrase, saltValueBytes, rIter)

        Dim keyBytes As Byte()
        keyBytes = password.GetBytes(rSize / 8)

        Dim symmetricKey As RijndaelManaged
        symmetricKey = New RijndaelManaged()

        symmetricKey.Mode = CipherMode.CBC

        Dim decryptor As ICryptoTransform
        decryptor = symmetricKey.CreateDecryptor(keyBytes, initVectorBytes)

        Dim memoryStream As IO.MemoryStream
        memoryStream = New IO.MemoryStream(cipherTextBytes)

        Dim cryptoStream As CryptoStream
        cryptoStream = New CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read)

        Dim plainTextBytes As Byte()
        ReDim plainTextBytes(cipherTextBytes.Length)

        Dim decryptedByteCount As Integer
        decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length)

        memoryStream.Close()
        cryptoStream.Close()

        Dim plainText As String
        plainText = Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount)

        bytes = plainTextBytes

        ReDim Preserve bytes(decryptedByteCount - 2)

        rDecrypt = plainText
    End Function

    Protected Shared Sub GenerateValues(ByVal pw As String)
        Try
            Dim out As String = String.Empty
            Dim z() As Byte = System.Text.Encoding.ASCII.GetBytes(pw)
            z.Reverse()
            nSaltValue = Base64Encode(Base64Encode(ecrypt(Base64Encode(func_MD5HashFromString(System.Text.ASCIIEncoding.ASCII.GetString(z)) & System.Text.ASCIIEncoding.ASCII.GetString(z)), pw, False)))
            nInitVector = func_MD5HashFromString(pw & nSaltValue).Substring(0, 16)
        Catch ex As Exception
            LogOutput(enums.enumLogType.ERROR, "Fatal error occured while generating cryptographic salt value. " & ex.Message)
        End Try

    End Sub
End Class
