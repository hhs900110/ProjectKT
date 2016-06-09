using UnityEngine;
using System.Collections;
using DefineBase;

#pragma warning disable 0168 // variable declared but not used.
#pragma warning disable 0219 // variable assigned but not used.
#pragma warning disable 0414 // private field assigned but not used.

public sealed partial class Main : MonoBehaviour
{
	private LOAD_STEP m_LoadStep;
	
	public void Start()
	{
		Create();
		enabled	= true;
	}
	
	private void Init()
	{
		Application.runInBackground = true;
		Resources.UnloadUnusedAssets();
		System.GC.Collect();
		
		SetROOTObjects();
		SetResourceObjectROOTChildren();
		TextParserScript	= new TextParser();
		if(!gameObject.GetComponent<DefineBaseManager>())
		{
			gameObject.AddComponent<DefineBaseManager>();
		}
		
		InitGameCamera();
		InitEzGuiCamera();
		
		FullHDScreenSize.x	= 1920;
		FullHDScreenSize.y	= 1080;
		
		UIScreenSizeX2Max	= 1600;
		UIScreenSizeX2Num	= 1.5f;
		
		SetDefineScreen();
		SetGameUICameraInspector();
		SetUICameraInspector();
		
		MainMoveXPercent	= 1.5f;
		MainMoveYPercent	= 1.5f;
		
		InitPlatform();
		InitDevice();
		ApplicationDataPath	= Application.persistentDataPath;
		
		if(DefineBaseManager.inst.GetTouchInput())
		{
			InputManagerScript	= this.gameObject.AddComponent<MobileInputManager>();
		}
		else
		{
			InputManagerScript	= this.gameObject.AddComponent<InputManager>();
		}
		InputManagerScript.Create();
		
		m_LoadStep	= LOAD_STEP._STEP1;
	}
	
#region ILoadingStep
	public void LoadStep()
	{
		switch(m_LoadStep)
		{
		case LOAD_STEP._STEP1:		LoadStep1();		break;
		case LOAD_STEP._STEP1_END:	LoadStep1_End();	break;
		case LOAD_STEP._STEP2:		LoadStep2();		break;
		case LOAD_STEP._STEP2_END:	LoadStep2_End();	break;
		case LOAD_STEP._STEP3:		LoadStep3();		break;
		case LOAD_STEP._STEP3_END:	LoadStep3_End();	break;
		case LOAD_STEP._STEP4:		LoadStep4();		break;
		case LOAD_STEP._STEP4_END:	LoadStep4_End();	break;
		case LOAD_STEP._STEP5:		LoadStep5();		break;
		case LOAD_STEP._STEP5_END:	LoadStep5_End();	break;
		}
	}
	
	public void LoadStep1()
	{
		m_LoadStep	= LOAD_STEP._STEP1_LOADING;
		
		SetGameUnitROOTChildren();
		
		StageManagerScript		= new StageManager();
		StageManagerScript.Create();
		
		GameManagerScript		= new GameManager();
		GameManagerScript.Create();
		
		m_LoadStep	= LOAD_STEP._STEP2;
	}
	
	public void LoadStep2()
	{
		m_LoadStep	= LOAD_STEP._STEP2_LOADING;
		
		if(DefineBaseManager.inst.GetPlatformType() != (int)PLATFORM_TYPE.PLATFORM_WEB)
		{
			SqliteManagerScript		= new SqliteManager();
			SqliteManagerScript.Create();
		}
		
		DataManagerScript		= new DataManager();
		DataManagerScript.Create();
		
		m_LoadStep	= LOAD_STEP._STEP3;
	}
	
	public void LoadStep3()
	{
		m_LoadStep	= LOAD_STEP._STEP3_LOADING;
		
		m_LoadStep	= LOAD_STEP._STEP4;
	}
	
	public void LoadStep4()
	{
		m_LoadStep	= LOAD_STEP._STEP4_LOADING;
		HudManagerScript		= new HudManager();
		HudManagerScript.Create();
	}
	public void LoadStep5()	{}
	
	public void LoadStep1_End()	{}
	public void LoadStep2_End()	{}
	public void LoadStep3_End()	{}
	public void LoadStep4_End()
	{
		EndLoadStep();
	}
	public void LoadStep5_End()	{}
	
