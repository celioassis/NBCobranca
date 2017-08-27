'Codigo fonte: Edgar Francis
'Incío: 01/03/2004
'última Alteração:
'mailto:edgar@yap.com.br
'http://www.yap.com.br
'file:\\stone\C\Pastas%20de%20Trabalho\Edgar\vss%20Working%20Folder\PB2000\CONTROLE_app\Bibliotecas\NBdbm\Classes\AdmDB\

Option Explicit On

Imports System.Configuration
Imports System.Collections
Imports System.Xml

Friend Class settings
    Implements IDisposable


#Region "  Start  "     'Variáveis, new, dispose, finalize

    Private AblyI As New AssemblyInfo
    Private xMnSetAll As AppSettings
    Private pvt_stringConnection As ADM.stringConnection
    Private pvt_Credencial As tipos.Retorno
    Private pvt_tipoBanco As String
    Private pvt_tipoConexao As String
    Private pvt_sintaxeData As String
    Private pvt_sintaxePontoDecimal As String
    Private aSelf As self

    Public Sub New(ByRef pSelf As self)
        aSelf = pSelf
        xMnSetAll = New AppSettings(AblyI.Config_Arquivo)
    End Sub

    Public Sub Dispose() Implements System.IDisposable.Dispose
        If Not IsNothing(xMnSetAll) Then xMnSetAll.Dispose()
        If Not IsNothing(pvt_stringConnection) Then pvt_stringConnection.Dispose()
        If Not IsNothing(pvt_Credencial) Then pvt_Credencial.Dispose()

        AblyI = Nothing
        xMnSetAll = Nothing
        pvt_stringConnection = Nothing
        pvt_Credencial = Nothing
        pvt_tipoBanco = Nothing
        pvt_tipoConexao = Nothing
        pvt_sintaxeData = Nothing
        pvt_sintaxePontoDecimal = Nothing

    End Sub 'settings

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
        Call Dispose()
    End Sub 'settings
#End Region

#Region "  Code Base  "     'sub and function privates

    Friend Sub save(Optional ByVal Xml As String = "")
        xMnSetAll.Save()
    End Sub

#End Region

