using UnityEngine;
using System.Collections;
using System.IO;
using DefineBase;

#pragma warning disable 0168 // variable declared but not used.
#pragma warning disable 0219 // variable assigned but not used.
#pragma warning disable 0414 // private field assigned but not used.

public class DebugManager : MonoBehaviour
{
	public static void Log(string format, params object[] args)
	{
//		Debug.Log( string.Format( format, args ) );
	}
	
	public static void Log(string message)
	{
//		Debug.Log( message );
	}
}
