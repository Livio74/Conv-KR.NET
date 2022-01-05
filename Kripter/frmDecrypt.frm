VERSION 5.00
Begin VB.Form frmDecrypt 
   Caption         =   "Form1"
   ClientHeight    =   1908
   ClientLeft      =   48
   ClientTop       =   348
   ClientWidth     =   8376
   LinkTopic       =   "Form1"
   ScaleHeight     =   1908
   ScaleWidth      =   8376
   StartUpPosition =   3  'Windows Default
   Begin VB.TextBox Text3 
      Height          =   288
      Left            =   1560
      TabIndex        =   6
      Top             =   1080
      Width           =   6732
   End
   Begin VB.CommandButton btnGet 
      Caption         =   "Chiave"
      Height          =   372
      Left            =   120
      TabIndex        =   5
      Top             =   1440
      Width           =   1332
   End
   Begin VB.CommandButton btnExit 
      Caption         =   "Esci"
      Height          =   372
      Left            =   6960
      TabIndex        =   4
      Top             =   1440
      Width           =   1332
   End
   Begin VB.TextBox Text2 
      Height          =   288
      Left            =   1560
      TabIndex        =   3
      Top             =   600
      Width           =   6732
   End
   Begin VB.TextBox Text1 
      Height          =   288
      Left            =   1560
      TabIndex        =   1
      Top             =   120
      Width           =   6732
   End
   Begin VB.Label Label3 
      Caption         =   "Chiave2"
      Height          =   252
      Left            =   120
      TabIndex        =   7
      Top             =   1080
      Width           =   1212
   End
   Begin VB.Label Label2 
      Caption         =   "Chiave1"
      Height          =   252
      Left            =   120
      TabIndex        =   2
      Top             =   600
      Width           =   1212
   End
   Begin VB.Label Label1 
      Caption         =   "File klog"
      Height          =   252
      Left            =   120
      TabIndex        =   0
      Top             =   120
      Width           =   1212
   End
End
Attribute VB_Name = "frmDecrypt"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Private Sub btnExit_Click()
    Unload Me
End Sub

Private Sub btnGet_Click()
    Text2.Text = reverseKey(Text1.Text, True)
    Text3.Text = reverseKey(Text1.Text, False)
End Sub
