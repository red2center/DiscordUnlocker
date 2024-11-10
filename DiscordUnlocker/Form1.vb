Imports System.Threading
Imports Microsoft.Win32
Public Class Form1
    Private Sub kapat()
        Dim processes As Process() = Process.GetProcessesByName("GoodbyeDPI")
        For Each proc As Process In processes
            proc.Kill()
            proc.WaitForExit()
        Next
    End Sub
    Private Sub suekle()
        Dim vbsPath As String = System.IO.Path.Combine(Application.StartupPath, "du.vbs")
        Dim regKey As RegistryKey = Registry.CurrentUser.OpenSubKey("SOFTWARE\Microsoft\Windows\CurrentVersion\Run", True)

        If regKey.GetValue("duVBS") Is Nothing Then
            regKey.SetValue("duVBS", vbsPath)

        Else
            MessageBox.Show("zaten başlangıçta var.")
        End If

        regKey.Close()
    End Sub
    Private Sub sukaldir()
        Dim regKey As RegistryKey = Registry.CurrentUser.OpenSubKey("SOFTWARE\Microsoft\Windows\CurrentVersion\Run", True)

        If regKey.GetValue("duVBS") IsNot Nothing Then
            regKey.DeleteValue("duVBS")

        Else
            MessageBox.Show("başlangıçta bulunamadı.")
        End If

        regKey.Close()
    End Sub
    Private Sub sukontrol()
        Thread.Sleep(3000)
        Dim regKey As RegistryKey = Registry.CurrentUser.OpenSubKey("SOFTWARE\Microsoft\Windows\CurrentVersion\Run", True)
        If regKey.GetValue("duVBS") IsNot Nothing Then

            Label2.Text = "Başlangıçta Çalışıyor"
            Label2.ForeColor = Color.Green
            Button5.Enabled = False
            Button4.Enabled = True

        Else
            Label2.Text = "Başlangıçta Çalışmıyor"
            Label2.ForeColor = Color.Red
            Button4.Enabled = False
            Button5.Enabled = True
        End If
    End Sub

    Private Sub calistir()
        Dim vbsFilePath As String = "du.vbs"


        Dim startInfo As New ProcessStartInfo()
        startInfo.FileName = "wscript.exe"
        startInfo.Arguments = Chr(34) & vbsFilePath & Chr(34)
        startInfo.WindowStyle = ProcessWindowStyle.Hidden

        ' Process'i başlat
        Dim process As Process = Process.Start(startInfo)
    End Sub
    Private Sub kontrol()
        Thread.Sleep(3000)
        Dim processes As Process() = Process.GetProcessesByName("GoodbyeDPI")
        If processes.Length > 0 Then
            Label1.Text = "Durum : Çalışıyor"
            Label1.ForeColor = Color.Green
            Button2.Enabled = False
            Button3.Enabled = True
        Else
            Label1.Text = "Durum : Çalışmıyor"
            Label1.ForeColor = Color.Red
            Button3.Enabled = False
            Button2.Enabled = True
        End If
    End Sub
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        kontrol()
        sukontrol()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        calistir()
        kontrol()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        kontrol()
        sukontrol()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        kapat()
        kontrol()
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        suekle()
        sukontrol()

    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        sukaldir()
        sukontrol()
    End Sub
End Class
