using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DefineBase;

public class UserInfoSlot
{
	private EzGui_Texture		m_imgBoard;
	private EzGui_Texture		m_imgIcon;
	private EzGui_Button		m_btnAdd;
	private EzGui_SpriteText	m_txtNum;
	
	public void SetResource(GameResourceManager ResourceObject, string SlotName)
	{
		if(ResourceObject != null)
		{
			m_imgBoard	= ResourceObject.GetResource(string.Format("{0}{1}{2}", "TEXTURE_", SlotName, "Board")).GetComponent<EzGui_Texture>();
			m_imgIcon	= ResourceObject.GetResource(string.Format("{0}{1}{2}", "TEXTURE_", SlotName, "Icon")).GetComponent<EzGui_Texture>();
			m_btnAdd	= ResourceObject.GetResource(string.Format("{0}{1}{2}", "BUTTON_", SlotName, "Add")).GetComponent<EzGui_Button>();
			m_txtNum	= ResourceObject.GetResource(string.Format("{0}{1}{2}", "SPRITE_", SlotName, "Num")).GetComponent<EzGui_SpriteText>();
			
			SetResources();
		}
	}
	
	private void SetResources()
	{
		m_imgBoard.SetAnchor(SpriteRoot.ANCHOR_METHOD.MIDDLE_LEFT);
		m_imgIcon.SetAnchor(SpriteRoot.ANCHOR_METHOD.MIDDLE_CENTER);
		m_btnAdd.SetAnchor(SpriteRoot.ANCHOR_METHOD.MIDDLE_CENTER);
	}
	
	public void SetPos(float PosX, float PosY)
	{
		if(m_imgBoard != null)		{ m_imgBoard.SetPos(PosX, PosY); }
		if(m_imgIcon != null)		{ m_imgIcon.SetPos(PosX, PosY); }
		if(m_btnAdd != null)		{ m_btnAdd.SetPos(PosX - m_imgBoard.GetTextureSizeX(), PosY); }
		if(m_txtNum != null)		{ m_txtNum.SetPos(PosX - m_imgBoard.GetTextureSizeX()/2, PosY); }
	}
	
	public void SetValid(bool IsValid)
	{
		if(m_imgBoard != null)		{ m_imgBoard.SetValid(IsValid); }
		if(m_imgIcon != null)		{ m_imgIcon.SetValid(IsValid); }
		if(m_btnAdd != null)		{ m_btnAdd.SetValid(IsValid); }
		if(m_txtNum != null)		{ m_txtNum.SetValid(IsValid); }
	}
	
	public string text
	{
		get {
			if(m_txtNum != null)	{ return m_txtNum.GetText(); }
			else
			{
				return "";
			}
		}
		set {
			if(m_txtNum != null)	{ m_txtNum.SetText(value); }
		}
	}
	
	public void SetValueChangedDelegate(EZValueChangedDelegate _del)
	{
		if(m_btnAdd != null)
		{
			m_btnAdd.SetValueChangedDelegate(_del);
		}
	}
}