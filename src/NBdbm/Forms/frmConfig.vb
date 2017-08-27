Imports System.Data
Imports System.Windows.Forms

Namespace forms
    Public Class frmConfig
        Inherits System.Windows.Forms.Form

        Private doDesigner As Boolean

#Region " Windows Form Designer generated code "

        Public Sub New()
            MyBase.New()
            Me.doDesigner = True

            InitializeComponent()
            Dim ainfo As New AssemblyInfo

            Me.mstrCFGFile = ainfo.Config_Arquivo
            Me.Text = "Configurações: " & ainfo.Product
            Me.txt_SeekKey.Text = "Chave a procurar"

            Me.doDesigner = False

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

        Friend WithEvents bt_Fechar As System.Windows.Forms.Button
        Friend WithEvents lbl_grid As System.Windows.Forms.Label
        Friend WithEvents bt_Procurar As System.Windows.Forms.Button
        Friend WithEvents lbl_seek As System.Windows.Forms.Label
        Friend WithEvents txt_SeekKey As System.Windows.Forms.TextBox
        Friend WithEvents lvSettings As System.Windows.Forms.ListView
        Friend WithEvents chKey As System.Windows.Forms.ColumnHeader
        Friend WithEvents chValue As System.Windows.Forms.ColumnHeader
        Friend WithEvents cMenu As System.Windows.Forms.ContextMenu
        Friend WithEvents MenuItem1 As System.Windows.Forms.MenuItem
        Friend WithEvents cmb_Block As System.Windows.Forms.ComboBox
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
            Me.bt_Fechar = New System.Windows.Forms.Button
            Me.lbl_grid = New System.Windows.Forms.Label
            Me.bt_Procurar = New System.Windows.Forms.Button
            Me.lbl_seek = New System.Windows.Forms.Label
            Me.txt_SeekKey = New System.Windows.Forms.TextBox
            Me.lvSettings = New System.Windows.Forms.ListView
            Me.chKey = New System.Windows.Forms.ColumnHeader
            Me.chValue = New System.Windows.Forms.ColumnHeader
            Me.cMenu = New System.Windows.Forms.ContextMenu
            Me.MenuItem1 = New System.Windows.Forms.MenuItem
            Me.cmb_Block = New System.Windows.Forms.ComboBox
            Me.SuspendLayout()
            '
            'bt_Fechar
            '
            Me.bt_Fechar.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.bt_Fechar.Location = New System.Drawing.Point(308, 265)
            Me.bt_Fechar.Name = "bt_Fechar"
            Me.bt_Fechar.Size = New System.Drawing.Size(90, 20)
            Me.bt_Fechar.TabIndex = 5
            Me.bt_Fechar.Text = "&Fechar"
            '
            'lbl_grid
            '
            Me.lbl_grid.Location = New System.Drawing.Point(16, 88)
            Me.lbl_grid.Name = "lbl_grid"
            Me.lbl_grid.TabIndex = 3
            Me.lbl_grid.Text = "&Informações:"
            '
            'bt_Procurar
            '
            Me.bt_Procurar.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.bt_Procurar.Location = New System.Drawing.Point(308, 24)
            Me.bt_Procurar.Name = "bt_Procurar"
            Me.bt_Procurar.Size = New System.Drawing.Size(90, 20)
            Me.bt_Procurar.TabIndex = 2
            Me.bt_Procurar.Text = "&Procurar"
            '
            'lbl_seek
            '
            Me.lbl_seek.AutoSize = True
            Me.lbl_seek.Location = New System.Drawing.Point(16, 8)
            Me.lbl_seek.Name = "lbl_seek"
            Me.lbl_seek.Size = New System.Drawing.Size(108, 16)
            Me.lbl_seek.TabIndex = 0
            Me.lbl_seek.Text = "Procurar pela &chave:"
            '
            'txt_SeekKey
            '
            Me.txt_SeekKey.Location = New System.Drawing.Point(17, 24)
            Me.txt_SeekKey.Name = "txt_SeekKey"
            Me.txt_SeekKey.Size = New System.Drawing.Size(285, 20)
            Me.txt_SeekKey.TabIndex = 1
            Me.txt_SeekKey.Text = ""
            '
            'lvSettings
            '
            Me.lvSettings.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                        Or System.Windows.Forms.AnchorStyles.Left) _
                        Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.lvSettings.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.chKey, Me.chValue})
            Me.lvSettings.FullRowSelect = True
            Me.lvSettings.Location = New System.Drawing.Point(16, 104)
            Me.lvSettings.MultiSelect = False
            Me.lvSettings.Name = "lvSettings"
            Me.lvSettings.Size = New System.Drawing.Size(382, 152)
            Me.lvSettings.Sorting = System.Windows.Forms.SortOrder.Ascending
            Me.lvSettings.TabIndex = 4
            Me.lvSettings.View = System.Windows.Forms.View.Details
            '
            'chKey
            '
            Me.chKey.Text = "Key"
            Me.chKey.Width = 150
            '
            'chValue
            '
            Me.chValue.Text = "Value"
            Me.chValue.Width = 227
            '
            'cMenu
            '
            Me.cMenu.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItem1})
            '
            'MenuItem1
            '
            Me.MenuItem1.Index = 0
            Me.MenuItem1.Text = "Editar chave"
            '
            'cmb_Block
            '
            Me.cmb_Block.Location = New System.Drawing.Point(16, 56)
            Me.cmb_Block.Name = "cmb_Block"
            Me.cmb_Block.Size = New System.Drawing.Size(288, 21)
            Me.cmb_Block.TabIndex = 6
            Me.cmb_Block.Text = "configuration//appSettings"
            '
            'frmConfig
            '
            Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
            Me.ClientSize = New System.Drawing.Size(408, 296)
            Me.Controls.Add(Me.cmb_Block)
            Me.Controls.Add(Me.txt_SeekKey)
            Me.Controls.Add(Me.lvSettings)
            Me.Controls.Add(Me.bt_Procurar)
            Me.Controls.Add(Me.lbl_grid)
            Me.Controls.Add(Me.bt_Fechar)
            Me.Controls.Add(Me.lbl_seek)
            Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow
            Me.MinimumSize = New System.Drawing.Size(410, 260)
            Me.Name = "frmConfig"
            Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.ResumeLayout(False)

        End Sub

