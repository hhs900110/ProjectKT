using UnityEngine;
using System.Collections;
using DefineBase;

public class StageManager : IMonoBase
{
	private StageBase[]	m_StageScript;
	
	private int OldStageState;
	private int NowStageState;
	
#region IClassBase
	private bool Valid;
	
	public void Create()
	{
		Valid	= false;
		
		m_StageScript	= new StageBase[(int)PLAY_STATE._MAX];
		
		m_StageScript[(int)PLAY_STATE._LOADING]		= new PlayStageLoading();
		m_StageScript[(int)PLAY_STATE._MAIN]		= new PlayStageMain();
		m_StageScript[(int)PLAY_STATE._READY]		= new PlayStageReady();
		m_StageScript[(int)PLAY_STATE._KITTYTURN]	= new PlayStageKittyTurn();
		
		for(int i = 0; i < (int)PLAY_STATE._MAX; i++)
		{
			if(m_StageScript[i] != null)
			{
				m_StageScript[i].Create();
				m_StageScript[i].SetValid(false);
			}
		}
		
		OldStageState	= (int)PLAY_STATE._MAX;
		NowStageState	= (int)PLAY_STATE._MAX;
		
		SetPlayState((int)PLAY_STATE._LOADING);
	}
	
	public virtual void SetValid(bool IsValid)
	{
		Valid	= IsValid;
	}
	
	public virtual bool GetValid()
	{
		return Valid;
	}
	
	public void Release()
	{
		for(int i = 0; i < (int)PLAY_STATE._MAX; i++)
		{
			if(m_StageScript[i] != null)
			{
				m_StageScript[i].Release();
			}
		}
	}
	
	public void Message(int Msg, int Param1, int Param2)
	{
		if(Msg == (int)MSG_TYPE._PLAY)
		{
			SetPlayState(Param1);
		}
	}
	
	public void Update()
	{
		for(int i = 0; i < (int)PLAY_STATE._MAX; i++)
		{
			if(m_StageScript[i] != null)
			{
				m_StageScript[i].Update();
			}
		}
	}
#endregion
	
#region GetScript
	public StageBase			GetPlayStage(int _Stage)	{ return m_StageScript[_Stage]; }
	
	public PlayStageLoading		GetPlayLoading()	{ return (PlayStageLoading)m_StageScript[(int)PLAY_STATE._LOADING]; }
	public PlayStageMain		GetPlayMain()		{ return (PlayStageMain)m_StageScript[(int)PLAY_STATE._MAIN]; }
	public PlayStageReady		GetPlayReady()		{ return (PlayStageReady)m_StageScript[(int)PLAY_STATE._READY]; }
	public PlayStageKittyTurn	GetPlayKittyTurn()	{ return (PlayStageKittyTurn)m_StageScript[(int)PLAY_STATE._KITTYTURN]; }
#endregion
	
	private void SetPlayState(int _PlayState)
	{
		OldStageState	= NowStageState;
		NowStageState	= _PlayState;
		
		for(int i = 0; i < (int)PLAY_STATE._MAX; i++)
		{
			if(m_StageScript[i] != null)
			{
				if(NowStageState == i)
				{
					m_StageScript[i].SetValid(true);
				}
				else
				{
					m_StageScript[i].SetValid(false);
				}
			}
		}
	}
	
	private void ReturnPlayState()
	{
		SetPlayState(OldStageState);
		OldStageState	= (int)PLAY_STATE._MAX;
	}
	
	public int GetNowStageState()	{ return NowStageState; }
}