using UnityEngine;
using System.Collections;
using ICSharpCode;



public class SharpZipLib : MonoBehaviour
{

	// Use this for initialization
	public void Start()
	{

	}

	// Update is called once per frame
	//function Update ()
	//{
	//
	//}

	public void Compress(string OutputZipFileName, System.IO.Stream InputStream)
	{
	
		System.IO.FileStream raw = System.IO.File.Create(OutputZipFileName);
		ICSharpCode.SharpZipLib.Zip.ZipOutputStream Compressor = new ICSharpCode.SharpZipLib.Zip.ZipOutputStream(raw);
		Compressor.PutNextEntry(new ICSharpCode.SharpZipLib.Zip.ZipEntry("Entry"));

		byte[] buf = new byte[1024];
		int n = 0;

		while (true)
		{
			n = InputStream.Read(buf, 0, buf.Length);

			if (n == 0)
			{
				break;
			}
			else
			{
				Compressor.Write(buf, 0, n);
			}
		}

		Compressor.Close();
		raw.Close();

		//delete(Compressor);
		//delete(buf);
	}


	public void Compress(System.IO.Stream OutputStream, System.IO.Stream InputStream)
	{
		ICSharpCode.SharpZipLib.Zip.ZipOutputStream Compressor = new ICSharpCode.SharpZipLib.Zip.ZipOutputStream(OutputStream);
		Compressor.PutNextEntry(new ICSharpCode.SharpZipLib.Zip.ZipEntry("Entry"));

		byte[] buf = new byte[1024];
		int n = 0;

		while (true)
		{
			n = InputStream.Read(buf, 0, buf.Length);

			if (n == 0)
			{
				break;
			}
			else
			{
				Compressor.Write(buf, 0, n);
			}
		}

		Compressor.Flush();
		Compressor.Close();

		//delete(Compressor);
		//delete(buf);

		//OutputStream.Seek(0, System.IO.SeekOrigin.Begin);
	}


	public void Decompress(string OutputFileName, string ZlibFileName)
	{
		System.IO.FileStream raw = System.IO.File.OpenRead(ZlibFileName);
		ICSharpCode.SharpZipLib.Zip.ZipInputStream Decompressor = new ICSharpCode.SharpZipLib.Zip.ZipInputStream(raw);
		Decompressor.GetNextEntry();

		System.IO.FileStream output = System.IO.File.Create(OutputFileName);

		byte[] buf = new byte[1024];
		int n = 0;

		while (true)
		{
			n = Decompressor.Read(buf, 0, buf.Length);

			if (n == 0)
			{
				break;
			}
			else
			{
				output.Write(buf, 0, n);
			}
		}

		output.Close();

		Decompressor.Close();
		raw.Close();
		//delete(Decompressor);
		//delete(buf);
	}

	public void Decompress(System.IO.Stream OutputStream, System.IO.Stream InputStream)
	{
		ICSharpCode.SharpZipLib.Zip.ZipInputStream Decompressor = new ICSharpCode.SharpZipLib.Zip.ZipInputStream(InputStream);
		Decompressor.GetNextEntry();

		byte[] buf = new byte[1024];
		int n = 0;

		while (true)
		{
			n = Decompressor.Read(buf, 0, buf.Length);

			if (n == 0)
			{
				break;
			}
			else
			{
				OutputStream.Write(buf, 0, n);
			}
		}

		OutputStream.Seek(0, System.IO.SeekOrigin.Begin);
		Decompressor.Close();
		//delete(Decompressor);
		//delete(buf);
	}

}
