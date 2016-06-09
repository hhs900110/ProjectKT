using UnityEngine;
using System.Collections;
using DefineBase;

public abstract class StageBase : IClassBase
{
	protected static InputManagerBase InputManagerScript;
	
#region IClassBase
	private bool Enabled;
	private bool Valid;
	
	public virtual void Create()
	{
		if(InputManagerScript == null)	{ InputManagerScript	= Main.input; }
		
		enabled	= false;
		Valid	= false;
	}
	
	public virtual void SetValid(bool IsValid)
	{
		enabled	= IsValid;
		Valid	= IsValid;
	}
	
	public virtual bool GetValid()
	{
		return Valid;
	}
	public bool enabled
	{
		get { return Enabled; }
		set { Enabled	= value; }
	}
	
	public virtual void Release()	{}
	
	public virtual void Message(int Msg, int Param1, int Param2)	{}
	
	public bool IsUpdate()
	{
		if(!enabled)
		{ return false; }
		return true;
	}
	
	public virtual void Update()	{}
#endregion
	
	protected void SendMessage(int Msg, int Param1)					{ Main.inst.Message(Msg, Param1); }
	protected void SendMessage(int Msg, int Param1, int Param2)		{ Main.inst.Message(Msg, Param1, Param2); }
	
	
	protected bool IsMove()
	{
#if UNITY_EDITOR
		// Move
		if(InputManagerScript.GetMouseButton((int)MOUSE_DOWN_STATE._LEFT))
		{
			if(InputManagerScript.GetMouseButton((int)MOUSE_DOWN_STATE._RIGHT))
			{
				return true;
			}
		}
#elif UNITY_IPHONE || UNITY_ANDROID
		InputManagerScript.SetTwoFingerStepState();
		if(InputManagerScript.GetTwoFingerState() == TWO_FINGER_STATE._MOVE)
		{
			InputManagerScript.TouchMove();
			return true;
		}
#else
		// Move
		if(InputManagerScript.GetMouseButton((int)MOUSE_DOWN_STATE._LEFT))
		{
			if(InputManagerScript.GetMouseButton((int)MOUSE_DOWN_STATE._RIGHT))
			{
				return true;
			}
		}
#endif
		return false;
	}
	
	protected bool IsZoom()
	{
#if UNITY_EDITOR
			// Zoom
			if(InputManagerScript.GetMouseWheelDown())
			{
				return true;
			}
			else if(InputManagerScript.GetMouseWheelUp())
			{
				return true;
			}
#elif UNITY_IPHONE || UNITY_ANDROID
			if(InputManagerScript.GetTwoFingerState() == TWO_FINGER_STATE._SMOTH_ZOOM_IN_OUT)
			{
				return true;
			}
#else
			// Zoom
			if(InputManagerScript.GetMouseWheelDown())
			{
				return true;
			}
			else if(InputManagerScript.GetMouseWheelUp())
			{
				return true;
			}
#endif
		return false;
	}
}