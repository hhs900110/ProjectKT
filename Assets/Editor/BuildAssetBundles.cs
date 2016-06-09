using UnityEngine;
using System.Collections;
using UnityEditor;
 
public class BuildAssetBundles{
 
    [MenuItem ("Assets/Auto Build Image File")]
 
    public static void buildImage()
	{
		bool isWin = false;

#if UNITY_WEBPLAYER
		isWin = true;
#endif


		BuildPipeline.PushAssetDependencies();
		//BuildPipeline.PushAssetDependencies();
		BuildAssetBundleOptions options = BuildAssetBundleOptions.CollectDependencies | BuildAssetBundleOptions.CompleteAssets;


//		if(false)
		{
			string bundle_name = "Media/Web/m3/m_b.croquis";
			const int MaxNum = 26;
			Object[] asset = new Object[MaxNum];

			asset[0] = AssetDatabase.LoadMainAssetAtPath("Assets/Model/Farm/Field.fbx");
			asset[1] = AssetDatabase.LoadMainAssetAtPath("Assets/Model/Farm/Seed.fbx");
			asset[2] = AssetDatabase.LoadMainAssetAtPath("Assets/Model/Farm/Bud.fbx");
			asset[3] = AssetDatabase.LoadMainAssetAtPath("Assets/Model/Farm/Apple.fbx");
			asset[4] = AssetDatabase.LoadMainAssetAtPath("Assets/Model/Farm/AppleMini.fbx");
			asset[5] = AssetDatabase.LoadMainAssetAtPath("Assets/Model/Farm/Banana.fbx");
			asset[6] = AssetDatabase.LoadMainAssetAtPath("Assets/Model/Farm/BananaMini.fbx");
			asset[7] = AssetDatabase.LoadMainAssetAtPath("Assets/Model/Farm/Cabbage.fbx");
			asset[8] = AssetDatabase.LoadMainAssetAtPath("Assets/Model/Farm/CabbageMini.fbx");
			asset[9] = AssetDatabase.LoadMainAssetAtPath("Assets/Model/Farm/Cucumber.fbx");
			asset[10] = AssetDatabase.LoadMainAssetAtPath("Assets/Model/Farm/CucumberMini.fbx");
			asset[11] = AssetDatabase.LoadMainAssetAtPath("Assets/Model/Farm/Rose_Red.fbx");
			asset[12] = AssetDatabase.LoadMainAssetAtPath("Assets/Model/Farm/Rose_RedMini.fbx");
			asset[13] = AssetDatabase.LoadMainAssetAtPath("Assets/Model/Farm/SunFlower.fbx");
			asset[14] = AssetDatabase.LoadMainAssetAtPath("Assets/Model/Farm/SunFlowerMini.fbx");
			asset[15] = AssetDatabase.LoadMainAssetAtPath("Assets/Model/Farm/Tulip_Yellow.fbx");
			asset[16] = AssetDatabase.LoadMainAssetAtPath("Assets/Model/Farm/Tulip_YellowMini.fbx");
			asset[17] = AssetDatabase.LoadMainAssetAtPath("Assets/Model/Farm/WaterMelon.fbx");
			asset[18] = AssetDatabase.LoadMainAssetAtPath("Assets/Model/Farm/WaterMelonMini.fbx");
			asset[19] = AssetDatabase.LoadMainAssetAtPath("Assets/Model/Farm/Berlay.fbx");
			asset[20] = AssetDatabase.LoadMainAssetAtPath("Assets/Model/Farm/Berlay_Mini.fbx");
			asset[21] = AssetDatabase.LoadMainAssetAtPath("Assets/Model/Farm/Rice.fbx");
			asset[22] = AssetDatabase.LoadMainAssetAtPath("Assets/Model/Farm/Rice_Mini.fbx");
			asset[23] = AssetDatabase.LoadMainAssetAtPath("Assets/Model/Building/factory_build_lv1.fbx");
			asset[24] = AssetDatabase.LoadMainAssetAtPath("Assets/Model/Building/factory_build_lv2.fbx");
			asset[25] = AssetDatabase.LoadMainAssetAtPath("Assets/Model/Building/factory_build_lv3.fbx");
			
			if( isWin)
			{
				BuildPipeline.BuildAssetBundle(null, asset, bundle_name, options);
			}
			else
			{
				BuildPipeline.BuildAssetBundle(null, asset, bundle_name, options, BuildTarget.iOS);
			}
        }
		
//		if(false)
		{
			string bundle_name = "Media/Web/m3/m_a.croquis";
			const int MaxNum = 27;
			Object[] asset = new Object[MaxNum];

			asset[0] = AssetDatabase.LoadMainAssetAtPath("Assets/Model/Animal/Chicken01.fbx");
			asset[1] = AssetDatabase.LoadMainAssetAtPath("Assets/Model/Animal/Chicken02.fbx");
			asset[2] = AssetDatabase.LoadMainAssetAtPath("Assets/Model/Animal/Chicken03.fbx");
			asset[3] = AssetDatabase.LoadMainAssetAtPath("Assets/Model/Animal/Chicken04.fbx");
			asset[4] = AssetDatabase.LoadMainAssetAtPath("Assets/Model/Animal/Chicken05.fbx");
			asset[5] = AssetDatabase.LoadMainAssetAtPath("Assets/Model/Animal/Chicken06.fbx");
			asset[6] = AssetDatabase.LoadMainAssetAtPath("Assets/Model/Animal/calf01.fbx");
			asset[7] = AssetDatabase.LoadMainAssetAtPath("Assets/Model/Animal/calf02.fbx");
			asset[8] = AssetDatabase.LoadMainAssetAtPath("Assets/Model/Animal/cow01.fbx");
			asset[9] = AssetDatabase.LoadMainAssetAtPath("Assets/Model/Animal/cow02.fbx");
			asset[10] = AssetDatabase.LoadMainAssetAtPath("Assets/Model/Animal/duck01.fbx");
			asset[11] = AssetDatabase.LoadMainAssetAtPath("Assets/Model/Animal/duck02.fbx");
			asset[12] = AssetDatabase.LoadMainAssetAtPath("Assets/Model/Animal/duck03.fbx");
			asset[13] = AssetDatabase.LoadMainAssetAtPath("Assets/Model/Animal/swan.fbx");
			asset[14] = AssetDatabase.LoadMainAssetAtPath("Assets/Model/Animal/goose01.fbx");
			asset[15] = AssetDatabase.LoadMainAssetAtPath("Assets/Model/Animal/goose02.fbx");
			asset[16] = AssetDatabase.LoadMainAssetAtPath("Assets/Model/Animal/goose03.fbx");
			asset[17] = AssetDatabase.LoadMainAssetAtPath("Assets/Model/Animal/goose04.fbx");
			asset[18] = AssetDatabase.LoadMainAssetAtPath("Assets/Model/Animal/donkey.fbx");
			asset[19] = AssetDatabase.LoadMainAssetAtPath("Assets/Model/Animal/foal.fbx");
			asset[20] = AssetDatabase.LoadMainAssetAtPath("Assets/Model/Animal/goat.fbx");
			asset[21] = AssetDatabase.LoadMainAssetAtPath("Assets/Model/Animal/horse01.fbx");
			asset[22] = AssetDatabase.LoadMainAssetAtPath("Assets/Model/Animal/horse02.fbx");
			asset[23] = AssetDatabase.LoadMainAssetAtPath("Assets/Model/Animal/horse03.fbx");
			asset[24] = AssetDatabase.LoadMainAssetAtPath("Assets/Model/Animal/horse04.fbx");
			asset[25] = AssetDatabase.LoadMainAssetAtPath("Assets/Model/Animal/sheep01.fbx");
			asset[26] = AssetDatabase.LoadMainAssetAtPath("Assets/Model/Animal/sheep02.fbx");

			if( isWin)
			{
				BuildPipeline.BuildAssetBundle(null, asset, bundle_name, options);
			}
			else
			{
				BuildPipeline.BuildAssetBundle(null, asset, bundle_name, options, BuildTarget.iOS);
			}
        }
		
//		if(false)
		{
			string bundle_name = "Media/Web/m3/m_i.croquis";
			const int MaxNum = 31;
			Object[] asset = new Object[MaxNum];

			asset[0] = AssetDatabase.LoadMainAssetAtPath("Assets/Model/Trophy/trophy01_farm_g.fbx");
			asset[1] = AssetDatabase.LoadMainAssetAtPath("Assets/Model/Trophy/trophy02_mine_g.fbx");
			asset[2] = AssetDatabase.LoadMainAssetAtPath("Assets/Model/Trophy/trophy03_fisher_g.fbx");
			asset[3] = AssetDatabase.LoadMainAssetAtPath("Assets/Model/Trophy/trophy04_ranch_g.fbx");
			asset[4] = AssetDatabase.LoadMainAssetAtPath("Assets/Model/Trophy/trophy05_food_g.fbx");
			asset[5] = AssetDatabase.LoadMainAssetAtPath("Assets/Model/Trophy/trophy06_craft_g.fbx");
			asset[6] = AssetDatabase.LoadMainAssetAtPath("Assets/Model/Trophy/trophy07_fashion_g.fbx");
			asset[7] = AssetDatabase.LoadMainAssetAtPath("Assets/Model/Item/Item01_lake.fbx");
			asset[8] = AssetDatabase.LoadMainAssetAtPath("Assets/Model/Item/Item02_OldWoodChairB.fbx");
			asset[9] = AssetDatabase.LoadMainAssetAtPath("Assets/Model/Item/Item03_SwingChairB.fbx");
			asset[10] = AssetDatabase.LoadMainAssetAtPath("Assets/Model/Item/Item04_WoodChairB.fbx");
			asset[11] = AssetDatabase.LoadMainAssetAtPath("Assets/Model/Item/Item05_WoodFenceB.fbx");
			asset[12] = AssetDatabase.LoadMainAssetAtPath("Assets/Model/Item/Item06_OldMetalFenceB.fbx");
			asset[13] = AssetDatabase.LoadMainAssetAtPath("Assets/Model/Item/Item07_RoseFenceB.fbx");
			asset[14] = AssetDatabase.LoadMainAssetAtPath("Assets/Model/Item/Item08_FlowerPot.fbx");
			asset[15] = AssetDatabase.LoadMainAssetAtPath("Assets/Model/Item/Item09_CactusPot.fbx");
			asset[16] = AssetDatabase.LoadMainAssetAtPath("Assets/Model/Item/Item10_SmallTreePot.fbx");
			asset[17] = AssetDatabase.LoadMainAssetAtPath("Assets/Model/Item/Item11_AppleTree.fbx");
			asset[18] = AssetDatabase.LoadMainAssetAtPath("Assets/Model/Item/Item12_BlueBerryWood.fbx");
			asset[19] = AssetDatabase.LoadMainAssetAtPath("Assets/Model/Item/Item13_WoodRoadSTRA.fbx");
			asset[20] = AssetDatabase.LoadMainAssetAtPath("Assets/Model/Item/Item1415_WoodRoadTurn.fbx");
			asset[21] = AssetDatabase.LoadMainAssetAtPath("Assets/Model/Item/Item16_1_WoodRoadTRI.fbx");
			asset[22] = AssetDatabase.LoadMainAssetAtPath("Assets/Model/Item/Item16_WoodRoadCROS.fbx");
			asset[23] = AssetDatabase.LoadMainAssetAtPath("Assets/Model/Item/Item17_GravelRoadSTRA.fbx");
			asset[24] = AssetDatabase.LoadMainAssetAtPath("Assets/Model/Item/Item1819_GravelRoadTurn.fbx");
			asset[25] = AssetDatabase.LoadMainAssetAtPath("Assets/Model/Item/Item20-1_GravelRoadTRI.fbx");
			asset[26] = AssetDatabase.LoadMainAssetAtPath("Assets/Model/Item/Item20_GravelRoadCROS.fbx");
			asset[27] = AssetDatabase.LoadMainAssetAtPath("Assets/Model/Item/Item21_AspaltRoadSTRA.fbx");
			asset[28] = AssetDatabase.LoadMainAssetAtPath("Assets/Model/Item/Item24_1_AspaltRoadTRI.fbx");
			asset[29] = AssetDatabase.LoadMainAssetAtPath("Assets/Model/Item/Item24_AspaltRoadCROS.fbx");
			asset[30] = AssetDatabase.LoadMainAssetAtPath("Assets/Model/Item/Item2223_AspaltRoadTurn.fbx");

			if( isWin)
			{
				BuildPipeline.BuildAssetBundle(null, asset, bundle_name, options);
			}
			else
			{
				BuildPipeline.BuildAssetBundle(null, asset, bundle_name, options, BuildTarget.iOS);
			}
        }
		{
			string bundle_name = "Media/Web/m3/m_1.croquis";
			//const int MaxNum = 22;
			const int MaxNum = 20;
			Object[] asset = new Object[MaxNum];

			asset[0] = AssetDatabase.LoadMainAssetAtPath("Assets/Model/Building/MyHome_lv01.fbx");
			asset[1] = AssetDatabase.LoadMainAssetAtPath("Assets/Model/Building/B001_Farmguild_lv01.fbx");
			asset[2] = AssetDatabase.LoadMainAssetAtPath("Assets/Model/Building/B006_FishingPlace_lv01.fbx");
			asset[3] = AssetDatabase.LoadMainAssetAtPath("Assets/Model/Building/B009_Chickenfactory_lv01.fbx");
			asset[4] = AssetDatabase.LoadMainAssetAtPath("Assets/Model/Building/B010_Cowshed_lv01.fbx");
			asset[5] = AssetDatabase.LoadMainAssetAtPath("Assets/Model/Building/B011_MineFactory_lv01.fbx");
			asset[6] = AssetDatabase.LoadMainAssetAtPath("Assets/Model/Building/B013_DonutsStore_lv01.fbx");
			asset[7] = AssetDatabase.LoadMainAssetAtPath("Assets/Model/Building/B014_pizzahouse_lv01.fbx");
			asset[8] = AssetDatabase.LoadMainAssetAtPath("Assets/Model/Building/B017_Woodfactory_lv01.fbx");
			asset[9] = AssetDatabase.LoadMainAssetAtPath("Assets/Model/Building/B023_MilkFactory_lv01.fbx");
			asset[10] = AssetDatabase.LoadMainAssetAtPath("Assets/Model/Building/B024_CheeseFactory_lv01.fbx");
			asset[11] = AssetDatabase.LoadMainAssetAtPath("Assets/Model/Building/B025_Japanrestorant_lv01.fbx");
			asset[12] = AssetDatabase.LoadMainAssetAtPath("Assets/Model/Building/B031_WitchMindShop_lv01.fbx");
			asset[13] = AssetDatabase.LoadMainAssetAtPath("Assets/Model/Building/B032_MagicFactory_lv01.fbx");
			asset[14] = AssetDatabase.LoadMainAssetAtPath("Assets/Model/Building/B001_Farmguild_lv02.fbx");
			asset[15] = AssetDatabase.LoadMainAssetAtPath("Assets/Model/Building/B006_FishingPlace_lv02.fbx");
			asset[16] = AssetDatabase.LoadMainAssetAtPath("Assets/Model/Building/B009_Chickenfactory_lv02.fbx");
			asset[17] = AssetDatabase.LoadMainAssetAtPath("Assets/Model/Building/B010_Cowshed_lv02.fbx");
			asset[18] = AssetDatabase.LoadMainAssetAtPath("Assets/Model/Building/B023_MilkFactory_lv02.fbx");
			asset[19] = AssetDatabase.LoadMainAssetAtPath("Assets/Model/Building/B025_Japanrestorant_lv02.fbx");
//			asset[20] = AssetDatabase.LoadMainAssetAtPath("Assets/Model/Building/B002_Farmguild_RC_1.prefab");
//			asset[21] = AssetDatabase.LoadMainAssetAtPath("Assets/Model/Building/new_house_farm.fbx");

			if( isWin)
			{
				BuildPipeline.BuildAssetBundle(null, asset, bundle_name, options);
			}
			else
			{
				BuildPipeline.BuildAssetBundle(null, asset, bundle_name, options, BuildTarget.iOS);
			}
        }
		
//		if(false)
		{
			string bundle_name = "Media/Web/m3/m_2.croquis";
			const int MaxNum = 34;
			Object[] asset = new Object[MaxNum];

			asset[0] = AssetDatabase.LoadMainAssetAtPath("Assets/Model/Building/MyHome_lv02.fbx");
			asset[1] = AssetDatabase.LoadMainAssetAtPath("Assets/Model/Building/B015_Cakehouse_lv01.fbx");
			asset[2] = AssetDatabase.LoadMainAssetAtPath("Assets/Model/Building/B016_Breadhouse_lv01.fbx");
			asset[3] = AssetDatabase.LoadMainAssetAtPath("Assets/Model/Building/B018_Mill_chair_lv01.fbx");
			asset[4] = AssetDatabase.LoadMainAssetAtPath("Assets/Model/Building/B019_Mill_accessories_lv01.fbx");
			asset[5] = AssetDatabase.LoadMainAssetAtPath("Assets/Model/Building/B021_Clothingstor_lv01.fbx");
			asset[6] = AssetDatabase.LoadMainAssetAtPath("Assets/Model/Building/B022_Casualshop_lv01.fbx");
			asset[7] = AssetDatabase.LoadMainAssetAtPath("Assets/Model/Building/B026_Chinarestorant_lv01.fbx");
			asset[8] = AssetDatabase.LoadMainAssetAtPath("Assets/Model/Building/B028_SteelFactory_Bath_lv01.fbx");
			asset[9] = AssetDatabase.LoadMainAssetAtPath("Assets/Model/Building/B030_Furniture_lv01.fbx");
			asset[10] = AssetDatabase.LoadMainAssetAtPath("Assets/Model/Building/B033_Cabin_lv01.fbx");
			asset[11] = AssetDatabase.LoadMainAssetAtPath("Assets/Model/Building/B035_MotelBlue_lv01.fbx");
			asset[12] = AssetDatabase.LoadMainAssetAtPath("Assets/Model/Building/B011_MineFactory_lv02.fbx");
			asset[13] = AssetDatabase.LoadMainAssetAtPath("Assets/Model/Building/B013_DonutsStore_lv02.fbx");
			asset[14] = AssetDatabase.LoadMainAssetAtPath("Assets/Model/Building/B014_pizzahouse_lv02.fbx");
			asset[15] = AssetDatabase.LoadMainAssetAtPath("Assets/Model/Building/B015_Cakehouse_lv02.fbx");
			asset[16] = AssetDatabase.LoadMainAssetAtPath("Assets/Model/Building/B017_Woodfactory_lv02.fbx");
			asset[17] = AssetDatabase.LoadMainAssetAtPath("Assets/Model/Building/B018_Mill_chair_lv02.fbx");
			asset[18] = AssetDatabase.LoadMainAssetAtPath("Assets/Model/Building/B019_Mill_accessories_lv02.fbx");
			asset[19] = AssetDatabase.LoadMainAssetAtPath("Assets/Model/Building/B021_Clothingstor_lv02.fbx");
			asset[20] = AssetDatabase.LoadMainAssetAtPath("Assets/Model/Building/B024_CheeseFactory_lv02.fbx");
			asset[21] = AssetDatabase.LoadMainAssetAtPath("Assets/Model/Building/B028_SteelFactory_Bath_lv02.fbx");
			asset[22] = AssetDatabase.LoadMainAssetAtPath("Assets/Model/Building/B030_Furniture_lv02.fbx");
			asset[23] = AssetDatabase.LoadMainAssetAtPath("Assets/Model/Building/B031_WitchMindShop_lv02.fbx");
			asset[24] = AssetDatabase.LoadMainAssetAtPath("Assets/Model/Building/B032_MagicFactory_lv02.fbx");
			asset[25] = AssetDatabase.LoadMainAssetAtPath("Assets/Model/Building/B033_Cabin_lv02.fbx");
			asset[26] = AssetDatabase.LoadMainAssetAtPath("Assets/Model/Building/B001_Farmguild_lv03.fbx");
			asset[27] = AssetDatabase.LoadMainAssetAtPath("Assets/Model/Building/B006_FishingPlace_lv03.fbx");
			asset[28] = AssetDatabase.LoadMainAssetAtPath("Assets/Model/Building/B009_Chickenfactory_lv03.fbx");
			asset[29] = AssetDatabase.LoadMainAssetAtPath("Assets/Model/Building/B010_Cowshed_lv03.fbx");
			asset[30] = AssetDatabase.LoadMainAssetAtPath("Assets/Model/Building/B023_MilkFactory_lv03.fbx");
			asset[31] = AssetDatabase.LoadMainAssetAtPath("Assets/Model/Building/B025_Japanrestorant_lv03.fbx");
			asset[32] = AssetDatabase.LoadMainAssetAtPath("Assets/Model/Building/B031_WitchMindShop_lv03.fbx");
			asset[33] = AssetDatabase.LoadMainAssetAtPath("Assets/Model/Building/B032_MagicFactory_lv03.fbx");

			if( isWin)
			{
				BuildPipeline.BuildAssetBundle(null, asset, bundle_name, options);
			}
			else
			{
				BuildPipeline.BuildAssetBundle(null, asset, bundle_name, options, BuildTarget.iOS);
			}
        }
		
//		if(false)
		{
			string bundle_name = "Media/Web/m3/m_3.croquis";
			const int MaxNum = 34;
			Object[] asset = new Object[MaxNum];

			asset[0] = AssetDatabase.LoadMainAssetAtPath("Assets/Model/Building/MyHome_lv03.fbx");
			asset[1] = AssetDatabase.LoadMainAssetAtPath("Assets/Model/Building/B020_Mill_wheel_lv01.fbx");
			asset[2] = AssetDatabase.LoadMainAssetAtPath("Assets/Model/Building/B027_Koreanrestorant_lv01.fbx");
			asset[3] = AssetDatabase.LoadMainAssetAtPath("Assets/Model/Building/B029_Jewelry_lv01.fbx");
			asset[4] = AssetDatabase.LoadMainAssetAtPath("Assets/Model/Building/B034_RedCabin_lv01.fbx");
			asset[5] = AssetDatabase.LoadMainAssetAtPath("Assets/Model/Building/B038_MansionYellow_lv01.fbx");
			asset[6] = AssetDatabase.LoadMainAssetAtPath("Assets/Model/Building/B016_Breadhouse_lv02.fbx");
			asset[7] = AssetDatabase.LoadMainAssetAtPath("Assets/Model/Building/B020_Mill_wheel_lv02.fbx");
			asset[8] = AssetDatabase.LoadMainAssetAtPath("Assets/Model/Building/B022_Casualshop_lv02.fbx");
			asset[9] = AssetDatabase.LoadMainAssetAtPath("Assets/Model/Building/B026_Chinarestorant_lv02.fbx");
			asset[10] = AssetDatabase.LoadMainAssetAtPath("Assets/Model/Building/B027_Koreanrestorant_lv02.fbx");
			asset[11] = AssetDatabase.LoadMainAssetAtPath("Assets/Model/Building/B029_Jewelry_lv02.fbx");
			asset[12] = AssetDatabase.LoadMainAssetAtPath("Assets/Model/Building/B034_RedCabin_lv02.fbx");
			asset[13] = AssetDatabase.LoadMainAssetAtPath("Assets/Model/Building/B035_MotelBlue_lv02.fbx");
			asset[14] = AssetDatabase.LoadMainAssetAtPath("Assets/Model/Building/B038_MansionYellow_lv02.fbx");
			asset[15] = AssetDatabase.LoadMainAssetAtPath("Assets/Model/Building/B011_MineFactory_lv03.fbx");
			asset[16] = AssetDatabase.LoadMainAssetAtPath("Assets/Model/Building/B013_DonutsStore_lv03.fbx");
			asset[17] = AssetDatabase.LoadMainAssetAtPath("Assets/Model/Building/B014_pizzahouse_lv03.fbx");
			asset[18] = AssetDatabase.LoadMainAssetAtPath("Assets/Model/Building/B015_Cakehouse_lv03.fbx");
			asset[19] = AssetDatabase.LoadMainAssetAtPath("Assets/Model/Building/B016_Breadhouse_lv03.fbx");
			asset[20] = AssetDatabase.LoadMainAssetAtPath("Assets/Model/Building/B017_Woodfactory_lv03.fbx");
			asset[21] = AssetDatabase.LoadMainAssetAtPath("Assets/Model/Building/B018_Mill_chair_lv03.fbx");
			asset[22] = AssetDatabase.LoadMainAssetAtPath("Assets/Model/Building/B019_Mill_accessories_lv03.fbx");
			asset[23] = AssetDatabase.LoadMainAssetAtPath("Assets/Model/Building/B020_Mill_wheel_lv03.fbx");
			asset[24] = AssetDatabase.LoadMainAssetAtPath("Assets/Model/Building/B021_Clothingstor_lv03.fbx");
			asset[25] = AssetDatabase.LoadMainAssetAtPath("Assets/Model/Building/B022_Casualshop_lv03.fbx");
			asset[26] = AssetDatabase.LoadMainAssetAtPath("Assets/Model/Building/B024_CheeseFactory_lv03.fbx");
			asset[27] = AssetDatabase.LoadMainAssetAtPath("Assets/Model/Building/B026_Chinarestorant_lv03.fbx");
			asset[28] = AssetDatabase.LoadMainAssetAtPath("Assets/Model/Building/B028_SteelFactory_Bath_lv03.fbx");
			asset[29] = AssetDatabase.LoadMainAssetAtPath("Assets/Model/Building/B030_Furniture_lv03.fbx");
			asset[30] = AssetDatabase.LoadMainAssetAtPath("Assets/Model/Building/B033_Cabin_lv03.fbx");
			asset[31] = AssetDatabase.LoadMainAssetAtPath("Assets/Model/Building/B034_RedCabin_lv03.fbx");
			asset[32] = AssetDatabase.LoadMainAssetAtPath("Assets/Model/Building/B035_MotelBlue_lv03.fbx");
			asset[33] = AssetDatabase.LoadMainAssetAtPath("Assets/Model/Building/B038_MansionYellow_lv03.fbx");

			if( isWin)
			{
				BuildPipeline.BuildAssetBundle(null, asset, bundle_name, options);
			}
			else
			{
				BuildPipeline.BuildAssetBundle(null, asset, bundle_name, options, BuildTarget.iOS);
			}
        }
		
//		if(false)
		{
			string bundle_name = "Media/Web/m3/m_4.croquis";
			const int MaxNum = 11;
			Object[] asset = new Object[MaxNum];

			asset[0] = AssetDatabase.LoadMainAssetAtPath("Assets/Model/Building/B003_Vinylhouse_lv01.fbx");
			asset[1] = AssetDatabase.LoadMainAssetAtPath("Assets/Model/Building/B004_greenhouse_lv01.fbx");
			asset[2] = AssetDatabase.LoadMainAssetAtPath("Assets/Model/Building/B012_Dwarfcoalmine_lv01.fbx");
			asset[3] = AssetDatabase.LoadMainAssetAtPath("Assets/Model/Building/B003_Vinylhouse_lv02.fbx");
			asset[4] = AssetDatabase.LoadMainAssetAtPath("Assets/Model/Building/B004_greenhouse_lv02.fbx");
			asset[5] = AssetDatabase.LoadMainAssetAtPath("Assets/Model/Building/B012_Dwarfcoalmine_lv02.fbx");
			asset[6] = AssetDatabase.LoadMainAssetAtPath("Assets/Model/Building/B003_Vinylhouse_lv03.fbx");
			asset[7] = AssetDatabase.LoadMainAssetAtPath("Assets/Model/Building/B004_greenhouse_lv03.fbx");
			asset[8] = AssetDatabase.LoadMainAssetAtPath("Assets/Model/Building/B012_Dwarfcoalmine_lv03.fbx");
			asset[9] = AssetDatabase.LoadMainAssetAtPath("Assets/Model/Building/B027_Koreanrestorant_lv03.fbx");
			asset[10] = AssetDatabase.LoadMainAssetAtPath("Assets/Model/Building/B029_Jewelry_lv03.fbx");

			if( isWin)
			{
				BuildPipeline.BuildAssetBundle(null, asset, bundle_name, options);
			}
			else
			{
				BuildPipeline.BuildAssetBundle(null, asset, bundle_name, options, BuildTarget.iOS);
			}
        }

		//for(int i = 0; i < MaxNum; i++)
		//{
		//    BuildPipeline.BuildAssetBundle(asset[i], null, "Media/web/m3/"+name[i]+".croquis", options);
		//}
		//Debug.Log(asset[181]);


		//      BuildPipeline.BuildAssetBundle(asset[0], null, "Shared.unity3d", options); 

		BuildPipeline.PopAssetDependencies();
	}
     
}
 
