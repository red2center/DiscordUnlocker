Set WshShell = CreateObject("WScript.Shell")

' Mimariyi belirleme (x86 veya x86_64)
Set objEnv = WshShell.Environment("PROCESS")
If InStr(objEnv("PROCESSOR_ARCHITECTURE"), "AMD64") > 0 Then
    arch = "x86_64"
Else
    arch = "x86"
End If
If objEnv("PROCESSOR_ARCHITEW6432") <> "" Then
    arch = "x86_64"
End If

' GoodbyeDPI dosya konumunu mimariye göre ayarlama
strExePath = WshShell.CurrentDirectory & "\" & arch & "\GoodbyeDPI.exe"
strParams = " -5 --dns-addr 77.88.8.8 --dns-port 1253 --dnsv6-addr 2a02:6b8::feed:0ff --dnsv6-port 1253"

' GoodbyeDPI'yi belirtilen parametrelerle arka planda çalıştırma
WshShell.Run Chr(34) & strExePath & Chr(34) & strParams, 0, False

Set WshShell = Nothing
