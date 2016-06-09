using UnityEngine;
using System.Collections;
using DefineBase;

#pragma warning disable 0168 // variable declared but not used.
#pragma warning disable 0219 // variable assigned but not used.
#pragma warning disable 0414 // private field assigned but not used.

public abstract class ResourceLoad
{
	public static string CheckHeight	= "[height]";
	
#region SetEzGui
	public static void SetSpriteText_OutLine(GameObject ObjectFont, string FrontPath, string BackPath)
	{
		Object FrontObject	= Resources.Load(FrontPath);
		Object BackObject	= Resources.Load(BackPath);
		SetSpriteText_OutLine(ObjectFont, FrontObject, BackObject);
		Object.Destroy(FrontObject);
		Object.Destroy(BackObject);
	}
	
	public static void SetSpriteText_OutLine(GameObject ObjectFont, Object FrontObject, Object BackObject)
	{
		GameObject	Front		= (GameObject)Object.Instantiate(FrontObject,new Vector3(0, 0, -1), Quaternion.identity);
		GameObject	Back		= (GameObject)Object.Instantiate(BackObject, Vector3.zero, Quaternion.identity);
		Front.transform.parent	= ObjectFont.transform;
		Back.transform.parent	= ObjectFont.transform;
		
		ObjectFont.AddComponent<EzGui_SpriteText_Outline>();
		ObjectFont.GetComponent<EzGui_SpriteText_Outline>().Create();
		ObjectFont.GetComponent<EzGui_SpriteText_Outline>().SetEZGUI(Front.GetComponent<SpriteText>(), Back.GetComponent<SpriteText>());
		
		ObjectFont.layer		= LayerMask.NameToLayer("GUI");
	}
#endregion
	
#region GetEzGui
	public static GameObject GetSpriteText(Object PrefabObject)
	{
		if(PrefabObject == null)
		{
			Debug.LogError("PREFAB IS NULL!!!!!");
			return null;
		}
		
		GameObject Front		= (GameObject)Object.Instantiate(PrefabObject, Vector3.zero, Quaternion.identity);
		
		Front.AddComponent<EzGui_SpriteText>();
		Front.GetComponent<EzGui_SpriteText>().Create();
		Front.GetComponent<EzGui_SpriteText>().SetEZGUI(Front.GetComponent<SpriteText>());
		
		Front.layer		= LayerMask.NameToLayer("GUI");
		
		return Front;
	}
	
	public static GameObject GetEzGuiTexture(Object PrefabObject)
	{
		if(PrefabObject == null)
		{
			Debug.LogError("PREFAB IS NULL!!!!!");
			return null;
		}
		
		GameObject Prefab	= (GameObject)Object.Instantiate(PrefabObject, Vector3.zero, Quaternion.identity);
		
		int TextureSizeX		= (int)Prefab.GetComponent<UIButton>().ImageSize.x;
		int TextureSizeY		= (int)Prefab.GetComponent<UIButton>().ImageSize.y;
		
		if(!Prefab.GetComponent<EzGui_Texture>())
		{
			Prefab.AddComponent<EzGui_Texture>();
		}
		Prefab.GetComponent<EzGui_Texture>().Create();
		Prefab.GetComponent<EzGui_Texture>().SetEZGUI(Prefab.GetComponent<UIButton>());
		Prefab.GetComponent<EzGui_Texture>().SetTextureSize((float)TextureSizeX, (float)TextureSizeY);
		
		Prefab.layer			= LayerMask.NameToLayer("GUI");
		
		return Prefab;
	}
	
	public static GameObject GetEzGuiTexture(GameObject PrefabObject)
	{
		if(PrefabObject == null)
		{
			Debug.LogError("PREFAB IS NULL!!!!!");
			return null;
		}
		
		GameObject Prefab	= (GameObject)GameObject.Instantiate(PrefabObject, Vector3.zero, Quaternion.identity);
		PrefabObject.SetActiveRecursively(false);
		
		int TextureSizeX		= (int)Prefab.GetComponent<UIButton>().ImageSize.x;
		int TextureSizeY		= (int)Prefab.GetComponent<UIButton>().ImageSize.y;
		
		if(!Prefab.GetComponent<EzGui_Texture>())
		{
			Prefab.AddComponent<EzGui_Texture>();
		}
		Prefab.GetComponent<EzGui_Texture>().Create();
		Prefab.GetComponent<EzGui_Texture>().SetEZGUI(Prefab.GetComponent<UIButton>());
		Prefab.GetComponent<EzGui_Texture>().SetTextureSize((float)TextureSizeX, (float)TextureSizeY);
		
		Prefab.layer			= LayerMask.NameToLayer("GUI");
		
		return Prefab;
	}
	
