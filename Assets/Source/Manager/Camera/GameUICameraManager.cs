using UnityEngine;
using System.Collections;
using DefineBase;

#pragma warning disable 0168 // variable declared but not used.
#pragma warning disable 0219 // variable assigned but not used.
#pragma warning disable 0414 // private field assigned but not used.

public class GameUICameraManager : CameraManagerBase
{
#region Create / Init
	public override void Init()
	{
		m_Camera.clearFlags		= CameraClearFlags.Depth;
		m_Camera.cullingMask	= (1 << LayerMask.NameToLayer("GameCamera"));
		m_Camera.orthographic	= true;
		m_Camera.near			=  -1.0f;
		m_Camera.far			= 100.0f;
		m_Camera.depth			= 1;
	}
	
	public override void SetCameraInspector()
	{
		// Test //
		float MapSize		= 0;
		float KittyGap		= DefineBaseManager.inst.KittyGap/2;
		
		if(DefineBaseManager.inst.KittyMaxMapY > DefineBaseManager.inst.KittyMaxMapX)
		{
			MapSize	= KittyGap*DefineBaseManager.inst.KittyMaxMapY+4;
		}
		else
		{
			MapSize	= KittyGap*DefineBaseManager.inst.KittyMaxMapX+4;
		}
		
		float NowScreenWidth	= Main.inst.GetScreenWidth();
		float NowScreenHeight	= Main.inst.GetScreenHeight();
		float GameScreenWidth	= Main.inst.GetGameWidth();
		float GameScreenHeight	= Main.inst.GetGameHeight();
		
		Rect  CameraRect;
		float GoalSize	= 0.0f;
		float GameBoard	= 0.0f;
		
		if(GameScreenHeight == 960)
		{
			GoalSize	= 608.0f;
			GameBoard	= 117.0f;
		}
		else
		{
			GoalSize	= 560.0f;
			GameBoard	= 167.0f;
		}
		
		if((NowScreenHeight/NowScreenWidth) < (GameScreenHeight/GameScreenWidth))
		{
			float Ratio	= (GameScreenWidth*NowScreenHeight)/(GameScreenHeight*NowScreenWidth);
			float Ret	= Ratio * (GoalSize / GameScreenWidth);
			float RetY	= GoalSize / GameScreenHeight;
			CameraRect	= new Rect((1 - Ret)/2, GameBoard/GameScreenHeight, Ret, RetY);
		}
		else
		{
			float Ratio	= (GameScreenHeight*NowScreenWidth)/(GameScreenWidth*NowScreenHeight);
			float RetX	= GoalSize / GameScreenWidth;
			float Ret	= Ratio * (GoalSize / GameScreenHeight);
			float PosY	= Ratio * (GameBoard / GameScreenHeight);
			PosY	= ((1 - Ratio)/2) + PosY;
			CameraRect	= new Rect((1 - RetX)/2, PosY, RetX, Ret);
		}
		
		int MaxKitty	= DefineBaseManager.inst.KittyMaxMapX;
		if(MaxKitty < DefineBaseManager.inst.KittyMaxMapY)
		{
			MaxKitty	= DefineBaseManager.inst.KittyMaxMapY;
		}
		
		m_ZoomMax			= GameScreenHeight/2.0f;
		if(DefineBaseManager.inst.KittyMinView < MaxKitty)
		{
			m_ZoomMin		= m_ZoomMax * ((float)DefineBaseManager.inst.KittyMinView / (float)MaxKitty);
		}
		else
		{
			m_ZoomMin		= m_ZoomMax;
		}
		m_Camera.depth		= 1;
		m_Camera.rect		= CameraRect;
		fieldOfView			= m_ZoomMax;
		transform.position	= Vector3.zero;
		SetMoveLimit();
	}
#endregion
	
	protected override void SetMoveLimit()
	{
		base.SetMoveLimit ();
		m_MoveLimit.x	= m_ZoomMax - fieldOfView;
		m_MoveLimit.y	= m_ZoomMax - fieldOfView;
		m_MoveLimit.z	= 0.0f;
	}
}