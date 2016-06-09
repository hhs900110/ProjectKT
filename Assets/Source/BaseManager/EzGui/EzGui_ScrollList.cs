using UnityEngine;
using System.Collections;

public class EzGui_ScrollList : EzGui_Base
{
	private float ViewSizeX;
	private float ViewSizeY;
	
	private UIScrollList ScrollList;
	
	
#region ObjectBase
	public override void Create()
	{
		base.Create();
		
		ScrollList	= null;
		ViewSizeX	= 0.0f;
		ViewSizeY	= 0.0f;
	}
	
	public override void SetValid(bool IsValid)
	{
		base.SetValid(IsValid);
		
		ScrollList.transform.gameObject.SetActiveRecursively(IsValid);
	}
	
	public override void Release()
	{
		base.Release();
		if (ScrollList)
		{
			ScrollList.DidClick(null);
			ScrollList.ClearList(true);
			DestroyImmediate(ScrollList);
		}
	}
#endregion
	
#region EzGui_Base
	public override void SetPos (float ObjectPosX, float ObjectPosY)
	{
		base.SetPos (ObjectPosX, ObjectPosY);
		transform.position = Pos;
	}
	
	public override void SetPos (float ObjectPosX, float ObjectPosY, float ObjectPosZ)
	{
		base.SetPos (ObjectPosX, ObjectPosY, ObjectPosZ);
		transform.position = Pos;
	}
	
	public override void SetLayer (float ObjectLayer)
	{
		base.SetLayer (ObjectLayer);
		transform.position = Pos;
	}
#endregion
	
	//////////////////////////////////////////////////
	
	public void SetEZGUIScrollList(UIScrollList ObjectScrollList)
	{
		ScrollList	= ObjectScrollList;
		ScrollList.transform.position	= new Vector3(0,0,0);
		
		SetValid(false);
	}
	
	public UIScrollList GetEZGUIScrollList()		{ return ScrollList; }
	
	public void SetValueChangedDelegate(EZValueChangedDelegate del)	{ ScrollList.SetValueChangedDelegate(del); }
	public void SetscriptWithMethodToInvoke(MonoBehaviour _Script)	{ ScrollList.scriptWithMethodToInvoke	= _Script; }
	public void SetmethodToInvokeOnSelect(string _Method)			{ ScrollList.methodToInvokeOnSelect		= _Method; }
	public void SetcontrolIsEnabled(bool IsEnabled)					{ ScrollList.controlIsEnabled			= IsEnabled; }
	public bool GetcontrolIsEnabled()								{ return ScrollList.controlIsEnabled; }
	
	public bool IsTextureHit()
	{
		RaycastHit[] hits;
		Camera UICamera = GameObject.Find("UICamera").GetComponent<Camera>();
		hits = Physics.RaycastAll(UICamera.ScreenPointToRay(new Vector3(Main.input.GetPos().x, Main.input.GetPos().y, 0.0f)), 1000);
		for (int i = 0; i < hits.Length; i++)
		{
			RaycastHit hit = hits[i];
			if(hit.collider.transform.parent)
			{
				if(hit.collider.transform.parent.gameObject.name == "ResourceObject_EZGUISCROLLLIST")
				{
					return true;
				}
			}
		}
		return false;
	}
	
	public IUIListObject CreateItem(GameObject ObjectItem)
	{
		//ObjectItem.transform.position	=	new Vector3(PosX,PosY,PosZ);
		return ScrollList.CreateItem(ObjectItem);
	}
	
	public IUIListObject CreateItem(GameObject ObjectItem, object ObjectData)
	{
		//ScrollList.Data		=	ObjectData;
		//Debug.Log(ScrollList.Data);
		return ScrollList.CreateItem(ObjectItem);
	}
	
	public void CreateItem(GameObject ObjectItem, int Index)
	{
		UIListItemContainer	List = (UIListItemContainer) ScrollList.CreateItem(ObjectItem, Index);
		List.Data = Index;
	}
	
	public void CreateItem(GameObject ObjectItem, int Index, GameObject Image, GameObject Name)
	{
		UIListItemContainer	List = (UIListItemContainer) ScrollList.CreateItem(ObjectItem, Index);
		List.Data = Index;		
		List.MakeChild(Image);
	}
	
