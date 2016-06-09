using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DefineBase;

public class KittyTotalManager : KittyManagerBase
{
	enum SEASON_TYPE
	{
		_SPRING = 0,
		_SUMMER,
		_AUTUMN,
		_WINTER,
		_MAX,
	}
	
	public static Object	objCharacter;
	private Dictionary<int, List<Rect>>	m_rectFaceUV;
	private Dictionary<int, List<Rect>>	m_rectLegUV;
	
	public static Object	objBackground;
	private Dictionary<int, List<Rect>>	m_listEffectUV;
	
	private const int m_dKittyLevelMax	= 7;
	
	private float m_fEffectSize;
	
	private GameObject	m_objKittyRoot;
	
	public void OnDestroy()
	{
		objBackground	= null;
		
		ReleaseResource();
	}
	
#region IClassBase
	public override void Create()
	{
		base.Create ();
		if(objBackground == null)
		{
			objBackground	= ResourceLoad.GetObject("KittyFace", string.Format("{0}/Model/KittyEffect/Background", ResourceLoad.CheckHeight));
		}
	}
#endregion
	
#region Resource Setting
	protected override void LoadResource()
	{
		m_objKittyRoot	= new GameObject("KittyObjectRoot");
		m_objKittyRoot.transform.parent	= Main.inst.GetResourceObject_EZGUITEXTURE().transform;
		
		m_vecTmpPos.x	=  0.0f;
		m_vecTmpPos.y	=  0.0f;
		m_vecTmpPos.z	= 90.0f;
		m_objKittyRoot.transform.position	= m_vecTmpPos;
	}
	
	protected override void SetResources()
	{
		SetCharacterFaceUV();
		SetCharacterLegUV();
		SetEffectUV();
	}
	
	private void SetCharacterFaceUV()
	{
		int packtype	= Main.game.GetPackageType();
		for(int kittylevel = 0; kittylevel < m_dKittyLevelMax; kittylevel++)
		{
			GameObject	tmpObj;
			
			string ObjectPath;
			if(packtype < 10)
			{
				ObjectPath	= string.Format("{0}/Mobile/EZGUI/Game/Kitty/Character/Skin0{1}/Face/Level{2}", ResourceLoad.CheckHeight, packtype, kittylevel);
			}
			else
			{
				ObjectPath	= string.Format("{0}/Mobile/EZGUI/Game/Kitty/Character/Skin{1}/Face/Level{2}", ResourceLoad.CheckHeight, packtype, kittylevel);
			}
			Object obj		= ResourceLoad.GetObject("KittyFace", ObjectPath);
			if(objCharacter == null)	{ objCharacter	= obj; }
			tmpObj		= Instantiate(obj);
			
			Rect		tmpUV1	= (Rect)tmpObj.GetComponent<UIButton>().GetUVs();
			tmpObj.GetComponent<UIButton>().SetState((int)UIButton.CONTROL_STATE.OVER);
			Rect		tmpUV2	= (Rect)tmpObj.GetComponent<UIButton>().GetUVs();
			tmpObj.GetComponent<UIButton>().SetState((int)UIButton.CONTROL_STATE.ACTIVE);
			Rect		tmpUV3	= (Rect)tmpObj.GetComponent<UIButton>().GetUVs();
			
			SetFaceUVs(kittylevel, tmpUV1);
			SetFaceUVs(kittylevel, tmpUV2);
			SetFaceUVs(kittylevel, tmpUV3);
			
			DestroyImmediate(tmpObj);
		}
	}
	
	private void SetCharacterLegUV()
	{
		int packtype	= Main.game.GetPackageType();
		for(int kittylevel = 0; kittylevel < m_dKittyLevelMax; kittylevel++)
		{
			GameObject	tmpObj;
			
			string ObjectPath;
			if(packtype < 10)
			{
				ObjectPath	= string.Format("{0}/Mobile/EZGUI/Game/Kitty/Character/Skin0{1}/Legs/Level{2}", ResourceLoad.CheckHeight, packtype, kittylevel);
			}
			else
			{
				ObjectPath	= string.Format("{0}/Mobile/EZGUI/Game/Kitty/Character/Skin{1}/Legs/Level{2}", ResourceLoad.CheckHeight, packtype, kittylevel);
			}
			Object obj		= ResourceLoad.GetObject("KittyLegs", ObjectPath);
			if(objCharacter == null)	{ objCharacter	= obj; }
			tmpObj			= Instantiate(obj);
			
			Rect		tmpUV1	= (Rect)tmpObj.GetComponent<UIButton>().GetUVs();
			tmpObj.GetComponent<UIButton>().SetState((int)UIButton.CONTROL_STATE.OVER);
			Rect		tmpUV2	= (Rect)tmpObj.GetComponent<UIButton>().GetUVs();
			tmpObj.GetComponent<UIButton>().SetState((int)UIButton.CONTROL_STATE.ACTIVE);
			Rect		tmpUV3	= (Rect)tmpObj.GetComponent<UIButton>().GetUVs();
			
			SetLegUVs(kittylevel, tmpUV1);
			SetLegUVs(kittylevel, tmpUV2);
			SetLegUVs(kittylevel, tmpUV3);
			
			DestroyImmediate(tmpObj);
		}
	}
	
