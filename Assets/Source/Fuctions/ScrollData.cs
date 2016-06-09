using UnityEngine;
using System.Collections;
using DefineBase;

#pragma warning disable 0168 // variable declared but not used.
#pragma warning disable 0219 // variable assigned but not used.
#pragma warning disable 0414 // private field assigned but not used.

public abstract class ScrollData
{
	public static Object		objEmptyImage				= Resources.Load("Mobile/EZGUI/DefaultImages/DefaultCubicWorldImage");
	public static Object		objCroquiEmptyImage			= Resources.Load("Mobile/EZGUI/DefaultImages/DefaultCroquisImage");
	
	public static Object		objSpriteText18				= Resources.Load("Mobile/EZGUI/SpriteText/SpriteText18");
	public static Object		objSpriteText22				= Resources.Load("Mobile/EZGUI/SpriteText/SpriteText22");
	public static Object		objSpriteText24				= Resources.Load("Mobile/EZGUI/SpriteText/SpriteText24");
	public static Object		objSpriteText30				= Resources.Load("Mobile/EZGUI/SpriteText/SpriteText30");
	public static Object		objSpriteText35				= Resources.Load("Mobile/EZGUI/SpriteText/SpriteText35");
	public static Object		objSpriteText50				= Resources.Load("Mobile/EZGUI/SpriteText/SpriteText50");
	
	public static Object		objSpriteText_stroke18		= Resources.Load("Mobile/EZGUI/SpriteText/SpriteText_stroke18");
	public static Object		objSpriteText_stroke22		= Resources.Load("Mobile/EZGUI/SpriteText/SpriteText_stroke22");
	public static Object		objSpriteText_stroke24		= Resources.Load("Mobile/EZGUI/SpriteText/SpriteText_stroke24");
	public static Object		objSpriteText_stroke30		= Resources.Load("Mobile/EZGUI/SpriteText/SpriteText_stroke30");
	public static Object		objSpriteText_stroke35		= Resources.Load("Mobile/EZGUI/SpriteText/SpriteText_stroke35");
	
	public static Object		objIconCube_39_40			= Resources.Load("Mobile/EZGUI/TotalIcon/IconCube_39_40");
	public static Object		objIconRainbowCube_39_40	= Resources.Load("Mobile/EZGUI/TotalIcon/IconRainbowCube_39_40");
	public static Object		objIconBuddyCube_40_40		= Resources.Load("Mobile/EZGUI/TotalIcon/IconBuddyCube_40_40");
	public static Object		objIconExp_39_40			= Resources.Load("Mobile/EZGUI/TotalIcon/IconExp_39_40");
	public static Object		objIconTime_39_40			= Resources.Load("Mobile/EZGUI/TotalIcon/IconTime_39_40");
	public static Object		objIconHeart_32_32			= Resources.Load("Mobile/EZGUI/TotalIcon/IconHeart_32_32");
	public static Object		objIconFaceBook_30_30		= Resources.Load("Mobile/EZGUI/TotalIcon/IconFaceBook_30_30");
	public static Object		objIconDwarf_39_40			= Resources.Load("Mobile/EZGUI/TotalIcon/IconDwarf_39_40");
	public static Object		objIconCustom				= Resources.Load("Mobile/EZGUI/TotalIcon/IconCustom_128_128");
	public static Object		objIconLevel_38_37			= Resources.Load("Mobile/EZGUI/TotalIcon/IconLevel_38_37");
	public static Object		objPictureFriend_30_30		= Resources.Load("Mobile/EZGUI/TotalIcon/IconFriend_30_30");
	public static Object		objIconPlus_20_21			= Resources.Load("Mobile/EZGUI/TotalIcon/IconPlus_20_21");
	public static Object		objIconMinus_20_21			= Resources.Load("Mobile/EZGUI/TotalIcon/IconMinus_20_21");
	public static Object		objIconRock_56_76			= Resources.Load("Mobile/EZGUI/TotalIcon/IconRock_56_76");
	public static Object		objIconRock_95_130			= Resources.Load("Mobile/EZGUI/TotalIcon/IconRock_95_130");
	public static Object		objIconSellTime_39_40		= Resources.Load("Mobile/EZGUI/TotalIcon/IconSellTime_39_40");
	
