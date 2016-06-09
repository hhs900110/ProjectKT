using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using DefineBase;
using DataStruct;

#pragma warning disable 0168 // variable declared but not used.
#pragma warning disable 0219 // variable assigned but not used.
#pragma warning disable 0414 // private field assigned but not used.

public partial class DataManager
{
	private void SetData_Resource_XML()
	{
		CSSql_DataBase SqliteScript = Main.inst.GetSqliteManager().GetSql_DataBase();
		try
		{
			if( SqliteScript.Parse_Resource() )
			{
				while( SqliteScript.FetchRow() )
				{
					int count	= 0;
					
					string	_resourceName	= SqliteScript.GetRowString(count++);
					string	_resourcePath	= SqliteScript.GetRowString(count++);
					
					m_tableData_Resource.Add(_resourceName, _resourcePath);
				}
			}
			else
			{
				throw new Exception(MethodBase.GetCurrentMethod().Name + ": Open Failed.");
			}
		}
		catch ( Exception e )
		{
			DebugError( MethodBase.GetCurrentMethod().Name +": "+ e.ToString() );
		}
		finally
		{
			SqliteScript.CloseParsing();
		}
	}
	private void SetData_Resource_Text()
	{
		TextParser Parser	= Main.parser;
		Parser.Open("Script/Data/ResourceData");
		
		bool	IsLoop	= true;
		int		Count	= 0;
		while(IsLoop)
		{
			int		_itemIdx	= Parser.GetTokenInt();
			if(Parser.INVALID_VALUE == _itemIdx)
			{
				IsLoop	= false;
			}
			else
			{
				string	_resourceName	= Parser.GetTokenChar();
				string	_resourcePath	= Parser.GetTokenChar();
				
				m_tableData_Resource.Add(_resourceName, _resourcePath);
			}
		}
		Parser.Close();
	}
	private void SetData_Resource()
	{
		if(m_tableData_Resource == null)	{ m_tableData_Resource	= new Dictionary<string, string>(); }
		m_tableData_Resource.Clear();
		
		if(DefineBaseManager.inst.GetPlatformType() == (int)PLATFORM_TYPE.PLATFORM_WEB)
		{
			SetData_Resource_Text();
		}
		else
		{
			SetData_Resource_XML();
		}
	}
	
	private void SetData_Explain()
	{
		if(m_tableData_Explain == null)		{ m_tableData_Explain	= new Dictionary<int, string>(); }
		m_tableData_Explain.Clear();
		
		CSSql_DataBase SqliteScript = Main.inst.GetSqliteManager().GetSql_DataBase();
		try
		{
			if( SqliteScript.Parse_Resource() )
			{
				while( SqliteScript.FetchRow() )
				{
					int count	= 0;
					
					int		_explainIndex	= SqliteScript.GetRowInt(count++);
					string	_explainText	= SqliteScript.GetRowString(count++);
					
					m_tableData_Explain.Add(_explainIndex, _explainText);
				}
			}
			else
			{
				throw new Exception(MethodBase.GetCurrentMethod().Name + ": Open Failed.");
			}
		}
		catch ( Exception e )
		{
			DebugError( MethodBase.GetCurrentMethod().Name +": "+ e.ToString() );
		}
		finally
		{
			SqliteScript.CloseParsing();
		}
	}
	
	private void SetData_Package()
	{
		if(m_tableData_Package == null)	{ m_tableData_Package = new Dictionary<int, SData_Package>(); }
		m_tableData_Package.Clear();
		
		TextParser	Parser	= Main.parser;
		Parser.Open("Script/Data/Package");
		
		bool	IsLoop	= true;
		int		Count	= 0;
		while(IsLoop)
		{
			int _DataIndex = Parser.GetTokenInt();
			if(Parser.INVALID_VALUE == _DataIndex)
			{
				IsLoop = false;
			}
			else
			{
				int _AddGold	= Parser.GetTokenInt();
				int _AddExp		= Parser.GetTokenInt();
				int _AddScore	= Parser.GetTokenInt();
				
				SData_Package tmpStruct	= new SData_Package(_DataIndex, _AddGold, _AddExp, _AddScore);
				m_tableData_Package.Add(_DataIndex, tmpStruct);
			}
		}
		Parser.Close();
	}
	
