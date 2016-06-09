using UnityEngine;
using System;
using System.Xml;

public class XmlManager : MonoBehaviour
{
	// xsd: // xsd: http://1.234.11.116/proof_xml/user_interface.xsd
	
	public static void Parse(string filename, Action<GameObject> callback)
	{
		if( callback == null )
		{
			DebugManager.Log( "Callback Function is null!!" );
			return;
		}
		
		XmlParser xmlParser = new XmlParser();
		bool bOpened = xmlParser.OpenXml( filename );
		
		if( bOpened == false )
		{
			DebugManager.Log( "Open Failed - [path:{0}]", filename );
			return;
		}
		
		XmlNode res = xmlParser.GetNode("GameResource");
		if( res == null )
		{
			DebugManager.Log( "Invalid XML!!" );
			return;
		}
		
		foreach( XmlNode nodes in res.ChildNodes )
		{
			string curVersion = _GetVersion( nodes );
			
			switch( nodes.Name )
			{
			case "UserInterface":
				_UserInterfaceParse( curVersion, nodes, (obj) => callback(obj) );
				break;
				
			case "":
				break;
			}
		}
		
	}
	
	//
	private static string _GetVersion( XmlNode node )
	{
		string curVersion = "";
		try
		{
			curVersion = node.Attributes.GetNamedItem("Version").Value;
		}
		catch (NullReferenceException ex)
		{
			DebugManager.Log( "Version Field is empty!!" );
			DebugManager.Log( ex.ToString() );
		}
		catch (Exception ex)
		{
			DebugManager.Log( ex.ToString() );
		}
		return curVersion;
	}
	
	private static void _UserInterfaceParse( string curVersion, XmlNode nodes, Action<GameObject> callback )
	{
		switch( curVersion )
		{
		case "1.0.1":
			foreach( XmlNode node in nodes.ChildNodes )
			{
				try
				{
					GameObject curObject = XmlUIParser.GetUserInterface_1_0_1( node );
					if( curObject )
					{
						callback(curObject);
					}
				}
				catch( Exception ex )
				{
					DebugManager.Log( ex.ToString() );
				}
			}
			break;
			
		default:
			DebugManager.Log( "Invalid XML UserInterface Version!!" );
			break;
		}
	}
}