	public static GameObject GetEzGuiButton(Object PrefabObject)
	{
		if(PrefabObject == null)
		{
			Debug.LogError("PREFAB IS NULL!!!!!");
			return null;
		}
		
		GameObject	Prefab		= (GameObject)Object.Instantiate(PrefabObject, Vector3.zero, Quaternion.identity);
		
		int TextureSizeX		= (int)Prefab.GetComponent<UIButton>().ImageSize.x;
		int TextureSizeY		= (int)Prefab.GetComponent<UIButton>().ImageSize.y;
		
		Prefab.AddComponent<EzGui_Button>();
		Prefab.GetComponent<EzGui_Button>().Create();
		Prefab.GetComponent<EzGui_Button>().SetEZGUI(Prefab.GetComponent<UIButton>());
		Prefab.GetComponent<EzGui_Button>().SetTextureSize((float)TextureSizeX, (float)TextureSizeY);
		
		Prefab.layer			= LayerMask.NameToLayer("GUI");
		
		return Prefab;
	}
#endregion
	
	public static void CheckMaxSizeEzGui(EzGui_Base _ObjectBase, float _MaxX, float _MaxY)
	{
		CheckMaxSizeEzGui(_ObjectBase, _MaxX, _MaxY, 1.0f);
	}
	
	public static void CheckMaxSizeEzGui(EzGui_Base _ObjectBase, float _MaxX, float _MaxY, float _fImagePercentage)
	{
		float SizeX	= _ObjectBase.GetTextureSizeX();
		float SizeY	= _ObjectBase.GetTextureSizeY();
		float Ratio	= GetImageRatio(SizeX, SizeY, _MaxX, _MaxY, _fImagePercentage);
		_ObjectBase.SetTextureSize(SizeX * _fImagePercentage * Ratio, SizeY * _fImagePercentage * Ratio);
	}
	
	public static float GetImageRatio(float _fImageSizeX, float _fImageSizeY, float _MaxX, float _MaxY, float _fImagePercentage)
	{
		float RatioX	= _MaxX / (_fImageSizeX * _fImagePercentage);
		float RatioY	= _MaxY / (_fImageSizeY * _fImagePercentage);
		
		if(RatioX > RatioY)
		{
			if(RatioY < 1.0f)
			{
				return RatioY;
			}
		}
		else if(RatioY > RatioX)
		{
			if(RatioX < 1.0f)
			{
				return RatioX;
			}
		}
		
		return 1.0f;
	}
	
	public static Object GetObject(string ObjectName, string _ObjectPath)
	{
		string ObjectPath	= _ObjectPath.Replace(CheckHeight, DefineBaseManager.inst.GameBaseHeight.ToString());
		if(!IsHaveResource(ObjectName, ObjectPath))
		{ return null; }
		
		return Resources.Load(ObjectPath);
	}
	
	public static GameObject GetGameObject(string ObjectName, string _ObjectPath)
	{
		Object tmpObj	= GetObject(ObjectName, _ObjectPath);
		if(tmpObj == null)
		{ return null; }
		
		return (GameObject)GameObject.Instantiate(tmpObj, Vector3.zero, Quaternion.identity);
	}
	
#region GameResourceManager
	private static bool IsHaveResource(string ObjectName, string ObjectPath)
	{
		if(Resources.Load(ObjectPath) == null)
		{
			Debug.LogError("Wrong Path!!!! " + ObjectName + " // " + ObjectPath);
			
			return false;
		}
		return true;
	}
	
	public static GameObject GetMOBILE_PREFAB_IMAGE(string ObjectName, string _ObjectPath)
	{
		GameObject CurObject		= GetGameObject(ObjectName, _ObjectPath);
		if(CurObject == null)
		{ return null ; }
		
		CurObject.GetComponent<UIButton>().SetAnchor(UIButton.ANCHOR_METHOD.BOTTOM_CENTER);
		CurObject.GetComponent<UIButton>().width	= CurObject.GetComponent<UIButton>().ImageSize.x;
		CurObject.GetComponent<UIButton>().height	= CurObject.GetComponent<UIButton>().ImageSize.y;
		
		CurObject.transform.position	= new Vector3(50.0f, -115.0f, 0.0f);
		CurObject.active			= false;
		CurObject.name				= ObjectName;
		CurObject.layer				= LayerMask.NameToLayer("GUI");
		CurObject.transform.parent	= Main.inst.GetResourceObject_EZGUIMOBILEPREFAB().transform;
		
		return CurObject;
	}
	
