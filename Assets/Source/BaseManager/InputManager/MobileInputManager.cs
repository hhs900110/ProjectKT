using UnityEngine;
using System.Collections;
using DefineBase;

#pragma warning disable 0168 // variable declared but not used.
#pragma warning disable 0219 // variable assigned but not used.
#pragma warning disable 0414 // private field assigned but not used.

public class MobileInputManager : InputManagerBase
{
	enum TOUCH_ZOOM_IN_OUT_STATE
	{
		_NULL = 0,
		_BEGIN,
		_MOVE,
		_ZOOM_IN,
		_ZOOM_OUT,
		_END,
	}
	
	enum TWO_FINGER_STEP
	{
		_NULL = 0,
		_FIRST_TOUCH,
		_CHOICE,
		_CHOICE_WAIT,
		_SMOTH_ZOOM_IN_OUT,
		_MOVE,
		_STAR_ROTATE,
	}
	
	private Vector3			m_TouchPos;
	private Vector2			Zoom_Touch1_OriginPos;
	private Vector2			Zoom_Touch2_OriginPos;
	private Vector2			Touch1_OriginPos;
	private Vector2			Touch2_OriginPos;
	private Vector2			TwoTouch1_OriginPos;
	private Vector2			TwoTouch2_OriginPos;
	private float			TouchMovX;
	private float			TouchMovY;
	private float			RotateTouchMovX;
	private float			RotateTouchMovY;
	private bool			TowFingerTouch;
	private bool			bIsTowFingerTouchMove;
	private int				TouchZoomInOutState;
	private int				ZoomState;
	private bool			bIsTouch;
	private float			minPinchSpeed;
	private float			varianceInDistances;
	private float			touchDelta;
	private Vector2			prevDist;
	private Vector2			curDist;
	private Vector2			Touch_prevDist;
	private Vector2			Touch_curDist;
	private float			speedTouch0;
	private float			speedTouch1;
	private TWO_FINGER_STEP	TwoFingerStepState;
	
	
	public override void Create()
	{
		TouchMovX				= 0.0f;
		TouchMovY				= 0.0f;
		RotateTouchMovX			= 0.0f;
		RotateTouchMovY			= 0.0f;
		touchDelta				= 0.0f;
		speedTouch0				= 0.0f;
		speedTouch1				= 0.0f;
		minPinchSpeed			= 5.0f;
		varianceInDistances		= 5.0f;
		bIsTouch				= false;
		TowFingerTouch			= false;
		bIsTowFingerTouchMove	= false;
		
		m_TouchPos				= Vector3.zero;
		Touch1_OriginPos		= Vector2.zero;
		Touch2_OriginPos		= Vector2.zero;
		TwoTouch1_OriginPos		= Vector2.zero;
		TwoTouch2_OriginPos		= Vector2.zero;
		prevDist				= Vector2.zero;
		curDist					= Vector2.zero;
		Touch_prevDist			= Vector2.zero;
		Touch_curDist			= Vector2.zero;
		
		ZoomState				= (int)ZOOM_STATE._NULL;
		TouchZoomInOutState		= (int)TOUCH_ZOOM_IN_OUT_STATE._NULL;
		TwoFingerStepState		= TWO_FINGER_STEP._NULL;
	}
	
	public override Vector3 GetPos()
	{
		m_TouchPos.x	= 0.0f;
		m_TouchPos.y	= 0.0f;
		m_TouchPos.z	= 0.0f;
		if(Input.touchCount >= 1)
		{
			Touch touch = Input.GetTouch(0);
			m_TouchPos.x	= touch.position.x;
			m_TouchPos.y	= touch.position.y;
		}
		return m_TouchPos;
	}
	
	public override float GetPosY_Height_Down_Add()
	{
		Touch touch = Input.GetTouch(0);
		return Screen.height - touch.position.y;
	}
	
	public override bool CheckRect(float ObjectLeft, float ObjectTop, float ObjectRight, float ObjectBottom)
	{
		if(Input.touchCount == 1)
		{
			Touch touch = Input.GetTouch(0);
			
			if(touch.position.x > ObjectLeft && touch.position.x < ObjectRight && touch.position.y < ObjectTop && touch.position.y > ObjectBottom)
			{
				return true;
			}
		}
		return false;
	}
	
