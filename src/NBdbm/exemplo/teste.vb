Public Class teste
    Inherits System.Windows.Forms.Form

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
  Friend WithEvents Button1 As System.Windows.Forms.Button
  Friend WithEvents Label1 As System.Windows.Forms.Label
  Friend WithEvents ComboBox1 As System.Windows.Forms.ComboBox
  Friend WithEvents lblEmpresa As System.Windows.Forms.Label
  <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
    Me.Button1 = New System.Windows.Forms.Button
    Me.Label1 = New System.Windows.Forms.Label
    Me.ComboBox1 = New System.Windows.Forms.ComboBox
    Me.lblEmpresa = New System.Windows.Forms.Label
    Me.SuspendLayout()
    '
    'Button1
    '
    Me.Button1.Location = New System.Drawing.Point(104, 104)
    Me.Button1.Name = "Button1"
    Me.Button1.TabIndex = 0
    Me.Button1.Text = "Abrir Banco"
    '
    'Label1
    '
    Me.Label1.Location = New System.Drawing.Point(56, 8)
    Me.Label1.Name = "Label1"
    Me.Label1.Size = New System.Drawing.Size(176, 16)
    Me.Label1.TabIndex = 2
    Me.Label1.Text = "Escolha o Banco"
    '
    'ComboBox1
    '
    Me.ComboBox1.Items.AddRange(New Object() {"Neobridge", "LugPhil"})
    Me.ComboBox1.Location = New System.Drawing.Point(56, 24)
    Me.ComboBox1.Name = "ComboBox1"
    Me.ComboBox1.Size = New System.Drawing.Size(208, 21)
    Me.ComboBox1.TabIndex = 3
    '
    'lblEmpresa
    '
    Me.lblEmpresa.Location = New System.Drawing.Point(56, 56)
    Me.lblEmpresa.Name = "lblEmpresa"
    Me.lblEmpresa.Size = New System.Drawing.Size(208, 16)
    Me.lblEmpresa.TabIndex = 5
    Me.lblEmpresa.Text = "Nome da Empresa:"
    '
    'teste
    '
    Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
    Me.ClientSize = New System.Drawing.Size(292, 273)
    Me.Controls.Add(Me.lblEmpresa)
    Me.Controls.Add(Me.ComboBox1)
    Me.Controls.Add(Me.Label1)
    Me.Controls.Add(Me.Button1)
    Me.Name = "teste"
    Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
    Me.Text = "teste"
    Me.ResumeLayout(False)

  End Sub

#End Region

  Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
    Dim obj As NBdbm.Fachadas.allClass
    Dim Dados As System.Data.DataView
    Select Case (ComboBox1.SelectedItem())
      Case "Neobridge"
        obj = New NBdbm.Fachadas.allClass("CTRL_Entidades", NBdbm.tipos.tiposConection.SQLSERVER)
        obj.filterWhere = "idEntidade = 911"
      Case "LugPhil"
        obj = New NBdbm.Fachadas.allClass("CTRL_Entidades", NBdbm.tipos.tiposConection.SQLSVR_LUG)
        obj.filterWhere = "idEntidade = 533"
    End Select
    Dados = obj.DataSource()
    lblEmpresa.Text = Dados.Table.Rows(0)("NomePrimary")
    Dados.Dispose()
    obj.Dispose()
  End Sub
End Class
