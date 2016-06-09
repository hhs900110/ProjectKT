using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DefineBase;

public sealed partial class KittyTotalObject : MonoBehaviour
{
	
	private static Vector3			m_HeadPos		= new Vector3(0.0f, 0.0f, -3.0f);
	private static Vector3			m_LegPos		= new Vector3(0.0f, 0.0f,  1.0f);
	private static Vector3			m_LegBoxPos		= new Vector3(0.0f, 0.0f,  100.0f);
	
	private static float			m_fKittySpeed	=  90.0f;
	
	private LEG_TYPE				m_dBeforeLegType;
	private LEG_TYPE				m_dLegType;
	
	private float					m_fChangeTime;
	private float					m_fChangeDeltaTime;
	
	private GameObject				m_objKittyLeg;
	private EzGui_Texture			m_imgKittyHead;
	private EzGui_Texture			m_imgKittyLeg;
	private Transform				m_transKittyHead;
	private List<Rect>				m_rectKittyFaceUV;
	private List<Rect>				m_rectKittyLegUV;
	
	private int						m_dKittyAddTurn	= 1;
	private int						m_dKittyRotType;
	private int						m_dKittyTurnType;
	
#region ObjectBase
	private void Create_Kitty()
	{
		m_dBeforeLegType	= LEG_TYPE._NULL;
		m_dLegType			= LEG_TYPE._NULL;
	}
	
	private void SetValid_Kitty(bool IsValid)
	{
		EzGuiSetting.SetValidEzGui(m_imgKittyHead, IsValid);
		EzGuiSetting.SetValidEzGui(m_imgKittyLeg, IsValid);
	}
	
	private void Release_Kitty()
	{
		EzGuiSetting.ReleaseEzGui(m_imgKittyHead);
		EzGuiSetting.ReleaseEzGui(m_imgKittyLeg);
		if(m_objKittyLeg != null)
		{
			DestroyImmediate(m_objKittyLeg);
		}
	}
#endregion
	
#region Kitty Resource Setting
	public void SetKittyHead(Object _BaseObj)
	{
		if(m_imgKittyHead != null)
		{ return ; }
		
		GameObject tmpObj	= ResourceLoad.GetEzGuiTexture(_BaseObj);
		if(tmpObj.GetComponent<EzGui_Texture>())
		{
			m_imgKittyHead	= tmpObj.GetComponent<EzGui_Texture>();
			m_imgKittyHead.SetAnchor(SpriteRoot.ANCHOR_METHOD.MIDDLE_CENTER);
		}
		tmpObj.name						= "KittyHead";
		tmpObj.layer					= m_LayerMask;
		m_transKittyHead				= tmpObj.transform;
		m_transKittyHead.parent			= this.transform;
		m_transKittyHead.localPosition	= m_HeadPos;
		SetTmpEffectZero();
		m_transKittyHead.localScale		= m_vecTmp;
	}
	
	public void SetKittyLeg(Object _BaseObj)
	{
		if(m_imgKittyLeg != null)
		{ return ; }
		
		GameObject tmpObj	= ResourceLoad.GetEzGuiTexture(_BaseObj);
		if(tmpObj.GetComponent<EzGui_Texture>())
		{
			m_imgKittyLeg	= tmpObj.GetComponent<EzGui_Texture>();
			m_imgKittyLeg.SetAnchor(SpriteRoot.ANCHOR_METHOD.MIDDLE_CENTER);
		}
		tmpObj.name						= "KittyLeg";
		tmpObj.layer					= m_LayerMask;
		tmpObj.transform.parent			= m_imgKittyHead.gameObject.transform;
		tmpObj.transform.localPosition	= m_LegPos;
		SetTmpEffectZero();
		tmpObj.transform.localScale		= m_vecTmp;
	}
	
	public void SetKittyLeg()
	{
		if(m_objKittyLeg == null)
		{
			m_objKittyLeg			= new GameObject("KittyLegObj");
			m_objKittyLeg.layer		= m_LayerMask;
			
			m_objKittyLeg.AddComponent<BoxCollider>();
			m_objKittyLeg.GetComponent<BoxCollider>().isTrigger	= true;
			m_objKittyLeg.AddComponent<IsTrigger>().Create();
			m_objKittyLeg.GetComponent<IsTrigger>().SetValid(false);
			
			m_objKittyLeg.transform.parent			= m_imgKittyHead.gameObject.transform;
			m_objKittyLeg.transform.localPosition	= m_LegBoxPos;
		}
	}
	
