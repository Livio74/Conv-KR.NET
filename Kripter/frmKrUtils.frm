VERSION 5.00
Begin VB.Form frmKrUtils 
   Caption         =   "KR Utility"
   ClientHeight    =   984
   ClientLeft      =   48
   ClientTop       =   348
   ClientWidth     =   10356
   LinkTopic       =   "Form1"
   ScaleHeight     =   984
   ScaleWidth      =   10356
   StartUpPosition =   3  'Windows Default
   Begin VB.CommandButton btnEsci 
      Caption         =   "&Esci"
      Height          =   288
      Left            =   9360
      TabIndex        =   5
      Top             =   600
      Width           =   852
   End
   Begin VB.CommandButton btnOK 
      Caption         =   "&OK"
      Height          =   288
      Left            =   3960
      TabIndex        =   4
      Top             =   600
      Width           =   852
   End
   Begin VB.TextBox txtPWD 
      Height          =   288
      Left            =   1320
      TabIndex        =   3
      Top             =   600
      Width           =   2412
   End
   Begin VB.TextBox txtPath 
      Height          =   288
      Left            =   1320
      TabIndex        =   1
      Text            =   "\\kudapc2011\Users\Livio\Root\Livio2\Varie\Soldi"
      Top             =   120
      Width           =   8892
   End
   Begin VB.Label lbl 
      Caption         =   "PWD"
      Height          =   252
      Left            =   120
      TabIndex        =   2
      Top             =   600
      Width           =   972
   End
   Begin VB.Label lblDir 
      Caption         =   "Directory"
      Height          =   252
      Left            =   120
      TabIndex        =   0
      Top             =   120
      Width           =   972
   End
End
Attribute VB_Name = "frmKrUtils"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Private Sub btnEsci_Click()
    Unload Me
End Sub

Private Sub btnOK_Click()
    Dim strLog As String, dateM As Date, strS As String
    strLog = txtPath.Text
    If Mid(strLog, Len(strLog), 1) <> "\" Then strLog = strLog & "\"
    strLog = strLog & "klog.txt"
    dateM = FileDateTime(strLog)
    If Dir(strLog) <> "" Then
        Open strLog For Input As #1
        Line Input #1, strS
        Close #1
        txtPWD.Text = InvKript(Format(dateM, "yyyymmdd") + Hex(Year(dateM)) + Format(dateM, "ddyyyymm") + Hex(Day(dateM) * Month(dateM)), strS)
    Else
        MsgBox "Not KR", vbCritical
    End If
End Sub
