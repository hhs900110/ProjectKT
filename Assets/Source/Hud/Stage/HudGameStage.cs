using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DefineBase;

public sealed class HudGameStage : HudBase
{
	private EzGui_Texture		m_imgGameBoard;
	
	private EzGui_Button		m_btnAdvice;
	private EzGui_Button		m_btnPause;
	private EzGui_Button		m_btnPlay;
	
	private EzGui_Texture		m_imgSelectBtn;
	private EzGui_Button[]		m_btnItem;
	private EzGui_SpriteText[]	m_txtItem;
	
	private HudNumberManager	m_scriptScore;
	
#region ObjectBase
	public override void Create()
	{
		base.Create();
	}
	
	public override void SetValid(bool IsValid)
	{
		base.SetValid(IsValid);
		
		EzGuiSetting.SetValidEzGui(m_imgGameBoard, IsValid);
		if(m_scriptScore)
		{
			m_scriptScore.SetValid(IsValid);
		}
		EzGuiSetting.SetValidEzGui(m_btnAdvice, IsValid);
		SetValid_Pause(IsValid);
		SetValid_ItemButton(IsValid);
		SetItemSelect(IsValid);
		
		if(IsValid)
		{
			Main.inst.GetHudManager().GetHudUserInfoManager().ClosePopup();
		}
	}
	
	private void SetValid_Pause(bool IsValid)
	{
		int PlatformType	= DefineBaseManager.inst.GetPlatformType();
		if(PlatformType == (int)PLATFORM_TYPE.PLATFORM_ANDROID
		|| PlatformType == (int)PLATFORM_TYPE.PLATFORM_IPHONE)
		{
			EzGuiSetting.SetValidEzGui(m_btnPause, IsValid && !Main.game.IsPause());
			EzGuiSetting.SetValidEzGui(m_btnPlay, IsValid && Main.game.IsPause());
		}
		else
		{
			EzGuiSetting.SetValidEzGui(m_btnPause, false);
			EzGuiSetting.SetValidEzGui(m_btnPlay, false);
		}
	}
	
	private void SetValid_ItemButton(bool IsValid)
	{
		int PlatformType	= DefineBaseManager.inst.GetPlatformType();
		if(PlatformType == (int)PLATFORM_TYPE.PLATFORM_ANDROID
		|| PlatformType == (int)PLATFORM_TYPE.PLATFORM_IPHONE)
		{
			EzGuiSetting.SetValidEzGui(m_btnItem, IsValid);
			EzGuiSetting.SetValidEzGui(m_txtItem, IsValid);
		}
		else
		{
			EzGuiSetting.SetValidEzGuiList(m_btnItem, IsValid, 0);
			EzGuiSetting.SetValidEzGuiList(m_txtItem, IsValid, 0);
		}
	}
	
