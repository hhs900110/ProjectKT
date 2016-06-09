using UnityEngine;
using System.Collections;
using System.Xml;

public class XmlParser
{
	private XmlDocument _xmlDoc;
	
	public bool OpenXml(string filename)
	{
		try
		{
			CloseXml();
			TextAsset asset = (TextAsset)Resources.Load( filename, typeof(TextAsset) );
			
			if(asset == null)
			{
				Debug.LogError("XML Can't Find!!!! / " + filename);
				return false;
			}
			else
			{
				_xmlDoc = new XmlDocument();
				_xmlDoc.LoadXml(asset.text.Trim());
			}
		}
		catch( XmlException ex )
		{
			ex.ToString();
			_xmlDoc = null;
		}
		return IsOpen();
	}
	
	public void CloseXml()
	{
		_xmlDoc = null;
	}
	
	public bool IsOpen()
	{
		return _xmlDoc != null;
	}
	
	public XmlNode GetNode(string element)
	{
		return _xmlDoc.SelectSingleNode(element);
	}
} 
