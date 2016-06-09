using UnityEngine;
using System;
using System.Collections;

using System.Data;
using Mono.Data;
using Mono.Data.Sqlite;
using Mono.Data.SqliteClient;

public class SqliteConnection
{
	private IDbConnection				m_dbConnection;
	private IDbCommand					m_dbCommand;
	private IDataReader					m_dbReader;
	
	public void OpenDB( string path )
	{
		CloseDB();
		string connectionString = "URI=file:" + path;
		
		m_dbConnection = (IDbConnection) new Mono.Data.Sqlite.SqliteConnection( connectionString );
		m_dbConnection.Close();
		m_dbConnection.Open();
		
		m_dbCommand = m_dbConnection.CreateCommand();
	}
	
	public void CloseDB()
	{
		if( m_dbReader != null )
		{
			m_dbReader.Close();
			m_dbReader = null;
		}
		
		if( m_dbCommand != null )
		{
			m_dbCommand.Dispose();
			m_dbCommand = null;
		}
		
		if( m_dbConnection != null )
		{
			m_dbConnection.Close();
			m_dbConnection = null;
		}
	}
	
	public bool Read( string query, bool bRead )
	{
		if( m_dbCommand == null )
			return false;
		m_dbCommand.Cancel();
		
		if( m_dbReader != null )
			m_dbReader.Close();
		m_dbReader = null;
		
		m_dbCommand.CommandText = query;
		
		bool bResult;
		try
		{
			m_dbReader = m_dbCommand.ExecuteReader();
			bResult = true;
			
			if( bRead )
			{
				bResult = m_dbReader.Read();
			}
		}
		catch( Exception e )
		{
			bResult = false;
		}
		
		return bResult;
	}
	
	public int GetRowInt(int nRow)
	{
		if( !HasData() )
			return 0;
		
		return m_dbReader.GetInt32( nRow );
	}
	
	public string GetRowString(int nRow)
	{
		if( !HasData() )
			return "";
		
		return m_dbReader.GetString( nRow ).Trim();
	}
	
	public float GetRowFloat(int nRow)
	{
		if( !HasData() )
			return 0;
		
		return float.Parse( m_dbReader.GetString( nRow ) );
	}
	
	public double GetRowDouble(int nRow)
	{
		if( !HasData() )
			return 0;
		
		return m_dbReader.GetDouble( nRow );
	}
	
	public bool GetRowBoolean(int nRow)
	{
		if( !HasData() )
			return false;
		
		return m_dbReader.GetBoolean( nRow );
	}
	
	public bool FetchRow()
	{
		if( HasData() )
			return m_dbReader.Read();
		return false;
	}
	
	public bool HasData()
	{
		if( m_dbReader == null )
			return false;
		if( m_dbReader.IsClosed )
			return false;
		if( m_dbReader.FieldCount == 0 )
			return false;
		return true;
	}
}