	private void SetItemSelect(bool IsValid)
	{
		if(Main.game.GetGameItemManager().GetUseActiveItem())
		{
			EzGuiSetting.SetValidEzGui(m_imgSelectBtn, IsValid);
			
			int ItemIdx	= Main.game.GetGameItemManager().GetUseActiveItemIndex();
			for(int i = 0; i < m_btnItem.Length; i++)
			{
				if(ItemIdx == (int)m_btnItem[i].GetUIButton().Data)
				{
					m_imgSelectBtn.SetPosX(m_btnItem[i].GetPosX());
					m_imgSelectBtn.SetPosY(m_btnItem[i].GetPosY());
				}
			}
		}
		else
		{
			EzGuiSetting.SetValidEzGui(m_imgSelectBtn, false);
		}
	}
#endregion
	
#region Resource Setting
	protected override void LoadResource()
	{
		SetResourceManagerCreateWithXml("Stage/HudGameStage");
		int count	= Main.game.GetGameItemManager().GetServiceableActiveItemNum();
		if(m_btnItem == null)		{ m_btnItem	= new EzGui_Button[count]; }
		if(m_txtItem == null)		{ m_txtItem	= new EzGui_SpriteText[count]; }
		
		m_imgGameBoard	= GetResource("TEXTURE_GameStage_GameBoard").GetComponent<EzGui_Texture>();
		
		m_btnAdvice		= GetResource("BUTTON_GameStage_Advice").GetComponent<EzGui_Button>();
		m_btnPause		= GetResource("BUTTON_GameStage_Pause").GetComponent<EzGui_Button>();
		m_btnPlay		= GetResource("BUTTON_GameStage_Play").GetComponent<EzGui_Button>();
		
		m_imgSelectBtn	= GetResource("TEXTURE_GameStage_SelectButton").GetComponent<EzGui_Texture>();
		
		for(int i = 0; i < count; i++)
		{
			int		itemIndex	= Main.game.GetGameItemManager().GetServiceableActiveItemIndex(i);
			string	itemRaster	= Main.data.GetData_Item(itemIndex).ItemRaster;
			string	itemPath	= Main.data.GetData_ResourcePath(itemRaster);
			m_btnItem[i]	= ResourceLoad.GetEZGUI_BUTTON("BUTTON_GameStage_Item"+i.ToString(), itemPath,
							0, 0, (int)m_imgGameBoard.GetLayer()+3, HUD_BASE_POS._BOTTOM_CENTER)
							.GetComponent<EzGui_Button>();
			m_txtItem[i]	= ResourceLoad.GetEZGUI_SPRITETEXT("SPRITE_GameStage_Item"+i.ToString(), "SpriteText/SpriteText24",
							0, 0, (int)m_imgGameBoard.GetLayer()+5,
							SpriteText.Anchor_Pos.Middle_Center, SpriteText.Alignment_Type.Right, "", HUD_BASE_POS._BOTTOM_CENTER)
							.GetComponent<EzGui_SpriteText>();
		}
	}
	
	protected override void SetResources()
	{
		GameObject tmpObject	= new GameObject("Score");
		m_scriptScore			= tmpObject.AddComponent<HudNumberManager>();
		m_scriptScore.Create();
		m_scriptScore.color			= Color.white;
		m_scriptScore.BasePosType	= (int)HUD_BASE_POS._TOP_CENTER;
		m_scriptScore.ChildAnchor	= SpriteRoot.ANCHOR_METHOD.UPPER_CENTER;
		m_scriptScore.IsCommaOn		= true;
		m_scriptScore.SetNumber(0);
		tmpObject.transform.parent	= Main.inst.GetResourceObject_EZGUITEXTURE().transform;
		
		m_imgGameBoard.SetTextureSize(DefineBaseManager.inst.GameBaseWidth, DefineBaseManager.inst.GameBaseHeight);
		
		m_btnAdvice.SetValueChangedDelegate(AdviceProcess);
		
		m_btnPause.SetAnchor(SpriteRoot.ANCHOR_METHOD.UPPER_RIGHT);
		m_btnPause.SetValueChangedDelegate(SetPauseProcess);
		
		m_btnPlay.SetAnchor(SpriteRoot.ANCHOR_METHOD.UPPER_RIGHT);
		m_btnPlay.SetValueChangedDelegate(SetPauseProcess);
		
		m_imgSelectBtn.SetAnchor(SpriteRoot.ANCHOR_METHOD.MIDDLE_CENTER);
		
		int count	= Main.game.GetGameItemManager().GetServiceableActiveItemNum();
		
		for(int i = 0; i < count; i++)
		{
			m_btnItem[i].GetUIButton().Data	= Main.game.GetGameItemManager().GetServiceableActiveItemIndex(i);
			m_btnItem[i].SetAnchor(SpriteRoot.ANCHOR_METHOD.MIDDLE_CENTER);
			m_btnItem[i].SetValueChangedDelegate(ItemProcess);
			
			m_txtItem[i].SetAnchor(SpriteText.Anchor_Pos.Lower_Right);
			m_txtItem[i].SetAllignment(SpriteText.Alignment_Type.Right);
		}
	}
	
