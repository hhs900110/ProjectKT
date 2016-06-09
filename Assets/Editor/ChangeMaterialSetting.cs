using UnityEngine;
using UnityEditor;
using System.IO;

public class ChangeMaterialSetting : ScriptableObject
{
	[MenuItem ("Custom/Create Prefab Object")]
	static void CreatePrefabfromFBX()
	{
		Object[] gameobjects = Selection.GetFiltered(typeof(GameObject), SelectionMode.DeepAssets);
		
		foreach(GameObject go in gameobjects)
		{
			if(!File.Exists("Assets/Prefabs/"+go.name+".prefab"))
			{
				GameObject prefab = CreatePrefab(go, go.name) as GameObject;
				
				Component[] transforms = prefab.GetComponentsInChildren(typeof(Transform), true);
				
				foreach(Transform tf in transforms)
				{
					if(!tf.name.Contains("ani"))
					{
						tf.gameObject.isStatic = true;
					}
					
					if(tf.gameObject.GetComponent<Renderer>())
					{
						if(tf.gameObject.name.Contains("collider"))
						{
							tf.gameObject.GetComponent<Renderer>().enabled = false;
							//Destroy(tf.gameObject.renderer);
						}
						else
						{
							for(int i = 0; i < tf.gameObject.GetComponent<Renderer>().sharedMaterials.Length; i++)
							{
								Material mat = tf.gameObject.GetComponent<Renderer>().sharedMaterials[i];
								
								if(mat.mainTexture != null)
								{
									CreateMaterial(mat.mainTexture as Texture2D);
									
									//tf.gameObject.renderer.sharedMaterials[i] = material;
								}
							}
						}
					}
				}

				//GameObject instance = EditorUtility.InstantiatePrefab(prefab) as GameObject;
				
				//SetNewMaterial(instance);
				//instance.SetActiveRecursively(false);
			}
		}
		
		AssetDatabase.Refresh();
	}
		   
	static Object CreatePrefab(GameObject go, string name) 
	{ 
		Object tempPrefab = EditorUtility.CreateEmptyPrefab("Assets/Prefabs/" + name + ".prefab");
		tempPrefab = EditorUtility.ReplacePrefab(go, tempPrefab);
		return tempPrefab;
	}
	
	static void CreateMaterial(Texture2D srcTexture)
	{
		string dstPath = CopyTextureFile(srcTexture);
		Texture texture = AssetDatabase.LoadAssetAtPath(dstPath, typeof(Texture)) as Texture;
		Material material = AssetDatabase.LoadAssetAtPath("Assets/Materials/"+texture.name, typeof(Material)) as Material;
		if(material == null)
		{
			if(texture.name.Contains("shadow"))
			{
				material = new Material(Shader.Find("Transparent/VertexLit with Z"));
			}
			else
			{
				material = new Material(Shader.Find("Diffuse"));
			}
			
			material.mainTexture = texture;
			AssetDatabase.CreateAsset(material, "Assets/Materials/"+texture.name+".mat");
			AssetDatabase.Refresh();
		}
	}
	
	static string CopyTextureFile(Texture srcTexture)
	{
		string srcPath = AssetDatabase.GetAssetPath(srcTexture);
		string dstPath = "Assets/Textures/"+srcTexture.name+".png";
		
		if(!File.Exists(dstPath))
		{
			FileUtil.CopyFileOrDirectory(srcPath, dstPath);
			//AssetDatabase.CopyAsset(srcPath, dstPath);
			AssetDatabase.Refresh();
		}
		
		return dstPath;
	}
	
	static void SetNewMaterial(GameObject go)
	{
		Component[] transforms = go.GetComponentsInChildren(typeof(Transform), true);
		
		foreach(Transform tf in transforms)
		{
			if(tf.gameObject.GetComponent<Renderer>())
			{
				if(tf.gameObject.name.Contains("collider"))
				{
					for(int i = 0; i < tf.gameObject.GetComponent<Renderer>().sharedMaterials.Length; i++)
					{
						tf.gameObject.GetComponent<Renderer>().sharedMaterials[i] = null;
					}
				}
				else
				{
					for(int i = 0; i < tf.gameObject.GetComponent<Renderer>().sharedMaterials.Length; i++)
					{
						Texture texture = tf.gameObject.GetComponent<Renderer>().sharedMaterials[i].mainTexture;
						
						if(texture != null)
						{
							Material material = AssetDatabase.LoadAssetAtPath("Assets/Materials/"+texture.name, typeof(Material)) as Material;
							tf.gameObject.GetComponent<Renderer>().sharedMaterials[i] = material;
						}
					}
				}
			}
		}
	}
	
	
	[MenuItem ("Custom/Create Materials")]
	static void CreateMaterialsfromTextures()
	{
		Object[] textures = Selection.GetFiltered(typeof(Texture2D), SelectionMode.DeepAssets);
		
		foreach(Texture2D texture in textures)
		{
			CreateMaterial(texture);
		}
	}
}
