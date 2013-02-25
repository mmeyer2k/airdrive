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
'    Filename: methods.vb


Imports System.Xml
Imports AirDrive.vars
Imports System.IO
Imports System.Text
Imports System.Security.Cryptography
Imports AirDrive.func_convert
Imports AirDrive.func_rcrypt
Imports AirDrive.func_gzip
Imports AirDrive.functions
Imports AirDrive.enums
Imports System.Reflection

Public Module methods

    Public Sub LogOutput(ByVal type As enumLogType, ByVal log As String)
        Try
            If b_EnableLoggingOutput Then
                Select Case type
                    Case enumLogType.INFO
                        Dim sLog As String = DateAndTime.Now & "- Info: " & log
                        Console.WriteLine(sLog)
                        props.AppendConsoleLine = sLog
                        If bLogToFile Then ExportToFile(sLogFileName, sLog & vbNewLine, False)
                        Exit Select
                    Case enumLogType.WARN
                        Dim sLog As String = DateAndTime.Now & "- Warn: " & log
                        Console.WriteLine(sLog)
                        props.AppendConsoleLine = sLog
                        If bLogToFile Then ExportToFile(sLogFileName, sLog & vbNewLine, False)
                        Exit Select
                    Case enumLogType.[ERROR]
                        Dim sLog As String = DateAndTime.Now & "- Error: " & log
                        Console.WriteLine(sLog)
                        props.AppendConsoleLine = sLog
                        If bLogToFile Then ExportToFile(sLogFileName, sLog & vbNewLine, False)
                        Exit Select
                    Case enumLogType.IMAPCom
                        If b_EnableIMAPOutputing = True Then
                            Dim sLog As String = DateAndTime.Now & "- IMAP-CMD>: " & log
                            Console.WriteLine(sLog)
                            props.AppendConsoleLine = sLog
                            If bLogToFile Then ExportToFile(sLogFileName, sLog & vbNewLine, False)
                        End If
                        Exit Select
                    Case enumLogType.IMAP
                        If b_EnableIMAPOutputing = True Then
                            props.AppendConsoleLine = log
                            Console.WriteLine(log)
                            If bLogToFile Then ExportToFile(sLogFileName, log & vbNewLine, False)
                        End If
                        Exit Select
                End Select
            End If
        Catch ex As Exception
            ' do nothing
        End Try
        
    End Sub

    Public Sub UpdateHash(ByRef hash As Hashtable, ByVal key As String, ByVal Value As String)
        Try
            If hash(key) = String.Empty Then
                'if hash key does not exist then create and add value
                hash.Add(key, Value)
            Else
                hash(key) = Value
            End If
        Catch ex As Exception

        End Try
    End Sub

    Public Sub HTColToXML(ByRef col As Collection, ByVal OutputPath As String)
        Dim mStream As New MemoryStream
        Dim dxml As New XmlTextWriter(mStream, System.Text.Encoding.UTF8)

        dxml.Formatting = Formatting.Indented
        dxml.Indentation = 4


        dxml.WriteStartDocument(True)

        dxml.WriteStartElement("Nodes")

        If col.Count <> 0 Then
            Dim ie As IEnumerator = col.GetEnumerator
            Do While ie.MoveNext
                dxml.WriteStartElement("Node")
                Dim type As Type = ie.Current.GetType
                Dim properties() As PropertyInfo = type.GetProperties()
                For Each p As PropertyInfo In properties
                    If p.Name = "FileName" OrElse p.Name = "SizeEncoded" Then
                        Continue For
                    End If
                    dxml.WriteStartElement(p.Name)
                    dxml.WriteString(p.GetValue(ie.Current, Nothing))
                    dxml.WriteEndElement()
                Next
                dxml.WriteEndElement()
            Loop

        End If

        dxml.WriteEndDocument()
        dxml.Flush()

        'write XML stream to string
        Dim strStreamOutput As String = ReadMemoryStream(mStream)
        Dim nUncompressedSize As Long = strStreamOutput.Length

        'compress XML
        If bCompressXML Then
            strStreamOutput = gzCompress(strStreamOutput)
        End If


        Dim CypherText As String = rEncrypt(strStreamOutput, strProfilePassword)
        LogOutput(enumLogType.INFO, String.Format("XML file [{0}] was encrypted and compressed. Uncompressed size: {1}; Compressed size: {2}", OutputPath, ReturnNiceSize(2, , nUncompressedSize), ReturnNiceSize(2, , CypherText.Length)))

        SaveTextToFile(CypherText, OutputPath)

        mStream.Close()
        dxml.Close()

    End Sub

    Public Sub StoreFileDataToXML()
        Dim fName As String = strProfileName & ".fxml"

        fName = Application.StartupPath & "\" & fName

        fName = SlashScrubber("\", fName)

        If System.IO.File.Exists(fName) Then
            System.IO.File.Delete(fName)
        End If

        HTColToXML(colRemoteFileData, fName)
    End Sub

    Public Sub StoreAcntDataToXML()
        Dim fName As String = strProfileName & ".axml"

        fName = Application.StartupPath & chrBackslash & fName

        fName = SlashScrubber(chrBackslash, fName)

        If System.IO.File.Exists(fName) Then
            System.IO.File.Delete(fName)
        End If

        HTColToXML(colAccountList, fName)
    End Sub

    Public Sub sub_ScanForOrphans(ByRef cCollection As Collection)
        'scans collections for orphan files and unneeded junk segments
    End Sub

    Public Sub sub_DeleteProfile(ByVal fname As String)
        Dim arrExt As String() = {"axml", "fxml", "sxml"}
        For Each t As String In arrExt
            Dim sPath As String = SlashScrubber(chrBackslash, Application.StartupPath & chrBackslash & chrPeriod & t)
            If File.Exists(sPath) Then
                Try
                    File.Delete(sPath)
                Catch ex As Exception
                    LogOutput(enumLogType.ERROR, "Could not delete file: " & sPath & ". " & ex.Message)
                End Try
            End If
        Next
    End Sub

    Public Sub OpenWiki(Optional ByVal w As String = "Main_Page")
        System.Diagnostics.Process.Start(strWikiRoot & w)
    End Sub

    Public Function ReturnCollectionKey(ByRef obj As objSegment) As String
        Return obj.EmailAddress & ":" & obj.UID & ":" & obj.FileName & ":" & obj.SizeTotal
    End Function

    Public Sub AddToCollection(ByRef col As Collection, ByRef obj As objSegment)
        Dim genKey As String = ReturnCollectionKey(obj)
        If Not col.Contains(genKey) Then
            col.Add(obj, genKey)
        Else
            'handle duplicate keys
            'as of 0.1.28 this should never happen
            LogOutput(enumLogType.ERROR, "Error adding key to colection. Duplicate key generated. Object with item uid: " & obj.UID & ". Account: " & obj.EmailAddress)
        End If
    End Sub
End Module
