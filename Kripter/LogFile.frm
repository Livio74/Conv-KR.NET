VERSION 5.00
Begin VB.Form LogFile 
   Caption         =   "Log File"
   ClientHeight    =   7320
   ClientLeft      =   60
   ClientTop       =   456
   ClientWidth     =   10056
   Icon            =   "LogFile.frx":0000
   LinkTopic       =   "Form1"
   ScaleHeight     =   7320
   ScaleWidth      =   10056
   StartUpPosition =   3  'Windows Default
   Begin VB.CheckBox chkVisualNE 
      Caption         =   "Visualizza file NE"
      Height          =   255
      Left            =   4440
      TabIndex        =   14
      Top             =   5640
      Value           =   1  'Checked
      Width           =   1935
   End
   Begin VB.CheckBox chkVisualE 
      Caption         =   "Visualizza file E"
      Height          =   255
      Left            =   4440
      TabIndex        =   13
      Top             =   5280
      Value           =   1  'Checked
      Width           =   1935
   End
   Begin VB.CheckBox chkVisualNK 
      Caption         =   "Visualizza file NK"
      Height          =   255
      Left            =   4440
      TabIndex        =   12
      Top             =   4920
      Value           =   1  'Checked
      Width           =   1935
   End
   Begin VB.CheckBox chkVisualK 
      Caption         =   "Visualizza file K"
      Height          =   255
      Left            =   4440
      TabIndex        =   11
      Top             =   4560
      Value           =   1  'Checked
      Width           =   1935
   End
   Begin VB.CommandButton btnConferma 
      Caption         =   "CONFERMA"
      Height          =   495
      Left            =   6240
      TabIndex        =   10
      Top             =   6720
      Width           =   1815
   End
   Begin VB.CheckBox chkSubDirs 
      Caption         =   "Anche sotto directory"
      Height          =   495
      Left            =   7680
      TabIndex        =   9
      Top             =   5640
      Width           =   1695
   End
   Begin VB.OptionButton OptStato 
      Caption         =   "Stato Cambiato"
      Height          =   255
      Index           =   2
      Left            =   7680
      TabIndex        =   8
      Top             =   5280
      Value           =   -1  'True
      Width           =   1815
   End
   Begin VB.OptionButton OptStato 
      Caption         =   "Stato D"
      Height          =   255
      Index           =   1
      Left            =   7680
      TabIndex        =   7
      Top             =   4920
      Width           =   1815
   End
   Begin VB.OptionButton OptStato 
      Caption         =   "Stato E"
      Height          =   255
      Index           =   0
      Left            =   7680
      TabIndex        =   6
      Top             =   4560
      Width           =   1815
   End
   Begin VB.CommandButton btnVisualizzaFiles 
      Caption         =   "Visualizza i files"
      Height          =   495
      Left            =   120
      TabIndex        =   5
      Top             =   6720
      Width           =   1815
   End
   Begin VB.CommandButton btnStato 
      Caption         =   "Cambia stato"
      Height          =   495
      Left            =   2040
      TabIndex        =   4
      Top             =   6720
      Width           =   1815
   End
   Begin VB.CommandButton btnEsci 
      Caption         =   "Esci"
      Height          =   495
      Left            =   8160
      TabIndex        =   3
      Top             =   6720
      Width           =   1815
   End
   Begin VB.ListBox lstDir 
      Height          =   4080
      ItemData        =   "LogFile.frx":030A
      Left            =   120
      List            =   "LogFile.frx":030C
      TabIndex        =   2
      Top             =   120
      Width           =   9735
   End
   Begin VB.FileListBox lstFiles 
      Height          =   1992
      Left            =   120
      TabIndex        =   1
      Top             =   4440
      Width           =   3975
   End
   Begin VB.CommandButton Rigenera 
      Caption         =   "Rigenera LOG File"
      Height          =   495
      Left            =   3960
      TabIndex        =   0
      Top             =   6720
      Width           =   1695
   End
End
Attribute VB_Name = "LogFile"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit

Private m_StatoE As String
Private m_StatoK As String

Private Sub btnConferma_Click()
    Dim strS As String
    strS = Kr.txtChiave
    KLOG.SalvaLogFile strS, G_strFileLog
    MsgBox "Salvato file Log", vbExclamation
    Unload Me
End Sub

Private Sub btnEsci_Click()
    Unload Me
End Sub

Private Sub btnStato_Click()
    Dim StatoNuovo As String
    If OptStato(0) Then
        StatoNuovo = "E"
    ElseIf OptStato(1) Then
        StatoNuovo = "D"
    ElseIf OptStato(2) Then
        StatoNuovo = ""
    End If
    If chkSubDirs = 0 Then
        If MsgBox("Vuoi solo la directory selezionata?", vbYesNo) = vbNo Then
            KLOG.CambiaStato m_StatoE, m_StatoK, StatoNuovo
        Else
            If Len(lstDir) = 0 Then
                MsgBox "Per applicare il cambio di stato alla sottodirectory" & Chr(13) & _
                 "bisogna selezionare la sottodirectory!", vbExclamation
                Exit Sub
            End If
            KLOG.SetNewStato Left(CStr(lstDir), Len(CStr(lstDir)) - 3), "", StatoNuovo
        End If
    Else
        If lstDir.ListIndex >= 0 Then
            KLOG.CambiaStato m_StatoE, m_StatoK, StatoNuovo, Left(CStr(lstDir), Len(CStr(lstDir)) - 3)
        Else
            MsgBox "Per avere le sottodirectory è necessario " & _
             "specificare una sola directory selezionandola", vbExclamation
        End If
    End If
    KLOG.LoadIntoList lstDir, m_StatoE, m_StatoK
