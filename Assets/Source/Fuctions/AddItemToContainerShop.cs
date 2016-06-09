using UnityEngine;
using System.Collections;
using DefineBase;
using System;

#pragma warning disable 0168 // variable declared but not used.
#pragma warning disable 0219 // variable assigned but not used.
#pragma warning disable 0414 // private field assigned but not used.

public abstract class AddItemToContainerShop
{
	// ChildName
	public static	String m_strSlotTitle			= "SlotTitle_";
	public static	String m_strSlotImage			= "SlotImage_";
	public static	String m_strCubeSlotFont		= "CubeSlotFont_";
	public static	String m_strGetCubeSlotFont		= "GetCubeSlotFont_";
	public static	String m_strTimeSlotFont		= "TimeSlotFont_";
	public static	String m_strLimitTimeSlotFont	= "LimitTimeSlotFont_";
	public static	String m_strExpSlotFont			= "ExpSlotFont_";
	public static	String m_strLevelSlotFont		= "LevelSlotFont_";
	public static	String m_strAPSlotBase			= "APSlotBase_";
	public static	String m_strAPSlotFont			= "APSlotFont_";
	public static	String m_strAPSlotIcon			= "APSlotIcon_";
	public static	String m_strAPSlotVari			= "APSlotVari_";
	public static	String m_strSlotColorImage		= "SlotColorImage_";
	
	public static	String m_strSlotDeleteTime		= "SlotDeleteTime";
	public static	String m_strSlotInventoryDeleteMent	= "SlotInventoryDeleteMent";
	
	
	
	// Layer (2 ~ 9)
	public static	Vector3	m_posTitleFont					= new Vector3( 156,   -9, -5);
	public static	Vector3	m_posShopImage					= new Vector3(  90, -220, -4);
	public static	Vector3	m_posShopImage_MiddleCenter		= new Vector3(  90, -140, -4);
	public static	Vector3	m_posColorImage					= new Vector3( 265, -168, -4);
	
	public static	Vector3	m_posPresentBuildingImage		= new Vector3(  95, -210, -3);
	public static	Vector3	m_posInvenImage					= new Vector3( 155, -210, -3);
	
	public static	Vector3	m_posItemNum					= new Vector3( 180, -200, -5);
	public static	Vector3	m_posInvenItemNum				= new Vector3( 200, -150, -5);
	public static	Vector3	m_posInvenAccesoryNum			= new Vector3( 200, -150, -4);
	
	public static	Vector3	m_posSlotBase					= new Vector3( 190,  -76, -4);
	public static	Vector3	m_posSlotGab					= new Vector3(   0,  -36,  0);
	public static	Vector3	m_posAddSlotFont				= new Vector3(  98,  -14, -1);
	public static	Vector3	m_posAddSlotIcon				= new Vector3(   0,  -14, -1);
	public static	Vector3	m_posAddSlotVari				= new Vector3( -10,    0, -2);
	
	public static	Vector3	m_posNewMark					= new Vector3(  -2,    2, -6);
	public static	Vector3	m_posRainbowMark				= new Vector3( 229,  -30, -6);
	public static	Vector3	m_posRockImage					= new Vector3(  65,  -50, -12);
	public static	Vector3	m_posRockMent					= new Vector3(  65,  -83, -13);
	
	public static	Vector2	m_sizeSlotImageMax				= new Vector2( 160,  160);
	
	public static	Vector3	m_posDeleteTimeFont				= new Vector3( 156.0f,  -150.0f, -10.0f);
	public static	Vector3	m_posDeleteTimeMentFont			= new Vector3( 156.0f,   -60.0f, -10.0f);
	
	public static	Vector3 m_posCompMark					= new Vector3( 155.0f,   -80.0f, -11.0f);
	
	
	public static void AddNameToContainer(GameObject _objContainer, int _dSlotIndex, string _strSlotTitle)
	{
		GameObject SlotTitle				= new GameObject(m_strSlotTitle + _dSlotIndex);
		ResourceLoad.SetSpriteText_OutLine(SlotTitle, ScrollData.objSpriteText30, ScrollData.objSpriteText_stroke30);
		
		SlotTitle.GetComponent<EzGui_SpriteText_Outline>().SetColor(ScrollData.colorShopSlotTitleFront, ScrollData.colorShopSlotTitleBack);
		SlotTitle.GetComponent<EzGui_SpriteText_Outline>().SetAnchor(SpriteText.Anchor_Pos.Upper_Center);
		SlotTitle.GetComponent<EzGui_SpriteText_Outline>().SetAllignment(SpriteText.Alignment_Type.Center);
		SlotTitle.GetComponent<EzGui_SpriteText_Outline>().SetText(_strSlotTitle);
		
//		ResourceLoad.SetSpriteText(SlotTitle, ScrollData.objSpriteText30);
//		
//		SlotTitle.GetComponent<EzGui_SpriteText>().SetColor(ScrollData.colorShopSlotTitle);
//		SlotTitle.GetComponent<EzGui_SpriteText>().SetAnchor(SpriteText.Anchor_Pos.Upper_Center);
//		SlotTitle.GetComponent<EzGui_SpriteText>().SetAllignment(SpriteText.Alignment_Type.Center);
//		SlotTitle.GetComponent<EzGui_SpriteText>().SetText(_strSlotTitle);
		
		SlotTitle.transform.localPosition	= m_posTitleFont;
		_objContainer.GetComponent<UIListItemContainer>().MakeChild(SlotTitle);
	}
	
#region AddImage
	public static void AddImageToContainer(GameObject _objContainer, int _dSlotIndex, GameObject _objImage, float _fImagePercentage)
	{
		GameObject SlotImage				= ResourceLoad.GetEzGuiTexture(_objImage);
		if(SlotImage == null)
		{
			return ;
		}
		SlotImage.name						= m_strSlotImage + _dSlotIndex;
		SetSlotImage_Bottom_Center(SlotImage, _fImagePercentage);
		
		SlotImage.transform.localPosition	= m_posShopImage;
		_objContainer.GetComponent<UIListItemContainer>().MakeChild(SlotImage);
	}
	
