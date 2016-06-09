using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using DefineBase;
using DataStruct;

public sealed class GameManager
{
	private struct KittyRect
	{
		public int MinX;
		public int MinY;
		public int MaxX;
		public int MaxY;
	}
	
	private struct KittyPos
	{
		public float x;
		public float y;
	}
	
	private struct KittyData
	{
		public KittyPos Pos;
	}
	
	private	GameUICameraManager	m_scriptGameUICameraMng;
	private	GameItemManager		m_scriptGameItemMng;
	
	private	int			m_dScore;
	private	bool		m_bIsPause;
	private	int			m_dInputCount;
	
	private	List<KittyData>	m_listTurnKittyData;
	
	
	private	KittyRect		m_rectKittyTurnSize;
	
	private	KittyPos		m_posCamera;
	private	KittyPos		m_posCameraGoal;
	private	int				m_dCameraView;
	private	int				m_dCameraViewGoal;
	
	private	bool			m_bIsGameEnd;
	private	float			m_fDeltaGameEnd;
	
	
	private	int				m_dPackageType;
	
	
	public void Create()
	{
		m_scriptGameItemMng	= new GameItemManager();
		m_dPackageType		= 0;
	}
	
	public void Release()	{}
	
	public void Update()
	{
		if(Main.stage.GetNowStageState() == (int)PLAY_STATE._KITTYTURN)
		{
			if(m_bIsGameEnd)
			{
				EndGame();
			}
		}
	}
	
	public void Message(int Msg, int Param1, int Param2)	{}
	
	private void SendMessage(int Msg, int Param1)					{ Main.inst.Message(Msg, Param1); }
	private void SendMessage(int Msg, int Param1, int Param2)		{ Main.inst.Message(Msg, Param1, Param2); }
	
	public GameItemManager	GetGameItemManager()	{ return m_scriptGameItemMng; }
	
	//////////////////////
	
	private KittyTotalManager m_pKittyTotalManager;
#region SetPopup/ClosePopup
	private void BeforeReset()
	{
		if(m_pKittyTotalManager == null)	{ m_pKittyTotalManager	= Main.stage.GetPlayKittyTurn().GetKittyTotalManager(); }
		m_bIsPause	= false;
		if(DefineBaseManager.inst.GetPlatformType() == (int)PLATFORM_TYPE.PLATFORM_WEB)
		{
			GetGameItemManager().CheckItem(100040);
		}
		Main.inst.GetHudManager().GetHudScorePopup().ClosePopup();
		
		m_dPackageType	= UnityEngine.Random.Range(1, 3);
	}
	
	private void Reset()
	{
		if(m_scriptGameUICameraMng == null)	{ m_scriptGameUICameraMng	= Main.inst.GetGameUICameraManager(); }
		if(m_listTurnKittyData == null)		{ m_listTurnKittyData		= new List<KittyData>(); }
		
		m_scriptGameUICameraMng.ResetCamera();
		GetGameItemManager().SetPopup();
		
		if(DefineBaseManager.inst.KittyMaxMapX < DefineBaseManager.inst.KittyMaxMapY)
		{
			m_dCameraView	= DefineBaseManager.inst.KittyMaxMapY;
		}
		else
		{
			m_dCameraView	= DefineBaseManager.inst.KittyMaxMapX;
		}
		
		m_bIsPause		= false;
		m_dScore		= 0;
		m_dInputCount	= 1;
		
		m_bIsGameEnd	= false;
		
		m_posCamera.x		= (float)(DefineBaseManager.inst.KittyMaxMapX-1)/2;
		m_posCamera.y		= (float)(DefineBaseManager.inst.KittyMaxMapY-1)/2;
		m_posCameraGoal.x	= (float)(DefineBaseManager.inst.KittyMaxMapX-1)/2;
		m_posCameraGoal.y	= (float)(DefineBaseManager.inst.KittyMaxMapY-1)/2;
		m_rectKittyTurnSize.MinX	= DefineBaseManager.inst.KittyMaxMapX-1;
		m_rectKittyTurnSize.MaxX	= 0;
		m_rectKittyTurnSize.MinY	= DefineBaseManager.inst.KittyMaxMapY-1;
		m_rectKittyTurnSize.MaxY	= 0;
		m_bIsPause	= true;
	}
	
	public void SetPopup()
	{
		BeforeReset();
		Main.stage.GetPlayKittyTurn().SetPopup();
		Main.inst.GetHudManager().GetHudGameStage().SetPopup();
		Reset();
	}
	
	public void ClosePopup()
	{
		Main.stage.GetPlayKittyTurn().ClosePopup();
		Main.inst.GetHudManager().GetHudGameStage().ClosePopup();
		GetGameItemManager().ClosePopup();
	}
	
