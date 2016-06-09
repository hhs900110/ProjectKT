using UnityEngine;
using System.Collections;
using DefineBase;

public class EzGui_Slider : EzGui_Base
{
	enum SLIDER_ORIENTATION
	{
		_VERTICAL	= 0,
		_HORIZONTAL,
	};
	private UISlider EZGUI_Slider = null;
//	private int SliderOrientation = 0;
	
	////////// ========== Object Base ========== //////////
	
	public override void SetValid (bool IsValid)
	{
		base.SetValid (IsValid);
		if(this.gameObject)
		{
			this.gameObject.active = IsValid;
		}
		if (EZGUI_Slider)
		{
			EZGUI_Slider.enabled = IsValid;
		}
	}
	
	public override void Release()
	{
		base.Release();
		if (EZGUI_Slider)
		{
			EZGUI_Slider.Delete();
			DestroyImmediate(EZGUI_Slider);
		}
	}
	
	////////// ========== EzGui Base ========== //////////
	
	public override void SetTextureSize (float SizeX, float SizeY)
	{
		base.SetTextureSize (SizeX, SizeY);
		EZGUI_Slider.width	= SizeX;
		EZGUI_Slider.height	= SizeY;
	}
	
	//////////////////////////////////////////////////
	
	public void SetEZGUI(UISlider EZGUI)
	{
		EZGUI_Slider			= EZGUI;
		EZGUI_Slider.enabled	= false;
		SetValid(false);
	}
	
	public UISlider GetUISlider()	{ return EZGUI_Slider; }
	
	public void SetValueChangedDelegate(EZValueChangedDelegate del)	{ EZGUI_Slider.SetValueChangedDelegate(del); }
	public void SetcontrolIsEnabled(bool IsValid)					{ EZGUI_Slider.controlIsEnabled	= IsValid; }
	public bool GetcontrolIsEnabled()								{ return EZGUI_Slider.controlIsEnabled; }
	
	public void SetAnchor(UISlider.ANCHOR_METHOD ObjectAnchor)		{ EZGUI_Slider.SetAnchor(ObjectAnchor); }
	public void SetAnchor(int ObjectAnchor)							{ EZGUI_Slider.SetAnchor((UISlider.ANCHOR_METHOD)ObjectAnchor); }
	
	public void SetAnchor(string ObjectAnchor)
	{
			 if(ObjectAnchor == "Upper_Center")
		{
			EZGUI_Slider.SetAnchor(UISlider.ANCHOR_METHOD.UPPER_CENTER);
		}
		else if(ObjectAnchor == "Upper_Left")
		{
			EZGUI_Slider.SetAnchor(UISlider.ANCHOR_METHOD.UPPER_LEFT);
		}
		else if(ObjectAnchor == "Upper_Right")
		{
			EZGUI_Slider.SetAnchor(UISlider.ANCHOR_METHOD.UPPER_RIGHT);
		}
		else if(ObjectAnchor == "Middle_Center")
		{
			EZGUI_Slider.SetAnchor(UISlider.ANCHOR_METHOD.MIDDLE_CENTER);
		}
		else if(ObjectAnchor == "Middle_Left")
		{
			EZGUI_Slider.SetAnchor(UISlider.ANCHOR_METHOD.MIDDLE_LEFT);
		}
		else if(ObjectAnchor == "Middle_Right")
		{
			EZGUI_Slider.SetAnchor(UISlider.ANCHOR_METHOD.MIDDLE_RIGHT);
		}
		else if(ObjectAnchor == "Lower_Center")
		{
			EZGUI_Slider.SetAnchor(UISlider.ANCHOR_METHOD.BOTTOM_CENTER);
		}
		else if(ObjectAnchor == "Lower_Left")
		{
			EZGUI_Slider.SetAnchor(UISlider.ANCHOR_METHOD.BOTTOM_LEFT);
		}
		else if(ObjectAnchor == "Lower_Right")
		{
			EZGUI_Slider.SetAnchor(UISlider.ANCHOR_METHOD.BOTTOM_RIGHT);
		}
	}
	
	public void SetDirection(string ObjectOrientation)
	{
		if(ObjectOrientation == "VERTICAL")
		{
//			SliderOrientation = (int)SLIDER_ORIENTATION._VERTICAL;
			EZGUI_Slider.transform.rotation = Quaternion.Euler(0,0,270);
		}
		else if(ObjectOrientation == "HORIZONTAL")
		{
//			SliderOrientation = (int)SLIDER_ORIENTATION._HORIZONTAL;
			EZGUI_Slider.transform.rotation = Quaternion.Euler(0,0,0);
		}
		else
		{
			//Debug.Log("Uncorrect Orientation");
		}
	}
	
	public void SetValue(float ObjectValue)
	{
		EZGUI_Slider.Value	= ObjectValue;
	}
	
	public void SetText(string ObjectText)
	{
		EZGUI_Slider.Text	= ObjectText;
	}
}