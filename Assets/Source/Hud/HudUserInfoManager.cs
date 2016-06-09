using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DefineBase;

public class HudUserInfoManager : HudBase
{
	enum INFO_SLOT
	{
		_MONEY	= 0,
		_GEM,
		_BELL,
		_MAX,
	}
	
	private UserInfoSlot[]	m_UserInfoSlot;
	
#region ObjectBase
	public override void Create()
	{
		base.Create();
	}
	
	public override void SetValid(bool IsValid)
	{
		base.SetValid(IsValid);
		
		if(m_UserInfoSlot != null)
		{
			for(int i = 0; i < (int)INFO_SLOT._MAX; i++)
			{
				if(m_UserInfoSlot[i] != null)
				{
					m_UserInfoSlot[i].SetValid(IsValid);
				}
			}
		}
	}
#endregion
	
#region Resource Setting
	protected override void LoadResource()
	{
		SetResourceManagerCreateWithXml("UserInfoManager");
		if(m_UserInfoSlot == null)	{ m_UserInfoSlot = new UserInfoSlot[(int)INFO_SLOT._MAX]; }
		
		m_UserInfoSlot[(int)INFO_SLOT._MONEY]	= new UserInfoSlot();
		m_UserInfoSlot[(int)INFO_SLOT._MONEY].SetResource(GetResourceManagerScript(), "UserInfo_Money");
		
		m_UserInfoSlot[(int)INFO_SLOT._GEM]		= new UserInfoSlot();
		m_UserInfoSlot[(int)INFO_SLOT._GEM].SetResource(GetResourceManagerScript(), "UserInfo_Gem");
		
		m_UserInfoSlot[(int)INFO_SLOT._BELL]	= new UserInfoSlot();
		m_UserInfoSlot[(int)INFO_SLOT._BELL].SetResource(GetResourceManagerScript(), "UserInfo_Bell");
	}
	
	protected override void SetResources()
	{
		m_UserInfoSlot[(int)INFO_SLOT._MONEY].text	= "000000";
		m_UserInfoSlot[(int)INFO_SLOT._GEM].text	= "0000";
		m_UserInfoSlot[(int)INFO_SLOT._BELL].text	= "00";
	}
	
	public override void ReleaseResource()
	{
		ReleaseResourceObject();
	}
	
	public override void SetBasePos()
	{
//		float BasePosX;
//		float BasePosY;
		
		m_UserInfoSlot[(int)INFO_SLOT._MONEY].SetPos(  70.0f, 40.0f);
		m_UserInfoSlot[(int)INFO_SLOT._GEM]	 .SetPos(-130.0f, 40.0f);
		m_UserInfoSlot[(int)INFO_SLOT._BELL] .SetPos( 220.0f, 40.0f);
//		if(DefineBaseManager.inst.GameBaseHeight == 960.0f)
//		{
//			m_UserInfoSlot[(int)INFO_SLOT._BELL].SetPos(0.0f, 330.0f);
//		}
//		else
//		{
//			m_UserInfoSlot[(int)INFO_SLOT._BELL].SetPos(0.0f, 350.0f);
//		}
	}
#endregion
	
#region SetPopup/ClosePopup
	public override void SetPopup()
	{
		if(m_UserInfoSlot == null)
		{
			base.SetPopup();
		}
		else
		{
			SetValid(true);
		}
	}
	
	public override void ClosePopup()
	{
		SetValid(false);
	}
#endregion
	
#region Delegate
//	private void 
#endregion
}