	public override bool GetMouseButton(int state)
	{
		if(Input.touchCount == 1)
		{
			if(Input.GetTouch(0).phase == TouchPhase.Began)
			{
				ResetAxis();
				Touch1_OriginPos = Input.GetTouch(0).position;
			}
			else if(Input.GetTouch(0).phase == TouchPhase.Ended)
			{
				ResetAxis();
			}
			if(Input.GetTouch(0).phase == TouchPhase.Moved)
			{
				float widthInInches = (Screen.width / Screen.dpi);
				if(widthInInches >= 4.0f && widthInInches <= 8.0f)
				{
				}
				else
				{
					widthInInches	= 2.0f;
				}
				TouchMovX	= (Input.GetTouch(0).deltaPosition.x / widthInInches);
				TouchMovY	= (Input.GetTouch(0).deltaPosition.y / widthInInches);
			}
			return true;
		}
		return false;
	}
	
	public override bool GetMouseButtonDown(int state)
	{
		if(Input.touchCount == 1)
		{
			if(Input.GetTouch(0).phase == TouchPhase.Began)
			{
				return true;
			}
		}
		return false;
	}
	
	public override bool GetCheckMouseButtonUp(int state)
	{
		return false;
	}
	
	public override bool GetMouseButtonUp(int state)
	{
		if(Input.touchCount == 1)
		{
			if(Input.GetTouch(0).phase == TouchPhase.Ended)
			{
				ResetAxis();
				return true;
			}
		}
		return false;
	}
	
	public override bool IsTouch()
	{
		if(Input.touchCount == 1)
		{
			if(Input.GetTouch(0).phase == TouchPhase.Began)
			{
				bIsTouch = true;
				Touch_curDist	= Input.GetTouch(0).position;
			}
			else if(Input.GetTouch(0).phase == TouchPhase.Moved)
			{
				Touch_prevDist	= Input.GetTouch(0).position;
				touchDelta		= Touch_curDist.magnitude - Touch_prevDist.magnitude;
				
				if(Mathf.Abs(touchDelta) >= 10.0f)
				{
					bIsTouch = false;
					return false;
				}
			}
			else if(Input.GetTouch(0).phase == TouchPhase.Ended && bIsTouch)
			{
				bIsTouch = false;
				return true;
			}
		}
		else
		{
			bIsTouch = false;
		}
		return false;
	}
	
	public override bool IsDoubleTouch()
	{
		if(!IsKeyFirstUp)
		{
			if(IsTouch())
			{
				IsKeyFirstUp	 = true;
				DoubleClickStart = Time.realtimeSinceStartup;
			}
		}
		else
		{
			if(IsTouch())
			{
				if((float)(Time.realtimeSinceStartup - DoubleClickStart) < DoubleClickTimeout)
				{
					DoubleClickStart = 0.0f;
					IsKeyFirstUp	 = false;
					return true;
				}
				else
				{
					DoubleClickStart = 0.0f;
					IsKeyFirstUp  = false;
					return false;
				}
			}
			else if((float)(Time.realtimeSinceStartup - DoubleClickStart) > DoubleClickTimeout)
			{
				DoubleClickStart = 0.0f;
				IsKeyFirstUp  = false;
				return false;
			}
		}
		return false;
	}
	
	
	public override bool GetTouchMoving()
	{
		if(Input.touchCount == 1)
		{
			if(Input.GetTouch(0).phase == TouchPhase.Moved)
			{
				return true;
			}
		}
		return false;
	}
	
	public override bool GetMouseWheelUp()		{ return true; }
	public override bool GetMouseWheelDown()	{ return true; }
	
	public override float GetAxisX()		{ return TouchMovX; }
	public override float GetAxisY()		{ return TouchMovY; }
	public override float GetRotateAxisX()	{ return RotateTouchMovX; }
	public override float GetRotateAxisY()	{ return RotateTouchMovY; }
	
	public override void ResetAxis()
	{
		Touch1_OriginPos.x	= 0.0f;
		Touch1_OriginPos.y	= 0.0f;
		
		TouchMovX		= 0.0f;
		TouchMovY		= 0.0f;
		RotateTouchMovX	= 0.0f;
		RotateTouchMovY	= 0.0f;
	}
	
