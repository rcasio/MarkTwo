using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class Table
{
	public static Dictionary<int, Multilingual> Multilingual;
	public static Dictionary<int, PR> PR;
	public static Dictionary<int, NPC> NPC;
	public static Dictionary<int, Enchant> Enchant;
	public static Dictionary<int, Grade> Grade;
	public static Dictionary<int, Tag> Tag;
	public static Dictionary<int, Map> Map;
}

public class TableLoad
{
	private BinaryReader GetBinaryReader(string fileName)
	{
		TextAsset textasset = Resources.Load(fileName) as TextAsset;
		MemoryStream stream = new MemoryStream(textasset.bytes);
		BinaryReader binaryReader = new BinaryReader(stream);
		stream.Close();

		return binaryReader;
	}

	public TableLoad()
	{
    }
}
