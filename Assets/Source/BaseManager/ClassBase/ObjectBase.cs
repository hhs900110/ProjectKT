using UnityEngine;
using System.Collections;
using DefineBase;

public abstract class ObjectBase : MonoBehaviour, IMonoBase
{
	protected bool Valid;
	
	private	GameObject		ScriptObject;
	
	public abstract void Create();
	public virtual void SetValid(bool IsValid)
	{
		enabled	= IsValid;
		Valid	= IsValid;
	}
	
	public virtual bool GetValid()
	{
		return Valid;
	}
	
	public virtual void Release()
	{
		DestroyScriptObject();
	}
	
	public virtual void Message(int Msg, int Param1, int Param2)	{}
	
	protected void SendMessage(int Msg, int Param1)					{ Main.inst.Message(Msg, Param1); }
	protected void SendMessage(int Msg, int Param1, int Param2)		{ Main.inst.Message(Msg, Param1, Param2); }
	
#region ScriptObject
	protected void SetScriptObject()
	{
		SetScriptObject(GetType().ToString());
	}
	
	protected void SetScriptObject(string _ObjectName)
	{
		if(ScriptObject == null)
		{
			ScriptObject	= new GameObject("SCRIPT_" + _ObjectName);
			ScriptObject.transform.parent	= gameObject.transform;
		}
		else
		{
			ScriptObject.name	= "SCRIPT_" + _ObjectName;
		}
	}
	
	protected void DestroyScriptObject()
	{
		if(ScriptObject)
		{
			DestroyImmediate(ScriptObject);
			ScriptObject	= null;
		}
	}
	
	protected GameObject GetScriptObject()	{ return ScriptObject; }
#endregion
}