	public void LoadStepLoading_End()
	{
		switch(Load_Step)
		{
		case LOAD_STEP._STEP1_LOADING:	Load_Step	= LOAD_STEP._STEP1_END;		break;
		case LOAD_STEP._STEP2_LOADING:	Load_Step	= LOAD_STEP._STEP2_END;		break;
		case LOAD_STEP._STEP3_LOADING:	Load_Step	= LOAD_STEP._STEP3_END;		break;
		case LOAD_STEP._STEP4_LOADING:	Load_Step	= LOAD_STEP._STEP4_END;		break;
		case LOAD_STEP._STEP5_LOADING:	Load_Step	= LOAD_STEP._STEP5_END;		break;
		}
	}
	
	public void EndLoadStep()
	{
		Load_Step	= LOAD_STEP._NULL;
		
		int PlatformType	= DefineBaseManager.inst.GetPlatformType();
		if(PlatformType == (int)PLATFORM_TYPE.PLATFORM_ANDROID
		|| PlatformType == (int)PLATFORM_TYPE.PLATFORM_IPHONE)
		{
			Message((int)MSG_TYPE._PLAY, (int)PLAY_STATE._MAIN);
		}
		else
		{
			Message((int)MSG_TYPE._PLAY, (int)PLAY_STATE._KITTYTURN);
		}
		GetHudManager().GetHudProgressCircle().ClosePopup();
	}
#endregion
	
	private void SetROOTObjects()
	{
		ResourceObject_ROOT									= new GameObject("ResourceObject_ROOT");
		GameUnit_ROOT										= new GameObject("GameUnit_ROOT");
	}
	
	private void SetResourceObjectROOTChildren()
	{
		ResourceObject_MESH									= new GameObject("ResourceObject_MESH");
		ResourceObject_EFFECT								= new GameObject("ResourceObject_EFFECT");
		ResourceObject_EZGUITEXTURE							= new GameObject("ResourceObject_EZGUITEXTURE");
		ResourceObject_EZGUIBUTTON							= new GameObject("ResourceObject_EZGUIBUTTON");
		ResourceObject_EZGUITEXTFIELD						= new GameObject("ResourceObject_EZGUITEXTFIELD");
		ResourceObject_EZGUISCROLLLIST						= new GameObject("ResourceObject_EZGUISCROLLLIST");
		ResourceObject_EZGUISPRITETEXT						= new GameObject("ResourceObject_EZGUISPRITETEXT");
		ResourceObject_EZPROGRESSBAR						= new GameObject("ResourceObject_EZPROGRESSBAR");
		ResourceObject_EZGUIOUTLINETEXT						= new GameObject("ResourceObject_EZGUIOUTLINETEXT");
		ResourceObject_EZGUISLIDER							= new GameObject("ResourceObject_EZGUISLIDER");
		ResourceObject_EZGUIRADIOBUTTON						= new GameObject("ResourceObject_EZGUIRADIOBUTTON");
		ResourceObject_EZGUIMOBILEPREFAB					= new GameObject("ResourceObject_EZGUIMOBILEPREFAB");
		
		ResourceObject_MESH.transform.parent				= ResourceObject_ROOT.transform;
		ResourceObject_EFFECT.transform.parent				= ResourceObject_ROOT.transform;
		ResourceObject_EZGUITEXTURE.transform.parent		= ResourceObject_ROOT.transform;
		ResourceObject_EZGUIBUTTON.transform.parent			= ResourceObject_ROOT.transform;
		ResourceObject_EZGUITEXTFIELD.transform.parent		= ResourceObject_ROOT.transform;
		ResourceObject_EZGUISCROLLLIST.transform.parent		= ResourceObject_ROOT.transform;
		ResourceObject_EZGUISPRITETEXT.transform.parent		= ResourceObject_ROOT.transform;
		ResourceObject_EZPROGRESSBAR.transform.parent		= ResourceObject_ROOT.transform;
		ResourceObject_EZGUIOUTLINETEXT.transform.parent	= ResourceObject_ROOT.transform;
		ResourceObject_EZGUISLIDER.transform.parent			= ResourceObject_ROOT.transform;
		ResourceObject_EZGUIRADIOBUTTON.transform.parent	= ResourceObject_ROOT.transform;
		ResourceObject_EZGUIMOBILEPREFAB.transform.parent	= ResourceObject_ROOT.transform;
	}
	
