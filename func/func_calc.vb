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
'    Filename: func_calc.vb



Imports System.Security.Cryptography
Imports System.Text

Public Module func_calc

    Public Function func_CalculateEncodedSize(ByVal fSize As Int64) As Int64
        Do Until fSize Mod 3 = 0
            fSize += 1
        Loop
        fSize = ((fSize * 4) / 3)

        Return fSize
    End Function

    Public Function func_ComputeHash(ByVal plainText As String, ByVal hashAlgorithm As String) As String

        Dim saltBytes() As Byte

        ' Define min and max salt sizes.
        Dim minSaltSize As Integer
        Dim maxSaltSize As Integer

        minSaltSize = 4
        maxSaltSize = 8

        ' Generate a random number for the size of the salt.
        Dim random As Random
        random = New Random()

        Dim saltSize As Integer
        saltSize = random.Next(minSaltSize, maxSaltSize)

        ' Allocate a byte array, which will hold the salt.
        saltBytes = New Byte(saltSize - 1) {}

        ' Initialize a random number generator.
        Dim rng As RNGCryptoServiceProvider
        rng = New RNGCryptoServiceProvider()

        ' Fill the salt with cryptographically strong byte values.
        rng.GetNonZeroBytes(saltBytes)

        ' Convert plain text into a byte array.
        Dim plainTextBytes As Byte()
        plainTextBytes = Encoding.UTF8.GetBytes(plainText)

        ' Allocate array, which will hold plain text and salt.
        Dim plainTextWithSaltBytes() As Byte = _
            New Byte(plainTextBytes.Length + saltBytes.Length - 1) {}

        ' Copy plain text bytes into resulting array.
        Dim I As Integer
        For I = 0 To plainTextBytes.Length - 1
            plainTextWithSaltBytes(I) = plainTextBytes(I)
        Next I

        ' Append salt bytes to the resulting array.
        For I = 0 To saltBytes.Length - 1
            plainTextWithSaltBytes(plainTextBytes.Length + I) = saltBytes(I)
        Next I

        ' Because we support multiple hashing algorithms, we must define
        ' hash object as a common (abstract) base class. We will specify the
        ' actual hashing algorithm class later during object creation.
        Dim hash As HashAlgorithm

        ' Make sure hashing algorithm name is specified.
        If (hashAlgorithm Is Nothing) Then
            hashAlgorithm = String.Empty
        End If

        ' Initialize appropriate hashing algorithm class.
        Select Case hashAlgorithm.ToUpper()

            Case "SHA1"
                hash = New SHA1Managed()

            Case "SHA256"
                hash = New SHA256Managed()

            Case "SHA384"
                hash = New SHA384Managed()

            Case "SHA512"
                hash = New SHA512Managed()

            Case Else
                hash = New MD5CryptoServiceProvider()

        End Select

        ' Compute hash value of our plain text with appended salt.
        Dim hashBytes As Byte()
        hashBytes = hash.ComputeHash(plainTextWithSaltBytes)

        ' Create array which will hold hash and original salt bytes.
        Dim hashWithSaltBytes() As Byte = _
                                   New Byte(hashBytes.Length + _
                                            saltBytes.Length - 1) {}

        ' Copy hash bytes into resulting array.
        For I = 0 To hashBytes.Length - 1
            hashWithSaltBytes(I) = hashBytes(I)
        Next I

        ' Append salt bytes to the result.
        For I = 0 To saltBytes.Length - 1
            hashWithSaltBytes(hashBytes.Length + I) = saltBytes(I)
        Next I

        ' Convert result into a base64-encoded string.
        Dim hashValue As String
        hashValue = Convert.ToBase64String(hashWithSaltBytes)

        ' Return the result.
        func_ComputeHash = hashValue
    End Function

    Public Function func_MD5HashFromFile(ByVal filepath As String) As String
        ' open file (as read-only)
        Using reader As New System.IO.FileStream(filepath, IO.FileMode.Open, IO.FileAccess.Read)
            Using md5 As New System.Security.Cryptography.MD5CryptoServiceProvider

                ' hash contents of this stream
                Dim hash() As Byte = md5.ComputeHash(reader)

                ' return formatted hash
                Return ByteArrayToString(hash)
            End Using
        End Using
    End Function

    Public Function func_MD5HashFromString(ByVal strToHash As String) As String
        Dim md5Obj As New Security.Cryptography.MD5CryptoServiceProvider
        Dim bytesToHash() As Byte = System.Text.Encoding.ASCII.GetBytes(strToHash)

        bytesToHash = md5Obj.ComputeHash(bytesToHash)

        Dim strResult As String = String.Empty

        For Each b As Byte In bytesToHash
            strResult += b.ToString("x2")
        Next

        Return strResult
    End Function

    Public Sub ShuffleArray(ByRef arrayToBeShuffled As Array, Optional ByVal numberOfTimesToShuffle As Integer = 10)
        Dim rndPosition As New Random(DateTime.Now.Millisecond)
        For i As Integer = 1 To numberOfTimesToShuffle
            For i2 As Integer = 1 To arrayToBeShuffled.Length
                swap(arrayToBeShuffled(rndPosition.Next(0, arrayToBeShuffled.Length)), arrayToBeShuffled(rndPosition.Next(0, arrayToBeShuffled.Length)))
            Next i2
        Next i
    End Sub

    Private Sub swap(ByRef arg1 As Object, ByRef arg2 As Object)
        Dim strTemp As String
        strTemp = arg1
        arg1 = arg2
        arg2 = strTemp
    End Sub
End Module