	public static void AddImageToContainer_MiddleCenter(GameObject _objContainer, int _dSlotIndex, GameObject _objImage, float _fImagePercentage)
	{
		//Add Acc
		GameObject SlotImage				= ResourceLoad.GetEzGuiTexture(_objImage);
		SlotImage.name						= m_strSlotImage + _dSlotIndex;
		SetSlotImage_Middle_Center(SlotImage, _fImagePercentage);
		
		SlotImage.transform.localPosition	= m_posShopImage_MiddleCenter;
		_objContainer.GetComponent<UIListItemContainer>().MakeChild(SlotImage);
	}
	
	public static void AddPresentFriendImageToContainer(GameObject _objContainer, int _dSlotIndex, ArrayList _GiftFriendArray, UnityEngine.Object _objImage, float _fImagePercentage)
	{
		GameObject SlotImage				= ResourceLoad.GetEzGuiTexture(_objImage);
		if(SlotImage == null)
		{
			return ;
		}
		SlotImage.name						= m_strSlotImage + _dSlotIndex;
		SetSlotImage_Bottom_Center(SlotImage, _fImagePercentage);
		_GiftFriendArray.Add(SlotImage);
		
		SlotImage.transform.position		= m_posInvenImage;
		_objContainer.GetComponent<UIListItemContainer>().MakeChild(SlotImage);
	}
	
	public static void AddPresentImageToContainer(GameObject _objContainer, int _dSlotIndex, GameObject _objTmpImage, float _fImagePercentage)
	{
		GameObject _objImage = (GameObject)UnityEngine.Object.Instantiate(_objTmpImage);
		_objTmpImage.SetActiveRecursively(false);
		_objTmpImage.GetComponent<MeshRenderer>().enabled = false;
		
		GameObject SlotImage				= new GameObject(m_strSlotImage + _dSlotIndex);
		SetSlotImage_Bottom_Center(SlotImage, _objImage, _fImagePercentage);
		
		SlotImage.transform.position		= m_posInvenImage;
		_objContainer.GetComponent<UIListItemContainer>().MakeChild(SlotImage);
	}
	
	public static void AddInvenImageToContainer(GameObject _objContainer, int _dSlotIndex, GameObject _objImage, float _fImagePercentage)
	{
		GameObject SlotImage				= ResourceLoad.GetEzGuiTexture(_objImage);
		if(SlotImage == null)
		{
			return ;
		}
		SlotImage.name						= m_strSlotImage + _dSlotIndex;
		SetSlotImage_Bottom_Center(SlotImage, _fImagePercentage);
		
		SlotImage.transform.position		= m_posInvenImage;
		_objContainer.GetComponent<UIListItemContainer>().MakeChild(SlotImage);
	}
	
	public static void SetSlotImage(GameObject _objSlotImage, GameObject _objImage, float _fImagePercentage)
	{
		GameObject ObjImage	= (GameObject)GameObject.Instantiate(_objImage, Vector3.zero, Quaternion.identity);
		_objImage.SetActiveRecursively(false);
		_objImage.GetComponent<MeshRenderer>().enabled = false;
		
		float SizeX	= ObjImage.GetComponent<UIButton>().ImageSize.x;
		float SizeY	= ObjImage.GetComponent<UIButton>().ImageSize.y;
		float Ratio	= GetImageRatio(SizeX, SizeY, _fImagePercentage);
		ObjImage.GetComponent<UIButton>().SetSize(SizeX * _fImagePercentage * Ratio, SizeY * _fImagePercentage * Ratio);
		ObjImage.transform.position	= Vector3.zero;
		ObjImage.transform.parent	= _objSlotImage.transform;
	}
	
