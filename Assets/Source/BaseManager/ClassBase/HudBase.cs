using UnityEngine;
using System.Collections;
using DefineBase;

public abstract class HudBase : IClassBase
{
	private		GameResourceManager		m_ResourceScript;
	
/*
#region IClassBase
	public override void Create()
	{
		base.Create();
	}
	
	public override void SetValid(bool IsValid)
	{
		base.SetValid(IsValid);
	}
	
	public override void Update()
	{
		if(!IsUpdate())
		{ return ; }
		base.Update();
	}
#endregion
	
#region Resource Setting
	protected override void LoadResource()
	{
		SetResourceManagerCreateWithXml("");
		
		//GetResource("").GetComponent<>();
		SetResources();
	}
	
	protected override void SetResources()
	{
	}
	
	public override void ReleaseResource()
	{
		//ReleaseResourceObject();
	}
	
	public override void SetBasePos()
	{
		float BasePosX;
		float BasePosY;
	}
#endregion
	
#region SetPopup/ClosePopup
	public override void SetPopup()
	{
		base.SetPopup();
	}
	
	public override void ClosePopup()
	{
		base.ClosePopup();
	}
#endregion
	
#region Delegate
#endregion
	
#region GetScript
#endregion
	
#region Network_Request
#endregion
	
#region Network_Receive
#endregion
	
	//////////////////////////////////////////////////
*/
	
#region HudObjectBase virtual
	protected	abstract void LoadResource();
	protected	virtual  void SetResources()	{}
	public		abstract void ReleaseResource();
	public		abstract void SetBasePos();
	
	public virtual void SetPopup()
	{
		LoadResourceImages();
		this.SetValid(true);
	}
	public virtual void ClosePopup()
	{
		this.SetValid(false);
		DestroyResourceObject();
	}
#endregion
	
#region IClassBase
	private bool Enabled;
	private bool Valid;
	
	public virtual void Create()
	{
	}
	public virtual void SetValid(bool IsValid)
	{
		enabled	= IsValid;
		Valid	= IsValid;
	}
	public bool GetValid()
	{
		return Valid;
	}
	public bool enabled
	{
		get { return Enabled; }
		set { Enabled	= value; }
	}
	public virtual void Release()
	{
		this.ReleaseResource();
		DestroyResourceObject();
	}
	
	public virtual void Message(int Msg, int Param1, int Param2)	{}
	
	public bool IsUpdate()
	{
		if(!enabled)
		{ return false; }
		return true;
	}
	
	public virtual void Update()	{}
#endregion
	
	protected void SendMessage(int Msg, int Param1)					{ Main.inst.Message(Msg, Param1); }
	protected void SendMessage(int Msg, int Param1, int Param2)		{ Main.inst.Message(Msg, Param1, Param2); }
	
	public virtual void LoadResourceImages()
	{
		this.LoadResource();
		SetResources();
		SetBasePos();
	}
	
#region ResourceObject
	protected void SetResourceObject()
	{
		if(m_ResourceScript == null)
		{
			m_ResourceScript	= new GameResourceManager();
		}
	}
	
	protected void SetResourceManagerCreateWithXml(string _ScriptPath)
	{
		this.ReleaseResource();
		if(m_ResourceScript == null)
		{
			SetResourceObject();
		}
		m_ResourceScript.CreateWithXml(string.Format("Script/Game/xml/{0}", _ScriptPath));
	}
	
	protected void SetResourceManagerCreate(string _ScriptPath)
	{
		this.ReleaseResource();
		if(m_ResourceScript == null)
		{
			SetResourceObject();
		}
		m_ResourceScript.Create(_ScriptPath);
	}
	
	protected GameObject GetResource(string _ResourceName)
	{
		if(m_ResourceScript != null)
		{
				return m_ResourceScript.GetResource(_ResourceName);
		}
		return null;
	}
	
	protected void ReleaseResourceObject()
	{
		if(m_ResourceScript != null)
		{
			m_ResourceScript.Release();
		}
	}
	
	protected void DestroyResourceObject()
	{
		this.ReleaseResource();
		ReleaseResourceObject();
	}
	
	public GameResourceManager GetResourceManagerScript()	{ return m_ResourceScript; }
#endregion
	
	protected void DestroyImmediate(GameObject _Object)
	{
		GameObject.DestroyImmediate(_Object);
	}
	
	protected GameObject Instantiate(Object _Object)
	{
		if(_Object == null)
		{
			Debug.LogError("Object Is NULL!!!!");
			return null;
		}
		return (GameObject)GameObject.Instantiate(_Object);
	}
	
#if UNITY_EDITOR
	protected virtual bool IsProcessContinue()
	{
		if(GetValid())
		{
			return true;
		}
		return false;
	}
#elif UNITY_IPHONE || UNITY_ANDROID
	protected virtual bool IsProcessContinue()
	{
		if(GetValid())
		{
			if(Main.inst.GetInputManager().GetTouchCount() == 1)
			{
				return true;
			}
		}
		return false;
	}
#else
	protected virtual bool IsProcessContinue()
	{
		if(GetValid())
		{
			return true;
		}
		return false;
	}
#endif
}