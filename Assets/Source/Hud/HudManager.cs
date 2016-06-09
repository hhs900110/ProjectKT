using UnityEngine;
using System.Collections;
using DefineBase;

public class HudManager : LoadingBase
{
	// Step 1 //
	private HudKittySetting			HudKittySettingScript;
	private HudProgressCircle		HudProgressCircleScript;
	
	// Step 2 //
	private HudUserInfoManager		HudUserInfoManagerScript;
	private HudNumberSetManager		HudNumberSetManagerScript;
	private HudPopupAdvice			HudPopupAdviceScript;
	
	// Step 4 //
	private HudPopupFrameManager	HudPopupFrameManagerScript;
	
	private HudMainStage			HudMainStageScript;
	private HudReadyStage			HudReadyStageScript;
	private HudGameStage			HudGameStageScript;
	private HudScorePopup			HudScorePopupScript;
	
	
#region IClassBase
	public override void Create()
	{
		base.Create();
		
		Load_Step	= LOAD_STEP._STEP1;
	}
	
	public override void Release ()
	{
		base.Release ();
		
		if(HudKittySettingScript != null)		{ HudKittySettingScript.Release(); }
		if(HudProgressCircleScript != null)		{ HudProgressCircleScript.Release(); }
		
		if(HudUserInfoManagerScript != null)	{ HudUserInfoManagerScript.Release(); }
		if(HudNumberSetManagerScript != null)	{ HudNumberSetManagerScript.Release(); }
		if(HudPopupAdviceScript != null)		{ HudPopupAdviceScript.Release(); }
		
		if(HudMainStageScript != null)			{ HudMainStageScript.Release(); }
		if(HudReadyStageScript != null)			{ HudReadyStageScript.Release(); }
		if(HudGameStageScript != null)			{ HudGameStageScript.Release(); }
		if(HudScorePopupScript != null)			{ HudScorePopupScript.Release(); }
		
		if(HudPopupFrameManagerScript != null)	{ HudPopupFrameManagerScript.Release(); }
	}
	
	public override void Update()
	{
		if(HudProgressCircleScript != null)		{ HudProgressCircleScript.Update(); }
		
		if(HudUserInfoManagerScript != null)	{ HudUserInfoManagerScript.Update(); }
		if(HudPopupAdviceScript != null)		{ HudPopupAdviceScript.Update(); }
		
		if(HudMainStageScript != null)			{ HudMainStageScript.Update(); }
		if(HudReadyStageScript != null)			{ HudReadyStageScript.Update(); }
		if(HudGameStageScript != null)			{ HudGameStageScript.Update(); }
		if(HudScorePopupScript != null)			{ HudScorePopupScript.Update(); }
		
		if(HudPopupFrameManagerScript != null)	{ HudPopupFrameManagerScript.Update(); }
		
		if(!IsUpdate())
		{ return ; }
		base.Update();
	}
#endregion
	
#region LoadingStep
	public override void LoadStep1()
	{
		HudKittySettingScript		= new HudKittySetting();
		HudKittySettingScript.Create();
		
		HudProgressCircleScript		= new HudProgressCircle();
		HudProgressCircleScript.Create();
		HudProgressCircleScript.SetPopup();
		
		Load_Step	= LOAD_STEP._STEP2;
	}
	public override void LoadStep2()
	{
		HudUserInfoManagerScript	= new HudUserInfoManager();
		HudUserInfoManagerScript.Create();
		
		HudNumberSetManagerScript	= new HudNumberSetManager();
		HudNumberSetManagerScript.Create();
		
		
		Load_Step	= LOAD_STEP._STEP3;
	}
	public override void LoadStep3()
	{
		Load_Step	= LOAD_STEP._STEP4;
	}
	public override void LoadStep4()
	{
		HudPopupFrameManagerScript	= new HudPopupFrameManager();
		HudPopupFrameManagerScript.Create();
		
		EndLoadStep();
	}
#endregion
	
	public override void Message(int Msg, int Param1, int Param2)
	{
		if(HudKittySettingScript != null)		{ HudKittySettingScript.Message(Msg, Param1, Param2); }
		
		if(HudProgressCircleScript != null)		{ HudProgressCircleScript.Message(Msg, Param1, Param2); }
		
		if(HudUserInfoManagerScript != null)	{ HudUserInfoManagerScript.Message(Msg, Param1, Param2); }
		if(HudNumberSetManagerScript != null)	{ HudNumberSetManagerScript.Message(Msg, Param1, Param2); }
		if(HudPopupAdviceScript != null)		{ HudPopupAdviceScript.Message(Msg, Param1, Param2); }
		
		if(HudMainStageScript != null)			{ HudMainStageScript.Message(Msg, Param1, Param2); }
		if(HudReadyStageScript != null)			{ HudReadyStageScript.Message(Msg, Param1, Param2); }
		if(HudGameStageScript != null)			{ HudGameStageScript.Message(Msg, Param1, Param2); }
		if(HudScorePopupScript != null)			{ HudScorePopupScript.Message(Msg, Param1, Param2); }
		
		if(HudPopupFrameManagerScript != null)	{ HudPopupFrameManagerScript.Message(Msg, Param1, Param2); }
		
		base.Message(Msg, Param1, Param2);
	}
	
	public HudKittySetting		GetHudKittySetting()		{ return HudKittySettingScript; }
	
	public HudProgressCircle	GetHudProgressCircle()		{ return HudProgressCircleScript; }
	
	public HudUserInfoManager	GetHudUserInfoManager()		{ return HudUserInfoManagerScript; }
	public HudNumberSetManager	GetHudNumberSetManager()	{ return HudNumberSetManagerScript; }
	public HudPopupAdvice		GetHudPopupAdvice()
	{
		if(HudPopupAdviceScript == null)
		{
			HudPopupAdviceScript		= new HudPopupAdvice();
			HudPopupAdviceScript.Create();
		}
		return HudPopupAdviceScript;
	}
	
	public HudMainStage			GetHudMainStage()
	{
		if(HudMainStageScript == null)
		{
			HudMainStageScript			= new HudMainStage();
			HudMainStageScript.Create();
		}
		return HudMainStageScript;
	}
	public HudReadyStage		GetHudReadyStage()
	{
		if(HudReadyStageScript == null)
		{
			HudReadyStageScript			= new HudReadyStage();
			HudReadyStageScript.Create();
		}
		return HudReadyStageScript;
	}
	public HudGameStage			GetHudGameStage()
	{
		if(HudGameStageScript == null)
		{
			HudGameStageScript			= new HudGameStage();
			HudGameStageScript.Create();
		}
		return HudGameStageScript;
	}
	public HudScorePopup		GetHudScorePopup()
	{
		if(HudScorePopupScript == null)
		{
			HudScorePopupScript			= new HudScorePopup();
			HudScorePopupScript.Create();
		}
		return HudScorePopupScript;
	}
	
	public HudPopupFrameManager	GetHudPopupFrameManager()	{ return HudPopupFrameManagerScript; }
}