	public static void SetSlotImage_Bottom_Center(GameObject _objSlotImage, GameObject _objImage, float _fImagePercentage)
	{
		GameObject ObjImage	= (GameObject)GameObject.Instantiate(_objImage, Vector3.zero, Quaternion.identity);
		_objImage.SetActiveRecursively(false);
		_objImage.GetComponent<MeshRenderer>().enabled = false;
		
		float SizeX	= ObjImage.GetComponent<UIButton>().ImageSize.x;
		float SizeY	= ObjImage.GetComponent<UIButton>().ImageSize.y;
		float Ratio	= GetImageRatio(SizeX, SizeY, _fImagePercentage);
		ObjImage.GetComponent<UIButton>().SetSize(SizeX * _fImagePercentage * Ratio, SizeY * _fImagePercentage * Ratio);
		ObjImage.GetComponent<UIButton>().SetAnchor(UIButton.ANCHOR_METHOD.BOTTOM_CENTER);
		ObjImage.transform.position	= Vector3.zero;
		ObjImage.transform.parent	= _objSlotImage.transform;
	}
	
	public static void SetSlotImage_Bottom_Center(GameObject _objSlotImage, float _fImagePercentage)
	{
		float SizeX	= _objSlotImage.GetComponent<UIButton>().ImageSize.x;
		float SizeY	= _objSlotImage.GetComponent<UIButton>().ImageSize.y;
		float Ratio	= GetImageRatio(SizeX, SizeY, _fImagePercentage);
		_objSlotImage.GetComponent<UIButton>().SetSize(SizeX * _fImagePercentage * Ratio, SizeY * _fImagePercentage * Ratio);
		_objSlotImage.GetComponent<UIButton>().SetAnchor(UIButton.ANCHOR_METHOD.BOTTOM_CENTER);
		_objSlotImage.transform.position	= Vector3.zero;
	}
	
	public static void SetSlotImage_Middle_Center(GameObject _objSlotImage, GameObject _objImage, float _fImagePercentage)
	{
		GameObject ObjImage	= (GameObject)GameObject.Instantiate(_objImage, Vector3.zero, Quaternion.identity);
		_objImage.SetActiveRecursively(false);
		_objImage.GetComponent<MeshRenderer>().enabled = false;
		
		float SizeX	= ObjImage.GetComponent<UIButton>().ImageSize.x;
		float SizeY	= ObjImage.GetComponent<UIButton>().ImageSize.y;
		float Ratio	= GetImageRatio(SizeX, SizeY, _fImagePercentage);
		ObjImage.GetComponent<UIButton>().SetSize(SizeX * _fImagePercentage * Ratio, SizeY * _fImagePercentage * Ratio);
		ObjImage.GetComponent<UIButton>().SetAnchor(UIButton.ANCHOR_METHOD.MIDDLE_CENTER);
		ObjImage.transform.position	= Vector3.zero;
		ObjImage.transform.parent	= _objSlotImage.transform;
	}
	
	public static void SetSlotImage_Middle_Center(GameObject _objSlotImage, float _fImagePercentage)
	{
		float SizeX	= _objSlotImage.GetComponent<UIButton>().ImageSize.x;
		float SizeY	= _objSlotImage.GetComponent<UIButton>().ImageSize.y;
		float Ratio	= GetImageRatio(SizeX, SizeY, _fImagePercentage);
		_objSlotImage.GetComponent<UIButton>().SetSize(SizeX * _fImagePercentage * Ratio, SizeY * _fImagePercentage * Ratio);
		_objSlotImage.GetComponent<UIButton>().SetAnchor(UIButton.ANCHOR_METHOD.MIDDLE_CENTER);
		_objSlotImage.transform.position	= Vector3.zero;
	}
#endregion
	
	public static void AddColorToContainer(GameObject p_objContainer, int p_dSlotIndex, GameObject p_objImage, float p_fImagePercentage)
	{
		//Add Acc
		GameObject SlotImage = new GameObject(m_strSlotColorImage + p_dSlotIndex);
		SetSlotColorImage(SlotImage, p_objImage, p_fImagePercentage);
		
		SlotImage.transform.localPosition	= m_posColorImage;
		p_objContainer.GetComponent<UIListItemContainer>().MakeChild(SlotImage);
	}

	public static void SetSlotColorImage(GameObject SlotImage, GameObject p_objImage, float p_fImagePercentage)
	{
		float SizeX	= p_objImage.GetComponent<UIButton>().ImageSize.x;
		float SizeY	= p_objImage.GetComponent<UIButton>().ImageSize.y / 2;
		float Ratio	= GetImageRatio(SizeX, SizeY, p_fImagePercentage);
		p_objImage.GetComponent<UIButton>().SetSize(SizeX * p_fImagePercentage * Ratio, SizeY * p_fImagePercentage * Ratio);
		p_objImage.GetComponent<UIButton>().SetAnchor(UIButton.ANCHOR_METHOD.BOTTOM_CENTER);
		p_objImage.transform.position	= Vector3.zero;
		p_objImage.transform.parent		= SlotImage.transform;
	}
	
#region AddCube
	
