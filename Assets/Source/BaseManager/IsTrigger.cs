using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DefineBase;

public class IsTrigger : MonoBehaviour
{
	private Dictionary<int, Collider> m_tableTrigger;
	
	private bool IsCollision;
	private bool Valid;
	
	public void Create()
	{
		m_tableTrigger	= new Dictionary<int, Collider>();
		
		if(!GetComponent<Rigidbody>())
		{
			gameObject.AddComponent<Rigidbody>();
			GetComponent<Rigidbody>().useGravity	= false;
		}
		
		IsCollision		= false;
		SetValid(true);
	}
	
	public void OnTriggerEnter(Collider myTrigger)
	{
		if(Valid)
		{
			if(m_tableTrigger == null)	{ m_tableTrigger = new Dictionary<int, Collider>(); }
			if(!m_tableTrigger.ContainsKey(myTrigger.GetInstanceID()))
			{
				m_tableTrigger.Add(myTrigger.GetInstanceID(), myTrigger);
			}
			IsCollision = true;
		}
	}
	
//	public void OnTriggerStay(Collider myTrigger)
//	{
//		if(Valid)
//		{
//			if(!m_tableTrigger.ContainsKey(myTrigger.GetInstanceID()))
//			{
//				m_tableTrigger.Add(myTrigger.GetInstanceID(), myTrigger);
//			}
//			IsCollision = true;
//		}
//	}
	
	public void OnTriggerExit(Collider myTrigger)
	{
//		if(Valid)
		{
			if(m_tableTrigger == null)	{ m_tableTrigger = new Dictionary<int, Collider>(); }
			if(m_tableTrigger.ContainsKey(myTrigger.GetInstanceID()))
			{
				m_tableTrigger.Remove(myTrigger.GetInstanceID());
			}
			IsCollision = false;
		}
	}
	
	public Collider[] GetTriggerObject()
	{
		CheckNull();
		Collider[] MyTrigger	= new Collider[m_tableTrigger.Count];
		
		int count	= 0;
		foreach( KeyValuePair<int, Collider> data in m_tableTrigger )
		{
			MyTrigger[count]	= data.Value;
			count++;
		}
		return MyTrigger;
	}
	
	public void ResetCollision()
	{
		IsCollision = false;
	}

	public bool GetCollision()
	{
		return IsCollision;
	}
	
	private void CheckNull()
	{
		if(m_tableTrigger == null)	{ m_tableTrigger = new Dictionary<int, Collider>(); }
		
		foreach( KeyValuePair<int, Collider> data in m_tableTrigger )
		{
			if(data.Value == null)
			{
				m_tableTrigger.Remove(data.Key);
			}
		}
	}

	public void SetValid(bool IsValid)
	{
		enabled = IsValid;
		Valid = IsValid;
		if(!IsValid)
		{
			IsCollision = false;
		}
	}

	public bool GetValid()
	{
		return Valid;
	}
}