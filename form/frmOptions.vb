Imports System.Windows.Forms
Imports System.IO
Imports AirDrive.vars
Imports AirDrive.func_rcrypt

Public Class frmOptions

    Public Shared Function LogCrypt(ByVal c As enums.CryptType) As ArrayList
        Dim arrLines As New ArrayList
        Dim srFileRdr As StreamReader

        Dim exDoesNotExist As New Exception

        Try
            srFileRdr = New StreamReader(sLogFileName)    'open handle on log file
        Catch ex As Exception
            Return New ArrayList
        End Try

        Try
            Do    'begin encrypting the line
                Dim sLine As String = String.Empty
                Dim eLine As String = String.Empty    'encrypted line

                sLine = srFileRdr.ReadLine

                If sLine = Nothing OrElse sLine.Length < 10 Then    'exit if end of file
                    Exit Do
                End If

                sLine = sLine.Trim

                sLine = sLine.Replace(vbNewLine, String.Empty)

                If c = enums.CryptType.Encrypt Then
                    eLine = rEncrypt(sLine, strProfilePassword)
                Else
                    eLine = rDecrypt(sLine, strProfilePassword)
                End If

                arrLines.Add(eLine)
            Loop
        Catch ex As Exception
            Return arrLines
        Finally
            srFileRdr.Close()
            srFileRdr.Dispose()
        End Try
        Return arrLines
    End Function

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click

        'determine if log file needs to be encrypted or decrypted
        If System.IO.File.Exists(sLogFileName) Then
            If checkEncryptLog.Checked AndAlso Not bIsLogEncryted Then
                Dim arr As ArrayList = LogCrypt(enums.CryptType.Encrypt)    'translate log and store to arraylist

                If arr.Count = 0 Then
                    MsgBox("Could not encrypt the log file. Please check the console.")
                Else
                    Try
                        System.IO.File.Delete(sLogFileName)
                        ArrayListToFile(arr, sLogFileName)
                        bIsLogEncryted = True
                    Catch ex As Exception
                        MsgBox("Could not encrypt the log file. Please check the console.")
                        checkEncryptLog.Checked = False
                    End Try
                End If

            ElseIf Not checkEncryptLog.Checked AndAlso bIsLogEncryted Then
                Dim arr As ArrayList = LogCrypt(enums.CryptType.Decrypt)    'translate log and store to arraylist

                If arr.Count = 0 Then
                    MsgBox("Could not decrypt the log file. Please check the console.")
                Else
                    Try
                        System.IO.File.Delete(sLogFileName)
                        ArrayListToFile(arr, sLogFileName)
                        bIsLogEncryted = False
                    Catch ex As Exception
                        MsgBox("Could not decrypt the log file. Please check the console.")
                        checkEncryptLog.Checked = False
                    End Try
                End If

            End If
        End If

        If chkAutoLogin.Checked Then
            My.Settings.AutoLoginString = ecrypt(strProfilePassword, My.Settings.HashKey)
            My.Settings.AutoLoginProfile = strProfileName
        Else
            My.Settings.AutoLoginString = String.Empty
            My.Settings.AutoLoginProfile = String.Empty
        End If


        'set global settings table to match settings in options window
        oSettings.LoggingEnabled = checkLogging.Checked
        oSettings.ShowConsole = checkDisplayConsole.Checked
        oSettings.CheckForUpdates = checkUpdates.Checked
        oSettings.LoadCache = checkLoadCache.Checked
        oSettings.EncryptLog = checkEncryptLog.Checked

        oSettings.Save()    'save settings table

        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()

    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub frmOptions_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If File.Exists(Application.StartupPath & chrBackslash & sLogFileName) Then
            Button1.Enabled = True
            Button2.Enabled = True
        Else
            Button1.Enabled = False
            Button2.Enabled = False
        End If

        'modify controls according to the settings in the settings table
        checkDisplayConsole.Checked = oSettings.ShowConsole
        checkLogging.Checked = oSettings.LoggingEnabled
        checkUpdates.Checked = oSettings.CheckForUpdates
        checkLoadCache.Checked = oSettings.LoadCache
        checkEncryptLog.Checked = oSettings.EncryptLog

        If My.Settings.AutoLoginProfile = strProfileName AndAlso Not My.Settings.AutoLoginString = String.Empty Then
            chkAutoLogin.Checked = True
        End If

        lblLogLocation.Text = SlashScrubber(chrBackslash, Application.StartupPath & chrBackslash & sLogFileName)
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Try
            File.Delete(SlashScrubber(chrBackslash, Application.StartupPath & chrBackslash & sLogFileName))
            MsgBox("Log file cleared")
        Catch ex As Exception
            LogOutput(enums.enumLogType.ERROR, "Could not clear log file: " & ex.Message)
            MsgBox("Could not clear log file: " & ex.Message)
        End Try
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If bIsLogEncryted Then
            Dim frm As New frmLogViewer(enums.CryptType.Decrypt)
            frm.Show()
        Else
            Dim frm As New frmLogViewer(enums.CryptType.Encrypt)
            frm.Show()
        End If

    End Sub

    Private Sub checkLogging_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles checkLogging.CheckedChanged
        If checkLogging.Checked Then
            checkEncryptLog.Enabled = True
        Else
            checkEncryptLog.Enabled = False
        End If
    End Sub

    Private Sub LinkLabel1_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        OpenWiki("Options")
    End Sub

    Private Sub LinkLabel2_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LinkLabel2.LinkClicked
        OpenWiki("AutoLogin")
    End Sub
End Class