	public void AddItem(GameObject ObjectItem, int Index, GameObject Image, GameObject Name)
	{
		Image	= (GameObject)Instantiate(Image);
		Name	= (GameObject)Instantiate(Name);
		Image.transform.position	= new Vector3(50,-115,0);
		Image.GetComponent<UIButton>().SetAnchor(UIButton.ANCHOR_METHOD.BOTTOM_CENTER);
		Image.GetComponent<UIButton>().width	= Image.GetComponent<UIButton>().ImageSize.x;
		Image.GetComponent<UIButton>().height	= Image.GetComponent<UIButton>().ImageSize.y;
		Image.layer = LayerMask.NameToLayer("GUI");
		
		Name.transform.position		= new Vector3(90,-5,0);
		Name.GetComponent<SpriteText>().Text	= "Name";
		Name.GetComponent<SpriteText>().SetAnchor(SpriteText.Anchor_Pos.Upper_Center);
		
		ObjectItem	= (GameObject)Instantiate(ObjectItem);
		
		ObjectItem.GetComponent<UIListItemContainer>().MakeChild(Image);
		ObjectItem.GetComponent<UIListItemContainer>().MakeChild(Name);
		ObjectItem.GetComponent<UIListItemContainer>().Data	= Index;
		ScrollList.AddItem(ObjectItem);
	}
	
	public void AddItem(GameObject ObjectItem)
	{
		ScrollList.AddItem(ObjectItem);
	}
	
	public void InsertItem(GameObject ObjectItem, int Position)
	{
		//ScrollList.InsertItem(
		ScrollList.InsertItem(ObjectItem.GetComponent<UIListItemContainer>(), Position);
	}
	
	public float GetData()
	{
		return ScrollList.ScrollPosition;
	}
	
//	public object GetData()
//	{
//		//return ScrollList.Data;
//	}
	
	public void SetOrientation(string ObjectOrientation)
	{
		if(ObjectOrientation == "HORIZONTAL")
		{
			ScrollList.orientation	= UIScrollList.ORIENTATION.HORIZONTAL;
		}
		else if(ObjectOrientation == "VERTICAL")
		{
			ScrollList.orientation	= UIScrollList.ORIENTATION.VERTICAL;
		}
	}

	public void SetOrientation(UIScrollList.ORIENTATION ObjectOrientation)
	{
		ScrollList.orientation	=	ObjectOrientation;
	}
	
	public void SetOrientation(int ObjectOrientation)
	{
		ScrollList.orientation	=	(UIScrollList.ORIENTATION)ObjectOrientation;
	}
		
	public void SetDirection(string ObjectDirection)
	{
		if(ObjectDirection == "BtoT_RtoL")
		{
			ScrollList.direction	= UIScrollList.DIRECTION.BtoT_RtoL;
		}
		else if(ObjectDirection == "TtoB_LtoR")
		{
			ScrollList.direction	= UIScrollList.DIRECTION.TtoB_LtoR;
		}
	}
	
	public void SetDirection(UIScrollList.DIRECTION ObjectDirection)
	{
		ScrollList.direction	= ObjectDirection;
	}
	
	public void SetDirection(int ObjectDirection)
	{
		ScrollList.direction	= (UIScrollList.DIRECTION)ObjectDirection;
	}
	
	public void SetAlignment(string ObjectAlignment)
	{
		if(ObjectAlignment == "Center")
		{
			ScrollList.alignment	= UIScrollList.ALIGNMENT.CENTER;
		}
		else if(ObjectAlignment == "Left_Top")
		{
			ScrollList.alignment	= UIScrollList.ALIGNMENT.LEFT_TOP;
		}
		else if(ObjectAlignment == "Right_Bottom")
		{
			ScrollList.alignment	= UIScrollList.ALIGNMENT.RIGHT_BOTTOM;
		}
	}
	
	public void SetAlignment(UIScrollList.ALIGNMENT ObjectAlignment)
	{
		ScrollList.alignment	= ObjectAlignment;
	}
	
	public void SetAlignment(int ObjectAlignment)
	{
		ScrollList.alignment	= (UIScrollList.ALIGNMENT)ObjectAlignment;
	}

	public void SetScrollListViewSize(float SizeX, float SizeY)
	{
		ScrollList.viewableArea.x	= SizeX;
		ScrollList.viewableArea.y	= SizeY;
		ViewSizeX	= SizeX;
		ViewSizeY	= SizeY;
	}
	
	public float GetSetScrollListViewSizeX()	{ return ViewSizeX; }
	public float GetSetScrollListViewSizeY()	{ return ViewSizeY; }
}