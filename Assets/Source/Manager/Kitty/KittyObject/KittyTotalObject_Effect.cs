using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DefineBase;

public sealed partial class KittyTotalObject : MonoBehaviour
{
	private static Vector3			m_posTurnBack	= new Vector3(0.0f, 0.0f, -1.0f);
	private static float			m_posEffectZ	= -5.0f;
	
	private static float			m_fBackSpeed	= 150.0f;
	private static float			m_fParticleSpeed= 200.0f;
	
	private static List<Rect>		m_rectEffectUV;
	
	private GameObject				m_objTurnBack;
	private List<GameObject>		m_objParticleList;
	
	private ControlMesh				m_renBackground;
	private ControlMesh				m_renTurnBack;
	private List<ControlMesh>		m_renParticleList;
	
	
#region ObjectBase
	private void Create_Effect()
	{
	}
	
	private void SetValid_Effect(bool IsValid)
	{
		if(m_objTurnBack != null)
		{
			m_objTurnBack.SetActiveRecursively(IsValid);
		}
		if(m_objParticleList != null)
		{
			int ParticleNum	= Random.Range(DefineBaseManager.inst.EffectParticleMin, DefineBaseManager.inst.EffectParticleMax);
			for(int i = 0; i < m_objParticleList.Count; i++)
			{
				if(m_objParticleList[i] != null)
				{
					if(i <= ParticleNum)
					{
						if(IsValid)
						{
							SetTmpEffectPos();
							m_objParticleList[i].transform.localPosition	= m_vecTmp;
							SetTmpEffectRot();
							m_objParticleList[i].transform.eulerAngles		= m_vecTmp;
//							SetTmpEffectZero();
							SetTmpEffectScale();
							m_objParticleList[i].transform.localScale		= m_vecTmp;
						}
						m_objParticleList[i].SetActiveRecursively(IsValid);
					}
					else
					{
						m_objParticleList[i].SetActiveRecursively(false);
					}
				}
			}
		}
	}
	
	private void SetTmpEffectPos()
	{
		float PosX	= Random.Range(-DefineBaseManager.inst.KittyGap*10, DefineBaseManager.inst.KittyGap*10) / 25;
		float PosY	= Random.Range(-DefineBaseManager.inst.KittyGap*10, DefineBaseManager.inst.KittyGap*10) / 25;
		
		m_vecTmp.x	= PosX;
		m_vecTmp.y	= PosY;
		m_vecTmp.z	= m_posEffectZ;
	}
	
	private void SetTmpEffectRot()
	{
		m_vecTmp.x	= 0.0f;
		m_vecTmp.y	= 0.0f;
		m_vecTmp.z	= Random.Range(0, 360.0f);
	}
	
	private void SetTmpEffectScale()
	{
		int RandomS	= Random.Range(0, 50);
		float ParticleScale	= 1.0f + (RandomS/100.0f);
		
		m_vecTmp.x	= (ParticleScale);
		m_vecTmp.y	= (ParticleScale);
		m_vecTmp.z	= 1.0f;
	}
	
	private void SetTmpEffectZero()
	{
		m_vecTmp.x	= 1.0f;
		m_vecTmp.y	= 1.0f;
		m_vecTmp.z	= 1.0f;
	}
	
	private void Release_Effect()
	{
		ReleaseTurnBack();
		ReleaseParticleList();
	}
	
	private void ReleaseTurnBack()
	{
		if(m_objTurnBack != null)
		{
			DestroyImmediate(m_objTurnBack);
		}
	}
	
	private void ReleaseParticleList()
	{
		if(m_objParticleList != null)
		{
			for(int i = 0; i < m_objParticleList.Count; i++)
			{
				if(m_objParticleList[i] != null)
				{
					DestroyImmediate(m_objParticleList[i]);
				}
			}
			m_objParticleList.Clear();
		}
	}
#endregion
	
#region Effect Resource Setting
	public void SetKittyObject()
	{
		SetTmpEffectZero();
		gameObject.layer		= m_LayerMask;
		
//		m_renBackground	= gameObject.AddComponent<ControlMesh>();
		m_renBackground	= new ControlMesh();
		m_renBackground.Create(gameObject);
		m_renBackground.Anchor	= SpriteRoot.ANCHOR_METHOD.MIDDLE_CENTER;
		m_renBackground.SetSize(DefineBaseManager.inst.KittyGap+0.5f, DefineBaseManager.inst.KittyGap+0.5f);
	}
	