#Region "  Code Property's & Metodos  "

    Friend Property Credencial() As tipos.Retorno
        Get
            Return pvt_Credencial
        End Get
        Set(ByVal Value As tipos.Retorno)
            pvt_Credencial = Value
        End Set
    End Property


    Friend ReadOnly Property BaseDirectory() As String
        Get
            'Return AppDomain.CurrentDomain.BaseDirectory()
            Return AblyI.BaseDirectory
        End Get
    End Property

    Friend ReadOnly Property appExeName() As String
        Get
            Return Reflection.AssemblyName.GetAssemblyName("").Name
        End Get
    End Property

    Friend Property tipoConexao(Optional ByVal atributo As String = "valor") As String
        Get
            If pvt_tipoConexao Is Nothing Then
                pvt_tipoConexao = xMnSetAll.Item("appSettings", "tipoconexao", "SQLSERVER").Value
            End If
            Return pvt_tipoConexao
        End Get
        Set(ByVal newTipoConexao As String)
            pvt_tipoConexao = newTipoConexao
            'xMnSetAll.Update("appSettings", "tipoconexao", newTipoConexao)
            If Not pvt_stringConnection Is Nothing Then
                pvt_stringConnection.Dirty = True
            End If
        End Set
    End Property

    Friend Property tipoBanco(Optional ByVal atributo As String = "valor") As String
        Get
            If pvt_tipoConexao Is Nothing Then
                pvt_tipoConexao = xMnSetAll.Item("appSettings", "tipobanco", "SQLSERVER").Value
            End If
            Return pvt_tipoBanco
        End Get
        Set(ByVal value As String)
            pvt_tipoBanco = value
            'xMnSetAll.Update("appSettings", "tipobanco", value)
        End Set
    End Property

    Friend Property sintaxeData(Optional ByVal atributo As String = "valor") As String
        Get
            If pvt_sintaxeData Is Nothing Then
                pvt_sintaxeData = xMnSetAll.Item("appSettings", "sintaxeData", "MM/dd/yyyy").Value
            End If
            Return pvt_sintaxeData
        End Get
        Set(ByVal value As String)
            pvt_sintaxeData = value
        End Set
    End Property

    Friend Property sintaxePontoDecimal(Optional ByVal atributo As String = "valor") As String
        Get
            If pvt_sintaxePontoDecimal Is Nothing Then
                pvt_sintaxePontoDecimal = xMnSetAll.Item("appSettings", "sintaxePontoDecimal", ".").Value
            End If
            Return pvt_sintaxePontoDecimal
        End Get
        Set(ByVal value As String)
            pvt_sintaxePontoDecimal = value
        End Set
    End Property

    Friend ReadOnly Property stringConnection() As String
        Get
            If pvt_stringConnection Is Nothing Then
                pvt_stringConnection = New ADM.stringConnection(aSelf)
                pvt_stringConnection.Dirty = True
            End If
            If pvt_stringConnection.Dirty = True Then
                Call Me.xMnSetAll.Item(Me.tipoConexao, pvt_stringConnection)

                'pvt_stringConnection.ApplicationName = xMnSetAll.Item(Me.tipoConexao, "aplicationName", "NeobridgeSistemas").Value
                'pvt_stringConnection.AttachDBFilename = xMnSetAll.Item(Me.tipoConexao, "AttachDBFilename", "c:\dbs\Neobridge.mdf").Value
                'pvt_stringConnection.ConnectionLifetime = xMnSetAll.Item(Me.tipoConexao, "ConnectionLifetime", 120).Value
                'pvt_stringConnection.ConnectionReset = xMnSetAll.Item(Me.tipoConexao, "ConnectionReset", 200).Value
                'pvt_stringConnection.ConnectTimeout = xMnSetAll.Item(Me.tipoConexao, "ConnectTimeout", 20).Value
                'pvt_stringConnection.CurrentLanguage = xMnSetAll.Item(Me.tipoConexao, "CurrentLanguage", "").Value
                'pvt_stringConnection.DataSource = xMnSetAll.Item(Me.tipoConexao, "DataSource", "127.0.0.1").Value
                'pvt_stringConnection.DSN = xMnSetAll.Item(Me.tipoConexao, "DSN", "Neobridge").Value()
                'pvt_stringConnection.SystemDB = xMnSetAll.Item(Me.tipoConexao, "SystemDB", "x:\Bancos de Dados\Logo.bmp").Value()
                'pvt_stringConnection.DataSourcePorta = xMnSetAll.Item(Me.tipoConexao, "DataSourcePorta", "1433").Value
                'pvt_stringConnection.Encrypt = xMnSetAll.Item(Me.tipoConexao, "Encrypt", "false").Value
                'pvt_stringConnection.Enlist = xMnSetAll.Item(Me.tipoConexao, "Enlist", "true").Value
                'pvt_stringConnection.InitialCatalog = xMnSetAll.Item(Me.tipoConexao, "InitialCatalog", "Neobridge").Value
                'pvt_stringConnection.IntegratedSecurity = xMnSetAll.Item(Me.tipoConexao, "IntegratedSecurity", "true").Value
                'pvt_stringConnection.MaxPoolSize = xMnSetAll.Item(Me.tipoConexao, "MaxPoolSize", "100").Value
                'pvt_stringConnection.MinPoolSize = xMnSetAll.Item(Me.tipoConexao, "MinPoolSize", "0").Value
                'pvt_stringConnection.NetworkLibrary = xMnSetAll.Item(Me.tipoConexao, "NetworkLibrary", "dbmssocn").Value
                'pvt_stringConnection.PacketSize = xMnSetAll.Item(Me.tipoConexao, "PacketSize", "8192").Value
                'pvt_stringConnection.PersistSecurityInfo = xMnSetAll.Item(Me.tipoConexao, "PersistSecurityInfo", "true").Value()
                'pvt_stringConnection.Pooling = xMnSetAll.Item(Me.tipoConexao, "Pooling", "true").Value()
                'pvt_stringConnection.WorkstationID = xMnSetAll.Item(Me.tipoConexao, "WorkstationID", "stone").Value()
                'pvt_stringConnection.UserID = xMnSetAll.Item(Me.tipoConexao, "UserId", "com login!").Value()
                'pvt_stringConnection.Password = xMnSetAll.Item(Me.tipoConexao, "Password", "com senha!").Value()
                'MsgBox("Deveria ter parado aqui!!! - stringConnection - Settings")
                pvt_stringConnection.Dirty = False
                'Me.save()
            End If
            Dim mConnBuilder As System.Data.SqlClient.SqlConnectionStringBuilder
            mConnBuilder = New System.Data.SqlClient.SqlConnectionStringBuilder(pvt_stringConnection.StringConnection)
            Return mConnBuilder.ToString
        End Get
    End Property

    Friend ReadOnly Property Versao() As NBdbm.tipos.Versao
        Get
            Dim mVersao As New NBdbm.tipos.Versao
            'Implementar ler do arquivo "ini"
            'Versao = "01.00.0001"
            mVersao.major = AblyI.Version.Major
            mVersao.minor = AblyI.Version.Minor
            mVersao.revision = AblyI.Version.Revision
            'Call xMng.writeNodo("versao nbDbm", "Valor", AblyI.Version.ToString)
            'xMnSetAll.Update("appSettings", "versao nbdbm", AblyI.Version.ToString)
            Return mVersao
        End Get
        'Set(ByVal newVersao As NBdbm.tipos.Versao)
        'newVersao
        'Implementar salvar no arquivo "ini"
        'End Set
    End Property

    Friend ReadOnly Property UserId() As String
        Get
            UserId = NBFuncoes.decripto("BZÏÍs·MÝ>¶").ToString
        End Get
    End Property

    Friend ReadOnly Property Password() As String
        Get
            Password = NBFuncoes.decripto("=*MZÏ>ÙMÝ").ToString
        End Get
    End Property

