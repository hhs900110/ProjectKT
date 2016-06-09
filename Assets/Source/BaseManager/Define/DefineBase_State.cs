using UnityEngine;
using System.Collections;

#pragma warning disable 0168 // variable declared but not used.
#pragma warning disable 0219 // variable assigned but not used.
#pragma warning disable 0414 // private field assigned but not used.

namespace DefineBase
{
	public enum MSG_TYPE
	{
		_PLAY = 0,
	}
	
	public enum PLAY_STATE
	{
		_LOADING	= 0,
		_MAIN,
		_READY,
		_KITTYTURN,
		_MAX,
	}
	
	public enum LOAD_STEP
	{
		_NULL = 0,
		_STEP1,	_STEP1_LOADING,	_STEP1_END,
		_STEP2,	_STEP2_LOADING,	_STEP2_END,
		_STEP3,	_STEP3_LOADING,	_STEP3_END,
		_STEP4,	_STEP4_LOADING,	_STEP4_END,
		_STEP5,	_STEP5_LOADING,	_STEP5_END,
	}
	
	public enum POPUP_STATE
	{
		_MESSAGE = 0,
		_SHOP,
		_FRIEND,
		_OPTION,
		_NEWS,
		_MAX,
	}
}