	public static GameObject GetEZGUI_TEXTURE(string ObjectName, string _ObjectPath,
		int PosX, int PosY, int PosZ, string BasePosType)
	{
		GameObject CurObject		= GetGameObject(ObjectName, _ObjectPath);
		if(CurObject == null)
		{ return null ; }
		
		int TextureSizeX			= (int)CurObject.GetComponent<UIButton>().ImageSize.x;
		int TextureSizeY			= (int)CurObject.GetComponent<UIButton>().ImageSize.y;
		
		CurObject.AddComponent<EzGui_Texture>();
		CurObject.GetComponent<EzGui_Texture>().Create();
		CurObject.GetComponent<EzGui_Texture>().SetName(ObjectName);
		CurObject.GetComponent<EzGui_Texture>().SetEZGUI(CurObject.GetComponent<UIButton>());
		CurObject.GetComponent<EzGui_Texture>().SetTextureSize((float)TextureSizeX, (float)TextureSizeY);
		CurObject.GetComponent<EzGui_Texture>().SetBasePos(BasePosType);
		CurObject.GetComponent<EzGui_Texture>().SetPos((float)PosX, (float)PosY, (float)PosZ);
		CurObject.GetComponent<EzGui_Texture>().SetOriginPos();
		CurObject.GetComponent<EzGui_Texture>().SetValid(false);
		
		CurObject.name				= ObjectName;
		CurObject.layer				= LayerMask.NameToLayer("GUI");
		CurObject.transform.parent	= Main.inst.GetResourceObject_EZGUITEXTURE().transform;
		
		return CurObject;
	}
	
	public static GameObject GetEZGUI_BUTTON_OUTLINE(string ObjectName, string _ObjectPath, string FrontPath, string BackPath,
		int PosX, int PosY, int PosZ, string Anchor, string Allignment, string Text, string BasePosType)
	{
		if(!IsHaveResource(ObjectName, FrontPath))
		{ return null; }
		if(!IsHaveResource(ObjectName, BackPath))
		{ return null; }
		
		GameObject CurObject		= GetGameObject(ObjectName, _ObjectPath);
		if(CurObject == null)
		{ return null ; }
		
		GameObject FrontObject		= (GameObject)GameObject.Instantiate(Resources.Load(FrontPath), new Vector3(0, 0, -2), Quaternion.identity);
		FrontObject.transform.parent	= CurObject.transform;
		
		GameObject BackObject		= (GameObject)GameObject.Instantiate(Resources.Load(BackPath), new Vector3(0, 0, -1), Quaternion.identity);
		BackObject.transform.parent		= CurObject.transform;
		
		int TextureSizeX			= (int)CurObject.GetComponent<UIButton>().ImageSize.x;
		int TextureSizeY			= (int)CurObject.GetComponent<UIButton>().ImageSize.y;
		
		CurObject.AddComponent<EzGui_Button_Outline>();
		CurObject.GetComponent<EzGui_Button_Outline>().Create();
		CurObject.GetComponent<EzGui_Button_Outline>().SetName(ObjectName);
		CurObject.GetComponent<EzGui_Button_Outline>().SetEZGUI(CurObject.GetComponent<UIButton>(), FrontObject.GetComponent<SpriteText>(), BackObject.GetComponent<SpriteText>());
		CurObject.GetComponent<EzGui_Button_Outline>().SetTextureSize((float)TextureSizeX, (float)TextureSizeY);
		CurObject.GetComponent<EzGui_Button_Outline>().SetBasePos(BasePosType);
		CurObject.GetComponent<EzGui_Button_Outline>().SetPos((float)PosX, (float)PosY, (float)PosZ);
		CurObject.GetComponent<EzGui_Button_Outline>().SetAnchor(Anchor);
		CurObject.GetComponent<EzGui_Button_Outline>().SetAllignment(Allignment);
		CurObject.GetComponent<EzGui_Button_Outline>().SetColor(new Color(1,1,1), new Color(0,0,0));
		CurObject.GetComponent<EzGui_Button_Outline>().SetText(Text);
		CurObject.GetComponent<EzGui_Button_Outline>().SetOriginPos();
		CurObject.GetComponent<EzGui_Button_Outline>().SetValid(false);
		
		CurObject.name				= ObjectName;
		CurObject.layer				= LayerMask.NameToLayer("GUI");
		CurObject.transform.parent	= Main.inst.GetResourceObject_EZGUIBUTTON().transform;
		
		return CurObject;
	}
	
