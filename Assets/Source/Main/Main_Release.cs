using UnityEngine;
using System.Collections;
using DefineBase;

#pragma warning disable 0168 // variable declared but not used.
#pragma warning disable 0219 // variable assigned but not used.
#pragma warning disable 0414 // private field assigned but not used.

public sealed partial class Main : MonoBehaviour
{
#region IClassBase
	public void Release()
	{
		ReleaseScript();
		
		ReleaseGameUnitROOT();
		ReleaseResourceObjectROOT();
		
		DefineBaseManager.inst.Release();
		
		m_inst	= null;
	}
#endregion
	
	private void ReleaseResourceObjectROOT()
	{
		DestroyImmediate(ResourceObject_MESH);
		DestroyImmediate(ResourceObject_EFFECT);
		DestroyImmediate(ResourceObject_EZGUITEXTURE);
		DestroyImmediate(ResourceObject_EZGUIBUTTON);
		DestroyImmediate(ResourceObject_EZGUITEXTFIELD);
		DestroyImmediate(ResourceObject_EZGUISCROLLLIST);
		DestroyImmediate(ResourceObject_EZGUISPRITETEXT);
		DestroyImmediate(ResourceObject_EZPROGRESSBAR);
		DestroyImmediate(ResourceObject_EZGUIOUTLINETEXT);
		DestroyImmediate(ResourceObject_EZGUISLIDER);
		DestroyImmediate(ResourceObject_EZGUIRADIOBUTTON);
		DestroyImmediate(ResourceObject_EZGUIMOBILEPREFAB);
		DestroyImmediate(ResourceObject_ROOT);
	}
	
	private void ReleaseGameUnitROOT()
	{
		DestroyImmediate(GameUnit_ROOT);
	}
	
	private void ReleaseScript()
	{
		if(InputManagerScript)				{ InputManagerScript.Release(); }
		if(DataManagerScript != null)		{ DataManagerScript.Release(); }
		if(StageManagerScript != null)		{ StageManagerScript.Release(); }
		if(HudManagerScript != null)		{ HudManagerScript.Release(); }
	}
}