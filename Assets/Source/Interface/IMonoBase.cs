using System;

public interface IMonoBase
{
	void Create();
	void SetValid(bool IsValid);
	bool GetValid();
	
	void Release();
	
	void Message(int Msg, int Param1, int Param2);
	
/*
#region IClassBase
	private bool Valid;
	
	public virtual void Create()	{}
	public virtual void SetValid(bool IsValid)
	{
		enabled	= IsValid;
		Valid	= IsValid;
	}
	public virtual bool GetValid()
	{
		return Valid;
	}
	public virtual void Release()	{}
	
	public virtual void Message(int Msg, int Param1, int Param2)	{}
#endregion
*/
}