	public static GameObject GetEZGUI_BUTTON(string ObjectName, string _ObjectPath,
		int PosX, int PosY, int PosZ, string BasePosType)
	{
		GameObject CurObject		= GetGameObject(ObjectName, _ObjectPath);
		if(CurObject == null)
		{ return null ; }
		
		int TextureSizeX			= (int)CurObject.GetComponent<UIButton>().ImageSize.x;
		int TextureSizeY			= (int)CurObject.GetComponent<UIButton>().ImageSize.y;
		
		CurObject.AddComponent<EzGui_Button>();
		CurObject.GetComponent<EzGui_Button>().Create();
		CurObject.GetComponent<EzGui_Button>().SetName(ObjectName);
		CurObject.GetComponent<EzGui_Button>().SetEZGUI(CurObject.GetComponent<UIButton>());
		CurObject.GetComponent<EzGui_Button>().SetTextureSize((float)TextureSizeX, (float)TextureSizeY);
		CurObject.GetComponent<EzGui_Button>().SetBasePos(BasePosType);
		CurObject.GetComponent<EzGui_Button>().SetPos((float)PosX, (float)PosY, (float)PosZ);
		CurObject.GetComponent<EzGui_Button>().SetOriginPos();
		CurObject.GetComponent<EzGui_Button>().SetValid(false);
		
		CurObject.name				= ObjectName;
		CurObject.layer				= LayerMask.NameToLayer("GUI");
		CurObject.transform.parent	= Main.inst.GetResourceObject_EZGUIBUTTON().transform;
		
		return CurObject;
	}
	
	public static GameObject GetEZGUI_BUTTON(string ObjectName, string _ObjectPath,
		int PosX, int PosY, int PosZ, HUD_BASE_POS BasePosType)
	{
		GameObject CurObject		= GetGameObject(ObjectName, _ObjectPath);
		if(CurObject == null)
		{ return null ; }
		
		int TextureSizeX			= (int)CurObject.GetComponent<UIButton>().ImageSize.x;
		int TextureSizeY			= (int)CurObject.GetComponent<UIButton>().ImageSize.y;
		
		CurObject.AddComponent<EzGui_Button>();
		CurObject.GetComponent<EzGui_Button>().Create();
		CurObject.GetComponent<EzGui_Button>().SetName(ObjectName);
		CurObject.GetComponent<EzGui_Button>().SetEZGUI(CurObject.GetComponent<UIButton>());
		CurObject.GetComponent<EzGui_Button>().SetTextureSize((float)TextureSizeX, (float)TextureSizeY);
		CurObject.GetComponent<EzGui_Button>().SetBasePos(BasePosType);
		CurObject.GetComponent<EzGui_Button>().SetPos((float)PosX, (float)PosY, (float)PosZ);
		CurObject.GetComponent<EzGui_Button>().SetOriginPos();
		CurObject.GetComponent<EzGui_Button>().SetValid(false);
		
		CurObject.name				= ObjectName;
		CurObject.layer				= LayerMask.NameToLayer("GUI");
		CurObject.transform.parent	= Main.inst.GetResourceObject_EZGUIBUTTON().transform;
		
		return CurObject;
	}
	
