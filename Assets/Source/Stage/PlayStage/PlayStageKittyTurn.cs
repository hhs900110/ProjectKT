using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DefineBase;

public class PlayStageKittyTurn : StageBase
{
	private	GameUICameraManager	m_scriptGameUICameraMng;
	private	KittyTotalManager	m_scriptKittyTotalManager;
	
#region IClassBase
	public override void Create()
	{
		base.Create();
		if(m_scriptGameUICameraMng == null)	{ m_scriptGameUICameraMng	= Main.inst.GetGameUICameraManager(); }
		
		m_scriptKittyTotalManager	= new KittyTotalManager();
		m_scriptKittyTotalManager.Create();
	}
	
	public override void SetValid(bool IsValid)
	{
		base.SetValid(IsValid);
		
		if(Main.inst.GetHudManager() != null)
		{
			if(IsValid)
			{
				Main.game.SetPopup();
			}
			else
			{
				Main.game.ClosePopup();
			}
		}
	}
	
	public override void Release()
	{
		if(m_scriptKittyTotalManager != null)	{ m_scriptKittyTotalManager.Release(); }
		base.Release();
	}
	
	public override void Update()
	{
		if(m_scriptKittyTotalManager != null)	{ m_scriptKittyTotalManager.Update(); }
		if(!IsUpdate())
		{ return ; }
		if(Main.game.GetIsGameEnd())
		{ return ; }
		base.Update();
		
//		CameraSlowMove();
		if(IsMove())
		{
			CameraMove();
		}
		else if(IsZoom())
		{
#if UNITY_EDITOR
			// Zoom
			if(InputManagerScript.GetMouseWheelDown())
			{
				InputManagerScript.SmothZoomInOut(Main.inst.GetGameUICameraManager(), (int)ZOOM_STATE._OUT, 200.0f);
			}
			else if(InputManagerScript.GetMouseWheelUp())
			{
				InputManagerScript.SmothZoomInOut(Main.inst.GetGameUICameraManager(), (int)ZOOM_STATE._IN, 200.0f);
			}
#elif UNITY_IPHONE || UNITY_ANDROID
			InputManagerScript.SmothZoomInOut(Main.inst.GetGameUICameraManager(), 200.0f);
			InputManagerScript.ResetAxis();
#else
			// Zoom
			if(InputManagerScript.GetMouseWheelDown())
			{
				InputManagerScript.SmothZoomInOut(Main.inst.GetGameUICameraManager(), (int)ZOOM_STATE._OUT, 200.0f);
			}
			else if(InputManagerScript.GetMouseWheelUp())
			{
				InputManagerScript.SmothZoomInOut(Main.inst.GetGameUICameraManager(), (int)ZOOM_STATE._IN, 200.0f);
			}
#endif
		}
	}
#endregion
	
	private float MoveSpeed	= 5.0f;
	private void CameraMove()
	{
		float MoveX = InputManagerScript.GetAxisX();
		float MoveY = InputManagerScript.GetAxisY();
		
		m_scriptGameUICameraMng.CameraLocalMove(-(MoveX*MoveSpeed), -(MoveY*MoveSpeed), 0.0f);
	}
	
	
#region SetPopup/ClosePopup
	public void SetPopup()
	{
		m_scriptKittyTotalManager.SetPopup();
	}
	
	public void ClosePopup()
	{
		m_scriptKittyTotalManager.ClosePopup();
	}
	
	public void SetRePopup()
	{
		m_scriptKittyTotalManager.SetRePopup();
	}
#endregion
	
	public KittyTotalManager	GetKittyTotalManager()	{ return m_scriptKittyTotalManager; }
	
	public void Pause(bool _Pause)
	{
		m_scriptKittyTotalManager.Pause(_Pause);
	}
}