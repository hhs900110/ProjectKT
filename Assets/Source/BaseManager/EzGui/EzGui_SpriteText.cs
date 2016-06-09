using UnityEngine;
using System.Collections;
using DefineBase;

public class EzGui_SpriteText : EzGui_Base
{
	public SpriteText EZGUI_SpriteText	= null;
	
	////////// ========== Object Base ========== //////////
	
	public override void Create()
	{
		Valid = false;
		Name = null;
		TextureSizeX = 0;
		TextureSizeY = 0;
		TextureRect = new Rect(0.0f, 0.0f, 0.0f, 0.0f);
		base.Create();
	}
	
	public override void SetValid(bool IsValid)
	{
		if(this.gameObject)
		{
			this.gameObject.active = IsValid;
		}
		enabled = IsValid;
		Valid = IsValid;
		if (EZGUI_SpriteText)
		{
			EZGUI_SpriteText.enabled = IsValid;
		}
	}
	
	public override void Release()
	{
		base.Release();
		if(EZGUI_SpriteText)
		{
			EZGUI_SpriteText.Delete();
			DestroyImmediate(EZGUI_SpriteText);
		}
	}
	
	////////// ========== EzGui Base - virtual ========== //////////
	
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
	
	public void SetEZGUI(SpriteText EZGUI)
	{
		EZGUI_SpriteText			= EZGUI;
		EZGUI_SpriteText.enabled	= false;
		SetValid(false);
	}
	
	public SpriteText GetSpriteText()	{ return EZGUI_SpriteText; }
	
	public void SetAnchor(SpriteText.Anchor_Pos ObjectAnchor)
	{
		EZGUI_SpriteText.SetAnchor(ObjectAnchor);
	}
	
	public void SetAnchor(int ObjectAnchor)
	{
		EZGUI_SpriteText.SetAnchor((SpriteText.Anchor_Pos)ObjectAnchor);
	}
	
	public void SetAnchor(string ObjectAnchor)
	{
		if(ObjectAnchor == "Upper_Center")
		{
			EZGUI_SpriteText.SetAnchor(SpriteText.Anchor_Pos.Upper_Center);
		}
		else if(ObjectAnchor == "Upper_Left")
		{
			EZGUI_SpriteText.SetAnchor(SpriteText.Anchor_Pos.Upper_Left);
		}
		else if(ObjectAnchor == "Upper_Right")
		{
			EZGUI_SpriteText.SetAnchor(SpriteText.Anchor_Pos.Upper_Right);
		}
		else if(ObjectAnchor == "Middle_Center")
		{
			EZGUI_SpriteText.SetAnchor(SpriteText.Anchor_Pos.Middle_Center);
		}
		else if(ObjectAnchor == "Middle_Left")
		{
			EZGUI_SpriteText.SetAnchor(SpriteText.Anchor_Pos.Middle_Left);
		}
		else if(ObjectAnchor == "Middle_Right")
		{
			EZGUI_SpriteText.SetAnchor(SpriteText.Anchor_Pos.Middle_Right);
		}
		else if(ObjectAnchor == "Lower_Center")
		{
			EZGUI_SpriteText.SetAnchor(SpriteText.Anchor_Pos.Lower_Center);
		}
		else if(ObjectAnchor == "Lower_Left")
		{
			EZGUI_SpriteText.SetAnchor(SpriteText.Anchor_Pos.Lower_Left);
		}
		else if(ObjectAnchor == "Lower_Right")
		{
			EZGUI_SpriteText.SetAnchor(SpriteText.Anchor_Pos.Lower_Right);
		}
	}
	
	public void SetText(string ObjectText)
	{
		EZGUI_SpriteText.Text	= ObjectText;
	}
	
	public void SetText(float ObjectText)
	{
		SetText(ObjectText.ToString());
	}
	
	public void SetText(int ObjectText)
	{
		SetText(ObjectText.ToString());
	}
	
	public void SetAllignment(int ObjectAllignment)
	{
		EZGUI_SpriteText.SetAlignment((SpriteText.Alignment_Type)ObjectAllignment);
	}
		
	public void SetAllignment(SpriteText.Alignment_Type ObjectAllignment)
	{
		EZGUI_SpriteText.SetAlignment(ObjectAllignment);
	}
	
	public void SetAllignment(string ObjectAllignment)
	{
		if(ObjectAllignment == "Center")
		{
			EZGUI_SpriteText.SetAlignment(SpriteText.Alignment_Type.Center);
		}
		else if(ObjectAllignment == "Left")
		{
			EZGUI_SpriteText.SetAlignment(SpriteText.Alignment_Type.Left);
		}
		else if(ObjectAllignment == "Right")
		{
			EZGUI_SpriteText.SetAlignment(SpriteText.Alignment_Type.Right);
		}
	}
	
	public override void SetColor(Color ObjectColorFront)
	{
		EZGUI_SpriteText.Color	= ObjectColorFront;
	}
	
	public void SetFontSize(float FontSize)
	{
		EZGUI_SpriteText.characterSize	= FontSize;
	}
	
	public void SetTextMaxWidth(float MaxWidth)
	{
		EZGUI_SpriteText.maxWidth	= MaxWidth;
	}
	
	public string GetText()	{ return EZGUI_SpriteText.Text; }
	public float GetFontSize()	{ return EZGUI_SpriteText.characterSize; }
}