#End Region

End Class

#Region " Motor settings "

Friend Class AppSetting
    ' A variável mParent será uma instância
    ' Que permitirá a atualização das
    ' configurações no arquivo
    Private mParent As AppSettings
    Private mstrKey As String
    Private mstrValue As String

    Public Property Key() As String
        Get
            Return mstrKey
        End Get
        Set(ByVal Value As String)
            Me.UpdateParent()
            'Value = mstrKey
            mstrKey = Value
        End Set
    End Property

    Public Property Value() As String
        Get
            Return mstrValue
        End Get
        Set(ByVal Value As String)
            Me.UpdateParent()
            mstrValue = Value
        End Set
    End Property

    Private Sub UpdateParent()
        If Not Me.mParent Is Nothing Then
            'Verificar como se comporta a falta do "Block"
            'Me.mParent.Update(String.Empty, Me)
        End If
    End Sub

    Public Sub New()
        Me.New(String.Empty, String.Empty)
    End Sub

    Public Sub New(ByVal Key As String, ByVal Value As String)
        Me.New(Key, Value, Nothing)
    End Sub

    Friend Sub New(ByVal Key As String, ByVal Value As String, ByVal Parent As AppSettings)
        MyBase.New()
        Me.mstrKey = Key
        Me.mstrValue = Value
        Me.mParent = Parent
    End Sub
End Class

