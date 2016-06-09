using UnityEngine;
using System.Collections;
using DefineBase;

#pragma warning disable 0168 // variable declared but not used.
#pragma warning disable 0219 // variable assigned but not used.
#pragma warning disable 0414 // private field assigned but not used.

public sealed partial class Main : MonoBehaviour, ILoadingStep, IMonoBase
{
#region IMonoBase
	public void Create()
	{
		Init();
	}
	
	public void SetValid(bool IsValid)	{}
	public bool GetValid()
	{
		return enabled;
	}
	
	public void Message(int Msg, int Param1)
	{
		Message(Msg, Param1, -1);
	}
	
	public void Message(int Msg, int Param1, int Param2)
	{
		if(StageManagerScript != null)	{ StageManagerScript.Message(Msg, Param1, Param2); }
		if(HudManagerScript != null)	{ HudManagerScript.Message(Msg, Param1, Param2); }
	}
#endregion
}