	private void SetEffectUV()
	{
		if(m_listEffectUV != null)
		{ return ; }
		
		m_listEffectUV	= new Dictionary<int, List<Rect>>((int)SEASON_TYPE._MAX);
		
		string ObjectPath;
		ObjectPath	= string.Format("{0}/Mobile/EZGUI/Game/Kitty/Effect/Background", ResourceLoad.CheckHeight);
		GameObject	tmpBackground		= ResourceLoad.GetGameObject("Background", ObjectPath);
		DefineBaseManager.inst.SetKittyGap(tmpBackground.GetComponent<UIButton>().ImageSize.x);
		
		Rect		tmpBackgroundUV		= (Rect)tmpBackground.GetComponent<UIButton>().GetUVs();
		
		ObjectPath	= string.Format("{0}/Mobile/EZGUI/Game/Kitty/Effect/TurnBack", ResourceLoad.CheckHeight);
		GameObject	tmpTurnBack			= ResourceLoad.GetGameObject("TurnBack", ObjectPath);
		Rect		tmpTurnBackUV0		= (Rect)tmpTurnBack.GetComponent<UIButton>().GetUVs();
		tmpTurnBack.GetComponent<UIButton>().SetState((int)UIButton.CONTROL_STATE.OVER);
		Rect		tmpTurnBackUV1		= (Rect)tmpTurnBack.GetComponent<UIButton>().GetUVs();
		tmpTurnBack.GetComponent<UIButton>().SetState((int)UIButton.CONTROL_STATE.ACTIVE);
		Rect		tmpTurnBackUV2		= (Rect)tmpTurnBack.GetComponent<UIButton>().GetUVs();
		tmpTurnBack.GetComponent<UIButton>().SetState((int)UIButton.CONTROL_STATE.DISABLED);
		Rect		tmpTurnBackUV3		= (Rect)tmpTurnBack.GetComponent<UIButton>().GetUVs();
		
		ObjectPath	= string.Format("{0}/Mobile/EZGUI/Game/Kitty/Effect/Effect", ResourceLoad.CheckHeight);
		GameObject	tmpEffect			= ResourceLoad.GetGameObject("Effect", ObjectPath);
		m_fEffectSize	= tmpEffect.GetComponent<UIButton>().ImageSize.x;
		
		Rect		tmpEffectUV0		= (Rect)tmpEffect.GetComponent<UIButton>().GetUVs();
		tmpEffect.GetComponent<UIButton>().SetState((int)UIButton.CONTROL_STATE.OVER);
		Rect		tmpEffectUV1		= (Rect)tmpEffect.GetComponent<UIButton>().GetUVs();
		tmpEffect.GetComponent<UIButton>().SetState((int)UIButton.CONTROL_STATE.ACTIVE);
		Rect		tmpEffectUV2		= (Rect)tmpEffect.GetComponent<UIButton>().GetUVs();
		tmpEffect.GetComponent<UIButton>().SetState((int)UIButton.CONTROL_STATE.DISABLED);
		Rect		tmpEffectUV3		= (Rect)tmpEffect.GetComponent<UIButton>().GetUVs();
		
		int seasontype	= 0;
		SetSeasonUVs(seasontype, tmpBackgroundUV);
		SetSeasonUVs(seasontype, tmpTurnBackUV0);
		SetSeasonUVs(seasontype, tmpEffectUV0);
		seasontype++;
		SetSeasonUVs(seasontype, tmpBackgroundUV);
		SetSeasonUVs(seasontype, tmpTurnBackUV1);
		SetSeasonUVs(seasontype, tmpEffectUV1);
		seasontype++;
		SetSeasonUVs(seasontype, tmpBackgroundUV);
		SetSeasonUVs(seasontype, tmpTurnBackUV2);
		SetSeasonUVs(seasontype, tmpEffectUV2);
		seasontype++;
		SetSeasonUVs(seasontype, tmpBackgroundUV);
		SetSeasonUVs(seasontype, tmpTurnBackUV3);
		SetSeasonUVs(seasontype, tmpEffectUV3);
		
		DestroyImmediate(tmpBackground);
		DestroyImmediate(tmpTurnBack);
		DestroyImmediate(tmpEffect);
	}
	
