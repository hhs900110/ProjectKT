using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DefineBase;
using DataStruct;

public sealed class HudReadyStage : HudBase
{
	private EzGui_Texture	m_imgBackBoard;
	private EzGui_Texture	m_imgReadyBoard;
	private EzGui_Texture	m_imgCharBoard;
	private EzGui_Texture	m_imgItemBoard;
	
	private EzGui_Button	m_btnBlockPos;
	private EzGui_Button	m_btnBlockSet;
	private EzGui_Button	m_btnStart;
	private EzGui_Button	m_btnExit;
	
	private EzGui_Button[]	m_btnItem;
	
#region IClassBase
	public override void Create()
	{
		base.Create();
	}
	
	public override void SetValid(bool IsValid)
	{
		base.SetValid(IsValid);
		
		EzGuiSetting.SetValidEzGui(m_imgBackBoard, IsValid);
		EzGuiSetting.SetValidEzGui(m_imgReadyBoard, IsValid);
		EzGuiSetting.SetValidEzGui(m_imgCharBoard, IsValid);
		EzGuiSetting.SetValidEzGui(m_imgItemBoard, IsValid);
		
		EzGuiSetting.SetValidEzGui(m_btnBlockPos, IsValid);
		EzGuiSetting.SetValidEzGui(m_btnBlockSet, IsValid);
		EzGuiSetting.SetValidEzGui(m_btnStart, IsValid);
		EzGuiSetting.SetValidEzGui(m_btnExit, IsValid);
		
		EzGuiSetting.SetValidEzGui(m_btnItem, IsValid);
	}
	
	public override void Update()
	{
		if(!IsUpdate())
		{ return ; }
		base.Update();
	}
#endregion
	
#region Resource Setting
	protected override void LoadResource()
	{
		SetResourceManagerCreateWithXml("Stage/HudReadyStage");
		
		m_imgBackBoard	= GetResource("TEXTURE_ReadyStage_BackBoard").GetComponent<EzGui_Texture>();
		m_imgReadyBoard	= GetResource("TEXTURE_ReadyStage_ReadyBoard").GetComponent<EzGui_Texture>();
		m_imgCharBoard	= GetResource("TEXTURE_ReadyStage_CharBoard").GetComponent<EzGui_Texture>();
		m_imgItemBoard	= GetResource("TEXTURE_ReadyStage_ItemBoard").GetComponent<EzGui_Texture>();
		
		m_btnBlockPos	= GetResource("BUTTON_ReadyStage_BlockPosChange").GetComponent<EzGui_Button>();
		m_btnBlockSet	= GetResource("BUTTON_ReadyStage_BlockSetChange").GetComponent<EzGui_Button>();
		m_btnStart		= GetResource("BUTTON_ReadyStage_GameStart").GetComponent<EzGui_Button>();
		m_btnExit		= GetResource("BUTTON_ReadyStage_Exit").GetComponent<EzGui_Button>();
		
		int count	= Main.data.GetCount_Item();
		if(m_btnItem == null)	{ m_btnItem	= new EzGui_Button[count]; }
		for(int i = 0; i < count; i++)
		{
			SData_Item tmpData	= Main.data.GetData_Item(Main.data.GetMenuIndex_Item(i));
			string resourcePath	= Main.data.GetData_ResourcePath(tmpData.ItemRaster);
			GameObject tmpObj	= ResourceLoad.GetEZGUI_BUTTON(tmpData.ItemRaster, resourcePath, 0, 0, (int)m_imgItemBoard.GetLayer()+3, HUD_BASE_POS._MIDDLE_CENTER);
			if(tmpObj != null)
			{
				m_btnItem[i]		= tmpObj.GetComponent<EzGui_Button>();
				m_btnItem[i].GetUIButton().Data	= tmpData.ItemIndex;
			}
		}
	}
	
