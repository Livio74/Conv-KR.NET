Attribute VB_Name = "PRG_UTILS"
Option Explicit

Public Function KritpStr(strNomeFile As String, strChiave As String) As String
    Dim Chiave(8) As Byte, strOut As String
    Dim Ch As String * 1, X As Integer
    Dim i As Long, V As Byte, intLngChiave As Byte
    Dim Chars As String
    Dim byt As Byte
    intLngChiave = 1
    Chars = "qwertyuiopasdfghjklzxcvbnmQWERTYUIOPASDFGHJKLZXCVBNM1234567890??"
    For i = 1 To Len(strChiave)
        Ch = Mid(strChiave, i, 1)
        Chiave(intLngChiave) = Asc(Ch)
        intLngChiave = intLngChiave + 1
    Next
    strOut = ""
    For i = 1 To Len(strNomeFile)
        Ch = Mid(strNomeFile, i, 1)
        If InStr(1, Chars, Ch) > 0 Then
            byt = InStr(1, Chars, Ch) - 1
            V = Krpt(byt, Chiave, intLngChiave, i - X - 1, 63) + 1
            strOut = strOut & Mid(Chars, V, 1)
            '''Debug.Print Ch & ":" & Mid(Chars, V, 1)
        Else
            strOut = strOut & Ch
        End If
    Next i
    KritpStr = strOut
End Function

Public Function KritpStr2(strNomeFile As String, strChiave As String) As String
    Dim Chiave(8) As Byte, strOut As String
    Dim Ch As String * 1
    Dim i As Long, V As Byte, intLngChiave As Byte
    Dim Chars As String
    Dim byt As Byte
    intLngChiave = 0
    Chars = "qwertyuiopasdfghjklzxcvbnmQWERTYUIOPASDFGHJKLZXCVBNM1234567890??"
    For i = 1 To Len(strChiave)
        Ch = Mid(strChiave, i, 1)
        Chiave(intLngChiave) = Asc(Ch)
        intLngChiave = intLngChiave + 1
    Next
    strOut = ""
    For i = 1 To Len(strNomeFile)
        Ch = Mid(strNomeFile, i, 1)
        If InStr(1, Chars, Ch) > 0 Then
            byt = InStr(1, Chars, Ch) - 1
            V = Krpt(byt, Chiave, intLngChiave, i - 1, 63) + 1
            strOut = strOut & Mid(Chars, V, 1)
            '''Debug.Print Ch & ":" & Mid(Chars, V, 1)
        Else
            strOut = strOut & Ch
        End If
    Next i
    KritpStr2 = strOut
End Function

Public Function Krpt(bytV As Byte, Chiave() As Byte, bytLngChiave As Byte, lngPos As Long, Optional just As Byte = 255) As Byte
    Dim bytVOut As Byte
    bytVOut = bytV Xor (Chiave(lngPos Mod bytLngChiave) And just)
    Krpt = bytVOut
End Function


Public Function getKey(strFileLog As String, strChiave As String) As String
    Dim strK As String, dateX As Date
    dateX = FileDateTime(strFileLog)
    strK = Format(dateX, "yyyymmdd") + Hex(Year(dateX)) + Format(dateX, "ddyyyymm") + Hex(Day(dateX) * Month(dateX))
    strK = KritpStr(strK, strChiave)
    getKey = strK
End Function

Public Function setKey(ByRef strK As String, strChiave As String)
    strK = Format(Now, "yyyymmdd") + Hex(Year(Now)) + Format(Now, "ddyyyymm") + Hex(Day(Now) * Month(Now))
    strK = KritpStr(strK, strChiave)
End Function

Public Sub Kritp(ByVal strNomeFile As String, ByVal strChiave As String, Optional strOut As String = "")
    Dim Chiave(8) As Byte
    Dim strNomeFileK As String
    Dim intLngChiave As Byte
    Dim Ch As String * 1
    Dim i As Long, V As Byte, lngLngFile As Long
    Dim GA_Buffer(1 To 3000) As Byte
    Dim Chars As String, X As Integer
    Dim byt As Byte, intPos As Integer, dtDataMod As Date
    On Error GoTo ErrCanc
    Chars = "qwertyuiopasdfghjklzxcvbnmQWERTYUIOPASDFGHJKLZXCVBNM1234567890??"
    strNomeFileK = "": intLngChiave = 0: intPos = 0
    X = InStrRev(strNomeFile, "\")
    If (X < 0) Then X = 0
    ' 1. Conversione TXT->BIN e calcolo della lunghezza
    For i = 1 To Len(strChiave)
        Ch = Mid(strChiave, i, 1)
        Chiave(intLngChiave) = Asc(Ch)
        intLngChiave = intLngChiave + 1
    Next
    ' 2. Kritp nome file
    strNomeFileK = Left(strNomeFile, X)
    For i = X + 1 To Len(strNomeFile)
        Ch = Mid(strNomeFile, i, 1)
        If InStr(1, Chars, Ch) > 0 Then
            byt = InStr(1, Chars, Ch) - 1
            V = Krpt(byt, Chiave, intLngChiave, i - X - 1, 63) + 1
            strNomeFileK = strNomeFileK & Mid(Chars, V, 1)
            '''Debug.Print Ch & ":" & Mid(Chars, V, 1)
        Else
            strNomeFileK = strNomeFileK & Ch
        End If
    Next i
    '''    If InStr(strNomeFile, "GestioneFam.mdb") > 0 Then
    '''        byt = byt
    '''    End If
    intPos = 1
    dtDataMod = FileDateTime(strNomeFile)
    Open strNomeFile For Binary Access Read As #1
    intPos = 2
    If Len(strOut) = 0 Then
        Open strNomeFile For Binary Access Write As #2
    Else
        Open strOut For Binary Access Write As #2
    End If
    intPos = 0
    lngLngFile = LOF(1)
    While lngLngFile > 0
        Get #1, , GA_Buffer
        For i = 1 To 3000
            GA_Buffer(i) = Krpt(GA_Buffer(i), Chiave, intLngChiave, i)
        Next i
        If lngLngFile > 3000 Then
            Put #2, , GA_Buffer
            lngLngFile = lngLngFile - 3000
        Else
            For i = 1 To lngLngFile
                V = GA_Buffer(i)
                Put #2, , V
            Next
            lngLngFile = 0
        End If
    Wend
    Close #2
    Close #1
    If Len(strOut) = 0 Then
        Name strNomeFile As strNomeFileK
        'Devo fare questa parte
        SetFileDateTime strNomeFileK, CStr(dtDataMod)
        
    End If
    Exit Sub
ErrCanc:
    '''MsgBox "Non ? possibile cancellare il file: " & strNomeFile
    G_strErr = G_strErr + "<EXCEPTION ID = ""0"" IDREF=""" + CStr(Err.Number) + """ DESCRIPTION=""" + XML.ConvToXML(Err.Description) + """ SOURCE=""" + _
     Err.Source + """ DATETIME=""" + Format(Now, "dd/mm/yy hh:nn:ss") + """><DETAILS><FILE>" + XML.ConvToXML(strNomeFile, 1) & "</FILE></DETAILS></EXCEPTION>" + vbCrLf
    If intPos > 0 Then Close #1
    If intPos > 1 Then Close #2
End Sub



