using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DefineBase;

public class HudPopupFrameManager : HudBase
{
	private EzGui_Texture	m_imgPopupBoard;
	private EzGui_Texture	m_imgPopupBackground;
	private EzGui_Button	m_btnPopupExit;
	
	private EzGui_Texture	m_imgPopupIcon;
	private EzGui_Texture	m_imgPopupTitle;
	
	private int				m_dPopupState;
	
	private float			m_fStartPosX;
	private float			m_fStartPosY;
	private float			m_fFinishPosX;
	private float			m_fFinishPosY;
	private float			m_bBaseSizeX;
	private float			m_bBaseSizeY;
	
	private bool			m_bAnimation;
	private float			m_fTotalDeltaTime;
	private float			m_fDeltaTime;
	
#region IClassBase
	public override void Create()
	{
		base.Create();
		
		m_fFinishPosX	= DefineBaseManager.inst.GetGameWidth()/2;
		m_fFinishPosY	= DefineBaseManager.inst.GetGameHeight()/2 - 44.0f;
		
		m_bBaseSizeX	= -1;
		m_bBaseSizeY	= -1;
	}
	
	public override void SetValid(bool IsValid)
	{
		base.SetValid(IsValid);
		
		EzGuiSetting.SetValidEzGui(m_imgPopupBoard, IsValid);
		EzGuiSetting.SetValidEzGui(m_imgPopupBackground, IsValid);
		EzGuiSetting.SetValidEzGui(m_btnPopupExit, IsValid);
		EzGuiSetting.SetValidEzGui(m_imgPopupIcon, IsValid);
		EzGuiSetting.SetValidEzGui(m_imgPopupTitle, IsValid);
	}
	
	public override void Update()
	{
		if(!IsUpdate())
		{ return ; }
		base.Update();
		
		if(m_bAnimation)
		{
			m_fDeltaTime	+= Time.smoothDeltaTime;
			
			SetBoardAnimation(m_fDeltaTime/m_fTotalDeltaTime);
			
			if(m_fDeltaTime >= m_fTotalDeltaTime)
			{
				SetBoardAnimation(1.0f);
				EzGuiSetting.SetValidEzGui(m_imgPopupIcon, true);
				EzGuiSetting.SetValidEzGui(m_imgPopupTitle, true);
				EzGuiSetting.SetValidEzGui(m_btnPopupExit, true);
				m_bAnimation	= false;
			}
		}
	}
#endregion
	
#region Resource Setting
	protected override void LoadResource()
	{
		if(m_imgPopupBoard == null)
		{
			SetResourceManagerCreateWithXml("HudPopup");
			
			m_imgPopupBoard			= GetResource("TEXTURE_Popup_Board").GetComponent<EzGui_Texture>();
			m_imgPopupBackground	= GetResource("TEXTURE_Popup_Background").GetComponent<EzGui_Texture>();
			m_btnPopupExit			= GetResource("BUTTON_Popup_Exit").GetComponent<EzGui_Button>();
		}
		if(m_dPopupState >= 0 && m_dPopupState < (int)POPUP_STATE._MAX)
		{
			EzGuiSetting.SetValidEzGui(m_imgPopupIcon, false);
			EzGuiSetting.SetValidEzGui(m_imgPopupTitle, false);
			m_imgPopupIcon			= GetResource(string.Format("{0}{1}", "TEXTURE_Popup_Icon", m_dPopupState)).GetComponent<EzGui_Texture>();
			m_imgPopupTitle			= GetResource(string.Format("{0}{1}", "TEXTURE_Popup_Title", m_dPopupState)).GetComponent<EzGui_Texture>();
		}
	}
	
