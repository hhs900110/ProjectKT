using UnityEngine;
using System.Collections;
using DefineBase;

#pragma warning disable 0168 // variable declared but not used.
#pragma warning disable 0219 // variable assigned but not used.
#pragma warning disable 0414 // private field assigned but not used.

public class InputManager : InputManagerBase
{
	bool bIsFocus = false;

	public override void Create()
	{
		base.Create();
		
		ZoomSpeed	= 10.0f;
	}
	
	
	public override void OnApplicationFocus(bool IsFocus)
	{
		bIsFocus = IsFocus;
	}

	public override Vector3 GetPos()
	{
		return Input.mousePosition;
	}
	
	public override float GetPosY_Height_Down_Add()
	{
		return Screen.height - Input.mousePosition.y;
	}
	
	public override bool CheckRect(float ObjectLeft, float ObjectTop, float ObjectRight, float ObjectBottom)
	{
		if (Input.mousePosition.x > ObjectLeft && Input.mousePosition.x < ObjectRight && Input.mousePosition.y < ObjectTop && Input.mousePosition.y > ObjectBottom)
		{
			return true;
		}
		
		return false;
	}
	public override bool GetMouseButton(int state)
	{
		if(Input.GetMouseButton(state))
		{
			return true;
		}
		return false;
	}
	
	public override bool GetMouseButtonDown(int state)
	{
		if(Input.GetMouseButtonDown(state))
		{
			return true;
		}
		return false;
	}
		
	public override bool IsDoubleClick()
	{
		if (!IsKeyFirstUp)
		{
			if (Input.GetMouseButtonUp( (int)MOUSE_DOWN_STATE._LEFT))
			{
				IsKeyFirstUp	 = true;
				DoubleClickStart = Time.realtimeSinceStartup;
			}
		}
		else
		{
			if (Input.GetMouseButtonDown( (int)MOUSE_DOWN_STATE._LEFT))
			{
				
				if ((float)(Time.realtimeSinceStartup - DoubleClickStart) < DoubleClickTimeout)
				{
					DoubleClickStart = 0.0f;
					IsKeyFirstUp	 = false;
					return true;
				}
				else
				{
					DoubleClickStart = 0.0f;
					IsKeyFirstUp 	 = false;
					return false;
				}
			}
			else if ((float)(Time.realtimeSinceStartup - DoubleClickStart) > DoubleClickTimeout)
			{
				DoubleClickStart = 0.0f;
				IsKeyFirstUp 	 = false;
				return false;
			}
		}
		return false;
	}
	
	public override bool GetCheckMouseButtonUp(int state)
	{
		if (Input.GetMouseButtonUp(state))
		{
			return true;
		}
		return false;
	}

	public override bool GetMouseButtonUp(int state)
	{
		if (Input.GetMouseButtonUp(state))
		{
			return true;
		}
		return false;
	}

	public override float GetAxisX()	{ return Input.GetAxis("Mouse X"); }
	public override float GetAxisY()	{ return Input.GetAxis("Mouse Y"); }
	
	public override void ResetAxis()
	{
		base.ResetAxis();
	}
	
	public override bool GetMouseWheelUp()
	{
		if (Input.GetAxis("Mouse ScrollWheel") > 0)
		{
			return true;
		}
		
		return false;
	}
	
	public override bool GetMouseWheelDown()
	{
		if (Input.GetAxis("Mouse ScrollWheel") < 0)
		{
			return true;
		}
		
		return false;
	}
	
	public override float PinchZoom()
	{
		return -1.0f;
	}
	
	public override void UpdateZoomInOut()
	{
		base.UpdateZoomInOut();
	}
	
	public override void SetZoomInOut(int State)
	{
	}
	
	public override void SmothZoomInOut ()
	{
	}
	
	public override void SmothZoomInOut (GameObject UI3DCamera) 
	{
	}
	
	public override void SmothZoomInOut(CameraManagerBase _Camera, int _ZoomState, float _ZoomSpeed)
	{
		if(_ZoomState == (int)ZOOM_STATE._OUT)
		{
			_Camera.fieldOfView = Mathf.Clamp(_Camera.fieldOfView + (_ZoomSpeed * Time.deltaTime),  _Camera.ZoomMin(), _Camera.ZoomMax());
		}
		else if(_ZoomState == (int)ZOOM_STATE._IN)
		{
			_Camera.fieldOfView = Mathf.Clamp(_Camera.fieldOfView - (_ZoomSpeed * Time.deltaTime), _Camera.ZoomMin(), _Camera.ZoomMax());
		}
	}
}