using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DefineBase;

public sealed class HudPopupAdvice : HudBase
{
	private EzGui_Texture	m_imgAdviceBoard;
	private EzGui_Texture	m_imgAdviceFilter;
	private EzGui_Texture[]	m_imgAdviceHelp;
	
	private EzGui_Button	m_btnAdviceBefore;
	private EzGui_Button	m_btnAdviceNext;
	private EzGui_Button	m_btnAdviceExit;
	
	private const int	m_dMaxPage	= 4;
	private int			m_dNowPage;
	
#region IClassBase
	public override void Create()
	{
		base.Create();
	}
	
	public override void SetValid(bool IsValid)
	{
		base.SetValid(IsValid);
		
		EzGuiSetting.SetValidEzGui(m_imgAdviceBoard, IsValid);
		EzGuiSetting.SetValidEzGui(m_imgAdviceFilter, IsValid);
		EzGuiSetting.SetValidEzGui(m_imgAdviceHelp, false);
		EzGuiSetting.SetValidEzGui(m_btnAdviceBefore, false);
		EzGuiSetting.SetValidEzGui(m_btnAdviceNext, false);
		EzGuiSetting.SetValidEzGui(m_btnAdviceExit, IsValid);
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
		SetResourceManagerCreateWithXml("HudPopupAdvice");
		if(m_imgAdviceHelp == null)		{ m_imgAdviceHelp	= new EzGui_Texture[m_dMaxPage]; }
		
		m_imgAdviceBoard	= GetResource("TEXTURE_PopupAdvice_AdviceBoard").GetComponent<EzGui_Texture>();
		m_imgAdviceFilter	= GetResource("TEXTURE_PopupAdvice_Filter").GetComponent<EzGui_Texture>();
		
		for(int i = 0; i < m_dMaxPage; i++)
		{
			m_imgAdviceHelp[i]	= GetResource(string.Format("TEXTURE_PopupAdvice_Help{0}", i.ToString())).GetComponent<EzGui_Texture>();
		}
		
		m_btnAdviceBefore	= GetResource("BUTTON_PopupAdvice_Help_ArrowLeft").GetComponent<EzGui_Button>();
		m_btnAdviceNext		= GetResource("BUTTON_PopupAdvice_Help_ArrowRight").GetComponent<EzGui_Button>();
		m_btnAdviceExit		= GetResource("BUTTON_PopupAdvice_Exit").GetComponent<EzGui_Button>();
	}
	
	protected override void SetResources()
	{
		m_imgAdviceBoard.SetAnchor(SpriteRoot.ANCHOR_METHOD.MIDDLE_CENTER);
		m_imgAdviceFilter.SetAnchor(SpriteRoot.ANCHOR_METHOD.MIDDLE_CENTER);
		for(int i = 0; i < m_dMaxPage; i++)
		{
			m_imgAdviceHelp[i].SetAnchor(SpriteRoot.ANCHOR_METHOD.MIDDLE_CENTER);
		}
		
		m_imgAdviceFilter.SetTextureSize(DefineBaseManager.inst.GameBaseWidth, DefineBaseManager.inst.GameBaseHeight);
		
		m_btnAdviceBefore.SetAnchor(SpriteRoot.ANCHOR_METHOD.BOTTOM_LEFT);
		m_btnAdviceNext.SetAnchor(SpriteRoot.ANCHOR_METHOD.BOTTOM_RIGHT);
		m_btnAdviceExit.SetAnchor(SpriteRoot.ANCHOR_METHOD.UPPER_RIGHT);
		
		m_btnAdviceBefore.SetValueChangedDelegate(BeforeProcess);
		m_btnAdviceNext.SetValueChangedDelegate(NextProcess);
		m_btnAdviceExit.SetValueChangedDelegate(ExitProcess);
	}
	
	public override void ReleaseResource()
	{
		EzGuiSetting.ReleaseEzGui(m_imgAdviceBoard);
		EzGuiSetting.ReleaseEzGui(m_imgAdviceFilter);
		EzGuiSetting.ReleaseEzGui(m_imgAdviceHelp);
		EzGuiSetting.ReleaseEzGui(m_btnAdviceBefore);
		EzGuiSetting.ReleaseEzGui(m_btnAdviceNext);
		EzGuiSetting.ReleaseEzGui(m_btnAdviceExit);
		
		ReleaseResourceObject();
	}
	
	public override void SetBasePos()
	{
		EzGuiSetting.SetPosEzGui(m_btnAdviceBefore,  250.0f, -270.0f);
		EzGuiSetting.SetPosEzGui(m_btnAdviceNext,   -250.0f, -270.0f);
		EzGuiSetting.SetPosEzGui(m_btnAdviceExit,   -255.0f,  270.0f);
	}
#endregion
	
#region SetPopup/ClosePopup
	public override void SetPopup()
	{
		m_dNowPage	= 0;
		
		base.SetPopup();
		SetPage();
	}
	
	public override void ClosePopup()
	{
		base.ClosePopup();
	}
#endregion
	
#region Delegate
	private void ExitProcess(IUIObject CurObject)
	{
		ClosePopup();
	}
	
	private void BeforeProcess(IUIObject CurObject)
	{
		m_dNowPage--;
		SetPage();
	}
	
	private void NextProcess(IUIObject CurObject)
	{
		m_dNowPage++;
		SetPage();
	}
#endregion
	
	private void SetPage()
	{
		if(m_imgAdviceHelp == null)
		{ return ; }
		
		for(int i = 0; i < m_dMaxPage; i++)
		{
			if(i == m_dNowPage)
			{
				EzGuiSetting.SetValidEzGui(m_imgAdviceHelp[i], GetValid());
			}
			else
			{
				EzGuiSetting.SetValidEzGui(m_imgAdviceHelp[i], false);
			}
		}
		if(m_dNowPage == 0)
		{
			EzGuiSetting.SetValidEzGui(m_btnAdviceNext, GetValid());
			EzGuiSetting.SetValidEzGui(m_btnAdviceBefore, false);
		}
		else if(m_dNowPage == m_dMaxPage - 1)
		{
			EzGuiSetting.SetValidEzGui(m_btnAdviceBefore, GetValid());
			EzGuiSetting.SetValidEzGui(m_btnAdviceNext, false);
		}
		else
		{
			EzGuiSetting.SetValidEzGui(m_btnAdviceBefore, GetValid());
			EzGuiSetting.SetValidEzGui(m_btnAdviceNext, GetValid());
		}
	}
}