	protected override void SetResources()
	{
		m_imgPopupBoard.SetAnchor(SpriteRoot.ANCHOR_METHOD.MIDDLE_CENTER);
		m_imgPopupBackground.SetAnchor(SpriteRoot.ANCHOR_METHOD.MIDDLE_CENTER);
		
		m_imgPopupIcon.SetAnchor(SpriteRoot.ANCHOR_METHOD.MIDDLE_LEFT);
		m_imgPopupTitle.SetAnchor(SpriteRoot.ANCHOR_METHOD.MIDDLE_LEFT);
		m_btnPopupExit.SetAnchor(SpriteRoot.ANCHOR_METHOD.MIDDLE_LEFT);
		
		if(m_bBaseSizeX == -1)
		{
			m_bBaseSizeX	= m_imgPopupBoard.GetTextureSizeX();
			m_bBaseSizeY	= m_imgPopupBoard.GetTextureSizeY();
		}
		
		m_btnPopupExit.SetValueChangedDelegate(ExitProcess);
		
		if(m_bAnimation)
		{
			SetBoardAnimation(0.0f);
		}
		else
		{
			m_imgPopupBackground.SetTextureSize(m_bBaseSizeX - 20.0f, m_bBaseSizeY - 90.0f);
		}
	}
	
	public override void ReleaseResource()
	{
		m_bBaseSizeX	= -1;
		m_bBaseSizeY	= -1;
		
		EzGuiSetting.ReleaseEzGui(m_imgPopupBoard);
		EzGuiSetting.ReleaseEzGui(m_imgPopupBackground);
		EzGuiSetting.ReleaseEzGui(m_btnPopupExit);
		ReleaseResourceObject();
	}
	
	public override void SetBasePos()
	{
		float BasePosX	= 0.0f;
		float BasePosY	= 44.0f;
		
		if(m_bAnimation)
		{
//			SetPosEzGui(m_imgPopupBoard, m_fStartPosX, m_fStartPosY);
//			SetPosEzGui(m_imgPopupBackground, m_fStartPosX, m_fStartPosY);
		}
		else
		{
			EzGuiSetting.SetPosEzGui(m_imgPopupBoard, BasePosX, BasePosY);
			EzGuiSetting.SetPosEzGui(m_imgPopupBackground, BasePosX, BasePosY - 35.0f);
		}
		
		EzGuiSetting.SetPosEzGui(m_imgPopupIcon, BasePosX + 217.0f, BasePosY + 225.0f);
		EzGuiSetting.SetPosEzGui(m_imgPopupTitle, BasePosX + 150.0f, BasePosY + 225.0f);
		EzGuiSetting.SetPosEzGui(m_btnPopupExit, BasePosX - 180.0f, BasePosY + 225.0f);
	}
	
	private void SetBoardAnimation(float _Delta)
	{
		m_imgPopupBoard.SetTextureSize(m_bBaseSizeX * _Delta, m_bBaseSizeY * _Delta);
		m_imgPopupBackground.SetTextureSize((m_bBaseSizeX - 20.0f) * _Delta, (m_bBaseSizeY - 90.0f) * _Delta);
		
		m_imgPopupBoard.SetPosX(m_fStartPosX - (m_fStartPosX - m_fFinishPosX)*_Delta);
		m_imgPopupBoard.SetPosY(m_fStartPosY - (m_fStartPosY - m_fFinishPosY)*_Delta);
		m_imgPopupBackground.SetPosX(m_fStartPosX - (m_fStartPosX - m_fFinishPosX)*_Delta);
		m_imgPopupBackground.SetPosY(m_fStartPosY - (m_fStartPosY - m_fFinishPosY - 35.0f)*_Delta);
	}
#endregion
	
#region SetPopup/ClosePopup
	public void SetPopup(int PopupState, float PosX, float PosY)
	{
		m_dPopupState	= PopupState;
		m_bAnimation	= true;
		m_fStartPosX	= PosX;
		m_fStartPosY	= PosY;
		m_fDeltaTime	= 0.0f;
		m_fTotalDeltaTime	= 0.5f;
		base.SetPopup();
		EzGuiSetting.SetValidEzGui(m_imgPopupIcon, false);
		EzGuiSetting.SetValidEzGui(m_imgPopupTitle, false);
		EzGuiSetting.SetValidEzGui(m_btnPopupExit, false);
	}
	
	public void SetPopup(int PopupState)
	{
		m_dPopupState	= PopupState;
		m_bAnimation	= false;
		base.SetPopup();
	}
	
	public override void SetPopup()
	{
	}
	
	public override void ClosePopup()
	{
		m_bAnimation	= false;
		base.ClosePopup();
	}
#endregion
	
#region Delegate
	private void ExitProcess(IUIObject CurObject)
	{
		ClosePopup();
	}
#endregion
}