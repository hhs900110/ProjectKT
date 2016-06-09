using UnityEngine;
using System.Collections;
using DefineBase;

#pragma warning disable 0168 // variable declared but not used.
#pragma warning disable 0219 // variable assigned but not used.
#pragma warning disable 0414 // private field assigned but not used.

public sealed partial class Main : MonoBehaviour
{
	public bool IsUpdate()
	{
		return true;
	}
	public void Update()
	{
		if(m_LoadStep != LOAD_STEP._NULL)
		{
			LoadStep();
		}
		if(GameManagerScript != null)	{ GameManagerScript.Update(); }
		if(StageManagerScript != null)	{ StageManagerScript.Update(); }
		if(HudManagerScript != null)	{ HudManagerScript.Update(); }
	}
	public DefineBase.LOAD_STEP Load_Step
	{
		get { return m_LoadStep; }
		set { m_LoadStep = value; }
	}
	
	public void CalcLoadingCount(int AddCountNum)
	{
		LoadingCountNum += AddCountNum;
	}
	
	public float GetLoadingProgressValue()
	{
		return ((float)LoadingCountNum/(float)LoadingMaxCountNum);
	}
	
	public void SendScore()
	{
#if UNITY_EDITOR
		if(DefineBaseManager.inst.GetPlatformType() == (int)PLATFORM_TYPE.PLATFORM_WEB)
		{
			StartCoroutine("EnumeratorSendScore");
		}
#elif UNITY_IPHONE || UNITY_ANDROID
#else
		StartCoroutine("EnumeratorSendScore");
#endif
	}
	
	private IEnumerator EnumeratorSendScore()
	{
		string url = "http://halk89.synology.me/croquis/game_research/kitty_turn/data/score_update.php";
		WWWForm form = new WWWForm();
		form.AddField("score", game.GetScore());
		
		WWW www = new WWW(url, form);
		yield return www;
		
		if( www.error != null )
		{
			//ERROR!
//			Debug.Log(www.error.ToString());
			yield return www;
		}
		if( www.error == null )
		{
			//SUCCESS
//			Debug.Log("Success");
		}
	}
	
	public void OnApplicationQuit()
	{
		Release();
	}
}