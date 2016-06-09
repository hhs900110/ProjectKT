using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DefineBase;

public class HudMainStage : HudBase
{
	private EzGui_Texture	m_imgMainBoard;
	private EzGui_Texture	m_imgBackground;
	private EzGui_Texture	m_imgRankingBoard;
	
	private EzGui_Button	m_btnGameStart;
	private EzGui_Button[]	m_btnTab;
	
	
#region ObjectBase
	public override void Create()
	{
		base.Create();
	}
	
	public override void SetValid(bool IsValid)
	{
		base.SetValid(IsValid);
		
		EzGuiSetting.SetValidEzGui(m_imgMainBoard, IsValid);
		EzGuiSetting.SetValidEzGui(m_imgBackground, IsValid);
		EzGuiSetting.SetValidEzGui(m_imgRankingBoard, IsValid);
		
		EzGuiSetting.SetValidEzGui(m_btnGameStart, IsValid);
		EzGuiSetting.SetValidEzGui(m_btnTab, IsValid);
		
		if(IsValid)
		{
			Main.inst.GetHudManager().GetHudUserInfoManager().SetPopup();
		}
	}
#endregion
	
#region Resource Setting
	protected override void LoadResource()
	{
		SetResourceManagerCreateWithXml("Stage/HudMainStage");
		if(m_btnTab == null)	{ m_btnTab	= new EzGui_Button[(int)POPUP_STATE._MAX]; }
		
		m_imgMainBoard		= GetResource("TEXTURE_MainStage_MainBoard").GetComponent<EzGui_Texture>();
		m_imgBackground		= GetResource("TEXTURE_MainStage_Background").GetComponent<EzGui_Texture>();
		m_imgRankingBoard	= GetResource("TEXTURE_MainStage_RankingBoard").GetComponent<EzGui_Texture>();
		
		m_btnGameStart		= GetResource("BUTTON_MainStage_GameStart").GetComponent<EzGui_Button>();
		for(int i = 0; i < (int)POPUP_STATE._MAX; i++)
		{
			m_btnTab[i]	= GetResource(string.Format("{0}{1}", "BUTTON_MainStage_Tab", i)).GetComponent<EzGui_Button>();
		}
	}
	
	protected override void SetResources()
	{
		m_imgMainBoard.SetAnchor(SpriteRoot.ANCHOR_METHOD.MIDDLE_CENTER);
		m_imgBackground.SetAnchor(SpriteRoot.ANCHOR_METHOD.MIDDLE_CENTER);
		m_imgRankingBoard.SetAnchor(SpriteRoot.ANCHOR_METHOD.MIDDLE_CENTER);
		m_btnGameStart.SetAnchor(SpriteRoot.ANCHOR_METHOD.UPPER_CENTER);
		
		m_imgBackground.SetTextureSize(m_imgRankingBoard.GetTextureSizeX() - 20.0f, m_imgRankingBoard.GetTextureSizeY() - 30.0f);
		m_btnGameStart.SetValueChangedDelegate(StartButtonProcess);
		for(int i = 0; i < (int)POPUP_STATE._MAX; i++)
		{
			m_btnTab[i].SetData(i);
			m_btnTab[i].SetAnchor(SpriteRoot.ANCHOR_METHOD.MIDDLE_CENTER);
			m_btnTab[i].SetValueChangedDelegate(TabButtonProcess);
		}
	}
	
	public override void ReleaseResource()
	{
		EzGuiSetting.ReleaseEzGui(m_imgMainBoard);
		EzGuiSetting.ReleaseEzGui(m_imgBackground);
		EzGuiSetting.ReleaseEzGui(m_btnGameStart);
		EzGuiSetting.ReleaseEzGui(m_btnTab);
		ReleaseResourceObject();
	}
	
	public override void SetBasePos()
	{
		EzGuiSetting.SetPosEzGui(m_imgBackground, 0.0f, 60.0f);
		EzGuiSetting.SetPosEzGui(m_imgRankingBoard, 0.0f, 60.0f);
		
		EzGuiSetting.SetPosEzGui(m_btnGameStart, 0.0f, -270.0f);
		if(m_btnTab != null)
		{
			float Gap	= 74.0f;
			for(int i = 0; i < (int)POPUP_STATE._MAX; i++)
			{
				EzGuiSetting.SetPosEzGui(m_btnTab[i], (((int)POPUP_STATE._MAX - i) * Gap), 50.0f);
			}
		}
	}
#endregion
	
#region SetPopup/ClosePopup
	public override void SetPopup()
	{
		base.SetPopup();
	}
	
	public override void ClosePopup()
	{
		base.ClosePopup();
	}
#endregion
	
#region Delegate
	private void StartButtonProcess(IUIObject CurObject)
	{
		if(Main.inst.GetHudManager().GetHudPopupFrameManager().GetValid())
		{
			Main.inst.GetHudManager().GetHudPopupFrameManager().ClosePopup();
		}
		SendMessage((int)MSG_TYPE._PLAY, (int)PLAY_STATE._READY);
	}
	
	private void TabButtonProcess(IUIObject CurObject)
	{
		Main.inst.GetHudManager().GetHudPopupFrameManager().SetPopup((int)CurObject.Data, m_btnTab[(int)CurObject.Data].GetPosX(), m_btnTab[(int)CurObject.Data].GetPosY());
	}
#endregion
}