	public override float PinchZoom()
	{
		Touch touch		= Input.GetTouch(0);
		Touch touch2	= Input.GetTouch(1);
		
		Vector2	PinchcurDist	= touch.position - touch2.position;
		Vector2	PinchprevDist	= (touch.position - touch.deltaPosition) - (touch2.position - touch2.deltaPosition);
		float	delta			= PinchcurDist.magnitude - PinchprevDist.magnitude;
		
		return	delta;
	}
	
	public override void UpdateZoomInOut()
	{
		if(Input.touchCount == 2)
		{
			if(TouchZoomInOutState == (int)TOUCH_ZOOM_IN_OUT_STATE._NULL)
			{
				TouchZoomInOutState = (int)TOUCH_ZOOM_IN_OUT_STATE._BEGIN;
			}
			else if(TouchZoomInOutState == (int)TOUCH_ZOOM_IN_OUT_STATE._BEGIN)
			{
				Zoom_Touch1_OriginPos	= Input.GetTouch(0).position;
				Zoom_Touch2_OriginPos	= Input.GetTouch(1).position;
				TouchZoomInOutState		= (int)TOUCH_ZOOM_IN_OUT_STATE._MOVE;
			}
			else if(TouchZoomInOutState  == (int)TOUCH_ZOOM_IN_OUT_STATE._MOVE)
			{
				Touch t1	= Input.GetTouch(0);
				Touch t2	= Input.GetTouch(1);
				
				float ang	= Vector2.Angle(Zoom_Touch1_OriginPos - t1.position, Touch2_OriginPos - t2.position);
				float dis	= Vector2.Distance(Zoom_Touch1_OriginPos, Zoom_Touch2_OriginPos) - Vector2.Distance(t1.position, t2.position);
				
				if(Mathf.Abs(dis) > 90)
				{
					if(dis > 0)
					{
						TouchZoomInOutState		= (int)TOUCH_ZOOM_IN_OUT_STATE._ZOOM_OUT;
						
						Zoom_Touch1_OriginPos	= t1.position;
						Zoom_Touch2_OriginPos	= t2.position;
					}
					else if(dis < 0)
					{
						TouchZoomInOutState		= (int)TOUCH_ZOOM_IN_OUT_STATE._ZOOM_IN;
						
						Zoom_Touch1_OriginPos	= t1.position;
						Zoom_Touch2_OriginPos	= t2.position;
					}
				}
			}
			else if(TouchZoomInOutState  == (int)TOUCH_ZOOM_IN_OUT_STATE._ZOOM_IN)
			{
				TouchZoomInOutState	= (int)TOUCH_ZOOM_IN_OUT_STATE._END;
				ZoomState			= (int)ZOOM_STATE._IN;
			}
			else if(TouchZoomInOutState  == (int)TOUCH_ZOOM_IN_OUT_STATE._ZOOM_OUT)
			{
				TouchZoomInOutState	= (int)TOUCH_ZOOM_IN_OUT_STATE._END;
				ZoomState			= (int)ZOOM_STATE._OUT;
			}
			else if(TouchZoomInOutState  == (int)TOUCH_ZOOM_IN_OUT_STATE._END)
			{
				ZoomState			= (int)ZOOM_STATE._NULL;
				TouchZoomInOutState	= (int)TOUCH_ZOOM_IN_OUT_STATE._NULL;
			}
		}
		else
		{
			TouchZoomInOutState		= (int)TOUCH_ZOOM_IN_OUT_STATE._NULL;
			ZoomState				= (int)ZOOM_STATE._NULL;
			Zoom_Touch1_OriginPos	= Vector2.zero;
			Zoom_Touch2_OriginPos	= Vector2.zero;
		}
	}
	
	public override void SetZoomInOut(int State)
	{
		ZoomState = State;
	}
	
