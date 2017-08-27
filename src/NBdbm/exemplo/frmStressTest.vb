Public Class frmStressTest
    Inherits System.Windows.Forms.Form

  Private tr1 As Op

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

    End Sub

    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
  Friend WithEvents lbl_Titulo As System.Windows.Forms.Label
  Friend WithEvents T1_Sinal As System.Windows.Forms.Panel
  Friend WithEvents lbl_T1 As System.Windows.Forms.Label
  Friend WithEvents lbl_T1_Conflito As System.Windows.Forms.Label
  Friend WithEvents StatusBar1 As System.Windows.Forms.StatusBar
  Friend WithEvents txt_T1 As System.Windows.Forms.TextBox
  Friend WithEvents Panel1 As System.Windows.Forms.Panel
  Friend WithEvents Panel2 As System.Windows.Forms.Panel
  Friend WithEvents txt_T2 As System.Windows.Forms.TextBox
  Friend WithEvents lbl_T2_Conflito As System.Windows.Forms.Label
  Friend WithEvents lbl_T2 As System.Windows.Forms.Label
  Friend WithEvents T2_Sinal As System.Windows.Forms.Panel
  Friend WithEvents Panel3 As System.Windows.Forms.Panel
  Friend WithEvents bt_T1 As System.Windows.Forms.Button
  Friend WithEvents ImageList1 As System.Windows.Forms.ImageList
  Friend WithEvents bt_T2 As System.Windows.Forms.Button
  Friend WithEvents ProgressBar1 As System.Windows.Forms.ProgressBar
  <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
    Me.components = New System.ComponentModel.Container
    Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(frmStressTest))
    Me.lbl_Titulo = New System.Windows.Forms.Label
    Me.T1_Sinal = New System.Windows.Forms.Panel
    Me.lbl_T1 = New System.Windows.Forms.Label
    Me.lbl_T1_Conflito = New System.Windows.Forms.Label
    Me.StatusBar1 = New System.Windows.Forms.StatusBar
    Me.txt_T1 = New System.Windows.Forms.TextBox
    Me.Panel1 = New System.Windows.Forms.Panel
    Me.Panel2 = New System.Windows.Forms.Panel
    Me.txt_T2 = New System.Windows.Forms.TextBox
    Me.lbl_T2_Conflito = New System.Windows.Forms.Label
    Me.lbl_T2 = New System.Windows.Forms.Label
    Me.T2_Sinal = New System.Windows.Forms.Panel
    Me.Panel3 = New System.Windows.Forms.Panel
    Me.bt_T1 = New System.Windows.Forms.Button
    Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
    Me.bt_T2 = New System.Windows.Forms.Button
    Me.ProgressBar1 = New System.Windows.Forms.ProgressBar
    Me.SuspendLayout()
    '
    'lbl_Titulo
    '
    Me.lbl_Titulo.Location = New System.Drawing.Point(16, 8)
    Me.lbl_Titulo.Name = "lbl_Titulo"
    Me.lbl_Titulo.Size = New System.Drawing.Size(408, 16)
    Me.lbl_Titulo.TabIndex = 0
    Me.lbl_Titulo.Text = "Teste de Estress da Aplicação:   "
    '
    'T1_Sinal
    '
    Me.T1_Sinal.BackColor = System.Drawing.Color.Lime
    Me.T1_Sinal.Location = New System.Drawing.Point(24, 88)
    Me.T1_Sinal.Name = "T1_Sinal"
    Me.T1_Sinal.Size = New System.Drawing.Size(24, 24)
    Me.T1_Sinal.TabIndex = 1
    '
    'lbl_T1
    '
    Me.lbl_T1.Location = New System.Drawing.Point(56, 72)
    Me.lbl_T1.Name = "lbl_T1"
    Me.lbl_T1.Size = New System.Drawing.Size(176, 16)
    Me.lbl_T1.TabIndex = 2
    Me.lbl_T1.Text = "Teste 1"
    '
    'lbl_T1_Conflito
    '
    Me.lbl_T1_Conflito.Location = New System.Drawing.Point(56, 88)
    Me.lbl_T1_Conflito.Name = "lbl_T1_Conflito"
    Me.lbl_T1_Conflito.Size = New System.Drawing.Size(176, 24)
    Me.lbl_T1_Conflito.TabIndex = 3
    Me.lbl_T1_Conflito.Text = "Sem conflito"
    '
    'StatusBar1
    '
    Me.StatusBar1.Location = New System.Drawing.Point(0, 423)
    Me.StatusBar1.Name = "StatusBar1"
    Me.StatusBar1.Size = New System.Drawing.Size(528, 22)
    Me.StatusBar1.TabIndex = 4
    Me.StatusBar1.Text = "Em conformidade..."
    '
    'txt_T1
    '
    Me.txt_T1.Location = New System.Drawing.Point(24, 120)
    Me.txt_T1.Multiline = True
    Me.txt_T1.Name = "txt_T1"
    Me.txt_T1.Size = New System.Drawing.Size(232, 96)
    Me.txt_T1.TabIndex = 5
    Me.txt_T1.Text = "Iniciando Testes T1..."
    '
    'Panel1
    '
    Me.Panel1.BackColor = System.Drawing.Color.Black
    Me.Panel1.Location = New System.Drawing.Point(-8, 240)
    Me.Panel1.Name = "Panel1"
    Me.Panel1.Size = New System.Drawing.Size(552, 4)
    Me.Panel1.TabIndex = 6
    '
    'Panel2
    '
    Me.Panel2.BackColor = System.Drawing.Color.Black
    Me.Panel2.Location = New System.Drawing.Point(264, 48)
    Me.Panel2.Name = "Panel2"
    Me.Panel2.Size = New System.Drawing.Size(4, 400)
    Me.Panel2.TabIndex = 7
    '
    'txt_T2
    '
    Me.txt_T2.Location = New System.Drawing.Point(280, 120)
    Me.txt_T2.Multiline = True
    Me.txt_T2.Name = "txt_T2"
    Me.txt_T2.Size = New System.Drawing.Size(232, 112)
    Me.txt_T2.TabIndex = 11
    Me.txt_T2.Text = "Iniciando Testes T2..."
    '
    'lbl_T2_Conflito
    '
    Me.lbl_T2_Conflito.Location = New System.Drawing.Point(312, 88)
    Me.lbl_T2_Conflito.Name = "lbl_T2_Conflito"
    Me.lbl_T2_Conflito.Size = New System.Drawing.Size(176, 24)
    Me.lbl_T2_Conflito.TabIndex = 10
    Me.lbl_T2_Conflito.Text = "Sem conflito"
    '
    'lbl_T2
    '
    Me.lbl_T2.Location = New System.Drawing.Point(312, 72)
    Me.lbl_T2.Name = "lbl_T2"
    Me.lbl_T2.Size = New System.Drawing.Size(176, 16)
    Me.lbl_T2.TabIndex = 9
    Me.lbl_T2.Text = "Teste 1"
    '
    'T2_Sinal
    '
    Me.T2_Sinal.BackColor = System.Drawing.Color.Lime
    Me.T2_Sinal.Location = New System.Drawing.Point(280, 88)
    Me.T2_Sinal.Name = "T2_Sinal"
    Me.T2_Sinal.Size = New System.Drawing.Size(24, 24)
    Me.T2_Sinal.TabIndex = 8
    '
    'Panel3
    '
    Me.Panel3.BackColor = System.Drawing.Color.Black
    Me.Panel3.Location = New System.Drawing.Point(-8, 48)
    Me.Panel3.Name = "Panel3"
    Me.Panel3.Size = New System.Drawing.Size(552, 4)
    Me.Panel3.TabIndex = 12
    '
    'bt_T1
    '
    Me.bt_T1.ImageIndex = 0
    Me.bt_T1.ImageList = Me.ImageList1
    Me.bt_T1.Location = New System.Drawing.Point(24, 64)
    Me.bt_T1.Name = "bt_T1"
    Me.bt_T1.Size = New System.Drawing.Size(24, 24)
    Me.bt_T1.TabIndex = 13
    '
    'ImageList1
    '
    Me.ImageList1.ImageSize = New System.Drawing.Size(8, 9)
    Me.ImageList1.ImageStream = CType(resources.GetObject("ImageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
    Me.ImageList1.TransparentColor = System.Drawing.Color.Transparent
    '
    'bt_T2
    '
    Me.bt_T2.ImageIndex = 0
    Me.bt_T2.ImageList = Me.ImageList1
    Me.bt_T2.Location = New System.Drawing.Point(280, 64)
    Me.bt_T2.Name = "bt_T2"
    Me.bt_T2.Size = New System.Drawing.Size(24, 24)
    Me.bt_T2.TabIndex = 14
    '
    'ProgressBar1
    '
    Me.ProgressBar1.Location = New System.Drawing.Point(32, 224)
    Me.ProgressBar1.Name = "ProgressBar1"
    Me.ProgressBar1.Size = New System.Drawing.Size(192, 8)
    Me.ProgressBar1.TabIndex = 15
    '
    'frmStressTest
    '
    Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
    Me.ClientSize = New System.Drawing.Size(528, 445)
    Me.Controls.Add(Me.ProgressBar1)
    Me.Controls.Add(Me.bt_T2)
    Me.Controls.Add(Me.bt_T1)
    Me.Controls.Add(Me.Panel3)
    Me.Controls.Add(Me.txt_T2)
    Me.Controls.Add(Me.lbl_T2_Conflito)
    Me.Controls.Add(Me.lbl_T2)
    Me.Controls.Add(Me.T2_Sinal)
    Me.Controls.Add(Me.Panel1)
    Me.Controls.Add(Me.txt_T1)
    Me.Controls.Add(Me.StatusBar1)
    Me.Controls.Add(Me.lbl_T1_Conflito)
    Me.Controls.Add(Me.lbl_T1)
    Me.Controls.Add(Me.T1_Sinal)
    Me.Controls.Add(Me.lbl_Titulo)
    Me.Controls.Add(Me.Panel2)
    Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
    Me.Name = "frmStressTest"
    Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
    Me.Text = "frmStressTest"
    Me.ResumeLayout(False)

  End Sub

#End Region

  Private Sub bt_T1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bt_T1.Click
    If Me.bt_T1.ImageIndex = 0 Then
      Me.bt_T1.ImageIndex = 1
      tr1 = New Op
      tr1.Vai()
    Else
      Me.bt_T1.ImageIndex = 0
      tr1.MataTudo()
      tr1 = Nothing
    End If
  End Sub

  Private Sub frmStressTest_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Me.lbl_Titulo.Text &= CStr(AppDomain.GetCurrentThreadId())
  End Sub
End Class

Public Class Op
  Dim Th As Threading.Thread
  Public Event Tarefa()


  Public Sub mVai()
    Dim buff As String
    Dim I As Long
    'buff = AbrirArquivo("c:\T.0399.0362.20031029.e.000001.txt")
    'buff = AbrirArquivo("\\stone\c\Documento recuperado.txt")
    For I = 1 To 5000
      buff = buff & "." & I
    Next
    MsgBox("Foi... ")
  End Sub

  Public Function AbrirArquivo(ByVal FullPath As String) As String
    Dim mFileNum As Integer
    Dim mFileLen As Long
    Dim mBuffer As String

    mFileNum = FileSystem.FreeFile
    mFileLen = FileSystem.FileLen(FullPath)
    FileSystem.FileOpen(mFileNum, FullPath, OpenMode.Input, OpenAccess.Default, OpenShare.Shared)
    mBuffer = FileSystem.InputString(mFileNum, mFileLen - 2)
    Return mBuffer
    FileSystem.FileClose(mFileNum)
  End Function

  Protected Overrides Sub Finalize()
    MyBase.Finalize()
  End Sub

  Public Sub Vai()
    Th = New Threading.Thread(AddressOf mVai)
    Th.Start()
    Beep()
  End Sub

  Public Sub MataTudo()
    If Th.ThreadState = Threading.ThreadState.Running Then
      Th.Abort()
    End If
    Th = Nothing
  End Sub


End Class