	public static GameObject GetEZGUI_RADIOBUTTON(string ObjectName, string _ObjectPath,
		int PosX, int PosY, int PosZ, string BasePosType)
	{
		GameObject CurObject		= GetGameObject(ObjectName, _ObjectPath);
		if(CurObject == null)
		{ return null ; }
		
		int TextureSizeX			= (int)CurObject.GetComponent<UIRadioBtn>().ImageSize.x;
		int TextureSizeY			= (int)CurObject.GetComponent<UIRadioBtn>().ImageSize.y;
		
		CurObject.AddComponent<EzGui_RadioButton>();
		CurObject.GetComponent<EzGui_RadioButton>().Create();
		CurObject.GetComponent<EzGui_RadioButton>().SetName(ObjectName);
		CurObject.GetComponent<EzGui_RadioButton>().SetEZGUI(CurObject.GetComponent<UIRadioBtn>());
		CurObject.GetComponent<EzGui_RadioButton>().SetTextureSize((float)TextureSizeX, (float)TextureSizeY);
		CurObject.GetComponent<EzGui_RadioButton>().SetBasePos(BasePosType);
		CurObject.GetComponent<EzGui_RadioButton>().SetPos((float)PosX, (float)PosY, (float)PosZ);
		CurObject.GetComponent<EzGui_RadioButton>().SetOriginPos();
		CurObject.GetComponent<EzGui_RadioButton>().SetValid(false);
		
		CurObject.name				= ObjectName;
		CurObject.layer				= LayerMask.NameToLayer("GUI");
		CurObject.transform.parent	= Main.inst.GetResourceObject_EZGUIRADIOBUTTON().transform;
		
		return CurObject;
	}
	
	public static GameObject GetEZGUI_TEXTFIELD(string ObjectName, string _ObjectPath,
		int PosX, int PosY, int PosZ, string BasePosType)
	{
		GameObject CurObject		= GetGameObject(ObjectName, _ObjectPath);
		if(CurObject == null)
		{ return null ; }
		
		int TextureSizeX			= (int)CurObject.GetComponent<UITextField>().ImageSize.x;
		int TextureSizeY			= (int)CurObject.GetComponent<UITextField>().ImageSize.y;
		
		CurObject.AddComponent<EzGui_TextField>();
		CurObject.GetComponent<EzGui_TextField>().Create();
		CurObject.GetComponent<EzGui_TextField>().SetName(ObjectName);
		CurObject.GetComponent<EzGui_TextField>().SetEZGUI(CurObject.GetComponent<UITextField>());
		CurObject.GetComponent<EzGui_TextField>().SetTextureSize((float)TextureSizeX, (float)TextureSizeY);
		CurObject.GetComponent<EzGui_TextField>().SetBasePos(BasePosType);
		CurObject.GetComponent<EzGui_TextField>().SetPos((float)PosX, (float)PosY, (float)PosZ);
		CurObject.GetComponent<EzGui_TextField>().SetValid(false);
		
		CurObject.name				= ObjectName;
		CurObject.layer				= LayerMask.NameToLayer("GUI");
		CurObject.transform.parent	= Main.inst.GetResourceObject_EZGUITEXTFIELD().transform;
		
		return CurObject;
	}
	
	public static GameObject GetEZGUI_SLIDER(string ObjectName, string _ObjectPath,
		int PosX, int PosY, int PosZ, string Direction, string Anchor, string BasePosType)
	{
		GameObject CurObject		= GetGameObject(ObjectName, _ObjectPath);
		if(CurObject == null)
		{ return null ; }
		
		int TextureSizeX			= (int)CurObject.GetComponent<UISlider>().ImageSize.x;
		int TextureSizeY			= (int)CurObject.GetComponent<UISlider>().ImageSize.y;
		
		CurObject.AddComponent<EzGui_Slider>();
		CurObject.GetComponent<EzGui_Slider>().Create();
		CurObject.GetComponent<EzGui_Slider>().SetName(ObjectName);
		CurObject.GetComponent<EzGui_Slider>().SetEZGUI(CurObject.GetComponent<UISlider>());
		CurObject.GetComponent<EzGui_Slider>().SetTextureSize((float)TextureSizeX, (float)TextureSizeY);
		CurObject.GetComponent<EzGui_Slider>().SetDirection(Direction);
		CurObject.GetComponent<EzGui_Slider>().SetAnchor(Anchor);
		CurObject.GetComponent<EzGui_Slider>().SetBasePos(BasePosType);
		CurObject.GetComponent<EzGui_Slider>().SetPos((float)PosX, (float)PosY, (float)PosZ);
		CurObject.GetComponent<EzGui_Slider>().SetValid(false);
		
		CurObject.name				= ObjectName;
		CurObject.layer				= LayerMask.NameToLayer("GUI");
		CurObject.transform.parent	= Main.inst.GetResourceObject_EZGUISLIDER().transform;
		
		return CurObject;
	}
	
