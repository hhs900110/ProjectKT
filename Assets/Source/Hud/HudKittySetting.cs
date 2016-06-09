using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DefineBase;

public class HudKittySetting : HudBase
{
	private EzGui_Button_Outline		m_btnNowTurnType;
	private EzGui_Button_Outline		m_btnNowTurnSpeed;
	private EzGui_Button_Outline		m_btnNowMapSize;
	
	private EzGui_SpriteText_Outline	m_txtSetTurnSpeed;
	private EzGui_TextField				m_fieldSetTurnSpeed;
	
	private EzGui_SpriteText_Outline	m_txtSetTurnType;
	private EzGui_TextField				m_fieldSetTurnType;
	
	private EzGui_Button_Outline		m_btnSetApply;
	
	private EzGui_SpriteText_Outline	m_txtSetMapX;
	private EzGui_TextField				m_fieldSetMapX;
	
	private EzGui_SpriteText_Outline	m_txtSetMapY;
	private EzGui_TextField				m_fieldSetMapY;
	
	private EzGui_Button_Outline		m_btnSetNewGame;
	
#region ObjectBase
	public override void Create()
	{
		base.Create();
	}
	
	public override void SetValid(bool IsValid)
	{
		base.SetValid(IsValid);
		
		EzGuiSetting.SetValidEzGui(m_btnNowTurnType, IsValid);
		EzGuiSetting.SetValidEzGui(m_btnNowTurnSpeed, IsValid);
		EzGuiSetting.SetValidEzGui(m_btnNowMapSize, IsValid);
		
		EzGuiSetting.SetValidEzGui(m_txtSetTurnSpeed, IsValid);
		EzGuiSetting.SetValidEzGui(m_fieldSetTurnSpeed, IsValid);
		
		EzGuiSetting.SetValidEzGui(m_txtSetTurnType, IsValid);
		EzGuiSetting.SetValidEzGui(m_fieldSetTurnType, IsValid);
		
		EzGuiSetting.SetValidEzGui(m_btnSetApply, IsValid);
		
		EzGuiSetting.SetValidEzGui(m_txtSetMapX, IsValid);
		EzGuiSetting.SetValidEzGui(m_fieldSetMapX, IsValid);
		
		EzGuiSetting.SetValidEzGui(m_txtSetMapY, IsValid);
		EzGuiSetting.SetValidEzGui(m_fieldSetMapY, IsValid);
		
		EzGuiSetting.SetValidEzGui(m_btnSetNewGame, IsValid);
		
		if(IsValid)
		{
			UpdateStageState();
		}
	}
#endregion
	
#region Resource Setting
	protected override void LoadResource()
	{
		SetResourceManagerCreate("Script/Game/HUD/KittyTurnSetting");
		
		m_btnNowTurnType	= GetResource("BUTTON_KittySetting_NowTurnType").GetComponent<EzGui_Button_Outline>();
		m_btnNowTurnSpeed	= GetResource("BUTTON_KittySetting_NowTurnSpeed").GetComponent<EzGui_Button_Outline>();
		m_btnNowMapSize		= GetResource("BUTTON_KittySetting_NowMapSize").GetComponent<EzGui_Button_Outline>();
		
		m_txtSetTurnSpeed	= GetResource("OUTLINE_KittySetting_TurnSpeed").GetComponent<EzGui_SpriteText_Outline>();
		m_fieldSetTurnSpeed	= GetResource("TEXTFIELD_KittySetting_TurnSpeed").GetComponent<EzGui_TextField>();
		
		m_txtSetTurnType	= GetResource("OUTLINE_KittySetting_TurnType").GetComponent<EzGui_SpriteText_Outline>();
		m_fieldSetTurnType	= GetResource("TEXTFIELD_KittySetting_TurnType").GetComponent<EzGui_TextField>();
		
		m_btnSetApply		= GetResource("BUTTON_KittySetting_Apply").GetComponent<EzGui_Button_Outline>();
		
		m_txtSetMapX		= GetResource("OUTLINE_KittySetting_MapX").GetComponent<EzGui_SpriteText_Outline>();
		m_fieldSetMapX		= GetResource("TEXTFIELD_KittySetting_MapX").GetComponent<EzGui_TextField>();
		
		m_txtSetMapY		= GetResource("OUTLINE_KittySetting_MapY").GetComponent<EzGui_SpriteText_Outline>();
		m_fieldSetMapY		= GetResource("TEXTFIELD_KittySetting_MapY").GetComponent<EzGui_TextField>();
		
		m_btnSetNewGame		= GetResource("BUTTON_KittySetting_NewGame").GetComponent<EzGui_Button_Outline>();
	}
	
