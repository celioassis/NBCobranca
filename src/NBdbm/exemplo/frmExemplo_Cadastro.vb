Public Class frmExemplo_Cadastro
  Inherits System.Windows.Forms.Form

#Region "  Windows Form Designer generated code  "

  'Variáveis do Form
  Dim admTB As NBdbm.Fachadas.CTR.primitivas.Entidade

  Dim doDesigner As Boolean

#Region "  Start  "

  Public Sub New()
    MyBase.New()

    'This call is required by the Windows Form Designer.
    doDesigner = True
    InitializeComponent()
    doDesigner = False

    'Add any initialization after the InitializeComponent() call
    admTB = New NBdbm.Fachadas.CTR.primitivas.Entidade
    'admTB = New NBdbm.Fachadas.CTR.CadastroPessoaFisica
  End Sub

  'Form overrides dispose to clean up the component list.
  Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
    If disposing Then
      If Not admTB Is Nothing Then
        admTB.Dispose() 'verificar dupla passagem
        admTB = Nothing
      End If
      If Not (components Is Nothing) Then
        components.Dispose()
      End If
    End If
    MyBase.Dispose(disposing)
  End Sub

  Protected Overrides Sub Finalize()
    MyBase.Finalize()
    Me.Dispose(True)
  End Sub
#End Region

