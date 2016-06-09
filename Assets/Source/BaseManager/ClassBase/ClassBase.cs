using UnityEngine;
using System.Collections;
using DefineBase;

public abstract class ClassBase : IClassBase
{
#region IClassBase
	protected bool Enabled;
	protected bool Valid;
	
	public virtual void Create()	{}
	public virtual void SetValid(bool IsValid)
	{
		Enabled	= IsValid;
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
	
	public virtual void Update()
	{
	}
#endregion
}