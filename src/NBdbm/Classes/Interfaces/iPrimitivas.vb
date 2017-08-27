'Informa��es aos outros programadores:
'Esta classe usa: TabSize = 2
'                 TabIdent = 2
'Lembre-se:
'   interface que come�a com iCadastro - trata-se de uma interface de Base
'   interface que come�a com iNMNMNMNM - trata-se de uma interface de especificidade
'
'   Toda classe tem uma propriedade ID que ao ser implementada faz refer�ncia
'   ao IdTabela autonumer�vel, para que se tenha certeza que:
'   classe.ID -> sempre ser� o ID autonumer�vel da tabela. Esta informa��o j� est�
'   na classe mais primitiva de todas: iCadastro

Namespace Interfaces

    Namespace Primitivas

        Public Interface iObjetoTabela
            Inherits IDisposable
            ReadOnly Property ID() As Integer
            ReadOnly Property Key() As String
            Property Parent() As Fachadas.allClass
            Sub salvar(ByVal noCommit As Boolean)
            Sub salvar()
            'Property Parent() As Interfaces.Primitivas.allClass
            Sub Clear_filters()
            Sub Clear_vars()
        End Interface

        Public Interface iAllClass

            Inherits System.IDisposable

            Property tableName() As String
            ReadOnly Property stringSQL(Optional ByVal forceReplace As Boolean = False) As String
            Sub Clear_filters()
            Sub Clear_vars()
            Sub getFields(ByVal pManterConexaoAberta As Boolean)
            Sub editar(ByVal noCommit As Boolean)
            Sub excluir(ByVal noCommit As Boolean)
            Sub inserir(ByVal noCommit As Boolean)
            Function datasourceTables() As Data.DataView
            Function datasourceFields() As Data.DataView
            Function DataSource() As Data.DataView
            WriteOnly Property filterGroupBy() As String
            WriteOnly Property filterHaving() As String
            WriteOnly Property filterOrderBy() As String
            WriteOnly Property filterWhere() As String
            WriteOnly Property filterTop() As Integer

        End Interface
    End Namespace
End Namespace