	public static void AddUseCubeToContainer(GameObject _objContainer, int _dSlotIndex, int _dSlotBaseIndex, string _strInitNum)
	{
		GameObject SlotBase	= ResourceLoad.GetEzGuiTexture(ScrollData.objShopSlotNumPad);
		GameObject SlotFont	= ResourceLoad.GetSpriteText(ScrollData.objSpriteText22);
		GameObject SlotIcon	= ResourceLoad.GetEzGuiTexture(ScrollData.objIconCube_39_40);
		GameObject SlotVari	= ResourceLoad.GetEzGuiTexture(ScrollData.objIconMinus_20_21);
		SlotBase.name		= "UseCube" + "SlotBase_"+_dSlotIndex;
		SlotFont.name		= m_strCubeSlotFont+_dSlotIndex;
		SlotIcon.name		= "UseCube" + "SlotIcon"+_dSlotIndex;
		SlotVari.name		= "UseCube" + "SlotVari"+_dSlotIndex;
		
		SlotFont.GetComponent<EzGui_SpriteText>().SetText(_strInitNum);
		SetSlotPos(_dSlotBaseIndex, SlotBase, SlotFont, SlotIcon, SlotVari);
		
		_objContainer.GetComponent<UIListItemContainer>().MakeChild(SlotBase);
	}
	
	public static void AddUseRainCubeToContainer(GameObject _objContainer, int _dSlotIndex, int _dSlotBaseIndex, string _strInitNum)
	{
		GameObject SlotBase	= ResourceLoad.GetEzGuiTexture(ScrollData.objShopSlotNumPad);
		GameObject SlotFont	= ResourceLoad.GetSpriteText(ScrollData.objSpriteText22);
		GameObject SlotIcon	= ResourceLoad.GetEzGuiTexture(ScrollData.objIconRainbowCube_39_40);
		GameObject SlotVari	= ResourceLoad.GetEzGuiTexture(ScrollData.objIconMinus_20_21);
		SlotBase.name		= "UseRainCube" + "SlotBase_"+_dSlotIndex;
		SlotFont.name		= m_strCubeSlotFont+_dSlotIndex;
		SlotIcon.name		= "UseRainCube" + "SlotIcon"+_dSlotIndex;
		SlotVari.name		= "UseRainCube" + "SlotVari"+_dSlotIndex;
		
		SlotFont.GetComponent<EzGui_SpriteText>().SetText(_strInitNum);
		SetSlotPos(_dSlotBaseIndex, SlotBase, SlotFont, SlotIcon, SlotVari);
		
		_objContainer.GetComponent<UIListItemContainer>().MakeChild(SlotBase);
	}
	
	public static void AddUseBuddyCubeToContainer(GameObject _objContainer, int _dSlotIndex, int _dSlotBaseIndex, string _strInitNum)
	{
		GameObject SlotBase	= ResourceLoad.GetEzGuiTexture(ScrollData.objShopSlotNumPad);
		GameObject SlotFont	= ResourceLoad.GetSpriteText(ScrollData.objSpriteText22);
		GameObject SlotIcon	= ResourceLoad.GetEzGuiTexture(ScrollData.objIconBuddyCube_40_40);
		GameObject SlotVari	= ResourceLoad.GetEzGuiTexture(ScrollData.objIconMinus_20_21);
		SlotBase.name		= "UseBuddyCube" + "SlotBase_"+_dSlotIndex;
		SlotFont.name		= m_strCubeSlotFont+_dSlotIndex;
		SlotIcon.name		= "UseBuddyCube" + "SlotIcon"+_dSlotIndex;
		SlotVari.name		= "UseBuddyCube" + "SlotVari"+_dSlotIndex;
		
		SlotFont.GetComponent<EzGui_SpriteText>().SetText(_strInitNum);
		SetSlotPos(_dSlotBaseIndex, SlotBase, SlotFont, SlotIcon, SlotVari);
		
		_objContainer.GetComponent<UIListItemContainer>().MakeChild(SlotBase);
	}
	
	public static void AddGetCubeToContainer(GameObject _objContainer, int _dSlotIndex, int _dSlotBaseIndex, string _strInitNum)
	{
		GameObject SlotBase	= ResourceLoad.GetEzGuiTexture(ScrollData.objShopSlotNumPad);
		GameObject SlotFont	= ResourceLoad.GetSpriteText(ScrollData.objSpriteText22);
		GameObject SlotIcon	= ResourceLoad.GetEzGuiTexture(ScrollData.objIconCube_39_40);
		GameObject SlotVari	= ResourceLoad.GetEzGuiTexture(ScrollData.objIconPlus_20_21);
		SlotBase.name		= "GetCube" + "SlotBase_"+_dSlotIndex;
		SlotFont.name		= m_strCubeSlotFont+_dSlotIndex;
		SlotIcon.name		= "GetCube" + "SlotIcon"+_dSlotIndex;
		SlotVari.name		= "GetCube" + "SlotVari"+_dSlotIndex;
		
		SlotFont.GetComponent<EzGui_SpriteText>().SetText(_strInitNum);
		SetSlotPos(_dSlotBaseIndex, SlotBase, SlotFont, SlotIcon, SlotVari);
		
		_objContainer.GetComponent<UIListItemContainer>().MakeChild(SlotBase);
	}
	
