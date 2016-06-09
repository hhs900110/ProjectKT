using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DefineBase;

public class HudProgressCircle : HudBase
{
	private int				m_dCurCount;
	private float			m_fRotateTime;
	
	private Vector3			m_vecImageRotateBase	= new Vector3(0.0f, 0.0f, -(360.0f/16.0f));
	
	private EzGui_Texture	m_imgProgressCircleFilter;
	private EzGui_Texture	m_imgProgressCircleImage;
	private EzGui_Texture	m_imgProgressCircle;
	
#region IClassBase
	public override void Create()
	{
		base.Create();
		
		m_dCurCount		= 0;
		m_fRotateTime	= 0.0f;
	}
	
	public override void SetValid(bool IsValid)
	{
		base.SetValid(IsValid);
		
		EzGuiSetting.SetValidEzGui(m_imgProgressCircleFilter, IsValid);
		EzGuiSetting.SetValidEzGui(m_imgProgressCircleImage, IsValid);
		EzGuiSetting.SetValidEzGui(m_imgProgressCircle, IsValid);
	}
	
	public override void Update()
	{
		if(!IsUpdate())
		{ return ; }
		base.Update();
		
		if(m_imgProgressCircle != null)
		{
			m_fRotateTime	+= Time.smoothDeltaTime;
			
			if(m_fRotateTime >= 0.1f)
			{
				m_dCurCount++;
				m_fRotateTime	= 0.0f;
				RotationProgressCircle();
			}
		}
	}
#endregion
	
#region Resource Setting
	protected override void LoadResource()
	{
		SetResourceManagerCreateWithXml("HudProgressCircle");
		
		m_imgProgressCircleFilter	= GetResource("TEXTURE_ProgressCircle_Filter").GetComponent<EzGui_Texture>();
		m_imgProgressCircleImage	= GetResource("TEXTURE_ProgressCircle_Image").GetComponent<EzGui_Texture>();
		m_imgProgressCircle			= GetResource("TEXTURE_ProgressCircle_Circle").GetComponent<EzGui_Texture>();
		
		SetResources();
	}
	
	protected override void SetResources()
	{
		m_imgProgressCircleFilter.SetAnchor(SpriteRoot.ANCHOR_METHOD.MIDDLE_CENTER);
		m_imgProgressCircleImage.SetAnchor(SpriteRoot.ANCHOR_METHOD.MIDDLE_CENTER);
		m_imgProgressCircle.SetAnchor(SpriteRoot.ANCHOR_METHOD.MIDDLE_CENTER);
		
		m_imgProgressCircleFilter.SetTextureSize(DefineBaseManager.inst.GameBaseWidth, DefineBaseManager.inst.GameBaseHeight);
	}
	
	public override void ReleaseResource()
	{
		//ReleaseResourceObject();
	}
	
	public override void SetBasePos()
	{
		float BasePosX	= 0.0f;
		float BasePosY	= 0.0f;
		
		EzGuiSetting.SetPosEzGui(m_imgProgressCircleFilter, BasePosX, BasePosY);
		EzGuiSetting.SetPosEzGui(m_imgProgressCircleImage, BasePosX, BasePosY);
		EzGuiSetting.SetPosEzGui(m_imgProgressCircle, BasePosX, BasePosY);
	}
#endregion
	
#region SetPopup/ClosePopup
	public override void SetPopup()
	{
		m_dCurCount		= 0;
		m_fRotateTime	= 0.0f;
		
		base.SetPopup();
	}
	
	public override void ClosePopup()
	{
		base.ClosePopup();
	}
#endregion
	
	private void RotationProgressCircle()
	{
		if(m_dCurCount > 16)
		{
			m_dCurCount	-= 16;
		}
		m_imgProgressCircle.transform.eulerAngles	= m_vecImageRotateBase * m_dCurCount;
	}
}