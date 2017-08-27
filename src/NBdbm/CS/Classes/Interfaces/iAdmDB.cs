using System.Data;
using System.Collections;
using Microsoft.VisualBasic;
using System.Diagnostics;
using System;

namespace NBdbm
{
    namespace Interfaces
    {

        public interface iAdmDB : IDisposable
        {
            string ConnString
            {
                get;
            }
            System.Data.IDbConnection Connection
            {
                get;
                set;
            }
            System.Data.IDbConnection ConnectionReader
            {
                get;
                set;
            }
            System.Data.IDbConnection NewConnection
            {
                get;
            }
            System.Data.IDbCommand Command(string stringCommand, IDbConnection pNewConnection);
            System.Data.IDbCommand Command(string stringCommand);
            System.Data.IDbCommand Command(string stringCommand, System.Data.IDbTransaction transaction);
            System.Data.IDbDataAdapter dataAdapter(string stringCommand);
            System.Data.IDbDataParameter dataParameter(string stringCommand);
            string sqlListaTabelas
            {
                get;
            }
            string sqlListaCampos(string tableName);
        }

        public interface iStringConnection : IDisposable
        {
            //ms-help://MS.VSCC.2003/MS.MSDNQTR.2003FEB.1033/cpref/html/frlrfsystemdatasqlclientsqlconnectionclassconnectionstringtopic.htm

            string StringConnection
            {
                get;
            }

            string ApplicationName
            {
                get;
                set;
            }
            //Sintaxes:Application Name
            //Fornece o nome da aplicação
            //The name of the application, or '.Net SqlClient Data Provider'
            //if no application name is provided.

            string AttachDBFilename
            {
                get;
                set;
            }
            //Sintaxes: AttachDBFilename ou extended properties ou Initial File Name
            //Fornece o nome do arquivo do banco de dados
            //O nome e o path - ou seja: Um FQN
            //Quando for especificar somente o nome da base de dados,
            //usar a keyword 'Database'

            string InitialCatalog
            {
                get;
                set;
            }
            //Sintaxes: Initial Catalog ou Database
            //Fornece o nome do catálogo inicial

            string ConnectTimeout
            {
                get;
                set;
            }
            //Sintaxes: Connect Timeout ou Connection Timeout
            //Valores Default: 15
            //Fornece o tempo em segundos para que a conexão se encerre sozinha gerando um erro

            string ConnectionLifetime
            {
                get;
                set;
            }
            //Sintaxes: Connection Lifetime
            //Valores Default: 0
            //Valores suportados: inteiros
            //Quando uma conexão é retornada ao pool,
            //seu tempo da criação está comparado com o tempo atual,
            //e a conexão será destruída se essa extensão de tempo
            //(em segundos) exceder o valor especificado por Conexão Vida.
            //Isto é útil em configurações aglomeradas forçar a carga que balança
            //entre um usuário running e um usuário trazidos apenas em linha.
            //Um valor (0) das causas zero pooled conexões para ter
            //o intervalo de parada máximo da conexão.

            string ConnectionReset
            {
                get;
                set;
            }
            //Sintaxes: Connection Reset
            //Valores Default: 'true'
            //Valores suportados: true e false
            //Determina se a conexão da base de dados está restaurada ao ser extraído do pool.  Para a versão 7,0 do usuário de Microsoft SQL, ajustar-se a falso evita de fazer a um usuário adicional o desengate redondo quando o obter uma conexão, mas deve estar ciente que o estado da conexão, tal como o contexto da base de dados, não está sendo restaurado.
            //Determines whether the database connection is reset when being drawn from the pool. For Microsoft SQL Server version 7.0, setting to false avoids making an additional server round trip when obtaining a connection, but you must be aware that the connection state, such as database context, is not being reset.

            string CurrentLanguage
            {
                get;
                set;
            }
            //Sintaxes: Current Language
            //Fornece a linguagem da conexao

            string DataSource
            {
                get;
                set;
            }
            //Sintaxes: Data Source ou Server ou Address ou Addr ou Network Address
            //Fornece o nome do local do recurso de banco de dados, onde ele está na rede

            string DSN
            {
                get;
                set;
            }
            //Sintaxes: DSN
            //Fornece o nome do local do recurso de banco de dados, onde ele está na rede

            string SystemDB
            {
                get;
                set;
            }
            //Sintaxes: SystemDB
            //Fornece o nome do local do arq de grupo de usuários para o banco de dados

            string DataSourcePorta
            {
                get;
                set;
            }
            //Sintaxes: Data Source ou Server ou Address ou Addr ou Network Address
            //Fornece o nome do local do recurso de banco de dados, onde ele está na rede

