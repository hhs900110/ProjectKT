using UnityEngine;
using System.Collections;
using DefineBase;

public class PlayStageReady: StageBase
{
#region IClassBase
	public override void SetValid(bool IsValid)
	{
		base.SetValid(IsValid);
		
		if(Main.inst.GetHudManager() != null)
		{
			if(IsValid)
			{
				Main.inst.GetHudManager().GetHudReadyStage().SetPopup();
			}
			else
			{
				Main.inst.GetHudManager().GetHudReadyStage().ClosePopup();
			}
		}
	}
#endregion
}