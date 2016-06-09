using UnityEngine;
using System.Collections;
using DefineBase;

#pragma warning disable 0168 // variable declared but not used.
#pragma warning disable 0219 // variable assigned but not used.
#pragma warning disable 0414 // private field assigned but not used.

public class EzGui_Button_Outline : EzGui_Base
{
	protected int		NowButtonState;
	
	private UIButton	EZGUI_Button;
	private SpriteText	EZGUI_SpriteTextFront	= null;
	private SpriteText	EZGUI_SpriteTextBack	= null;
	
	private bool		SizeChangeState	= true;
	
	
#region ObjectBase
	public override void Create ()
	{
		base.Create ();
		
		NowButtonState = (int)MOUSE_BUTTON_TYPE._BASE;
		
		SetValid(false);
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
		if (EZGUI_SpriteTextFront)
		{
			EZGUI_SpriteTextFront.enabled = IsValid;
		}
		if (EZGUI_SpriteTextBack)
		{
			EZGUI_SpriteTextBack.enabled = IsValid;
		}
	}
	
	public override void Release()
	{
		base.Release();
		if (EZGUI_SpriteTextFront)
		{
			EZGUI_SpriteTextFront.Delete();
			DestroyImmediate(EZGUI_SpriteTextFront);
		}
		if (EZGUI_SpriteTextBack)
		{
			EZGUI_SpriteTextBack.Delete();
			DestroyImmediate(EZGUI_SpriteTextBack);
		}
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
	
	public void SetEZGUI(UIButton EZGUI, SpriteText EZGUIFront, SpriteText EZGUIBack)
	{
		EZGUI_Button	= EZGUI;
		EZGUI_Button.SetAnchor(0);
		
		EZGUI_SpriteTextFront			= EZGUIFront;
		EZGUI_SpriteTextFront.enabled	= false;
		
		EZGUI_SpriteTextBack			= EZGUIBack;
		EZGUI_SpriteTextBack.enabled	= false;
		SetValid(false);
	} 
	
	public UIButton GetUIButton()			{ return EZGUI_Button; }
	public SpriteText GetSpriteTextFront()	{ return EZGUI_SpriteTextFront; }
	public SpriteText GetSpriteTextBack()	{ return EZGUI_SpriteTextBack; }
	
	public void SetMultiLine(bool _IsValue)
	{
		EZGUI_SpriteTextFront.multiline	= _IsValue;
		EZGUI_SpriteTextBack.multiline	= _IsValue;
	}
	
	public void SetValueChangedDelegate(EZValueChangedDelegate del)	{ EZGUI_Button.SetValueChangedDelegate(del); }
	public void SetData(Object _Data)								{ EZGUI_Button.data						= _Data; }
	public void SetData(int _Data)									{ EZGUI_Button.data						= _Data; }
	public void SetscriptWithMethodToInvoke(MonoBehaviour _Script)	{ EZGUI_Button.scriptWithMethodToInvoke	= _Script; }
	public void SetWhenToInvoke(POINTER_INFO.INPUT_EVENT _Type)		{ EZGUI_Button.whenToInvoke				= _Type; }
	public void SetcontrolIsEnabled(bool _IsValid)					{ EZGUI_Button.controlIsEnabled			= _IsValid; }
	public bool GetcontrolIsEnabled()								{ return EZGUI_Button.controlIsEnabled; }
	
	public void SetAnchor(SpriteRoot.ANCHOR_METHOD _Ancor)
	{
		EZGUI_Button.anchor				= _Ancor;
	}
	
	public void SetAnchor(SpriteText.Anchor_Pos ObjectAnchor)
	{
		EZGUI_SpriteTextFront.SetAnchor(ObjectAnchor);
		EZGUI_SpriteTextBack.SetAnchor(ObjectAnchor);
	}
	
	public void SetAnchor(int ObjectAnchor)
	{
		EZGUI_Button.anchor				= (SpriteRoot.ANCHOR_METHOD)ObjectAnchor;
		EZGUI_SpriteTextFront.SetAnchor((SpriteText.Anchor_Pos)ObjectAnchor);
		EZGUI_SpriteTextBack.SetAnchor((SpriteText.Anchor_Pos)ObjectAnchor);
	}
	
	public void SetAnchor(string ObjectAnchor)
	{
		if(ObjectAnchor == "Upper_Center")
		{
			SetAnchor(SpriteText.Anchor_Pos.Upper_Center);
		}
		else if(ObjectAnchor == "Upper_Left")
		{
			SetAnchor(SpriteText.Anchor_Pos.Upper_Left);
		}
		else if(ObjectAnchor == "Upper_Right")
		{
			SetAnchor(SpriteText.Anchor_Pos.Upper_Right);
		}
		else if(ObjectAnchor == "Middle_Center")
		{
			SetAnchor(SpriteText.Anchor_Pos.Middle_Center);
		}
		else if(ObjectAnchor == "Middle_Left")
		{
			SetAnchor(SpriteText.Anchor_Pos.Middle_Left);
		}
		else if(ObjectAnchor == "Middle_Right")
		{
			SetAnchor(SpriteText.Anchor_Pos.Middle_Right);
		}
		else if(ObjectAnchor == "Lower_Center")
		{
			SetAnchor(SpriteText.Anchor_Pos.Lower_Center);
		}
		else if(ObjectAnchor == "Lower_Left")
		{
			SetAnchor(SpriteText.Anchor_Pos.Lower_Left);
		}
		else if(ObjectAnchor == "Lower_Right")
		{
			SetAnchor(SpriteText.Anchor_Pos.Lower_Right);
		}
	}
	
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
	
	public int GetNowButtonState ()	{ return (int)EZGUI_Button.controlState; }
	
	public void SetAllignment(int ObjectAllignment)
	{
		EZGUI_SpriteTextFront.SetAlignment((SpriteText.Alignment_Type)ObjectAllignment);
		EZGUI_SpriteTextBack.SetAlignment((SpriteText.Alignment_Type)ObjectAllignment);
	}
	
	public void SetAllignment(SpriteText.Alignment_Type ObjectAllignment)
	{
		EZGUI_SpriteTextFront.SetAlignment(ObjectAllignment);
		EZGUI_SpriteTextBack.SetAlignment(ObjectAllignment);
	}
	
	public void SetAllignment(string ObjectAllignment)
	{
		if(ObjectAllignment == "Center")
		{
			EZGUI_SpriteTextFront.SetAlignment(SpriteText.Alignment_Type.Center);
			EZGUI_SpriteTextBack.SetAlignment(SpriteText.Alignment_Type.Center);
		}
		else if(ObjectAllignment == "Left")
		{
			EZGUI_SpriteTextFront.SetAlignment(SpriteText.Alignment_Type.Left);
			EZGUI_SpriteTextBack.SetAlignment(SpriteText.Alignment_Type.Left);
		}
		else if(ObjectAllignment == "Right")
		{
			EZGUI_SpriteTextFront.SetAlignment(SpriteText.Alignment_Type.Right);
			EZGUI_SpriteTextBack.SetAlignment(SpriteText.Alignment_Type.Right);
		}
	}
	
	public void SetColor(Color ObjectColorFront, Color ObjectColorBack)
	{
		EZGUI_SpriteTextFront.Color	= ObjectColorFront;
		EZGUI_SpriteTextBack.Color	= ObjectColorBack;
	}
	
	public Color GetFrontColor()		{ return EZGUI_SpriteTextFront.Color; }
	public Color GetBackColor()			{ return EZGUI_SpriteTextBack.Color; }
	
	public override void SetAlpha(float _Alpha)
	{
		base.SetAlpha(_Alpha);
		
		Color FrontColor	= GetFrontColor();
		Color BackColor		= GetBackColor();
		
		FrontColor.a	= _Alpha;
		BackColor.a		= _Alpha;
		
		SetColor(FrontColor, BackColor);
	}
	
	public void SetText(string ObjectText)
	{
		EZGUI_SpriteTextFront.Text	= ObjectText;
		EZGUI_SpriteTextBack.Text	= ObjectText;
	}
	
	public void SetFontSize(float FontSize)
	{
		EZGUI_SpriteTextFront.characterSize	= FontSize;
		EZGUI_SpriteTextBack.characterSize	= FontSize;
	}

	public void SetTextMaxWidth(float MaxWidth)
	{
		EZGUI_SpriteTextFront.maxWidth	= MaxWidth;
		EZGUI_SpriteTextBack.maxWidth	= MaxWidth;
	}
	
	public void SetFontPos(float PosX, float PosY)
	{
		EZGUI_SpriteTextFront.transform.localPosition	= new Vector3(PosX, PosY, -2);
		EZGUI_SpriteTextBack.transform.localPosition	= new Vector3(PosX, PosY, -1);
	}
	
	public string GetText()	{ return EZGUI_SpriteTextFront.Text; }
	public float GetFontSize()	{ return EZGUI_SpriteTextFront.characterSize; }
}