	public override void ReleaseResource()
	{
		if(m_scriptScore)
		{
			m_scriptScore.Release();
			GameObject.DestroyImmediate(m_scriptScore.gameObject);
		}
		EzGuiSetting.ReleaseEzGui(m_imgGameBoard);
		EzGuiSetting.ReleaseEzGui(m_btnAdvice);
		EzGuiSetting.ReleaseEzGui(m_btnPause);
		EzGuiSetting.ReleaseEzGui(m_btnPlay);
		if(m_btnItem != null)
		{
			for(int i = 0; i < m_btnItem.Length; i++)
			{
				EzGuiSetting.ReleaseEzGui(m_btnItem[i]);
			}
		}
		m_btnItem	= null;
		if(m_txtItem != null)
		{
			for(int i = 0; i < m_txtItem.Length; i++)
			{
				EzGuiSetting.ReleaseEzGui(m_txtItem[i]);
			}
		}
		m_txtItem	= null;
		
		ReleaseResourceObject();
	}
	
	public override void SetBasePos()
	{
		float Gap		= DefineBaseManager.inst.KittyGap;
		
		EzGuiSetting.SetPosEzGui(m_btnPause, 0.0f, 0.0f);
		EzGuiSetting.SetPosEzGui(m_btnPlay, 0.0f, 0.0f);
		
		int count	= Main.game.GetGameItemManager().GetServiceableActiveItemNum();
		float BasePosX	= 150.0f;
		float BasePosY	=  55.0f;
		
		int PlatformType	= DefineBaseManager.inst.GetPlatformType();
		if(PlatformType == (int)PLATFORM_TYPE.PLATFORM_ANDROID
		|| PlatformType == (int)PLATFORM_TYPE.PLATFORM_IPHONE)
		{
			for(int i = 0; i < count; i++)
			{
				EzGuiSetting.SetPosEzGui(m_btnItem[i], BasePosX - BasePosX*i, BasePosY);
				EzGuiSetting.SetPosEzGui(m_txtItem[i], BasePosX - BasePosX*i - 35.0f, BasePosY + 15.0f);
			}
		}
		else
		{
			for(int i = 0; i < count; i++)
			{
				EzGuiSetting.SetPosEzGui(m_btnItem[i], 0.0f, BasePosY);
				EzGuiSetting.SetPosEzGui(m_txtItem[i],  - 35.0f, BasePosY + 15.0f);
			}
		}
		
		if(m_scriptScore)
		{
			m_scriptScore.SetPos(0.0f, 65.0f);
		}
	}
#endregion
	
#region Delegate
	private void AdviceProcess(IUIObject CurObject)
	{
		Main.inst.GetHudManager().GetHudPopupAdvice().SetPopup();
	}
	
	private void SetPauseProcess(IUIObject CurObject)
	{
		Main.game.ClickPause();
		SetValid_Pause(true);
	}
	
	private void ItemProcess(IUIObject CurObject)
	{
		if(Main.game.GetIsGameEnd())
		{ return ; }
		
		Main.game.GetGameItemManager().ItemButtonPush((int)CurObject.Data);
		SetItemSelect(true);
	}
#endregion
	
#region SetPopup/ClosePopup
	public override void SetPopup()
	{
		base.SetPopup();
		SetScore(0);
	}
	
	public override void ClosePopup()
	{
		base.ClosePopup();
	}
	
	public void SetRePopup()
	{
		SetScore(0);
		SetBasePos();
		SetValid(true);
	}
#endregion
	
	public void SetScore(int _Score)
	{
		if(m_scriptScore != null)
		{
			m_scriptScore.SetNumber(_Score);
		}
	}
	
	public void SetItemNum()
	{
		int count	= Main.game.GetGameItemManager().GetServiceableActiveItemNum();
		for(int i = 0; i < count; i++)
		{
			SetItemNum(i);
		}
		SetItemSelect(true);
	}
	
	public void SetItemNum(int _index)
	{
		int num	= Main.game.GetGameItemManager().GetItemCanUse((int)m_btnItem[_index].GetUIButton().Data);
		
		if(num > 0)
		{
			m_btnItem[_index].SetcontrolIsEnabled(true);
			m_txtItem[_index].SetText(num.ToString());
		}
		else
		{
			EzGuiSetting.SetValidEzGui(m_txtItem[_index], false);
			m_btnItem[_index].SetcontrolIsEnabled(false);
		}
	}
}