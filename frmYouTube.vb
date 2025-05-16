Public Class frmYouTube

    Public sChaptersFolderPath As String = ""
    Private iSeconds As Integer = 0

    Private Sub frmYouTube_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        If IO.Directory.Exists(sChaptersFolderPath) Then
            UpdateText()
            txtTotalSeconds.Text = iSeconds
            UpdateHourMinSec()
        End If

    End Sub

    Sub UpdateText()

        TextBox1.Text = ""

        Dim iStart As Integer = 0
        Dim oFiles As String() = System.IO.Directory.GetFiles(sChaptersFolderPath)
        For Each sFilePath In oFiles
            Dim sFileName As String = IO.Path.GetFileNameWithoutExtension(sFilePath)
            Dim oMP3Info As New Monotic.Multimedia.MP3.MP3Info(sFilePath)
            Dim sStartSec As String = TimeSpan.FromSeconds(iStart).ToString()
            TextBox1.AppendText(sStartSec & vbTab & sFileName & vbCrLf)
            iStart += oMP3Info.Length
        Next

        iSeconds = iStart
    End Sub

    Sub UpdateHourMinSec()
        Dim oTotalSec As String() = TimeSpan.FromSeconds(iSeconds).ToString().Split(":")
        If oTotalSec.Length > 2 Then
            txtHours.Text = oTotalSec(0)
            txtMinutes.Text = oTotalSec(1)
            txtSeconds.Text = oTotalSec(2)
        Else
            txtHours.Text = 0
            txtMinutes.Text = 0
            txtSeconds.Text = 0
        End If
    End Sub

    Sub UpdateTotalSec()
        Dim iHours As Integer = 0
        Dim iMinutes As Integer = 0
        Dim iSeconds As Integer = 0

        If IsNumeric(txtHours.Text) Then
            iHours = txtHours.Text
        End If

        If IsNumeric(txtMinutes.Text) Then
            iMinutes = txtMinutes.Text
        End If

        If IsNumeric(txtSeconds.Text) Then
            iSeconds = txtSeconds.Text
        End If

        iSeconds = (iHours * 60 * 60) + (iMinutes * 60) + iSeconds
        txtTotalSeconds.Text = iSeconds
    End Sub

    Private Sub txtTotalSeconds_LostFocus(sender As Object, e As EventArgs) Handles txtTotalSeconds.LostFocus
        If IsNumeric(txtTotalSeconds.Text) Then
            iSeconds = txtTotalSeconds.Text
        End If

        UpdateHourMinSec()
    End Sub

    Private Sub txtHours_TextChanged(sender As Object, e As EventArgs) Handles txtHours.TextChanged
        UpdateTotalSec()
    End Sub

    Private Sub txtMinutes_TextChanged(sender As Object, e As EventArgs) Handles txtMinutes.TextChanged
        UpdateTotalSec()
    End Sub

    Private Sub txtSeconds_TextChanged(sender As Object, e As EventArgs) Handles txtSeconds.TextChanged
        UpdateTotalSec()
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

End Class