	public override int GetZoomInOut()	{ return ZoomState;}
	public override int GetTouchCount()	{ return Input.touchCount;}
	
#if UNITY_IPHONE
	public override void SmothZoomInOut()
	{
		if(Input.touchCount == 2 && Input.GetTouch(0).phase == TouchPhase.Moved && Input.GetTouch(1).phase == TouchPhase.Moved)
		{
			
			Touch touch = Input.GetTouch(0);
			Touch touch2 = Input.GetTouch(1);
			
			curDist		= touch.position - touch2.position;
			prevDist	= ((touch.position - touch.deltaPosition) - (touch2.position - touch2.deltaPosition));
			touchDelta	= curDist.magnitude - prevDist.magnitude;
			
			speedTouch0	= touch.deltaPosition.magnitude / touch.deltaTime;
			speedTouch1	= touch2.deltaPosition.magnitude / touch2.deltaTime;
			
			if((touchDelta + varianceInDistances <= 1) && (speedTouch0 > minPinchSpeed) && (speedTouch1 > minPinchSpeed))
			{
				if(ZoomState == (int)ZOOM_STATE._NULL)
				{
					Camera.main.fieldOfView = Mathf.Clamp(Camera.main.fieldOfView + (ZoomSpeed * Time.deltaTime),  DefineBaseManager.inst.GetCameraMainZoomMin(), DefineBaseManager.inst.GetCameraMainZoomMax());
				}
				
				if(Camera.main.fieldOfView >= DefineBaseManager.inst.GetCameraMainZoomMax())
				{
					ZoomState = (int)ZOOM_STATE._OUT;
				}
				else
				{
					ZoomState = (int)ZOOM_STATE._NULL;
				}
			}
			
			if((touchDelta +varianceInDistances > 1) && (speedTouch0 > minPinchSpeed) && (speedTouch1 > minPinchSpeed))
			{
				if(ZoomState == (int)ZOOM_STATE._NULL)
				{
					Camera.main.fieldOfView = Mathf.Clamp(Camera.main.fieldOfView - (ZoomSpeed * Time.deltaTime), DefineBaseManager.inst.GetCameraMainZoomMin(), DefineBaseManager.inst.GetCameraMainZoomMax());
				}
				
				if(Camera.main.fieldOfView <= DefineBaseManager.inst.GetCameraMainZoomMin())
				{
					ZoomState = (int)ZOOM_STATE._IN;
				}
				else
				{
					ZoomState = (int)ZOOM_STATE._NULL;
				}
			}
		}
	}
	
	public override bool TowFingerTouchMove()
	{
		if(Input.touchCount == 1)
		{
			if(Input.GetTouch(0).phase == TouchPhase.Began)
			{
				ResetAxis();
				Touch1_OriginPos = Input.GetTouch(0).position;
			}
			else if(Input.GetTouch(0).phase == TouchPhase.Ended)
			{
				ResetAxis();
			}
			return false;
		}
		else if(Input.touchCount == 2)
		{
			if(Input.GetTouch(0).phase == TouchPhase.Moved && Input.GetTouch(1).phase == TouchPhase.Moved)
			{
				TouchMovX		 = Input.GetTouch(0).deltaPosition.x * 1.5f * Time.deltaTime;
				TouchMovY		 = Input.GetTouch(0).deltaPosition.y * 1.5f * Time.deltaTime;
				RotateTouchMovX	+= Input.GetTouch(0).deltaPosition.x * 1.5f * Time.deltaTime;
				RotateTouchMovY	+= Input.GetTouch(0).deltaPosition.x * 1.5f * Time.deltaTime;
				
				if((Input.GetTouch(0).deltaPosition.x >= 0.0f && Input.GetTouch(1).deltaPosition.x >= 0.0f)
				|| (Input.GetTouch(0).deltaPosition.x <= 0.0f && Input.GetTouch(1).deltaPosition.x <= 0.0f))
				{
					return true;
				}
				else if((Input.GetTouch(0).deltaPosition.x >= 0.0f && Input.GetTouch(1).deltaPosition.x <= 0.0f)
					 || (Input.GetTouch(0).deltaPosition.x <= 0.0f && Input.GetTouch(1).deltaPosition.x >= 0.0f))
				{
					return false;
				}
				else if((Input.GetTouch(0).deltaPosition.y >= 0.0f && Input.GetTouch(1).deltaPosition.y >= 0.0f)
					 || (Input.GetTouch(0).deltaPosition.y <= 0.0f && Input.GetTouch(1).deltaPosition.y <= 0.0f))
				{
					return true;
				}
			}
		}
		return false;
	}
	
#elif UNITY_ANDROID
	
	public override void SetTwoFingerState(TWO_FINGER_STATE State)
	{
		base.SetTwoFingerState(State);
	}
	
	public override TWO_FINGER_STATE GetTwoFingerState()
	{
		return base.GetTwoFingerState();
	}
	