	private void SetData_Item_XML()
	{
		CSSql_DataBase SqliteScript = Main.inst.GetSqliteManager().GetSql_DataBase();
		try
		{
			if( SqliteScript.Parse_ItemData() )
			{
				while( SqliteScript.FetchRow() )
				{
					int count	= 0;
					
					int		_itemIdx		= SqliteScript.GetRowInt(count++);
					int		_itemName		= SqliteScript.GetRowInt(count++);
					int		_itemInfo		= SqliteScript.GetRowInt(count++);
					string	_itemRaster		= SqliteScript.GetRowString(count++);
					int		_itemPriceGold	= SqliteScript.GetRowInt(count++);
					int		_itemPriceCash	= SqliteScript.GetRowInt(count++);
					
					int		_itemTypeIdx	= SqliteScript.GetRowInt(count++);
					int		_itemUseTime	= SqliteScript.GetRowInt(count++);
					int		_itemAreaX		= SqliteScript.GetRowInt(count++);
					int		_itemAreaY		= SqliteScript.GetRowInt(count++);
					int		_itemUseValue	= SqliteScript.GetRowInt(count++);
					bool	_itemIsActive	= SqliteScript.GetRowBoolean(count++);
					
					SData_Item tmpStruct	= new SData_Item(_itemIdx, _itemName, _itemInfo, _itemRaster, _itemPriceGold, _itemPriceCash,
							_itemTypeIdx, _itemUseTime, _itemAreaX, _itemAreaY, _itemUseValue, _itemIsActive);
					m_tableData_Item.Add(_itemIdx, tmpStruct);
					m_listMenuIndex_Item.Add(_itemIdx);
				}
			}
			else
			{
				throw new Exception(MethodBase.GetCurrentMethod().Name + ": Open Failed.");
			}
		}
		catch ( Exception e )
		{
			DebugError( MethodBase.GetCurrentMethod().Name +": "+ e.ToString() );
		}
		finally
		{
			SqliteScript.CloseParsing();
		}
	}
	private void SetData_Item_Text()
	{
		TextParser Parser	= Main.parser;
		Parser.Open("Script/Data/ItemData");
		
		bool	IsLoop	= true;
		int		Count	= 0;
		while(IsLoop)
		{
			int		_itemIdx	= Parser.GetTokenInt();
			if(Parser.INVALID_VALUE == _itemIdx)
			{
				IsLoop	= false;
			}
			else
			{
				int		_itemName		= Parser.GetTokenInt();
				int		_itemInfo		= Parser.GetTokenInt();
				string	_itemRaster		= Parser.GetTokenChar();
				int		_itemPriceGold	= Parser.GetTokenInt();
				int		_itemPriceCash	= Parser.GetTokenInt();
				
				int		_itemTypeIdx	= Parser.GetTokenInt();
				int		_itemUseTime	= Parser.GetTokenInt();
				int		_itemAreaX		= Parser.GetTokenInt();
				int		_itemAreaY		= Parser.GetTokenInt();
				int		_itemUseValue	= Parser.GetTokenInt();
				int		_itemIsActived	= Parser.GetTokenInt();
				bool	_itemIsActive	= false;
				if(_itemIsActived == 1)
				{
					_itemIsActive	= true;
				}
				
				SData_Item tmpStruct	= new SData_Item(_itemIdx, _itemName, _itemInfo, _itemRaster, _itemPriceGold, _itemPriceCash,
						_itemTypeIdx, _itemUseTime, _itemAreaX, _itemAreaY, _itemUseValue, _itemIsActive);
				m_tableData_Item.Add(_itemIdx, tmpStruct);
				m_listMenuIndex_Item.Add(_itemIdx);
			}
		}
		Parser.Close();
	}
	private void SetData_Item()
	{
		if(m_tableData_Item == null)		{ m_tableData_Item		= new Dictionary<int, SData_Item>(); }
		if(m_listMenuIndex_Item == null)	{ m_listMenuIndex_Item	= new List<int>(); }
		m_tableData_Item.Clear();
		m_listMenuIndex_Item.Clear();
		
		if(DefineBaseManager.inst.GetPlatformType() == (int)PLATFORM_TYPE.PLATFORM_WEB)
		{
			SetData_Item_Text();
		}
		else
		{
			SetData_Item_XML();
		}
	}
}