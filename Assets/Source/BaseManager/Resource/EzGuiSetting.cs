using UnityEngine;
using System.Collections;
using DefineBase;

namespace DefineBase
{
	public enum DATUM_POINT
	{
		_FIRST,
		_CENTER,
		_LAST,
	}
	
	public enum DIRECTION
	{
		_UP_DOWN,
		_LEFT_RIGHT,
	}
	
	public enum DIRECTION_SUB
	{
		_UP_LEFT,
		_DOWN_RIGHT,
	}
}

public abstract class EzGuiSetting
{
#region EzGui
	public static void SetControllEnabledEzGui(EzGui_Button _Object, bool _IsEnabled)
	{
		if(_Object != null)
		{
			_Object.SetcontrolIsEnabled(_IsEnabled);
		}
	}
	public static void SetControllEnabledEzGui(EzGui_ProgressBar _Object, bool _IsEnabled)
	{
		if(_Object != null)
		{
			_Object.SetcontrolIsEnabled(_IsEnabled);
		}
	}
	public static void SetControllEnabledEzGui(EzGui_RadioButton _Object, bool _IsEnabled)
	{
		if(_Object != null)
		{
			_Object.SetcontrolIsEnabled(_IsEnabled);
		}
	}
	public static void SetControllEnabledEzGui(EzGui_ScrollList _Object, bool _IsEnabled)
	{
		if(_Object != null)
		{
			_Object.SetcontrolIsEnabled(_IsEnabled);
		}
	}
	public static void SetControllEnabledEzGui(EzGui_Slider _Object, bool _IsEnabled)
	{
		if(_Object != null)
		{
			_Object.SetcontrolIsEnabled(_IsEnabled);
		}
	}
	public static void SetControllEnabledEzGui(EzGui_TextField _Object, bool _IsEnabled)
	{
		if(_Object != null)
		{
			_Object.SetcontrolIsEnabled(_IsEnabled);
		}
	}
	public static void SetControllEnabledEzGui(EzGui_Texture _Object, bool _IsEnabled)
	{
		if(_Object != null)
		{
			_Object.SetcontrolIsEnabled(_IsEnabled);
		}
	}
	
	public static void SetValidEzGui(EzGui_Base _Object, bool _IsValid)
	{
		if(_Object != null)
		{
			_Object.SetValid(_IsValid);
		}
	}
	
	public static void SetValidEzGui(EzGui_Base[] _Object, bool _IsValid)
	{
		if(_Object != null)
		{
			for(int i = 0; i < _Object.Length; i++)
			{
				SetValidEzGui(_Object[i], _IsValid);
			}
		}
	}
	
	public static void SetValidEzGuiList(EzGui_Base[] _Object, bool _IsValid, int _SetIndex)
	{
		if(_Object != null)
		{
			for(int i = 0; i < _Object.Length; i++)
			{
				if(i == _SetIndex)
				{
					SetValidEzGui(_Object[i], _IsValid);
				}
				else
				{
					SetValidEzGui(_Object[i], false);
				}
			}
		}
	}
	
	public static void SetPosEzGui(EzGui_Base _Object, float _PosX, float _PosY)
	{
		if(_Object != null)
		{
			_Object.SetPos(_PosX, _PosY);
		}
	}
	
	public static void SetPosEzGui(EzGui_Base[] _Object, float _PosX, float _PosY, float _Gap, DATUM_POINT _DatumPoint, DIRECTION _Direction, DIRECTION_SUB _SubDirection)
	{
		if(_Object != null)
		{
			int count	= _Object.Length;
			for(int i = 0; i < count; i++)
			{
				float _ComputeX	= 0.0f;
				float _ComputeY	= 0.0f;
				
				switch(_DatumPoint)
				{
				case DATUM_POINT._FIRST:
					if(_Direction == DIRECTION._LEFT_RIGHT)
					{ _ComputeX	= i; }
					else
					{ _ComputeY	= i; }
					break;
					
				case DATUM_POINT._CENTER:
					if(_Direction == DIRECTION._LEFT_RIGHT)
					{ _ComputeX	= (1.0f - count + 2.0f*i) / 2.0f; }
					else
					{ _ComputeY	= (1.0f - count + 2.0f*i) / 2.0f; }
					break;
					
				case DATUM_POINT._LAST:
					if(_Direction == DIRECTION._LEFT_RIGHT)
					{ _ComputeX	= (count - i - 1.0f) * -1.0f; }
					else
					{ _ComputeY	= (count - i - 1.0f) * -1.0f; }
					break;
				}
				
				if(_SubDirection == DIRECTION_SUB._DOWN_RIGHT)
				{
					_ComputeX *= -1;
					_ComputeY *= -1;
				}
				
				_Object[i].SetPos(_PosX - _ComputeX*_Gap, _PosY - _ComputeY*_Gap);
			}
		}
	}
	
	public static void ReleaseEzGui(EzGui_Base _Object)
	{
		if(_Object != null)
		{
			_Object.Release();
			_Object	= null;
		}
	}
	
	public static void ReleaseEzGui(EzGui_Base[] _Object)
	{
		if(_Object != null)
		{
			for(int i = 0; i < _Object.Length; i++)
			{
				ReleaseEzGui(_Object[i]);
			}
			_Object	= null;
		}
	}
#endregion
}