	public override void SetTwoFingerStepState()
	{
		if(Input.touchCount != 2)
		{
			SetTwoFingerState(TWO_FINGER_STATE._NULL);
			TwoFingerStepState = TWO_FINGER_STEP._NULL;
		}
		if(TwoFingerStepState == TWO_FINGER_STEP._NULL)
		{
			TwoFingerStepState	= TWO_FINGER_STEP._FIRST_TOUCH;
		}
		else if(TwoFingerStepState == TWO_FINGER_STEP._FIRST_TOUCH)
		{
			ResetAxis();
			TwoTouch1_OriginPos	= Input.GetTouch(0).position;
			TwoTouch2_OriginPos	= Input.GetTouch(1).position;
			
			TwoFingerStepState	= TWO_FINGER_STEP._CHOICE;
		}
		else if(TwoFingerStepState == TWO_FINGER_STEP._CHOICE_WAIT)
		{
			ResetAxis();
			SetTwoFingerState(TWO_FINGER_STATE._NULL);
			TwoFingerStepState	= TWO_FINGER_STEP._CHOICE;
		}
		else if(TwoFingerStepState == TWO_FINGER_STEP._CHOICE)
		{
			Vector2 Pos		= TwoTouch1_OriginPos - Input.GetTouch(0).position;
			Vector2 Pos2	= TwoTouch2_OriginPos - Input.GetTouch(1).position;
			
			if(Pos == Pos2)
			{
				ResetAxis();
				SetTwoFingerState(TWO_FINGER_STATE._NULL);
				TwoFingerStepState = TWO_FINGER_STEP._NULL;
				return;
			}
			float GapX	= Mathf.Abs(Pos.x);
			float GapY	= Mathf.Abs(Pos.y);
			float GapX2	= Mathf.Abs(Pos2.x);
			float GapY2	= Mathf.Abs(Pos2.y);
			
			if(GapX >= 15 || GapY >= 15 || GapX2 >= 15 || GapY >= 15)
			{
			}
			else
			{
				TwoFingerStepState = TWO_FINGER_STEP._CHOICE_WAIT;
				return;
			}
			
			if((Pos.x > 0.0f && Pos2.x > 0.0f) || (Pos.x < 0.0f && Pos2.x < 0.0f))
			{
				TwoFingerStepState	= TWO_FINGER_STEP._MOVE;
			}
			else
			{
				TwoFingerStepState	= TWO_FINGER_STEP._SMOTH_ZOOM_IN_OUT;
			}
		}
		else if(TwoFingerStepState == TWO_FINGER_STEP._SMOTH_ZOOM_IN_OUT)
		{
			SetTwoFingerState(TWO_FINGER_STATE._SMOTH_ZOOM_IN_OUT);
		}
		else if(TwoFingerStepState == TWO_FINGER_STEP._MOVE)
		{
			SetTwoFingerState(TWO_FINGER_STATE._MOVE);
		}
	}
	
	public override void SmothZoomInOut()
	{
		if(Input.touchCount == 2)
		{
			if(Input.GetTouch(0).phase == TouchPhase.Moved && Input.GetTouch(1).phase == TouchPhase.Moved)
			{
				Touch touch  = Input.GetTouch(0);
				Touch touch2 = Input.GetTouch(1);
				
				curDist  =  touch.position - touch2.position;
				prevDist = (touch.position - touch.deltaPosition) - (touch2.position - touch2.deltaPosition);
				
				float delta = curDist.magnitude - prevDist.magnitude;
				
				if (delta < 0)
				{
					if(ZoomState == (int)ZOOM_STATE._NULL)
					{
						//Camera.main.fieldOfView = Mathf.Clamp(Camera.main.fieldOfView + (ZoomSpeed * Time.deltaTime),  DefineBaseManager.inst.GetCameraMainZoomMin(), DefineBaseManager.inst.GetCameraMainZoomMax());
					}
					
//					if(Camera.main.fieldOfView >= DefineBaseManager.inst.GetCameraMainZoomMax())
//					{
//						ZoomState = (int)ZOOM_STATE._OUT;
//					}
//					else
//					{
//						ZoomState = (int)ZOOM_STATE._NULL;
//					}
				}
				else 
				{
					if(ZoomState == (int)ZOOM_STATE._NULL)
					{
						//Camera.main.fieldOfView = Mathf.Clamp(Camera.main.fieldOfView - (ZoomSpeed * Time.deltaTime), DefineBaseManager.inst.GetCameraMainZoomMin(), DefineBaseManager.inst.GetCameraMainZoomMax());
					}
					
//					if(Camera.main.fieldOfView <= DefineBaseManager.inst.GetCameraMainZoomMin())
//					{
//						ZoomState = (int)ZOOM_STATE._IN;
//					}
//					else
//					{
//						ZoomState = (int)ZOOM_STATE._NULL;
//					}
				}
			}
		}
	}
	
