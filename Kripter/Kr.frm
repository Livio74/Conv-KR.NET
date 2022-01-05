VERSION 5.00
Begin VB.Form Kr 
   Caption         =   "KR"
   ClientHeight    =   7272
   ClientLeft      =   60
   ClientTop       =   456
   ClientWidth     =   11160
   Icon            =   "Kr.frx":0000
   LinkTopic       =   "Form1"
   ScaleHeight     =   7272
   ScaleWidth      =   11160
   StartUpPosition =   3  'Windows Default
   Begin VB.CheckBox chkEnableFileList 
      Caption         =   "Abilita Lista File"
      Height          =   255
      Left            =   120
      TabIndex        =   15
      Top             =   6240
      Width           =   3135
   End
   Begin VB.TextBox txtFileList 
      Height          =   285
      Left            =   120
      TabIndex        =   14
      Top             =   6600
      Width           =   10935
   End
   Begin VB.CommandButton btnCreaFileList 
      Caption         =   "Crea File List"
      Height          =   375
      Left            =   120
      TabIndex        =   13
      Top             =   5760
      Width           =   3135
   End
   Begin VB.CheckBox chkBlock 
      Caption         =   "Blocco"
      Height          =   255
      Left            =   7080
      TabIndex        =   12
      Top             =   0
      Value           =   1  'Checked
      Width           =   2775
   End
   Begin VB.CommandButton btnDir 
      Caption         =   "&Importa dal Copia/Incolla"
      Height          =   375
      Left            =   120
      TabIndex        =   11
      Top             =   5280
      Width           =   3135
   End
   Begin VB.CommandButton btnLogFile 
      Caption         =   "Log file"
      Height          =   375
      Left            =   120
      TabIndex        =   10
      Top             =   4800
      Width           =   3135
   End
   Begin VB.TextBox txtChiave 
      Height          =   375
      IMEMode         =   3  'DISABLE
      Left            =   120
      PasswordChar    =   "*"
      TabIndex        =   4
      Top             =   3840
      Width           =   3135
   End
   Begin VB.CommandButton btnVai 
      Caption         =   "VAI"
      Height          =   375
      Left            =   120
      TabIndex        =   3
      Top             =   4320
      Width           =   3135
   End
   Begin VB.ListBox lstFileK 
      Height          =   6000
      ItemData        =   "Kr.frx":030A
      Left            =   3480
      List            =   "Kr.frx":030C
      TabIndex        =   2
      Top             =   360
      Width           =   7455
   End
   Begin VB.DirListBox dirRadice 
      Height          =   2115
      Left            =   120
      TabIndex        =   1
      Top             =   1200
      Width           =   3135
   End
   Begin VB.DriveListBox drv 
      Height          =   315
      Left            =   120
      TabIndex        =   0
      Top             =   360
      Width           =   3135
   End
   Begin VB.Label lblStato 
      BorderStyle     =   1  'Fixed Single
      Caption         =   "Stato: PRONTO"
      Height          =   255
      Left            =   120
      TabIndex        =   9
      Top             =   6960
      Width           =   10935
   End
   Begin VB.Label lbl 
      Caption         =   "Lista Files k"
      Height          =   255
      Index           =   3
      Left            =   3600
      TabIndex        =   8
      Top             =   120
      Width           =   3135
   End
   Begin VB.Label lbl 
      Caption         =   "Drive"
      Height          =   255
      Index           =   2
      Left            =   120
      TabIndex        =   7
      Top             =   120
      Width           =   3135
   End
   Begin VB.Label lbl 
      Caption         =   "Directory radice"
      Height          =   255
      Index           =   1
      Left            =   120
      TabIndex        =   6
      Top             =   960
      Width           =   3135
   End
   Begin VB.Label lbl 
      Caption         =   "Chiave"
      Height          =   255
      Index           =   0
      Left            =   120
      TabIndex        =   5
      Top             =   3480
      Width           =   3135
   End
End
Attribute VB_Name = "Kr"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit
Dim strFileLog As String