	public static GameObject GetEZGUI_PROGRESSBAR(string ObjectName, string _ObjectPath,
		int PosX, int PosY, int PosZ, string BasePosType)
	{
		GameObject CurObject		= GetGameObject(ObjectName, _ObjectPath);
		if(CurObject == null)
		{ return null ; }
		
		int TextureSizeX			= (int)CurObject.GetComponent<UIProgressBar>().ImageSize.x;
		int TextureSizeY			= (int)CurObject.GetComponent<UIProgressBar>().ImageSize.y;
		
		CurObject.AddComponent<EzGui_ProgressBar>();
		CurObject.GetComponent<EzGui_ProgressBar>().Create();
		CurObject.GetComponent<EzGui_ProgressBar>().SetName(ObjectName);
		CurObject.GetComponent<EzGui_ProgressBar>().SetEZGUI(CurObject.GetComponent<UIProgressBar>());
		CurObject.GetComponent<EzGui_ProgressBar>().SetTextureSize((float)TextureSizeX, (float)TextureSizeY);
		CurObject.GetComponent<EzGui_ProgressBar>().SetBasePos(BasePosType);
		CurObject.GetComponent<EzGui_ProgressBar>().SetPos((float)PosX, (float)PosY, (float)PosZ);
		CurObject.GetComponent<EzGui_ProgressBar>().SetValid(false);
		
		CurObject.name				= ObjectName;
		CurObject.layer				= LayerMask.NameToLayer("GUI");
		CurObject.transform.parent	= Main.inst.GetResourceObject_EZGUITEXTFIELD().transform;
		
		return CurObject;
	}
	
	public static GameObject GetEZGUI_SPRITETEXT(string ObjectName, string _ObjectPath,
		int PosX, int PosY, int PosZ, string Anchor, string Allignment, string Text, string BasePosType)
	{
		GameObject CurObject		= GetGameObject(ObjectName, _ObjectPath);
		if(CurObject == null)
		{ return null ; }
		
		CurObject.AddComponent<EzGui_SpriteText>();
		CurObject.GetComponent<EzGui_SpriteText>().Create();
		CurObject.GetComponent<EzGui_SpriteText>().SetName(ObjectName);
		CurObject.GetComponent<EzGui_SpriteText>().SetEZGUI(CurObject.GetComponent<SpriteText>());
		CurObject.GetComponent<EzGui_SpriteText>().SetBasePos(BasePosType);
		CurObject.GetComponent<EzGui_SpriteText>().SetPos((float)PosX, (float)PosY, (float)PosZ);
		CurObject.GetComponent<EzGui_SpriteText>().SetAnchor(Anchor);
		CurObject.GetComponent<EzGui_SpriteText>().SetAllignment(Allignment);
		CurObject.GetComponent<EzGui_SpriteText>().SetText(Text);
		CurObject.GetComponent<EzGui_SpriteText>().SetColor(Color.black);
		CurObject.GetComponent<EzGui_SpriteText>().SetValid(false);
		
		CurObject.name		= ObjectName;
		CurObject.layer		= LayerMask.NameToLayer("GUI");
		CurObject.transform.parent	= Main.inst.GetResourceObject_EZGUISPRITETEXT().transform;
		
		return CurObject;
	}
	
	public static GameObject GetEZGUI_SPRITETEXT(string ObjectName, string _ObjectPath,
		int PosX, int PosY, int PosZ, SpriteText.Anchor_Pos Anchor, SpriteText.Alignment_Type Allignment, string Text, HUD_BASE_POS BasePosType)
	{
		GameObject CurObject		= GetGameObject(ObjectName, _ObjectPath);
		if(CurObject == null)
		{ return null ; }
		
		CurObject.AddComponent<EzGui_SpriteText>();
		CurObject.GetComponent<EzGui_SpriteText>().Create();
		CurObject.GetComponent<EzGui_SpriteText>().SetName(ObjectName);
		CurObject.GetComponent<EzGui_SpriteText>().SetEZGUI(CurObject.GetComponent<SpriteText>());
		CurObject.GetComponent<EzGui_SpriteText>().SetBasePos(BasePosType);
		CurObject.GetComponent<EzGui_SpriteText>().SetPos((float)PosX, (float)PosY, (float)PosZ);
		CurObject.GetComponent<EzGui_SpriteText>().SetAnchor(Anchor);
		CurObject.GetComponent<EzGui_SpriteText>().SetAllignment(Allignment);
		CurObject.GetComponent<EzGui_SpriteText>().SetText(Text);
		CurObject.GetComponent<EzGui_SpriteText>().SetColor(Color.black);
		CurObject.GetComponent<EzGui_SpriteText>().SetValid(false);
		
		CurObject.name		= ObjectName;
		CurObject.layer		= LayerMask.NameToLayer("GUI");
		CurObject.transform.parent	= Main.inst.GetResourceObject_EZGUISPRITETEXT().transform;
		
		return CurObject;
	}
	
