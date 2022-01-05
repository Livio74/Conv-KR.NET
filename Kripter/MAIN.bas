Attribute VB_Name = "MAIN_MOD"
Public G_strErr As String
Option Explicit

Public G_bolEsisteLog As Boolean
Public G_bolErrLog As Boolean
Public G_lng_NumFiles As Long
Public G_strFileLog As String
Public G_strDirRoot As String
Public G_strFileList As String


Public Function getErrorMsg(ByVal strErrCode As String) As String
    Dim strErr As String
    strErr = ""
    Select Case UCase(strErrCode)
        Case "OVERFLOW"
            strErr = "Ci sono troppi file nella directory specificata"
        Case Else
            strErr = "ERROR CODE : " & strErrCode
    End Select
    getErrorMsg = strErr
End Function

Public Sub Main()
    G_strDirRoot = GetParam(1)
    If G_strDirRoot = "" Then
        G_strFileLog = "klog.txt"
        Load Kr
        Kr.Show
    Else
        G_strFileLog = G_strDirRoot & "\klog.txt"
        Load minKR
        minKR.Show
    End If
End Sub

Private Function GetParam(ind As Integer) As String
    GetParam = Replace(Interaction.Command$, """", "")
End Function


