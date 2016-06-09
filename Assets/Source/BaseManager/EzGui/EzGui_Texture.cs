using UnityEngine;
using System.Collections;
using DefineBase;

#pragma warning disable 0168 // variable declared but not used.
#pragma warning disable 0219 // variable assigned but not used.
#pragma warning disable 0414 // private field assigned but not used.

public class EzGui_Texture : EzGui_Base
{
	private UIButton EZGUI_Texture;
	
	////////// ========== Object Base ========== //////////
	
	public override void SetValid(bool IsValid)
	{
		base.SetValid(IsValid);
		if(this.gameObject)
		{
			this.gameObject.active = IsValid;
		}
		if (EZGUI_Texture)
		{
			EZGUI_Texture.enabled = IsValid;
		}
	}
	
	public override void Release()
	{
		base.Release();
		if (EZGUI_Texture)
		{
			EZGUI_Texture.Delete();
			DestroyImmediate(EZGUI_Texture);
		}
	}
	
	////////// ========== EzGui Base ========== //////////
	
	public override void SetTextureSize(float SizeX, float SizeY)
	{
		base.SetTextureSize(SizeX, SizeY);
		EZGUI_Texture.SetSize(SizeX, SizeY);
	}
	
	public override void SetTextureSizeX(float SizeX)
	{
		base.SetTextureSizeX(SizeX);
		EZGUI_Texture.width  = SizeX;
	}
	
	public override void SetTextureSizeY(float SizeY)
	{
		base.SetTextureSizeY(SizeY);
		EZGUI_Texture.height = SizeY;
	}
	
	public override void SetColor(Color _color)
	{
		base.SetColor(_color);
		EZGUI_Texture.SetColor(_color);
	}
	
	//////////////////////////////////////////////////
	
	public void SetEZGUI(UIButton EZGUI)
	{
		EZGUI_Texture	= EZGUI;
		EZGUI_Texture.SetAnchor(0);
		EZGUI_Texture.enabled	= false;
		SetValid(false);
	}
	
	public UIButton GetEZGUITexture()	{ return EZGUI_Texture; }
	
	public void SetValueChangedDelegate(EZValueChangedDelegate del)	{ EZGUI_Texture.SetValueChangedDelegate(del); }
	public void SetAnchor(SpriteRoot.ANCHOR_METHOD _Ancor)			{ EZGUI_Texture.anchor				= _Ancor; }
	public void SetWhenToInvoke(POINTER_INFO.INPUT_EVENT Type)		{ EZGUI_Texture.whenToInvoke		= Type; }
	
	public void SetcontrolIsEnabled(bool IsValid)					{ EZGUI_Texture.controlIsEnabled	= IsValid; }
	public bool GetcontrolIsEnabled()								{ return EZGUI_Texture.controlIsEnabled; }
	
	public Vector2 GetImageSize()									{ return EZGUI_Texture.ImageSize; }
	
	public bool IsTextureHit()
	{
		if((int)EZGUI_Texture.controlState == (int)CONTROL_STATE.OVER
		|| (int)EZGUI_Texture.controlState == (int)CONTROL_STATE.ACTIVE)
		{
			return true;
		}
		return false;
	}
	
	public void SetText(string ObjectText)
	{
		EZGUI_Texture.Text = ObjectText;
		EZGUI_Texture.spriteText.SetAnchor(0);
		EZGUI_Texture.spriteText.transform.position	= new Vector3(40, -40, Pos.z);
		EZGUI_Texture.spriteText.pixelPerfect		= false;
		EZGUI_Texture.spriteText.characterSize		= 100.0f;
	}
	
	public override void SetAlpha (float _a)
	{
		base.SetAlpha(_a);
		
		if(EZGUI_Texture != null)
		{
			Color TextureColor	= EZGUI_Texture.Color;
			TextureColor.a	= _a;
			
			EZGUI_Texture.Color	= TextureColor;
		}
	}
}
