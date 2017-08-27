Public Class subConfig_semUso
    Inherits System.Windows.Forms.Form

    Private mParent As Boolean

#Region " Windows Form Designer generated code "

    Public Sub New(ByVal key As String, ByVal value As String)
        Me.New(key, value, 0, 0)
    End Sub

    Friend Sub New(ByVal key As String, ByVal value As String, ByVal x As Integer, ByVal y As Integer)
        MyBase.New()
        'This call is required by the Windows Form Designer.
        InitializeComponent()
        'Add any initialization after the InitializeComponent() call
        Me.Left = x + 20
        Me.Top = y + 20
        Me.txt_key.Text = key
        Me.txt_valueKey.Text = value
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
    Friend WithEvents bt_ok As System.Windows.Forms.Button
    Friend WithEvents bt_cancelar As System.Windows.Forms.Button
    Friend WithEvents lbl_key As System.Windows.Forms.Label
    Friend WithEvents txt_key As System.Windows.Forms.TextBox
    Friend WithEvents txt_valueKey As System.Windows.Forms.TextBox
    Friend WithEvents lbl_value As System.Windows.Forms.Label
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.lbl_key = New System.Windows.Forms.Label
        Me.txt_key = New System.Windows.Forms.TextBox
        Me.txt_valueKey = New System.Windows.Forms.TextBox
        Me.lbl_value = New System.Windows.Forms.Label
        Me.bt_ok = New System.Windows.Forms.Button
        Me.bt_cancelar = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'lbl_key
        '
        Me.lbl_key.Location = New System.Drawing.Point(16, 8)
        Me.lbl_key.Name = "lbl_key"
        Me.lbl_key.Size = New System.Drawing.Size(208, 20)
        Me.lbl_key.TabIndex = 0
        Me.lbl_key.Text = "Nome da chave:"
        '
        'txt_key
        '
        Me.txt_key.Location = New System.Drawing.Point(16, 24)
        Me.txt_key.Name = "txt_key"
        Me.txt_key.ReadOnly = True
        Me.txt_key.Size = New System.Drawing.Size(408, 20)
        Me.txt_key.TabIndex = 1
        Me.txt_key.Text = "chave"
        '
        'txt_valueKey
        '
        Me.txt_valueKey.Location = New System.Drawing.Point(16, 72)
        Me.txt_valueKey.Name = "txt_valueKey"
        Me.txt_valueKey.Size = New System.Drawing.Size(408, 20)
        Me.txt_valueKey.TabIndex = 3
        Me.txt_valueKey.Text = "valor"
        '
        'lbl_value
        '
        Me.lbl_value.Location = New System.Drawing.Point(16, 56)
        Me.lbl_value.Name = "lbl_value"
        Me.lbl_value.Size = New System.Drawing.Size(208, 20)
        Me.lbl_value.TabIndex = 2
        Me.lbl_value.Text = "Valor da chave:"
        '
        'bt_ok
        '
        Me.bt_ok.Location = New System.Drawing.Point(288, 104)
        Me.bt_ok.Name = "bt_ok"
        Me.bt_ok.Size = New System.Drawing.Size(65, 20)
        Me.bt_ok.TabIndex = 5
        Me.bt_ok.Text = "&ok"
        '
        'bt_cancelar
        '
        Me.bt_cancelar.Location = New System.Drawing.Point(360, 104)
        Me.bt_cancelar.Name = "bt_cancelar"
        Me.bt_cancelar.Size = New System.Drawing.Size(65, 20)
        Me.bt_cancelar.TabIndex = 4
        Me.bt_cancelar.Text = "&Cancelar"
        '
        'subConfig
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(440, 133)
        Me.ControlBox = False
        Me.Controls.AddRange(New System.Windows.Forms.Control() {Me.bt_cancelar, Me.bt_ok, Me.txt_valueKey, Me.lbl_value, Me.txt_key, Me.lbl_key})
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Name = "subConfig"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Editar Chave"
        Me.ResumeLayout(False)

    End Sub

#End Region

    ReadOnly Property getValue() As String
        Get
            getValue = Me.txt_valueKey.Text
        End Get
    End Property

    ReadOnly Property getChange() As Boolean
        Get
            getChange = mParent
        End Get
    End Property

    Private Sub bt_cancelar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bt_cancelar.Click
        Me.mParent = False
        Me.Close()
    End Sub

    Private Sub bt_ok_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bt_ok.Click
        Me.Close()
    End Sub

    Private Sub txt_valueKey_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txt_valueKey.TextChanged
        Me.mParent = True
    End Sub
End Class
