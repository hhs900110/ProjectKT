using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using DefineBase;

public sealed class GameItemManager
{
	private List<int>				m_dItemPassiveCheck		= null;
	private List<int>				m_dItemActiveCheck		= null;
	
	private int						m_dUseActiveItemIndex	= -1;
	private Dictionary<int, int>	m_dItemCanUse			= null;
	
	
	public void CheckItem(int _ItemIndex)
	{
		if(m_dItemPassiveCheck == null)	{ m_dItemPassiveCheck	= new List<int>(); }
		if(m_dItemActiveCheck == null)	{ m_dItemActiveCheck	= new List<int>(); }
		
		if(Main.data.GetData_Item(_ItemIndex).ItemIsActive)
		{
			if(m_dItemActiveCheck.Contains(_ItemIndex))
			{
				m_dItemActiveCheck.Remove(_ItemIndex);
			}
			else
			{
				m_dItemActiveCheck.Add(_ItemIndex);
			}
		}
		else
		{
			if(m_dItemPassiveCheck.Contains(_ItemIndex))
			{
				m_dItemPassiveCheck.Remove(_ItemIndex);
			}
			else
			{
				m_dItemPassiveCheck.Add(_ItemIndex);
			}
		}
	}
	
	
	public void SetPopup()
	{
		if(m_dItemPassiveCheck == null)	{ m_dItemPassiveCheck	= new List<int>(); }
		if(m_dItemActiveCheck == null)	{ m_dItemActiveCheck	= new List<int>(); }
		if(m_dItemCanUse == null)		{ m_dItemCanUse	= new Dictionary<int, int>(m_dItemActiveCheck.Count); }
		
		int count	= m_dItemActiveCheck.Count;
		for(int i = 0; i < count; i++)
		{
			int Index	= m_dItemActiveCheck[i];
			if(m_dItemCanUse.ContainsKey(Index))
			{
				m_dItemCanUse[Index]	= Main.data.GetData_Item(Index).ItemUseValue;
			}
			else
			{
				m_dItemCanUse.Add(Index, Main.data.GetData_Item(Index).ItemUseValue);
			}
		}
		UseItemReset();
	}
	
	public void ClosePopup()
	{
		if(m_dItemPassiveCheck != null)	{ m_dItemPassiveCheck.Clear(); }
		if(m_dItemActiveCheck != null)	{ m_dItemActiveCheck.Clear(); }
		if(m_dItemCanUse != null)		{ m_dItemCanUse.Clear(); }
	}
	
	public void UseItemReset()
	{
		m_dUseActiveItemIndex	= -1;
		Main.inst.GetHudManager().GetHudGameStage().SetItemNum();
	}
	
	public void ItemButtonPush(int _ItemIndex)
	{
		if(m_dItemCanUse[_ItemIndex] <= 0)
		{ return ; }
		
		if(m_dUseActiveItemIndex == _ItemIndex)
		{
			if(GetUseActiveItem())
			{
				m_dUseActiveItemIndex	= -1;
			}
			else
			{
				m_dUseActiveItemIndex	=_ItemIndex;
			}
		}
		else
		{
			m_dUseActiveItemIndex	=_ItemIndex;
		}
	}
	
	public void UseNowItem()
	{
		m_dItemCanUse[m_dUseActiveItemIndex]--;
		UseItemReset();
	}
	
	public int GetServiceablePassiveItemIndex(int _MenuIndex)
	{
		if(m_dItemPassiveCheck == null)
		{ return 0 ; }
		if(m_dItemPassiveCheck.Count > _MenuIndex)
		{ return m_dItemPassiveCheck[_MenuIndex]; }
		return 0 ;
	}
	public int GetServiceablePassiveItemNum()
	{
		if(m_dItemPassiveCheck == null)
		{ return 0 ; }
		return m_dItemPassiveCheck.Count;
	}
	
	public int GetServiceableActiveItemIndex(int _MenuIndex)
	{
		if(m_dItemActiveCheck == null)
		{ return 0 ; }
		if(m_dItemActiveCheck.Count > _MenuIndex)
		{ return m_dItemActiveCheck[_MenuIndex]; }
		return 0 ;
	}
	public int GetServiceableActiveItemNum()
	{
		if(m_dItemActiveCheck == null)
		{ return 0 ; }
		return m_dItemActiveCheck.Count;
	}
	
	public bool	GetUseActiveItem()
	{
		if(m_dUseActiveItemIndex == -1)
		{
			return false;
		}
		return true;
	}
	public int	GetUseActiveItemIndex()		{ return m_dUseActiveItemIndex; }
	public int	GetItemCanUse(int _index)	{ return m_dItemCanUse[_index]; }
}