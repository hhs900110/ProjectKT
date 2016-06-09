using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DefineBase;

#pragma warning disable 0168 // variable declared but not used.
#pragma warning disable 0219 // variable assigned but not used.
#pragma warning disable 0414 // private field assigned but not used.

public class GameResourceManager
{
	private Dictionary<string, GameObject> m_tableResource;
	
	public void Create(string FileName)
	{
		m_tableResource		= new Dictionary<string, GameObject>();
		TextParser Parser	= Main.parser;
		Parser.Open(FileName);
		
		bool IsLoop			= true;
		while (IsLoop)
		{
			GameObject	CurObject			= null;
			string		ResourceTypeName	= Parser.GetTokenChar();
			if(Parser.INVALID_VALUE.ToString() == ResourceTypeName || ResourceTypeName == null)
			{
				IsLoop = false;
			}
			else
			{
				if(ResourceTypeName == "EZGUI_TEXTURE")
				{
					string ObjectName		= Parser.GetTokenChar();
					string ObjectPath		= Parser.GetTokenChar();
					int PosX				= Parser.GetTokenInt();
					int PosY				= Parser.GetTokenInt();
					int PosZ				= Parser.GetTokenInt();
					string BasePosType		= Parser.GetTokenChar();
					
					CurObject				= ResourceLoad.GetEZGUI_TEXTURE(ObjectName, ObjectPath, PosX, PosY, PosZ, BasePosType);
				}
				else if(ResourceTypeName == "EZGUI_BUTTON")
				{
					string ObjectName		= Parser.GetTokenChar();
					string ObjectPath		= Parser.GetTokenChar();
					int PosX				= Parser.GetTokenInt();
					int PosY				= Parser.GetTokenInt();
					int PosZ				= Parser.GetTokenInt();
					string BasePosType		= Parser.GetTokenChar();
					
					CurObject				= ResourceLoad.GetEZGUI_BUTTON(ObjectName, ObjectPath, PosX, PosY, PosZ, BasePosType);
				}
				else if(ResourceTypeName == "EZGUI_RADIO")
				{
					string ObjectName		= Parser.GetTokenChar();
					string ObjectPath		= Parser.GetTokenChar();
					int PosX				= Parser.GetTokenInt();
					int PosY				= Parser.GetTokenInt();
					int PosZ				= Parser.GetTokenInt();
					string BasePosType		= Parser.GetTokenChar();
					
					CurObject				= ResourceLoad.GetEZGUI_RADIOBUTTON(ObjectName, ObjectPath, PosX, PosY, PosZ, BasePosType);
				}
				else if(ResourceTypeName == "MOBILE_PREFAB_IMAGE")
				{
					string ObjectName		= Parser.GetTokenChar();
					string ObjectPath		= Parser.GetTokenChar();
					
					CurObject				= ResourceLoad.GetMOBILE_PREFAB_IMAGE(ObjectName, ObjectPath);
				}
				else if(ResourceTypeName == "EZTEXT_FIELD")
				{
					string ObjectName		= Parser.GetTokenChar();
					string ObjectPath		= Parser.GetTokenChar();
					int PosX				= Parser.GetTokenInt();
					int PosY				= Parser.GetTokenInt();
					int PosZ				= Parser.GetTokenInt();
					string BasePosType		= Parser.GetTokenChar();
					
					CurObject				= ResourceLoad.GetEZGUI_TEXTFIELD(ObjectName, ObjectPath, PosX, PosY, PosZ, BasePosType);
				}
				
				else if(ResourceTypeName == "EZGUI_SLIDER")
				{
					string ObjectName		= Parser.GetTokenChar();
					string ObjectPath		= Parser.GetTokenChar();
					int PosX				= Parser.GetTokenInt();
					int PosY				= Parser.GetTokenInt();
					int PosZ				= Parser.GetTokenInt();
					string Direction		= Parser.GetTokenChar();
					string Anchor			= Parser.GetTokenChar();
					string BasePosType		= Parser.GetTokenChar();
					
					CurObject				= ResourceLoad.GetEZGUI_SLIDER(ObjectName, ObjectPath, PosX, PosY, PosZ, Direction, Anchor, BasePosType);
				}
				else if(ResourceTypeName == "EZ_PROGRESS_BAR")
				{
					string ObjectName		= Parser.GetTokenChar();
					string ObjectPath		= Parser.GetTokenChar();
					int PosX				= Parser.GetTokenInt();
					int PosY				= Parser.GetTokenInt();
					int PosZ				= Parser.GetTokenInt();
					string BasePosType		= Parser.GetTokenChar();
					
					CurObject				= ResourceLoad.GetEZGUI_PROGRESSBAR(ObjectName, ObjectPath, PosX, PosY, PosZ, BasePosType);
				}
				else if(ResourceTypeName == "EZGUI_SPRITE_TEXT")
				{
					string ObjectName		= Parser.GetTokenChar();
					string ObjectPath		= Parser.GetTokenChar();
					int PosX				= Parser.GetTokenInt();
					int PosY				= Parser.GetTokenInt();
					int PosZ				= Parser.GetTokenInt();
					string Anchor			= Parser.GetTokenChar();
					string Allignment		= Parser.GetTokenChar();
					string Text				= Parser.GetTokenChar();
					string BasePosType		= Parser.GetTokenChar();
					
					CurObject				= ResourceLoad.GetEZGUI_SPRITETEXT(ObjectName, ObjectPath, PosX, PosY, PosZ, Anchor, Allignment, Text, BasePosType);
				}
				else if(ResourceTypeName == "EZGUI_OUTLINE_TEXT")
				{
					string ObjectName		= Parser.GetTokenChar();
					string FrontPath		= Parser.GetTokenChar();
					string BackPath			= Parser.GetTokenChar();
					int PosX				= Parser.GetTokenInt();
					int PosY				= Parser.GetTokenInt();
					int PosZ				= Parser.GetTokenInt();
					string Anchor			= Parser.GetTokenChar();
					string Allignment		= Parser.GetTokenChar();
					string Text				= Parser.GetTokenChar();
					string BasePosType		= Parser.GetTokenChar();
					
					CurObject				= ResourceLoad.GetEZGUI_OUTLINETEXT(ObjectName, FrontPath, BackPath, PosX, PosY, PosZ, Anchor, Allignment, Text, BasePosType);
				}
				else if(ResourceTypeName == "EZGUI_BUTTON_OUTLINE")
				{
					string ObjectName		= Parser.GetTokenChar();
					string ObjectPath		= Parser.GetTokenChar();
					string FrontPath		= Parser.GetTokenChar();
					string BackPath			= Parser.GetTokenChar();
					int PosX				= Parser.GetTokenInt();
					int PosY				= Parser.GetTokenInt();
					int PosZ				= Parser.GetTokenInt();
					string Anchor			= Parser.GetTokenChar();
					string Allignment		= Parser.GetTokenChar();
					string Text				= Parser.GetTokenChar();
					string BasePosType		= Parser.GetTokenChar();
					
					CurObject				= ResourceLoad.GetEZGUI_BUTTON_OUTLINE(ObjectName, ObjectPath, FrontPath, BackPath, PosX, PosY, PosZ, Anchor, Allignment, Text, BasePosType);
				}
				else if(ResourceTypeName == "EZGUI_SCROLL_LIST")
				{
					string ObjectName		= Parser.GetTokenChar();
					int PosX				= Parser.GetTokenInt();
					int PosY				= Parser.GetTokenInt();
					int PosZ				= Parser.GetTokenInt();
					int TextureSizeX		= Parser.GetTokenInt();
					int TextureSizeY		= Parser.GetTokenInt();
					string Orientation		= Parser.GetTokenChar();
					string Direction		= Parser.GetTokenChar();
					string Alignment		= Parser.GetTokenChar();
					string BasePosType		= Parser.GetTokenChar();
					
					CurObject				= ResourceLoad.GetEZGUI_SCROLLLIST(ObjectName, PosX, PosY, PosZ, TextureSizeX, TextureSizeY, Orientation, Direction, Alignment, BasePosType);
				}
				else if(ResourceTypeName == "MESH")
				{
					string ObjectName		= Parser.GetTokenChar();
					string ObjectPath		= Parser.GetTokenChar();
					
					float  ModelPosX		= Parser.GetTokenReal();
					float  ModelPosY		= Parser.GetTokenReal();
					float  ModelPosZ		= Parser.GetTokenReal();
					float  RotateX			= Parser.GetTokenReal();
					float  RotateY			= Parser.GetTokenReal();
					float  RotateZ			= Parser.GetTokenReal();
					float  ScaleX			= Parser.GetTokenReal();
					float  ScaleY			= Parser.GetTokenReal();
					float  ScaleZ			= Parser.GetTokenReal();
					
					//CurObject				= (GameObject)Instantiate(ObjectPath, Vector3.zero, Quaternion.identity);
					CurObject = (GameObject)GameObject.Instantiate(Resources.Load(ObjectPath, typeof(GameObject)), Vector3.zero, Quaternion.identity);
					
					CurObject.AddComponent<ObjectMesh>();
					CurObject.GetComponent<ObjectMesh>().Create();
					CurObject.GetComponent<ObjectMesh>().SetName(ObjectName);
					CurObject.GetComponent<ObjectMesh>().SetPos(new Vector3(ModelPosX, ModelPosY, ModelPosZ));
					CurObject.GetComponent<ObjectMesh>().SetRotate(new Vector3(RotateX, RotateY, RotateZ));
					CurObject.GetComponent<ObjectMesh>().SetScale(new Vector3(ScaleX, ScaleY, ScaleZ));
					
					Transform[] trans;
					trans	= CurObject.GetComponentsInChildren<Transform>();
					
					MeshFilter		mf	= null;
					MeshRenderer	mr	= null;
					
//					foreach (Transform ObjectTrans in trans)
//					{
//						if(ObjectTrans.gameObject.name == "collider")
//						{
//							ObjectTrans.gameObject.AddComponent<Rigidbody>();
//							
//							Rigidbody ObjectRigidbody = ObjectTrans.gameObject.GetComponent<Rigidbody>();
//							
//							ObjectRigidbody.drag			 = 0;
//							ObjectRigidbody.useGravity		 = false;
//							ObjectRigidbody.isKinematic		 = true;
//							ObjectRigidbody.detectCollisions = true;
//							
//							ObjectTrans.gameObject.AddComponent<BoxCollider>();
//							ObjectTrans.gameObject.GetComponent<BoxCollider>().collider.isTrigger = true;
//							
//							ObjectTrans.gameObject.GetComponent<BoxCollider>().center = new Vector3(0.0f, 0.8f, 0.0f);
//							ObjectTrans.gameObject.GetComponent<BoxCollider>().size   = new Vector3(ObjectTrans.gameObject.GetComponent<BoxCollider>().size.x - 0.15f,
//							                                                                        1.5f,
//							                                                                        ObjectTrans.gameObject.GetComponent<BoxCollider>().size.z- 0.15f);
//							
//							mf	= ObjectTrans.gameObject.GetComponent<MeshFilter>();
//							mr	= ObjectTrans.gameObject.GetComponent<MeshRenderer>();
//							
//							DestroyImmediate(mf);
//							DestroyImmediate(mr);
//						}
//						else if(ObjectTrans.gameObject.name == "collider2")
//						{
//							DestroyImmediate(ObjectTrans.gameObject);
//						}
//						else if(ObjectTrans.gameObject.name == "smoke")
//						{
//							mf = ObjectTrans.gameObject.GetComponent<MeshFilter>();
//							mr = ObjectTrans.gameObject.GetComponent<MeshRenderer>();
//							
//							DestroyImmediate(mf);
//							DestroyImmediate(mr);
//						}
//						CurObject.SetActiveRecursively(false);
//					}
					CurObject.name			= ObjectName;
					CurObject.transform.parent	= Main.inst.GetResourceObject_MESH().transform;
				}
			}
			
			if(CurObject)
			{
				AddResource(CurObject);
			}
		}
		Parser.Close();
	}
	
