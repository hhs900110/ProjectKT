using UnityEngine;
using System.Collections;
using DefineBase;

public sealed class DefineBaseManager : MonoBehaviour
{
	private static DefineBaseManager m_inst = null;
	public static DefineBaseManager inst	{ get { return m_inst; } }
	
#if UNITY_EDITOR
//	private float	m_GameBaseWidth			=  567.0f;
//	private float	m_GameBaseHeight		= 1008.0f;
	private float	m_GameBaseWidth			=  640.0f;
	private float	m_GameBaseHeight		=  960.0f;
#elif UNITY_IPHONE
	private float	m_GameBaseWidth			=  640.0f;
	private float	m_GameBaseHeight		=  960.0f;
#elif UNITY_ANDROID
	private float	m_GameBaseWidth			=  567.0f;
	private float	m_GameBaseHeight		= 1008.0f;
#else
	private float	m_GameBaseWidth			=  640.0f;
	private float	m_GameBaseHeight		=  960.0f;
#endif
	
	private int		dScreenType				= (int)SCREEN_TYPE._SCREEN_RECTANGLE;
	private float	GameWidth				= 0.0f;
	private float	GameHeight				= 0.0f;
	private float	ScreenWidth				= 0.0f;
	private float	ScreenHeight			= 0.0f;
	private float	GameSizeGap				= 0.0f;
	private bool	IsDebug					= false;
	private bool	IsNetWork				= true;
//	private int		Platform_Type			= (int)PLATFORM_TYPE.PLATFORM_ANDROID;
	private int		Platform_Type			= (int)PLATFORM_TYPE.PLATFORM_WEB;
	private bool	IsTouchInput			= false;
	
	private int		dKittyMaxMapX			= 16;
	private int		dKittyMaxMapY			= 16;
	private float	fKittyGap				= 70.0f;
	
	private int		dKittyLegDir			= 4;
//	private int		dKittyMinLeg			= 2;
//	private int		dKittyMaxLeg			= 2;
	private int		dKittyTurnType			= (int)KITTY_TURN_TYPE._REBOUND;
    private int dKittyTurnLegType = (int) LEG_BASE_SET._RIGHT_ANGLE;
    public const int kKittyLegStraight = 5; // 1자 다리가 나올 확률 1/n
    public const int kKittyLegDiamond = 20; // 십자 다리가 나올 확률 1/n

    private float	fKittyTurnSpeed			= 250.0f;
	
	private int		dEffectParticleMin		= 1;
	private int		dEffectParticleMax		= 3;
	
	private int		m_KittyMinView			= 8;
	
	DefineBaseManager()
	{
	}
	
	void Awake()
	{
		if(m_inst == null)
		{
			m_inst = this;
		}
	}
	
	public void Create()
	{
		GameWidth = 0.0f;
		GameHeight = 0.0f;
	}
	
	public void Release()
	{
		m_inst = null;
	}
	
	public void SetGameSize(float Width, float Height)
	{
		GameWidth = Width;
		GameHeight = Height;
		if(GameHeight == GameWidth)
		{
			dScreenType	= (int)SCREEN_TYPE._SCREEN_RECTANGLE;
		}
		else if(GameHeight > GameWidth)
		{
			dScreenType	= (int)SCREEN_TYPE._SCREEN_TALL;
		}
		else
		{
			dScreenType	= (int)SCREEN_TYPE._SCREEN_WIDE;
		}
	}
	
	public void SetScreenSize(float Width, float Height)
	{
		ScreenWidth = Width;
		ScreenHeight = Height;
	}
	
	public void SetGameSizeGap(float Size)				{ GameSizeGap = Size; }
	public void SetPlatformType(int ObjectPlatformType)	{ Platform_Type = ObjectPlatformType; }
	public void SetTouchInput(bool IsValid)				{ IsTouchInput = IsValid; }
	
	////////////////////
	
	public float GameBaseWidth	{ get { return m_GameBaseWidth; } }
	public float GameBaseHeight	{ get { return m_GameBaseHeight; } }
	
	public float GetGameWidth()		{ return GameWidth; }
	public float GetGameHeight()	{ return GameHeight; }
	
	public float GetScreenWidth()	{ return ScreenWidth; }
	public float GetScreenHeight()	{ return ScreenHeight; }
	
	public float GetGameSizeGap()	{ return GameSizeGap; }
	public bool	 GetDebugValid()	{ return IsDebug; }
	public bool	 GetNetWork()		{ return IsNetWork; }
	
	public int	 GetPlatformType()	{ return Platform_Type; }
	
	public bool	 GetTouchInput()	{ return IsTouchInput; }
	
	public int	 ScreenType			{ get { return dScreenType; } }
	
	////////////////////
	
	public void SetKittyMaxMapX(int _Num)			{ dKittyMaxMapX		= _Num; }
	public void SetKittyMaxMapY(int _Num)			{ dKittyMaxMapY		= _Num; }
	public void SetKittyLegDir(int _Num)			{ dKittyLegDir		= _Num; }
//	public void SetKittyMinLeg(int _Num)
//	{
//		if(_Num > dKittyMaxLeg)	{ dKittyMinLeg		= dKittyMaxLeg; }
//		else					{ dKittyMinLeg		= _Num; }
//	}
//	public void SetKittyMaxLeg(int _Num)			{ dKittyMaxLeg		= _Num; }
	public void SetKittyTurnType(int _Type)			{ dKittyTurnType	= _Type; }
	public void SetKittyTurnSpeed(float _Speed)		{ fKittyTurnSpeed	= _Speed; }
	public void SetKittyGap(float _Gap)				{ fKittyGap			= _Gap; }
	
	public void SetEffectParticleMin(int _Num)		{ dEffectParticleMin	= _Num; }
	public void SetEffectParticleMax(int _Num)		{ dEffectParticleMax	= _Num; }
	
	////////////////////
	
	public int	 KittyMaxMapX		{ get { return dKittyMaxMapX; } }
	public int	 KittyMaxMapY		{ get { return dKittyMaxMapY; } }
	public int	 KittyLegDir		{ get { return dKittyLegDir; } }
//	public int	 KittyMinLeg		{ get { return dKittyMinLeg; } }
//	public int	 KittyMaxLeg		{ get { return dKittyMaxLeg; } }
	public int	 KittyTurnType		{ get { return dKittyTurnType; } }
    public int KittyTurnLegType     { get { return dKittyTurnLegType; } }

    public float KittyTurnSpeed		{ get { return fKittyTurnSpeed; } }
	public float KittyGap			{ get { return fKittyGap; } }
	
	public int	 KittyMinView		{ get { return m_KittyMinView; } }
	
	public int	 EffectParticleMin	{ get { return dEffectParticleMin; } }
	public int	 EffectParticleMax	{ get { return dEffectParticleMax; } }
}