	public void SetKittyLeg_RightAngle()
	{
		SetKittyLeg();
		SetLegType(LEG_TYPE._RIGHT_ANGLE);
		
		m_vecTmp.x	=  0.354f * DefineBaseManager.inst.KittyGap;
		m_vecTmp.y	=  0.0f;
		m_vecTmp.z	=  0.0f;
		m_objKittyLeg.GetComponent<BoxCollider>().center	= m_vecTmp;
		m_vecTmp.x	=  DefineBaseManager.inst.KittyGap * 0.1f;
		m_vecTmp.y	=  DefineBaseManager.inst.KittyGap - DefineBaseManager.inst.KittyGap * 0.2f;
		m_vecTmp.z	=  1.0f;
		m_objKittyLeg.GetComponent<BoxCollider>().size		= m_vecTmp;
		
		m_vecTmp.x	=   0.0f;
		m_vecTmp.y	=   0.0f;
		m_vecTmp.z	= -45.0f;
		m_objKittyLeg.transform.localEulerAngles	= m_vecTmp;
	}
	
	public void SetKittyLeg_Diamond()
	{
		SetKittyLeg();
		SetLegType(LEG_TYPE._DIAMOND);
		
		m_vecTmp.x	=  0.0f;
		m_vecTmp.y	=  0.0f;
		m_vecTmp.z	=  0.0f;
		m_objKittyLeg.GetComponent<BoxCollider>().center	= m_vecTmp;
		m_vecTmp.x	=  DefineBaseManager.inst.KittyGap - DefineBaseManager.inst.KittyGap * 0.2f;
		m_vecTmp.y	=  DefineBaseManager.inst.KittyGap - DefineBaseManager.inst.KittyGap * 0.2f;
		m_vecTmp.z	=  1.0f;
		m_objKittyLeg.GetComponent<BoxCollider>().size		= m_vecTmp;
		
		m_vecTmp.x	=   0.0f;
		m_vecTmp.y	=   0.0f;
		m_vecTmp.z	= -45.0f;
		m_objKittyLeg.transform.localEulerAngles	= m_vecTmp;
	}
	
	public void SetKittyLeg_StraightLine()
	{
		SetKittyLeg();
		SetLegType(LEG_TYPE._STRAIGHT_LINE);
		
		m_vecTmp.x	=  0.0f;
		m_vecTmp.y	=  0.0f;
		m_vecTmp.z	=  0.0f;
		m_objKittyLeg.GetComponent<BoxCollider>().center	= m_vecTmp;
		m_vecTmp.x	=  DefineBaseManager.inst.KittyGap * 0.1f;
		m_vecTmp.y	=  DefineBaseManager.inst.KittyGap + DefineBaseManager.inst.KittyGap * 0.1f;
		m_vecTmp.z	=  1.0f;
		m_objKittyLeg.GetComponent<BoxCollider>().size		= m_vecTmp;
		m_vecTmp.x	=  0.0f;
		m_vecTmp.y	=  0.0f;
		m_vecTmp.z	=  0.0f;
		m_objKittyLeg.transform.localEulerAngles	= m_vecTmp;
	}
#endregion
	
#region Kitty Resource UV
	private void SetKittyHeadUV(int _UV)
	{
		if(m_imgKittyHead != null)
		{
//			m_imgKittyHead.SetcontrolIsEnabled(!m_bIsTurn);
			m_imgKittyHead.GetEZGUITexture().animations	= null;
			m_imgKittyHead.GetEZGUITexture().SetUVs(m_rectKittyFaceUV[_UV]);
		}
	}
	
	private void SetKittyLegUV()
	{
		if(m_dLegType == LEG_TYPE._NULL)
		{ return ; }
		if(m_rectKittyLegUV == null)
		{ return ; }
		
		if(m_imgKittyLeg != null)
		{
//			m_imgKittyLeg.SetcontrolIsEnabled(false);
			m_imgKittyLeg.GetEZGUITexture().animations	= null;
			m_imgKittyLeg.GetEZGUITexture().SetUVs(m_rectKittyLegUV[(int)m_dLegType]);
		}
	}
	
	public void SetKittyHeadUVList(List<Rect> _KittyHeadUV)
	{
		m_rectKittyFaceUV	= _KittyHeadUV;
		SetKittyHeadUV(0);
	}
	