	protected override void SetResources()
	{
		m_txtSetTurnSpeed.SetAnchor(SpriteText.Anchor_Pos.Lower_Left);
		m_txtSetTurnType.SetAnchor(SpriteText.Anchor_Pos.Lower_Left);
		
		m_txtSetMapX.SetAnchor(SpriteText.Anchor_Pos.Lower_Left);
		m_txtSetMapY.SetAnchor(SpriteText.Anchor_Pos.Lower_Left);
		
#if		UNITY_EDITOR
		TextFieldBaseSetting(m_fieldSetTurnSpeed, 4, Color.white);
		TextFieldBaseSetting(m_fieldSetTurnType, 1, Color.white);
		
		TextFieldBaseSetting(m_fieldSetMapX, 2, Color.white);
		TextFieldBaseSetting(m_fieldSetMapY, 2, Color.white);
#elif	UNITY_IPHONE || UNITY_ANDROID
		TextFieldBaseSetting(m_fieldSetTurnSpeed, TouchScreenKeyboardType.NumberPad, 4, Color.white);
		TextFieldBaseSetting(m_fieldSetTurnType, TouchScreenKeyboardType.NumberPad, 1, Color.white);
		
		TextFieldBaseSetting(m_fieldSetMapX, TouchScreenKeyboardType.NumberPad, 2, Color.white);
		TextFieldBaseSetting(m_fieldSetMapY, TouchScreenKeyboardType.NumberPad, 2, Color.white);
#else
		TextFieldBaseSetting(m_fieldSetTurnSpeed, 4, Color.white);
		TextFieldBaseSetting(m_fieldSetTurnType, 1, Color.white);
		
		TextFieldBaseSetting(m_fieldSetMapX, 2, Color.white);
		TextFieldBaseSetting(m_fieldSetMapY, 2, Color.white);
#endif
		
		ButtonTextBaseSetting(m_btnNowTurnType, SpriteRoot.ANCHOR_METHOD.BOTTOM_RIGHT, "", null);
		ButtonTextBaseSetting(m_btnNowTurnSpeed, SpriteRoot.ANCHOR_METHOD.BOTTOM_RIGHT, "", null);
		ButtonTextBaseSetting(m_btnNowMapSize, SpriteRoot.ANCHOR_METHOD.BOTTOM_RIGHT, "", null);
		
		m_btnNowTurnType.SetTextureSizeX(120.0f);
		m_btnNowTurnType.SetFontPos(-60.0f, 20.0f);
		
		ButtonTextBaseSetting(m_btnSetApply, SpriteRoot.ANCHOR_METHOD.BOTTOM_RIGHT, GameString.strApply, SetApplyProcess);
		ButtonTextBaseSetting(m_btnSetNewGame, SpriteRoot.ANCHOR_METHOD.BOTTOM_RIGHT, GameString.strNewGame, SetNewGameProcess);
		
		m_btnNowTurnType.SetBasePos((int)HUD_BASE_POS._TOP_LEFT);
		m_btnNowTurnSpeed.SetBasePos((int)HUD_BASE_POS._TOP_LEFT);
		m_btnNowMapSize.SetBasePos((int)HUD_BASE_POS._TOP_LEFT);
		m_btnNowTurnType.SetAnchor(SpriteRoot.ANCHOR_METHOD.UPPER_LEFT);
		m_btnNowTurnSpeed.SetAnchor(SpriteRoot.ANCHOR_METHOD.UPPER_LEFT);
		m_btnNowMapSize.SetAnchor(SpriteRoot.ANCHOR_METHOD.UPPER_LEFT);
		m_btnNowTurnType.SetFontPos(60.0f, -20.0f);
		m_btnNowTurnSpeed.SetFontPos(40.0f, -20.0f);
		m_btnNowMapSize.SetFontPos(40.0f, -20.0f);
		
		m_txtSetTurnSpeed.SetBasePos((int)HUD_BASE_POS._BOTTOM_LEFT);
		m_txtSetTurnType.SetBasePos((int)HUD_BASE_POS._BOTTOM_LEFT);
		
		m_txtSetMapX.SetBasePos((int)HUD_BASE_POS._BOTTOM_LEFT);
		m_txtSetMapY.SetBasePos((int)HUD_BASE_POS._BOTTOM_LEFT);
		
		m_fieldSetTurnSpeed.SetBasePos((int)HUD_BASE_POS._BOTTOM_LEFT);
		m_fieldSetTurnType.SetBasePos((int)HUD_BASE_POS._BOTTOM_LEFT);
		
		m_fieldSetMapX.SetBasePos((int)HUD_BASE_POS._BOTTOM_LEFT);
		m_fieldSetMapY.SetBasePos((int)HUD_BASE_POS._BOTTOM_LEFT);
	}
	
