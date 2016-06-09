using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DefineBase;

public class HudNumberSetManager : IClassBase
{
	private static Object	m_objNumber;
	private static Vector3	m_vecChildBasePos	= new Vector3(18.0f, 0.0f, 0.0f);
	private List<Rect>		m_listNumberUV;
	private float			m_NumberSizeX;
	private float			m_NumberSizeY;
	
	
#region IClassBase
	protected bool Enabled;
	protected bool Valid;
	
	public void Create()
	{
		SetNumberUV();
		SetValid(false);
	}
	public void SetValid(bool IsValid)
	{
		Enabled	= IsValid;
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
	
	public void Release()
	{
		if(m_listNumberUV != null)
		{
			m_listNumberUV.Clear();
			m_listNumberUV	= null;
		}
	}
	
	public void Message(int Msg, int Param1, int Param2)	{}
	
	public bool IsUpdate()
	{
		if(!enabled)
		{ return false; }
		return true;
	}
	public void Update()	{}
#endregion
	
	private void SetNumberUV()
	{
		if(m_listNumberUV == null)	{ m_listNumberUV	= new List<Rect>(11); }
		
		for(int i = 0; i < 3; i++)
		{
			if(m_objNumber == null)
			{
				m_objNumber	= Resources.Load("Mobile/EZGUI/Score/Number/Score");
			}
			GameObject	tmpNumber	= (GameObject)GameObject.Instantiate(Resources.Load(string.Format("{0}{1}", "Mobile/EZGUI/Score/Number/Score_", i)));
			Rect		tmpNumberUV;
			tmpNumberUV	= (Rect)tmpNumber.GetComponent<UIButton>().GetUVs();
			m_listNumberUV.Add(tmpNumberUV);
			
			tmpNumber.GetComponent<UIButton>().SetState((int)UIButton.CONTROL_STATE.OVER);
			tmpNumberUV	= (Rect)tmpNumber.GetComponent<UIButton>().GetUVs();
			m_listNumberUV.Add(tmpNumberUV);
			
			tmpNumber.GetComponent<UIButton>().SetState((int)UIButton.CONTROL_STATE.ACTIVE);
			tmpNumberUV	= (Rect)tmpNumber.GetComponent<UIButton>().GetUVs();
			m_listNumberUV.Add(tmpNumberUV);
			
			if(i != 2)
			{
				tmpNumber.GetComponent<UIButton>().SetState((int)UIButton.CONTROL_STATE.DISABLED);
				tmpNumberUV	= (Rect)tmpNumber.GetComponent<UIButton>().GetUVs();
				m_listNumberUV.Add(tmpNumberUV);
			}
			
			if(i == 0)
			{
				m_NumberSizeX	= tmpNumber.GetComponent<UIButton>().ImageSize.x;
				m_NumberSizeY	= tmpNumber.GetComponent<UIButton>().ImageSize.y;
				
				m_vecChildBasePos.x	= m_NumberSizeX * 0.8f;
			}
			
			GameObject.Destroy(tmpNumber);
		}
	}
	
//	public GameObject SetNumberManager()
//	{
//		GameObject 
//	}
	
	public Object		GetObjectNumber()		{ return m_objNumber; }
	public List<Rect>	GetListNumberUV()		{ return m_listNumberUV; }
	public Rect			GetNumberUV(int _Num)	{ return m_listNumberUV[_Num]; }
	public static Vector3 GetChildBasePos()		{ return m_vecChildBasePos; }
}