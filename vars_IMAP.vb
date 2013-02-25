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
'    Filename: vars_imap.vb

Module vars_IMAP

#Region "Generic Expressions"
    Public const_str_copyuid As String = "COPYUID"
#End Region

#Region "Command Constants"
    Public Const imap_cmd_GetQuota As String = "GETQUOTAROOT"
    Public Const imap_cmd_LogOut As String = "LOGOUT"
    Public Const imap_cmd_Select As String = "SELECT"
    Public Const imap_cmd_LogIn As String = "LOGIN"
    Public Const imap_cmd_Connect As String = "CONNECT"
    Public Const imap_cmd_Capability As String = "CAPABILITY"
#End Region

#Region "Server Response Identifiers"
    Public Const imap_res_Untagged As String = "*"
    Public Const imap_res_Bad As String = "BAD"
    Public Const imap_res_serv_OK As String = "* OK"
    Public Const imap_res_OK As String = "OK"
    Public Const imap_res_Quota As String = "QUOTA"
    Public Const imap_res_No As String = "NO"
#End Region

#Region "Numeric Constants"
    Public Const imap_num_DefaultPort As UShort = 143
#End Region

End Module