	public void CreateWithXml(string FileName)
	{
		m_tableResource		= new Dictionary<string, GameObject>();
		XmlManager.Parse(FileName, delegate(GameObject obj)
		{
			AddResource(obj);
		});
	}
	
	public void AddResource(GameObject ObjectResource)
	{
		ObjectResource.active = false;
		
		if(!m_tableResource.ContainsKey(ObjectResource.name))
		{
			m_tableResource.Add(ObjectResource.name, ObjectResource);
		}
		else
		{
			Debug.LogError ("ERROR===================ERROR\n" + "Add Resource Same Name -> " + ObjectResource.name);
		}
	}
	
	public GameObject GetResource(string Name)
	{
		if(m_tableResource.ContainsKey(Name))
		{
			return m_tableResource[Name];
		}
		
		return null;
	}
	
	public GameObject GetResourceModelCopy(string Name)
	{
		if(m_tableResource.ContainsKey(Name))
		{
			return (GameObject)GameObject.Instantiate(GetResource(Name), Vector3.zero, Quaternion.identity);
		}
		
		return null;
	}
	
	public string GetResourceNameMesh(string Name)
	{
		string MeshName = null;
		MeshName = "MESH_";
		for(int i = MeshName.Length - 1; i < Name.Length; i++)
		{
			MeshName += Name[i];
		}
		return MeshName;
	}
	
	public void Release()
	{
		if(m_tableResource != null)
		{
			foreach(KeyValuePair<string, GameObject> Res in m_tableResource)
			{
				GameObject.DestroyImmediate(m_tableResource[Res.Key]);
			}
			
			m_tableResource.Clear();
		}
	}
}