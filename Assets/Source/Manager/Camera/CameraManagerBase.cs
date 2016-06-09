using UnityEngine;
using System.Collections;
using DefineBase;

#pragma warning disable 0168 // variable declared but not used.
#pragma warning disable 0219 // variable assigned but not used.
#pragma warning disable 0414 // private field assigned but not used.

public class CameraManagerBase : MonoBehaviour
{
	protected Camera	m_Camera;
	
	protected Vector3	m_LocalPos;
	protected Vector3	m_LocalRot;
	
	protected Vector3	m_MoveLimit;
	protected Vector3	m_RotaLimit;
	
	protected Vector3	m_MoveDir;
	protected Vector3	m_LotDir;
	
	protected float		m_ZoomMin;
	protected float		m_ZoomMax;
	
	protected bool		m_bIsPIDMove;
	protected float		m_fPIDMove_Kp;
	protected float		m_fPIDMove_Ki;
	protected float		m_fPIDMove_ErrorAccX;
	protected float		m_fPIDMove_ErrorAccY;
	protected float		m_fPIDMove_ErrorAccZ;
	protected Vector3	m_vecPIDMove_Goal;
	
#region Create / Init
	public virtual void Create()
	{
		m_MoveLimit	= Vector3.zero;
		m_RotaLimit	= Vector3.zero;
		m_ZoomMin	= 0.0f;
		m_ZoomMax	= 0.0f;
		
		m_MoveDir	= Vector3.zero;
		m_LotDir	= Vector3.zero;
		
		m_bIsPIDMove	= false;
		
		if(!gameObject.GetComponent<Camera>())
		{
			gameObject.AddComponent<Camera>();
		}
		m_Camera	= gameObject.GetComponent<Camera>();
		
		m_LocalPos	= transform.localPosition;
		m_LocalRot	= transform.localEulerAngles;
	}
	public virtual void Init() {}
	public virtual void SetCameraInspector() {}
#endregion
	
	public void Update()
	{
		if(IsPIDMove)
		{
			PIDCameraMove();
		}
	}
	
	public float fieldOfView
	{
		get {
			if(m_Camera.orthographic)
			{
				return m_Camera.orthographicSize;
			}
			else
			{
				return m_Camera.fieldOfView;
			}
		}
		set {
			if(m_Camera.orthographic)
			{
				m_Camera.orthographicSize	= value;
			}
			else
			{
				m_Camera.fieldOfView	= value;
			}
			SetMoveLimit();
			CheckMoveLimit();
		}
	}
	
	public Vector3 MoveLimit()		{ return m_MoveLimit; }
	public float MoveLimitX()		{ return m_MoveLimit.x; }
	public float MoveLimitY()		{ return m_MoveLimit.y; }
	public float MoveLimitZ()		{ return m_MoveLimit.z; }
	
	public Vector3 RotLimit()		{ return m_RotaLimit; }
	public float RotLimitX()		{ return m_RotaLimit.x; }
	public float RotLimitY()		{ return m_RotaLimit.y; }
	public float RotLimitZ()		{ return m_RotaLimit.z; }
	
	public float ZoomMin()			{ return m_ZoomMin; }
	public float ZoomMax()			{ return m_ZoomMax; }
	
	public virtual void ResetCamera()
	{
		CameraLocalMove(-m_MoveDir.x, -m_MoveDir.y, -m_MoveDir.z);
		fieldOfView			= m_ZoomMax;
		
		SetCameraInspector();
	}
	
	protected virtual void SetMoveLimit()	{}
	
	public virtual void CameraLocalMove(float MoveX, float MoveY, float MoveZ)
	{
		m_LocalPos.x	+= MoveX;
		m_LocalPos.y	+= MoveY;
		m_LocalPos.z	+= MoveZ;
		
		m_MoveDir.x		+= MoveX;
		m_MoveDir.y		+= MoveY;
		m_MoveDir.z		+= MoveZ;
		
		transform.localPosition	= m_LocalPos;
		CheckMoveLimit();
	}
	
	private void CheckMoveLimit()
	{
		bool IsOver	= false;
		float OverX	= 0;
		float OverY	= 0;
		float OverZ	= 0;
		
		if(m_MoveDir.x > m_MoveLimit.x)
		{
			OverX	= m_MoveLimit.x - m_MoveDir.x;
			IsOver	= true;
		}
		else if(m_MoveDir.x < -(m_MoveLimit.x))
		{
			OverX	= -(m_MoveLimit.x + m_MoveDir.x);
			IsOver	= true;
		}
		if(m_MoveDir.y > m_MoveLimit.y)
		{
			OverY	= m_MoveLimit.y - m_MoveDir.y;
			IsOver	= true;
		}
		else if(m_MoveDir.y < -(m_MoveLimit.y))
		{
			OverY	= -(m_MoveLimit.y + m_MoveDir.y);
			IsOver	= true;
		}
		if(m_MoveDir.z > m_MoveLimit.z)
		{
			OverZ	= m_MoveLimit.z - m_MoveDir.z;
			IsOver	= true;
		}
		else if(m_MoveDir.z < -(m_MoveLimit.z))
		{
			OverZ	= -(m_MoveLimit.z + m_MoveDir.z);
			IsOver	= true;
		}
		
		if(IsOver)
		{
			CameraLocalMove(OverX, OverY, OverZ);
		}
	}
	
#region PIDMove
	public bool IsPIDMove	{ get { return m_bIsPIDMove; } }
	
	public virtual void SetPIDCameraMove(Vector3 _GoalPos, float _Kp, float _Ki)
	{
		m_bIsPIDMove		= true;
		m_vecPIDMove_Goal	= _GoalPos;
		m_fPIDMove_Kp		= _Kp;
		m_fPIDMove_Ki		= _Ki;
		m_fPIDMove_ErrorAccX	= 0.0f;
		m_fPIDMove_ErrorAccY	= 0.0f;
		m_fPIDMove_ErrorAccZ	= 0.0f;
	}
	
	private void PIDCameraMove()
	{
		float ErrorX	= m_vecPIDMove_Goal.x - m_LocalPos.x;
		float ErrorY	= m_vecPIDMove_Goal.y - m_LocalPos.y;
		float ErrorZ	= m_vecPIDMove_Goal.z - m_LocalPos.z;
		
		m_fPIDMove_ErrorAccX	+= ErrorX;
		m_fPIDMove_ErrorAccY	+= ErrorY;
		m_fPIDMove_ErrorAccZ	+= ErrorZ;
		
		m_LocalPos.x	= m_LocalPos.x + ErrorX*m_fPIDMove_Kp + m_fPIDMove_ErrorAccX * m_fPIDMove_Ki;
		m_LocalPos.y	= m_LocalPos.y + ErrorY*m_fPIDMove_Kp + m_fPIDMove_ErrorAccY * m_fPIDMove_Ki;
		m_LocalPos.z	= m_LocalPos.z + ErrorZ*m_fPIDMove_Kp + m_fPIDMove_ErrorAccZ * m_fPIDMove_Ki;
		
		transform.localPosition	= m_LocalPos * Time.smoothDeltaTime * 10;
		CheckMoveLimit();
	}
#endregion
}