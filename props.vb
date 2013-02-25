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
'    Filename: props.vb


Imports AirDrive.vars

Public Class props
    Public Shared Event ConsoleViewChanged(ByVal mvalue As enums.ConsoleViewSetting)
    Public Shared Property frmMainConsoleView() As enums.ConsoleViewSetting
        Get
            frmMainConsoleView = enumMainConsole
        End Get
        Set(ByVal value As enums.ConsoleViewSetting)
            enumMainConsole = value
            RaiseEvent ConsoleViewChanged(value)
        End Set
    End Property

    Public Shared Event AppendConsole(ByVal sLine As String)
    Public Shared Property AppendConsoleLine() As String
        Get
            AppendConsoleLine = sAppendLine
        End Get
        Set(ByVal value As String)
            sAppendLine = value
            RaiseEvent AppendConsole(value)
        End Set
    End Property

    Public Shared Event StatusMessageChanged(ByVal mvalue As String)
    Public Shared Property StatusMessage() As String
        Get
            StatusMessage = strStatusMessage
        End Get
        Set(ByVal value As String)
            strStatusMessage = value
            RaiseEvent StatusMessageChanged(value)
        End Set
    End Property

    Public Shared Event ProgressBarValueChanged(ByVal nVal As Long)
    Public Shared Property ProgressBarValue() As Long
        Get
            ProgressBarValue = longProgBarValue
        End Get
        Set(ByVal value As Long)
            longProgBarValue = value
            RaiseEvent ProgressBarValueChanged(value)
        End Set
    End Property

    Public Shared Event ProgressBarMaxChanged(ByVal nVal As Long)
    Public Shared Property ProgressBarMax() As Long
        Get
            ProgressBarMax = longProgBarMax
        End Get
        Set(ByVal value As Long)
            longProgBarMax = value
            RaiseEvent ProgressBarMaxChanged(value)
        End Set
    End Property

    Public Shared Event DisplayWindowEvent(ByRef w As Form)
    Public Shared Property DisplayNewWindow() As Form
        Get
            DisplayNewWindow = Nothing
        End Get
        Set(ByVal value As Form)
            RaiseEvent DisplayWindowEvent(value)
        End Set
    End Property

End Class