	public void SetKittyLegUVList(List<Rect> _KittyLegUV)
	{
		m_rectKittyLegUV	= _KittyLegUV;
		SetKittyLegUV();
	}
#endregion
	
#region Kitty Turn Process
	public void SetKittyProcess()
	{
		if(m_imgKittyHead)
		{
			m_imgKittyHead.SetValueChangedDelegate(KittyProcess);
		}
	}
	
	public void InputKittyTurn(int _TurnType)
	{
		switch((KITTY_TURN_TYPE)DefineBaseManager.inst.KittyTurnType)
		{
		case KITTY_TURN_TYPE._REBOUND:
			if(_TurnType == (int)KITTY_TURN_TYPE._RIGHT)
			{
				SetKittyTurn((int)KITTY_TURN_TYPE._LEFT);
			}
			else
			{
				SetKittyTurn((int)KITTY_TURN_TYPE._RIGHT);
			}
			break;
			
		case KITTY_TURN_TYPE._LEFT:
			SetKittyTurn((int)KITTY_TURN_TYPE._LEFT);
			break;
			
		case KITTY_TURN_TYPE._RIGHT:
			SetKittyTurn((int)KITTY_TURN_TYPE._RIGHT);
			break;
		}
	}
	
	private void KittyProcess(IUIObject CurObject)
	{
		if(Main.input.GetTouchCount() >= 2)
		{ return ; }
		if(Main.game.GetIsGameEnd())
		{ return ; }
		if(Main.game.GetGameItemManager().GetUseActiveItem())
		{
			Main.game.UseActiveItem(m_dKittyPosX, m_dKittyPosY);
			return ;
		}
		if(Main.game.GetInputCount() <= 0)
		{ return ; }
		
		switch((KITTY_TURN_TYPE)DefineBaseManager.inst.KittyTurnType)
		{
		case KITTY_TURN_TYPE._LEFT:
			SetKittyTurn((int)KITTY_TURN_TYPE._LEFT);
			Main.game.ClickKitty();
			break;
			
		case KITTY_TURN_TYPE._RIGHT:
			SetKittyTurn((int)KITTY_TURN_TYPE._RIGHT);
			Main.game.ClickKitty();
			break;
			
		case KITTY_TURN_TYPE._REBOUND:
			SetKittyTurn((int)KITTY_TURN_TYPE._RIGHT);
			Main.game.ClickKitty();
			break;
		}
	}
	
	private void SetKittyTurn(int TurnType)
	{
		if(m_bIsTurn)
		{ return ; }
		if(Main.game.IsPause())
		{ return ; }
		
		SetIsTurn(true);
		m_fRotTime	= 0.0f;
		
		m_dKittyTurnType	= TurnType;
		SetKittyHeadUV(Random.Range(1, 3));
		InputKittyTurn();
		Main.game.OneTurn(m_dKittyPosX, m_dKittyPosY);
	}
	
	private void EndKittyTurn()
	{
		if(m_dKittyTurnType == (int)KITTY_TURN_TYPE._LEFT)
		{
			SetRotateType(m_dKittyRotType+m_dKittyAddTurn);
		}
		else
		{
			SetRotateType(m_dKittyRotType-m_dKittyAddTurn);
		}
		
		SetIsTurn(false);
		enabled	= true;
		SetKittyHeadUV(0);
		m_dKittyAddTurn	= 1;
		
		if(m_objKittyLeg != null)
		{
			Collider[] MyTrigger	= m_objKittyLeg.GetComponent<IsTrigger>().GetTriggerObject();
			
			for(int j = 0; j < MyTrigger.Length; j++)
			{
				if(MyTrigger[j] != null)
				{
					if(MyTrigger[j].transform.parent != null)
					{
						if(MyTrigger[j].transform.parent.parent != null)
						{
							KittyTotalObject tmpScript	= MyTrigger[j].transform.parent.parent.GetComponent<KittyTotalObject>();
							if(tmpScript != null)
							{
								int TriggerPosX	= tmpScript.GetKittyPosX();
								int TriggerPosY	= tmpScript.GetKittyPosY();
								
								if(TriggerPosX == m_dKittyPosX && TriggerPosY == m_dKittyPosY)
									continue;
								
								tmpScript.InputKittyTurn(GetKittyTurnType());
							}
						}
					}
				}
			}
		}
		Main.game.EndTurn(m_dKittyPosX, m_dKittyPosY);
	}
#endregion
	
#region Event
	// 1020
	private void SetLegType(LEG_TYPE _Type)
	{
		m_dBeforeLegType	= m_dLegType;
		m_dLegType			= _Type;
		SetKittyLegUV();
	}
	
