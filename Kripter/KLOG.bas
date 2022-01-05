Attribute VB_Name = "KLOG"
' OGGETTO LOG
Option Explicit

Private strListaDir(30000) As String
Private strListaDirOld(30000) As String
Private intNumDir As Integer
Private bolEsisteLog As Boolean
Private bolErrLog As Boolean

'' OGGETTO LOGFILE
Public Function CaricaLogFile(ByVal strKey As String, ByVal strFileLog As String) As String
    Dim i As Long, j As Long, i1 As Long, strGet As String
    Dim strD As String, strCriptKey As String
    intNumDir = 0
    On Error GoTo ErrEsistenza
    strGet = getKey(strFileLog, strKey)
    Open strFileLog For Input As #1
    Input #1, strCriptKey
    If Len(strKey) > 0 Then
        If strGet <> strCriptKey Then
            CaricaLogFile = "": Close 1: Exit Function
        End If
    End If
    While Not EOF(1)
        Input #1, strListaDir(intNumDir + 1)
        intNumDir = intNumDir + 1
    Wend
    Close 1
    bolEsisteLog = True
    For i = 1 To intNumDir
        i1 = InStr(strListaDir(i), ":")
        If i1 < 0 Then
            bolErrLog = True: Exit Function
        End If
        If Right(strListaDir(i), 1) = "F" Then
            strD = Left(strListaDir(i), i1 - 1)
            For j = i To intNumDir
                If Left(strListaDir(j), i1 - 1) = strD Then
                    Mid(strListaDir(j), Len(strListaDir(j)), 1) = "D"
                End If
            Next j
        End If
    Next
    CaricaLogFile = strKey
    Exit Function
ErrEsistenza:
    bolEsisteLog = False
    CaricaLogFile = ""
End Function

Public Sub SalvaLogFile(ByVal strChiave As String, ByVal strFileLog As String)
    Dim i As Long, strK As String
    Open strFileLog For Output As #3
    setKey strK, strChiave
    Write #3, strK
    For i = 1 To intNumDir
        Write #3, strListaDir(i)
    Next
    Close 3
End Sub

Public Function IsStato(ByVal strDir As String) As String
    Dim strTemp As String, i As Integer
    If bolEsisteLog Then
        If Mid(strDir, Len(strDir), 1) = "\" Then strDir = Left(strDir, Len(strDir) - 1)
        For i = 1 To intNumDir
            If UCase(Left(strListaDir(i), Len(strListaDir(i)) - 3)) = UCase(strDir) Then
                strTemp = Right(strListaDir(i), 1)
            End If
        Next i
    Else
        strTemp = "E"
    End If
    IsStato = strTemp
End Function

Public Sub SetStato(ByVal strDir As String)
    Dim i As Integer
    If bolEsisteLog Then
        If Mid(strDir, Len(strDir), 1) = "\" Then strDir = Left(strDir, Len(strDir) - 1)
        For i = 1 To intNumDir
            If Left(strListaDir(i), Len(strListaDir(i)) - 3) = strDir Then
                If Mid(strListaDir(i), Len(strListaDir(i)), 1) = "E" Then
                    If Mid(strListaDir(i), Len(strListaDir(i)) - 1, 1) <> "K" Then
                        Mid(strListaDir(i), Len(strListaDir(i)) - 1, 1) = "K"
                    Else
                        Mid(strListaDir(i), Len(strListaDir(i)) - 1, 1) = "_"
                    End If
                End If
            End If
        Next i
    Else
        strListaDir(intNumDir + 1) = strDir & ":KE"
        intNumDir = intNumDir + 1
    End If
End Sub

