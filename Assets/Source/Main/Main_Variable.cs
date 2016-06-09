using UnityEngine;
using System.Collections;
using DefineBase;

#pragma warning disable 0168 // variable declared but not used.
#pragma warning disable 0219 // variable assigned but not used.
#pragma warning disable 0414 // private field assigned but not used.

public sealed partial class Main : MonoBehaviour, ILoadingStep, IClassBase
{
	private static Main	m_inst	= null;
	
	public static Main				inst	{ get { return m_inst; } }
	public static DataManager		data	{ get { return DataManagerScript; } }
	public static InputManagerBase	input	{ get { return InputManagerScript; } }
	public static TextParser		parser	{ get { return TextParserScript; } }
	public static StageManager		stage	{ get { return StageManagerScript; } }
	public static GameManager		game	{ get { return GameManagerScript; } }
	
	void Awake()
	{
		if(m_inst == null)	{ m_inst	= this; }
	}
	
	Main()	{}
	
	// Resource Objects
	private static GameObject		ResourceObject_ROOT;
	private static GameObject		ResourceObject_MESH;
	private static GameObject		ResourceObject_EFFECT;
	private static GameObject		ResourceObject_EZGUITEXTURE;
	private static GameObject		ResourceObject_EZGUIBUTTON;
	private static GameObject		ResourceObject_EZGUITEXTFIELD;
	private static GameObject		ResourceObject_EZGUISCROLLLIST;
	private static GameObject		ResourceObject_EZGUISPRITETEXT;
	private static GameObject		ResourceObject_EZPROGRESSBAR;
	private static GameObject		ResourceObject_EZGUIOUTLINETEXT;
	private static GameObject		ResourceObject_EZGUISLIDER;
	private static GameObject		ResourceObject_EZGUIRADIOBUTTON;
	private static GameObject		ResourceObject_EZGUIMOBILEPREFAB;
	
	// GameUnit Objects
	private static GameObject		GameUnit_ROOT;
	
	
	// GameUnit Scripts
	private static DataManager		DataManagerScript		= null;
	private static InputManagerBase	InputManagerScript		= null;
	private static TextParser		TextParserScript		= null;
	private static SqliteManager	SqliteManagerScript		= null;
	
	private static StageManager		StageManagerScript		= null;
	private static GameManager		GameManagerScript		= null;
	
	private static HudManager		HudManagerScript		= null;
	
	// Camera
	private GameObject				gameUICamera;
	private GameUICameraManager		gameUICameraManager;
	
	private GameObject				ezGuiUICamera;
	private GameObject				ezGuiUIManager;
	
	private System.DateTime			LoginTime;
	private int						LoadingCountNum		= 0;
	private const int				LoadingMaxCountNum	= 156;
	
	private string	ApplicationDataPath;
	private bool	bIsApplicationQuit;
	
	private float	MainMoveXPercent;
	private float	MainMoveYPercent;
	
	private float	StarMoveSpeed;
	private float	StarMoveDpi;
	private float	StarMoveZoomDpi;
	private float	CustomizeMoveSpeed;
	private float	ZoomInOutMoveSpeed;
	
	private Vector2	FullHDScreenSize;
	private int		UIScreenSizeX2Max;
	private float	UIScreenSizeX2Num;
	
	// Is Get //
	// Get Resource Objects //
	public GameObject				GetResourceObject_ROOT()				{ return ResourceObject_ROOT; }
	public GameObject				GetResourceObject_MESH()				{ return ResourceObject_MESH; }
	public GameObject				GetResourceObject_EFFECT()				{ return ResourceObject_EFFECT; }
	public GameObject				GetResourceObject_EZGUITEXTURE()		{ return ResourceObject_EZGUITEXTURE; }
	public GameObject				GetResourceObject_EZGUIBUTTON()			{ return ResourceObject_EZGUIBUTTON; }
	public GameObject				GetResourceObject_EZGUITEXTFIELD()		{ return ResourceObject_EZGUITEXTFIELD; }
	public GameObject				GetResourceObject_EZGUISCROLLLIST()		{ return ResourceObject_EZGUISCROLLLIST; }
	public GameObject				GetResourceObject_EZGUISPRITETEXT()		{ return ResourceObject_EZGUISPRITETEXT; }
	public GameObject				GetResourceObject_EZPROGRESSBAR()		{ return ResourceObject_EZPROGRESSBAR; }
	public GameObject				GetResourceObject_EZGUIOUTLINETEXT()	{ return ResourceObject_EZGUIOUTLINETEXT; }
	public GameObject				GetResourceObject_EZGUISLIDER()			{ return ResourceObject_EZGUISLIDER; }
	public GameObject				GetResourceObject_EZGUIRADIOBUTTON()	{ return ResourceObject_EZGUIRADIOBUTTON; }
	public GameObject				GetResourceObject_EZGUIMOBILEPREFAB()	{ return ResourceObject_EZGUIMOBILEPREFAB; }
	
	// Get GameUnit Objects //
	public GameObject				GetGameUnit_ROOT()						{ return GameUnit_ROOT; }
	
	// Get GameUnit Scripts //
	public SqliteManager			GetSqliteManager()						{ return SqliteManagerScript; }
	public StageManager				GetStageManager()						{ return StageManagerScript; }
	public HudManager				GetHudManager()							{ return HudManagerScript; }
	
	public float					GetGameWidth()							{ return DefineBaseManager.inst.GetGameWidth(); }
	public float					GetGameHeight()							{ return DefineBaseManager.inst.GetGameHeight(); }
	public float					GetScreenWidth()						{ return DefineBaseManager.inst.GetScreenWidth(); }
	public float					GetScreenHeight()						{ return DefineBaseManager.inst.GetScreenHeight(); }
	public float					GetGameSizeGap()						{ return DefineBaseManager.inst.GetGameSizeGap(); }
	public int						GetScreenType()							{ return DefineBaseManager.inst.ScreenType; }
	
	public GameUICameraManager		GetGameUICameraManager()				{ return gameUICameraManager; }
	public GameObject				GetGameUICamera()						{ return gameUICamera; }
	public GameObject				GetUICamera()							{ return ezGuiUICamera; }
	
	public Vector2	GetFullHDScreenSize()		{ return FullHDScreenSize; }
	public int		GetUIScreenSizeX2Max()		{ return UIScreenSizeX2Max; }
	public float	GetUIScreenSizeX2Num()		{ return UIScreenSizeX2Num; }
	public string	GetApplicationDataPath()	{ return ApplicationDataPath; }
}