	public void ReverseKittyTurn()
	{
		switch((KITTY_TURN_TYPE)DefineBaseManager.inst.KittyTurnType)
		{
		case KITTY_TURN_TYPE._REBOUND:
			m_dKittyAddTurn	= 2;
			if(m_dKittyTurnType == (int)KITTY_TURN_TYPE._RIGHT)
			{
				SetKittyTurn((int)KITTY_TURN_TYPE._LEFT);
			}
			else
			{
				SetKittyTurn((int)KITTY_TURN_TYPE._RIGHT);
			}
			break;
		}
	}
	
	public void SetLegChange(LEG_TYPE _Type, float _Time)
	{
		if(m_bIsChangeLeg)
		{
			m_dLegType	= m_dBeforeLegType;
			if(m_fChangeTime - m_fChangeDeltaTime < _Time)
			{
				m_fChangeTime		= _Time;
				m_fChangeDeltaTime	= 0.0f;
			}
		}
		else
		{
			m_fChangeTime		= _Time;
			m_fChangeDeltaTime	= 0.0f;
		}
		
		switch(_Type)
		{
		case LEG_TYPE._NULL:
			return;
			
		case LEG_TYPE._RIGHT_ANGLE:
			SetKittyLeg_RightAngle();
			break;
			
		case LEG_TYPE._DIAMOND:
			SetKittyLeg_Diamond();
			break;
			
		case LEG_TYPE._STRAIGHT_LINE:
			SetKittyLeg_StraightLine();
			break;
		}
		
		SetIsChangeLeg(true);
	}
	
	// 1030
	public void SetRefresh()
	{
		int ran	= Random.Range(0, DefineBaseManager.inst.KittyLegDir);
		
		if(ran == m_dKittyRotType)
		{
			SetRefresh();
		}
		else
		{
			SetRotateType(ran);
		}
	}
#endregion
	
	public void SetKittyLegRestore()
	{
		if(m_dBeforeLegType != LEG_TYPE._NULL)
		{
			switch(m_dBeforeLegType)
			{
			case LEG_TYPE._RIGHT_ANGLE:
				SetKittyLeg_RightAngle();
				break;
				
			case LEG_TYPE._DIAMOND:
				SetKittyLeg_Diamond();
				break;
				
			case LEG_TYPE._STRAIGHT_LINE:
				SetKittyLeg_StraightLine();
				break;
			}
		}
		
		m_dBeforeLegType	= LEG_TYPE._NULL;
	}
	
	public void SetRotateType(int _RotType)
	{
		if(_RotType > DefineBaseManager.inst.KittyLegDir)
		{
			_RotType	= _RotType % DefineBaseManager.inst.KittyLegDir;
		}
		else if(_RotType < 0)
		{
			_RotType	+= DefineBaseManager.inst.KittyLegDir;
		}
		
		m_dKittyRotType	= _RotType;
		
		SetRotateTmp(0.0f);
	}
	
	private void SetRotateTmp(float _AddRot)
	{
		m_vecTmp.x	= 0.0f;
		m_vecTmp.y	= 0.0f;
		m_vecTmp.z	= (360.0f/DefineBaseManager.inst.KittyLegDir)*m_dKittyRotType + _AddRot;
		m_transKittyHead.eulerAngles	= m_vecTmp;
	}
	
	private void SetUpdate_Kitty()
	{
		if(m_dKittyTurnType == (int)KITTY_TURN_TYPE._LEFT)
		{
			SetRotateTmp(m_fRotTime * m_dKittyAddTurn);
		}
		else
		{
			SetRotateTmp(-(m_fRotTime * m_dKittyAddTurn));
		}
		
		if(m_fRotTime >= m_fKittySpeed)
		{
			EndKittyTurn();
		}
	}
	
	private void SetUpdate_KittyLeg()
	{
		if(m_fChangeDeltaTime > m_fChangeTime)
		{
			SetKittyLegRestore();
		}
	}
	
	public int GetKittyTurnType()	{ return m_dKittyTurnType; }
}