	public override void ReleaseResource()
	{
		if(m_rectFaceUV != null)
		{
			int count	= m_rectFaceUV.Count;
			for(int i = 0; i < count; i++)
			{
				m_rectFaceUV[i].Clear();
			}
			m_rectFaceUV.Clear();
			m_rectFaceUV	= null;
		}
		if(m_rectLegUV != null)
		{
			int count	= m_rectLegUV.Count;
			for(int i = 0; i < count; i++)
			{
				m_rectLegUV[i].Clear();
			}
			m_rectLegUV.Clear();
			m_rectLegUV		= null;
		}
		objCharacter	= null;
		base.ReleaseResource();
	}
	
	protected override void SetKittyObject(int _line)
	{
		float Gap		= DefineBaseManager.inst.KittyGap;
		for(int j = 0; j < LoadObjNum; j++)
		{
			GameObject tmpObj;
			
			tmpObj	= Instantiate(objBackground);
			tmpObj.name						= string.Format("{0}{1}_{2}", "Kitty_Object_", m_loadObjectLineX, m_loadObjectLineY);
			
			m_vecTmpPos.x	=  (m_loadObjectLineX*Gap);
			m_vecTmpPos.y	= -(m_loadObjectLineY*Gap);
			m_vecTmpPos.z	=  0.0f;
			tmpObj.transform.parent			= m_objKittyRoot.transform;
			tmpObj.transform.localPosition	= m_vecTmpPos;
			m_vecTmpPos.x	= 1.0f;
			m_vecTmpPos.y	= 1.0f;
			m_vecTmpPos.z	= 1.0f;
			tmpObj.transform.localScale		= m_vecTmpPos;
			
			KittyTotalObject tmpScript		= tmpObj.AddComponent<KittyTotalObject>();
			tmpScript.Create();
			tmpScript.SetKittyObject();
			tmpScript.SetKittyHead(objCharacter);
			tmpScript.SetKittyLeg(objCharacter);
            LEG_TYPE legType = LEG_TYPE._NULL;
            switch ( (LEG_BASE_SET)DefineBaseManager.inst.KittyTurnLegType )
            {
                case LEG_BASE_SET._RIGHT_ANGLE:
                    legType = LEG_TYPE._RIGHT_ANGLE;
                    break;
                case LEG_BASE_SET._RIGHT_AND_STRAIGHT:
                    if ( Random.Range(0, DefineBaseManager.kKittyLegStraight) == 1 )
                    {
                        legType = LEG_TYPE._STRAIGHT_LINE;
                    }
                    else
                    {
                        legType = LEG_TYPE._RIGHT_ANGLE;
                    }
                    break;
                case LEG_BASE_SET._RIGHT_AND_DIAMOND:
                    if ( Random.Range(0, DefineBaseManager.kKittyLegDiamond) == 1 )
                    {
                        legType = LEG_TYPE._DIAMOND;
                    }
                    else
                    {
                        legType = LEG_TYPE._RIGHT_ANGLE;
                    }
                    break;
                case LEG_BASE_SET._STRAIGHT_LINE:
                    legType = LEG_TYPE._STRAIGHT_LINE;
                    break;
                case LEG_BASE_SET._STRAIGHT_AND_DIAMOND:
                    if ( Random.Range(0, DefineBaseManager.kKittyLegDiamond) == 1 )
                    {
                        legType = LEG_TYPE._DIAMOND;
                    }
                    else
                    {
                        legType = LEG_TYPE._STRAIGHT_LINE;
                    }
                    break;
                case LEG_BASE_SET._ALL_RANDOM:
                    if ( Random.Range(0, DefineBaseManager.kKittyLegDiamond) == 1 )
                    {
                        legType = LEG_TYPE._DIAMOND;
                    }
                    else if ( Random.Range(0, DefineBaseManager.kKittyLegStraight) == 1 )
                    {
                        legType = LEG_TYPE._STRAIGHT_LINE;
                    }
                    else
                    {
                        legType = LEG_TYPE._RIGHT_ANGLE;
                    }
                    break;
            }
            switch ( legType )
            {
                case LEG_TYPE._RIGHT_ANGLE:
                    tmpScript.SetKittyLeg_RightAngle();
                    break;
                case LEG_TYPE._STRAIGHT_LINE:
                    tmpScript.SetKittyLeg_StraightLine();
                    break;
                case LEG_TYPE._DIAMOND:
                    tmpScript.SetKittyLeg_Diamond();
                    break;
            }
			tmpScript.SetEffectTurnBack(objBackground);
			tmpScript.SetEffectParticle(objBackground, m_fEffectSize);
			tmpScript.SetKittyProcess();
			tmpScript.SetKittyPos(m_loadObjectLineX, m_loadObjectLineY);
			
			if(!AddKittyObject(m_loadObjectLineX, tmpScript))
			{
				return;
			}
		}
	}
	