Friend Class AppSettings
    Implements IEnumerable, IDisposable

    ' This classes wraps access to the configuration//appSettings
    ' section of the config file specified when an instance is created.
    ' XPath expressions are used to find values when requested.
    ' In addition, the class supports enumeration by implementing
    ' IEnumerable and providing a private Iterator which implements
    ' IEnumerator.

    Private cfg As New XmlDocument
    'Private xAS As XmlNode
    Private mstrFileName As String      'Nome do Arquivo de Configurações
    Private mblnAutoSave As Boolean     'Informa se é para salvar ao sair
    Private mblnDirty As Boolean        '
    Private maItems() As AppSetting     'Itens da configuração
    Private mblnDisposing As Boolean    'Informa que está sendo disponibilizada
    Private mblnDisposed As Boolean     'Informa que foi disponibilizada

    'Private Const APPSETTINGS_ELEMENT As String = "configuration//appSettings"
    Private Const APPSETTINGS_ELEMENT As String = "configuration"
    Private xmlBLOCK As String = "configuration//appSettings"
    Private Const NEWELEMENT As String = "item"
    Private Const XPATH_KEY_ADD As String = "//item"
    Private Const XPATH_KEY_ADD_KEY As String = "//item[@key='{0}']"

    Public Sub New(ByVal ConfigFile As String, Optional ByVal block As String = APPSETTINGS_ELEMENT)
        Me.New(ConfigFile, False)
    End Sub

    Public Sub New(ByVal ConfigFile As String, ByVal AutoSave As Boolean, Optional ByVal block As String = APPSETTINGS_ELEMENT)
        MyBase.New()
        Me.xmlBLOCK = block
        If ConfigFile.Length = 0 Then
            Throw New ArgumentNullException("Você deve Informar um nome de arquivo 'fqn' válido.")
        Else
            If System.IO.File.Exists(ConfigFile) Then
                Try
                    cfg.Load(ConfigFile)
                Catch exp As Exception
                    Throw New System.IO.FileLoadException("O arquivo informado não pode ser aberto. Por favor, veja mais informações em 'more information'.", exp)
                End Try
                ' Get the main appSettings element
                ' so we can add new settings
                'xAS = cfg.SelectSingleNode(APPSETTINGS_ELEMENT)

                'If xAS(APPSETTINGS_ELEMENT) Is Nothing Then
                If xAS(block) Is Nothing Then
                    Throw New ConfigurationErrorsException("O arquivo especificado não possui um XML válido, este arquivo não contém uma configuração válida.")
                End If

                ' If we get this far we need to
                ' store the file name for any changes
                mstrFileName = ConfigFile

                Me.AutoSave = AutoSave
            Else
                'Throw New System.IO.FileNotFoundException(String.Format("O arquivo especificado não existe.", ConfigFile))
            End If
        End If

    End Sub

    Public Sub Dispose() Implements System.IDisposable.Dispose
        Me.mblnDisposing = True
        If Me.Dirty Then
            Me.Save()
        End If
        Me.mblnDisposed = True
        Me.mblnDisposing = False
        GC.SuppressFinalize(Me)
    End Sub

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
        If Me.Dirty Then
            If Not Me.mblnDisposed Then
                If Not Me.mblnDisposing Then
                    Me.Dispose()
                End If
            End If
        End If
    End Sub

    Private ReadOnly Property xAS(ByVal block As String) As XmlNode
        Get
            Return cfg.SelectSingleNode("//" & block)
        End Get
    End Property

    Public Property AutoSave() As Boolean
        Get
            Return Me.mblnAutoSave
        End Get
        Set(ByVal Value As Boolean)
            Me.mblnAutoSave = Value
        End Set
    End Property

    Public ReadOnly Property Dirty() As Boolean
        Get
            Return Me.mblnDirty
        End Get
    End Property

    Public Function Add(ByVal block As String, ByVal Key As String, ByVal Value As String) As AppSetting
        Dim NewSetting As New AppSetting(Key, Value, Me)
        Me.Add(block, NewSetting)
        Return NewSetting
    End Function

    Public Sub Add(ByVal block As String, ByVal NewSetting As AppSetting)
        Dim newElem As XmlElement
        Dim newAttr As XmlAttribute

        newElem = cfg.SelectSingleNode("//" & block)
        If newElem Is Nothing Then
            newElem = cfg.CreateElement(block)
            xAS("configuration").AppendChild(newElem)
        End If

        newElem = cfg.CreateElement(NEWELEMENT)

        newAttr = cfg.CreateAttribute("key")
        newAttr.Value = NewSetting.Key
        newElem.Attributes.Append(newAttr)

        newAttr = cfg.CreateAttribute("value")
        newAttr.Value = NewSetting.Value
        newElem.Attributes.Append(newAttr)

        xAS(block).AppendChild(newElem)

        Me.mblnDirty = True
        If Me.AutoSave Then
            'Me.Save()
        End If
    End Sub

    Public Function Item(ByVal block As String, ByVal stringConnection As ADM.stringConnection) As Collection
        Dim xNode As XmlNode
        Dim xNodeList As XmlNodeList
        Try
            xNodeList = cfg.SelectSingleNode("//" & block).SelectNodes("//" & block & "//item")

            If stringConnection.ConnProperty.Count > 0 Then stringConnection.ZeraConnProperty()

            For Each xNode In xNodeList
                If Trim(xNode.Attributes(1).InnerText) <> String.Empty Then
                    Call stringConnection.ConnProperty.Add(xNode.Attributes(0).InnerText, xNode.Attributes(1).InnerText, System.Type.GetType("System.String"), True)
                End If
            Next
            Return stringConnection.ConnProperty.Collection
        Catch ex As Exception
            Throw New Exception("Não foi possível carregar a coleção.")
        End Try

    End Function

    Public Function Item(ByVal block As String, ByVal Key As String) As AppSetting
        Return Me.Item(block, Key, String.Empty)
    End Function

    Public Function Item(ByVal block As String, ByVal Key As String, ByVal valueDefault As String) As AppSetting
        Dim xNode As XmlNode
        Dim strSearch As String = XPATH_KEY_ADD_KEY
        Dim las As New AppSetting

        Try

            xNode = xAS(block).SelectSingleNode("//" & block & String.Format(strSearch, Key))

            If xNode Is Nothing Then
                las.Key = Key
                las.Value = valueDefault
                Me.Add(block, las)
                'Return las
            Else
                las.Key = Key
                las.Value = xNode.Attributes.Item(1).Value
            End If

            Return las

        Catch ex As Exception
            Throw ex
            'Dim las As New AppSetting(Key, valueDefault)
            'Me.Add(block, las)
            'xNode = xAS(block).SelectSingleNode(String.Format(strSearch, Key))
        End Try

    End Function

    Public Sub RemoveByKey(ByVal block As String, ByVal Setting As AppSetting)
        Dim xNode As XmlNode
        Dim strSearch As String = XPATH_KEY_ADD_KEY

        xNode = xAS(block).SelectSingleNode(String.Format(strSearch, Setting.Key))

        If Not xNode Is Nothing Then

        End If
        Me.mblnDirty = True
        If Me.AutoSave Then
            Me.Save()
        End If

    End Sub

    Public Sub RemoveByKey(ByVal Key As String)
        Beep()
        Throw New NotSupportedException("Função: RemoveByKey(ByVal Key As String), ainda não implementada!")
    End Sub

    Public Function Update(ByVal block As String, ByVal Key As String, ByVal Value As String) As AppSetting
        Dim NewSetting As New AppSetting(Key, Value, Me)
        Me.Update(block, NewSetting)
        Return NewSetting
    End Function

    Public Sub Update(ByVal block As String, ByVal NewSetting As AppSetting)
        Dim xNode As XmlNode
        Dim strSearch As String = XPATH_KEY_ADD_KEY

        xNode = xAS(block).SelectSingleNode(String.Format(strSearch, NewSetting.Key))

        If xNode Is Nothing Then
            'Throw New ArgumentOutOfRangeException("Key", NewSetting.Key, "O item que está sendo solicitado existe. Um novo item está sendo criado.")
            Me.Add(block, NewSetting)
        Else
            xNode.Attributes.Item(1).Value = NewSetting.Value
        End If

        Me.mblnDirty = True

        If Me.AutoSave Then
            Me.Save()
        End If
    End Sub

    Public Sub Save()
        ' We don't have a try catch here so
        ' that if we fail, it will bounce up
        ' to our caller.
        'cfg.Save(Me.mstrFileName)
        Me.mblnDirty = False
    End Sub

    Private Function GetAllItems(ByVal block As String) As AppSetting()
        Dim xNode As XmlNode
        Dim xNodeList As XmlNodeList
        Dim atts As XmlAttributeCollection
        Dim xmNew As New XmlDocument

        If block.Trim = String.Empty Then
            block = APPSETTINGS_ELEMENT & "//appSettings"
        End If

        xmNew.LoadXml(xAS(block).OuterXml)
        xNodeList = xmNew.SelectNodes(XPATH_KEY_ADD)

        Dim asa(xNodeList.Count - 1) As AppSetting
        Dim asi As AppSetting
        Dim i As Integer = -1

        For Each xNode In xNodeList
            i += 1
            atts = xNode.Attributes

            With atts
                asi = New AppSetting(.Item(0).Value, .Item(1).Value, Me)
            End With
            asa(i) = asi
        Next

        Return asa
    End Function

    Private Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
        'Observe que não há em AppSettings uma coleção de AppSetting
        'Porque a coleção está baseada no XMLdocument
        'Por isso há uma função getAllItens que lê o xml
        'e o envia para dentro da classe iterator
        'Para que esta classe faça o que o método GetEnumerator faz em uma coleção.
        'Dim colecao As Collection
        'Return colecao.GetEnumerator
        '
        'Neste caso maItens é uma matriz de appSetting
        Me.maItems = Me.GetAllItems(Me.xmlBLOCK)
        Return New Iterator(Me.maItems)
    End Function

    Private Class Iterator
        Implements IEnumerator

        ' This private class exposes the necessary
        ' functions so that For..Each will work.
        Private mData() As AppSetting
        Private Index As Integer = -1

        Public Sub New(ByVal Keys() As AppSetting)
            mData = Keys
        End Sub
        Private ReadOnly Property Current() As Object Implements System.Collections.IEnumerator.Current
            Get
                Return mData(Index)
            End Get
        End Property
        Private Function MoveNext() As Boolean Implements System.Collections.IEnumerator.MoveNext
            Index += 1
            If (Index <= (mData.Length - 1)) Then
                Return True
            End If
        End Function
        Private Sub Reset() Implements System.Collections.IEnumerator.Reset
            Index = -1
        End Sub
    End Class

    Public Class nodeCollection
        Public Caption As String
        Public Value As String
        Public Dirty As Boolean
    End Class
End Class

#End Region




