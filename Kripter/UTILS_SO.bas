Attribute VB_Name = "UTILS_SO"
Option Explicit

Const MAX_PATH = 260

Type FILETIME
        dwLowDateTime As Long
        dwHighDateTime As Long
End Type

Type WIN32_FIND_DATA
        dwFileAttributes As Long
        ftCreationTime As FILETIME
        ftLastAccessTime As FILETIME
        ftLastWriteTime As FILETIME
        nFileSizeHigh As Long
        nFileSizeLow As Long
        dwReserved0 As Long
        dwReserved1 As Long
        cFileName As String * MAX_PATH
        cAlternate As String * 14
End Type

Private Type SYSTEMTIME
     wYear      As Integer
     wMonth     As Integer
     wDayOfWeek As Integer
     wDay       As Integer
     wHour      As Integer
     wMinute    As Integer
     wSecond    As Integer
     wMillisecs As Integer
End Type

Private Const OPEN_EXISTING = 3
Private Const FILE_SHARE_READ = &H1
Private Const FILE_SHARE_WRITE = &H2
Private Const GENERIC_WRITE = &H40000000

Declare Function FindFirstFile Lib "kernel32" Alias "FindFirstFileA" (ByVal lpFileName As String, lpFindFileData As WIN32_FIND_DATA) As Long
Declare Function GetLastError Lib "kernel32" () As Long
Declare Function FindNextFile Lib "kernel32" Alias "FindNextFileA" (ByVal hFindFile As Long, lpFindFileData As WIN32_FIND_DATA) As Long
Declare Function FindClose Lib "kernel32" (ByVal hFindFile As Long) As Long

Private Declare Function LocalFileTimeToFileTime Lib _
     "kernel32" (lpLocalFileTime As FILETIME, _
      lpFileTime As FILETIME) As Long

Private Declare Function SetFileTime Lib "kernel32" _
   (ByVal hFile As Long, ByVal MullP As Long, _
    ByVal NullP2 As Long, lpLastWriteTime _
    As FILETIME) As Long

Private Declare Function SystemTimeToFileTime Lib _
    "kernel32" (lpSystemTime As SYSTEMTIME, lpFileTime _
    As FILETIME) As Long
    
Private Declare Function CloseHandle Lib "kernel32" _
   (ByVal hObject As Long) As Long

Private Declare Function CreateFile Lib "kernel32" Alias _
   "CreateFileA" (ByVal lpFileName As String, _
   ByVal dwDesiredAccess As Long, _
   ByVal dwShareMode As Long, _
   ByVal lpSecurityAttributes As Long, _
   ByVal dwCreationDisposition As Long, _
   ByVal dwFlagsAndAttributes As Long, _
   ByVal hTemplateFile As Long) _
   As Long


' Data una directory lista tutti i file html esistenti
Public Sub ListaFileEDirs(strDir As String, ByRef strLista() As String, ByRef intNum As Integer)
    Dim lpDett As WIN32_FIND_DATA, hSearch As Long, lngLastErr As Long
    Dim bolTrovatoFile As Boolean, strFile As String
    intNum = 0
    lngLastErr = GetLastError
    hSearch = FindFirstFile(strDir & "\*.*", lpDett)
    bolTrovatoFile = True
    While (bolTrovatoFile)
            strFile = Left(lpDett.cFileName, InStr(1, lpDett.cFileName, Chr(0)) - 1)
            intNum = intNum + 1
            strLista(intNum) = strFile
            bolTrovatoFile = FindNextFile(hSearch, lpDett)
    Wend
    FindClose hSearch
End Sub


Public Sub ErrorLog(ByRef strS As String)
    Dim intFile As Integer
    intFile = FreeFile
    Open App.Path + "\ErrorLog.xml" For Output As intFile
    Print #intFile, strS
    Close intFile
End Sub

Public Sub WriteErrorLog(ByRef strS As String)
    Dim intFile As Integer
    intFile = FreeFile
    Open App.Path + "\ErrorLog.txt" For Append As intFile
    Print #intFile, strS
    Close intFile
End Sub