	private void SetGameUnitROOTChildren()
	{
	}
	
	public void InitGameCamera()
	{
		gameUICamera	= GameObject.Find("GameUICamera");
		gameUICameraManager	= gameUICamera.AddComponent<GameUICameraManager>();
		gameUICameraManager.Create();
	}
	
	public void InitEzGuiCamera()
	{
		Camera.main.depth	= 0;
		
		ezGuiUICamera	= GameObject.Find("UICamera");
		
		ezGuiUICamera.GetComponent<Camera>().clearFlags		= CameraClearFlags.Depth;
		ezGuiUICamera.GetComponent<Camera>().cullingMask	= (1 << LayerMask.NameToLayer("GUI"));
		ezGuiUICamera.GetComponent<Camera>().orthographic	= true;
		ezGuiUICamera.GetComponent<Camera>().near			=  -1.0f;
		ezGuiUICamera.GetComponent<Camera>().far			= 100.0f;
		ezGuiUICamera.GetComponent<Camera>().depth			= 2;
		
		ezGuiUIManager = GameObject.Find("UIManager");
	}
	
	public void InitPlatform()
	{
			 if(Application.platform == RuntimePlatform.Android)
		{
			DefineBaseManager.inst.SetPlatformType((int)PLATFORM_TYPE.PLATFORM_ANDROID);
		}
		else if(Application.platform == RuntimePlatform.IPhonePlayer)
		{
			DefineBaseManager.inst.SetPlatformType((int)PLATFORM_TYPE.PLATFORM_IPHONE);
		}
		else if(Application.platform == RuntimePlatform.WindowsWebPlayer
			 || Application.platform == RuntimePlatform.OSXWebPlayer)
		{
			DefineBaseManager.inst.SetPlatformType((int)PLATFORM_TYPE.PLATFORM_WEB);
		}
		else if(Application.platform == RuntimePlatform.WindowsEditor)
		{
		}
		else if(Application.platform == RuntimePlatform.OSXEditor)
		{
			DefineBaseManager.inst.SetPlatformType((int)PLATFORM_TYPE.PLATFORM_IPHONE);
		}
	}
	
	public void InitDevice()
	{
		Application.targetFrameRate	= 60;
		if(DefineBaseManager.inst.GetPlatformType() == (int)PLATFORM_TYPE.PLATFORM_WEB
		|| DefineBaseManager.inst.GetPlatformType() == (int)PLATFORM_TYPE.PLATFORM_EDITOR_WIN)
		{
			DefineBaseManager.inst.SetGameSize(DefineBaseManager.inst.GameBaseWidth, DefineBaseManager.inst.GameBaseHeight);
			DefineBaseManager.inst.SetTouchInput(false);
			
			gameUICamera.SetActiveRecursively(true);
			ezGuiUICamera.SetActiveRecursively(true);
			ezGuiUIManager.SetActiveRecursively(true);
			
			Camera.main.GetComponent<GUILayer>().enabled	= true;
			Camera.main.cullingMask	= ~(1 << LayerMask.NameToLayer("GUI"))
									& ~(1 << LayerMask.NameToLayer("GameCamera"));
		}
		else if(DefineBaseManager.inst.GetPlatformType() == (int)PLATFORM_TYPE.PLATFORM_IPHONE
			 || DefineBaseManager.inst.GetPlatformType() == (int)PLATFORM_TYPE.PLATFORM_EDITOR_OSX)
		{
			DefineBaseManager.inst.SetGameSize(DefineBaseManager.inst.GameBaseWidth, DefineBaseManager.inst.GameBaseHeight);
			
			gameUICamera.SetActiveRecursively(true);
			ezGuiUICamera.SetActiveRecursively(true);
			ezGuiUIManager.SetActiveRecursively(true);
			
			Camera.main.cullingMask	= ~(1 << LayerMask.NameToLayer("GUI"))
									& ~(1 << LayerMask.NameToLayer("GameCamera"));
			Camera.main.GetComponent<GUILayer>().enabled	= false;
		}
		else if(DefineBaseManager.inst.GetPlatformType() == (int)PLATFORM_TYPE.PLATFORM_ANDROID)
		{
			DefineBaseManager.inst.SetGameSize(DefineBaseManager.inst.GameBaseWidth, DefineBaseManager.inst.GameBaseHeight);
			
			gameUICamera.SetActiveRecursively(true);
			ezGuiUICamera.SetActiveRecursively(true);
			ezGuiUIManager.SetActiveRecursively(true);
			
			Camera.main.cullingMask	= ~(1 << LayerMask.NameToLayer("GUI"))
									& ~(1 << LayerMask.NameToLayer("GameCamera"));
		}
		else
		{
			DefineBaseManager.inst.SetGameSize(DefineBaseManager.inst.GameBaseWidth, DefineBaseManager.inst.GameBaseHeight);
			DefineBaseManager.inst.SetTouchInput(false);
			
			gameUICamera.SetActiveRecursively(true);
			ezGuiUICamera.SetActiveRecursively(true);
			ezGuiUIManager.SetActiveRecursively(true);
			
			Camera.main.cullingMask	= ~(1 << LayerMask.NameToLayer("GUI"))
									& ~(1 << LayerMask.NameToLayer("GameCamera"));
		}
		
#if UNITY_EDITOR
#elif UNITY_IPHONE || UNITY_ANDROID
		iPhoneSettings.screenCanDarken = false;
		DefineBaseManager.inst.SetTouchInput(true);
#endif
	}
	