            string Encrypt
            {
                get;
                set;
            }
            //Sintaxes: Encrypt
            //Valores default: 'false'
            //Valores suportados: true, false, yes, e no.
            //Fornece a configuração para criptografia SSL através de um certificado

            string Enlist
            { //Enlist
                get;
                set;
            }
            //Sintaxes: Enlist
            //Valores Default: 'true'
            //Valores suportados: true, false, yes, e no
            //Quando verdadeiro, o pooler alista automaticamente a conexão no contexto atual da transação da linha da criação.
            //When true, the pooler automatically enlists the connection in the creation thread's current transaction context.
            //Recognized values are true, false, yes, and no.

            string IntegratedSecurity
            {
                get;
                set;
            }
            //Sintaxes: Integrated Security ou Trusted_Connection
            //Valores default: 'false'
            //Quando false, o usuário ID e a senha são especificados na conexão.
            //Quando true, as credenciais atuais do cliente(logon) de Windows
            //são usadas para o authentication.
            //Os valores reconhecidos são true, false, yes, no e sspi (recomendado fortemente),
            //que é equivalente rectificar.

            string PersistSecurityInfo
            {
                get;
                set;
            }
            //Sintaxes: Persist Security Info
            //Valores Default: 'false'
            //Valores suportados: true, false, yes, e no.
            //true ou yes - o login e senha não são retornados pela função stringconnection da conexão.
            //false ou no - são retornados.

            string Password
            {
                get;
                set;
            }
            //Sintaxes: Password ou Pwd
            //Valores Default:
            //Valores suportados: caracteres
            //A senha para o cliente do usuário do SQL registrado.
            //Para manter o nível o mais elevado da segurança,
            //recomenda-se fortemente que você use o keyword integrado da segurança
            //ou do Trusted_Connection preferivelmente).

            string UserID
            {
                get;
                set;
            }
            //Sintaxes: User ID
            //Valores Default:
            //Valores suportados: caracteres

            string NetworkLibrary
            {
                get;
                set;
            }
            //Sintaxes: Network Library ou Net
            //Valores default: 'dbmssocn'
            //Valores suportados: o dbnmpntw (nomeado Tubulação)
            //                    o dbmsrpcn (multiprotocol)
            //                    o dbmsadsn (conversa de Apple)
            //                    o dbmsgnet (ATRAVÉS DE)
            //                    o dbmslpcn (memória compartilhada)
            //                    o dbmsspxn (IPX/SPX)
            //                    o dbmssocn (TCP/IP)
            //The network library used to establish a connection to an instance of SQL Server. Supported values include dbnmpntw (Named Pipes), dbmsrpcn (Multiprotocol), dbmsadsn (Apple Talk), dbmsgnet (VIA), dbmslpcn (Shared Memory) and dbmsspxn (IPX/SPX), and dbmssocn (TCP/IP).
            //The corresponding network DLL must be installed on the system to which you connect. If you do not specify a network and you use a local server (for example, "." or "(local)"), shared memory is used.

            string PacketSize
            {
                get;
                set;
            }
            //Sintaxes: Packet Size
            //Valores default: 8192
            //O tamanho(bytes) dos pacotes da rede usa para comunicar-se com
            //uma instancia do usuário do SQL.
            //Size in bytes of the network packets used to communicate with
            //an instance of SQL Server.

            string WorkstationID
            {
                get;
                set;
            }
            //Sintaxes: Workstation ID
            //Valores Default: the local computer name
            //Valores suportados: 'stone'
            //The name of the workstation connecting to SQL Server.

            string MaxPoolSize
            {
                get;
                set;
            }
            //Sintaxes: Max Pool Size
            //Valores Default: 100
            //Valores suportados:
            //The maximum number of connections allowed in the pool.

            string MinPoolSize
            {
                get;
                set;
            }
            //Sintaxes: Min Pool Size
            //Valores Default: 0
            //Valores suportados:
            //The minimum number of connections allowed in the pool.

            string Pooling
            {
                get;
                set;
            }
            //Sintaxes: Pooling
            //Valores Default: 'true'
            //Valores suportados: true, false, yes, e no
            //Quando verdadeiro, o objeto de SQLConnection é extraído do pool apropriado,
            //ou se necessário, criado e adicionado ao pool apropriado.
            //When true, the SQLConnection object is drawn from the appropriate pool,
            //or if necessary, is created and added to the appropriate pool.
            //Recognized values are true, false, yes, and no.

        }

    }

}
