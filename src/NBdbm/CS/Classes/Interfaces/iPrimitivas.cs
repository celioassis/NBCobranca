using System.Data;
using System.Collections;
using Microsoft.VisualBasic;
using System.Diagnostics;
using System;

//Informações aos outros programadores:
//Esta classe usa: TabSize = 2
//                 TabIdent = 2
//Lembre-se:
//   interface que começa com iCadastro - trata-se de uma interface de Base
//   interface que começa com iNMNMNMNM - trata-se de uma interface de especificidade
//
//   Toda classe tem uma propriedade ID que ao ser implementada faz referência
//   ao IdTabela autonumerável, para que se tenha certeza que:
//   classe.ID -> sempre será o ID autonumerável da tabela. Esta informação já está
//   na classe mais primitiva de todas: iCadastro

namespace NBdbm
{
	namespace Interfaces
	{
		
		namespace Primitivas
		{
			
			public interface iObjetoTabela : IDisposable
			{
				int ID{
					get;
				}
				string Key{
					get;
				}
				void salvar(bool noCommit);
				void salvar();
				//Property Parent() As Interfaces.Primitivas.allClass
				void Clear_filters();
				void Clear_vars();
			}
			
			public interface iAllClass : System.IDisposable
			{
				
				
				string tableName{
					get;
					set;
				}
				string stringSQL(bool forceReplace);
				void Clear_filters();
				void Clear_vars();
				Collection getFields();
				void editar(bool noCommit);
				void excluir(bool noCommit);
				void inserir(bool noCommit);
				System.Data.IDataReader DataReader();
				System.Data.DataView datasourceTables();
				System.Data.DataView datasourceFields();
				System.Data.DataView DataSource();
				string filterGroupBy
				{
					set;
				}
				string filterHaving
				{
					set;
				}
				string filterOrderBy
				{
					set;
				}
				string filterWhere
				{
					set;
				}
				int filterTop
				{
					set;
				}
				
			}
		}
	}
}
