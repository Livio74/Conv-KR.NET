Attribute VB_Name = "FILE_LIST"
Option Explicit

Const MAX_PATH1 As Integer = 5000
Const MAX_PATH2 As Long = 32267

Private m_ind As Long
Private m_cnt As Long
Private m_Type As Integer
Private m_Resource As String
Private m_List(0 To MAX_PATH2) As String

Public Sub INIT_FL()
    m_ind = 0: m_cnt = 0
End Sub

Public Function Genera(ByVal intType As String, ByVal strResource As String, ByVal strParam1 As String) As String
    Dim strOut As String
    m_ind = 0: m_cnt = 0: strOut = ""
    If intType = 1 Then
        m_Type = 1: m_Resource = strResource
        strOut = GeneraDaKLog(strResource, strParam1)
    End If
    Genera = strOut
End Function

Public Function GetSize()
    GetSize = m_cnt
End Function

Public Function GeneraDaKLog(ByVal strDirRadice As String, ByVal strFileLog As String)
    Dim strListLV1(MAX_PATH1) As String, intNumLV1 As Integer ' Lista directory livello i-1
    Dim strListLV2(MAX_PATH1) As String, intNumLV2 As Integer ' Lista directory livello i
    Dim strListFD(MAX_PATH1) As String, intNums As Integer ' Lista dei files e delle dirs per una dirs
    Dim intTipo As Integer ' Tipo (File , Directory o altro) per strListFD(i)
    Dim strErr As String, strS As String, i As Integer, j As Integer
    Dim strFileLogName As String
    strErr = ""
    If Mid(strDirRadice, Len(strDirRadice), 1) = "\" Then strDirRadice = Left(strDirRadice, Len(strDirRadice) - 1)
    strFileLogName = Mid(strFileLog, Len(strDirRadice) + 2)
    strListLV1(1) = strDirRadice: intNumLV1 = 1
    ' 2. Per ogni directory di un certo livello vengono generate le sottodirectory
    ' -. e inserite nella lista di uscita lstListaDir ->lstFile
    ' -. poi viene riportata in lstListaDir per il loop successivo
    While (intNumLV1 > 0) And strErr = ""
        '2.1 Genera file e dirs per tutte le dir di un livello i (0,1,2,..)
        intNumLV2 = 0
        For i = 1 To intNumLV1
            intNums = 0
            ListaFileEDirs strListLV1(i), strListFD, intNums
            If IsStato(strListLV1(i)) <> "A" Then
                SetStato strListLV1(i)
                For j = 1 To intNums
                    '2.2 Distinzione File o Directory
                    If GetAttr(strListLV1(i) & "\" & strListFD(j)) And vbDirectory Then
                        If Left(strListFD(j), 1) <> "." Then
                            intNumLV2 = intNumLV2 + 1
                            strListLV2(intNumLV2) = strListLV1(i) & "\" & strListFD(j)
                        End If
                    Else
                        If IsStato(strListLV1(i)) = "E" Then
                            If strListFD(j) <> strFileLogName Then
                                If m_ind < MAX_PATH2 Then
                                    strS = strListLV1(i) & "\" & strListFD(j)
                                    m_List(m_cnt) = "." & Mid(strS, Len(strDirRadice) + 1)
                                    m_cnt = m_cnt + 1
                                Else
                                    strErr = "OVERFLOW"
                                End If
                            ElseIf strListLV1(i) <> strDirRadice Then
                                '''MsgBox "ATTENZIONE FILE LOG PRESENTE IN " & strListLV1(i), vbExclamation
                            End If
                        End If
                    End If
                Next j
            End If
        Next
        '2.2 SUCC (Copia della lista di output come lista di input per il loop successivo
        For i = 1 To intNumLV2
            strListLV1(i) = strListLV2(i)
        Next
        intNumLV1 = intNumLV2
    Wend
    GeneraDaKLog = strErr
End Function

Public Function GetFile(ByVal lngInd As Long) As String
    Dim strOut As String
    strOut = ""
    If m_ind < m_cnt Then
        m_ind = lngInd
        strOut = m_List(lngInd)
        If Left(strOut, 1) = "." Then
            strOut = m_Resource & Mid(m_List(lngInd), 2)
        End If
    End If
    GetFile = strOut
End Function

Public Function MoveFirst() As String

End Function

Public Function MoveNext() As String

End Function

Public Sub DESTROY_FL()

End Sub
