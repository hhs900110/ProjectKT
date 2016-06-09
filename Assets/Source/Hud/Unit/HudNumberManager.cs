using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DefineBase;

public class HudNumberManager : ObjectBase
{
	////////////////////
	
	private static HudNumberSetManager	m_scriptNumberSetManager;
	
	////////////////////
	
	private List<GameObject>	m_listChild;
	
	private bool				m_dCountEffect	= false;
	private float				m_dEffectTime	= 10.0f;
	private int					m_dEffectFrame	= 10;
//	private float				m_fBaseScale	= 1.0f;
//	private float				m_fMaxScale		= 1.3f;
//	private float				m_fMinScale		= 1.0f;
	
	////////////////////
	
//	private float				m_fDeltaTime	=  0.0f;
//	private int					m_fNowFrameNum	=  0;
	
	private int					m_dGap;
	private int					m_dOldNumberCount;
	private int					m_dNowNumberCount;
	
	////////////////////
	
	private bool				m_IsCommaOn;
	
	////////////////////
	
	private Color						m_Color			= Color.white;
	private Vector3						m_Pos			= Vector3.zero;
	private HUD_BASE_POS				m_BasePosType	= HUD_BASE_POS._TOP_LEFT;
	private SpriteRoot.ANCHOR_METHOD	m_ChildAnchor	= SpriteRoot.ANCHOR_METHOD.UPPER_LEFT;
	private int							m_OldNumber	= -1;
	private int							m_NowNumber	= -1;
	
	
#region Object Base
	public override void Create()
	{
		if(m_scriptNumberSetManager == null)	{ m_scriptNumberSetManager = Main.inst.GetHudManager().GetHudNumberSetManager(); }
		if(m_listChild == null)					{ m_listChild = new List<GameObject>(); }
		
		m_IsCommaOn	= false;
	}
	
	public override void SetValid(bool IsValid)
	{
		base.SetValid(IsValid);
		
		if(m_listChild != null)
		{
			for(int i = 0; i < m_listChild.Count; i++)
			{
				m_listChild[i].GetComponent<EzGui_Texture>().SetValid(IsValid);
			}
		}
		if(!IsValid)
		{
			m_dCountEffect	= IsValid;
		}
	}
	
	public override void Release()
	{
		if(m_listChild != null)
		{
			for(int i = 0; i < m_listChild.Count; i++)
			{
				if(m_listChild[i] != null)
				{
					if(m_listChild[i].GetComponent<EzGui_Texture>())
					{
						m_listChild[i].GetComponent<EzGui_Texture>().Release();
					}
					DestroyImmediate(m_listChild[i]);
				}
			}
			m_listChild.Clear();
			m_listChild	= null;
		}
		base.Release();
	}
#endregion
	
#region Resource
	public void SetNumber(int _Number)
	{
		if(m_NowNumber != _Number)
		{
			m_OldNumber	= m_NowNumber;
			m_NowNumber	= _Number;
			
//			m_fDeltaTime	= 0.0f;
//			m_fNowFrameNum	= 0;
			
			char[] OldArray	= m_OldNumber.ToString().ToCharArray();
			char[] NowArray	= m_NowNumber.ToString().ToCharArray();
			
			if(m_OldNumber >= 0)
			{
				m_dOldNumberCount	= OldArray.Length;
			}
			else
			{
				m_dOldNumberCount	= 0;
			}
			m_dNowNumberCount	= NowArray.Length;
			if(m_dNowNumberCount> m_dOldNumberCount)
			{
				m_dGap	= m_dNowNumberCount;
			}
			else
			{
				for(int i = NowArray.Length-1; i >= 0; i--)
				{
					if(OldArray[i] != NowArray[i])
					{
						m_dGap	= i+1;
						break;
					}
				}
			}
			
			RemoveChild(m_dOldNumberCount, m_dNowNumberCount);
			SetChild();
			
			if(CountEffect)
			{
				enabled	= true;
			}
		}
	}
	
	private void RemoveChild(int _OldLength, int _NowLength)
	{
		if(m_IsCommaOn)
		{
			_NowLength	= _NowLength + (_NowLength - 1)/3;
		}
		if(m_listChild.Count > _NowLength)
		{
			for(int i = m_listChild.Count-1; i > _NowLength-1; i--)
			{
				if(m_listChild[i] != null)
				{
					if(m_listChild[i].GetComponent<EzGui_Texture>())
					{
						m_listChild[i].GetComponent<EzGui_Texture>().Release();
					}
					DestroyImmediate(m_listChild[i]);
				}
				m_listChild.RemoveAt(i);
			}
		}
	}
	
