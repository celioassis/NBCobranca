Namespace Interfaces

    Public Interface iAdmDB
        Inherits IDisposable
        ReadOnly Property ConnString() As String
        Property Connection() As System.Data.IDbConnection
        Property ConnectionReader() As System.Data.IDbConnection
        ReadOnly Property NewConnection() As System.Data.IDbConnection
        Property Transaction() As System.Data.IDbTransaction
        Sub CancelarTransaction()
        Sub AbreTransaction()
        ReadOnly Property Command(ByVal stringCommand As String, ByVal pNewConnection As IDbConnection) As System.Data.IDbCommand
        ReadOnly Property Command(ByVal stringCommand As String) As System.Data.IDbCommand
        ReadOnly Property Command(ByVal stringCommand As String, ByVal transaction As System.Data.IDbTransaction) As System.Data.IDbCommand
        ReadOnly Property dataAdapter(ByVal stringCommand As String) As System.Data.IDbDataAdapter
        ReadOnly Property dataParameter(ByVal stringCommand As String) As System.Data.IDbDataParameter
        ReadOnly Property sqlListaTabelas() As String
        ReadOnly Property sqlListaCampos(ByVal tableName As String) As String
    End Interface

    Public Interface iStringConnection
        Inherits IDisposable
        'ms-help://MS.VSCC.2003/MS.MSDNQTR.2003FEB.1033/cpref/html/frlrfsystemdatasqlclientsqlconnectionclassconnectionstringtopic.htm

        ReadOnly Property StringConnection() As String

        Property ApplicationName() As String
        'Sintaxes:Application Name
        'Fornece o nome da aplicação
        'The name of the application, or '.Net SqlClient Data Provider'
        'if no application name is provided.

        Property AttachDBFilename() As String
        'Sintaxes: AttachDBFilename ou extended properties ou Initial File Name
        'Fornece o nome do arquivo do banco de dados
        'O nome e o path - ou seja: Um FQN
        'Quando for especificar somente o nome da base de dados,
        'usar a keyword 'Database'

        Property InitialCatalog() As String
        'Sintaxes: Initial Catalog ou Database
        'Fornece o nome do catálogo inicial

        Property ConnectTimeout() As String
        'Sintaxes: Connect Timeout ou Connection Timeout
        'Valores Default: 15
        'Fornece o tempo em segundos para que a conexão se encerre sozinha gerando um erro

        Property ConnectionLifetime() As String
        'Sintaxes: Connection Lifetime
        'Valores Default: 0
        'Valores suportados: inteiros
        'Quando uma conexão é retornada ao pool,
        'seu tempo da criação está comparado com o tempo atual,
        'e a conexão será destruída se essa extensão de tempo
        '(em segundos) exceder o valor especificado por Conexão Vida.
        'Isto é útil em configurações aglomeradas forçar a carga que balança
        'entre um usuário running e um usuário trazidos apenas em linha.
        'Um valor (0) das causas zero pooled conexões para ter
        'o intervalo de parada máximo da conexão.

        Property ConnectionReset() As String
        'Sintaxes: Connection Reset
        'Valores Default: 'true'
        'Valores suportados: true e false
        'Determina se a conexão da base de dados está restaurada ao ser extraído do pool.  Para a versão 7,0 do usuário de Microsoft SQL, ajustar-se a falso evita de fazer a um usuário adicional o desengate redondo quando o obter uma conexão, mas deve estar ciente que o estado da conexão, tal como o contexto da base de dados, não está sendo restaurado.
        'Determines whether the database connection is reset when being drawn from the pool. For Microsoft SQL Server version 7.0, setting to false avoids making an additional server round trip when obtaining a connection, but you must be aware that the connection state, such as database context, is not being reset.

        Property CurrentLanguage() As String
        'Sintaxes: Current Language
        'Fornece a linguagem da conexao

        Property DataSource() As String
        'Sintaxes: Data Source ou Server ou Address ou Addr ou Network Address
        'Fornece o nome do local do recurso de banco de dados, onde ele está na rede

        Property DSN() As String
        'Sintaxes: DSN
        'Fornece o nome do local do recurso de banco de dados, onde ele está na rede

        Property SystemDB() As String
        'Sintaxes: SystemDB
        'Fornece o nome do local do arq de grupo de usuários para o banco de dados

        Property DataSourcePorta() As String
        'Sintaxes: Data Source ou Server ou Address ou Addr ou Network Address
        'Fornece o nome do local do recurso de banco de dados, onde ele está na rede

        Property Encrypt() As String
        'Sintaxes: Encrypt
        'Valores default: 'false'
        'Valores suportados: true, false, yes, e no.
        'Fornece a configuração para criptografia SSL através de um certificado

        Property Enlist() As String 'Enlist
        'Sintaxes: Enlist
        'Valores Default: 'true'
        'Valores suportados: true, false, yes, e no
        'Quando verdadeiro, o pooler alista automaticamente a conexão no contexto atual da transação da linha da criação.
        'When true, the pooler automatically enlists the connection in the creation thread's current transaction context.
        'Recognized values are true, false, yes, and no.

        Property IntegratedSecurity() As String
        'Sintaxes: Integrated Security ou Trusted_Connection
        'Valores default: 'false'
        'Quando false, o usuário ID e a senha são especificados na conexão.
        'Quando true, as credenciais atuais do cliente(logon) de Windows
        'são usadas para o authentication.
        'Os valores reconhecidos são true, false, yes, no e sspi (recomendado fortemente),
        'que é equivalente rectificar.

        Property PersistSecurityInfo() As String
        'Sintaxes: Persist Security Info
        'Valores Default: 'false'
        'Valores suportados: true, false, yes, e no.
        'true ou yes - o login e senha não são retornados pela função stringconnection da conexão.
        'false ou no - são retornados.

        Property Password() As String
        'Sintaxes: Password ou Pwd
        'Valores Default: 
        'Valores suportados: caracteres
        'A senha para o cliente do usuário do SQL registrado.
        'Para manter o nível o mais elevado da segurança,
        'recomenda-se fortemente que você use o keyword integrado da segurança
        'ou do Trusted_Connection preferivelmente).

        Property UserID() As String
        'Sintaxes: User ID
        'Valores Default: 
        'Valores suportados: caracteres

        Property NetworkLibrary() As String
        'Sintaxes: Network Library ou Net
        'Valores default: 'dbmssocn'
        'Valores suportados: o dbnmpntw (nomeado Tubulação)
        '                    o dbmsrpcn (multiprotocol)
        '                    o dbmsadsn (conversa de Apple)
        '                    o dbmsgnet (ATRAVÉS DE)
        '                    o dbmslpcn (memória compartilhada)
        '                    o dbmsspxn (IPX/SPX)
        '                    o dbmssocn (TCP/IP)
        'The network library used to establish a connection to an instance of SQL Server. Supported values include dbnmpntw (Named Pipes), dbmsrpcn (Multiprotocol), dbmsadsn (Apple Talk), dbmsgnet (VIA), dbmslpcn (Shared Memory) and dbmsspxn (IPX/SPX), and dbmssocn (TCP/IP). 
        'The corresponding network DLL must be installed on the system to which you connect. If you do not specify a network and you use a local server (for example, "." or "(local)"), shared memory is used.

        Property PacketSize() As String
        'Sintaxes: Packet Size
        'Valores default: 8192
        'O tamanho(bytes) dos pacotes da rede usa para comunicar-se com
        'uma instancia do usuário do SQL.
        'Size in bytes of the network packets used to communicate with
        'an instance of SQL Server.

        Property WorkstationID() As String
        'Sintaxes: Workstation ID
        'Valores Default: the local computer name
        'Valores suportados: 'stone'
        'The name of the workstation connecting to SQL Server.

        Property MaxPoolSize() As String
        'Sintaxes: Max Pool Size
        'Valores Default: 100
        'Valores suportados: 
        'The maximum number of connections allowed in the pool.

        Property MinPoolSize() As String
        'Sintaxes: Min Pool Size
        'Valores Default: 0
        'Valores suportados: 
        'The minimum number of connections allowed in the pool.

        Property Pooling() As String
        'Sintaxes: Pooling
        'Valores Default: 'true'
        'Valores suportados: true, false, yes, e no
        'Quando verdadeiro, o objeto de SQLConnection é extraído do pool apropriado,
        'ou se necessário, criado e adicionado ao pool apropriado.
        'When true, the SQLConnection object is drawn from the appropriate pool,
        'or if necessary, is created and added to the appropriate pool.
        'Recognized values are true, false, yes, and no.

    End Interface

End Namespace
