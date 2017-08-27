Public Class frmFields
  Inherits System.Windows.Forms.Form

  Private admTB As NBdbm.Fachadas.allClass

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

  Protected Overrides Sub Finalize()
    Me.Dispose(True)
    MyBase.Finalize()
  End Sub

  'Required by the Windows Form Designer
  Private components As System.ComponentModel.IContainer

  'NOTE: The following procedure is required by the Windows Form Designer
  'It can be modified using the Windows Form Designer.  
  'Do not modify it using the code editor.
  Friend WithEvents txt_fields As System.Windows.Forms.TextBox
  Friend WithEvents bt_Fields As System.Windows.Forms.Button
  Friend WithEvents bt_campos_me As System.Windows.Forms.Button
  Friend WithEvents cmb_Table As System.Windows.Forms.ComboBox
  Friend WithEvents bt_Interface As System.Windows.Forms.Button
  Friend WithEvents bt_Fechar As System.Windows.Forms.Button
  Friend WithEvents bt_Copiar As System.Windows.Forms.Button
  <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
    Me.txt_fields = New System.Windows.Forms.TextBox
    Me.bt_Fields = New System.Windows.Forms.Button
    Me.bt_campos_me = New System.Windows.Forms.Button
    Me.cmb_Table = New System.Windows.Forms.ComboBox
    Me.bt_Interface = New System.Windows.Forms.Button
    Me.bt_Fechar = New System.Windows.Forms.Button
    Me.bt_Copiar = New System.Windows.Forms.Button
    Me.SuspendLayout()
    '
    'txt_fields
    '
    Me.txt_fields.Location = New System.Drawing.Point(16, 48)
    Me.txt_fields.Multiline = True
    Me.txt_fields.Name = "txt_fields"
    Me.txt_fields.Size = New System.Drawing.Size(464, 304)
    Me.txt_fields.TabIndex = 6
    Me.txt_fields.Text = "campos:"
    '
    'bt_Fields
    '
    Me.bt_Fields.Location = New System.Drawing.Point(488, 80)
    Me.bt_Fields.Name = "bt_Fields"
    Me.bt_Fields.Size = New System.Drawing.Size(128, 24)
    Me.bt_Fields.TabIndex = 2
    Me.bt_Fields.Text = "Listar Campos"
    '
    'bt_campos_me
    '
    Me.bt_campos_me.Location = New System.Drawing.Point(488, 112)
    Me.bt_campos_me.Name = "bt_campos_me"
    Me.bt_campos_me.Size = New System.Drawing.Size(128, 24)
    Me.bt_campos_me.TabIndex = 3
    Me.bt_campos_me.Text = "Listar Collection"
    '
    'cmb_Table
    '
    Me.cmb_Table.Location = New System.Drawing.Point(16, 16)
    Me.cmb_Table.Name = "cmb_Table"
    Me.cmb_Table.Size = New System.Drawing.Size(464, 21)
    Me.cmb_Table.TabIndex = 0
    Me.cmb_Table.Text = "Escolha a Tabela"
    '
    'bt_Interface
    '
    Me.bt_Interface.Location = New System.Drawing.Point(488, 48)
    Me.bt_Interface.Name = "bt_Interface"
    Me.bt_Interface.Size = New System.Drawing.Size(128, 24)
    Me.bt_Interface.TabIndex = 1
    Me.bt_Interface.Text = "Interface"
    '
    'bt_Fechar
    '
    Me.bt_Fechar.Location = New System.Drawing.Point(496, 320)
    Me.bt_Fechar.Name = "bt_Fechar"
    Me.bt_Fechar.Size = New System.Drawing.Size(120, 24)
    Me.bt_Fechar.TabIndex = 5
    Me.bt_Fechar.Text = "Sai&r"
    '
    'bt_Copiar
    '
    Me.bt_Copiar.Location = New System.Drawing.Point(496, 280)
    Me.bt_Copiar.Name = "bt_Copiar"
    Me.bt_Copiar.Size = New System.Drawing.Size(120, 32)
    Me.bt_Copiar.TabIndex = 4
    Me.bt_Copiar.Text = "Copiar para Clipboard"
    '
    'frmFields
    '
    Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
    Me.ClientSize = New System.Drawing.Size(624, 365)
    Me.Controls.Add(Me.bt_Copiar)
    Me.Controls.Add(Me.bt_Fechar)
    Me.Controls.Add(Me.bt_Interface)
    Me.Controls.Add(Me.cmb_Table)
    Me.Controls.Add(Me.bt_campos_me)
    Me.Controls.Add(Me.bt_Fields)
    Me.Controls.Add(Me.txt_fields)
    Me.Name = "frmFields"
    Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
    Me.Text = "frmFields"
    Me.ResumeLayout(False)

  End Sub