	private void SetChild()
	{
		if(m_dOldNumberCount < m_dNowNumberCount)
		{
			for(int i = m_dOldNumberCount; i < m_dNowNumberCount; i++)
			{
				GameObject tmpObject	= ResourceLoad.GetEzGuiTexture(m_scriptNumberSetManager.GetObjectNumber());
				tmpObject.name			= string.Format("{0}{1}", "NumberChild_", m_listChild.Count);
				tmpObject.GetComponent<EzGui_Texture>().SetColor(m_Color);
				tmpObject.transform.parent			= gameObject.transform;
				tmpObject.GetComponent<EzGui_Texture>().SetValid(Valid);
				
				m_listChild.Add(tmpObject);
				
				if(m_IsCommaOn)
				{
					if(i % 3 == 0 && i != 0)
					{
						GameObject tmpObject1	= ResourceLoad.GetEzGuiTexture(m_scriptNumberSetManager.GetObjectNumber());
						tmpObject1.name			= string.Format("{0}{1}", "NumberChild_", m_listChild.Count);
						tmpObject1.GetComponent<EzGui_Texture>().SetColor(m_Color);
						tmpObject1.transform.parent			= gameObject.transform;
						tmpObject1.GetComponent<EzGui_Texture>().SetValid(Valid);
						
						m_listChild.Add(tmpObject1);
					}
				}
			}
		}
		SetChildPos();
		
		List<int> tmpNumList	= new List<int>();
		Fuctions.SetNumberList(m_NowNumber, ref tmpNumList);
		int ListCount	= m_listChild.Count;
		for(int i = 0, count = 0; count < ListCount; i++, count++)
		{
			if(m_IsCommaOn)
			{
				if(m_dNowNumberCount >= 4)
				{
					if((m_dNowNumberCount - i) % 3 == 0 && i != 0)
					{
						m_listChild[count].GetComponent<EzGui_Texture>().GetEZGUITexture().SetUVs(m_scriptNumberSetManager.GetNumberUV(10));
						count++;
					}
				}
			}
			m_listChild[count].GetComponent<EzGui_Texture>().SetcontrolIsEnabled(false);
			m_listChild[count].GetComponent<EzGui_Texture>().GetEZGUITexture().SetUVs(m_scriptNumberSetManager.GetNumberUV(tmpNumList[i]));
		}
	}
	
#endregion
	
	public void FixedUpdate()
	{
//		if(CountEffect)
//		{
//			m_fDeltaTime	= Time.smoothDeltaTime;
//			
//			if(m_fDeltaTime > (m_dEffectTime*1000)/m_dEffectFrame)
//			{
//				m_fDeltaTime	= (m_dEffectTime*1000/m_dEffectFrame) - m_fDeltaTime;
//				m_fNowFrameNum++;
//				SetScale();
//				
//				if(m_fNowFrameNum >= EffectFrame)
//				{
//					CountEffect	= false;
//					enabled		= false;
//				}
//			}
//		}
//		else
//		{
//			enabled	= false;
//		}
	}
	
	private void SetScale()
	{
	}
	
#region Get / Set
	public bool IsCommaOn	{
		get { return m_IsCommaOn; }
		set { m_IsCommaOn		= value; }
	}
	
	public bool CountEffect	{
		get { return m_dCountEffect; }
		set { m_dCountEffect	= value; }
	}
	
	public float EffectTime	{
		get { return m_dEffectTime; }
		set { m_dEffectTime		= value; }
	}
	
	public int EffectFrame	{
		get { return m_dEffectFrame; }
		set { m_dEffectFrame	= value; }
	}
	
	public Color color	{
		get { return m_Color; }
		set {
			m_Color	= value;
			SetColor();
		}
	}
	
	public Vector3 Pos	{
		get { return m_Pos; }
		set {
			m_Pos	= value;
			SetPos();
		}
	}
	
	public int BasePosType	{
		get { return (int)m_BasePosType; }
		set {
			m_BasePosType	= (HUD_BASE_POS)value;
			SetPos();
		}
	}
	
	public SpriteRoot.ANCHOR_METHOD ChildAnchor	{
		get { return m_ChildAnchor; }
		set {
			m_ChildAnchor	= value;
			SetChildPos();
		}
	}
	
	private void SetColor()
	{
		for(int i = 0; i < m_listChild.Count; i++)
		{
			if(m_listChild[i] != null)
			{
				if(m_listChild[i].GetComponent<EzGui_Texture>())
				{
					m_listChild[i].GetComponent<EzGui_Texture>().SetColor(m_Color);
				}
			}
		}
	}
#endregion
	
#region SetPos
	public void SetPos(float ObjectPosX, float ObjectPosY, float ObjectPosZ)
	{
		m_Pos.x = ObjectPosX;
		m_Pos.y = ObjectPosY;
		m_Pos.z = (int)TEXTURE_LAYER._MAX - ObjectPosZ;
		SetPos();
	}
	
