VERSION 5.00
Begin VB.Form minKR 
   Caption         =   "KR"
   ClientHeight    =   564
   ClientLeft      =   48
   ClientTop       =   336
   ClientWidth     =   4764
   Icon            =   "minKR.frx":0000
   LinkTopic       =   "Form1"
   ScaleHeight     =   564
   ScaleWidth      =   4764
   StartUpPosition =   3  'Windows Default
   Begin VB.CommandButton btnEsci 
      Caption         =   "&Esci"
      Height          =   372
      Left            =   3480
      TabIndex        =   3
      Top             =   120
      Width           =   612
   End
   Begin VB.CommandButton btnVai 
      Caption         =   "&VAI"
      Height          =   372
      Left            =   4200
      TabIndex        =   1
      Top             =   120
      Width           =   492
   End
   Begin VB.TextBox txtChiave 
      Height          =   375
      IMEMode         =   3  'DISABLE
      Left            =   120
      PasswordChar    =   "*"
      TabIndex        =   0
      Top             =   120
      Width           =   3135
   End
   Begin VB.Label lblStato 
      BorderStyle     =   1  'Fixed Single
      Height          =   252
      Left            =   120
      TabIndex        =   2
      Top             =   120
      Width           =   4572
   End
End
Attribute VB_Name = "minKR"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Private Sub ManageControls(ByVal boolInvert As Boolean)
    lblStato.Visible = boolInvert
    btnVai.Visible = Not boolInvert: txtChiave.Visible = Not boolInvert
    btnEsci.Visible = Not boolInvert
End Sub

Private Sub btnEsci_Click()
    Unload Me
End Sub

Private Sub btnVai_Click()
    EseguiConKLog
End Sub

Private Sub Form_Load()
    ManageControls False
End Sub

Private Sub txtChiave_KeyPress(KeyAscii As Integer)
    Dim strS As String
    If KeyAscii = 13 Then
        strS = txtChiave
        strS = CaricaLogFile(strS, G_strFileLog)
        If Len(strS) > 0 Then
            RigeneraLog strS, G_strDirRoot, G_strFileLog
            EseguiConKLog
        Else
            MsgBox "Chiave inserita non corretta", vbCritical
        End If
    End If
End Sub

Private Sub EseguiConKLog()
    Dim strNomeFile As String, strS As String, strIn As String
    Dim strErr As String, lngCount As Long, i As Long
    '2. CaricaLogFile
    ManageControls True
    strS = txtChiave.Text
    strS = CaricaLogFile(strS, G_strFileLog)
    If Len(strS) = 0 Then
        MsgBox "Chiave inserita non valida", vbCritical
        ManageControls False: Exit Sub
    End If
    If (G_bolErrLog) Then
        MsgBox "FORMATO LOG NON VALIDO", vbCritical
        ManageControls False: Exit Sub
        Exit Sub
    End If
    '3. Generazione di Tutti i Files
    lblStato = "Stato: Generazione di tutti i file ...."
    lblStato.Refresh
    strErr = Genera(1, G_strDirRoot, G_strFileLog)
    lngCount = GetSize()
    '4. Per Ogni File vi è la K
    If strErr <> "" Then
        MsgBox strErr, vbCritical: Exit Sub
    End If
    If (lngCount <= 0) Then
        MsgBox "Non sono stati trovati files all'interno della directory", vbCritical: Exit Sub
        Exit Sub
    End If
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
    SalvaLogFile txtChiave, G_strFileLog
    '5. Operazione Completata
    lblStato = "Stato: PRONTO!"
    lblStato.Refresh
    lstFileK.Clear
    If Len(G_strErr) > 0 Then
        G_strErr = "<?xml version=""1.0"" encoding=""UTF-8""?><EXCEPTIONS DATETIME=""" + Format(Now, "dd/mm/yy hh:nn:ss") + """>" + G_strErr + "</EXCEPTIONS>"
        ErrorLog G_strErr
    End If
    End
ErrCanc:
    '''MsgBox "Non è possibile cancellare il file: " & strNomeFile
    G_strErr = G_strErr + "<EXCEPTION ID = ""0"" IDREF=""" + CStr(Err.Number) + """ DESCRIPTION=""" + XML.ConvToXML(Err.Description) + """ SOURCE=""" + _
     Err.Source + "DATETIME=""" + Format(Now, "dd/mm/yy hh:nn:ss") + """><DETAILS><FILE>" + XML.ConvToXML(strNomeFile, 1) & "</FILE></DETAILS></EXCEPTION>" + vbCrLf
    Resume Next
End Sub