	public static void AddRealPriceToContainer(GameObject _objContainer, int _dSlotIndex, string _strInitNum)
	{
		GameObject SlotBase	= ResourceLoad.GetEzGuiTexture(ScrollData.objShopSlotNumPad);
		GameObject SlotFont	= ResourceLoad.GetSpriteText(ScrollData.objSpriteText22);
		SlotBase.name		= "RealPrice" + "SlotBase_"+_dSlotIndex;
		SlotFont.name		= m_strCubeSlotFont+_dSlotIndex;
		
		SlotFont.GetComponent<EzGui_SpriteText>().SetText(_strInitNum);
		SetSlotPos(1, SlotBase, SlotFont, null);
		
		_objContainer.GetComponent<UIListItemContainer>().MakeChild(SlotBase);
	}
#endregion
	
#region AddAccesoryPoint
	public static void AddGetAccPointToContainer(GameObject _objContainer, int _dSlotIndex, int _dSlotBaseIndex, string _strInitNum)
	{
		GameObject SlotBase	= ResourceLoad.GetEzGuiTexture(ScrollData.objShopSlotNumPad);
		GameObject SlotFont	= ResourceLoad.GetSpriteText(ScrollData.objSpriteText22);
		GameObject SlotIcon	= ResourceLoad.GetEzGuiTexture(ScrollData.objIconDwarf_39_40);
		GameObject SlotVari	= ResourceLoad.GetEzGuiTexture(ScrollData.objIconPlus_20_21);
		SlotBase.name		= m_strAPSlotBase+_dSlotIndex;
		SlotFont.name		= m_strAPSlotFont+_dSlotIndex;
		SlotIcon.name		= m_strAPSlotIcon+_dSlotIndex;
		SlotVari.name		= m_strAPSlotVari+_dSlotIndex;
		
		SlotFont.GetComponent<EzGui_SpriteText>().SetText(_strInitNum);
		SetSlotPos(_dSlotBaseIndex, SlotBase, SlotFont, SlotIcon, SlotVari);
		
		_objContainer.GetComponent<UIListItemContainer>().MakeChild(SlotBase);
	}
	
	public static void AddNeedAccPointToContainer(GameObject _objContainer, int _dSlotIndex, int _dSlotBaseIndex, string _strInitNum)
	{
		GameObject SlotBase	= ResourceLoad.GetEzGuiTexture(ScrollData.objShopSlotNumPad);
		GameObject SlotFont	= ResourceLoad.GetSpriteText(ScrollData.objSpriteText22);
		GameObject SlotIcon	= ResourceLoad.GetEzGuiTexture(ScrollData.objIconDwarf_39_40);
		GameObject SlotVari	= ResourceLoad.GetEzGuiTexture(ScrollData.objIconMinus_20_21);
		SlotBase.name		= m_strAPSlotBase+_dSlotIndex;
		SlotFont.name		= m_strAPSlotFont+_dSlotIndex;
		SlotIcon.name		= m_strAPSlotIcon+_dSlotIndex;
		SlotVari.name		= m_strAPSlotVari+_dSlotIndex;
		
		SlotFont.GetComponent<EzGui_SpriteText>().SetText(_strInitNum);
		SetSlotPos(_dSlotBaseIndex, SlotBase, SlotFont, SlotIcon, SlotVari);
		
		_objContainer.GetComponent<UIListItemContainer>().MakeChild(SlotBase);
	}
#endregion
	
#region AddSlot
	public static void AddExpToContainer(GameObject _objContainer, int _dSlotIndex, int _dSlotBaseIndex, string _strInitNum)
	{
		GameObject SlotBase	= ResourceLoad.GetEzGuiTexture(ScrollData.objShopSlotNumPad);
		GameObject SlotFont	= ResourceLoad.GetSpriteText(ScrollData.objSpriteText22);
		GameObject SlotIcon	= ResourceLoad.GetEzGuiTexture(ScrollData.objIconExp_39_40);
		GameObject SlotVari	= ResourceLoad.GetEzGuiTexture(ScrollData.objIconPlus_20_21);
		SlotBase.name		= "Exp" + "SlotBase_"+_dSlotIndex;
		SlotFont.name		= m_strExpSlotFont+_dSlotIndex;
		SlotIcon.name		= "Exp" + "SlotIcon"+_dSlotIndex;
		SlotVari.name		= "Exp" + "SlotVari"+_dSlotIndex;
		
		SlotFont.GetComponent<EzGui_SpriteText>().SetText(_strInitNum);
		SetSlotPos(_dSlotBaseIndex, SlotBase, SlotFont, SlotIcon, SlotVari);
		
		_objContainer.GetComponent<UIListItemContainer>().MakeChild(SlotBase);
	}
	
