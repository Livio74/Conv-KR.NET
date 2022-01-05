Attribute VB_Name = "XML"
Option Explicit

Public Function ConvToXML(strS As String, Optional intForce As Integer = 0) As String
    Dim strS2 As String, i As Long, strChr As String
    Dim bBlank As Boolean
    bBlank = True
    For i = 1 To Len(strS)
        strChr = Mid(strS, i, 1)
        Select Case Asc(strChr)
            Case Is <= 31:
                strS2 = strS2 + "&#" + CStr(Asc(strChr)) + ";"
                If strChr <> Chr(9) And strChr <> Chr(10) And strChr <> Chr(13) Then bBlank = False
            Case 32: strS2 = strS2 + "&#32;"
            Case 38: strS2 = strS2 + "&amp;": bBlank = False
            Case 39: strS2 = strS2 + "'": bBlank = False
            Case 60: strS2 = strS2 + "&lt;": bBlank = False
            Case Is >= 128:
                strS2 = strS2 + "&#" + CStr(Asc(strChr)) + ";": bBlank = False
            Case Else:
                strS2 = strS2 + strChr: bBlank = False
        End Select
    Next
    If bBlank And intForce = 0 Then strS2 = strS
    ConvToXML = strS2
End Function