	public override void ReleaseResource()
	{
		ReleaseResourceObject();
	}
	
	public override void SetBasePos()
	{
//		float BasePosX	=  0.0f;
		float BasePosY	=  0.0f;
		float GapX		= 65.0f;
		float GapY		= 80.0f;
		
		float SubPosX	= 0.0f;
		float SubPosY	= 0.0f;
		
		GapY	= 40.0f;
		m_btnNowTurnType.SetPos(0.0f, 0.0f);
		m_btnNowTurnSpeed.SetPos(0.0f, GapY);
		m_btnNowMapSize.SetPos(0.0f, GapY*2);
		
		//
		GapY		= 80.0f;
		SubPosY	= BasePosY + 100.0f;
		m_txtSetTurnSpeed.SetPos(SubPosX, SubPosY + 5);
		m_fieldSetTurnSpeed.SetPos(SubPosX, SubPosY);
		
		m_txtSetTurnType.SetPos(SubPosX + GapX, SubPosY + 5);
		m_fieldSetTurnType.SetPos(SubPosX + GapX, SubPosY);
		
		m_btnSetApply.SetPos(0.0f, 50.0f);
		
		//
		SubPosY	= BasePosY + 35.0f;
		m_txtSetMapX.SetPos(SubPosX, SubPosY + 5);
		m_fieldSetMapX.SetPos(SubPosX, SubPosY);
		
		m_txtSetMapY.SetPos(SubPosX + GapX, SubPosY + 5);
		m_fieldSetMapY.SetPos(SubPosX + GapX, SubPosY);
	}
#endregion
	
#region Delegate
	protected void SetApplyProcess(IUIObject CurObject)
	{
		int TurnSpeed	= 0;
		int.TryParse(m_fieldSetTurnSpeed.GetUITextField().text, out TurnSpeed);
		if(TurnSpeed > 0)
		{
			DefineBaseManager.inst.SetKittyTurnSpeed(TurnSpeed);
		}
		
		int TurnType	= 0;
		string TurnText	= m_fieldSetTurnType.GetUITextField().text;
		if(TurnText != "")
		{
			int.TryParse(TurnText, out TurnType);
			if(0 <= TurnType && TurnType < (int)DefineBase.KITTY_TURN_TYPE._MAX)
			{
				DefineBaseManager.inst.SetKittyTurnType(TurnType);
			}
		}
		
		ClearTextField(m_fieldSetTurnSpeed);
		ClearTextField(m_fieldSetTurnType);
		UpdateStageState();
	}
	