	public override void SetBasePos()
	{
		int MaxMap	= DefineBaseManager.inst.KittyMaxMapX;
		if(MaxMap < DefineBaseManager.inst.KittyMaxMapY)
		{
			MaxMap	= DefineBaseManager.inst.KittyMaxMapY;
		}
		if(DefineBaseManager.inst.GameBaseHeight == 960.0f)
		{
			m_vecTmpPos.x	= 12.7f/MaxMap;
			m_vecTmpPos.y	= 12.7f/MaxMap;
			m_vecTmpPos.z	=  1.0f;
		}
		else
		{
			m_vecTmpPos.x	= 14.4f/MaxMap;
			m_vecTmpPos.y	= 14.4f/MaxMap;
			m_vecTmpPos.z	=  1.0f;
		}
		m_objKittyRoot.transform.localScale	= m_vecTmpPos;
		
		if(m_listKitty != null)
		{
			float Gap	= DefineBaseManager.inst.KittyGap;
			int MaxX	= m_listKitty.Count;
			for(int i = 0; i < MaxX; i++)
			{
				float computeX	= (1.0f - MaxX + 2.0f*i) / 2.0f;
				int MaxY	= m_listKitty[i].Count;
				for(int j = 0; j < MaxY; j++)
				{
					float computeY	= (1.0f - MaxY + 2.0f*j) / 2.0f;
					m_listKitty[i][j].GetComponent<KittyTotalObject>().SetBasePos((computeX*Gap), -(computeY*Gap));
				}
			}
		}
	}
	
	public override void SetRandomDir()
	{
		int seasonType	= Random.Range(0, (int)SEASON_TYPE._MAX);
		KittyTotalObject.SetEffectUVList(m_listEffectUV[seasonType]);
		
		for(int i = 0; i < m_listKitty.Count; i++)
		{
			for(int j = 0; j < m_listKitty[i].Count; j++)
			{
				if(m_listKitty[i][j].GetComponent<KittyTotalObject>())
				{
					int kittylevel			= Random.Range(0, m_dKittyLevelMax);
					KittyTotalObject tmpScript	= m_listKitty[i][j].GetComponent<KittyTotalObject>();
					
					tmpScript.SetValid(false);
					tmpScript.KittyLevel	= kittylevel;
					tmpScript.SetKittyHeadUVList(m_rectFaceUV[kittylevel]);
					tmpScript.SetKittyLegUVList(m_rectLegUV[kittylevel]);
					tmpScript.SetEffectUV();
					tmpScript.SetKittyPos(i, j);
					tmpScript.SetRotateType(Random.Range(0, DefineBaseManager.inst.KittyLegDir));
				}
			}
		}
	}
#endregion
	
	protected void SetFaceUVs(int _KittyLevel, Rect _tmpRect)
	{
		// Face
		if(m_rectFaceUV == null)	{ m_rectFaceUV	= new Dictionary<int, List<Rect>>(7); }
		
		if(m_rectFaceUV.ContainsKey(_KittyLevel))
		{
			List<Rect> tmpList	= m_rectFaceUV[_KittyLevel];
			tmpList.Add(_tmpRect);
		}
		else
		{
			List<Rect> tmpList	= new List<Rect>(3);
			tmpList.Add(_tmpRect);
			
			m_rectFaceUV.Add(_KittyLevel, tmpList);
		}
	}
	
	protected void SetLegUVs(int _KittyLevel, Rect _tmpRect)
	{
		// Leg
		if(m_rectLegUV == null)		{ m_rectLegUV	= new Dictionary<int, List<Rect>>(7); }
		
		if(m_rectLegUV.ContainsKey(_KittyLevel))
		{
			List<Rect> tmpList	= m_rectLegUV[_KittyLevel];
			tmpList.Add(_tmpRect);
		}
		else
		{
			List<Rect> tmpList	= new List<Rect>(4);
			tmpList.Add(_tmpRect);
			
			m_rectLegUV.Add(_KittyLevel, tmpList);
		}
	}
	
	protected void SetSeasonUVs(int _SeasonType, Rect _tmpRect)
	{
		// Effect
		if(m_listEffectUV.ContainsKey(_SeasonType))
		{
			List<Rect> tmpList		= m_listEffectUV[_SeasonType];
			tmpList.Add(_tmpRect);
		}
		else
		{
			List<Rect> tmpList		= new List<Rect>(3);
			tmpList.Add(_tmpRect);
			m_listEffectUV.Add(_SeasonType, tmpList);
		}
	}
}