	public static void AddLevelToContainer(GameObject _objContainer, int _dSlotIndex, int _dSlotBaseIndex, string _strInitNum)
	{
		GameObject SlotBase	= ResourceLoad.GetEzGuiTexture(ScrollData.objShopSlotNumPad);
		GameObject SlotFont	= ResourceLoad.GetSpriteText(ScrollData.objSpriteText22);
		GameObject SlotIcon	= ResourceLoad.GetEzGuiTexture(ScrollData.objIconLevel_38_37);
		SlotBase.name		= "Level" + "SlotBase_"+_dSlotIndex;
		SlotFont.name		= m_strLevelSlotFont+_dSlotIndex;
		SlotIcon.name		= "Level" + "SlotIcon"+_dSlotIndex;
		
		SlotFont.GetComponent<EzGui_SpriteText>().SetText(_strInitNum);
		SetSlotPos(_dSlotBaseIndex, SlotBase, SlotFont, SlotIcon);
		
		_objContainer.GetComponent<UIListItemContainer>().MakeChild(SlotBase);
	}
	
	public static void AddTimeToContainer(GameObject _objContainer, int _dSlotIndex, int _dSlotBaseIndex, string _strInitNum)
	{
		GameObject SlotBase	= ResourceLoad.GetEzGuiTexture(ScrollData.objShopSlotNumPad);
		GameObject SlotFont	= ResourceLoad.GetSpriteText(ScrollData.objSpriteText22);
		GameObject SlotIcon	= ResourceLoad.GetEzGuiTexture(ScrollData.objIconTime_39_40);
		SlotBase.name		= "Time"+"SlotBase_"+_dSlotIndex;
		SlotFont.name		= m_strTimeSlotFont+_dSlotIndex;
		SlotIcon.name		= "Time"+"SlotIcon_"+_dSlotIndex;
		
		SlotFont.GetComponent<EzGui_SpriteText>().SetText(_strInitNum);
		SetSlotPos(_dSlotBaseIndex, SlotBase, SlotFont, SlotIcon);
		
		_objContainer.GetComponent<UIListItemContainer>().MakeChild(SlotBase);
	}
	
	public static void AddLimitTimeToContainer(GameObject _objContainer, int _dSlotIndex, int _dSlotBaseIndex, string _strInitNum)
	{
		GameObject SlotBase	= ResourceLoad.GetEzGuiTexture(ScrollData.objShopSlotNumPad);
		GameObject SlotFont	= ResourceLoad.GetSpriteText(ScrollData.objSpriteText22);
		GameObject SlotIcon	= ResourceLoad.GetEzGuiTexture(ScrollData.objIconSellTime_39_40);
		SlotBase.name		= "LimitTime"+"SlotBase_"+_dSlotIndex;
		SlotFont.name		= m_strLimitTimeSlotFont +_dSlotIndex;
		SlotIcon.name		= "LimitTime"+"SlotIcon_"+_dSlotIndex;
		
		SlotFont.GetComponent<EzGui_SpriteText>().SetText(_strInitNum);
		SetSlotPos(_dSlotBaseIndex, SlotBase, SlotFont, SlotIcon);
		
		_objContainer.GetComponent<UIListItemContainer>().MakeChild(SlotBase);
	}
#endregion
	
	public static void AddInvenNumToContainer(GameObject _objContainer, int _dSlotIndex, string _strInitNum)
	{
		GameObject CubeSlotFont				= new GameObject("InvenNum_"+_dSlotIndex);
		ResourceLoad.SetSpriteText_OutLine(CubeSlotFont, ScrollData.objSpriteText30, ScrollData.objSpriteText_stroke30);
		
		CubeSlotFont.GetComponent<EzGui_SpriteText_Outline>().SetColor(Color.white, Color.black);
		CubeSlotFont.GetComponent<EzGui_SpriteText_Outline>().SetAnchor(SpriteText.Anchor_Pos.Middle_Center);
		CubeSlotFont.GetComponent<EzGui_SpriteText_Outline>().SetAllignment(SpriteText.Alignment_Type.Center);
		CubeSlotFont.GetComponent<EzGui_SpriteText_Outline>().SetText("X"+_strInitNum);
		
		CubeSlotFont.transform.position		= m_posInvenItemNum;
		_objContainer.GetComponent<UIListItemContainer>().MakeChild(CubeSlotFont);
	}
	
	public static void AddItemNumToContainer(GameObject _objContainer, int _dSlotIndex, string _strInitNum)
	{
		GameObject CubeSlotFont				= new GameObject("ItemNum_"+_dSlotIndex);
		ResourceLoad.SetSpriteText_OutLine(CubeSlotFont, ScrollData.objSpriteText30, ScrollData.objSpriteText_stroke30);
		
		CubeSlotFont.GetComponent<EzGui_SpriteText_Outline>().SetColor(Color.white, Color.black);
		CubeSlotFont.GetComponent<EzGui_SpriteText_Outline>().SetAnchor(SpriteText.Anchor_Pos.Middle_Right);
		CubeSlotFont.GetComponent<EzGui_SpriteText_Outline>().SetAllignment(SpriteText.Alignment_Type.Center);
		CubeSlotFont.GetComponent<EzGui_SpriteText_Outline>().SetText("X"+_strInitNum);
		
		CubeSlotFont.transform.position		= m_posItemNum;
		_objContainer.GetComponent<UIListItemContainer>().MakeChild(CubeSlotFont);
	}
	
#region ETC Marks
	public static void AddRainCubeMarkToContainer(GameObject _objContainer, int _dSlotIndex)
	{
		GameObject RainbowMark	= ResourceLoad.GetEzGuiTexture(ScrollData.objShopRainbowMark);
		RainbowMark.name		= "RainbowMark_"+_dSlotIndex;
		
		RainbowMark.transform.localPosition	= m_posRainbowMark;
		_objContainer.GetComponent<UIListItemContainer>().MakeChild(RainbowMark);
	}
	
