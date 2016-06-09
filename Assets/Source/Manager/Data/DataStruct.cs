using UnityEngine;
using System.Collections;

#pragma warning disable 0168 // variable declared but not used.
#pragma warning disable 0219 // variable assigned but not used.
#pragma warning disable 0414 // private field assigned but not used.

namespace DataStruct
{
	public struct SData_Package
	{
		private int m_DataIndex;
		private int m_AddGold;
		private int m_AddExp;
		private int m_AddScore;
		
		public SData_Package(int _DataIndex, int _AddGold, int _AddExp, int _AddScore)
		{
			m_DataIndex	= _DataIndex;
			m_AddGold	= _AddGold;
			m_AddExp	= _AddExp;
			m_AddScore	= _AddScore;
		}
		
		public int DataIndex	{ get { return m_DataIndex; } }
		public int AddGold		{ get { return m_AddGold; } }
		public int AddExp		{ get { return m_AddExp; } }
		public int AddScore		{ get { return m_AddScore; } }
	}
	
	public struct SData_Item
	{
		private int		m_item_idx;
		private int		m_item_name;
		private int		m_item_info;
		private string	m_item_raster;
		private int		m_item_price_gold;
		private int		m_item_price_cash;
		
		private int		m_item_type_idx;
		private int		m_item_use_time;
		private int		m_item_area_x;
		private int		m_item_area_y;
		private int		m_item_use_value;
		
		private bool	m_item_is_active;
		
		public SData_Item(int _itemIdx, int _itemName, int _itemInfo, string _itemRaster, int _itemPriceGold, int _itemPriceCash,
						int _itemTypeIdx, int _itemUseTime, int _itemAreaX, int _itemAreaY, int _itemUseValue, bool _itemIsActive)
		{
			m_item_idx			= _itemIdx;
			m_item_name			= _itemName;
			m_item_info			= _itemInfo;
			m_item_raster		= _itemRaster;
			m_item_price_gold	= _itemPriceGold;
			m_item_price_cash	= _itemPriceCash;
			
			m_item_type_idx		= _itemTypeIdx;
			m_item_use_time		= _itemUseTime;
			m_item_area_x		= _itemAreaX;
			m_item_area_y		= _itemAreaY;
			m_item_use_value	= _itemUseValue;
			m_item_is_active	= _itemIsActive;
		}
		
		public int		ItemIndex		{ get { return m_item_idx; } }
		public int		ItemName		{ get { return m_item_name; } }
		public int		ItemInfo		{ get { return m_item_info; } }
		public string	ItemRaster		{ get { return m_item_raster; } }
		public int		ItemPriceGold	{ get { return m_item_price_gold; } }
		public int		ItemPriceCash	{ get { return m_item_price_cash; } }
		
		public int		ItemType		{ get { return m_item_type_idx; } }
		public int		ItemUseTime		{ get { return m_item_use_time; } }
		public int		ItemAreaX		{ get { return m_item_area_x; } }
		public int		ItemAreaY		{ get { return m_item_area_y; } }
		public int		ItemUseValue	{ get { return m_item_use_value; } }
		
		public bool		ItemIsActive	{ get { return m_item_is_active; } }
	}
}