Private Sub btnCreaFileList_Click()
    Dim strS As String
    If Len(txtFileList) = 0 Then
        MsgBox "Non è possibile creare il file ""Lista file""", vbExclamation: Exit Sub
    End If
    If ExistsFile(txtFileList.Text) Then
        If MsgBox("Vuoi sovrascrivere il file esistente?", vbYesNo + vbQuestion + vbDefaultButton2) = vbNo Then Exit Sub
    End If
    lblStato = "Stato: Generazione della lista di file ...."
    lblStato.Refresh
    strS = SalvaListaFile(txtFileList.Text, dirRadice.Path, txtChiave.Text, strFileLog)
    If Len(strS) > 0 Then
        MsgBox strS, vbExclamation
    End If
    lblStato = "PRONTO!"
    lblStato.Refresh
End Sub

Private Sub btnDir_Click()
    If Len(Clipboard.GetText) > 0 Then
        drv = Left(dirRadice, 2)
        dirRadice = Clipboard.GetText
        chkBlock.Value = 0
        G_strFileList = dirRadice.Path + "\FileList.txt"
        strFileLog = dirRadice.Path + "\klog.txt"
        txtChiave.SetFocus
    End If
End Sub

Private Sub btnLogFile_Click()
    G_strDirRoot = dirRadice.Path
    G_strFileLog = dirRadice.Path + "\klog.txt"
    Load LogFile
    LogFile.Show
End Sub

Private Sub btnVai_Click()
    Dim i As Long, j As Long, strDir As String, k As Long, bNotEsci As Boolean
    Dim lngCount As Long
    '1. Controlli di correttezza
    If Len(txtChiave) = 0 Then
        MsgBox "E' necessario inserire una chiave", vbCritical: Exit Sub
    End If
    If chkBlock.Value = 1 Then
        MsgBox "E' necessario effettuare lo sblocco", vbCritical: Exit Sub
    End If
    If Len(txtFileList) = 0 Then
        EseguiConKLog
    Else
        EseguiConListaKript
    End If
    Exit Sub
End Sub

