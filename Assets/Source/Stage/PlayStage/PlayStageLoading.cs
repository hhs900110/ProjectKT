using UnityEngine;
using System.Collections;
using DefineBase;

public class PlayStageLoading : StageBase
{
	private ControlMesh			m_imgLoading;
	
#region IClassBase
	public override void Create()
	{
		base.Create();
	}
	
	public override void SetValid(bool IsValid)
	{
		base.SetValid(IsValid);
		
		if(IsValid)
		{
			LoadResource();
			SetBasePos();
			m_imgLoading.SetValid(IsValid);
		}
		else
		{
			if(m_imgLoading != null)
			{
				m_imgLoading.SetValid(IsValid);
			}
			ReleaseResource();
		}
	}
	
	public override void Release()
	{
		base.Release();
		ReleaseResource();
	}
#endregion
	
	private void LoadResource()
	{
		if(m_imgLoading == null)
		{
			string ObjectPath	= string.Format("{0}/Model/Loading/Loading", ResourceLoad.CheckHeight);
			GameObject tmpObject	= ResourceLoad.GetGameObject("Loading", ObjectPath);
			
			m_imgLoading	= new ControlMesh();
			m_imgLoading.Create(tmpObject);
			m_imgLoading.transform.parent	= Main.inst.GetResourceObject_EZGUITEXTURE().transform;
		}
		else if(m_imgLoading.gameObject == null)
		{
			string ObjectPath	= string.Format("{0}/Model/Loading/Loading", ResourceLoad.CheckHeight);
			GameObject tmpObject	= ResourceLoad.GetGameObject("Loading", ObjectPath);
			
			m_imgLoading.Create(tmpObject);
			m_imgLoading.transform.parent	= Main.inst.GetResourceObject_EZGUITEXTURE().transform;
		}
		SetResources();
	}
	
	protected void SetResources()
	{
		m_imgLoading.Anchor	= SpriteRoot.ANCHOR_METHOD.MIDDLE_CENTER;
		m_imgLoading.SetSize(m_imgLoading.spriteMesh.texture.width, m_imgLoading.spriteMesh.texture.height);
		m_imgLoading.gameObject.layer	= LayerMask.NameToLayer("GUI");
	}
	
	private void ReleaseResource()
	{
		if(m_imgLoading != null)
		{
			if(m_imgLoading.gameObject != null)
			{
				GameObject.DestroyImmediate(m_imgLoading.gameObject);
			}
		}
	}
	
	public void SetBasePos()
	{
		if(m_imgLoading != null)
		{
			m_imgLoading.SetValid(false);
			if(m_imgLoading.gameObject)
			{
				m_imgLoading.gameObject.transform.position	= new Vector3(Main.inst.GetUICamera().transform.position.x, Main.inst.GetUICamera().transform.position.y, 100);
			}
		}
	}
}