using UnityEngine;
using System.Collections;
using DefineBase;

#pragma warning disable 0168 // variable declared but not used.
#pragma warning disable 0219 // variable assigned but not used.
#pragma warning disable 0414 // private field assigned but not used.

public class EzGui_Button : EzGui_Base
{
	protected int		NowButtonState;
	
	private UIButton	EZGUI_Button;
	private bool		SizeChangeState	= true;
	
	
#region ObjectBase
	public override void Create ()
	{
		base.Create ();
		
		NowButtonState = (int)MOUSE_BUTTON_TYPE._BASE;
	}
	
	public override void SetValid(bool IsValid)
	{
		base.SetValid(IsValid);
		if(this.gameObject)
		{
			this.gameObject.active = IsValid;
		}
		if (EZGUI_Button)
		{
			EZGUI_Button.enabled = IsValid;
		}
	}
	
	public override void Release()
	{
		base.Release();
		if (EZGUI_Button)
		{
			EZGUI_Button.Delete();
			DestroyImmediate(EZGUI_Button);
		}
	}
#endregion
	
#region EzGui_Base
	public override void SetTextureSize(float SizeX, float SizeY)
	{
		base.SetTextureSize(SizeX, SizeY);
		
		EZGUI_Button.width  = SizeX;
		EZGUI_Button.height = SizeY;
	}
	
	public override void SetTextureSizeX(float SizeX)
	{
		base.SetTextureSizeX(SizeX);
		EZGUI_Button.width  = SizeX;
	}
	
	public override void SetTextureSizeY(float SizeY)
	{
		base.SetTextureSizeX(SizeY);
		EZGUI_Button.height = SizeY;
	}
#endregion
	
	//////////////////////////////////////////////////
	
	public void SetEZGUI(UIButton EZGUI)
	{
		EZGUI_Button	= EZGUI;
		EZGUI_Button.SetAnchor(0);
		SetValid(false);
	} 
	
	public UIButton GetUIButton()	{ return EZGUI_Button; }
	
	public void SetValueChangedDelegate(EZValueChangedDelegate del)	{ EZGUI_Button.SetValueChangedDelegate(del); }
	public void SetAnchor(SpriteRoot.ANCHOR_METHOD _Ancor)			{ EZGUI_Button.anchor					= _Ancor; }
	public void SetData(Object _Data)								{ EZGUI_Button.data						= _Data; }
	public void SetData(int _Data)									{ EZGUI_Button.data						= _Data; }
	public void SetscriptWithMethodToInvoke(MonoBehaviour _Script)	{ EZGUI_Button.scriptWithMethodToInvoke	= _Script; }
	public void SetWhenToInvoke(POINTER_INFO.INPUT_EVENT _Type)		{ EZGUI_Button.whenToInvoke				= _Type; }
	public void SetcontrolIsEnabled(bool _IsValid)					{ EZGUI_Button.controlIsEnabled			= _IsValid; }
	public bool GetcontrolIsEnabled()								{ return EZGUI_Button.controlIsEnabled; }
	
	public bool IsMouseOver()
	{
		if((int)EZGUI_Button.controlState == (int)CONTROL_STATE.OVER)
		{
			return true;
		}
		return false;
	}
	
	public bool IsTextureHit()
	{
		if((int)EZGUI_Button.controlState == (int)CONTROL_STATE.OVER
		|| (int)EZGUI_Button.controlState == (int)CONTROL_STATE.ACTIVE)
		{
			return true;
		}
		return false;
	}
	
	public void SetNowButtonState (int State)
	{
		NowButtonState	= State;
		EZGUI_Button.SetState(State);
		if(State == (int)UIButton.CONTROL_STATE.DISABLED)
		{
			SetcontrolIsEnabled(false);
		}
		else
		{
			SetcontrolIsEnabled(true);
		}
	}
	
	public int GetNowButtonState ()	{ return (int)EZGUI_Button.controlState;}
}
