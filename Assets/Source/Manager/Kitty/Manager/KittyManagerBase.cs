using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DefineBase;

public abstract class KittyManagerBase : HudBase
{
	protected enum LOAD_STEP
	{
		_NULL = 0,
		_RESOURCE_LOAD,
		_OBJECT_REMOVE,
		_OBJECT_LINE,
		_OBJECT_LINE_LOAD_END,
		_SETTING_END,
	}
	
	protected static Vector3		m_vecTmpPos;
	
	protected static int LoadObjNum		=  5;
	protected static int RemoveObjNum	= 10;
	
	protected List<List<KittyTotalObject>>	m_listKitty;
	protected int m_loadObjectLineX;
	protected int m_loadObjectLineY;
	private int m_loadStep;
	
#region IClassBase
	public override void Create()
	{
		base.Create();
		if(m_vecTmpPos == null)	{ m_vecTmpPos	= Vector3.zero; }
		
		m_loadObjectLineX	= 0;
		m_loadObjectLineY	= 0;
	}
	
	public override void SetValid(bool IsValid)
	{
		base.SetValid(IsValid);
		SetValidList(IsValid);
	}
	
	protected void SetValidList(bool IsValid)
	{
		if(m_listKitty != null)
		{
			for(int i = 0; i < m_listKitty.Count; i++)
			{
				List<KittyTotalObject> tmpList	= m_listKitty[i];
				for(int j = 0; j < tmpList.Count; j++)
				{
					if(tmpList[j] != null)
					{
						tmpList[j].SetValid(IsValid);
					}
				}
			}
		}
	}
#endregion
	
#region Resource Setting
//	protected void SetKittyObject()
//	{
//		for(int i = 0; i < DefineBaseManager.inst.KittyMaxMapX; i++)
//		{
//			SetKittyObject(i);
//		}
//	}
	
	protected abstract void SetKittyObject(int _line);
	
	protected bool AddKittyObject(int _line, KittyTotalObject _AddObj)
	{
		if(_AddObj)
		{
			_AddObj.SetValid(false);
			
			if(m_listKitty.Count > _line)
			{
				m_listKitty[_line].Add(_AddObj);
			}
			else
			{
				List<KittyTotalObject> tmpList	= new List<KittyTotalObject>();
				tmpList.Add(_AddObj);
				m_listKitty.Add(tmpList);
			}
		}
		
		return SetLoadObjectLineY();
	}
	
	protected bool SetLoadObjectLineY()
	{
		if(m_listKitty.Count > m_loadObjectLineX)
		{
			m_loadObjectLineY	= m_listKitty[m_loadObjectLineX].Count;
		}
		else
		{
			m_loadObjectLineY	= 0;
		}
		if(m_loadObjectLineY >= DefineBaseManager.inst.KittyMaxMapY)
		{
			m_loadObjectLineX++;
			if(m_loadObjectLineX >= DefineBaseManager.inst.KittyMaxMapX)
			{
				m_loadObjectLineX	= DefineBaseManager.inst.KittyMaxMapX;
				m_loadObjectLineY	= DefineBaseManager.inst.KittyMaxMapY;
				return false;
			}
			SetLoadObjectLineY();
		}
		return true;
	}
	
	public override void ReleaseResource()
	{
		ReleaseKittyObject();
		ReleaseResourceObject();
	}
	
	protected void ReleaseKittyObject()
	{
		if(m_listKitty != null)
		{
			for(int i = 0; i < m_listKitty.Count; )
			{
				List<KittyTotalObject> tmpList	= m_listKitty[i];
				for(int j = 0; j < tmpList.Count; )
				{
					if(tmpList[j].GetComponent<EzGui_Texture>())
					{
						tmpList[j].GetComponent<EzGui_Texture>().Release();
					}
					GameObject.DestroyImmediate(tmpList[j].gameObject);
					tmpList.RemoveAt(j);
				}
				tmpList.Clear();
				m_listKitty.RemoveAt(i);
			}
			m_listKitty.Clear();
		}
	}
	
	protected bool RemoveKittyObject()
	{
		if(m_listKitty != null)
		{
			int RemoveNum	= 0;
			for(int i = m_listKitty.Count-1; i >= 0; i--)
			{
				if(i < DefineBaseManager.inst.KittyMaxMapX)
				{
					List<KittyTotalObject> tmpList	= m_listKitty[i];
					for(int j = tmpList.Count-1; j >= 0; j--)
					{
						if(j >= DefineBaseManager.inst.KittyMaxMapY)
						{
							if(tmpList[j].GetComponent<EzGui_Texture>())
							{
								tmpList[j].GetComponent<EzGui_Texture>().Release();
							}
							GameObject.DestroyImmediate(tmpList[j].gameObject);
							tmpList.RemoveAt(j);
							
							RemoveNum++;
							if(RemoveNum >= RemoveObjNum)
							{
								if(i == 0 && j == 0)
								{
									return true;
								}
								return false;
							}
						}
					}
				}
				else
				{
					RemoveKittyObjectXLine(i, ref RemoveNum);
				}
				if(RemoveNum >= RemoveObjNum)
				{
					return false;
				}
			}
		}
		return true;
	}
	