Public Function SalvaListaFile(ByVal strFile As String, ByVal strDir As String, ByVal strChiave As String, ByVal strFileLog As String) As String
    Dim strListLV1(5000) As String, intNumLV1 As Integer ' Lista directory livello i-1
    Dim strListLV2(5000) As String, intNumLV2 As Integer ' Lista directory livello i
    Dim strListFD(5000) As String, intNums As Integer ' Lista dei files e delle dirs per una dirs
    Dim intTipo As Integer ' Tipo (File , Directory o altro) per strListFD(i)
    Dim strOut As String, i As Integer, j As Integer
    ' 1. Inizializzazione
    strOut = ""
    If ExistsFile(strFile) Then Kill strFile
    Open strFile For Binary As #510
    strListLV1(1) = strDir: intNumLV1 = 1
    ' 2. Per ogni directory di un certo livello vengono generate le sottodirectory
    ' -. e inserite nella lista di uscita lstListaDir ->lstFile
    ' -. poi viene riportata in lstListaDir per il loop successivo
    While (intNumLV1 > 0)
        '2.1 Genera file e dirs per tutte le dir di un livello i (0,1,2,..)
        intNumLV2 = 0
        For i = 1 To intNumLV1
            intNums = 0
            ListaFileEDirs strListLV1(i), strListFD, intNums
            For j = 1 To intNums
                '2.2 Distinzione File o Directory
                If GetAttr(strListLV1(i) & "\" & strListFD(j)) And vbDirectory Then
                    If Left(strListFD(j), 1) <> "." Then
                        intNumLV2 = intNumLV2 + 1
                        strListLV2(intNumLV2) = strListLV1(i) & "\" & strListFD(j)
                    End If
                Else
                    If strListFD(j) <> strFileLog Then
                        strOut = strOut + strListLV1(i) & vbTab & strListFD(j) + vbTab + KritpStr2(strListFD(j), strChiave) + vbCrLf
                    End If
                End If
            Next j
        Next
        '2.2 Eventuale salvataggio
        If Len(strOut) > 30000 Then
            Put #510, , strOut: strOut = ""
        End If
        '2.3 SUCC (Copia della lista di output come lista di input per il loop successivo
        For i = 1 To intNumLV2
            strListLV1(i) = strListLV2(i)
        Next
        intNumLV1 = intNumLV2
    Wend
    '3. Salvataggio finale e chiusura
    If Len(strOut) > 0 Then
        Put #510, , strOut
    End If
    Close #510
End Function

Public Function ExistsFile(strFile As String, Optional strDir As String = "") As Boolean
    If Len(strDir) = 0 Then
        ExistsFile = Len(Dir(strFile)) > 0
    End If
End Function

Public Function ExistsDir(strDir As String) As Boolean
    ExistsDir = Len(Dir(strDir + "\*.*")) > 0
End Function

Public Function SetFileDateTime(ByVal FileName As String, ByVal TheDate As String) As Boolean
    '************************************************
    'PURPOSE:    Set File Date (and optionally time)
    '            for a given file)
             
    'PARAMETERS: TheDate -- Date to Set File's Modified Date/Time
    '            FileName -- The File Name
    
    'Returns:    True if successful, false otherwise
    '************************************************
    If Dir(FileName) = "" Then Exit Function
    If Not IsDate(TheDate) Then Exit Function
    
    Dim lFileHnd As Long
    Dim lRet As Long
    
    Dim typFileTime As FILETIME
    Dim typLocalTime As FILETIME
    Dim typSystemTime As SYSTEMTIME
    
    With typSystemTime
        .wYear = Year(TheDate)
        .wMonth = Month(TheDate)
        .wDay = Day(TheDate)
        .wDayOfWeek = Weekday(TheDate) - 1
        .wHour = Hour(TheDate)
        .wMinute = Minute(TheDate)
        .wSecond = Second(TheDate)
    End With
    
    If ExistsFile(FileName) Then
        lRet = SystemTimeToFileTime(typSystemTime, typLocalTime)
        lRet = LocalFileTimeToFileTime(typLocalTime, typFileTime)
        
        lFileHnd = CreateFile(FileName, GENERIC_WRITE, _
            FILE_SHARE_READ Or FILE_SHARE_WRITE, ByVal 0&, _
            OPEN_EXISTING, 0, 0)
            
        lRet = SetFileTime(lFileHnd, ByVal 0&, ByVal 0&, _
                 typFileTime)
        
        CloseHandle lFileHnd
    Else
        lRet = 0
    End If
    SetFileDateTime = lRet > 0
    
End Function