	public static void AddNewMarkToContainer(GameObject _objContainer, int _dSlotIndex)
	{
		GameObject NewMark		= ResourceLoad.GetEzGuiTexture(ScrollData.objShopIconNewMark);
		NewMark.name			= "NewMark_"+_dSlotIndex;
		
		NewMark.transform.localPosition		= m_posNewMark;
		_objContainer.GetComponent<UIListItemContainer>().MakeChild(NewMark);
	}
	
	public static void AddCompMarkToContainer(GameObject _objContainer, int _dSlotIndex, UnityEngine.Object _Prefab)
	{
		GameObject CompMark		= ResourceLoad.GetEzGuiTexture(_Prefab);
		CompMark.name			= "SlotComp";
//		CompMark.name			= "Comp_"+_dSlotIndex;
		
		CompMark.GetComponent<EzGui_Texture>().SetTextureSize(150.0f, 136.0f);
		CompMark.transform.localPosition	= m_posCompMark;
		_objContainer.GetComponent<UIListItemContainer>().MakeChild(CompMark);
	}
	
	public static void AddUpgradeMarkToContainer(GameObject _objContainer, int _dSlotIndex)
	{
		AddCompMarkToContainer(_objContainer, _dSlotIndex, ScrollData.objMarkUpgradeComp);
	}
	
	public static void AddResearchMarkToContainer(GameObject _objContainer, int _dSlotIndex)
	{
		AddCompMarkToContainer(_objContainer, _dSlotIndex, ScrollData.objMarkResearchComp);
	}
#endregion
	
	public static void AddDeleteTimeMentToContainer(GameObject _objContainer, int _dSlotIndex, string _strSlotDeleteTime)
	{
		GameObject ObjectDeleteTimeMent = new GameObject(m_strSlotInventoryDeleteMent);
		ResourceLoad.SetSpriteText_OutLine(ObjectDeleteTimeMent, ScrollData.objSpriteText24, ScrollData.objSpriteText_stroke24);
		
		ObjectDeleteTimeMent.GetComponent<EzGui_SpriteText_Outline>().SetColor(Color.white, Color.black);
		ObjectDeleteTimeMent.GetComponent<EzGui_SpriteText_Outline>().SetAnchor(SpriteText.Anchor_Pos.Middle_Center);
		ObjectDeleteTimeMent.GetComponent<EzGui_SpriteText_Outline>().SetAllignment(SpriteText.Alignment_Type.Center);
		ObjectDeleteTimeMent.GetComponent<EzGui_SpriteText_Outline>().SetText(_strSlotDeleteTime);
		
		ObjectDeleteTimeMent.transform.localPosition	= m_posDeleteTimeMentFont;
		_objContainer.GetComponent<UIListItemContainer>().MakeChild(ObjectDeleteTimeMent);
	}
	
	public static void AddDeleteTimeToContainer(GameObject _objContainer, int _dSlotIndex, string _strSlotDeleteTime)
	{
		GameObject ObjectDeleteTime = new GameObject(m_strSlotDeleteTime);
		ResourceLoad.SetSpriteText_OutLine(ObjectDeleteTime, ScrollData.objSpriteText30, ScrollData.objSpriteText_stroke30);
		
		ObjectDeleteTime.GetComponent<EzGui_SpriteText_Outline>().SetColor(Color.white, Color.black);
		ObjectDeleteTime.GetComponent<EzGui_SpriteText_Outline>().SetAnchor(SpriteText.Anchor_Pos.Middle_Center);
		ObjectDeleteTime.GetComponent<EzGui_SpriteText_Outline>().SetAllignment(SpriteText.Alignment_Type.Center);
		ObjectDeleteTime.GetComponent<EzGui_SpriteText_Outline>().SetText(_strSlotDeleteTime);
		
		ObjectDeleteTime.transform.localPosition	= m_posDeleteTimeFont;
		_objContainer.GetComponent<UIListItemContainer>().MakeChild(ObjectDeleteTime);
	}
	
