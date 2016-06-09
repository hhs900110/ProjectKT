using UnityEngine;
using System.Collections;
#pragma warning disable 0168 // variable declared but not used.
#pragma warning disable 0219 // variable assigned but not used.
#pragma warning disable 0414 // private field assigned but not used.
public class ObjectMesh : MonoBehaviour
{
	private bool	Valid;
	private string	Name;
	private Vector4	Color;
	private string	CollidName;
	private bool	IsCollision;
	// Add by FruitBingsu
	private Animation[] AnimationList;
	//
	public void Create()
	{
		Valid		= false;
		Name		= null;
		CollidName	= null;
		IsCollision	= false;
		// Add by FruitBingsu
		AnimationList = GetComponentsInChildren<Animation>();
		// if this script was copied, those variable not have value. maybe......
	}
	
	// Edit by FruitBingsu
	public void SetAnimList(Animation[] ObjectAnimList)
	{
		AnimationList = ObjectAnimList;
	}
	
	public void SetAnimMode(WrapMode ObjectMode)
	{
		Transform[] trans;
		trans = transform.GetComponentsInChildren<Transform>();
		foreach (Transform ObjectTrans in trans)
		{
			if (ObjectTrans.GetComponent<Animation>())
			{
				ObjectTrans.GetComponent<Animation>().wrapMode = ObjectMode;
				break;
			}
		}
	}
	
	public void SetPlayAutomatiCally(bool IsValid)
	{
		Transform[] trans;
		trans = transform.GetComponentsInChildren<Transform>();
		foreach (Transform ObjectTrans in trans)
		{
			if (ObjectTrans.GetComponent<Animation>())
			{
				ObjectTrans.GetComponent<Animation>().playAutomatically = IsValid;
				break;
			}
		}
	}
	
	public void SetPlayAnim()
	{
		Transform[] trans;
		trans = transform.GetComponentsInChildren<Transform>();
		foreach (Transform ObjectTrans in trans)
		{
			if (ObjectTrans.GetComponent<Animation>())
			{
				ObjectTrans.GetComponent<Animation>().Play();
				break;
			}
		}
	}
	
	public void SetPlayAnim(string ObjectAnimName)
	{
		Transform[] trans;
		trans = transform.GetComponentsInChildren<Transform>();
		foreach (Transform ObjectTrans in trans)
		{
			if (ObjectTrans.GetComponent<Animation>())
			{
				ObjectTrans.GetComponent<Animation>().Play(ObjectAnimName);
				break;
			}
		}
	}
	
	public void SetChangeAnim(string ObjectAnimName)
	{
		Transform[] trans;
		trans = transform.GetComponentsInChildren<Transform>();
		foreach (Transform ObjectTrans in trans)
		{
			if (ObjectTrans.GetComponent<Animation>())
			{
				if(ObjectTrans.GetComponent<Animation>()[ObjectAnimName])
				{
					ObjectTrans.GetComponent<Animation>().Play(ObjectAnimName);
					break;
				}
			}
		}
	}
	
	public void SetStopAnim(string ObjectAnimName)
	{
		Transform[] trans;
		trans = transform.GetComponentsInChildren<Transform>();
		foreach (Transform ObjectTrans in trans)
		{
			if (ObjectTrans.GetComponent<Animation>())
			{
				if(ObjectTrans.GetComponent<Animation>()[ObjectAnimName])
				{
					ObjectTrans.GetComponent<Animation>().Stop(ObjectAnimName);
					break;
				}
			}
		}
	}
	
	public void SetStopAnim()
	{
		Transform[] trans;
		trans = transform.GetComponentsInChildren<Transform>();
		foreach (Transform ObjectTrans in trans)
		{
			if (ObjectTrans.GetComponent<Animation>())
			{
				ObjectTrans.GetComponent<Animation>().Stop();
				break;
			}
		}
	}
	
	public bool GetAnimPlaying()
	{
		Transform[] trans;
		trans = transform.GetComponentsInChildren<Transform>();
		foreach (Transform ObjectTrans in trans)
		{
			if (ObjectTrans.GetComponent<Animation>())
			{
				return ObjectTrans.GetComponent<Animation>().isPlaying;
			}
		}
		return false;
	}
	
	public void SetName(string ObjectName)
	{
		Name = ObjectName;
	}
	
	public string GetName()
	{
		return Name;
	}
	
	public Transform[] GetChildren()
	{
		Transform[] trans;
		trans = transform.GetComponentsInChildren<Transform>();
		return trans;
	}
	
	public void SetLocalPos(Vector3 ObjectPos)
	{
		transform.localPosition = ObjectPos;
	}
	