	protected void RemoveKittyObjectXLine(int _Line, ref int RemoveNum)
	{
		if(m_listKitty != null)
		{
			if(m_listKitty.Count > _Line)
			{
				List<KittyTotalObject> tmpList	= m_listKitty[_Line];
				for(int j = tmpList.Count-1; j >= 0; j--)
				{
					if(tmpList[j].GetComponent<EzGui_Texture>())
					{
						tmpList[j].GetComponent<EzGui_Texture>().Release();
					}
					GameObject.DestroyImmediate(tmpList[j].gameObject);
					tmpList.RemoveAt(j);
					
					RemoveNum++;
					if(RemoveNum >= RemoveObjNum)
					{
						return;
					}
				}
				m_listKitty.RemoveAt(_Line);
			}
		}
	}
	
	public virtual void SetRandomDir()	{}
#endregion
	
#region SetPopup/ClosePopup
	public override void SetPopup()
	{
		SetLoadStep((int)LOAD_STEP._RESOURCE_LOAD);
		m_loadObjectLineX	= 0;
		m_loadObjectLineY	= 0;
		if(m_listKitty == null)
		{
			m_listKitty	= new List<List<KittyTotalObject>>(8);
		}
		base.SetValid(true);
	}
	
	public void SetRePopup()
	{
		if(m_loadObjectLineX == DefineBaseManager.inst.KittyMaxMapX
		&& m_loadObjectLineY == DefineBaseManager.inst.KittyMaxMapY)
		{
			SetLoadStep((int)LOAD_STEP._OBJECT_LINE_LOAD_END);
		}
		else
		{
			SetValidList(false);
			SetLoadStep((int)LOAD_STEP._OBJECT_REMOVE);
			m_loadObjectLineX	= 0;
			SetLoadObjectLineY();
			if(m_listKitty == null)
			{
				m_listKitty	= new List<List<KittyTotalObject>>(8);
			}
		}
		base.SetValid(true);
	}
#endregion
	
	public void Pause(bool _IsPause)
	{
		for(int i = 0; i < m_listKitty.Count; i++)
		{
			List<KittyTotalObject> tmpList	= m_listKitty[i];
			for(int j = 0; j < tmpList.Count; j++)
			{
				if(tmpList[j].GetComponent<KittyTotalObject>())
				{
					tmpList[j].GetComponent<KittyTotalObject>().SetPause(_IsPause);
				}
			}
		}
	}
	
	public override void Update()
	{
		if(!IsUpdate())
		{ return ; }
		base.Update();
		
		switch((LOAD_STEP)GetLoadStep())
		{
		case LOAD_STEP._RESOURCE_LOAD:
			base.SetPopup();
			base.SetValid(true);
			SetLoadStep((int)LOAD_STEP._OBJECT_REMOVE);
			break;
			
		case LOAD_STEP._OBJECT_REMOVE:
			if(RemoveKittyObject())
			{
				SetLoadStep((int)LOAD_STEP._OBJECT_LINE);
			}
			break;
			
		case LOAD_STEP._OBJECT_LINE:
			if(m_loadObjectLineX >= DefineBaseManager.inst.KittyMaxMapX)
			{
				SetLoadStep((int)LOAD_STEP._OBJECT_LINE_LOAD_END);
			}
			else
			{
				SetKittyObject(m_loadObjectLineX);
			}
			break;
			
		case LOAD_STEP._OBJECT_LINE_LOAD_END:
			SetRandomDir();
			SetBasePos();
			SetValid(true);
			SetLoadStep((int)LOAD_STEP._SETTING_END);
			break;
			
		case LOAD_STEP._SETTING_END:
			SetLoadStep((int)LOAD_STEP._NULL);
			Main.game.EndLoadObject();
			break;
			
		case LOAD_STEP._NULL:
			enabled	= false;
			break;
		}
	}
	
	protected void SetLoadStep(int _LoadStep)
	{
		m_loadStep	= _LoadStep;
//		Debug.Log("SetLoadStep : " + ((LOAD_STEP)m_loadStep).ToString());
	}
	
	public int GetLoadStep()	{ return m_loadStep; }
	
	public KittyTotalObject GetKittyTotalObject(int _KittyPosX, int _KittyPosY)
	{
		if(m_listKitty.Count > _KittyPosX)
		{
			if(m_listKitty[_KittyPosX].Count > _KittyPosY)
			{
				return m_listKitty[_KittyPosX][_KittyPosY].GetComponent<KittyTotalObject>();
			}
		}
		return null;
	}
}