	public void SetDefineScreen()
	{
		DefineBaseManager.inst.SetScreenSize(Screen.width, Screen.height);
		DefineBaseManager.inst.SetGameSize(DefineBaseManager.inst.GameBaseWidth, DefineBaseManager.inst.GameBaseHeight);
		
		Vector2 MoveSpeed	= new Vector2(Screen.width, Screen.height);
		StarMoveSpeed 		= 1.0f;
		
#if UNITY_IPHONE
		if(iPhone.generation == iPhoneGeneration.iPhone5)
		{
			StarMoveSpeed 		= 5.0f;
		}
# elif UNITY_ANDROID
		StarMoveSpeed 		= 5.0f;
#endif
		
		CustomizeMoveSpeed	= (float)(MoveSpeed.magnitude / 28.5);
		ZoomInOutMoveSpeed	= (float)(MoveSpeed.magnitude / 48.0);
//		float Gap = 0.0f;
//		
//		Gap = 0.5f* ( GetGameHeight() * Screen.width / Screen.height - GetGameWidth());
//		DefineBaseManager.inst.SetGameSizeGap(Gap);
		float GameScreenWidth	= GetGameWidth();
		StarMoveZoomDpi = Screen.dpi / GameScreenWidth;
		StarMoveDpi		= 0.5f;
	}
	
	public void SetGameUICameraInspector()
	{
		gameUICameraManager.SetCameraInspector();
	}
	
	public void SetUICameraInspector()
	{
		SetUICameraInspector(GetUICamera());
	}
	
	public void SetUICameraInspector(GameObject _UICamera)
	{
		float NowScreenWidth	= GetScreenWidth();
		float NowScreenHeight	= GetScreenHeight();
		float GameScreenWidth	= GetGameWidth();
		float GameScreenHeight	= GetGameHeight();
		_UICamera.transform.position	= new Vector3(GameScreenWidth/2.0f, -GameScreenHeight/2.0f, 0);
		
		Rect  CameraRect;
		
		if((NowScreenHeight/NowScreenWidth) < (GameScreenHeight/GameScreenWidth))
		{
			float Ret	= (GameScreenWidth*NowScreenHeight)/(GameScreenHeight*NowScreenWidth);
			CameraRect	= new Rect((1 - Ret)/2, 0, Ret, 1.0f);
		}
		else
		{
			float Ret	= (GameScreenHeight*NowScreenWidth)/(GameScreenWidth*NowScreenHeight);
			CameraRect	= new Rect(0, (1 - Ret)/2, 1.0f, Ret);
		}
		
		_UICamera.GetComponent<Camera>().rect				= CameraRect;
		_UICamera.GetComponent<Camera>().orthographicSize	= GameScreenHeight/2.0f;
	}
}