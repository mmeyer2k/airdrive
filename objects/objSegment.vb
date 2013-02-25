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
'    Filename: objSegment.vb

Public Class objSegment

#Region "Private Variables"

    'strings
    Protected sFilePath As String = String.Empty
    Protected sMD5total As String = String.Empty
    Protected sMD5Seg As String = String.Empty
    Protected sEmail As String = String.Empty

    'numbers
    Protected lConCat As Long = 0
    Protected nSizeTotal As Long = 0
    Protected nSegment As Integer = 0
    Protected nTotalSegments As Integer = 0
    Protected nUID As Integer = 0
    Protected nArc As Integer = 0

    'bools
    Protected bEncrypted As Boolean = False

    'other
    Protected dDate As Date = Nothing

#End Region

#Region "Properties"

    Public Property FilePath() As String
        Get
            FilePath = sFilePath
        End Get
        Set(ByVal value As String)
            sFilePath = value
        End Set
    End Property

    Public ReadOnly Property FileName() As String
        Get
            FileName = sFilePath.Substring(sFilePath.LastIndexOf(chrFrontslash) + 1)
        End Get
    End Property

    Public Property MD5Total() As String
        Get
            MD5Total = sMD5total
        End Get
        Set(ByVal value As String)
            sMD5total = value
        End Set
    End Property

    Public Property MD5Seg() As String
        Get
            MD5Seg = sMD5Seg
        End Get
        Set(ByVal value As String)
            sMD5Seg = value
        End Set
    End Property

    Public Property SizeTotal() As Long
        Get
            SizeTotal = nSizeTotal
        End Get
        Set(ByVal value As Long)
            nSizeTotal = value
        End Set
    End Property

    Public ReadOnly Property SizeEncoded() As Long
        Get
            SizeEncoded = func_CalculateEncodedSize(nSizeTotal)
        End Get
    End Property

    Public Property UID() As Integer
        Get
            UID = nUID
        End Get
        Set(ByVal value As Integer)
            nUID = value
        End Set
    End Property

    Public Property ArchiveNumber() As Integer
        Get
            ArchiveNumber = nArc
        End Get
        Set(ByVal value As Integer)
            nArc = value
        End Set
    End Property

    Public Property TotalSegments() As Integer
        Get
            TotalSegments = nTotalSegments
        End Get
        Set(ByVal value As Integer)
            nTotalSegments = value
        End Set
    End Property

    Public Property SizeConCat() As Long
        Get
            SizeConCat = lConCat
        End Get
        Set(ByVal value As Long)
            lConCat = value
        End Set
    End Property

    Public Property Segment() As Integer
        Get
            Segment = nSegment
        End Get
        Set(ByVal value As Integer)
            nSegment = value
        End Set
    End Property

    Public Property DateValue() As Date
        Get
            DateValue = dDate
        End Get
        Set(ByVal value As Date)
            dDate = value
        End Set
    End Property

    Public Property EmailAddress() As String
        Get
            EmailAddress = sEmail
        End Get
        Set(ByVal value As String)
            sEmail = value
        End Set
    End Property

    Public Property IsEncrypted() As Boolean
        Get
            IsEncrypted = bEncrypted
        End Get
        Set(ByVal value As Boolean)
            bEncrypted = value
        End Set
    End Property

#End Region

#Region "Functions and methods"

    Public Function ReturnHashTable() As Hashtable
        Dim ht As New Hashtable

        Return ht
    End Function

    Public Sub New(ByRef h As Hashtable)
        For Each item As DictionaryEntry In h
            If item.Key = "Segment" Then
                nSegment = item.Value

            ElseIf item.Key = "DateValue" Then
                dDate = Convert.ToDateTime(item.Value)

            ElseIf item.Key = "MD5Seg" Then
                sMD5Seg = item.Value

            ElseIf item.Key = "MD5Total" Then
                sMD5total = item.Value

            ElseIf item.Key = "EmailAddress" Then
                sEmail = item.Value

            ElseIf item.Key = "TotalSegments" Then
                nTotalSegments = item.Value

            ElseIf item.Key = "UID" Then
                nUID = item.Value

            ElseIf item.Key = "ArchiveNumber" Then
                nArc = item.Value

            ElseIf item.Key = "FilePath" Then
                sFilePath = item.Value

            ElseIf item.Key = "SizeTotal" Then
                nSizeTotal = item.Value

            ElseIf item.Key = "IsEncrypted" Then
                bEncrypted = item.Value

            ElseIf item.Key = "SizeConCat" Then
                lConCat = item.Value

            End If
        Next
    End Sub

    Public Sub New()

    End Sub

#End Region

End Class
