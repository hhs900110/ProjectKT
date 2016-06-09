using UnityEngine;
using System.Collections;
using DefineBase;

public class PlayStageMain : StageBase
{
#region IClassBase
	public override void SetValid(bool IsValid)
	{
		base.SetValid(IsValid);
		
		if(Main.inst.GetHudManager() != null)
		{
			if(IsValid)
			{
				Main.inst.GetHudManager().GetHudMainStage().SetPopup();
			}
			else
			{
				Main.inst.GetHudManager().GetHudMainStage().ClosePopup();
			}
		}
	}
#endregion
}