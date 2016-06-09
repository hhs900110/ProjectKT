using UnityEngine;
using System.Collections;

#pragma warning disable 0168 // variable declared but not used.
#pragma warning disable 0219 // variable assigned but not used.
#pragma warning disable 0414 // private field assigned but not used.

namespace DefineBase
{
	public enum PLATFORM_TYPE
	{
		PLATFORM_EDITOR_WIN = 0,
		PLATFORM_EDITOR_OSX,
		PLATFORM_WEB,
		PLATFORM_IPHONE,
		PLATFORM_ANDROID,
	}
	
	public enum SCREEN_TYPE
	{
		_SCREEN_RECTANGLE	= 0,
		_SCREEN_TALL,
		_SCREEN_WIDE,
	}
	
	public enum CONTROL_STATE
	{
		NORMAL,
		OVER,
		ACTIVE,
		DISABLED
	}
	
	public enum TEXTURE_LAYER
	{
		_MAX = 100,
	}
	
	public enum ZOOM_STATE
	{
		_NULL = 0,
		_IN,
		_OUT,
	}
	
	public enum HUD_BASE_POS
	{
		_TOP_LEFT = 0,
		_TOP_RIGHT,
		_BOTTOM_CENTER,
		_MIDDLE_CENTER,
		_BOTTON_RIGHT,
		_BOTTOM_LEFT,
		_TOP_CENTER,
	}
	
	public enum ROTATE_TYPE
	{
		_RIGHT = 0,
		_LEFT,
	}
	
	public enum HUD_SLIDE_DRECTION
	{
		_NULL = 0,
		_MOVE_UP,
		_MOVE_DOWN,
		_MOVE_LEFT,
		_MOVE_RIGHT,
		_MOVE_END,
	}
	
	public enum FONT_WRITE_TYPE
	{
		_RIGHT = 0,
		_CENTER,
		_LEFT,
	}
	
	public enum GUI_MOUSE_STATE
	{
		_NULL = 0,
		_PRODUCT,
		_ITEM,
		_PAINTING,
		_REDESIGN,
	}
	
	public enum MOUSE_DOWN_STATE
	{
		_LEFT = 0,
		_RIGHT,
		_CENTER,
	}
	
	public enum MOUSE_BUTTON_TYPE
	{
		_BASE = 0,
		_CURSOR,
		_DOWN,
		_UP,
		_FALSE,
		_TAB,
		_TOUCH,
	}
	
	public enum TWO_FINGER_STATE
	{
		_NULL = 0,
		_SMOTH_ZOOM_IN_OUT,
		_MOVE,
		_STAR_ROTATE,
	}
	
	public enum KITTY_TURN_TYPE
	{
		_LEFT	= 0,
		_RIGHT,
		_REBOUND,
		_MAX,
	}
	
	public enum LEG_TYPE
	{
		_RIGHT_ANGLE	= 0,
		_DIAMOND,
		_STRAIGHT_LINE,
		_NULL,
	}

    public enum LEG_BASE_SET
    {
        _RIGHT_ANGLE,
        _STRAIGHT_LINE,
        _RIGHT_AND_DIAMOND,
        _RIGHT_AND_STRAIGHT,
        _STRAIGHT_AND_DIAMOND,
        _ALL_RANDOM,
    }
}