	public static GameObject GetEZGUI_OUTLINETEXT(string ObjectName, string FrontPath, string BackPath,
		int PosX, int PosY, int PosZ, string Anchor, string Allignment, string Text, string BasePosType)
	{
		if(!IsHaveResource(ObjectName, FrontPath))
		{ return null; }
		if(!IsHaveResource(ObjectName, BackPath))
		{ return null; }
		
		GameObject CurObject		= new GameObject(ObjectName);
		
		GameObject FrontObject		= (GameObject)GameObject.Instantiate(Resources.Load(FrontPath), new Vector3(0, 0, -1), Quaternion.identity);
		FrontObject.transform.parent	= CurObject.transform;
		
		GameObject BackObject		= (GameObject)GameObject.Instantiate(Resources.Load(BackPath), Vector3.zero, Quaternion.identity);
		BackObject.transform.parent		= CurObject.transform;
		
		CurObject.AddComponent<EzGui_SpriteText_Outline>();
		CurObject.GetComponent<EzGui_SpriteText_Outline>().Create();
		CurObject.GetComponent<EzGui_SpriteText_Outline>().SetName(ObjectName);
		CurObject.GetComponent<EzGui_SpriteText_Outline>().SetEZGUI(FrontObject.GetComponent<SpriteText>(), BackObject.GetComponent<SpriteText>());
		CurObject.GetComponent<EzGui_SpriteText_Outline>().SetBasePos(BasePosType);
		CurObject.GetComponent<EzGui_SpriteText_Outline>().SetPos((float)PosX, (float)PosY, (float)PosZ);
		CurObject.GetComponent<EzGui_SpriteText_Outline>().SetAnchor(Anchor);
		CurObject.GetComponent<EzGui_SpriteText_Outline>().SetAllignment(Allignment);
		CurObject.GetComponent<EzGui_SpriteText_Outline>().SetColor(new Color(1,1,1), new Color(0,0,0));
		CurObject.GetComponent<EzGui_SpriteText_Outline>().SetText(Text);
		CurObject.GetComponent<EzGui_SpriteText_Outline>().SetValid(false);
		
		CurObject.name				= ObjectName;
		CurObject.layer				= LayerMask.NameToLayer("GUI");
		CurObject.transform.parent	= Main.inst.GetResourceObject_EZGUIOUTLINETEXT().transform;
		
		return CurObject;
	}
	
	public static GameObject GetEZGUI_SCROLLLIST(string ObjectName,
		int PosX, int PosY, int PosZ, int TextureSizeX, int TextureSizeY, string Orientation, string Direction, string Alignment, string BasePosType)
	{
		GameObject CurObject		= new GameObject(ObjectName);
		
		CurObject.AddComponent<UIScrollList>();
		CurObject.AddComponent<EzGui_ScrollList>();
		CurObject.GetComponent<EzGui_ScrollList>().Create();
		CurObject.GetComponent<EzGui_ScrollList>().SetName(ObjectName);
		CurObject.GetComponent<EzGui_ScrollList>().SetEZGUIScrollList(CurObject.GetComponent<UIScrollList>());
		CurObject.GetComponent<EzGui_ScrollList>().SetScrollListViewSize((float)TextureSizeX, (float)TextureSizeY);
		CurObject.GetComponent<EzGui_ScrollList>().SetBasePos(BasePosType);
		CurObject.GetComponent<EzGui_ScrollList>().SetOrientation(Orientation);
		CurObject.GetComponent<EzGui_ScrollList>().SetDirection(Direction);
		CurObject.GetComponent<EzGui_ScrollList>().SetAlignment(Alignment);
		CurObject.GetComponent<EzGui_ScrollList>().SetPos((float)PosX, (float)PosY, (float)PosZ);
		CurObject.GetComponent<EzGui_ScrollList>().SetValid(false);
		
		CurObject.name				= ObjectName;
		CurObject.layer				= LayerMask.NameToLayer("GUI");
		CurObject.transform.parent	= Main.inst.GetResourceObject_EZGUISCROLLLIST().transform;
		
		return CurObject;
	}
	
#endregion
}