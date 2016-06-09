using UnityEngine;
using UnityEditor;
using System.IO;

public class CreateEZGuiButton : ScriptableObject
{
	private static UIButton  Button;
	private static Texture2D BaseTexture;
	private static ASCSEInfo StateInfo;
	
	[MenuItem ("Custom/Create EzGui Button")]	
	static void CreateEzGuiButton()
	{
		TextParser Parser	=	new TextParser();
		Parser.Open("Script/iPhone/ButtonResource/CreateButtonPrefab");
		bool IsLoop = true;
		while (IsLoop)
		{
			GameObject prefab 	 = null;
			var ResourceTypeName = Parser.GetTokenChar();
			if (Parser.INVALID_VALUE.ToString() == ResourceTypeName || ResourceTypeName == null)
			{
				IsLoop = false;
			}
			else
			{
				string ObjectName = null;
				string PathNormal 	= null;
				string PathOver 	= null;
				string PathActive 	= null;
				string PathDisabled = null;
				string PathMaterial = null;
				string PathGoalPos  = null;
				
				if (ResourceTypeName == "UIBUTTON")
				{
					ObjectName = Parser.GetTokenChar();
					prefab = new GameObject(ObjectName);
					prefab.AddComponent<UIButton>();
					Button = prefab.GetComponent<UIButton>();
					
					PathNormal	 	= Parser.GetTokenChar();

					int CurState			= (int)UIButton.CONTROL_STATE.NORMAL;
					SetTexture(CurState, PathNormal);
					
					PathOver= Parser.GetTokenChar();
					CurState				= (int)UIButton.CONTROL_STATE.OVER;
					if(PathOver	!=	"NULL")
					{
						SetTexture(CurState, PathOver);
					}
					else
					{
						SetTexture(CurState, PathNormal);						
					}
					
					PathActive= Parser.GetTokenChar();
					CurState				= (int)UIButton.CONTROL_STATE.ACTIVE;
					if(PathActive	!=	"NULL")
					{	
						SetTexture(CurState, PathActive);
					}
					else
					{
						if(PathOver != "NULL")
						{
							SetTexture(CurState, PathOver);						
						}
						else
						{
							SetTexture(CurState, PathNormal);						
						}
					}
					
					PathDisabled= Parser.GetTokenChar();
					CurState				= (int)UIButton.CONTROL_STATE.DISABLED;
					if(PathDisabled	!=	"NULL")
					{	
						SetTexture(CurState, PathDisabled);
					}
					else
					{
						SetTexture(CurState, PathNormal);						
					}
					
					PathMaterial= Parser.GetTokenChar();
					if(PathMaterial	!=	"NULL")
					{
						Material BuildingMat = (Material)Resources.Load(PathMaterial, typeof(Material));
						BuildingMat.mainTexture	=	BaseTexture;
						Button.SetMaterial(BuildingMat);
					}
					else
					{
						Debug.LogError("Material Missing");
					}
					
					PathGoalPos= Parser.GetTokenChar();					
					if(PathGoalPos	!=	"NULL")
					{
						CreatePrefab(prefab, ObjectName, PathGoalPos);// as GameObject;
						DestroyImmediate(prefab);
						
					}
					else
					{
						Debug.LogError("Invalid Path");
					}
					
					AssetDatabase.Refresh();
				}
			}
		}
	}
	
	static void SetTexture(int StateNum, string TexPath)
	{
		Texture2D TempTex 		= (Texture2D)Resources.Load(TexPath, typeof(Texture2D));
		StateInfo				= Button.GetStateElementInfo(StateNum);
		StateInfo.tex 			= TempTex;
		StateInfo.stateObj.frameGUIDs = new string[1] { "" };
		StateInfo.stateObj.frameGUIDs[0] = AssetDatabase.AssetPathToGUID(AssetDatabase.GetAssetPath(StateInfo.tex));		
		
		if(StateNum == 0)
		{
			BaseTexture	=	TempTex;
		}
	}
	
	static Object CreatePrefab(GameObject go, string name, string GoalPath)
	{
		Object tempPrefab = EditorUtility.CreateEmptyPrefab(GoalPath + name + ".prefab");
		tempPrefab = EditorUtility.ReplacePrefab(go, tempPrefab);
		return tempPrefab;
	}
}

