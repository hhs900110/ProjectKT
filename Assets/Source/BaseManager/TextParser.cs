using UnityEngine;
using System.Collections;

public class TextParser
{
	private TextAsset textFile;
	private int StringMaxNum;
	private System.IO.StringReader ReadBuff;
	private string[] Buff;
	private string ReadLine;
	private char[] WhiteCode;
	public int INVALID_VALUE;
	
	public void Craete()
	{
	}
	
	public void Open(string FileName)
	{
		textFile = null; 
		ReadBuff = null;
		ReadLine = null;
		StringMaxNum = 128;
		WhiteCode = new char[5];
		WhiteCode[0] = '\n';
		WhiteCode[1] = '\r';
		WhiteCode[2] = '\t';
		WhiteCode[3] = ',';
		INVALID_VALUE = -9999;
		
		Buff = new string[StringMaxNum];
		textFile = (TextAsset)Resources.Load(FileName, typeof(TextAsset));
		ReadBuff = new System.IO.StringReader(textFile.text);
	}
	
	public bool GetTokenLineString()
	{
		for (int i = 0; i < StringMaxNum; i++)
		{
			if (Buff[i] != null)
			{
				return true;
			}
		}
		ReadLine = null;
		if (ReadLine == null)
		{
			var IsReadLine = true;
			while (IsReadLine)
			{
				ReadLine = ReadBuff.ReadLine();
				if (ReadLine == null)
				{
					return false;
				}
				else if (ReadLine.Length > 1)
				{
					if (ReadLine != "" && ReadLine[0] != '/' && ReadLine[1] != '/' && ReadLine[0] != '\t')
					{
						IsReadLine = false;
					}
				}
			}
			CheckWhiteCode();
			if (ReadLine == null)
			{
				return false;
			}
		}
		return true;
	}
	
	public void CheckWhiteCode()
	{
		int		Count = 0;
		string	NowBuff = null;
		bool	bIsNext = false;
		
		for(int i = 0; i < ReadLine.Length; i++)
		{
			bool AddBuff = false;
			if(bIsNext)
			{
				bIsNext = false;
			}
			else if (ReadLine[i] == ' ')
			{
				AddBuff = true;
			}
			else if (ReadLine[i] == '\t')
			{
				AddBuff = true;
			}
			else if (ReadLine[i] == '\\')
			{
				NowBuff += "\n";
				bIsNext = true;
			}
			else
			{
				NowBuff += ReadLine[i];
			}
			
			if (AddBuff)
			{
				if (NowBuff != null)
				{
					Buff[Count] = NowBuff;
					Count++;
					NowBuff = null;
				}
			}
		}
		if (NowBuff != null)
		{
			Buff[Count] = NowBuff;
		}
	}

	public void DeleteString()
	{
		for (int i = 0; i < StringMaxNum; i++)
		{
			if (Buff[i] != null)
			{
				Buff[i] = null;
				return;
			}
		}
	}
	
	public string GetTokenString()
	{
		if (GetTokenLineString())
		{
			for (int i = 0; i < StringMaxNum; i++)
			{
				if (Buff[i] != null)
				{
					return Buff[i];
				}
			}
		}
		else
		{
			for (int i = 0; i < StringMaxNum; i++)
			{
				if (Buff[i] != null)
				{
					return Buff[i];
				}
			}
		}
		return null;
	}
	
	public int GetTokenInt()
	{
		string buff = null;
		buff = GetTokenString();
		DeleteString();
		
		if (buff != null)
		{
			int Check = buff[0];
			if (Check == 45 || Check >= 48 && Check <= 57)
			{
				return int.Parse(buff);
			}
			return INVALID_VALUE;
		}
		return INVALID_VALUE;
	}
	
	public string GetTokenChar()
	{
		string buff = null;
		buff = GetTokenString();
		
		DeleteString();
		if (buff != null)
		{
			return buff;
		}
		else
		{
			return buff;
		}
	}
	
	public float GetTokenReal()
	{
		string buff = null;
		buff = GetTokenString();
		DeleteString();
		
		if (buff != null)
		{
			return float.Parse(buff);
		}
		
		return INVALID_VALUE;
	}
	
	public string GetTokenSpeech()
	{
		string buff = null;
		bool IsReadLine = true;
		while (IsReadLine)
		{
			ReadLine = ReadBuff.ReadLine();
			CheckWhiteCode();
			if (ReadLine == null)
			{
				return null;
			}
			else if (Buff[0] == "END")
			{
				for (int i = 0; i < StringMaxNum; i++)
				{
					if (Buff[i] != null)
					{
						Buff[i] = null;
					}
				}
				return buff;
			}
			for(int i = 0; i < StringMaxNum; i++)
			{
				if(Buff[i] != null)
				{
					if(Buff[i + 1] != null)
					{
						buff += Buff[i] + " ";
					}
					else
					{
						buff += Buff[i];
						Buff[i] = null;
						break;
					}
					Buff[i] = null;
				}
			}
		}
		return null;
	}
	
	public void Close()
	{
		ReadBuff.Close();
	}
}