	protected void SetNewGameProcess(IUIObject CurObject)
	{
		SetApplyProcess(CurObject);
		
		int MapX		= 0;
		int.TryParse(m_fieldSetMapX.GetUITextField().text, out MapX);
		if(MapX > 0)
		{
			DefineBaseManager.inst.SetKittyMaxMapX(MapX);
		}
		
		int MapY		= 0;
		int.TryParse(m_fieldSetMapY.GetUITextField().text, out MapY);
		if(MapY > 0)
		{
			DefineBaseManager.inst.SetKittyMaxMapY(MapY);
		}
		
		ClearTextField(m_fieldSetMapX);
		ClearTextField(m_fieldSetMapY);
		
		UpdateStageState();
		Main.game.SetRePopup();
	}
#endregion
	
	private void UpdateStageState()
	{
		m_btnNowTurnType.SetText(((KITTY_TURN_TYPE)DefineBaseManager.inst.KittyTurnType).ToString());
		m_btnNowTurnSpeed.SetText((DefineBaseManager.inst.KittyTurnSpeed).ToString());
		m_btnNowMapSize.SetText(string.Format("{0}, {1}", DefineBaseManager.inst.KittyMaxMapX, DefineBaseManager.inst.KittyMaxMapY));
	}
	
	protected void ClearTextField(EzGui_TextField _Field)
	{
		string Log	= "";
		int insert		= Log.Length;
		_Field.GetUITextField().SetInputText(Log, ref insert);
	}
	
	protected void ButtonTextBaseSetting(EzGui_Button_Outline _Button, SpriteRoot.ANCHOR_METHOD _Anchor, string _Text, EZValueChangedDelegate _Del)
	{
		_Button.SetAnchor(_Anchor);
		_Button.SetAnchor(SpriteText.Anchor_Pos.Middle_Center);
		_Button.SetTextureSize(80.0f, 40.0f);
		_Button.SetFontPos(-40.0f, 20.0f);
		_Button.SetText(_Text);
		_Button.SetValueChangedDelegate(_Del);
	}
	
#if		UNITY_EDITOR
	protected void TextFieldBaseSetting(EzGui_TextField _Field, int MaxLength, Color _Color)
	{
		_Field.GetUITextField().maxLength		= MaxLength;
		_Field.SetColor(_Color);
	}
#elif	UNITY_IPHONE
	protected void TextFieldBaseSetting(EzGui_TextField _Field, TouchScreenKeyboardType _Type, int MaxLength, Color _Color)
	{
		_Field.GetUITextField().type			= _Type;
		_Field.GetUITextField().hideInput		= true;
		_Field.GetUITextField().autoCorrect		= false;
		_Field.GetUITextField().alert			= false;
		
		_Field.GetUITextField().maxLength		= MaxLength;
		_Field.SetColor(_Color);
	}
#elif	UNITY_ANDROID
	protected void TextFieldBaseSetting(EzGui_TextField _Field, TouchScreenKeyboardType _Type, int MaxLength, Color _Color)
	{
		_Field.GetUITextField().type			= _Type;
		_Field.GetUITextField().hideInput		= true;
		_Field.GetUITextField().autoCorrect		= false;
		_Field.GetUITextField().alert			= false;
		
		_Field.GetUITextField().maxLength		= MaxLength;
		_Field.SetColor(_Color);
	}
#else
	protected void TextFieldBaseSetting(EzGui_TextField _Field, int MaxLength, Color _Color)
	{
		_Field.GetUITextField().maxLength		= MaxLength;
		_Field.SetColor(_Color);
	}
#endif
}