	protected override void SetResources()
	{
		m_imgBackBoard.SetAnchor(SpriteRoot.ANCHOR_METHOD.MIDDLE_CENTER);
		m_imgReadyBoard.SetAnchor(SpriteRoot.ANCHOR_METHOD.BOTTOM_CENTER);
		m_imgCharBoard.SetAnchor(SpriteRoot.ANCHOR_METHOD.UPPER_CENTER);
		m_imgItemBoard.SetAnchor(SpriteRoot.ANCHOR_METHOD.UPPER_CENTER);
		
		m_btnBlockPos.SetAnchor(SpriteRoot.ANCHOR_METHOD.UPPER_CENTER);
		m_btnBlockSet.SetAnchor(SpriteRoot.ANCHOR_METHOD.UPPER_CENTER);
		m_btnStart.SetAnchor(SpriteRoot.ANCHOR_METHOD.UPPER_CENTER);
		m_btnExit.SetAnchor(SpriteRoot.ANCHOR_METHOD.MIDDLE_CENTER);
		
		m_btnStart.SetValueChangedDelegate(StartButtonProcess);
		m_btnExit.SetValueChangedDelegate(ExitButtonProcess);
		
		int count	= Main.data.GetCount_Item();
		for(int i = 0; i < count; i++)
		{
			m_btnItem[i].SetAnchor(SpriteRoot.ANCHOR_METHOD.MIDDLE_CENTER);
			m_btnItem[i].SetValueChangedDelegate(ItemButtonProcess);
		}
	}
	
	public override void ReleaseResource()
	{
		if(m_btnItem != null)
		{
			int count	= Main.data.GetCount_Item();
			for(int i = 0; i < count; i++)
			{
				m_btnItem[i].Release();
				m_btnItem[i]	= null;
			}
			m_btnItem	= null;
		}
		ReleaseResourceObject();
	}
	
	public override void SetBasePos()
	{
		EzGuiSetting.SetPosEzGui(m_imgBackBoard,  0.0f,    0.0f);
		EzGuiSetting.SetPosEzGui(m_imgReadyBoard, 0.0f, -250.0f);
		EzGuiSetting.SetPosEzGui(m_imgCharBoard,  0.0f,  360.0f);
		
		EzGuiSetting.SetPosEzGui(m_btnBlockSet,  120.0f,  210.0f);
		EzGuiSetting.SetPosEzGui(m_btnBlockPos, -120.0f,  210.0f);
		EzGuiSetting.SetPosEzGui(m_btnStart,       0.0f, -270.0f);
		EzGuiSetting.SetPosEzGui(m_btnExit,     -250.0f,  340.0f);
		
		float SubPosX	= 0.0f;
		float SubPosY	= 80.0f;
		float Gap		= 130.0f;
		
		EzGuiSetting.SetPosEzGui(m_imgItemBoard,  SubPosX, SubPosY);
		EzGuiSetting.SetPosEzGui(m_btnItem, SubPosX + 3.0f, SubPosY - 80.0f, Gap, DATUM_POINT._CENTER, DIRECTION._LEFT_RIGHT, DIRECTION_SUB._UP_LEFT);
	}
#endregion
	
#region SetPopup/ClosePopup
	public override void SetPopup()
	{
		base.SetPopup();
	}
	
	public override void ClosePopup()
	{
		base.ClosePopup();
	}
#endregion
	
#region Delegate
	private void StartButtonProcess(IUIObject CurObject)
	{
		SendMessage((int)MSG_TYPE._PLAY, (int)PLAY_STATE._KITTYTURN);
	}
	
	private void ExitButtonProcess(IUIObject CurObject)
	{
		SendMessage((int)MSG_TYPE._PLAY, (int)PLAY_STATE._MAIN);
	}
	
	private void ItemButtonProcess(IUIObject CurObject)
	{
		int ItemIndex	= (int)CurObject.Data;
		Debug.Log("ItemButtonProcess / " + ItemIndex);
		Main.game.GetGameItemManager().CheckItem(ItemIndex);
	}
#endregion
}