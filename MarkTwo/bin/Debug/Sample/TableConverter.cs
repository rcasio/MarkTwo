using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;

public class Table
{
	public static Dictionary<int, Multilingual> Multilingual;
	public static Dictionary<int, PR> PR;
	public static Dictionary<int, Tag> Tag;
	public static Dictionary<int, NPC> NPC;
	public static Dictionary<int, Map> Map;
	public static Dictionary<int, Enchant> Enchant;
	public static Dictionary<int, Grade> Grade;
	public static Dictionary<int, ConquerorLevel> ConquerorLevel;
	public static Dictionary<int, Test01> Test01;
	public static Dictionary<int, Test02> Test02;
	public static Dictionary<int, Test03> Test03;
}

public class TableLoad
{
	public bool isLoad = false;

	private string SetPath(string fileName)
	{
		string tableDBPath = null;

		if (Application.platform == RuntimePlatform.Android) { tableDBPath = "jar:file://" + Application.dataPath + "!/assets/" + fileName + ".bytes"; } // Android Path
		else if (Application.platform == RuntimePlatform.IPhonePlayer) { tableDBPath = "file://" + Application.dataPath + "/Raw/" + fileName + ".bytes"; } // IOS Path
		else { tableDBPath = "file://" + Application.dataPath + "/StreamingAssets/" + fileName + ".bytes"; } // Editor PAth

		return tableDBPath;
	}

	public IEnumerator Load()
	{
		MemoryStream stream;
		WWW www;

		// Multilingual
		www = new WWW(this.SetPath("Multilingual_Multilingual"));
		yield return www;

		stream = new MemoryStream(www.bytes);
		BinaryReader multilingualBinaryReader = new BinaryReader(stream);

		Table.Multilingual = new Dictionary<int, Multilingual>();

		for (int i = 0; i < 100; i++)
		{
			Multilingual multilingual = new Multilingual();

			multilingual.Num = multilingualBinaryReader.ReadInt32();
			multilingual.Kor = multilingualBinaryReader.ReadString();
			multilingual.Eng = multilingualBinaryReader.ReadString();
			multilingual.Jpn = multilingualBinaryReader.ReadString();

			Table.Multilingual.Add(multilingual.Num, multilingual);
		}
		multilingualBinaryReader.Close();
		stream.Close();

		// PR
		www = new WWW(this.SetPath("PR_Client"));
		yield return www;

		stream = new MemoryStream(www.bytes);
		BinaryReader prBinaryReader = new BinaryReader(stream);

		Table.PR = new Dictionary<int, PR>();

		for (int i = 0; i < 19; i++)
		{
			PR pr = new PR();

			pr.Num = prBinaryReader.ReadInt32();
			pr.Type = prBinaryReader.ReadString();
			pr.Key = prBinaryReader.ReadString();
			pr.Value = prBinaryReader.ReadString();
			pr.Comment = prBinaryReader.ReadString();

			Table.PR.Add(pr.Num, pr);
		}
		prBinaryReader.Close();
		stream.Close();

		// Tag
		www = new WWW(this.SetPath("Tag_Client"));
		yield return www;

		stream = new MemoryStream(www.bytes);
		BinaryReader tagBinaryReader = new BinaryReader(stream);

		Table.Tag = new Dictionary<int, Tag>();

		for (int i = 0; i < 15; i++)
		{
			Tag tag = new Tag();

			tag.Num = tagBinaryReader.ReadInt32();
			tag.Type = tagBinaryReader.ReadString();
			tag.Item = tagBinaryReader.ReadString();
			tag.Weapon = tagBinaryReader.ReadString();
			tag.Armor = tagBinaryReader.ReadString();

			Table.Tag.Add(tag.Num, tag);
		}
		tagBinaryReader.Close();
		stream.Close();

		this.isLoad = true;
    }
}
