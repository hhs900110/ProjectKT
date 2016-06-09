using UnityEngine;
using System;
using System.Collections;
using System.Xml;

public class XmlUIParser : MonoBehaviour
{
	private static string _GetString( XmlNode node, string element )
	{
		string retVal = "";
		try
		{
			XmlNode child = node.SelectSingleNode(element);
			retVal = child.InnerText;
		}
		catch ( NullReferenceException ex )
		{
			DebugManager.Log( ex.ToString() );
		}
		catch ( XmlException ex )
		{
			DebugManager.Log( ex.ToString() );
		}
		finally
		{
			DebugManager.Log( "[node:{0}] [element:{1}][return:{2}]", node.Name, element, retVal );
		}
		return retVal;
	}
	
	private static int _GetInt( XmlNode node, string element )
	{
		int retVal = 0;
		try
		{
			XmlNode child = node.SelectSingleNode(element);
			retVal = int.Parse(child.InnerText);
		}
		catch ( NullReferenceException ex )
		{
			DebugManager.Log( ex.ToString() );
		}
		catch ( XmlException ex )
		{
			DebugManager.Log( ex.ToString() );
		}
		finally
		{
			DebugManager.Log( "[node:{0}] [element:{1}][return:{2}]", node.Name, element, retVal );
		}
		return retVal;
	}
	
	private static float _GetSingle( XmlNode node, string element )
	{
		float retVal = 0.0f;
		try
		{
			XmlNode child = node.SelectSingleNode(element);
			retVal = float.Parse(child.InnerText);
		}
		catch ( NullReferenceException ex )
		{
			DebugManager.Log( ex.ToString() );
		}
		catch ( XmlException ex )
		{
			DebugManager.Log( ex.ToString() );
		}
		finally
		{
			DebugManager.Log( "[node:{0}] [element:{1}][return:{2}]", node.Name, element, retVal );
		}
		return retVal;
	}
	
	private static string _GetAttribute( XmlNode node, string element )
	{
		return _GetAttribute( node, element, element );
	}
	
	private static string _GetAttribute( XmlNode node, string element, string attribute )
	{
		string retVal = "";
		try
		{
			XmlNode child = node.SelectSingleNode(element).Attributes.GetNamedItem(attribute);
			retVal = child.InnerText;
		}
		catch ( NullReferenceException ex )
		{
			DebugManager.Log( ex.ToString() );
		}
		catch ( XmlException ex )
		{
			DebugManager.Log( ex.ToString() );
		}
		finally
		{
			DebugManager.Log( "[node:{0}] [element:{1}][return:{2}]", node.Name, element, retVal );
		}
		return retVal;
	}
	
