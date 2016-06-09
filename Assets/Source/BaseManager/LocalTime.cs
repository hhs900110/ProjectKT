using UnityEngine;
using System.Collections;

#pragma warning disable 0168 // variable declared but not used.
#pragma warning disable 0219 // variable assigned but not used.
#pragma warning disable 0414 // private field assigned but not used.


public class LocalTime : MonoBehaviour
{
	private bool Valid;
	private int Time_Hour;
	private int Time_Minute;
	private int Time_Second;
	private float Time_Count;
	private float Time_Passed;

	private int TimeSpeed;
	private Main NowMain;	
	private int InventorySlotIndex;
	public void Create()
	{
		Time_Hour = 0;
		Time_Minute = 0;
		Time_Second = 0;
		Time_Count = 0.0f;
		Time_Passed = 0.0f;

		NowMain = Main.inst;
		//TimeSpeed   =   NowMain.GetBuildManager().GetBuildSpeed();
		TimeSpeed = 1;
		InventorySlotIndex	=	0;
		SetValid(false);
	}
	public void SetInventorySlotIndex(int Index)
	{
		InventorySlotIndex  = Index;
	}
	public int GetInventorySlotIndex()
	{
		return InventorySlotIndex;
	}
	public void SetName(string ObjectName)
	{
		this.name	=	ObjectName;
	}
	public void FixedUpdate()
	{
		Time_Count += Time.deltaTime * TimeSpeed;
		Time_Passed += Time.deltaTime * TimeSpeed;
		if (Time_Count > 1.0f)
		{
			Time_Count = 0.0f;
			//Debug.LogError("H : " + Time_Hour + "M : " + Time_Minute + " S" + Time_Second);
			Time_Second--;
			if (Time_Second < 0)
			{
				Time_Second = 59;
				Time_Minute--;
				if (Time_Minute < 0)
				{
					Time_Minute = 59;
					Time_Hour--;
					if (Time_Hour < 0)
					{
						Time_Hour = 0;
						Time_Minute = 0;
						Time_Second = 0;
						Time_Count = 0.0f;
						SetValid(false);
					}
				}
			}
		}
	}

	public void SubTime(int ObjectTime)
	{
		if (GetTime() <= ObjectTime)
		{
			Time_Passed += GetTime();
			Time_Hour = 0;
			Time_Minute = 0;
			Time_Second = 0;
			Time_Count = 0.0f;
			SetValid(false);
		}
		else
		{
			Time_Passed += ObjectTime;
			SetTime(GetTime() - ObjectTime);
		}
	}

	public void SetBeginTime(int ObjectTime)
	{
		int hourTime = ObjectTime / 3600;
		int MinTime = (ObjectTime - (hourTime * 3600)) / 60;
		int SecTime = ObjectTime - (hourTime * 3600) - (MinTime * 60);

		SetBeginTime(hourTime, MinTime, SecTime);
	}

	public void SetBeginTime(int ObjectHour, int ObjectMinute, int ObjectSecond)
	{
		Time_Hour = ObjectHour;
		Time_Minute = ObjectMinute;
		Time_Second = ObjectSecond;
		Time_Count = 0.0f;
		Time_Passed = 0.0f;
		SetValid(true);
	}

	public void SetTime(int ObjectTime)
	{
		int hourTime = ObjectTime / 3600;
		int MinTime = (ObjectTime - (hourTime * 3600)) / 60;
		int SecTime = ObjectTime - (hourTime * 3600) - (MinTime * 60);
		SetTime(hourTime, MinTime, SecTime);
	}

	public void SetTime(int ObjectHour, int ObjectMinute, int ObjectSecond)
	{
		Time_Hour	= ObjectHour;
		Time_Minute = ObjectMinute;
		Time_Second = ObjectSecond;
	}
	
	public void SetTime_Hour(int ObjectHour)
	{
		Time_Hour = ObjectHour;
	}
	public void SetTime_Minute(int ObjectMinete)
	{
		Time_Minute = ObjectMinete;
	}
	public void SetTime_Second(int ObjectSecond)
	{
		Time_Second = ObjectSecond;
	}

	public int GetTime_Hour()
	{
		return Time_Hour;
	}
	public int GetTime_Minute()
	{
		return Time_Minute;
	}
	public int GetTime_Second()
	{
		return Time_Second;
	}

	public string GetStringTime()
	{
		string StringHour = Time_Hour.ToString();
		if (Time_Hour < 10)
		{
			StringHour = "0" + Time_Hour.ToString();
		}
		string StringMin = Time_Minute.ToString();
		if (Time_Minute < 10)
		{
			StringMin = "0" + Time_Minute.ToString();
		}
		string StringSec = Time_Second.ToString();
		if (Time_Second < 10)
		{
			StringSec = "0" + Time_Second.ToString();
		}
		string StringTime = StringHour + ":" + StringMin + ":" + StringSec;
		return StringTime;
	}

	//add jh
	public int GetTime()
	{
		return (Time_Hour * 3600 + Time_Minute * 60 + Time_Second);
	}

	public void Reset_Time_Passed()
	{
		Time_Passed = 0.0f;
	}

	public void Set_Time_Passed(float ObjectTime)
	{
		Time_Passed = ObjectTime;
	}

	public float GetTime_Passed()
	{
		return Time_Passed;
	}

	//end jh

	public void SetTimeSpeed(int Speed)
	{
		TimeSpeed = Speed;
	}

	public void SetValid(bool IsValid)
	{
		enabled = IsValid;
		Valid = IsValid;
	}

	public bool GetValid()
	{
		return Valid;
	}

	public void Message(int Msg, int Param1, int Param2)
	{
	}

	public void Release()
	{
	}

}