#Region "  Form Contructor  "

  'Required by the Windows Form Designer
  Private components As System.ComponentModel.IContainer

  'NOTE: The following procedure is required by the Windows Form Designer
  'It can be modified using the Windows Form Designer.  
  'Do not modify it using the code editor.
  Friend WithEvents lbl_frm As System.Windows.Forms.Label
  Friend WithEvents lbl_seek As System.Windows.Forms.Label
  Friend WithEvents txt_seek As System.Windows.Forms.TextBox
  Friend WithEvents lbl_fieldSeek As System.Windows.Forms.Label
  Friend WithEvents DG As System.Windows.Forms.DataGrid
  Friend WithEvents bt_fechar As System.Windows.Forms.Button
  Friend WithEvents bt_Consultar As System.Windows.Forms.Button
  Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
  Friend WithEvents bt_Opcoes As System.Windows.Forms.Button
  Friend WithEvents txt_where As System.Windows.Forms.TextBox
  Friend WithEvents lbl_Where As System.Windows.Forms.Label
  Friend WithEvents lbl_Having As System.Windows.Forms.Label
  Friend WithEvents txt_having As System.Windows.Forms.TextBox
  Friend WithEvents lbl_GroupBy As System.Windows.Forms.Label
  Friend WithEvents lbl_OrderBy As System.Windows.Forms.Label
  Friend WithEvents cmb_fieldSeek As System.Windows.Forms.ComboBox
  Friend WithEvents lbl_Count As System.Windows.Forms.Label
  Friend WithEvents txt_count As System.Windows.Forms.Label
  Friend WithEvents txt_GroupBy As System.Windows.Forms.TextBox
  Friend WithEvents txt_OrderBy As System.Windows.Forms.TextBox
  Friend WithEvents bt_Editar As System.Windows.Forms.Button
  Friend WithEvents bt_Excluir As System.Windows.Forms.Button
  Friend WithEvents bt_Incluir As System.Windows.Forms.Button
  Friend WithEvents toolTip As System.Windows.Forms.ToolTip
  Friend WithEvents bt_consultarCampos As System.Windows.Forms.Button
  Friend WithEvents bt_teste As System.Windows.Forms.Button
  Friend WithEvents bt_testeAdd As System.Windows.Forms.Button
  <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
    Me.components = New System.ComponentModel.Container
    Me.lbl_frm = New System.Windows.Forms.Label
    Me.DG = New System.Windows.Forms.DataGrid
    Me.lbl_seek = New System.Windows.Forms.Label
    Me.txt_seek = New System.Windows.Forms.TextBox
    Me.lbl_fieldSeek = New System.Windows.Forms.Label
    Me.cmb_fieldSeek = New System.Windows.Forms.ComboBox
    Me.bt_fechar = New System.Windows.Forms.Button
    Me.bt_Consultar = New System.Windows.Forms.Button
    Me.GroupBox1 = New System.Windows.Forms.GroupBox
    Me.txt_where = New System.Windows.Forms.TextBox
    Me.txt_having = New System.Windows.Forms.TextBox
    Me.txt_GroupBy = New System.Windows.Forms.TextBox
    Me.txt_OrderBy = New System.Windows.Forms.TextBox
    Me.lbl_GroupBy = New System.Windows.Forms.Label
    Me.lbl_Having = New System.Windows.Forms.Label
    Me.lbl_Where = New System.Windows.Forms.Label
    Me.lbl_OrderBy = New System.Windows.Forms.Label
    Me.bt_Opcoes = New System.Windows.Forms.Button
    Me.lbl_Count = New System.Windows.Forms.Label
    Me.txt_count = New System.Windows.Forms.Label
    Me.bt_Editar = New System.Windows.Forms.Button
    Me.bt_Excluir = New System.Windows.Forms.Button
    Me.bt_Incluir = New System.Windows.Forms.Button
    Me.toolTip = New System.Windows.Forms.ToolTip(Me.components)
    Me.bt_consultarCampos = New System.Windows.Forms.Button
    Me.bt_teste = New System.Windows.Forms.Button
    Me.bt_testeAdd = New System.Windows.Forms.Button
    CType(Me.DG, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.GroupBox1.SuspendLayout()
    Me.SuspendLayout()
    '
    'lbl_frm
    '
    Me.lbl_frm.AutoSize = True
    Me.lbl_frm.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.lbl_frm.Location = New System.Drawing.Point(11, 8)
    Me.lbl_frm.Name = "lbl_frm"
    Me.lbl_frm.Size = New System.Drawing.Size(310, 23)
    Me.lbl_frm.TabIndex = 0
    Me.lbl_frm.Text = "Exemplo de Manipulação em Tabelas"
    '
    'DG
    '
    Me.DG.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.DG.CaptionText = "Registros da Tabela:"
    Me.DG.DataMember = ""
    Me.DG.HeaderForeColor = System.Drawing.SystemColors.ControlText
    Me.DG.Location = New System.Drawing.Point(8, 72)
    Me.DG.Name = "DG"
    Me.DG.ReadOnly = True
    Me.DG.Size = New System.Drawing.Size(432, 296)
    Me.DG.TabIndex = 1
    '
    'lbl_seek
    '
    Me.lbl_seek.AutoSize = True
    Me.lbl_seek.Location = New System.Drawing.Point(10, 40)
    Me.lbl_seek.Name = "lbl_seek"
    Me.lbl_seek.Size = New System.Drawing.Size(53, 16)
    Me.lbl_seek.TabIndex = 2
    Me.lbl_seek.Text = "&Localizar:"
    '
    'txt_seek
    '
    Me.txt_seek.Location = New System.Drawing.Point(66, 38)
    Me.txt_seek.Name = "txt_seek"
    Me.txt_seek.Size = New System.Drawing.Size(262, 20)
    Me.txt_seek.TabIndex = 3
    Me.txt_seek.Text = "digite aqui..."
    '
    'lbl_fieldSeek
    '
    Me.lbl_fieldSeek.AutoSize = True
    Me.lbl_fieldSeek.Location = New System.Drawing.Point(338, 22)
    Me.lbl_fieldSeek.Name = "lbl_fieldSeek"
    Me.lbl_fieldSeek.Size = New System.Drawing.Size(102, 16)
    Me.lbl_fieldSeek.TabIndex = 4
    Me.lbl_fieldSeek.Text = "Campo de &Procura:"
    '
    'cmb_fieldSeek
    '
    Me.cmb_fieldSeek.Location = New System.Drawing.Point(340, 37)
    Me.cmb_fieldSeek.Name = "cmb_fieldSeek"
    Me.cmb_fieldSeek.Size = New System.Drawing.Size(100, 21)
    Me.cmb_fieldSeek.TabIndex = 5
    Me.cmb_fieldSeek.Text = "Escolha o campo"
    '
    'bt_fechar
    '
    Me.bt_fechar.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.bt_fechar.Location = New System.Drawing.Point(448, 312)
    Me.bt_fechar.Name = "bt_fechar"
    Me.bt_fechar.Size = New System.Drawing.Size(100, 23)
    Me.bt_fechar.TabIndex = 6
    Me.bt_fechar.Text = "&Fechar"
    Me.toolTip.SetToolTip(Me.bt_fechar, "Clique para fechar esta tela")
    '
    'bt_Consultar
    '
    Me.bt_Consultar.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.bt_Consultar.Location = New System.Drawing.Point(448, 72)
    Me.bt_Consultar.Name = "bt_Consultar"
    Me.bt_Consultar.Size = New System.Drawing.Size(100, 23)
    Me.bt_Consultar.TabIndex = 7
    Me.bt_Consultar.Text = "Con&sultar"
    Me.toolTip.SetToolTip(Me.bt_Consultar, "Clique para realizar a consulta novamente")
    '
    'GroupBox1
    '
    Me.GroupBox1.Controls.Add(Me.txt_where)
    Me.GroupBox1.Controls.Add(Me.txt_having)
    Me.GroupBox1.Controls.Add(Me.txt_GroupBy)
    Me.GroupBox1.Controls.Add(Me.txt_OrderBy)
    Me.GroupBox1.Controls.Add(Me.lbl_GroupBy)
    Me.GroupBox1.Controls.Add(Me.lbl_Having)
    Me.GroupBox1.Controls.Add(Me.lbl_Where)
    Me.GroupBox1.Controls.Add(Me.lbl_OrderBy)
    Me.GroupBox1.Location = New System.Drawing.Point(8, 384)
    Me.GroupBox1.Name = "GroupBox1"
    Me.GroupBox1.Size = New System.Drawing.Size(540, 128)
    Me.GroupBox1.TabIndex = 8
    Me.GroupBox1.TabStop = False
    Me.GroupBox1.Text = "Mais Opções:"
    '
    'txt_where
    '
    Me.txt_where.Location = New System.Drawing.Point(57, 24)
    Me.txt_where.Name = "txt_where"
    Me.txt_where.Size = New System.Drawing.Size(473, 20)
    Me.txt_where.TabIndex = 0
    Me.txt_where.Text = "where arguments..."
    '
    'txt_having
    '
    Me.txt_having.Location = New System.Drawing.Point(57, 49)
    Me.txt_having.Name = "txt_having"
    Me.txt_having.Size = New System.Drawing.Size(473, 20)
    Me.txt_having.TabIndex = 2
    Me.txt_having.Text = "Having arguments..."
    '
    'txt_GroupBy
    '
    Me.txt_GroupBy.Location = New System.Drawing.Point(57, 74)
    Me.txt_GroupBy.Name = "txt_GroupBy"
    Me.txt_GroupBy.Size = New System.Drawing.Size(473, 20)
    Me.txt_GroupBy.TabIndex = 4
    Me.txt_GroupBy.Text = "Group By [arguments...]"
    '
    'txt_OrderBy
    '
    Me.txt_OrderBy.Location = New System.Drawing.Point(57, 101)
    Me.txt_OrderBy.Name = "txt_OrderBy"
    Me.txt_OrderBy.Size = New System.Drawing.Size(473, 20)
    Me.txt_OrderBy.TabIndex = 6
    Me.txt_OrderBy.Text = "Order By [arguments...]"
    '
    'lbl_GroupBy
    '
    Me.lbl_GroupBy.AutoSize = True
    Me.lbl_GroupBy.Location = New System.Drawing.Point(8, 76)
    Me.lbl_GroupBy.Name = "lbl_GroupBy"
    Me.lbl_GroupBy.Size = New System.Drawing.Size(52, 16)
    Me.lbl_GroupBy.TabIndex = 5
    Me.lbl_GroupBy.Text = "&GroupBy:"
    '
    'lbl_Having
    '
    Me.lbl_Having.AutoSize = True
    Me.lbl_Having.Location = New System.Drawing.Point(8, 51)
    Me.lbl_Having.Name = "lbl_Having"
    Me.lbl_Having.Size = New System.Drawing.Size(43, 16)
    Me.lbl_Having.TabIndex = 3
    Me.lbl_Having.Text = "&Having:"
    '
    'lbl_Where
    '
    Me.lbl_Where.AutoSize = True
    Me.lbl_Where.Location = New System.Drawing.Point(8, 26)
    Me.lbl_Where.Name = "lbl_Where"
    Me.lbl_Where.Size = New System.Drawing.Size(41, 16)
    Me.lbl_Where.TabIndex = 1
    Me.lbl_Where.Text = "&Where:"
    '
    'lbl_OrderBy
    '
    Me.lbl_OrderBy.AutoSize = True
    Me.lbl_OrderBy.Location = New System.Drawing.Point(8, 103)
    Me.lbl_OrderBy.Name = "lbl_OrderBy"
    Me.lbl_OrderBy.Size = New System.Drawing.Size(49, 16)
    Me.lbl_OrderBy.TabIndex = 7
    Me.lbl_OrderBy.Text = "&OrderBy:"
    '
    'bt_Opcoes
    '
    Me.bt_Opcoes.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.bt_Opcoes.FlatStyle = System.Windows.Forms.FlatStyle.Flat
    Me.bt_Opcoes.Location = New System.Drawing.Point(448, 344)
    Me.bt_Opcoes.Name = "bt_Opcoes"
    Me.bt_Opcoes.Size = New System.Drawing.Size(100, 23)
    Me.bt_Opcoes.TabIndex = 9
    Me.bt_Opcoes.Tag = "Fechado"
    Me.bt_Opcoes.Text = "Opções >>"
    Me.toolTip.SetToolTip(Me.bt_Opcoes, "Mais opções de consulta")
    '
    'lbl_Count
    '
    Me.lbl_Count.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.lbl_Count.AutoSize = True
    Me.lbl_Count.Location = New System.Drawing.Point(446, 22)
    Me.lbl_Count.Name = "lbl_Count"
    Me.lbl_Count.Size = New System.Drawing.Size(55, 16)
    Me.lbl_Count.TabIndex = 10
    Me.lbl_Count.Text = "Registros:"
    '
    'txt_count
    '
    Me.txt_count.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txt_count.BackColor = System.Drawing.SystemColors.ActiveCaptionText
    Me.txt_count.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.txt_count.Location = New System.Drawing.Point(448, 37)
    Me.txt_count.Name = "txt_count"
    Me.txt_count.Size = New System.Drawing.Size(100, 21)
    Me.txt_count.TabIndex = 11
    Me.txt_count.Text = "0 de 0"
    Me.txt_count.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
    '
    'bt_Editar
    '
    Me.bt_Editar.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.bt_Editar.Location = New System.Drawing.Point(448, 104)
    Me.bt_Editar.Name = "bt_Editar"
    Me.bt_Editar.Size = New System.Drawing.Size(100, 23)
    Me.bt_Editar.TabIndex = 12
    Me.bt_Editar.Text = "&Editar"
    Me.toolTip.SetToolTip(Me.bt_Editar, "Clique para editar o Registro atual")
    '
    'bt_Excluir
    '
    Me.bt_Excluir.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.bt_Excluir.Location = New System.Drawing.Point(448, 136)
    Me.bt_Excluir.Name = "bt_Excluir"
    Me.bt_Excluir.Size = New System.Drawing.Size(100, 23)
    Me.bt_Excluir.TabIndex = 13
    Me.bt_Excluir.Text = "E&xcluir"
    Me.toolTip.SetToolTip(Me.bt_Excluir, "Clique para excluir o Registro atual")
    '
    'bt_Incluir
    '
    Me.bt_Incluir.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.bt_Incluir.Location = New System.Drawing.Point(448, 168)
    Me.bt_Incluir.Name = "bt_Incluir"
    Me.bt_Incluir.Size = New System.Drawing.Size(100, 23)
    Me.bt_Incluir.TabIndex = 14
    Me.bt_Incluir.Text = "I&ncluir"
    Me.toolTip.SetToolTip(Me.bt_Incluir, "Clique para incluir um novo Registro")
    '
    'bt_consultarCampos
    '
    Me.bt_consultarCampos.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.bt_consultarCampos.Location = New System.Drawing.Point(448, 208)
    Me.bt_consultarCampos.Name = "bt_consultarCampos"
    Me.bt_consultarCampos.Size = New System.Drawing.Size(100, 23)
    Me.bt_consultarCampos.TabIndex = 15
    Me.bt_consultarCampos.Text = "Campos"
    Me.toolTip.SetToolTip(Me.bt_consultarCampos, "Leitura dos Campos")
    '
    'bt_teste
    '
    Me.bt_teste.Location = New System.Drawing.Point(448, 240)
    Me.bt_teste.Name = "bt_teste"
    Me.bt_teste.Size = New System.Drawing.Size(96, 24)
    Me.bt_teste.TabIndex = 16
    Me.bt_teste.Text = "ex 1 a 1"
    '
    'bt_testeAdd
    '
    Me.bt_testeAdd.Location = New System.Drawing.Point(448, 272)
    Me.bt_testeAdd.Name = "bt_testeAdd"
    Me.bt_testeAdd.Size = New System.Drawing.Size(96, 24)
    Me.bt_testeAdd.TabIndex = 17
    Me.bt_testeAdd.Text = "ex c/Add"
    '
    'frmExemplo_Cadastro
    '
    Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
    Me.ClientSize = New System.Drawing.Size(556, 385)
    Me.Controls.Add(Me.bt_testeAdd)
    Me.Controls.Add(Me.bt_teste)
    Me.Controls.Add(Me.bt_consultarCampos)
    Me.Controls.Add(Me.bt_Incluir)
    Me.Controls.Add(Me.bt_Excluir)
    Me.Controls.Add(Me.bt_Editar)
    Me.Controls.Add(Me.txt_count)
    Me.Controls.Add(Me.lbl_Count)
    Me.Controls.Add(Me.txt_seek)
    Me.Controls.Add(Me.lbl_seek)
    Me.Controls.Add(Me.lbl_frm)
    Me.Controls.Add(Me.lbl_fieldSeek)
    Me.Controls.Add(Me.bt_Opcoes)
    Me.Controls.Add(Me.GroupBox1)
    Me.Controls.Add(Me.bt_Consultar)
    Me.Controls.Add(Me.bt_fechar)
    Me.Controls.Add(Me.cmb_fieldSeek)
    Me.Controls.Add(Me.DG)
    Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
    Me.MaximumSize = New System.Drawing.Size(1030, 545)
    Me.Name = "frmExemplo_Cadastro"
    Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
    Me.Text = "frmExemplo_Cadastro"
    CType(Me.DG, System.ComponentModel.ISupportInitialize).EndInit()
    Me.GroupBox1.ResumeLayout(False)
    Me.ResumeLayout(False)

  End Sub

#End Region

#End Region

#Region "  Code Base  " 'Codigo privado

  Private Sub DG_CurrentCellChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DG.CurrentCellChanged
    Call bookmark()
  End Sub

  Private Function bookmark()
    If Me.DG.CurrentRowIndex = -1 Then
      Me.txt_count.Text = "0 de 0"
    Else
      Me.txt_count.Text = DG.CurrentCell.RowNumber + 1 & " de " & CStr(admTB.DataSource.Count)
    End If
  End Function

  Private Sub txt_where_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txt_where.TextChanged
    If doDesigner = False Then
      Me.txt_where.Tag = True
      Call Me.txt_filter_update("where")
    End If
  End Sub

  Private Sub txt_having_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txt_having.TextChanged
    If doDesigner = False Then
      Me.txt_having.Tag = True
      Call Me.txt_filter_update("having")
    End If
  End Sub

  Private Sub txt_GroupBy_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txt_GroupBy.TextChanged
    If doDesigner = False Then
      Me.txt_GroupBy.Tag = True
      Call Me.txt_filter_update("groupby")
    End If
  End Sub

  Private Sub txt_OrderBy_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txt_OrderBy.TextChanged
    If doDesigner = False Then
      Me.txt_OrderBy.Tag = True
      Call Me.txt_filter_update("orderby")
    End If
  End Sub

  Private Sub txt_filter_update(ByVal argument As String)

    Select Case argument
      Case "where"
        admTB.filterWhere = Me.txt_where.Text
      Case "having"
        admTB.filterHaving = Me.txt_having.Text
      Case "groupby"
        admTB.filterGroupBy = Me.txt_GroupBy.Text
      Case "orderby"
        admTB.filterOrderBy = Me.txt_OrderBy.Text
      Case "all"
        If Me.txt_where.Tag = True Then admTB.filterWhere = Me.txt_where.Text
        If Me.txt_having.Tag = True Then admTB.filterHaving = Me.txt_having.Text
        If Me.txt_GroupBy.Tag = True Then admTB.filterGroupBy = Me.txt_GroupBy.Text
        If Me.txt_OrderBy.Tag = True Then admTB.filterOrderBy = Me.txt_OrderBy.Text
    End Select

  End Sub

  Private Sub txt_seek_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txt_seek.TextChanged
    Try
      If doDesigner = False And Me.txt_seek.Text <> String.Empty Then
        Dim dv As Data.DataView
        dv = admTB.DataSource
        If dv.Table.Columns.Item(Me.cmb_fieldSeek.Text).DataType.FullName = "System.String" Then
          dv.RowFilter = "[" & Me.cmb_fieldSeek.Text & "] like '" & Me.txt_seek.Text & "'"
        Else
          dv.RowFilter = "[" & Me.cmb_fieldSeek.Text & "] = " & Me.txt_seek.Text
        End If
        dv.Sort = "[" & Me.cmb_fieldSeek.Text & "]" 'DESC"
        Me.DG.DataSource = dv
        Call Me.bookmark()
      End If
    Catch ex As Exception
      'Me.Text = ex.ToString
      Me.Text = "Verifique os dados de localização!"
    End Try
  End Sub


#End Region

#Region "  Botões  "

  Private Sub bt_Opcoes_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bt_Opcoes.Click
    If Me.bt_Opcoes.Tag = "Aberto" Then
      Me.bt_Opcoes.Tag = "Fechado"
      Me.bt_Opcoes.Text = "Opções >>"
      Me.toolTip.SetToolTip(Me.bt_Opcoes, "opções de consulta")
      Me.Height = 410
      Me.Width = 562
    Else
      Me.bt_Opcoes.Tag = "Aberto"
      Me.bt_Opcoes.Text = "Opções <<"
      Me.toolTip.SetToolTip(Me.bt_Opcoes, "clique para fechar opções")
      Me.Height = 545
      Me.Width = 562
    End If
  End Sub

  Private Sub bt_Consultar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bt_Consultar.Click

    DG.DataSource = admTB.DataSource
    Call bookmark()

  End Sub

  Private Sub bt_consultarCampos_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bt_consultarCampos.Click

    DG.DataSource = admTB.datasourceFields

    Dim fields As Data.DataView
    Dim item As Data.DataRowView
    fields = admTB.datasourceFields
    MsgBox(fields.Count)
    For Each item In fields
      Me.cmb_fieldSeek.Items.Add(item(0).ToString)
      'Debug.WriteLine(item(0).ToString)
    Next

  End Sub

  Private Sub bt_Editar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles bt_Editar.Click
    Dim resultado As NBdbm.tipos.Retorno

    Me.admTB.Clear_filters()
    Me.admTB.filterWhere = "idEntidade = 5"

    Call Me.admTB.getFields()

    Me.admTB.Campos.NomeRazaoSocial_Key = Me.admTB.Campos.NomeRazaoSocial_Key & " e 1" '"Edgar 3" 
    Me.admTB.Campos.CPFCNPJ_Key = "80782914934"
    Me.admTB.campos.ApelidoNomeFantasia = "Eddy"


    resultado = Me.admTB.editar(False)

    If resultado.Sucesso = False Then
      Beep()
    Else
      Beep()
      'houve sucesso!
      'for each ...
      ' prenchimento de todos campos através do DataSource que está em Resultado.objeto
      ' Dim DS As DataView
      ' DS = resultado.Objeto
      ' ds.
      'next
    End If

    Me.admTB.Clear_filters()
    Me.txt_filter_update("all")


  End Sub

  Private Sub bt_Excluir_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles bt_Excluir.Click

    Dim resultado As NBdbm.tipos.Retorno
    Me.admTB.Clear_filters()

    Me.admTB.filterWhere = "[idEntidade] = " & DG.Item(DG.CurrentCell).ToString()

    resultado = Me.admTB.excluir(False)
    If resultado.Sucesso = False Then
      Beep()
    Else
      'houve sucesso!
      MsgBox(resultado.Tag)
      MsgBox(resultado.ToString)
      DG.DataSource = admTB.DataSource
      Call bookmark()

    End If

    Me.admTB.Clear_filters()
    Me.txt_filter_update("all")
  End Sub

  Private Sub bt_Incluir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bt_Incluir.Click
    'Call Me.gravaFuncionario()
    Call Me.gravaFuncionario_MetodoEvt()
  End Sub

  Private Sub bt_fechar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bt_fechar.Click
    admTB.Dispose()
    Me.Close()
  End Sub

  Private Sub bt_teste_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bt_teste.Click

    Call Me.gravaFone()

  End Sub

  Private Sub bt_testeAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bt_testeAdd.Click
    Call Me.gravaEntidade_MetodoAdd()
  End Sub

#End Region

  Private Function gravaFuncionario()

    Dim resultado As NBdbm.tipos.Retorno
    Dim ob As New NBdbm.Fachadas.plxEVT.CadastroFuncionario
    Dim oEntidade As New NBdbm.Fachadas.CTR.primitivas.Entidade
    Dim oFone As New NBdbm.Fachadas.CTR.primitivas.Telefone
    Dim oCelular As New NBdbm.Fachadas.CTR.primitivas.Telefone
    Dim oEndereco As New NBdbm.Fachadas.CTR.primitivas.Endereco
    Dim oEMail As New NBdbm.Fachadas.CTR.primitivas.eMail
    Dim oUrl As New NBdbm.Fachadas.CTR.primitivas.Url

    Beep()
    With oEntidade.campos
      .Clear_vars()
      .CPFCNPJ_key = "807.829.149-00"
      .NomeRazaoSocial_key = "Edgar fulano 001"
      .ApelidoNomeFantasia = "Nosso novo Usuario"
      .dtNascimentoInicioAtividades = CDate("08/12/1973")
      .OrgaoEmissorIM = "SSP-SC"
      .RgIE = "2.503.661"
      .TextoRespeito = "esse é o texto a respeito novo"
      ob.Entidade = oEntidade.campos
    End With
    With oFone.Campos
      .Clear_vars()
      .DDD_key = "47"
      .Fone_key = "252-1624"
      .Ramal = "222"
      .Contato = "Com beltrano"
      .Descricao = "Residencial"
      //ob.colecaoTelefones.Add(.Key, oFone.Campos)
    End With
    With oCelular.Campos
      .Clear_vars()
      .DDD_key = "47"
      .Fone_key = "9987-1624"
      .Ramal = "232"
      .Contato = "Cicrano"
      .Descricao = "Celular Pessoal"
      //ob.colecaoTelefones.Add(.Key, oCelular.Campos)
    End With
    With oEndereco.Campos
      .Clear_vars()
      .Logradouro_key = "minha rua, 002"
      .Bairro = "meu bairro"
      .Municipio = "minha cidade"
      .UF = "SC"
      .complemento = "casa dos fundos"
      .Comentario = "endereço da vovozinha"
      .CEP = "88040420"
      .Principal = True
      ob.colecaoEnderecos.Add(.Key, oEndereco.Campos)
    End With
    With oEMail.Campos
      .Clear_vars()
      .Descricao = "Email do chefe"
      .eMail_key = "edgar@neobridge.com.br"
      ob.colecaoEmail.Add(.Key, oEMail)
    End With
    With oUrl.Campos
      .Clear_vars()
      .Descricao = "página principal"
      .Url_key = "http://www.neobridge.com.br"
      ob.colecaoEmail.Add(.Key, oEMail)
    End With

    '------------------
    'Exemplo de familiaridade das classes!
    '
    'ob.Entidade.NomeRazaoSocial_key = ""
    'ob.Endereco.Logradouro_key = ""
    'ob.Telefone.Fone_key = ""
    'ob.EnderecoEletronico.EnderecoEletronico_key = ""
    'ob.Usuario.login_key = ""
    '---------------------
    With ob.Usuario
      .login_key = "eddy0001"
      .senha = "123456"
      .matricula = "CFDRT1256"
      .dtAdmissao = "12/08/2001"
      .idEmpresa = "1"
      .idUsuarioCadastrador = "7"
    End With

    With ob.UsuarioConfig
      .UsuarioAtivo = True
      .Funcao = "Auxiliar"
      .Credencial = 3
      .pmEditar = True
      .pmIncluir = True
      .pmLer = True
    End With



    resultado = ob.Salvar(False)
    'MsgBox(Me.admTB.Campos.IdadeTempoExistencia())
    'resultado = Me.admTB.inserir

    'Me.DG.DataSource = ob.DataSource

    MsgBox(resultado.Tag & " - id: " & ob.Entidade.ID & " - " & ob.Entidade.NomeRazaoSocial_key)


  End Function

  Private Function gravaEntidade_Metodo_1_a_1()
    'Parte 1 - Entidade ===============================================================
    Dim w As New NBdbm.Fachadas.CTR.CadastroEntidade
    Dim retorno As New NBdbm.tipos.Retorno
    Dim tmpIdEntidade As Double
    Dim tmpIdEndereco As Double
    Dim tmpIdTelefone As Double

    Me.Text = "Iniciando gravação da Entidade"
    With w.Entidade
      .Clear_vars()
      .CPFCNPJ_key = "807.829.149-34"
      .NomeRazaoSocial_key = "Edgar Francis da Silveira Vieira"
      .ApelidoNomeFantasia = "Eddy"
      .dtNascimentoInicioAtividades = "12/08/1973"
      .RgIE = "2.503.661"
      .OrgaoEmissorIM = "SSP-SC"
      .TextoRespeito = "nào há nada para por no txt respeito."
      retorno = .salvar(False)
      If retorno.Sucesso = True Then
        tmpIdEntidade = w.Entidade.ID
      End If
    End With

    'Parte 2 - Endereços ==============================================================
    Me.Text = "Iniciando gravação da Endereços"
    With w.Endereco
      .Clear_vars()
      .idEntidade_key = tmpIdEntidade
      .Logradouro_key = "rua José"
      .complemento = "ap 303"
      .Principal = True
      .Bairro = "Aqui"
      .Municipio = "esse"
      .UF = "SC"
      .Contato = "Fulano"
      .CEP = "88040-420"
      .Comentario = "Endereço da vovozinha"
      retorno = .salvar(False)
      If retorno.Sucesso = True Then
        tmpIdEndereco = w.Endereco.ID
      End If
    End With

    'Parte 3 - Telefones ==============================================================
    Me.Text = "Iniciando gravação da Telefones"
    With w.Telefone
      .Clear_vars()
      .idEntidade_key = tmpIdEntidade
      .idEndereco = tmpIdEndereco
      .DDD_key = "048"
      .Fone_key = "234 7620"
      .Ramal = "222"
      .Contato = "Beltrano"
      .Descricao = "PABX"
      retorno = .salvar(False)
      If retorno.Sucesso = True Then
        tmpIdTelefone = w.Telefone.ID
      End If
    End With
    '==================================================================================

  End Function

  Private Function gravaEntidade_MetodoAdd()

    Dim resultado As NBdbm.tipos.Retorno
    Dim ob As New NBdbm.Fachadas.CTR.CadastroEntidade
    Dim oEntidade As New NBdbm.Fachadas.CTR.primitivas.Entidade
    Dim oFone As New NBdbm.Fachadas.CTR.primitivas.Telefone
    Dim oCelular As New NBdbm.Fachadas.CTR.primitivas.Telefone
    Dim oEndereco As New NBdbm.Fachadas.CTR.primitivas.Endereco
    Dim oEmail As New NBdbm.Fachadas.CTR.primitivas.eMail
    Dim ourl As New NBdbm.Fachadas.CTR.primitivas.Url


    Beep()
    With oEntidade.campos
      .Clear_vars()
      .ApelidoNomeFantasia = "novo apelido"
      .CPFCNPJ_key = "807.829.149-34"
      .dtNascimentoInicioAtividades = CDate("08/12/1973")
      .NomeRazaoSocial_key = "Edgar Zé firino 10"
      .OrgaoEmissorIM = "SSP-SC"
      .RgIE = "2.503.661"
      .TextoRespeito = "esse é o texto a respeito novo"
      'Me.admTB.Campos.Email = "edgar@neobridge.com.br"
      'Me.admTB.Campos.Site = "www.neobridge.com.br"
      ob.Entidade = oEntidade.campos
    End With
    With oFone.Campos
      .Clear_vars()
      .idEntidade_key = 8
      .Descricao = "Residencial"
      .DDD_key = "47"
      .Fone_key = "252-1624"
      ob.colecaoTelefones.Add(.Fone_key, oFone)
    End With
    With oCelular.Campos
      .Clear_vars()
      .idEntidade_key = 8
      .Descricao = "Celular Pessoal"
      .DDD_key = "47"
      .Fone_key = "9987-1624"
      ob.colecaoTelefones.Add(.Fone_key, oCelular)
    End With
    With oEndereco.Campos
      .Clear_vars()
      .Logradouro_key = "minha rua"
      .Bairro = "meu bairro"
      .Municipio = "minha cidade"
      .UF = "SC"
      .complemento = "casa dos fundos"
      .Comentario = "endereço da vovozinha"
      .CEP = "88040420"
      .Principal = True
      ob.colecaoEnderecos.Add(.Comentario, oEndereco)
    End With
    With oEmail.Campos
      .Clear_vars()
      .Descricao = "Email do chefe"
      .eMail_key = "edgar@neobridge.com.br"
      ob.colecaoEmail.Add(.Descricao, oEmail)
    End With
    With ourl.Campos
      .Clear_vars()
      .Descricao = "site"
      .Url_key = "http://www.pagina.com.br"
      ob.colecaoUrl.Add(.Descricao, ourl)
    End With

    resultado = ob.Salvar(False)
    'MsgBox(Me.admTB.Campos.IdadeTempoExistencia())
    'resultado = Me.admTB.inserir

    'Me.DG.DataSource = ob.DataReader

    MsgBox(resultado.Tag & " - id: " & ob.Entidade.ID & " - " & ob.Entidade.NomeRazaoSocial_key)
    MsgBox(resultado.ToString)

  End Function

  Private Function gravaFone()
    'Exemplo para gravar 1 telefone
    Dim o As New NBdbm.Fachadas.CTR.primitivas.Telefone
    Dim sucesso As New NBdbm.tipos.Retorno

    Me.Text = o.tableName.ToString

    With o.Campos
      .idEntidade_key = "8"
      .idEndereco = "1"
      .DDD_key = "048"
      .Fone_key = "233 4327"
      .Ramal = "222"
      .Contato = "Edgar"
      .Descricao = "Telefone particular"
      sucesso = .salvar(False)
    End With

    MsgBox(sucesso.Tag)
    MsgBox(sucesso.Sucesso)
    MsgBox(sucesso.ToString)

  End Function

  Private Function gravaFuncionario_MetodoEvt()

    Dim resultado As NBdbm.tipos.Retorno
    Dim ob As New NBdbm.Fachadas.CTR.CadastroEntidade
    'Dim ob As New NBdbm.Fachadas.plxEVT.CadastroFuncionario
    'Dim oEntidade As New NBdbm.Fachadas.CTR.primitivas.Entidade
    'Dim oFone As New NBdbm.Fachadas.CTR.primitivas.Telefone
    'Dim oCelular As New NBdbm.Fachadas.CTR.primitivas.Telefone
    'Dim oEndereco As New NBdbm.Fachadas.CTR.primitivas.Endereco
    'Dim oEnderecoEletronico As New NBdbm.Fachadas.CTR.primitivas.EnderecoEletronico

    Beep()
    With ob.entidade
      .Clear_vars()
      .CPFCNPJ_key = "04.721.618/0001-00"
      .NomeRazaoSocial_key = "Neobridge Sistemas Ltda"
      .dtNascimentoInicioAtividades = CDate("01/06/2001")
      .OrgaoEmissorIM = "JUCESC"
      .RgIE = "415.970-5"
      .TextoRespeito = "Sistemas & Soluções"
    End With
    With ob.Telefone
      .Clear_vars()
      .DDD_key = "48"
      .Fone_key = "233-4327"
      .Ramal = "0"
      .Descricao = "Telefone/Fax"
      ob.colecaoTelefones.Add(.Fone_key, ob.Telefone)
    End With
    With ob.Telefone
      .Clear_vars()
      .DDD_key = "48"
      .Fone_key = "234-7620"
      .Ramal = "0"
      .Descricao = "PABX"
      ob.colecaoTelefones.Add(.Fone_key, ob.Telefone)
    End With
    With ob.Telefone
      .Clear_vars()
      .DDD_key = "48"
      .Fone_key = "9973-3064"
      .Ramal = "0"
      .Descricao = "Celular"
      ob.colecaoTelefones.Add(.Fone_key, ob.Telefone)
    End With
    With ob.Endereco
      .Clear_vars()
      .Logradouro_key = "Rua José João Martendal, 151"
      .Bairro = "Trindade SUL"
      .Municipio = "Florianópolis"
      .UF = "SC"
      .complemento = "Sala 303"
      .CEP = "88040-420"
      .Principal = True
      ob.colecaoEnderecos.Add(.Comentario, ob.Endereco)
    End With
    With ob.eMail
      .Clear_vars()
      .Descricao = "email"
      .eMail_key = "neo@neobridge.com.br"
      ob.colecaoEmail.Add(.Descricao, ob.eMail)
    End With
    With ob.Url
      .Clear_vars()
      .Descricao = "site"
      .Url_key = "http://www.neobridge.com.br"
      ob.colecaoUrl.Add(.Descricao, ob.Url)
    End With
    '------------------
    'Exemplo de familiaridade das classes!
    '
    'ob.Entidade.NomeRazaoSocial_key = ""
    'ob.Endereco.Logradouro_key = ""
    'ob.Telefone.Fone_key = ""
    'ob.EnderecoEletronico.EnderecoEletronico_key = ""
    'ob.Usuario.login_key = ""
    '---------------------
    'With ob.Usuario
    '  .login_key = "celioassis"
    '  .senha = "123456"
    'End With

    'With ob.UsuarioConfig
    '  .UsuarioAtivo = True
    '  .Funcao = "Auxiliar"
    '  .Credencial = 3
    'End With



    resultado = ob.Salvar(False)
    'MsgBox(Me.admTB.Campos.IdadeTempoExistencia())
    'resultado = Me.admTB.inserir

    'Me.DG.DataSource = ob.DataSource

    MsgBox(resultado.Tag & " - id: " & ob.Entidade.ID & " - " & ob.Entidade.NomeRazaoSocial_key)


  End Function

End Class