Private Sub EseguiConKLog()
    Dim strNomeFile As String, strS As String, strIn As String
    Dim strErr As String, lngCount As Long, i As Long
    If Len(strFileLog) = 0 Then
        MsgBox "File log mancante", vbCritical: Exit Sub
    End If
    '2. CaricaLogFile
    strS = txtChiave.Text
    strS = CaricaLogFile(strS, strFileLog)
    If Len(strS) = 0 Then
        MsgBox "Chiave inserita non valida", vbCritical: Exit Sub
    End If
    If (G_bolErrLog) Then
        MsgBox "FORMATO LOG NON VALIDO", vbCritical
        Exit Sub
    End If
    '3. Generazione di Tutti i Files
    lblStato = "Stato: Generazione di tutti i file ...."
    lblStato.Refresh
    lstFileK.Clear
    strErr = Genera(1, dirRadice.Path, strFileLog)
    lngCount = GetSize()
    '4. Per Ogni File vi è la K
    If strErr <> "" Then
        MsgBox strErr, vbCritical: Exit Sub
    End If
    If (lngCount <= 0) Then
        MsgBox "Non sono stati trovati files all'interno della directory", vbCritical: Exit Sub
        Exit Sub
    End If
    lstFileK.Clear
    For i = 0 To lngCount - 1
        strNomeFile = GetFile(i)
        lstFileK.AddItem strNomeFile
    Next
    For i = 0 To lngCount - 1
        '3.1 Crpt del file (che cambia anche nome)
        ''lstFileK.ListIndex = i - 1
        strNomeFile = GetFile(i)
        '3.2 Conversione chiave da testo ad array di byte
        If i Mod 1000 = 0 Then DoEvents
        lblStato = "Stato: kritp di " & CStr(i) & "/" & CStr(lngCount)
        lblStato.Refresh
        Kritp strNomeFile, txtChiave
        '3.2 Cancellazione del file vecchio
        On Error GoTo ErrCanc
        '''Kill strNomeFile
    Next i
    '4. SalvaLogFile
    SalvaLogFile txtChiave, strFileLog
    '5. Operazione Completata
    MsgBox "Operazione Completata"
    lblStato = "Stato: PRONTO!"
    lblStato.Refresh
    lstFileK.Clear
    If Len(G_strErr) > 0 Then
        G_strErr = "<?xml version=""1.0"" encoding=""UTF-8""?><EXCEPTIONS DATETIME=""" + Format(Now, "dd/mm/yy hh:nn:ss") + """>" + G_strErr + "</EXCEPTIONS>"
        ErrorLog G_strErr
    End If
ErrCanc:
    '''MsgBox "Non è possibile cancellare il file: " & strNomeFile
    G_strErr = G_strErr + "<EXCEPTION ID = ""0"" IDREF=""" + CStr(Err.Number) + """ DESCRIPTION=""" + XML.ConvToXML(Err.Description) + """ SOURCE=""" + _
     Err.Source + "DATETIME=""" + Format(Now, "dd/mm/yy hh:nn:ss") + """><DETAILS><FILE>" + XML.ConvToXML(strNomeFile, 1) & "</FILE></DETAILS></EXCEPTION>" + vbCrLf
    Resume Next
End Sub

Private Sub EseguiConListaKript()
    Dim strNomeFile As String, strS As String, strIn As String
    Dim strErr As String, strDir As String
    Dim bNotEsci As Boolean, i As Long, j As Long, k As Long
    If Not ExistsFile(txtFileList.Text) Then
        MsgBox "Lista File non trovato", vbExclamation: Exit Sub
    End If
    i = 1: j = InStrRev(txtFileList.Text, "\")
    If j > 0 Then strDir = Left(txtFileList.Text, j - 1) Else strDir = App.Path
    Open txtFileList For Input As #510
    bNotEsci = EOF(510)
    While Not bNotEsci
        Line Input #510, strIn
        '3.1 Crpt del file in un'altro
        ''lstFileK.ListIndex = i - 1
        j = InStr(strIn, vbTab)
        If j = 0 Then
            strDir = strIn: If Mid(strDir, Len(strDir), 1) <> "\" Then strDir = strDir + "\"
        Else
            '3.2 Conversione chiave da testo ad array di byte
            k = InStr(j + 1, strIn, vbTab)
            strNomeFile = Replace(Left(strIn, k - 1), vbTab, "\")
            strS = strDir + Mid(strIn, k + 1)
            lblStato = "Stato: kritp di " & CStr(i) & "/ ? "
            lblStato.Refresh
            Kritp strNomeFile, txtChiave.Text, strS
            '3.2 Cancellazione del file vecchio
        End If
        On Error GoTo ErrCanc
        ''NEXT
        i = i + 1
        bNotEsci = EOF(510)
    Wend
    Close #510
ErrCanc:
    '''MsgBox "Non è possibile cancellare il file: " & strNomeFile
    G_strErr = G_strErr + "<EXCEPTION ID = ""0"" IDREF=""" + CStr(Err.Number) + """ DESCRIPTION=""" + XML.ConvToXML(Err.Description) + """ SOURCE=""" + _
     Err.Source + "DATETIME=""" + Format(Now, "dd/mm/yy hh:nn:ss") + """><DETAILS><FILE>" + XML.ConvToXML(strNomeFile, 1) & "</FILE></DETAILS></EXCEPTION>" + vbCrLf
    Resume Next
End Sub

Private Sub chkEnableFileList_Click()
    If chkEnableFileList.Value = Checked Then
        txtFileList.Text = G_strFileList
    Else
        txtFileList.Text = ""
    End If
End Sub

Private Sub drv_Change()
    dirRadice = Left(drv, 2) & "\"
    strFileLog = dirRadice & "\klog.txt"
End Sub

Private Sub Form_Activate()
    txtChiave.SetFocus
End Sub

Private Sub Form_Load()
    If Len(G_strDirRoot) > 0 Then
        drv = Left(G_strDirRoot, 2)
        dirRadice = G_strDirRoot
        chkBlock = 0: G_lng_NumFiles = 0
    Else
        drv = "c:"
        dirRadice = "C:\"
        G_lng_NumFiles = 0
    End If
    strFileLog = dirRadice & "\klog.txt"
    G_bolEsisteLog = False
End Sub

Private Sub txtChiave_KeyPress(KeyAscii As Integer)
    Dim strS As String
    If KeyAscii = 13 Then
        strS = txtChiave
        strS = CaricaLogFile(strS, strFileLog)
        If Len(strS) > 0 Then
            RigeneraLog strS, dirRadice.Path, strFileLog
            EseguiConKLog
        Else
            MsgBox "Chiave inserita non corretta", vbCritical
        End If
    End If
End Sub
