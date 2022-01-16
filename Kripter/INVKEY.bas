Attribute VB_Name = "INVKEY"
Public G_strErr As String

Public Function reverseKey(strFileLog As String, bAdd64 As Boolean) As String
    Dim strK As String, dateX As Date, strCriptKey As String
    dateX = FileDateTime(strFileLog)
    strK = Format(dateX, "yyyymmdd") + Hex(Year(dateX)) + Format(dateX, "ddyyyymm") + Hex(Day(dateX) * Month(dateX))
    Open strFileLog For Input As #1
    Input #1, strCriptKey
    Close #1
    strK = InvKript(strCriptKey, strK, bAdd64)
    reverseKey = strK
End Function

Public Function InvKript_OLD(strS1 As String, strS2 As String) As String
    Dim Chiave(8) As Byte, strOut As String
    Dim Ch As String * 1
    Dim i As Long, V As Byte, intLngChiave As Byte
    Dim Chars As String, X As Integer
    Dim byt As Byte, strChiave As String
    strChiave = strS2: X = 0
    intLngChiave = Len(strChiave)
    Chars = "qwertyuiopasdfghjklzxcvbnmQWERTYUIOPASDFGHJKLZXCVBNM1234567890иа"
    For i = 1 To Len(strS1)
        Ch = Mid(strS1, i, 1)
        If InStr(1, Chars, Ch) > 0 Then
            byt = InStr(1, Chars, Ch) - 1
            V = Krpt(byt, Chiave, intLngChiave, i - X - 1, 63) + 1
            strOut = strOut & Mid(Chars, V, 1)
            '''Debug.Print Ch & ":" & Mid(Chars, V, 1)
        Else
            strOut = strOut & Ch
        End If
    Next i
End Function

Public Function InvKript(strS1 As String, strS2 As String, ByVal add64 As Boolean) As String
    Dim Chiave(100) As Byte, strOut As String
    Dim Ch As String * 1
    Dim i As Long, V, V2 As Byte, intLngChiave As Byte
    Dim Chars As String, X As Integer
    Dim byt As Byte, strChiave As String
    Chars = "qwertyuiopasdfghjklzxcvbnmQWERTYUIOPASDFGHJKLZXCVBNM1234567890иа"
    intLngChiave = Len(strS2)
    For i = 1 To Len(strS2)
        Ch = Mid(strS2, i, 1)
        byt = InStr(Chars, Ch) - 1
        If byt >= 1 Then
            Chiave(i - 1) = byt
        Else
            Chiave(i - 1) = Asc(Ch)
        End If
    Next
    For i = 1 To Len(strS1)
        Ch = Mid(strS1, i, 1)
        If InStr(1, Chars, Ch) > 0 Then
            byt = InStr(1, Chars, Ch) - 1
            V = Krpt(byt, Chiave, intLngChiave, i - X - 1, 63)
            If add64 Then V2 = V + 64 Else V2 = V
            If (Asc("a") <= V2 And V2 <= Asc("z")) Or (Asc("A") <= V2 And V2 <= Asc("Z")) Then
                strOut = strOut & Chr(V2)
            ElseIf (Asc("0") <= V2 And V2 <= Asc("9")) Then
                strOut = strOut & Chr(V)
            Else
                strOut = strOut & "[" + CStr(V2) + "]"
            End If
        Else
            strOut = strOut & Ch
        End If
    Next i
    InvKript = strOut
End Function

