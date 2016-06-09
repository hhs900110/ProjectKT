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
	public string GetData_ResourcePath(string _Key)
	{
		if(m_tableData_Resource == null)	{ SetData_Resource(); }
		if(m_tableData_Resource != null)
		{
			if(m_tableData_Resource.ContainsKey(_Key))
			{
				return m_tableData_Resource[_Key];
			}
		}
		DebugError(MethodBase.GetCurrentMethod().Name +": Null / " + _Key.ToString());
		return "";
	}
	
	public string GetData_Explain(int _Key)
	{
		if(m_tableData_Explain == null)		{ SetData_Explain(); }
		if(m_tableData_Explain != null)
		{
			if(m_tableData_Explain.ContainsKey(_Key))
			{
				return m_tableData_Explain[_Key];
			}
		}
		DebugError(MethodBase.GetCurrentMethod().Name +": Null / " + _Key.ToString());
		return "";
	}
	
	public SData_Package GetData_Package(int _Key)
	{
		if(m_tableData_Package == null)	{ SetData_Package(); }
		
		if(m_tableData_Package != null)
		{
			if(m_tableData_Package.ContainsKey(_Key))
			{
				return m_tableData_Package[_Key];
			}
		}
		DebugError(MethodBase.GetCurrentMethod().Name +": Null / " + _Key.ToString());
		return new SData_Package(-1, -1, -1, -1);
	}
	
	public int GetCount_Package()
	{
		if(m_tableData_Package == null)	{ SetData_Package(); }
		
		return m_tableData_Package.Count;
	}
	
#region ItemData
	private void CheckData_Item()
	{
		if(m_tableData_Item == null || m_listMenuIndex_Item == null)	{ SetData_Item(); }
	}
	
	public SData_Item GetData_Item(int _Key)
	{
		CheckData_Item();
		if(m_tableData_Item != null)
		{
			if(m_tableData_Item.ContainsKey(_Key))
			{
				return m_tableData_Item[_Key];
			}
		}
		DebugError(MethodBase.GetCurrentMethod().Name +": Null / " + _Key.ToString());
		return new SData_Item(-1, -1, -1, "", -1, -1,	-1, -1, -1, -1, -1, false);
	}
	
	public int GetMenuIndex_Item(int _MenuIdx)
	{
		CheckData_Item();
		if(_MenuIdx < m_listMenuIndex_Item.Count)
		{
			return m_listMenuIndex_Item[_MenuIdx];
		}
		return -1;
	}
	
	public int GetCount_Item()
	{
		CheckData_Item();
		if(m_listMenuIndex_Item == null)
		{
			return -1;
		}
		return m_listMenuIndex_Item.Count;
	}
#endregion
}