	public static GameObject GetUserInterface_1_0_1( XmlNode node )
	{
		GameObject retObject = null;
		switch( node.Name )
		{
		case "EZGUI_TEXTURE":
			{
				string ObjectName		= _GetString( node, "Name" );
				string ObjectPath		= _GetString( node, "MainPath" );
				int PosX				= _GetInt( node, "Position/x" );
				int PosY				= _GetInt( node, "Position/y" );
				int PosZ				= _GetInt( node, "Position/z" );
				string BasePosType		= _GetAttribute( node, "BasePosType" );
				
				retObject = ResourceLoad.GetEZGUI_TEXTURE( ObjectName, ObjectPath, PosX, PosY, PosZ, BasePosType );
			}break;
			
		case "EZGUI_BUTTON":
			{
				string ObjectName		= _GetString( node, "Name" );
				string ObjectPath		= _GetString( node, "MainPath" );
				int PosX				= _GetInt( node, "Position/x" );
				int PosY				= _GetInt( node, "Position/y" );
				int PosZ				= _GetInt( node, "Position/z" );
				string BasePosType		= _GetAttribute( node, "BasePosType" );
				
				retObject = ResourceLoad.GetEZGUI_BUTTON( ObjectName, ObjectPath, PosX, PosY, PosZ, BasePosType );
			}break;
			
		case "EZGUI_RADIO":
			{
				string ObjectName		= _GetString( node, "Name" );
				string ObjectPath		= _GetString( node, "MainPath" );
				int PosX				= _GetInt( node, "Position/x" );
				int PosY				= _GetInt( node, "Position/y" );
				int PosZ				= _GetInt( node, "Position/z" );
				string BasePosType		= _GetAttribute( node, "BasePosType" );
				
				retObject = ResourceLoad.GetEZGUI_RADIOBUTTON(ObjectName, ObjectPath, PosX, PosY, PosZ, BasePosType);
			}break;
			
		case "MOBILE_PREFAB_IMAGE":
			{
				string ObjectName		= _GetString( node, "Name" );
				string ObjectPath		= _GetString( node, "MainPath" );
				
				retObject = ResourceLoad.GetMOBILE_PREFAB_IMAGE(ObjectName, ObjectPath);
			}break;
			
		case "EZTEXT_FIELD":
			{
				string ObjectName		= _GetString( node, "Name" );
				string ObjectPath		= _GetString( node, "MainPath" );
				int PosX				= _GetInt( node, "Position/x" );
				int PosY				= _GetInt( node, "Position/y" );
				int PosZ				= _GetInt( node, "Position/z" );
				string BasePosType		= _GetAttribute( node, "BasePosType" );
				
				retObject = ResourceLoad.GetEZGUI_TEXTFIELD(ObjectName, ObjectPath, PosX, PosY, PosZ, BasePosType);
			}break;
			
		case "EZGUI_SLIDER":
			{
				string ObjectName		= _GetString( node, "Name" );
				string ObjectPath		= _GetString( node, "MainPath" );
				int PosX				= _GetInt( node, "Position/x" );
				int PosY				= _GetInt( node, "Position/y" );
				int PosZ				= _GetInt( node, "Position/z" );
				string BasePosType		= _GetAttribute( node, "BasePosType" );
				string Orientation		= _GetAttribute( node, "Orientation" );
				string Anchor			= _GetAttribute( node, "Anchor" );
				
				retObject = ResourceLoad.GetEZGUI_SLIDER(ObjectName, ObjectPath, PosX, PosY, PosZ, Orientation, Anchor, BasePosType);
			}break;
			
		case "EZ_PROGRESS_BAR":
			{
				string ObjectName		= _GetString( node, "Name" );
				string ObjectPath		= _GetString( node, "MainPath" );
				int PosX				= _GetInt( node, "Position/x" );
				int PosY				= _GetInt( node, "Position/y" );
				int PosZ				= _GetInt( node, "Position/z" );
				string BasePosType		= _GetAttribute( node, "BasePosType" );
				
				retObject = ResourceLoad.GetEZGUI_PROGRESSBAR(ObjectName, ObjectPath, PosX, PosY, PosZ, BasePosType);
			}break;
			
		case "EZGUI_SPRITE_TEXT":
			{
				string ObjectName		= _GetString( node, "Name" );
				string ObjectPath		= _GetString( node, "MainPath" );
				int PosX				= _GetInt( node, "Position/x" );
				int PosY				= _GetInt( node, "Position/y" );
				int PosZ				= _GetInt( node, "Position/z" );
				string Alignment		= _GetAttribute( node, "Alignment" );
				string Anchor			= _GetAttribute( node, "Anchor" );
				string Text				= _GetString( node, "Text" );
				string BasePosType		= _GetAttribute( node, "BasePosType" );
				
				retObject = ResourceLoad.GetEZGUI_SPRITETEXT(ObjectName, ObjectPath, PosX, PosY, PosZ, Anchor, Alignment, Text, BasePosType);
			}break;
			
		case "EZGUI_OUTLINE_TEXT":
			{
				string ObjectName		= _GetString( node, "Name" );
				string FrontPath		= _GetString( node, "MainPath" );
				string BackPath			= _GetString( node, "SubPath" );
				int PosX				= _GetInt( node, "Position/x" );
				int PosY				= _GetInt( node, "Position/y" );
				int PosZ				= _GetInt( node, "Position/z" );
				string Anchor			= _GetAttribute( node, "Anchor" );
				string Alignment		= _GetAttribute( node, "Alignment" );
				string Text				= _GetString( node, "Text" );
				string BasePosType		= _GetAttribute( node, "BasePosType" );
				
				retObject = ResourceLoad.GetEZGUI_OUTLINETEXT(ObjectName, FrontPath, BackPath, PosX, PosY, PosZ, Anchor, Alignment, Text, BasePosType);
			}break;
			
		case "EZGUI_SCROLL_LIST":
			{
				string ObjectName		= _GetString( node, "Name" );
				int PosX				= _GetInt( node, "Position/x" );
				int PosY				= _GetInt( node, "Position/y" );
				int PosZ				= _GetInt( node, "Position/z" );
				int TextureSizeX		= _GetInt( node, "TextureSize/x" );
				int TextureSizeY		= _GetInt( node, "TextureSize/y" );
				string Orientation		= _GetAttribute( node, "Orientation" );
				string Alignment		= _GetAttribute( node, "Alignment" );
				string Direction		= _GetAttribute( node, "Direction" );
				string BasePosType		= _GetAttribute( node, "BasePosType" );
				
				retObject = ResourceLoad.GetEZGUI_SCROLLLIST(ObjectName, PosX, PosY, PosZ, TextureSizeX, TextureSizeY, Orientation, Direction, Alignment, BasePosType);
			}break;
			
		case "MESH":
			{
				string ObjectName		= _GetString( node, "Name" );
				string ObjectPath		= _GetString( node, "MainPath" );
				
				float  ModelPosX		= _GetSingle(node, "RealPosition/x");
				float  ModelPosY		= _GetSingle(node, "RealPosition/y");
				float  ModelPosZ		= _GetSingle(node, "RealPosition/z");
				float  RotateX			= _GetSingle(node, "RealRotation/x");
				float  RotateY			= _GetSingle(node, "RealRotation/y");
				float  RotateZ			= _GetSingle(node, "RealRotation/z");
				float  ScaleX			= _GetSingle(node, "RealScale/x");
				float  ScaleY			= _GetSingle(node, "RealScale/y");
				float  ScaleZ			= _GetSingle(node, "RealScale/z");
				
				retObject = (GameObject)Instantiate(Resources.Load(ObjectPath, typeof(GameObject)), Vector3.zero, Quaternion.identity);
				
				ObjectMesh objectMesh = retObject.AddComponent<ObjectMesh>();
				objectMesh.Create();
				objectMesh.SetName(ObjectName);
				objectMesh.SetPos(new Vector3(ModelPosX, ModelPosY, ModelPosZ));
				objectMesh.SetRotate(new Vector3(RotateX, RotateY, RotateZ));
				objectMesh.SetScale(new Vector3(ScaleX, ScaleY, ScaleZ));
				
				retObject.name = ObjectName;
				retObject.transform.parent = Main.inst.GetResourceObject_MESH().transform;
			}break;
			
		default:
			{
				DebugManager.Log( "Unknown Target!!!" );
			}break;
		}
		
		return retObject;
	}
}
