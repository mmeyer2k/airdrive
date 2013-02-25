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
'    Filename: func_io_file.vb

Imports System.IO
Imports System.Text
Imports AirDrive.func_rcrypt

Public Module func_io_file

    Public Function ExportToFile(ByVal sFileName As String, ByVal myContent As String, ByRef WriteLog As Boolean) As Boolean
        Try
            'If oSettings.EncryptLog Then
            'Dim strCipherContent As String = String.Empty
            'strCipherContent = func_rEncrypt(myContent, strProfilePassword)
            'File.AppendAllText(sFileName, strCipherContent, System.Text.Encoding.ASCII)
            'Else
            File.AppendAllText(sFileName, myContent, System.Text.Encoding.ASCII)
            'End If
        Catch ex As Exception
            'If WriteLog Then LogOutput(enums.enumLogType.ERROR, String.Format("Could not open [{0}] for writing. Abort", sFileName))
            Return False
        End Try

        Return True
    End Function

    Public Function ExportToFile(ByVal sFileName As String, ByRef myContentBytes() As Byte) As Boolean
        Try
            Dim size As Long = Nothing
            If File.Exists(sFileName) Then
                size = GetFileSize(sFileName)
            Else
                size = 0
            End If

            Dim fsi As New FileStream(sFileName, FileMode.OpenOrCreate)
            Dim br As New BinaryReader(fsi)
            Dim buff() As Byte = br.ReadBytes(size)
            br.Close()
            fsi.Close()

            Dim fso As New FileStream(sFileName, FileMode.OpenOrCreate)
            Dim bw As New BinaryWriter(fso)
            ReDim Preserve buff(buff.Length + myContentBytes.Length - 1)
            myContentBytes.CopyTo(buff, size)

            bw.Write(buff, 0, buff.Length)
            bw.Close()
            fso.Close()
        Catch ex As Exception
            'LogOutput(enums.enumLogType.ERROR, String.Format("[ExportToFile-ByteArray]: Could not open [{0}] for writing. System error: {1}", sFileName, ex.Message))
            Return False
        End Try

        Return True
    End Function

    Public Function DirectoryWizard(ByVal sDir As String) As Boolean
        Try
            If Not System.IO.Directory.Exists(sDir) Then
                LogOutput(enums.enumLogType.INFO, String.Format("Creating directory: {0};", sDir))
                System.IO.Directory.CreateDirectory(sDir)
                If Not System.IO.Directory.Exists(sDir) Then
                    LogOutput(enums.enumLogType.WARN, String.Format("Could not create direcotry [{0}] using the .NET directory functions. Attempting creation with the 'mkdir' commandline program.", sDir))
                    Shell(String.Format("mkdir {0}", sDir), AppWinStyle.Hide)
                    System.Threading.Thread.Sleep(100)
                    If Not System.IO.Directory.Exists(sDir) Then
                        LogOutput(enums.enumLogType.ERROR, String.Format("Creation of the directory structure {0} could not be completed. Please check that this is a valid directory", sDir))
                        Return False
                    Else
                        Return True
                    End If
                Else
                    Return True
                End If
            Else
                Return True
            End If
        Catch ex As Exception
            LogOutput(enums.enumLogType.ERROR, ex.Message)
            Return False
        End Try
    End Function

    Public Function ExtractEncodedFile(ByVal fileBase As String, ByVal extractFileName As String, ByVal tsize As Long, ByVal bAESencrypted As Boolean) As Boolean
        'check if any of the input parameters are null and escape if so
        If extractFileName = String.Empty OrElse fileBase = String.Empty Then
            LogOutput(enums.enumLogType.ERROR, "Escaping [ExtractEncodedFile] method due to null parameter.")
            Return False
        End If

        Try
            'open the input file for reading.
            Dim fsInput As New FileStream(fileBase, FileMode.Open, FileAccess.Read)
            Dim srInput As New BinaryReader(fsInput)

            'open output file stream and binarywriter
            Dim fsOutput As New FileStream(extractFileName, FileMode.Append, FileAccess.Write)
            Dim bwOutput As New BinaryWriter(fsOutput, System.Text.Encoding.ASCII)


            If bAESencrypted Then
                ''============================
                ''AES256 files.

                Dim b(tsize) As Byte
                srInput.Read(b, 0, tsize)
                bwOutput.Write(b)

            Else
                '============================
                'BASE64 files.

                'define default block size to read from file and decode
                Dim nBlockSize As Integer = 1024 * 1000 * 4

                'if blocksize is greater than file size, adjust to actual file size
                Dim fileBaseSize As Long = GetFileSize(fileBase)
                If nBlockSize > fileBaseSize Then
                    nBlockSize = fileBaseSize
                End If

                'begin loop that will parse all file data
                Do
                    'define Char buffer
                    Dim buffer(nBlockSize) As Char

                    'create integer to store .read return in
                    Dim intPos As Long = 0

                    'read block of data from file
                    intPos = srInput.Read(buffer, 0, nBlockSize)

                    'escape if the intPos is 0. This indicates end of block
                    If intPos = 0 Then
                        Exit Do
                    End If

                    'create output buffer from Decode command
                    Dim oBuffer() As Byte = Convert.FromBase64CharArray(buffer, 0, intPos)

                    'write to output writer
                    bwOutput.Write(oBuffer, 0, oBuffer.Length)
                Loop
            End If

            'resize output stream to the specified size
            fsOutput.SetLength(tsize)

            'close stream and reader/writer handles
            bwOutput.Close()
            srInput.Close()
            fsInput.Close()

        Catch ex As Exception
            LogOutput(enums.enumLogType.ERROR, String.Format("[ExtractEncodedFile]: An error occurred while decoding and concatenating the file {0}; System message: {1}.", extractFileName, ex.Message))
            Return False
        End Try
        Return True
    End Function

    Public Function GetFileContents(ByVal FullPath As String) As String
        Dim strContents As String = String.Empty
        Dim objReader As StreamReader
        Try
            objReader = New StreamReader(FullPath)
            strContents = objReader.ReadToEnd()

            objReader.Close()
            Return strContents
        Catch Ex As Exception
            LogOutput(enums.enumLogType.ERROR, String.Format("[GetFileContents]: {0}; {1}", FullPath, Ex.Message))
            Return String.Empty
        End Try
        Return strContents
    End Function

    Public Function ArrayListToFile(ByVal arrInput As ArrayList, ByVal FullPath As String) As Boolean
        Try
            Dim sw As New StreamWriter(FullPath)
            For Each sLine As String In arrInput
                sw.WriteLine(sLine)
            Next
        Catch ex As Exception
            Throw ex
        End Try
        Return True
    End Function

    Public Function SaveTextToFile(ByVal strData As String, ByVal FullPath As String) As Boolean
        Dim bAns As Boolean = False
        Dim objReader As StreamWriter

        Try
            objReader = New StreamWriter(FullPath)
            objReader.Write(strData)
            objReader.Close()
            bAns = True
        Catch Ex As Exception
            bAns = False
        End Try

        Return bAns
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="strPath">String path to file</param>
    ''' <returns>Length in bytes</returns>
    ''' <remarks></remarks>
    Public Function GetFileSize(ByVal strPath As String) As Long
        Try
            Dim fi As New FileInfo(strPath)
            Return fi.Length
        Catch ex As Exception

        End Try

    End Function

End Module