	public void SetPos(float ObjectPosX, float ObjectPosY)
	{
		m_Pos.x = ObjectPosX;
		m_Pos.y = ObjectPosY;
		SetPos();
	}
	
	private void SetPos()
	{
			 if(m_BasePosType == HUD_BASE_POS._TOP_LEFT)		{ SetTopLeftPos(); }
		else if(m_BasePosType == HUD_BASE_POS._TOP_RIGHT)		{ SetTopRightPos(); }
		else if(m_BasePosType == HUD_BASE_POS._BOTTOM_CENTER)	{ SetBottomCenterPos(); }
		else if(m_BasePosType == HUD_BASE_POS._MIDDLE_CENTER)	{ SetMiddleCenterPos(); }
		else if(m_BasePosType == HUD_BASE_POS._BOTTON_RIGHT)	{ SetBottomRightPos(); }
		else if(m_BasePosType == HUD_BASE_POS._BOTTOM_LEFT)		{ SetBottomLeftPos(); }
		else if(m_BasePosType == HUD_BASE_POS._TOP_CENTER)		{ SetTopCenterPos(); }
		else
		{
			m_Pos.y = m_Pos.y - Screen.height;
			transform.position	= Pos;
		}
	}
	
	public void SetTopLeftPos()
	{
		m_Pos.x = Pos.x - Main.inst.GetGameSizeGap();
		m_Pos.y = -Pos.y;
		transform.position = Pos;
	}
	
	public void SetTopCenterPos()
	{
		m_Pos.x = (Main.inst.GetGameWidth() / 2) - Pos.x;
		m_Pos.y = -Pos.y;
		transform.position = Pos;
	}
	
	public void SetTopRightPos()
	{
		m_Pos.x = (Main.inst.GetGameWidth() - Pos.x) + Main.inst.GetGameSizeGap();
		m_Pos.y = -Pos.y;
		transform.position = Pos;
	}
	
	public void SetMiddleCenterPos()
	{
		m_Pos.x = (Main.inst.GetGameWidth() / 2) - Pos.x;
		m_Pos.y = -(Main.inst.GetGameHeight() / 2) + Pos.y;
		transform.position = Pos;
	}
	
	public void SetBottomLeftPos()
	{
		m_Pos.x = Pos.x - Main.inst.GetGameSizeGap();
		m_Pos.y = -Main.inst.GetGameHeight() + Pos.y;
		transform.position = Pos;
	}
	
	public void SetBottomCenterPos()
	{
		m_Pos.x = (Main.inst.GetGameWidth() / 2) - Pos.x;
		m_Pos.y = - Main.inst.GetGameHeight() + Pos.y;
		transform.position = Pos;
	}
	
	public void SetBottomRightPos()
	{
		m_Pos.x = (Main.inst.GetGameWidth() - Pos.x) + Main.inst.GetGameSizeGap();
		m_Pos.y = -Main.inst.GetGameHeight() + Pos.y;
		transform.position = Pos;
	}
	
	private void SetChildPos()
	{
		if(m_listChild != null)
		{
			int Max	= m_listChild.Count;
			for(int i = 0; i < Max; i++)
			{
				if(m_listChild[i].GetComponent<EzGui_Texture>())
				{
					m_listChild[i].GetComponent<EzGui_Texture>().SetAnchor(m_ChildAnchor);
				}
				
				float compute	= 0.0f;
				switch(m_ChildAnchor)
				{
				case SpriteRoot.ANCHOR_METHOD.UPPER_LEFT:
				case SpriteRoot.ANCHOR_METHOD.MIDDLE_LEFT:
				case SpriteRoot.ANCHOR_METHOD.BOTTOM_LEFT:
					compute	= i;
					break;
					
				case SpriteRoot.ANCHOR_METHOD.UPPER_CENTER:
				case SpriteRoot.ANCHOR_METHOD.MIDDLE_CENTER:
				case SpriteRoot.ANCHOR_METHOD.BOTTOM_CENTER:
					compute	= (1.0f - Max + 2.0f*i) / 2.0f;
					
					break;
					
				case SpriteRoot.ANCHOR_METHOD.UPPER_RIGHT:
				case SpriteRoot.ANCHOR_METHOD.MIDDLE_RIGHT:
				case SpriteRoot.ANCHOR_METHOD.BOTTOM_RIGHT:
					compute	= (Max - i - 1.0f) * -1.0f;
					break;
				}
				m_listChild[i].transform.localPosition	= HudNumberSetManager.GetChildBasePos() * compute;
			}
		}
	}
#endregion
}