Attribute VB_Name = "MAIN_MOD"
Public G_strErr As String
Option Explicit

Public G_bolEsisteLog As Boolean
Public G_bolErrLog As Boolean
Public G_lng_NumFiles As Long
Public G_strFileLog As String
Public G_strDirRoot As String
Public G_strFileList As String
Public G_strChiave As String


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
    G_strChiave = GetParam(2)
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
    Dim params As String, param1 As String, param2 As String
    Dim i As Integer
    param1 = "": param2 = ""
    params = Interaction.Command$
    If Len(params) > 0 Then
        param1 = Replace(params, """", "")
        If Not ExistsDir(param1) Then
            i = InStrRev(params, " ")
            If i > 0 Then
                param1 = Left(params, i - 1)
                param2 = Mid(params, i + 1)
                If Mid(params, i - 1, 1) = """" Then
                    param1 = Replace(params, """", "")
                End If
                If Not ExistsDir(param1) Then
                    param1 = "": param2 = ""
                End If
            End If
        End If
    End If
    If ind = 1 Then
        GetParam = param1
    ElseIf ind = 2 Then
        GetParam = param2
    Else
        GetParam = ""
    End If
End Function


