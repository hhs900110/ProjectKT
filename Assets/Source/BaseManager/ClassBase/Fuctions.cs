using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DefineBase;

public abstract class Fuctions
{
	public static void SetNumberList(int _ComputeNum, ref List<int> _NumList)
	{
		if(_NumList == null)	{ _NumList = new List<int>(); }
		
		_NumList.Clear();
		
		char[] curCompute	= _ComputeNum.ToString().ToCharArray();
		
		for(int i = 0; i < curCompute.Length; i++)
		{
			_NumList.Add(int.Parse(curCompute[i].ToString()));
		}
	}
}