	public void SetRePopup()
	{
		BeforeReset();
		Main.stage.GetPlayKittyTurn().SetRePopup();
		Main.inst.GetHudManager().GetHudGameStage().SetRePopup();
		Reset();
	}
	
	public void EndLoadObject()
	{
		Resources.UnloadUnusedAssets();
		System.GC.Collect();
		UsePassiveItem();
		m_bIsPause	= false;
	}
#endregion
	
#region Kitty Turn
	public void OneTurn(int _KittyX, int _KittyY)
	{
		AddTurnKitty(_KittyX, _KittyY);
		SetRect();
		
		m_dScore	+= 1;
		Main.inst.GetHudManager().GetHudGameStage().SetScore(m_dScore);
	}
	
	public void EndTurn(int _KittyX, int _KittyY)
	{
		RemoveTurnKitty(_KittyX, _KittyY);
		SetRect();
		
		if(m_listTurnKittyData.Count == 0 && m_dInputCount == 0)
		{
			m_bIsGameEnd	= true;
			m_fDeltaGameEnd	= 0.0f;
			
			m_scriptGameUICameraMng.ResetCamera();
			if(Main.inst.GetHudManager().GetHudPopupAdvice().GetValid())
			{
				Main.inst.GetHudManager().GetHudPopupAdvice().ClosePopup();
			}
		}
	}
#endregion
	
#region Passive Item
	public void UsePassiveItem()
	{
		int count	= GetGameItemManager().GetServiceablePassiveItemNum();
		
		for(int i = 0; i < count; i++)
		{
			int ItemIndex	= GetGameItemManager().GetServiceablePassiveItemIndex(i);
			
			SData_Item itemData	= Main.data.GetData_Item(ItemIndex);
			
			if(itemData.ItemType == 1010)
			{
				UsePassiveItem_Reflection(itemData);
			}
		}
	}
	
	private void UsePassiveItem_Reflection(SData_Item itemData)
	{
		for(int i = 0; i < DefineBaseManager.inst.KittyMaxMapX; i++)
		{
			for(int j = 0; j <DefineBaseManager.inst.KittyMaxMapY; j++)
			{
				if(i == 0 || i + 1 == DefineBaseManager.inst.KittyMaxMapX)
				{
					KittyTotalObject tmpKittyObj	= m_pKittyTotalManager.GetKittyTotalObject(i, j);
					if(tmpKittyObj != null)
					{
						tmpKittyObj.SetLegChange(LEG_TYPE._DIAMOND, itemData.ItemUseTime);
					}
				}
				else if(j == 0 || j + 1 == DefineBaseManager.inst.KittyMaxMapY)
				{
					KittyTotalObject tmpKittyObj	= m_pKittyTotalManager.GetKittyTotalObject(i, j);
					if(tmpKittyObj != null)
					{
						tmpKittyObj.SetLegChange(LEG_TYPE._DIAMOND, itemData.ItemUseTime);
					}
				}
			}
		}
	}
#endregion
	
#region Active Item
	public void UseActiveItem(int _KittyX, int _KittyY)
	{
		bool Use		= false;
		int ItemIndex	= GetGameItemManager().GetUseActiveItemIndex();
		
		SData_Item itemData	= Main.data.GetData_Item(ItemIndex);
		int ItemType		= itemData.ItemType;
		if(ItemType == 1020)
		{
			Use	= true;
			UseActiveItem_AbsoluteArea(itemData, _KittyX, _KittyY);
		}
		else if(ItemType == 1030)
		{
			Use	= true;
			UseActiveItem_Refresh(itemData, _KittyX, _KittyY);
		}
		else if(ItemType == 1040)
		{
			Use	= true;
			UseActiveItem_Bomb(itemData, _KittyX, _KittyY);
		}
		
		if(Use)	{ GetGameItemManager().UseNowItem(); }
	}
	
	private void UseActiveItem_AbsoluteArea(SData_Item itemData, int _KittyX, int _KittyY)
	{
		int DirX	= 0;
		int DirY	= 0;
		
		if(itemData.ItemAreaX % 2 == 0)
		{
			DirX	= (itemData.ItemAreaX - 1) / 2;
		}
		else
		{
			DirX	= (itemData.ItemAreaX) / 2;
		}
		
		if(itemData.ItemAreaY % 2 == 0)
		{
			DirY	= (itemData.ItemAreaY - 1) / 2;
		}
		else
		{
			DirY	= (itemData.ItemAreaY) / 2;
		}
		
		for(int i = 0; i < itemData.ItemAreaX && _KittyX - DirX + i < DefineBaseManager.inst.KittyMaxMapX; i++)
		{
			if(_KittyX - DirX + i < 0)
			{ continue ; }
			
			for(int j = 0; j < itemData.ItemAreaY && _KittyY - DirY + j < DefineBaseManager.inst.KittyMaxMapY; j++)
			{
				if(_KittyY - DirY + j < 0)
				{ continue ; }
				
				KittyTotalObject tmpKittyObj	= m_pKittyTotalManager.GetKittyTotalObject(_KittyX - DirX + i, _KittyY - DirY + j);
				if(tmpKittyObj != null)
				{
					tmpKittyObj.SetLegChange(LEG_TYPE._DIAMOND, itemData.ItemUseTime);
				}
			}
		}
	}
	
