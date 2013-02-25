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
'    Filename: vars.vb


Imports AirDrive.enums
Imports AirDrive.objimap
Imports AirDrive.frmMain

Public Module vars

#Region "Boolean"
    'enable icon rendering... possible cause of windows 7 errors
    Public bEnabledIcons As Boolean = False

    'clean shutdown switch
    Public bCleanShutdown As Boolean = False

    'switch that is set when settings XML file is loaded
    Public bSettingsLoaded As Boolean = False

    'frmMain_FirstPaint switch
    Public bFirstPaint As Boolean = True

    'switch to turn on encryption of XML data storage. Set to false for debugging
    Public Const bEncryptStore As Boolean = True
    'Public Const bObscureStore As Boolean = True

    'This switch tells frmMain whether to run the bgwCloudData event
    Public bRefreshRemoteData As Boolean = False

    'Write output to console. -not functioning
    Public Const bIMAPConsole As Boolean = False

    'compress XML profile data
    Public Const bCompressXML As Boolean = True

    'Enable general logging to console
    Public b_EnableLoggingOutput As Boolean = True

    'displays all data sent/received over the IMAP protocol. Good for debugging, but not much else.
    Public b_EnableIMAPOutputing As Boolean = False

    'This switch tells queue handler when to process items in colPendingDelete
    Public b_PendingDelete As Boolean = False

    'setting to enable drag drop
    Public Const bEnableDragDrop As Boolean = True

    'setting to enable logging to file
    Public bLogToFile As Boolean = False

    'global shutdown flag
    'Public bCloseApp As Boolean = False

    'hive quota reached
    Public bHiveQuotaReached As Boolean = False

    Public bIsLogEncryted As Boolean = False
#End Region

#Region "Characters"
    Public Const chrSpace As Char = " "
    Public Const chrQuote As Char = Chr(34)
    Public Const chrPlus As Char = "+"
    Public Const chrBackslash As Char = "\"
    Public Const chrFrontslash As Char = "/"
    Public Const chrPeriod As Char = "."
#End Region

#Region "Numbers"
    'these inegers keep track of version data.
    Public nMajor As Integer = My.Application.Info.Version.Major
    Public nMinor As Integer = My.Application.Info.Version.Minor
    Public nBuild As Integer = My.Application.Info.Version.Build
    Public nRevis As Integer = My.Application.Info.Version.Revision

    'This constant is the maximum number of accounts that can be linked to a profile.
    Public Const nActLim As Integer = 100000
    Public Const nproflim As Integer = 10

    'Determines the size of each line when a file is uploaded.
    'Most email clients use '60'
    Public Const uChunk As Integer = 60

    'determines where to break files up when uploading them
    Public longMaxATT As Long = 1024 * 1000 * 20
    'Public longMaxATT As Long = 10

    'Value of frmMain's progres bar
    Public longProgBarValue As Long = 0
    Public longProgBarMax As Long = 0

    'Register for the speed monitor
    Public nByteRegister As Long = 0

#End Region

#Region "Strings"
    'For prefixing extries to the sourceforge media wiki
    Public strWikiRoot As String = "http://airdrive.sourceforge.net/wiki/index.php?title="

    Public strLocalDir As String = String.Empty

    'generate current version string
    Public cv As String = My.Application.Info.Version.Minor & chrPeriod & My.Application.Info.Version.Build & chrPeriod & My.Application.Info.Version.Revision

    'End of Line terminator for IMAP commands
    Public Const strEOL As String = vbCr & vbLf

    'Data about current profile
    Public strProfileName As String = String.Empty
    Public strProfilePassword As String = String.Empty

    'name of application
    Public Const strAppName As String = "AirDrive"

    'status message
    Public strStatusMessage As String = String.Empty

    'Console append line
    Public sAppendLine As String = String.Empty

    'location of .net framework download page
    Public Const sNetFrameWorkURL As String = "http://www.microsoft.com/downloads/details.aspx?FamilyId=333325FD-AE52-4E35-B531-508D977D32A6&displaylang=en"

    'location of output log file
    'only written if bLogToFile is TRUE
    Public sLogFileName As String = String.Empty

    'str last remote node
    Public strRemoteNodeSelected As String = String.Empty

    'encoded subject literal flag
    Public Const str_EncodedFlag As String = "<enc>true<;enc>"
#End Region

#Region "Other"

    'ENUMd responses to file overwrite prompts
    Public rLocalDupResponse As RemoteFileExistsResponse

    'Main/Global IMAP instance
    Public smIMAP As New objIMAP

    'Hashtable collections to store data about Remote accounts and file lists
    Public colRemoteFileData As New Collection
    Public colAccountList As New Collection

    'Create profile object. Stores data about the currently selected profile
    Public arrProfiles As ArrayList
    Public htProfileHeader As New Hashtable

    'main window state
    Public enumMainState As enums.frmMainStates
    Public enumMainConsole As enums.ConsoleViewSetting

    'collection of files pending delete
    Public colPendingDelete As New Collection

    'Global settings table
    Public oSettings As New objSettingsTable

    Public soRemoteList As SortOrder
    Public soLocalList As SortOrder

#End Region

#Region "Duplicate Form Feedback"

    Public bDupResAlways As Boolean = False
    Public bDupResQueue As Boolean = False
    Public bDupResOnlyDiff As Boolean = False
    Public ePreviousAction As enums.RemoteFileExistsResponse

#End Region

End Module
