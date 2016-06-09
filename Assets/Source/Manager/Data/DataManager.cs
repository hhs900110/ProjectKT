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
	////////////////////
	private Dictionary<string, string>		m_tableData_Resource;
	private Dictionary<int, string>			m_tableData_Explain;
	
	private Dictionary<int, SData_Package>	m_tableData_Package;
	private Dictionary<int, SData_Item>		m_tableData_Item;
	
	private List<int>						m_listMenuIndex_Item;
	
	public void Create()
	{
//		SetData_Package();
//		SetData_Item();
	}
	
	public void Release()
	{
		if(m_tableData_Resource != null)
		{
			m_tableData_Resource.Clear();
			m_tableData_Resource	= null;
		}
		if(m_tableData_Explain != null)
		{
			m_tableData_Explain.Clear();
			m_tableData_Explain		= null;
		}
		
		if(m_tableData_Package != null)
		{
			m_tableData_Package.Clear();
			m_tableData_Package		= null;
		}
		if(m_tableData_Item != null)
		{
			m_tableData_Item.Clear();
			m_tableData_Item		= null;
		}
		if(m_listMenuIndex_Item != null)
		{
			m_listMenuIndex_Item.Clear();
			m_listMenuIndex_Item	= null;
		}
	}
	
	private void DebugError(string error)
	{
#if UNITY_EDITOR
		Debug.LogError(error);
#endif
	}
}