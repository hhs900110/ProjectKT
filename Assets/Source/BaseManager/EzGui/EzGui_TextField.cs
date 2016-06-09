using UnityEngine;
using System.Collections;
using DefineBase;

public class EzGui_TextField : EzGui_Base 
{
	private UITextField EZGUI_TextField;
	
	////////// ========== Object Base ========== //////////
	
	public override void Release()
	{
		base.Release();
		if (EZGUI_TextField)
		{
			EZGUI_TextField.Delete();
			DestroyImmediate(EZGUI_TextField);
		}
	}
	
	public override void SetValid (bool IsValid)
	{
		base.SetValid (IsValid);
		if(this.gameObject)
		{
			this.gameObject.active = IsValid;
		}
		if (EZGUI_TextField)
		{
			EZGUI_TextField.enabled = IsValid;
		}
	}
	
	public void SetEZGUI(UITextField EZGUI)
	{
		EZGUI_TextField	= EZGUI;
		//EZGUI_TextField.SetAnchor(0);
		EZGUI_TextField.enabled	= false;
		SetValid(false);
	}
	
	public UITextField GetUITextField()	{ return EZGUI_TextField; }
	
	public override void SetColor(Color ObjectColor)
	{
		EZGUI_TextField.spriteText.Color = ObjectColor;
		//this.GetComponentInChildren<SpriteText>().Color = ObjectColor;
	}
	
#if UNITY_IPHONE || UNITY_ANDROID
	public void SetAlert(bool b)					{ EZGUI_TextField.alert				= b; }
	public void SetAutoCorrect(bool b)				{ EZGUI_TextField.autoCorrect		= b; }
	public void SetHideInput(bool b)				{ EZGUI_TextField.hideInput			= b; }
	public void SetType(TouchScreenKeyboardType t)	{ EZGUI_TextField.type				= t; }
#endif
	
	public void SetCommitOnLostFocus(bool b)		{ EZGUI_TextField.commitOnLostFocus	= b; }
	public void SetMaxLength(int f)					{ EZGUI_TextField.maxLength			= f; }
	public void SetMultiLine(bool b)				{ EZGUI_TextField.multiline			= b; }
	public void SetMaskingCharacter(string text)	{ EZGUI_TextField.maskingCharacter	= text; }
	public void SetPassWord(bool b)					{ EZGUI_TextField.password			= b; }
	public void SetShowCaretOnMobile(bool b)		{ EZGUI_TextField.showCaretOnMobile	= b; }
	
	public void SetcontrolIsEnabled(bool IsValid)	{ EZGUI_TextField.controlIsEnabled	= IsValid; }
	public bool GetcontrolIsEnabled()				{ return EZGUI_TextField.controlIsEnabled; }
	
	public void SetSize(float x, float y)			{ EZGUI_TextField.SetSize(x, y); }
	
	public Vector2 GetImageSize()					{ return EZGUI_TextField.ImageSize; }
	
	public void SetFocusDelegate(UITextField.FocusDelegate del)		{ EZGUI_TextField.SetFocusDelegate(del); }
	public void SetValueChangedDelegate(EZValueChangedDelegate del)	{ EZGUI_TextField.SetValueChangedDelegate(del); }
}
