using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

public abstract class CSSqliteBase
{
	protected bool								m_bOpened;
	protected string							m_strFile;
	protected string							m_strPath;
	protected SqliteConnection					m_pSqlite;
	
	public virtual void Create()
	{
//#if UNITY_EDITOR
		m_strPath = string.Format("Assets/Plugins/Sqlite/{0}", m_strFile);
//#else
//		m_strPath = string.Format("{0}/{1}", Application.persistentDataPath, m_strFile);
//#endif
		
		m_pSqlite = new SqliteConnection();
		
		m_pSqlite.OpenDB( m_strPath );
		m_pSqlite.CloseDB();
		if( !CheckValid() )
		{
			// Send Request To Web
			Debug.LogError("Don't Find SQL");
		}
	}
	
	//
	public void Open()
	{
		if( m_pSqlite != null )
		{
			m_bOpened = true;
			m_pSqlite.OpenDB( m_strPath );
		}
	}
	
	public void Close()
	{
		if( m_pSqlite != null )
		{
			m_bOpened = false;
			m_pSqlite.CloseDB();
		}
	}
	
	public bool IsOpen()	{return m_bOpened;}
	public bool IsValid()	{return m_pSqlite != null;}
	public string GetPath()	{return m_strPath;}
	
	protected byte[] Convert( string s )
	{
		s = s.Replace(" ", "");
		byte[] compBuffer = new byte[s.Length / 2];
		for( int i=0; i<s.Length; i += 2)
		{
			try
			{
				compBuffer[i/2] = (byte)System.Convert.ToByte(s.Substring(i, 2), 16);
			}
			catch
			{
				//Debug.LogError("Hex Error!!");
			}
		}
		return compBuffer;
	}
	
	protected string Convert( byte[] bytes )
	{
		StringBuilder hex = new StringBuilder( bytes.Length * 3 );
		foreach( byte b in bytes )
			hex.Append( System.Convert.ToString( b, 16 ).PadLeft( 2, '0' ) );
		return hex.ToString().ToUpper();
	}
	
	public bool CheckValid()
	{
		if( !System.IO.File.Exists( m_strPath ) )
		{
			return false;
		}
		return true;
	}
	
	public bool FetchRow()					{ return m_pSqlite.FetchRow(); }
	public int GetRowInt(int nRow)			{ return m_pSqlite.GetRowInt(nRow); }
	
	public string GetRowString(int nRow)	
	{
		string	str = m_pSqlite.GetRowString(nRow);
		str = str.Replace("\\n", "\n" );
		return str; 
	}
	
	public float GetRowFloat(int nRow)		{ return m_pSqlite.GetRowFloat(nRow); }
	public double GetRowDouble(int nRow)	{ return m_pSqlite.GetRowDouble(nRow); }
	public bool GetRowBoolean(int nRow)		{ return m_pSqlite.GetRowBoolean(nRow); }
	public void CloseParsing()				{ m_pSqlite.CloseDB(); }
}
