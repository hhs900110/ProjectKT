using UnityEngine;
using System.Collections;
using DefineBase;

#pragma warning disable 0168 // variable declared but not used.
#pragma warning disable 0219 // variable assigned but not used.
#pragma warning disable 0414 // private field assigned but not used.

public abstract class EzGui_Base : MonoBehaviour 
{
	protected bool Valid;
	
	protected Vector3	Pos;
	protected int		BasePosType;
	protected string	Name;
	protected float		TextureSizeX;
	protected float		TextureSizeY;
	protected Rect		TextureRect;
	protected string	m_Anchor;
	protected float		OriginPosX;
	protected float		OriginPosY;
	protected int		PopUpCount;
	
	protected float		GameSizeGap;
	protected float		GameWidth;
	protected float		GameHeight;
	
	protected Color		MatColor;
	
	protected MeshRenderer EzRenderer;
	
	
#region ObjectBase
	public virtual void Create()
	{
		EzRenderer	= null;
		
		MatColor	= new Color(1.0f, 1.0f, 1.0f, 1.0f);
		Pos			= new Vector3(0.0f, 0.0f, 0.0f);
		
		BasePosType		= (int)HUD_BASE_POS._TOP_LEFT;
		Valid			= false;
		Name			= null;
		TextureSizeX	= 0.0f;
		TextureSizeY	= 0.0f;
		TextureRect		= new Rect(0.0f, 0.0f, 0.0f, 0.0f);
		m_Anchor			= "LEFT_TOP";
		
		OriginPosX		= 0.0f;
		OriginPosY		= 0.0f;
		
		PopUpCount		= -1;
		
		GameSizeGap		= 0.0f;
		GameWidth		= 0.0f;
		GameHeight		= 0.0f;
		
		if(Main.inst != null)
		{
			GameSizeGap 	= Main.inst.GetGameSizeGap();
			GameWidth		= Main.inst.GetGameWidth();
			GameHeight		= Main.inst.GetGameHeight();
		}
	}
	
	public virtual void SetValid(bool IsValid)
	{
		enabled	= IsValid;
		Valid	= IsValid;
	}
	
	public virtual bool GetValid()
	{
		return Valid;
	}
	
	public virtual void Release()
	{
	}
#endregion
	
	////////// ========== EzGui Base ========== //////////
	
#region EzGui_Base Pos
	public void SetBasePos(string _BasePosType)
	{
			 if(_BasePosType == "Upper_Left")		{ SetBasePos((int)HUD_BASE_POS._TOP_LEFT); }
		else if(_BasePosType == "Upper_Center")		{ SetBasePos((int)HUD_BASE_POS._TOP_CENTER); }
		else if(_BasePosType == "Upper_Right")		{ SetBasePos((int)HUD_BASE_POS._TOP_RIGHT); }
		else if(_BasePosType == "Middle_Center")	{ SetBasePos((int)HUD_BASE_POS._MIDDLE_CENTER); }
		else if(_BasePosType == "Bottom_Left")		{ SetBasePos((int)HUD_BASE_POS._BOTTOM_LEFT); }
		else if(_BasePosType == "Bottom_Center")	{ SetBasePos((int)HUD_BASE_POS._BOTTOM_CENTER); }
		else if(_BasePosType == "Bottom_Right")		{ SetBasePos((int)HUD_BASE_POS._BOTTON_RIGHT); }
	}
	
	public void SetBasePos(HUD_BASE_POS ObjectBasePosType)	{ BasePosType	= (int)ObjectBasePosType; }
	public void SetBasePos(int ObjectBasePosType)			{ BasePosType	= ObjectBasePosType; }
	
	public void SetOriginPos()
	{
		OriginPosX	= Pos.x;
		OriginPosY	= Pos.y;
	}
	
	public void AddPos(float ObjectPosX, float ObjectPosY)
	{
		Pos.x += ObjectPosX;
		Pos.y += ObjectPosY;
		SetPos(Pos.x, Pos.y);
	}
	
	public void AddPosX(float ObjectPosX)	{ SetPosX(Pos.x += ObjectPosX); }
	public void AddPosY(float ObjectPosY)	{ SetPosY(Pos.y += ObjectPosY); }
	
	public void AddLocalPosY(float ObjectPosY)
	{
		Pos.y += (ObjectPosY * -1.0f);
		
		transform.position = new Vector3(transform.position.x, Pos.y, transform.position.z);
	}
	
	public void SetTopLeftPos()
	{
		Pos.x = Pos.x - GameSizeGap;
		Pos.y = -Pos.y;
		transform.position = Pos;
	}
	
	public void SetTopCenterPos()
	{
		Pos.x = (GameWidth / 2) - Pos.x;
		Pos.y = -Pos.y;
		transform.position = Pos;
	}
	
	public void SetTopRightPos()
	{
		Pos.x = (GameWidth - Pos.x) + GameSizeGap;
		Pos.y = -Pos.y;
		transform.position = Pos;
	}
	
	public void SetMiddleCenterPos()
	{
		Pos.x = (GameWidth / 2) - Pos.x;
		Pos.y = -(GameHeight / 2) + Pos.y;
		transform.position = Pos;
	}
	
	public void SetBottomLeftPos()
	{
		Pos.x = Pos.x - GameSizeGap;
		Pos.y = -GameHeight + Pos.y;
		transform.position = Pos;
	}
	
	public void SetBottomCenterPos()
	{
		Pos.x = (GameWidth / 2) - Pos.x;
		Pos.y = - GameHeight + Pos.y;
		transform.position = Pos;
	}
	
	public void SetBottomRightPos()
	{
		Pos.x = (GameWidth - Pos.x) + GameSizeGap;
		Pos.y = -GameHeight + Pos.y;
		transform.position = Pos;
	}
	