	public static void AddRockImage(GameObject _objParent, int _dSlotIndex, string _strNeed)
	{
		GameObject RockImage				= ResourceLoad.GetEzGuiTexture(ScrollData.objIconRock_56_76);
		GameObject RockMent					= new GameObject();
		RockImage.name						= "RockImage_"+_dSlotIndex;
		RockMent.name						= "RockMent_" +_dSlotIndex;
		ResourceLoad.SetSpriteText_OutLine(RockMent, ScrollData.objSpriteText24, ScrollData.objSpriteText_stroke24);
		
		RockMent.GetComponent<EzGui_SpriteText_Outline>().SetColor(new Color(0.7451f, 0.0f, 0.0f), Color.white);
		RockMent.GetComponent<EzGui_SpriteText_Outline>().SetAnchor(SpriteText.Anchor_Pos.Middle_Center);
		RockMent.GetComponent<EzGui_SpriteText_Outline>().SetAllignment(SpriteText.Alignment_Type.Center);
		RockMent.GetComponent<EzGui_SpriteText_Outline>().SetText(_strNeed);
		
		if(RockImage.GetComponent<EzGui_Texture>())
		{
			RockImage.GetComponent<EzGui_Texture>().GetEZGUITexture().anchor	= SpriteRoot.ANCHOR_METHOD.MIDDLE_CENTER;
		}
//		RockImage.transform.localScale		= new Vector3(0.5f, 0.5f, 0.5f);
		
		RockImage.transform.localPosition	= m_posRockImage;
		RockMent.transform.localPosition	= m_posRockMent;
		RockImage.transform.parent			= _objParent.transform;
		RockMent.transform.parent			= _objParent.transform;
	}
	
#region SetSlotPos
	private static void SetSlotPos(int SlotNum, GameObject _objBase, GameObject _objFont, GameObject _objIcon)
	{
		if(_objBase != null)	{ _objBase.transform.position	= m_posSlotBase + m_posSlotGab*SlotNum; }
		if(_objFont != null)
		{
			if(_objFont.GetComponent<EzGui_SpriteText>())
			{
				_objFont.GetComponent<EzGui_SpriteText>().SetColor(ScrollData.colorShopSlotCube);
				_objFont.GetComponent<EzGui_SpriteText>().SetAnchor(SpriteText.Anchor_Pos.Middle_Right);
				_objFont.GetComponent<EzGui_SpriteText>().SetAllignment(SpriteText.Alignment_Type.Right);
			}
			
			if(_objBase != null)
			{
				_objFont.transform.parent			= _objBase.transform;
				_objFont.transform.localPosition	= m_posAddSlotFont;
			}
			else
			{
				_objFont.transform.position	= m_posSlotBase + m_posSlotGab*SlotNum + m_posAddSlotFont;
			}
		}
		if(_objIcon != null)
		{
			if(_objIcon.GetComponent<EzGui_Texture>())
			{
				_objIcon.GetComponent<EzGui_Texture>().GetEZGUITexture().anchor	= SpriteRoot.ANCHOR_METHOD.MIDDLE_CENTER;
			}
			if(_objIcon.GetComponent<EzGui_Button>())
			{
				_objIcon.GetComponent<EzGui_Button>().GetUIButton().anchor		= SpriteRoot.ANCHOR_METHOD.MIDDLE_CENTER;
			}
			if(_objBase != null)
			{
				_objIcon.transform.parent			= _objBase.transform;
				_objIcon.transform.localPosition	= m_posAddSlotIcon;
			}
			else
			{
				_objIcon.transform.position	= m_posSlotBase + m_posSlotGab*SlotNum + m_posAddSlotIcon;
			}
		}
	}
	
	private static void SetSlotPos(int SlotNum, GameObject _objBase, GameObject _objFont, GameObject _objIcon, GameObject _objVari)
	{
		SetSlotPos(SlotNum, _objBase, _objFont, _objIcon);
		if(_objVari != null)
		{
			if(_objVari.GetComponent<EzGui_Texture>())
			{
				_objVari.GetComponent<EzGui_Texture>().GetEZGUITexture().anchor	= SpriteRoot.ANCHOR_METHOD.MIDDLE_CENTER;
			}
			if(_objVari.GetComponent<EzGui_Button>())
			{
				_objVari.GetComponent<EzGui_Button>().GetUIButton().anchor		= SpriteRoot.ANCHOR_METHOD.MIDDLE_CENTER;
			}
			
			if(_objBase != null)
			{
				_objVari.transform.parent			= _objBase.transform;
				_objVari.transform.localPosition	= m_posAddSlotVari;
			}
			else
			{
				_objVari.transform.position	= m_posSlotBase + m_posSlotGab*SlotNum + m_posAddSlotVari;
			}
		}
	}
#endregion
	
	public static float GetImageRatio(float _fImageSizeX, float _fImageSizeY, float _fImagePercentage)
	{
		float RatioX	= AddItemToContainerShop.m_sizeSlotImageMax.x / (_fImageSizeX * _fImagePercentage);
		float RatioY	= AddItemToContainerShop.m_sizeSlotImageMax.y / (_fImageSizeY * _fImagePercentage);
		
		if(RatioX > RatioY)
		{
			if(RatioY < 1.0f)
			{
				return RatioY;
			}
		}
		else if(RatioY > RatioX)
		{
			if(RatioX < 1.0f)
			{
				return RatioX;
			}
		}
		else
		{
			if(RatioX < 1.0f)
			{
				return RatioX;
			}
		}
		
		return 1.0f;
	}
	
}