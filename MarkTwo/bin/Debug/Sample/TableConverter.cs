using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;

public class Table
{
	public static Dictionary<int, Multilingual> Multilingual;
	public static Dictionary<int, PR> PR;
	public static Dictionary<int, NPC> NPC;
	public static Dictionary<int, Tag> Tag;
	public static Dictionary<int, Map> Map;
	public static Dictionary<int, Enchant> Enchant;
	public static Dictionary<int, Grade> Grade;
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

		// NPC
		www = new WWW(this.SetPath("NPC_Client"));
		yield return www;

		stream = new MemoryStream(www.bytes);
		BinaryReader npcBinaryReader = new BinaryReader(stream);

		Table.NPC = new Dictionary<int, NPC>();

		for (int i = 0; i < 23; i++)
		{
			NPC npc = new NPC();

			npc.Num = npcBinaryReader.ReadInt32();
			npc.Property = npcBinaryReader.ReadInt32();
			npc.Weapon = (TagManager.Table.Tag.Weapon) Enum.Parse(typeof(TagManager.Table.Tag.Weapon), npcBinaryReader.ReadString());
			npc.Race = npcBinaryReader.ReadInt32();
			npc.Grade = npcBinaryReader.ReadByte();
			npc.Level = npcBinaryReader.ReadByte();
			npc.HP = npcBinaryReader.ReadInt32();
			npc.Exp = npcBinaryReader.ReadInt32();
			npc.CLevelExp = npcBinaryReader.ReadInt32();
			npc.AddSkill01 = npcBinaryReader.ReadInt32();
			npc.RateSkill01 = npcBinaryReader.ReadInt32();
			npc.AddSkill02 = npcBinaryReader.ReadInt32();
			npc.RateSkill02 = npcBinaryReader.ReadInt32();
			npc.AddSkill03 = npcBinaryReader.ReadInt32();
			npc.RateSkill03 = npcBinaryReader.ReadInt32();
			npc.AddSkill04 = npcBinaryReader.ReadInt32();
			npc.RateSkill04 = npcBinaryReader.ReadInt32();
			npc.AddSkill05 = npcBinaryReader.ReadInt32();
			npc.RateSkill05 = npcBinaryReader.ReadInt32();
			npc.MinGold = npcBinaryReader.ReadInt32();
			npc.MaxGold = npcBinaryReader.ReadInt32();
			npc.DropPosibility = npcBinaryReader.ReadInt32();
			npc.Bag01 = npcBinaryReader.ReadInt32();
			npc.Rate01 = npcBinaryReader.ReadInt32();
			npc.Bag02 = npcBinaryReader.ReadInt32();
			npc.Rate02 = npcBinaryReader.ReadInt32();
			npc.Bag03 = npcBinaryReader.ReadInt32();
			npc.Rate03 = npcBinaryReader.ReadInt32();
			npc.Bag04 = npcBinaryReader.ReadInt32();
			npc.Rate04 = npcBinaryReader.ReadInt32();
			npc.Bag05 = npcBinaryReader.ReadInt32();
			npc.Rate05 = npcBinaryReader.ReadInt32();
			npc.NPC_FileName = npcBinaryReader.ReadString();
			npc.NPC_Prefab = npcBinaryReader.ReadString();

			Table.NPC.Add(npc.Num, npc);
		}
		npcBinaryReader.Close();
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

		// Map
		www = new WWW(this.SetPath("Map_Client"));
		yield return www;

		stream = new MemoryStream(www.bytes);
		BinaryReader mapBinaryReader = new BinaryReader(stream);

		Table.Map = new Dictionary<int, Map>();

		for (int i = 0; i < 4; i++)
		{
			Map map = new Map();

			map.Num = mapBinaryReader.ReadInt32();
			map.ChapterNum = mapBinaryReader.ReadInt32();
			map.MapNum = mapBinaryReader.ReadInt32();
			map.Difficulty = mapBinaryReader.ReadByte();
			map.Attribute = mapBinaryReader.ReadInt32();
			map.NeedAction = mapBinaryReader.ReadByte();
			map.WeaponShopNum = mapBinaryReader.ReadInt32();
			map.ArmorShopNum = mapBinaryReader.ReadInt32();
			map.PotionShopNum = mapBinaryReader.ReadInt32();
			map.NPCBag01 = mapBinaryReader.ReadInt32();
			map.NPCBag02 = mapBinaryReader.ReadInt32();
			map.NPCBag03 = mapBinaryReader.ReadInt32();

			Table.Map.Add(map.Num, map);
		}
		mapBinaryReader.Close();
		stream.Close();

		this.isLoad = true;
    }
}