	private void UseActiveItem_Refresh(SData_Item itemData, int _KittyX, int _KittyY)
	{
		int DirX	= 0;
		int DirY	= 0;
		
		if(itemData.ItemAreaX % 2 == 0)
		{
			DirX	= (itemData.ItemAreaX - 1) / 2;
		}
		else
		{
			DirX	= (itemData.ItemAreaX) / 2;
		}
		
		if(itemData.ItemAreaY % 2 == 0)
		{
			DirY	= (itemData.ItemAreaY - 1) / 2;
		}
		else
		{
			DirY	= (itemData.ItemAreaY) / 2;
		}
		
		for(int i = 0; i < itemData.ItemAreaX && _KittyX - DirX + i < DefineBaseManager.inst.KittyMaxMapX; i++)
		{
			if(_KittyX - DirX + i < 0)
			{ continue ; }
			
			for(int j = 0; j < itemData.ItemAreaY && _KittyY - DirY + j < DefineBaseManager.inst.KittyMaxMapY; j++)
			{
				if(_KittyY - DirY + j < 0)
				{ continue ; }
				
				KittyTotalObject tmpKittyObj	= m_pKittyTotalManager.GetKittyTotalObject(_KittyX - DirX + i, _KittyY - DirY + j);
				if(tmpKittyObj != null)
				{
					tmpKittyObj.SetRefresh();
				}
			}
		}
	}
	
	private void UseActiveItem_Bomb(SData_Item itemData, int _KittyX, int _KittyY)
	{
		int DirX	= 0;
		int DirY	= 0;
		
		if(itemData.ItemAreaX % 2 == 0)
		{
			DirX	= (itemData.ItemAreaX - 1) / 2;
		}
		else
		{
			DirX	= (itemData.ItemAreaX) / 2;
		}
		
		if(itemData.ItemAreaY % 2 == 0)
		{
			DirY	= (itemData.ItemAreaY - 1) / 2;
		}
		else
		{
			DirY	= (itemData.ItemAreaY) / 2;
		}
		
		for(int i = 0; i < itemData.ItemAreaX && _KittyX - DirX + i < DefineBaseManager.inst.KittyMaxMapX; i++)
		{
			if(_KittyX - DirX + i < 0)
			{ continue ; }
			
			for(int j = 0; j < itemData.ItemAreaY && _KittyY - DirY + j < DefineBaseManager.inst.KittyMaxMapY; j++)
			{
				if(_KittyY - DirY + j < 0)
				{ continue ; }
				
				KittyTotalObject tmpKittyObj	= m_pKittyTotalManager.GetKittyTotalObject(_KittyX - DirX + i, _KittyY - DirY + j);
				if(tmpKittyObj != null)
				{
					tmpKittyObj.InputKittyTurn((int)KITTY_TURN_TYPE._LEFT);
				}
			}
		}
	}
#endregion
	
