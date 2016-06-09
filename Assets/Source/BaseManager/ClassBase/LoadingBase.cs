using UnityEngine;
using System.Collections;
using DefineBase;

public abstract class LoadingBase : ILoadingStep, IClassBase
{
	private	GameObject		ScriptObject;
	
#region IClassBase
	protected bool Enabled;
	protected bool Valid;
	
	public virtual void Create()
	{
		Enabled	= true;
	}
	
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
	
	public virtual void Release()
	{
	}
	
	public virtual void Message(int Msg, int Param1, int Param2)	{}
	
	public bool IsUpdate()
	{
		if(!enabled)
		{ return false; }
		return true;
	}
	
	public virtual void Update()
	{
		if(Load_Step != LOAD_STEP._NULL)
		{
			LoadStep();
		}
	}
#endregion
	
	protected void SendMessage(int Msg, int Param1)					{ Main.inst.Message(Msg, Param1); }
	protected void SendMessage(int Msg, int Param1, int Param2)		{ Main.inst.Message(Msg, Param1, Param2); }
	
#region LoadingStep
	private LOAD_STEP	m_vLoadStep;
	
	public DefineBase.LOAD_STEP Load_Step
	{
		get { return m_vLoadStep; }
		set { m_vLoadStep = value; }
	}
	
	public void LoadStep()
	{
		switch(Load_Step)
		{
		case LOAD_STEP._STEP1:		LoadStep1();		break;
		case LOAD_STEP._STEP1_END:	LoadStep1_End();	break;
		case LOAD_STEP._STEP2:		LoadStep2();		break;
		case LOAD_STEP._STEP2_END:	LoadStep2_End();	break;
		case LOAD_STEP._STEP3:		LoadStep3();		break;
		case LOAD_STEP._STEP3_END:	LoadStep3_End();	break;
		case LOAD_STEP._STEP4:		LoadStep4();		break;
		case LOAD_STEP._STEP4_END:	LoadStep4_End();	break;
		case LOAD_STEP._STEP5:		LoadStep5();		break;
		case LOAD_STEP._STEP5_END:	LoadStep5_End();	break;
		}
	}
	
	public virtual void LoadStep1()		{}
	public virtual void LoadStep2()		{}
	public virtual void LoadStep3()		{}
	public virtual void LoadStep4()		{}
	public virtual void LoadStep5()		{}
	public virtual void LoadStep1_End()	{}
	public virtual void LoadStep2_End()	{}
	public virtual void LoadStep3_End()	{}
	public virtual void LoadStep4_End()	{}
	public virtual void LoadStep5_End()	{}
	public void LoadStepLoading_End()
	{
		switch(Load_Step)
		{
		case LOAD_STEP._STEP1_LOADING:	Load_Step	= LOAD_STEP._STEP1_END;		break;
		case LOAD_STEP._STEP2_LOADING:	Load_Step	= LOAD_STEP._STEP2_END;		break;
		case LOAD_STEP._STEP3_LOADING:	Load_Step	= LOAD_STEP._STEP3_END;		break;
		case LOAD_STEP._STEP4_LOADING:	Load_Step	= LOAD_STEP._STEP4_END;		break;
		case LOAD_STEP._STEP5_LOADING:	Load_Step	= LOAD_STEP._STEP5_END;		break;
		}
	}
	public void EndLoadStep()
	{
		Load_Step	= LOAD_STEP._NULL;
		Main.inst.LoadStepLoading_End();
	}
#endregion
}