using UnityEngine;
using System.Collections;
using System.Text;

public class SqliteManager : ClassBase
{
	private CSSql_DataBase	m_pSqliteScript = null;
	
	public override void Create()
	{
		m_pSqliteScript	= new CSSql_DataBase();
		m_pSqliteScript.Create();
	}
	
	public CSSql_DataBase	GetSql_DataBase()	{ return m_pSqliteScript; }
}
