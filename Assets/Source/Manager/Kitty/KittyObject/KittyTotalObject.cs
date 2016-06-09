using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DefineBase;

public sealed partial class KittyTotalObject : MonoBehaviour
{
	private static Vector3		m_vecTmp;
	private static Color			m_colorTmp;
	
	private static int				m_LayerMask	= -1;
	
	private bool					m_bIsTurn;
	private bool					m_bIsEffect;
	private bool					m_bIsChangeLeg;
	
	private float					m_fRotTime;
	
	private int						m_dKittyLevel;
	private int						m_dKittyPosX;
	private int						m_dKittyPosY;
	
	
#region ObjectBase
	public void Create()
	{
		if(m_vecTmp == null)			{ m_vecTmp	= Vector3.zero; }
		if(m_LayerMask == -1)			{ m_LayerMask	= LayerMask.NameToLayer("GameCamera"); }
		Create_Kitty();
		Create_Effect();
		
		SetIsTurn(false);
		SetIsEffect(false);
	}
	
	public void SetValid(bool IsValid)
	{
		if(!IsValid)
		{
			SetIsTurn(IsValid);
			SetIsEffect(IsValid);
			enabled	= IsValid;
		}
		
		gameObject.SetActiveRecursively(IsValid);
		SetValid_Kitty(IsValid);
		SetValid_Effect(m_bIsTurn && IsValid);
	}
	
	public void Release()
	{
		Release_Kitty();
		Release_Effect();
	}
	
	private void SetIsTurn(bool _IsTurn)
	{
		if(_IsTurn)
		{
			enabled		= _IsTurn;
		}
		m_bIsTurn	= _IsTurn;
		
		if(m_objKittyLeg != null)
		{
			m_objKittyLeg.GetComponent<IsTrigger>().SetValid(_IsTurn);
		}
	}
	
	private void SetIsEffect(bool _IsEffect)
	{
		if(_IsEffect)
		{
			enabled		= _IsEffect;
		}
		m_bIsEffect	= _IsEffect;
	}
	
	private void SetIsChangeLeg(bool _IsChange)
	{
		if(_IsChange)
		{
			enabled		= _IsChange;
		}
		m_bIsChangeLeg	= _IsChange;
	}
	
	public void SetPause(bool _IsPause)
	{
		if(m_bIsTurn || m_bIsEffect || m_bIsChangeLeg)
		{
			enabled	= !_IsPause;
		}
	}
#endregion
	
#region Resource Setting
	public int KittyLevel
	{
		get	{ return m_dKittyLevel; }
		set { m_dKittyLevel	= value; }
	}
	
	public void SetKittyPos(int _KittyPosX, int _KittyPosY)
	{
		m_dKittyPosX	= _KittyPosX;
		m_dKittyPosY	= _KittyPosY;
	}
	
	public void SetBasePos(float _PosX, float _PosY)
	{
		m_vecTmp.x	= _PosX;
		m_vecTmp.y	= _PosY;
		m_vecTmp.z	= 0.0f;
		transform.localPosition	= m_vecTmp;
		
		SetTmpEffectZero();
		if(m_transKittyHead)
		{
			m_transKittyHead.localScale				= m_vecTmp;
			m_transKittyHead.localPosition			= m_HeadPos;
		}
		if(m_imgKittyLeg)
		{
			m_imgKittyLeg.transform.localScale		= m_vecTmp;
			m_imgKittyLeg.transform.localPosition	= m_LegPos;
		}
		if(m_objKittyLeg)
		{
			m_objKittyLeg.transform.localScale		= m_vecTmp;
			m_objKittyLeg.transform.localPosition	= m_LegBoxPos;
		}
		if(m_objTurnBack)
		{
			m_objTurnBack.transform.localScale		= m_vecTmp;
			m_objTurnBack.transform.localPosition	= m_posTurnBack;
		}
	}
	
	public int GetKittyPosX()	{ return m_dKittyPosX; }
	public int GetKittyPosY()	{ return m_dKittyPosY; }
#endregion
	
	public void FixedUpdate()
	{
		if(m_bIsChangeLeg)
		{
			m_fChangeDeltaTime	+= Time.smoothDeltaTime;
			SetUpdate_KittyLeg();
		}
		else
		{
			SetIsChangeLeg(false);
		}
		
		if(m_bIsTurn || m_bIsEffect)
		{
			float Speed	= DefineBaseManager.inst.KittyTurnSpeed * Time.smoothDeltaTime;
			
			m_fRotTime	+= Speed;
			if(m_bIsTurn)
			{
				SetUpdate_Kitty();
			}
			if(m_bIsEffect)
			{
				SetUpdate_Effect();
			}
		}
		else
		{
			SetIsTurn(false);
			SetIsEffect(false);
		}
		
		if(!m_bIsTurn && !m_bIsEffect && !m_bIsChangeLeg)
		{
			enabled	= false;
		}
	}
}