	public static Object		objShopSlotNumPad			= Resources.Load("Mobile/EZGUI/Frames/Shop/SlotNumPad");
	public static Object		objShopIconNewMark			= Resources.Load("Mobile/EZGUI/TotalIcon/IconNew/IconBandNew");
	public static Object		objShopRainbowMark			= Resources.Load("Mobile/EZGUI/Frames/Shop/Mark_Rainbow");
	
	public static Object		objRectangleConfirm			= Resources.Load("Mobile/EZGUI/Buttons/Rectangle/97_54/Rectangle_Confirm");
	public static Object		objRectangleDelete			= Resources.Load("Mobile/EZGUI/Buttons/Rectangle/97_54/Rectangle_Delete");
	public static Object		objRectangleVisit			= Resources.Load("Mobile/EZGUI/Buttons/Rectangle/97_54/Rectangle_Visit");
//	public static Object		objRectangleInvite			= Resources.Load("Mobile/EZGUI/Buttons/Rectangle/97_54/Rectangle_Invite");
	public static Object		objRectangleViewCustom		= Resources.Load("Mobile/EZGUI/Buttons/Rectangle/97_54/Rectangle_ViewCustomize");
	public static Object		objRectangleFollow			= Resources.Load("Mobile/EZGUI/Buttons/Rectangle/97_54/Rectangle_FriendFollow");
	public static Object		objRectangleReject			= Resources.Load("Mobile/EZGUI/Buttons/Rectangle/97_54/Rectangle_Reject");
	public static Object		objRectangleOpen			= Resources.Load("Mobile/EZGUI/Buttons/Rectangle/97_54/Rectangle_Open");
	public static Object		objRectangleClose			= Resources.Load("Mobile/EZGUI/Buttons/Rectangle/97_54/Rectangle_Close");
	
	
	public static Object		objMarkUpgradeComp			= Resources.Load("Mobile/EZGUI/Container/CompMark/UpgradeComp");
	public static Object		objMarkResearchComp			= Resources.Load("Mobile/EZGUI/Container/CompMark/ResearchComp");
	
	
	//////////   Message Color   //////////
	public static Color			colorMessageName			= new Color(0.667f, 0.000f, 0.000f);
	public static Color			colorMessageDate			= new Color(0.667f, 0.000f, 0.000f);
	public static Color			colorMessageLevel			= new Color(0.663f, 0.412f, 0.302f);
	public static Color			colorMessageContents		= new Color(0.663f, 0.412f, 0.302f);
	public static Color			colorMessageHeart			= new Color(0.333f, 0.000f, 0.000f);
	public static Color			colorMessageMent			= new Color(0.331f, 0.206f, 0.151f);
	public static Color			colorMessageSNS				= new Color(0.000f, 0.000f, 0.000f);
	public static Color			colorMessageSNSName			= new Color(0.000f, 0.000f, 0.000f);
	public static Color			colorEmptyString			= new Color(0.800f, 0.800f, 0.800f);
	
	//////////   Shop Color   //////////
	public static Color			colorShopSlotTitle			= new Color(0.150f, 0.190f, 0.020f);
//	public static Color			colorShopSlotTitleFront		= Color.white;
//	public static Color			colorShopSlotTitleBack		= new Color(0.150f, 0.190f, 0.020f);
	public static Color			colorShopSlotTitleFront		= new Color(0.150f, 0.190f, 0.020f);
	public static Color			colorShopSlotTitleBack		= Color.white;
//	public static Color			colorShopSlotTitleBack		= new Color(0.843f, 0.369f, 0.082f);
	public static Color			colorShopSlotCube			= Color.black;
	
