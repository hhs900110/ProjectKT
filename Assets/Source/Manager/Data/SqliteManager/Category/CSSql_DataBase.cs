using UnityEngine;
using System.Collections.Generic;

public partial class CSSql_DataBase : CSSqliteBase
{
	private Dictionary<string, int> m_VersionTable = new Dictionary<string, int>();
	
	public override void Create()
	{
		m_strFile	= "database.crq";
		base.Create();
	}
	
	public bool Parse_Resource()
	{
		string query = "SELECT " +
			"`resource_name`, `resource_path`" +
			" FROM `resource`;";
		
		m_pSqlite.OpenDB( m_strPath );
		return m_pSqlite.Read( query, false );
	}
	
	public bool Parse_ItemData()
	{
		string query = "SELECT " +
			"`item_idx`, `item_name`, `item_info`, `item_raster`, `item_price_gold`, `item_price_cash`," +
			"`item_type_idx`, `item_use_time`, `item_area_x`, `item_area_y`, `item_use_value`, `item_is_active`" +
			" FROM `itemdata`;";
		
		m_pSqlite.OpenDB( m_strPath );
		return m_pSqlite.Read( query, false );
	}
}