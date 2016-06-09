using UnityEngine;
using System.Collections;
using DefineBase;

public class EzGui_SpriteText_Outline : EzGui_Base
{
	private SpriteText EZGUI_SpriteTextFront	= null;
	private SpriteText EZGUI_SpriteTextBack		= null;
	
	////////// ========== Object Base ========== //////////
	
	public override void Create()
	{
		base.Create();
		SetValid(false);
	}
	
	public override void SetValid(bool IsValid)
	{
		if(this.gameObject)
		{
			//this.gameObject.active = IsValid;
			this.gameObject.SetActiveRecursively(IsValid);
		}
		base.SetValid(IsValid);
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
	}
	
	////////// ========== EzGui Base ========== //////////
	
	public override void SetPos(float ObjectPosX, float ObjectPosY, float ObjectPosZ)
	{
		base.SetPos(ObjectPosX, ObjectPosY, ObjectPosZ);
		transform.position = Pos;
	}
	
	public override void SetPos(float ObjectPosX, float ObjectPosY)
	{
		base.SetPos(ObjectPosX, ObjectPosY);
		transform.position = Pos;
	}
	
	//////////////////////////////////////////////////
	
	public void SetEZGUI(SpriteText EZGUIFront, SpriteText EZGUIBack)
	{
		EZGUI_SpriteTextFront			= EZGUIFront;
		EZGUI_SpriteTextFront.enabled	= false;
		
		EZGUI_SpriteTextBack			= EZGUIBack;
		EZGUI_SpriteTextBack.enabled	= false;
		SetValid(false);
	}
	
	public SpriteText GetSpriteTextFront()	{ return EZGUI_SpriteTextFront; }
	public SpriteText GetSpriteTextBack()	{ return EZGUI_SpriteTextBack; }
	
	public void SetMultiLine(bool _IsValue)
	{
		EZGUI_SpriteTextFront.multiline	= _IsValue;
		EZGUI_SpriteTextBack.multiline	= _IsValue;
	}
	
	public void SetAnchor(SpriteText.Anchor_Pos ObjectAnchor)
	{
		EZGUI_SpriteTextFront.SetAnchor(ObjectAnchor);
		EZGUI_SpriteTextBack.SetAnchor(ObjectAnchor);
	}
	
	public void SetAnchor(int ObjectAnchor)
	{
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
	
	public string GetText()	{ return EZGUI_SpriteTextFront.Text; }
	public float GetFontSize()	{ return EZGUI_SpriteTextFront.characterSize; }
}