Public Sub RigeneraLog(ByVal strChiave As String, ByVal strDirRadice As String, ByVal strLogFile As String)
    Dim strListLV1(5000) As String, intNumLV1 As Integer ' Lista directory livello i-1
    Dim strListLV2(5000) As String, intNumLV2 As Integer ' Lista directory livello i
    Dim strListFD(5000) As String, intNums As Integer ' Lista dei files e delle dirs per una dirs
    Dim intTipo As Integer ' Tipo (File , Directory o altro) per strListFD(i)
    Dim i As Integer, j As Integer, strDir As String, strS As String
    '1. Salva Vecchio caricato
    Dim intNum1 As Integer
    If Mid(strDirRadice, Len(strDirRadice), 1) = "\" Then
        strDirRadice = Left(strDirRadice, Len(strDirRadice) - 1)
    End If
    For i = 1 To intNumDir
        strListaDirOld(i) = strListaDir(i)
    Next
    intNum1 = intNumDir
    intNumDir = 0
    '2. Rigenera Log e controlla nei vecchi
    If Len(strListaDirOld(1)) = 0 Then
        strDir = ""
    ElseIf Left(strListaDirOld(1), Len(strListaDirOld(1)) - 3) <> Kr.dirRadice Then
        strDir = strDirRadice
    Else
        strDir = ""
    End If
    strListLV1(1) = strDirRadice: intNumLV1 = 1
    ' 2.1 Per ogni directory di un certo livello vengono generate le sottodirectory
    ' -.- e inserite nella lista di uscita lstListaDir ->lstFile
    ' -.- poi viene riportata in lstListaDir per il loop successivo
    
    While (intNumLV1 > 0)
        '2.1 Genera file e dirs per tutte le dir di un livello i (0,1,2,..)
        intNumLV2 = 0
        For i = 1 To intNumLV1
            intNums = 0
            'PARTE BUONA
            intNumDir = intNumDir + 1
            strListaDir(intNumDir) = strListLV1(i) & ":_E"
            If Len(strDir) > 0 Then
                strS = Left(strListaDirOld(1), Len(strListaDirOld(1)) - 3)
                strS = strS + Mid(strListLV1(i), Len(strDir) + 1)
            End If
            For j = 1 To intNum1
                '''WriteErrorLog "(" & Left(strListaDirOld(j), Len(strListaDirOld(j)) - 3) & "):(" & strListLV1(i) & ")"
                If Left(strListaDirOld(j), Len(strListaDirOld(j)) - 3) = strListLV1(i) Then
                    strListaDir(intNumDir) = strListaDirOld(j)
                ElseIf Left(strListaDirOld(j), Len(strListaDirOld(j)) - 3) = strS Then
                    strListaDir(intNumDir) = strListLV1(i) + Right(strListaDirOld(j), 3)
                End If
            Next
            'FINE
            ListaFileEDirs strListLV1(i), strListFD, intNums
            For j = 1 To intNums
                '2.2 Distinzione File o Directory
                If GetAttr(strListLV1(i) & "\" & strListFD(j)) And vbDirectory Then
                    If Left(strListFD(j), 1) <> "." Then
                        intNumLV2 = intNumLV2 + 1
                        strListLV2(intNumLV2) = strListLV1(i) & "\" & strListFD(j)
                    End If
                End If
            Next
        Next
        '2.2 SUCC (Copia della lista di output come lista di input per il loop successivo
        For i = 1 To intNumLV2
            strListLV1(i) = strListLV2(i)
        Next
        intNumLV1 = intNumLV2
    Wend
    SalvaLogFile strChiave, strLogFile
    CaricaLogFile strChiave, strLogFile
End Sub

Public Sub LoadIntoList(lst As ListBox, ByVal StatoE As String, ByVal StatoK As String)
    Dim i As Integer
    lst.Clear
    For i = 1 To intNumDir
        '''Debug.Print Mid(strListaDir(i), Len(strListaDir(i)) - 1, 1)
        '''Debug.Print Mid(strListaDir(i), Len(strListaDir(i)), 1)
        If Len(StatoK) = 0 Or Mid(strListaDir(i), Len(strListaDir(i)) - 1, 1) = StatoK Then
            If Len(StatoE) = 0 Or Mid(strListaDir(i), Len(strListaDir(i)), 1) = StatoE Then
                lst.AddItem strListaDir(i)
            End If
        End If
    Next
End Sub

Public Sub CambiaStato(strStatoE As String, strStatoK As String, ByVal strStatoNuovo As String, Optional strSubDir As String = "")
    Dim strStato1 As String, i As Integer
    If Len(strSubDir) = 0 Then
        For i = 1 To intNumDir
            Debug.Print Mid(strListaDir(i), Len(strListaDir(i)) - 1, 1)
            Debug.Print Mid(strListaDir(i), Len(strListaDir(i)), 1)
            If Len(strStatoK) = 0 Or Mid(strListaDir(i), Len(strListaDir(i)) - 1, 1) = strStatoK Then
                If Len(strStatoE) = 0 Or Mid(strListaDir(i), Len(strListaDir(i)), 1) = strStatoE Then
                    If Len(strStatoNuovo) = 0 Then
                        strStato1 = Mid(strListaDir(i), Len(strListaDir(i)), 1)
                        If strStato1 = "D" Then strStato1 = "E" Else strStato1 = "D"
                    Else
                        strStato1 = strStatoNuovo
                    End If
                    Mid(strListaDir(i), Len(strListaDir(i)), 1) = strStato1
                End If
            End If
        Next
    Else
        For i = 1 To intNumDir
            If InStr(strListaDir(i), strSubDir) > 0 Then
                If Len(strStatoNuovo) = 0 Then
                    strStato1 = Mid(strListaDir(i), Len(strListaDir(i)), 1)
                    If strStato1 = "D" Then strStato1 = "E" Else strStato1 = "D"
                Else
                    strStato1 = strStatoNuovo
                End If
                Mid(strListaDir(i), Len(strListaDir(i)), 1) = strStato1
            End If
        Next
    End If
End Sub

Public Sub SetNewStato(strDir As String, strStatoK As String, strStatoE As String)
    Dim i As Integer
    For i = 1 To intNumDir
        If Left(strListaDir(i), Len(strListaDir(i)) - 3) = strDir Then
            If Len(strStatoK) > 0 Then Mid(strListaDir(i), Len(strListaDir(i)) - 1, 1) = strStatoK
            If Len(strStatoE) > 0 Then Mid(strListaDir(i), Len(strListaDir(i)), 1) = strStatoE
        End If
    Next
End Sub


