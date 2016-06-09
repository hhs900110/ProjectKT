using UnityEngine;
using System.Collections;
using DefineBase;

public class EzGui_RadioButton : EzGui_Base
{
	protected int		NowButtonState;
	
	private UIRadioBtn EZGUI_RadioButton;
	
	private bool IsMouseOver;
	
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
		if (EZGUI_RadioButton)
		{
			EZGUI_RadioButton.enabled = IsValid;
		}
	}
	
	public override void Release()
	{
		base.Release();
		if (EZGUI_RadioButton)
		{
			EZGUI_RadioButton.Delete();
			DestroyImmediate(EZGUI_RadioButton);
		}
	}
#endregion
	
#region EzGui_Base
	public override void SetTextureSize(float SizeX, float SizeY)
	{
		base.SetTextureSize(SizeX, SizeY);
		
		EZGUI_RadioButton.width  = SizeX;
		EZGUI_RadioButton.height = SizeY;
	}
	
	public override void SetTextureSizeX(float SizeX)
	{
		base.SetTextureSizeX(SizeX);
		EZGUI_RadioButton.width		= SizeX;
	}
	
	public override void SetTextureSizeY(float SizeY)
	{
		base.SetTextureSizeX(SizeY);
		EZGUI_RadioButton.height	= SizeY;
	}
#endregion
	
	//////////////////////////////////////////////////
	
	public void SetEZGUI(UIRadioBtn EZGUI)
	{
		EZGUI_RadioButton						= EZGUI;
		EZGUI_RadioButton.useParentForGrouping	= false;
		EZGUI_RadioButton.SetAnchor(0);
		SetValid(false);
	} 
	
	public UIRadioBtn GetUIRadioButton()	{ return EZGUI_RadioButton; }
	
	public void SetInputDelegate(EZInputDelegate del)				{ EZGUI_RadioButton.SetInputDelegate(del); }
	public void SetValueChangedDelegate(EZInputDelegate del)		{ EZGUI_RadioButton.SetInputDelegate(del); }
	public void SetInputDelegate(EZValueChangedDelegate del)		{ EZGUI_RadioButton.SetValueChangedDelegate(del); }
	public void SetValueChangedDelegate(EZValueChangedDelegate del)	{ EZGUI_RadioButton.SetValueChangedDelegate(del); }
	public void SetGroup(GameObject _Group)							{ EZGUI_RadioButton.SetGroup(_Group); }
	public void SetGroup(Transform _Group)							{ EZGUI_RadioButton.SetGroup(_Group); }
	public void SetGroup(int _Group)								{ EZGUI_RadioButton.SetGroup(_Group); }
	public void SetAnchor(SpriteRoot.ANCHOR_METHOD _Ancor)			{ EZGUI_RadioButton.anchor				= _Ancor; }
	public void SetWhenToInvoke(POINTER_INFO.INPUT_EVENT Type)		{ EZGUI_RadioButton.whenToInvoke		= Type; }
	public void SetcontrolIsEnabled(bool IsValid)					{ EZGUI_RadioButton.controlIsEnabled	= IsValid; }
	public bool GetcontrolIsEnabled()								{ return EZGUI_RadioButton.controlIsEnabled; }
	
	public void SetMouseOver(bool over)								{ IsMouseOver = over; }
	public bool IsTextureHit()										{ return IsMouseOver; }
	
	public void SetNowButtonState(int State)
	{
		NowButtonState	= State;
		EZGUI_RadioButton.SetState(State);
		if(State == (int)UIButton.CONTROL_STATE.DISABLED)
		{
			SetcontrolIsEnabled(false);
		}
		else
		{
			SetcontrolIsEnabled(true);
		}
	}
	
	public int GetNowButtonState()	{ return NowButtonState; }
}
