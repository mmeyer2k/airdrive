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
'    Filename: objAccount.vb


Public Class objAccount

#Region "Private Variables"
    'strings
    Protected sUsername As String = String.Empty
    Protected sPassword As String = String.Empty
    Protected sHostname As String = String.Empty
    Protected sEmail As String = String.Empty

    'numbers
    Protected nPort As Int32 = 993
    Protected nBytesUsed As Long = 0
    Protected nBytesTotal As Long = 0
    Protected nMaxAttachmentSize As Long = 0
    Protected nQuota As Short = 70
    Protected nItems As Long = 0

    'boolean
    Protected bSSL As Boolean = True
    Protected bUnlimitedQuota As Boolean = False
    Protected bQuotaReached As Boolean = False
    Protected bIsGMail As Boolean = False
    Protected bIsFunctioning As Boolean = False
#End Region

#Region "Properties"

    Public Property IsFunctioning() As Boolean
        Get
            IsFunctioning = bIsFunctioning
        End Get
        Set(ByVal value As Boolean)
            bIsFunctioning = value
        End Set
    End Property

    Public Property Username() As String
        Get
            Username = sUsername
        End Get
        Set(ByVal value As String)
            sUsername = value
        End Set
    End Property

    Public Property Password() As String
        Get
            Password = sPassword
        End Get
        Set(ByVal value As String)
            sPassword = value
        End Set
    End Property

    Public Property Hostname() As String
        Get
            Hostname = sHostname
        End Get
        Set(ByVal value As String)
            sHostname = value
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

    Public Property Port() As Integer
        Get
            Port = nPort
        End Get
        Set(ByVal value As Integer)
            nPort = value
        End Set
    End Property

    Public Property BytesUsed() As Long
        Get
            BytesUsed = nBytesUsed
        End Get
        Set(ByVal value As Long)
            nBytesUsed = value
        End Set
    End Property

    Public Property BytesTotal() As Long
        Get
            BytesTotal = nBytesTotal
        End Get
        Set(ByVal value As Long)
            nBytesTotal = value
        End Set
    End Property

    Public Property MaxAttachmentSize() As Int64
        Get
            MaxAttachmentSize = nMaxAttachmentSize
        End Get
        Set(ByVal value As Int64)
            nMaxAttachmentSize = value
        End Set
    End Property

    Public Property Quota() As Short
        Get
            Quota = nQuota
        End Get
        Set(ByVal value As Short)
            nQuota = value
        End Set
    End Property

    Public ReadOnly Property PercentUsed() As Short
        Get
            PercentUsed = nBytesUsed / nBytesTotal * 100
        End Get
    End Property

    Public Property SSL() As Boolean
        Get
            SSL = bSSL
        End Get
        Set(ByVal value As Boolean)
            bSSL = value
        End Set
    End Property

    Public Property UnlimitedQuota() As Boolean
        Get
            UnlimitedQuota = bUnlimitedQuota
        End Get
        Set(ByVal value As Boolean)
            bUnlimitedQuota = value
        End Set
    End Property

    Public Property QuotaReached() As Boolean
        Get
            QuotaReached = bQuotaReached
        End Get
        Set(ByVal value As Boolean)
            bQuotaReached = value
        End Set
    End Property

    Public Property Items() As Long
        Get
            Items = nItems
        End Get
        Set(ByVal value As Long)
            nItems = value
        End Set
    End Property

    Public Property GMail() As Boolean
        Get
            GMail = bIsGMail
        End Get
        Set(ByVal value As Boolean)
            bIsGMail = value
        End Set
    End Property


#End Region

#Region "Public functions and methods"

    Public Sub New()

    End Sub

    Public Sub New(ByRef ht As Hashtable)
        For Each dv As DictionaryEntry In ht
            If dv.Key = "MaxAttachmentSize" Then
                nMaxAttachmentSize = Convert.ToInt64(dv.Value)

            ElseIf dv.Key = "BytesUsed" Then
                nBytesUsed = Convert.ToInt64(dv.Value)

            ElseIf dv.Key = "BytesTotal" Then
                nBytesTotal = Convert.ToInt64(dv.Value)

            ElseIf dv.Key = "SSL" Then
                bSSL = Convert.ToBoolean(dv.Value)

            ElseIf dv.Key = "Username" Then
                sUsername = dv.Value

            ElseIf dv.Key = "EmailAddress" Then
                sEmail = dv.Value

            ElseIf dv.Key = "Password" Then
                sPassword = dv.Value

            ElseIf dv.Key = "Items" Then
                nItems = dv.Value

            ElseIf dv.Key = "Hostname" Then
                sHostname = dv.Value

            ElseIf dv.Key = "Port" Then
                nPort = Convert.ToInt32(dv.Value)

            ElseIf dv.Key = "Quota" Then
                nQuota = dv.Value

            ElseIf dv.Key = "GMail" Then
                bIsGMail = dv.Value

            End If
        Next
    End Sub

    Public Function OutputToHashtable() As Hashtable
        Dim ht As New Hashtable
        ht.Add("username", sUsername)
        ht.Add("password", sPassword)
        ht.Add("hostname", sHostname)
        ht.Add("port", nPort)
        ht.Add("email", sEmail)
        ht.Add("bytes-used", nBytesUsed)
        ht.Add("bytes-total", nBytesTotal)
        ht.Add("quota", nQuota)
        ht.Add("maxattachment", nMaxAttachmentSize)
        ht.Add("ssl", Convert.ToString(bSSL))
        ht.Add("quota-reached", Convert.ToString(bQuotaReached))
        ht.Add("quota-unlimited", Convert.ToString(bUnlimitedQuota))
        ht.Add("gmail", Convert.ToString(bIsGMail))
        Return ht
    End Function

    Public Sub AppendUsedBytes(ByVal n As Int64)
        'add to total
        nBytesUsed += n
    End Sub

#End Region

End Class