	public void SetPos(Vector3 ObjectPos)
	{
		transform.position = ObjectPos;
	}
	
	public Vector3 GetPos()
	{
		return transform.position;
	}
	
	public float GetPosX()
	{
		return transform.position.x;
	}
	
	public float GetPosY()
	{
		return transform.position.y;
	}
	
	public float GetPosZ()
	{
		return transform.position.z;
	}
	
	public void SetPosX(float ObjectPosX)
	{
		transform.position =  new Vector3(ObjectPosX, transform.position.y, transform.position.z);
	}
	
	public void SetPosY(float ObjectPosY)
	{
		transform.position = new Vector3(transform.position.x, ObjectPosY, transform.position.z);
	}
	
	public void SetPosZ(float ObjectPosZ)
	{
		transform.position = new Vector3(transform.position.x, transform.position.y, ObjectPosZ);
	}
	
	public void SetRotate(Vector3 ObjectRotate)
	{
		transform.eulerAngles = ObjectRotate;
	}
	
	public void SetRotate(float RotateX, float RotateY, float RotateZ)
	{
		transform.eulerAngles = new Vector3(RotateX, RotateY, RotateZ);
	}
	
	public void AddRotate(Vector3 ObjectRotate)
	{
		transform.Rotate(ObjectRotate, Space.World);
	}
	
	public void AddRotate(float RotateX, float RotateY, float RotateZ)
	{
		transform.Rotate(RotateX, RotateY, RotateZ, Space.World);
	}
	
	public void SetLocalRotate(Vector3 ObjectRotate)
	{
		transform.localEulerAngles = ObjectRotate;
	}
	
	public void SetLocalRotate(float RotateX, float RotateY, float RotateZ)
	{
		transform.localEulerAngles = new Vector3 (RotateX, RotateY, RotateZ);		
	}
	
	public void SetColor(float r, float g, float b, float a)
	{
		Color.x = r;
		Color.y = g;
		Color.z = b;
		Color.w = a;
	}
	
	public Vector4 GetColor()
	{
		return Color;
	}
	
	public float GetRotateX()
	{
		return transform.eulerAngles.x;
	}
	
	public float GetRotateY()
	{
		return transform.eulerAngles.y;
	}
	public float GetRotateZ()
	{
		return transform.eulerAngles.z;
	}
	
	public Vector4 GetRotate()
	{
		return transform.eulerAngles;
	}
	
	public void SetScale(float ObjectScaleX, float ObjectScaleY, float ObjectScaleZ)
	{
		transform.localScale = new Vector3(ObjectScaleX, ObjectScaleY, ObjectScaleZ);
	}
	
	public void SetScale(Vector3 ObjectScale)
	{
		transform.localScale = ObjectScale;
	}
	
	public void SetScaleX(float ObjectScaleX)
	{
		transform.localScale = new Vector3(ObjectScaleX, transform.localScale.y, transform.localScale.z);
	}
	
	public void SetScaleY(float ObjectScaleY)
	{
		transform.localScale = new Vector3(transform.localScale.x, ObjectScaleY, transform.localScale.z);
	}
	
	public void SetScaleZ(float ObjectScaleZ)
	{
		transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, ObjectScaleZ);
	}
	
	public Vector3 GetScale()
	{
		return transform.localScale;
	}
	
	public Vector3 GetSizeCenter()
	{
		return GetComponent<Renderer>().bounds.center;
	}
	
	public Vector3 GetSizeMax()
	{
		return GetComponent<Renderer>().bounds.max;
	}
	
	public Vector3 GetSizeMin()
	{
		return GetComponent<Renderer>().bounds.min;
	}
	
	public string GetCollidName()
	{
		return CollidName;
	}
	
	public void ResetCollision()
	{
		IsCollision = false;
	}
	
	public void ResetCollidName()
	{
		CollidName = null;
	}
	
	public void OnTriggerEnter(Collider myTrigger)
	{
		CollidName = null;
		CollidName = myTrigger.gameObject.name;
	}
	
	public void OnTriggerStay(Collider myTrigger)
	{
		IsCollision = true;
		CollidName = myTrigger.gameObject.name;
	}
	
	public void OnTriggerExit(Collider myTrigger)
	{
		IsCollision = false;
		CollidName = null;
	}
	
	public bool GetCollision()
	{
		return IsCollision;
	}
	
	public void SetValid(bool IsValid)
	{
		enabled = IsValid;
		Valid = IsValid;
	}
	
	public bool GetValid()
	{
		return Valid;
	}
}