	public float GetPosX()	{ return Pos.x; }
	public float GetPosY()	{ return -Pos.y; }
	public float GetLayer()	{ return (int)TEXTURE_LAYER._MAX - Pos.z; }
#endregion
	
	public void		SetGameSizeGap(float Gap)		{ GameSizeGap	= Gap; }
	public void		SetGameWidth(float Width)		{ GameWidth		= Width; }
	public void		SetGameHeight(float Height)		{ GameHeight	= Height; }
	
	public void		SetName(string ObjectName)		{ Name	= ObjectName; }
	public string	GetName()						{ return Name; }
	
	public float	GetTextureSizeX()				{ return TextureSizeX; }
	public float	GetTextureSizeY()				{ return TextureSizeY; }
	
	public void		SetPopUpCount(int ObjectCount)	{ PopUpCount = ObjectCount; }
	public int		GetPopUpCount()					{ return PopUpCount; }
	
	public void SetScale(float ObjectX, float ObjectY)
	{
		transform.localScale = new Vector3(ObjectX, ObjectY, 1.0f);
	}
	
	////////// ========== EzGui Base - virtual ========== //////////
	
	public virtual void SetTextureSize(float SizeX, float SizeY)
	{
		TextureSizeX	= SizeX;
		TextureSizeY	= SizeY;
	}
	
	public virtual void SetTextureSizeX(float SizeX)	{ TextureSizeX	= SizeX; }
	public virtual void SetTextureSizeY(float SizeY)	{ TextureSizeY	= SizeY; }
	
	public virtual void SetPos(float ObjectPosX, float ObjectPosY, float ObjectPosZ)
	{
		Pos.x = ObjectPosX;
		Pos.y = ObjectPosY;
		Pos.z = (int)TEXTURE_LAYER._MAX - ObjectPosZ;
		
			 if(BasePosType == (int)HUD_BASE_POS._TOP_LEFT)
		{
			SetTopLeftPos();
		}
		else if(BasePosType == (int)HUD_BASE_POS._TOP_RIGHT)
		{
			SetTopRightPos();
		}
		else if(BasePosType == (int)HUD_BASE_POS._BOTTOM_CENTER)
		{
			SetBottomCenterPos();
		}
		else if(BasePosType == (int)HUD_BASE_POS._MIDDLE_CENTER)
		{
			SetMiddleCenterPos();
		}
		else if(BasePosType == (int)HUD_BASE_POS._BOTTON_RIGHT)
		{
			SetBottomRightPos();
		}
		else if(BasePosType == (int)HUD_BASE_POS._BOTTOM_LEFT)
		{
			SetBottomLeftPos();
		}
		else if(BasePosType == (int)HUD_BASE_POS._TOP_CENTER)
		{
			SetTopCenterPos();
		}
		else
		{
			transform.position = new Vector3(ObjectPosX, ObjectPosY, ObjectPosZ);
		}
	}
	
	public virtual void SetPos(float ObjectPosX, float ObjectPosY)
	{
		Pos.x = ObjectPosX;
		Pos.y = ObjectPosY;

		if(BasePosType == (int)HUD_BASE_POS._TOP_LEFT)
		{
			SetTopLeftPos();
		}
		else if(BasePosType == (int)HUD_BASE_POS._TOP_RIGHT)
		{
			SetTopRightPos();
		}
		else if(BasePosType == (int)HUD_BASE_POS._BOTTOM_CENTER)
		{
			SetBottomCenterPos();
		}
		else if(BasePosType == (int)HUD_BASE_POS._MIDDLE_CENTER)
		{
			SetMiddleCenterPos();
		}
		else if(BasePosType == (int)HUD_BASE_POS._BOTTON_RIGHT)
		{
			SetBottomRightPos();
		}
		else if(BasePosType == (int)HUD_BASE_POS._BOTTOM_LEFT)
		{
			SetBottomLeftPos();
		}
		else if(BasePosType == (int)HUD_BASE_POS._TOP_CENTER)
		{
			SetTopCenterPos();
		}
		else
		{
			Pos.y = Pos.y - Screen.height;
			transform.position = Pos;
		}
	}
	
	public virtual void SetPosX(float ObjectPosX)
	{
		Pos.x = ObjectPosX;
		transform.position = new Vector3(Pos.x, transform.position.y, transform.position.z);
	}
	
	public virtual void SetPosY(float ObjectPosY)
	{
		Pos.y = -ObjectPosY;
		transform.position = new Vector3(transform.position.x, Pos.y, transform.position.z);
	}
	
	public virtual void SetLayer(float ObjectLayer)
	{
		Pos.z = (int)TEXTURE_LAYER._MAX - ObjectLayer;
		transform.position = new Vector3(transform.position.x, Pos.y, Pos.z);
	}
	
	public virtual void SetColor(float r, float g, float b, float a)
	{
		if(EzRenderer)
		{
			MatColor.r	= r;
			MatColor.g	= g;
			MatColor.b	= b;
			MatColor.a	= a;
			EzRenderer.material.color = MatColor;
		}
	}
	
	public virtual void SetColor(Color _color)
	{
		if(EzRenderer)
		{
			MatColor	= _color;
			EzRenderer.material.color = MatColor;
		}
	}
	
	public virtual void SetAlpha(float _a)
	{
		if(EzRenderer)
		{
			MatColor.a	= _a;
			EzRenderer.material.color = MatColor;
		}
	}
	
	public virtual void SetMeshRenderer(MeshRenderer ObjectRenderer)
	{
		EzRenderer = ObjectRenderer;
	}
	
	public virtual void SetShader(string ShaderPath)
	{
		if(EzRenderer)
		{
			EzRenderer.material.shader = Shader.Find(ShaderPath);
		}
	}
}