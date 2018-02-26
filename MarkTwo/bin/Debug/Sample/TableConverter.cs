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
	public static Dictionary<int, Enchant> Enchant;
	public static Dictionary<int, Grade> Grade;
	public static Dictionary<int, Tag> Tag;
	public static Dictionary<int, Map> Map;
}

public class TableLoad
{
	private BinaryReader GetBinaryReader(string fileName, out MemoryStream stream)
	{
		string tableDBPath = null;
		if (Application.platform == RuntimePlatform.Android) { tableDBPath = "jar: file://" + Application.dataPath + "!/assets/" + fileName + ".bytes"; } // Android Path
		else if (Application.platform == RuntimePlatform.IPhonePlayer) { tableDBPath = "file://" + Application.dataPath + "/Raw/" + fileName + ".bytes"; } // IOS Path
		else { tableDBPath = "file://" + Application.dataPath + "/StreamingAssets/" + fileName + ".bytes"; } // Editor PAth

		WWW www = new WWW(tableDBPath);;

		stream = new MemoryStream(www.bytes);
		BinaryReader binaryReader = new BinaryReader(stream);
		return binaryReader;
	}

	public TableLoad()
	{
		MemoryStream stream;

		// Multilingual
		Table.Multilingual = new Dictionary<int, Multilingual>();
		BinaryReader multilingualBinaryReader = this.GetBinaryReader("Multilingual_Multilingual", out stream);

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
		Table.PR = new Dictionary<int, PR>();
		BinaryReader prBinaryReader = this.GetBinaryReader("PR_Client", out stream);

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
		Table.NPC = new Dictionary<int, NPC>();
		BinaryReader npcBinaryReader = this.GetBinaryReader("NPC_Client", out stream);

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

		// Enchant
		Table.Enchant = new Dictionary<int, Enchant>();
		BinaryReader enchantBinaryReader = this.GetBinaryReader("Enchant_Client", out stream);

		for (int i = 0; i < 21; i++)
		{
			Enchant enchant = new Enchant();

			enchant.Num = enchantBinaryReader.ReadInt32();
			enchant.Value = enchantBinaryReader.ReadInt16();
			enchant.Grade = enchantBinaryReader.ReadInt32();
			enchant.Rate = enchantBinaryReader.ReadInt32();
			enchant.AtkPercentage = enchantBinaryReader.ReadInt32();
			enchant.DefPercentage = enchantBinaryReader.ReadInt32();
			enchant.AddCardOption = enchantBinaryReader.ReadInt32();
			enchant.GamePrice = enchantBinaryReader.ReadInt32();
			enchant.CashPrice = enchantBinaryReader.ReadInt32();

			Table.Enchant.Add(enchant.Num, enchant);
		}
		enchantBinaryReader.Close();
		stream.Close();

		// Grade
		Table.Grade = new Dictionary<int, Grade>();
		BinaryReader gradeBinaryReader = this.GetBinaryReader("Grade_Client", out stream);

		for (int i = 0; i < 7; i++)
		{
			Grade grade = new Grade();

			grade.Num = gradeBinaryReader.ReadInt32();
			grade.Color_R = gradeBinaryReader.ReadByte();
			grade.Color_G = gradeBinaryReader.ReadByte();
			grade.Color_B = gradeBinaryReader.ReadByte();
			grade.Hex = gradeBinaryReader.ReadString();
			grade.Sounds = gradeBinaryReader.ReadString();

			Table.Grade.Add(grade.Num, grade);
		}
		gradeBinaryReader.Close();
		stream.Close();

		// Tag
		Table.Tag = new Dictionary<int, Tag>();
		BinaryReader tagBinaryReader = this.GetBinaryReader("Tag_Client", out stream);

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
		Table.Map = new Dictionary<int, Map>();
		BinaryReader mapBinaryReader = this.GetBinaryReader("Map_Client", out stream);

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

    }
}