End Sub

Private Sub btnVisualizzaFiles_Click()
    On Error Resume Next
    lstFiles.Path = Left(lstDir, Len(lstDir) - 3)
    If Err.Number = 0 Then lstFiles.Refresh
    If Err.Number > 0 Then
        MsgBox Err.Description, vbCritical
    End If
    On Error GoTo 0
End Sub

Private Sub chkVisualE_Click()
    '''3
    If chkVisualE = 0 And chkVisualNE = 0 Then
        m_StatoE = ""
    ElseIf chkVisualE = 0 And chkVisualNE = 1 Then
        m_StatoE = "D"
    ElseIf chkVisualE = 1 And chkVisualNE = 0 Then
        m_StatoE = "E"
    ElseIf chkVisualE = 1 And chkVisualNE = 1 Then
        m_StatoE = ""
    End If
    KLOG.LoadIntoList lstDir, m_StatoE, m_StatoK
End Sub

Private Sub chkVisualK_Click()
    '''1
    If chkVisualK = 0 And chkVisualNK = 0 Then
        m_StatoK = ""
    ElseIf chkVisualK = 0 And chkVisualNK = 1 Then
        m_StatoK = "_"
    ElseIf chkVisualK = 1 And chkVisualNK = 0 Then
        m_StatoK = "K"
    ElseIf chkVisualK = 1 And chkVisualNK = 1 Then
        m_StatoK = ""
    End If
    KLOG.LoadIntoList lstDir, m_StatoE, m_StatoK
End Sub

Private Sub chkVisualNE_Click()
    '''4
    If chkVisualE = 0 And chkVisualNE = 0 Then
        m_StatoE = ""
    ElseIf chkVisualE = 0 And chkVisualNE = 1 Then
        m_StatoE = "D"
    ElseIf chkVisualE = 1 And chkVisualNE = 0 Then
        m_StatoE = "E"
    ElseIf chkVisualE = 1 And chkVisualNE = 1 Then
        m_StatoE = ""
    End If
    KLOG.LoadIntoList lstDir, m_StatoE, m_StatoK
End Sub

Private Sub chkVisualNK_Click()
    If chkVisualK = 0 And chkVisualNK = 0 Then
        m_StatoK = ""
    ElseIf chkVisualK = 0 And chkVisualNK = 1 Then
        m_StatoK = "_"
    ElseIf chkVisualK = 1 And chkVisualNK = 0 Then
        m_StatoK = "K"
    ElseIf chkVisualK = 1 And chkVisualNK = 1 Then
        m_StatoK = ""
    End If
    KLOG.LoadIntoList lstDir, m_StatoE, m_StatoK
End Sub

Private Sub Form_Load()
    Dim strKey As String, strS As String
    m_StatoE = "": m_StatoK = ""
    strS = Kr.txtChiave
    If strS = "" Then
        MsgBox "Inserire la Chiave", vbExclamation
        Kr.txtChiave = "": Exit Sub
    End If
    strS = KLOG.CaricaLogFile(strS, G_strFileLog)
    If strS = "" Then
        MsgBox "Inserire chiave non valida", vbExclamation: Exit Sub
        Kr.txtChiave = "": Exit Sub
    End If
    KLOG.LoadIntoList lstDir, m_StatoE, m_StatoK
End Sub

Private Sub optVisual_Click(Index As Integer)
    '' PER ORA Niente
End Sub

Private Sub lstDir_DblClick()
    Dim risp As Integer, strStatoK As String
    btnVisualizzaFiles_Click
    risp = MsgBox("Osserva la lista dei file :" & _
    "Scegli Sì se vuoi impostare lo stato directory a K" & Chr(13) & _
    "No per lo stato a" & Chr(13) & " Annulla se non lo vuoi impostare", vbYesNoCancel)
    '''MsgBox CStr(risp)
    If risp = vbYes Then
        KLOG.SetNewStato Left(CStr(lstDir), Len(CStr(lstDir)) - 3), "K", ""
    ElseIf risp = vbNo Then
        KLOG.SetNewStato Left(CStr(lstDir), Len(CStr(lstDir)) - 3), "_", ""
    End If
    KLOG.LoadIntoList lstDir, m_StatoE, m_StatoK
End Sub

Private Sub Rigenera_Click()
    If Kr.txtChiave = "" Then
        MsgBox "Chiave non valida", vbExclamation
        Exit Sub
    End If
    KLOG.RigeneraLog Kr.txtChiave, G_strDirRoot, G_strFileLog
    MsgBox "Log Rigenerato", vbExclamation
    KLOG.LoadIntoList lstDir, m_StatoE, m_StatoK
End Sub