	//////////   Image Size   //////////
	public static Vector2		sizeMessageImage			= new Vector2(71.00f, 73.00f);
	public static Vector2		sizeMessageIconImage		= new Vector2(30.00f, 30.00f);
	
	public static string GetStringTime(int SecondTime)
	{
		int		hourTime	= SecondTime / 3600;
		int		MinTime		= (SecondTime - (hourTime * 3600)) / 60;
		int		SecTime		= SecondTime - (hourTime * 3600) - (MinTime * 60);
		
		string	StringHour	= hourTime.ToString();
		if (hourTime < 10)
		{
			StringHour	= "0" + hourTime.ToString();
		}
		string	StringMin	= MinTime.ToString();
		if (MinTime < 10)
		{
			StringMin	= "0" + MinTime.ToString();
		}
		string	StringSec	= SecTime.ToString();
		if (SecTime < 10)
		{
			StringSec	= "0" + SecTime.ToString();
		}
		string	StringTime	= StringHour + ":" + StringMin + ":" + StringSec;
		return StringTime;
	}
	
	public void OnDestroy()
	{
		if(objEmptyImage != null)
		{
			Object.Destroy(objEmptyImage);
			Object.Destroy(objCroquiEmptyImage);
			
			Object.Destroy(objSpriteText18);
			Object.Destroy(objSpriteText22);
			Object.Destroy(objSpriteText24);
			Object.Destroy(objSpriteText30);
			Object.Destroy(objSpriteText35);
			Object.Destroy(objSpriteText50);
			
			Object.Destroy(objSpriteText_stroke18);
			Object.Destroy(objSpriteText_stroke22);
			Object.Destroy(objSpriteText_stroke24);
			Object.Destroy(objSpriteText_stroke30);
			Object.Destroy(objSpriteText_stroke35);
			
			Object.Destroy(objIconCube_39_40);
			Object.Destroy(objIconRainbowCube_39_40);
			Object.Destroy(objIconExp_39_40);
			Object.Destroy(objIconTime_39_40);
			Object.Destroy(objIconHeart_32_32);
			Object.Destroy(objIconFaceBook_30_30);
			Object.Destroy(objIconDwarf_39_40);
			Object.Destroy(objIconCustom);
			Object.Destroy(objIconLevel_38_37);
			Object.Destroy(objPictureFriend_30_30);
			
			Object.Destroy(objShopSlotNumPad);
			Object.Destroy(objShopIconNewMark);
			Object.Destroy(objShopRainbowMark);
			
			Object.Destroy(objRectangleConfirm);
			Object.Destroy(objRectangleDelete);
			Object.Destroy(objRectangleVisit);
			Object.Destroy(objRectangleReject);
			Object.Destroy(objRectangleOpen);
			Object.Destroy(objRectangleClose);
			
			
			
			objEmptyImage				= null;
			objCroquiEmptyImage			= null;
			
			objSpriteText18				= null;
			objSpriteText22				= null;
			objSpriteText24				= null;
			objSpriteText30				= null;
			objSpriteText35				= null;
			objSpriteText50				= null;
			
			objSpriteText_stroke18		= null;
			objSpriteText_stroke22		= null;
			objSpriteText_stroke24		= null;
			objSpriteText_stroke30		= null;
			objSpriteText_stroke35		= null;
			
			objIconCube_39_40			= null;
			objIconRainbowCube_39_40	= null;
			objIconExp_39_40			= null;
			objIconTime_39_40			= null;
			objIconHeart_32_32			= null;
			objIconFaceBook_30_30		= null;
			objIconDwarf_39_40			= null;
			objIconCustom				= null;
			objIconLevel_38_37			= null;
			objPictureFriend_30_30		= null;
			
			objShopSlotNumPad			= null;
			objShopIconNewMark			= null;
			objShopRainbowMark			= null;
			
			objRectangleConfirm			= null;
			objRectangleDelete			= null;
			objRectangleVisit			= null;
			objRectangleReject			= null;
			objRectangleOpen			= null;
			objRectangleClose			= null;
			
		}
	}
}