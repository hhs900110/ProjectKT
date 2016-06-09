using UnityEngine;
using System.Collections;
using DefineBase;

public class EzGui_ProgressBar : EzGui_Base
{
	private UIProgressBar EZGUI_ProgressBar	= null;
	
	
#region ObjectBase
	public override void Release()
	{
		base.Release();
		if (EZGUI_ProgressBar)
		{
			EZGUI_ProgressBar.Delete();
			DestroyImmediate(EZGUI_ProgressBar);
		}
	}
	
	public override void SetValid (bool IsValid)
	{
		base.SetValid (IsValid);
		if(this.gameObject)
		{
			//this.gameObject.active = IsValid;
			this.gameObject.SetActiveRecursively(IsValid);
		}		
		if (EZGUI_ProgressBar)
		{
			EZGUI_ProgressBar.enabled = IsValid;
		}
	}
#endregion
	
#region EzGui_Base
	public override void SetTextureSize (float SizeX, float SizeY)
	{
		base.SetTextureSize (SizeX, SizeY);
		EZGUI_ProgressBar.width		= SizeX;
		EZGUI_ProgressBar.height	= SizeY;
	}
	
//	public virtual void SetTextureSize(float SizeX, float SizeY)
//	{
//		if(Name == "PROGRESS_USER_EXP")
//		{
//			Debug.Log(SizeY);
//		}
//		TextureSizeX	=	SizeX;
//		TextureSizeY	=	SizeY;
//	}
#endregion
	
	//////////////////////////////////////////////////
	
	public void SetEZGUI(UIProgressBar EZGUI)
	{
		EZGUI_ProgressBar			= EZGUI;
		EZGUI_ProgressBar.enabled	= false;
		SetValid(false);
	}
	
	public UIProgressBar GetUIProgressBar()	{ return EZGUI_ProgressBar; }
	
	public void SetValueChangedDelegate(EZValueChangedDelegate del)	{ EZGUI_ProgressBar.SetValueChangedDelegate(del); }
	public void SetAnchor(SpriteRoot.ANCHOR_METHOD _Ancor)			{ EZGUI_ProgressBar.anchor				= _Ancor; }
	public void SetcontrolIsEnabled(bool IsValid)					{ EZGUI_ProgressBar.controlIsEnabled	= IsValid; }
	public bool GetcontrolIsEnabled()								{ return EZGUI_ProgressBar.controlIsEnabled; }
	
	public void SetValue(float ObjectValue)	{ EZGUI_ProgressBar.Value	= ObjectValue; }
	public void SetText(string ObjectText)	{ EZGUI_ProgressBar.Text	= ObjectText; }
}