	public void SetEffectTurnBack(Object _BaseObj)
	{
		ReleaseTurnBack();
		
		m_objTurnBack							= (GameObject)Instantiate(_BaseObj);
		m_objTurnBack.name						= "EffectTurnBack";
		m_objTurnBack.layer						= m_LayerMask;
		
		m_objTurnBack.transform.parent			= this.transform;
		m_objTurnBack.transform.localPosition	= m_posTurnBack;
		m_objTurnBack.transform.eulerAngles		= Vector3.zero;
		SetTmpEffectZero();
		m_objTurnBack.transform.localScale		= m_vecTmp;
		
//		m_renTurnBack	= m_objTurnBack.AddComponent<ControlMesh>();
		m_renTurnBack	= new ControlMesh();
		m_renTurnBack.Create(m_objTurnBack);
		m_renTurnBack.Anchor	= SpriteRoot.ANCHOR_METHOD.MIDDLE_CENTER;
		m_renTurnBack.SetSize(DefineBaseManager.inst.KittyGap, DefineBaseManager.inst.KittyGap);
	}
	
	public void SetEffectParticle(Object _BaseObj, float _ImageSize)
	{
		ReleaseParticleList();
		if(m_objParticleList == null)			{ m_objParticleList	= new List<GameObject>(DefineBaseManager.inst.EffectParticleMax); }
		if(m_renParticleList == null)			{ m_renParticleList	= new List<ControlMesh>(DefineBaseManager.inst.EffectParticleMax); }
		
		for(int i = 0; i < DefineBaseManager.inst.EffectParticleMax; i++)
		{
			m_objParticleList.Add((GameObject)Instantiate(_BaseObj));
			m_objParticleList[i].name				= "EffectParticle_" + i;
			m_objParticleList[i].layer				= m_LayerMask;
			
			m_objParticleList[i].transform.parent	= this.transform;
			
//			m_renParticleList.Add(m_objParticleList[i].AddComponent<ControlMesh>());
			ControlMesh tmpControlMesh	= new ControlMesh();
			tmpControlMesh.Create(m_objParticleList[i]);
			tmpControlMesh.Anchor	= SpriteRoot.ANCHOR_METHOD.MIDDLE_CENTER;
			tmpControlMesh.SetSize(_ImageSize, _ImageSize);
			m_renParticleList.Add(tmpControlMesh);
		}
	}
#endregion
	
#region Effect Resource UV
	public static void SetEffectUVList(List<Rect> _KittyHeadUV)
	{
		m_rectEffectUV	= _KittyHeadUV;
	}
	
	public void SetEffectUV()
	{
		m_renBackground.SetUVs(m_rectEffectUV[0]);
		m_renTurnBack.SetUVs(m_rectEffectUV[1]);
		for(int i = 0; i < m_renParticleList.Count; i++)
		{
			m_renParticleList[i].SetUVs(m_rectEffectUV[2]);
		}
	}
#endregion
	
#region Effect Alpha
	private void SetEffectAlpha(float _Alpha)
	{
		SetEffectBackAlpha(_Alpha);
		SetEffectParticleAlpha(_Alpha);
	}
	
	private void SetEffectBackAlpha(float _Alpha)
	{
		SetAlpha(m_renTurnBack, _Alpha);
	}
	
	private void SetEffectParticleAlpha(float _Alpha)
	{
		if(m_renParticleList != null)
		{
			for(int i = 0; i < m_renParticleList.Count; i++)
			{
				SetAlpha(m_renParticleList[i], _Alpha);
			}
		}
	}
	
	private void SetAlpha(ControlMesh _ren, float _Alpha)
	{
		m_colorTmp		= _ren.color;
		m_colorTmp.a	= _Alpha;
		_ren.color		= m_colorTmp;
	}
#endregion
	
#region Kitty Turn Process
	public void InputKittyTurn()
	{
		if(Main.game.IsPause())
		{ return ; }
		
		SetValid_Effect(true);
		SetEffectAlpha(1.0f);
		
		SetIsEffect(true);
	}
	
	private void EndEffect()
	{
		SetValid_Effect(false);
		
		SetIsEffect(false);
	}
#endregion
	
	private void SetUpdate_Effect()
	{
		float BackRatio	= (m_fBackSpeed - m_fRotTime) / m_fBackSpeed;
		if(BackRatio >= 0)
		{
			SetEffectBackAlpha(BackRatio);
		}
		else
		{
			m_objTurnBack.SetActiveRecursively(false);
		}
		
		float ParticleRatio	= (m_fParticleSpeed - m_fRotTime*0.7f) / m_fParticleSpeed;
		if(ParticleRatio >= 0)
		{
			SetEffectParticleAlpha(ParticleRatio);
		}
		else
		{
			for(int i = 0; i < m_objParticleList.Count; i++)
			{
				m_objParticleList[i].SetActiveRecursively(false);
			}
		}
		
		if(m_fRotTime >= m_fBackSpeed && m_fRotTime >= m_fParticleSpeed)
		{
			EndEffect();
		}
	}
}