#End Region

  Private Sub frmFields_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load

    admTB = New NBdbm.Fachadas.allClass
    'admTB = New NBdbm.Fachadas.allClass("", NBdbm.tipos.tiposConection.OUTDB)
    'admTB = New NBdbm.Fachadas.allClass("", NBdbm.tipos.tiposConection.SQLSERVER)
    'admTB = New NBdbm.Fachadas.allClass("tabelas")
    Dim tables As Data.DataView
    Dim item As Data.DataRowView
    tables = admTB.datasourceTables
    'MsgBox(tables.Count)
    'Me.txt_fields.Text = tables.Count & " itens." & vbCrLf
    For Each item In tables
      Me.cmb_Table.Items.Add(item(0).ToString)
      'Debug.WriteLine(item(0).ToString)
      'Me.txt_fields.Text = Me.txt_fields.Text & vbCrLf & item(0).ToString
    Next


  End Sub

  Private Sub bt_Fields_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bt_Fields.Click

    Dim fields As Data.DataView
    Dim item As Data.DataRowView
    fields = Me.colItens

    Me.txt_fields.Text = fields.Count & " itens:" & vbCrLf
    For Each item In fields
      'Me.cmb_fieldSeek.Items.Add(item(0).ToString)
      'Debug.WriteLine(item(0).ToString)
      Me.txt_fields.Text = Me.txt_fields.Text & _
                           vbCrLf & _
                           "public " & item(1).ToString & " as " & item(2).ToString
    Next
  End Sub

  Private Function colItens() As DataView

    admTB.tableName = Me.cmb_Table.Text
    Return admTB.datasourceFields

  End Function

  Private Sub bt_campos_me_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bt_campos_me.Click

    Dim fields As Data.DataView
    Dim item As Data.DataRowView
    fields = Me.colItens

    Me.txt_fields.Text = fields.Count & " itens:" & vbCrLf
    For Each item In fields
      'Me.cmb_fieldSeek.Items.Add(item(0).ToString)
      'Debug.WriteLine(item(0).ToString)
      Me.txt_fields.Text = Me.txt_fields.Text & _
                           vbCrLf & _
                           "me." & item(1).ToString & " = dr(" & Chr(34) & item(1).ToString & Chr(34) & ").tostring"
    Next

  End Sub

  Private Sub bt_Fechar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bt_Fechar.Click
    Me.Close()
  End Sub

  Private Sub bt_Interface_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bt_Interface.Click
    Dim fields As Data.DataView
    Dim item As Data.DataRowView
    fields = Me.colItens

    Me.txt_fields.Text = fields.Count & " itens:" & vbCrLf & vbCrLf
    Me.txt_fields.Text = Me.txt_fields.Text & "Public Interface i" & Me.cmb_Table.Text & vbCrLf
    For Each item In fields
      'Me.cmb_fieldSeek.Items.Add(item(0).ToString)
      'Debug.WriteLine(item(0).ToString)
      Me.txt_fields.Text = Me.txt_fields.Text & _
                           vbCrLf & _
                           "Property " & item(1).ToString & " as " & item(2).ToString
    Next
    Me.txt_fields.Text = Me.txt_fields.Text & vbCrLf & "end Interface"
  End Sub

  Private Sub bt_Copiar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bt_Copiar.Click
    If Me.txt_fields.Text <> "" Then
      Clipboard.SetDataObject(Me.txt_fields.Text)
    End If
  End Sub
End Class
