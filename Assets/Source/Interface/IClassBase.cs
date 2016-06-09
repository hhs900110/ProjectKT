using System;

public interface IClassBase
{
	void Create();
	void SetValid(bool IsValid);
	bool GetValid();
	
	bool enabled	{ get; set; }
	void Release();
	
	void Message(int Msg, int Param1, int Param2);
	
	bool IsUpdate();
	void Update();
	
/*
#region IClassBase
	private bool Enabled;
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
	public bool enabled
	{
		get { return Enabled; }
		set { Enabled	= value; }
	}
	public virtual void Release()	{}
	
	public virtual void Message(int Msg, int Param1, int Param2)	{}
	
	public virtual void Update()
	{
		if(!IsUpdate())
		{ return ; }
	}
#endregion
*/
}