#End Region

#Region "  Start  "

        ' Sortes the name of our configuration file.
        Private mstrCFGFile As String
        ' From the System.Collections Namespace.
        ' Note the version returned by 
        ' ConfigurationSettings.AppSettings is
        ' read-only even though instances of
        ' Specialized.NameValueCollection can be 
        ' read-write.
        Dim mAppSet As Specialized.NameValueCollection
        ' Custom class to work with app settings
        Private mcustAppSettings As AppSettings
        ' Individual setting
        Private mcustAppSet As AppSetting

        Private mKey As String
        Private mvalueKey As String
        Private mChangeKey As Boolean
        Private loadConfig As Boolean
        Private aSelf As self


#End Region

#Region " Form Code "

        Private Sub frmMain_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
            aSelf = New self
            Call Me.cmdLoadFormConfig()
        End Sub

        ReadOnly Property selectKey() As String
            Get
                selectKey = mKey
            End Get
        End Property

        Property valueKey() As String
            Get
                valueKey = mvalueKey
            End Get
            Set(ByVal valueKey As String)
                mvalueKey = valueKey
                If Me.mChangeKey = True Then
                    'implementar a alteração
                End If
            End Set
        End Property

        Private Sub bt_Procurar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bt_Procurar.Click
            Dim i As Integer
            Dim localizado As Boolean
            'Me.lvSettings.Scrollable = True
            localizado = False
            For i = Me.lvSettings.Items.Count To 1 Step -1
                If UCase(Me.lvSettings.Items(i - 1).SubItems(0).Text) = UCase(Me.txt_SeekKey.Text) Then
                    localizado = True
                    Exit For
                End If
            Next
            'mcustAppSettings.Item(Me.txt_SeekKey)
            If localizado = True Then
                Me.lvSettings.Items(i - 1).Selected = True
                Me.lvSettings.SelectedItems(0).Selected = True
                Me.lvSettings.Focus()
                Me.lvSettings.EnsureVisible(i - 1)
            Else
                MsgBox("Não há uma chave com esse nome!", MsgBoxStyle.Critical)
            End If
        End Sub

        Private Sub cmdUpdate(ByVal Key As String, ByVal value As String)
            ' Atualiza caso exista o item
            Try
                If Key = String.Empty Then
                    MessageBox.Show("Você precisa informar uma chave 'key'", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
                If value = String.Empty Then
                    MessageBox.Show("Você precisa informar um valor para chave 'key'.", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If

                mcustAppSet = New AppSetting(Key, value)
                mcustAppSettings.Update(Me.cmb_Block.Text, mcustAppSet)

                MessageBox.Show("Arquivo 'setting' atualizado.", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)

            Catch exp As Exception
                MessageBox.Show(exp.Message, exp.Source, MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try

        End Sub

        Private Sub bt_Fechar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bt_Fechar.Click
            Me.Close()
        End Sub

        Private Sub lvSettings_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lvSettings.SelectedIndexChanged
            If Me.lvSettings.SelectedItems.Count = 1 Then
                Me.txt_SeekKey.Text = Me.lvSettings.SelectedItems(0).SubItems(0).Text
                'Me.mKey = Me.lvSettings.SelectedItems(0).SubItems(0).Text
                'Me.valueKey = Me.lvSettings.SelectedItems(0).SubItems(1).Text
            End If
        End Sub

        Private Sub lvSettings_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs)
            Call cmdLoadSubConfig(Me.selectKey, Me.valueKey)
        End Sub

        Private Sub cmdLoadFormConfig()
            Try

                'adicionando os Block's
                Me.cmb_Block.Items.Add("configuration//appSettings")
                Me.cmb_Block.Items.Add("configuration//SQLSERVER")
                Me.cmb_Block.Items.Add("configuration//ODBC")
                Me.cmb_Block.Items.Add("configuration//OUTDB")
                Me.cmb_Block.Items.Add("configuration//PostGreSQL")


                Call Me.loadInfo()

            Catch exp As Exception
                MessageBox.Show(exp.Message, exp.Source, MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

        Private Sub lvSettings_DoubleClick1(ByVal sender As Object, ByVal e As System.EventArgs) Handles lvSettings.DoubleClick
            Me.mKey = Me.lvSettings.SelectedItems(0).Text
            Me.mvalueKey = Me.lvSettings.SelectedItems(0).SubItems(1).Text
            Call cmdLoadSubConfig(Me.mKey, Me.mvalueKey)
            aSelf.Settings.Dispose()
            Call Me.loadInfo()

        End Sub

        Private Sub cmdLoadSubConfig(ByVal key As String, ByVal value As String)
            Dim sb As New subConfig(key, value, Me.Left, Me.Top)
            sb.ShowDialog()
            If sb.getChange = True Then
                Me.valueKey = sb.getValue
                Me.cmdUpdate(Me.mKey, Me.valueKey)
            End If
            sb.Dispose()
            sb = Nothing
        End Sub

        Private Sub lvSettings_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles lvSettings.MouseUp

            Dim p As System.Drawing.Point
            p.X = e.X
            p.Y = e.Y
            If e.Button = MouseButtons.Right Then
                Me.cMenu.Show(Me.lvSettings, p)
            End If

        End Sub

        Private Sub MenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItem1.Click
            Me.mKey = Me.lvSettings.SelectedItems(0).Text
            Me.mvalueKey = Me.lvSettings.SelectedItems(0).SubItems(1).Text
            Call cmdLoadSubConfig(Me.mKey, Me.mvalueKey)
        End Sub

        Private Sub cmb_Block_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmb_Block.TextChanged
            If doDesigner = False Then
                Call loadInfo()
            End If
        End Sub

        Private Sub cmb_Block_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmb_Block.SelectedIndexChanged
            'Call loadInfo()
        End Sub

        Private Sub loadInfo()
            If Not mcustAppSettings Is Nothing Then
                mcustAppSettings.Dispose()
                mcustAppSettings = Nothing
            End If
            mcustAppSettings = New AppSettings(Me.mstrCFGFile, False, Me.cmb_Block.Text)


            If Me.lvSettings.Items.Count > 0 Then
                Me.lvSettings.Items.Clear()
            End If

            For Each mcustAppSet In mcustAppSettings
                With Me.lvSettings.Items.Add(mcustAppSet.Key)
                    .SubItems.Add(mcustAppSet.Value)
                End With
            Next
        End Sub

#End Region

#Region " Sub Class Form "

        Private Class subConfig
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
                Me.ClientSize = New System.Drawing.Size(440, 150)
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
        End Class 'frmSubConfig

#End Region

    End Class 'frmConfig
End Namespace 'Forms