	public override bool TowFingerTouchMove()
	{
		TwoTouch1_OriginPos = Input.GetTouch(0).position;
		TwoTouch2_OriginPos = Input.GetTouch(1).position;
		
		if(Input.GetTouch(0).phase == TouchPhase.Moved || Input.GetTouch(1).phase == TouchPhase.Moved)
		{
			if((Input.GetTouch(0).deltaPosition.x * 1.5f * Time.deltaTime) >= (Input.GetTouch(1).deltaPosition.x * 1.5f * Time.deltaTime))
			{
				TouchMovX			= Input.GetTouch(0).deltaPosition.x * 1.5f * Time.deltaTime;
			}
			else
			{
				TouchMovX			= Input.GetTouch(1).deltaPosition.x * 1.5f * Time.deltaTime;
			}
			
			if((Input.GetTouch(0).deltaPosition.y * 1.5f * Time.deltaTime) >= (Input.GetTouch(1).deltaPosition.y * 1.5f * Time.deltaTime))
			{
				TouchMovY			= Input.GetTouch(0).deltaPosition.y * 1.5f * Time.deltaTime;
			}
			else
			{
				TouchMovY			= Input.GetTouch(1).deltaPosition.y * 1.5f * Time.deltaTime;
			}
		}
		else if(Input.GetTouch(1).phase == TouchPhase.Moved)
		{
			TouchMovX			= Input.GetTouch(1).deltaPosition.x * 1.5f * Time.deltaTime;
			TouchMovY			= Input.GetTouch(1).deltaPosition.y * 1.5f * Time.deltaTime;
		}
		else if(Input.GetTouch(0).phase == TouchPhase.Moved)
		{
			TouchMovX			= Input.GetTouch(0).deltaPosition.x * 1.5f * Time.deltaTime;
			TouchMovY			= Input.GetTouch(0).deltaPosition.y * 1.5f * Time.deltaTime;
		}
		RotateTouchMovX += Input.GetTouch(1).deltaPosition.x * 1.5f * Time.deltaTime;
		RotateTouchMovY += Input.GetTouch(1).deltaPosition.x * 1.5f * Time.deltaTime;
		
		return false;
	}
	
	public override bool TouchMove()
	{
		if(Input.touchCount == 2)
		{
			if(Input.GetTouch(0).phase == TouchPhase.Moved)
			{
				float widthInInches = (Screen.width / Screen.dpi);
				if(widthInInches >= 4.0f)
				{
					widthInInches = 4.0f;
				}
				
				TouchMovX = (Input.GetTouch(0).deltaPosition.x / widthInInches) / 2;
				TouchMovY = (Input.GetTouch(0).deltaPosition.y / widthInInches) / 2;
			}
			
			if(Input.GetTouch(1).phase == TouchPhase.Moved)
			{
				float widthInInches = (Screen.width / Screen.dpi);
				if(widthInInches >= 4.0f)
				{
					widthInInches = 4.0f;
				}
				if(TouchMovX <= (Input.GetTouch(1).deltaPosition.x / widthInInches) || TouchMovY <= (Input.GetTouch(1).deltaPosition.y / widthInInches))
				{
					TouchMovX = (Input.GetTouch(1).deltaPosition.x / widthInInches) / 2;
					TouchMovY = (Input.GetTouch(1).deltaPosition.y / widthInInches) / 2;
				}
			}
			
			return true;
		}
		SetTwoFingerState(TWO_FINGER_STATE._NULL);
		return false;
	}
#endif
	
	public override void ReSetTwoFingerState()
	{
		TwoFingerStepState = TWO_FINGER_STEP._NULL;
		SetTwoFingerState(TWO_FINGER_STATE._NULL);
	}
	
