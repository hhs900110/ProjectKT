using UnityEngine;
using System.Collections;
using DefineBase;

#pragma warning disable 0168 // variable declared but not used.
#pragma warning disable 0219 // variable assigned but not used.
#pragma warning disable 0414 // private field assigned but not used.

public abstract class InputManagerBase : ObjectBase
{
	protected bool  IsKeyFirstUp			= false;
	protected float DoubleClickTimeout		= 0.2f;
	protected float DoubleClickStart		= 0.0f;
	protected float			ZoomSpeed;
	
	protected TWO_FINGER_STATE TwoFingerState;
	
	public override void Create()
	{
		TwoFingerState = TWO_FINGER_STATE._NULL;
	}
	
	public virtual void SetTwoFingerState(TWO_FINGER_STATE State)
	{
		TwoFingerState = State;
	}
	
	public virtual TWO_FINGER_STATE GetTwoFingerState()
	{
		return TwoFingerState;
	}
	
	public virtual void ReSetTwoFingerState()				{}
	public virtual void SetTwoFingerStateStarRotate()		{}
	public virtual void SetTwoFingerStepState()				{}
	public virtual void OnApplicationFocus(bool IsFocus)	{}
	public virtual bool GetTextureHit()						{ return false; }
	public virtual Vector3 GetPos()							{ return Vector3.zero; }
	public virtual float GetPosY_Height_Down_Add()			{ return 0.0f; }
	public virtual bool CheckRect(float ObjectLeft, float ObjectTop, float ObjectRight, float ObjectBottom)
	{
		return false;
	}

	public virtual bool GetMouseButton(int state)			{ return false; }
	public virtual bool GetMouseButtonDown(int state)		{ return false; }
	public virtual bool GetCheckMouseButtonUp(int state)	{ return false; }
	public virtual bool GetMouseButtonUp(int state)			{ return false; }
	
	public virtual bool IsDoubleClick()		{ return false; }
	public virtual bool IsTouch()			{ return false; }
	public virtual bool IsDoubleTouch()		{ return false; }
	
	public virtual float GetAxisX()			{ return -1.0f; }
	public virtual float GetAxisY()			{ return -1.0f; }
	
	public virtual float GetRotateAxisX()	{ return -1.0f; }
	public virtual float GetRotateAxisY()	{ return -1.0f; }
	
	public virtual void ResetAxis()			{}
	
	public virtual bool GetMouseWheelUp()	{ return false; }
	public virtual bool GetMouseWheelDown()	{ return false; }
	
	public virtual float PinchZoom()		{ return -1.0f; }
	
	public virtual void UpdateZoomInOut()	{}
	
	public virtual int GetTouchCount()		{ return 0; }
	public virtual int GetZoomInOut()		{ return -1; }
	
	public virtual bool GetTouchMoving()	{ return false; }
	
	public virtual void SmothZoomInOut()	{}
	public virtual void SmothZoomInOut(GameObject UI3DCamera)		{}
	public virtual void SmothZoomInOut(CameraManagerBase _Camera, float _ZoomSpeed)	{}
	public virtual void SmothZoomInOut(CameraManagerBase _Camera, int _ZoomState, float _ZoomSpeed)	{}
	
	public virtual bool TowFingerTouchMove()	{ return false; }
	public virtual bool TouchMove()				{ return false; }
	public virtual bool GetTouch()				{ return false; }
	public virtual void SetZoomInOut(int State)	{}
}