	private void EndGame()
	{
		m_fDeltaGameEnd	+= Time.smoothDeltaTime;
		
		if(m_fDeltaGameEnd > 1)
		{
//			SendMessage((int)MSG_TYPE._PLAY, (int)PLAY_STATE._MAIN);
			if(!Main.inst.GetHudManager().GetHudScorePopup().GetValid())
			{
				Main.inst.GetHudManager().GetHudScorePopup().SetPopup();
				//Main.inst.SendScore();
			}
			else
			{
				Main.inst.GetHudManager().GetHudScorePopup().SetScore(m_dScore);
			}
		}
	}
	
#region Camera Move
	private void SetRect()
	{
		if(m_listTurnKittyData == null)	{ m_listTurnKittyData	= new List<KittyData>(); }
		
		int count	= m_listTurnKittyData.Count;
		m_rectKittyTurnSize.MinX	= DefineBaseManager.inst.KittyMaxMapX;
		m_rectKittyTurnSize.MaxX	= 0;
		m_rectKittyTurnSize.MinY	= DefineBaseManager.inst.KittyMaxMapY;
		m_rectKittyTurnSize.MaxY	= 0;
		
		for(int i = 0; i < count; i++)
		{
			if(m_rectKittyTurnSize.MinX > m_listTurnKittyData[i].Pos.x)
			{
				m_rectKittyTurnSize.MinX	= (int)m_listTurnKittyData[i].Pos.x;
			}
			else if(m_rectKittyTurnSize.MaxX < m_listTurnKittyData[i].Pos.x)
			{
				m_rectKittyTurnSize.MaxX	= (int)m_listTurnKittyData[i].Pos.x;
			}
			if(m_rectKittyTurnSize.MinY > m_listTurnKittyData[i].Pos.y)
			{
				m_rectKittyTurnSize.MinY	= (int)m_listTurnKittyData[i].Pos.y;
			}
			else if(m_rectKittyTurnSize.MaxY < m_listTurnKittyData[i].Pos.y)
			{
				m_rectKittyTurnSize.MaxY	= (int)m_listTurnKittyData[i].Pos.y;
			}
		}
		
		m_posCameraGoal.x	= (float)(m_rectKittyTurnSize.MaxX + m_rectKittyTurnSize.MinX)/2;
		m_posCameraGoal.y	= (float)(m_rectKittyTurnSize.MaxY + m_rectKittyTurnSize.MinY)/2;
		
		if(m_rectKittyTurnSize.MaxX - m_rectKittyTurnSize.MinX < m_rectKittyTurnSize.MaxY - m_rectKittyTurnSize.MinY)
		{
			m_dCameraViewGoal	= m_rectKittyTurnSize.MaxY - m_rectKittyTurnSize.MinY;
		}
		else
		{
			m_dCameraViewGoal	= m_rectKittyTurnSize.MaxX - m_rectKittyTurnSize.MinX;
		}
		if(m_dCameraViewGoal < DefineBaseManager.inst.KittyMinView)
		{
			m_dCameraViewGoal	= DefineBaseManager.inst.KittyMinView;
		}
	}
	
	private void CameraSlowMove()
	{
		if(m_scriptGameUICameraMng == null)
		{ return ; }
		
		float tmpX;
		float tmpY;
		int MaxMap	= DefineBaseManager.inst.KittyMaxMapX;
		if(MaxMap < DefineBaseManager.inst.KittyMaxMapY)
		{
			MaxMap	= DefineBaseManager.inst.KittyMaxMapY;
		}
		if(DefineBaseManager.inst.GameBaseHeight == 960.0f)
		{
			tmpX	= 12.7f/MaxMap;
			tmpY	= 12.7f/MaxMap;
		}
		else
		{
			tmpX	= 14.4f/MaxMap;
			tmpY	= 14.4f/MaxMap;
		}
		float Gap	= DefineBaseManager.inst.KittyGap;
		float computeX	=  (tmpX * Gap * (1.0f - DefineBaseManager.inst.KittyMaxMapX + 2.0f*m_posCameraGoal.x) / 2.0f);
		float computeY	= -(tmpY * Gap * (1.0f - DefineBaseManager.inst.KittyMaxMapY + 2.0f*m_posCameraGoal.y) / 2.0f);
		m_scriptGameUICameraMng.SetPIDCameraMove(new Vector3(computeX, computeY, m_scriptGameUICameraMng.transform.position.z), 0.01f, 0.0f);
	}
	
	private void AddTurnKitty(int _KittyX, int _KittyY)
	{
		KittyData	tmpData;
		tmpData.Pos.x	= _KittyX;
		tmpData.Pos.y	= _KittyY;
		m_listTurnKittyData.Add(tmpData);
	}
	
	private void RemoveTurnKitty(int _KittyX, int _KittyY)
	{
		int count	= m_listTurnKittyData.Count;
		for(int i = 0 ; i < count; i++)
		{
			if(m_listTurnKittyData[i].Pos.x == _KittyX
			&& m_listTurnKittyData[i].Pos.y == _KittyY)
			{
				m_listTurnKittyData.RemoveAt(i);
				i = count;
			}
		}
	}
#endregion
	
	public void ClickPause()
	{
		Pause(!m_bIsPause);
	}
	
	private void Pause(bool _IsPause)
	{
		m_bIsPause	= _IsPause;
		Main.stage.GetPlayKittyTurn().Pause(m_bIsPause);
	}
	
	public bool	IsPause()			{ return m_bIsPause; }
	public void	ClickKitty()
	{
		m_dInputCount--;
	}
	public int	GetInputCount()		{ return m_dInputCount; }
	
	public bool	GetIsGameEnd()		{ return m_bIsGameEnd; }
	public int	GetScore()			{ return m_dScore; }
	
	public int	GetPackageType()	{ return m_dPackageType; }
}