	public override void SetTwoFingerStateStarRotate()
	{
		TwoFingerStepState = TWO_FINGER_STEP._STAR_ROTATE;
		SetTwoFingerState(TWO_FINGER_STATE._STAR_ROTATE);
	}
	
	public override void SmothZoomInOut(CameraManagerBase _Camera, float _ZoomSpeed)
	{
		if(Input.touchCount == 2)
		{
			if(Input.GetTouch(0).phase == TouchPhase.Moved && Input.GetTouch(1).phase == TouchPhase.Moved)
			{
				Touch touch  = Input.GetTouch(0);
				Touch touch2 = Input.GetTouch(1);
				
				curDist  =  touch.position - touch2.position;
				prevDist = (touch.position - touch.deltaPosition) - (touch2.position - touch2.deltaPosition);
				
				float delta = curDist.magnitude - prevDist.magnitude;
				
				if(delta < 0)
				{
					if(ZoomState == (int)ZOOM_STATE._NULL)
					{
						_Camera.fieldOfView = Mathf.Clamp(_Camera.fieldOfView + (_ZoomSpeed * Time.deltaTime),  _Camera.ZoomMin(), _Camera.ZoomMax());
					}
					
					
					if(_Camera.fieldOfView >= _Camera.ZoomMax())
					{
						ZoomState = (int)ZOOM_STATE._OUT;
					}
					else
					{
						ZoomState = (int)ZOOM_STATE._NULL;
					}
				}
				else
				{
					if(ZoomState == (int)ZOOM_STATE._NULL)
					{
						_Camera.fieldOfView = Mathf.Clamp(_Camera.fieldOfView - (_ZoomSpeed * Time.deltaTime), _Camera.ZoomMin(), _Camera.ZoomMax());
					}
					
					if(_Camera.fieldOfView <= _Camera.ZoomMin())
					{
						ZoomState = (int)ZOOM_STATE._IN;
					}
					else
					{
						ZoomState = (int)ZOOM_STATE._NULL;
					}
				}
			}
		}
	}
	
	public override void SmothZoomInOut(CameraManagerBase _Camera, int _ZoomState, float _ZoomSpeed)
	{
		if(_ZoomState == (int)ZOOM_STATE._OUT)
		{
			if(ZoomState == (int)ZOOM_STATE._NULL)
			{
				_Camera.fieldOfView = Mathf.Clamp(_Camera.fieldOfView + (_ZoomSpeed * Time.deltaTime),  _Camera.ZoomMin(), _Camera.ZoomMax());
			}
			
			
			if(_Camera.fieldOfView >= _Camera.ZoomMax())
			{
				ZoomState = (int)ZOOM_STATE._OUT;
			}
			else
			{
				ZoomState = (int)ZOOM_STATE._NULL;
			}
		}
		else if(_ZoomState == (int)ZOOM_STATE._IN)
		{
			if(ZoomState == (int)ZOOM_STATE._NULL)
			{
				_Camera.fieldOfView = Mathf.Clamp(_Camera.fieldOfView - (_ZoomSpeed * Time.deltaTime), _Camera.ZoomMin(), _Camera.ZoomMax());
			}
			
			if(_Camera.fieldOfView <= _Camera.ZoomMin())
			{
				ZoomState = (int)ZOOM_STATE._IN;
			}
			else
			{
				ZoomState = (int)ZOOM_STATE._NULL;
			}
		}
	}
	
	public override void SmothZoomInOut(GameObject UI3DCamera)
	{
		if(Input.touchCount == 2)
		{
			if(Input.GetTouch(0).phase == TouchPhase.Moved && Input.GetTouch(1).phase == TouchPhase.Moved)
			{
				Touch touch  = Input.GetTouch(0);
				Touch touch2 = Input.GetTouch(1);
				
				curDist  =  touch.position - touch2.position;
				prevDist = (touch.position - touch.deltaPosition) - (touch2.position - touch2.deltaPosition);
				
				float delta = curDist.magnitude - prevDist.magnitude;
				
				if (delta < 0)
				{
					UI3DCamera.transform.position	-= UI3DCamera.transform.forward*(ZoomSpeed * Time.deltaTime);
				}
				else
				{
					UI3DCamera.transform.position	+= UI3DCamera.transform.forward*(ZoomSpeed * Time.deltaTime);
				}
			}
		}
	}
}
