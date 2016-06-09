using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DefineBase;

public sealed class HudScorePopup : HudBase
{
	private EzGui_Texture		m_imgScoreBoard;
	private EzGui_Texture		m_imgBackground;
	private EzGui_Button		m_btnGoReady;
	private EzGui_Button		m_btnGoMain;
	private HudNumberManager	m_scriptScore;
	
#region IClassBase
	public override void Create()
	{
		base.Create();
	}
	
	public override void SetValid(bool IsValid)
	{
		base.SetValid(IsValid);
		
		if(m_scriptScore)
		{
			m_scriptScore.SetValid(IsValid);
		}
		EzGuiSetting.SetValidEzGui(m_imgScoreBoard, IsValid);
		EzGuiSetting.SetValidEzGui(m_imgBackground, IsValid);
		EzGuiSetting.SetValidEzGui(m_btnGoReady, IsValid);
		
		int PlatformType	= DefineBaseManager.inst.GetPlatformType();
		if(PlatformType == (int)PLATFORM_TYPE.PLATFORM_ANDROID
		|| PlatformType == (int)PLATFORM_TYPE.PLATFORM_IPHONE)
		{
			EzGuiSetting.SetValidEzGui(m_btnGoMain, IsValid);
		}
		else
		{
			EzGuiSetting.SetValidEzGui(m_btnGoMain, false);
		}
	}
	
	public override void Update()
	{
		if(!IsUpdate())
		{ return ; }
		base.Update();
	}
#endregion
	
#region Resource Setting
	protected override void LoadResource()
	{
		SetResourceManagerCreateWithXml("HudScorePopup");
		
		m_imgScoreBoard	= GetResource("TEXTURE_ScorePopup_BoardScore").GetComponent<EzGui_Texture>();
		m_imgBackground	= GetResource("TEXTURE_ScorePopup_Background").GetComponent<EzGui_Texture>();
		
		m_btnGoReady	= GetResource("BUTTON_ScorePopup_Regame").GetComponent<EzGui_Button>();
		m_btnGoMain		= GetResource("BUTTON_ScorePopup_GoMain").GetComponent<EzGui_Button>();
	}
	
	protected override void SetResources()
	{
		GameObject tmpObject	= new GameObject("Score");
		m_scriptScore			= tmpObject.AddComponent<HudNumberManager>();
		m_scriptScore.Create();
		m_scriptScore.color			= Color.white;
		m_scriptScore.BasePosType	= (int)HUD_BASE_POS._MIDDLE_CENTER;
		m_scriptScore.ChildAnchor	= SpriteRoot.ANCHOR_METHOD.UPPER_CENTER;
		m_scriptScore.IsCommaOn		= true;
		SetScore(0);
		
		m_imgScoreBoard.SetAnchor(SpriteRoot.ANCHOR_METHOD.MIDDLE_CENTER);
		m_imgBackground.SetAnchor(SpriteRoot.ANCHOR_METHOD.MIDDLE_CENTER);
		m_btnGoReady.SetAnchor(SpriteRoot.ANCHOR_METHOD.UPPER_CENTER);
		m_btnGoMain.SetAnchor(SpriteRoot.ANCHOR_METHOD.UPPER_CENTER);
		
		m_imgBackground.SetTextureSize(DefineBaseManager.inst.GameBaseWidth, DefineBaseManager.inst.GameBaseHeight);
		
		m_btnGoReady.SetValueChangedDelegate(RegameProcess);
		m_btnGoMain.SetValueChangedDelegate(GoMainProcess);
	}
	
	public override void ReleaseResource()
	{
		if(m_scriptScore)
		{
			m_scriptScore.Release();
			GameObject.DestroyImmediate(m_scriptScore.gameObject);
		}
		EzGuiSetting.ReleaseEzGui(m_imgScoreBoard);
		EzGuiSetting.ReleaseEzGui(m_imgBackground);
		EzGuiSetting.ReleaseEzGui(m_btnGoReady);
		EzGuiSetting.ReleaseEzGui(m_btnGoMain);
		
		ReleaseResourceObject();
	}
	
	public override void SetBasePos()
	{
		if(m_scriptScore)
		{
			m_scriptScore.SetPos(0.0f, 60.0f);
		}
		
		int PlatformType	= DefineBaseManager.inst.GetPlatformType();
		if(PlatformType == (int)PLATFORM_TYPE.PLATFORM_ANDROID
		|| PlatformType == (int)PLATFORM_TYPE.PLATFORM_IPHONE)
		{
			EzGuiSetting.SetPosEzGui(m_btnGoReady, -120.0f, -40.0f);
			EzGuiSetting.SetPosEzGui(m_btnGoMain,   120.0f, -40.0f);
		}
		else
		{
			EzGuiSetting.SetPosEzGui(m_btnGoReady, 0.0f, -40.0f);
			EzGuiSetting.SetPosEzGui(m_btnGoMain, 0.0f, -40.0f);
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
	private void GoMainProcess(IUIObject CurObject)
	{
		ClosePopup();
		SendMessage((int)MSG_TYPE._PLAY, (int)PLAY_STATE._MAIN);
	}
	
	private void RegameProcess(IUIObject CurObject)
	{
		int PlatformType	= DefineBaseManager.inst.GetPlatformType();
		if(PlatformType == (int)PLATFORM_TYPE.PLATFORM_ANDROID
		|| PlatformType == (int)PLATFORM_TYPE.PLATFORM_IPHONE)
		{
			ClosePopup();
			SendMessage((int)MSG_TYPE._PLAY, (int)PLAY_STATE._READY);
		}
		else
		{
			Main.game.SetRePopup();
		}
	}
#endregion
	
	public void SetScore(int _Score)
	{
		if(m_scriptScore)
		{
			m_scriptScore.SetNumber(_Score);
		}
	}
}