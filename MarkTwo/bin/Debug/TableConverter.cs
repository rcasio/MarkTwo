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
		// Multilingual
		Table.Multilingual = new Dictionary<int, Multilingual>();
		BinaryReader multilingualBinaryReader = this.GetBinaryReader("Multilingual_Multilingual");

		for (int i = 0; i < 100; i++)
		{
			Multilingual multilingual = new Multilingual();

			multilingual.Num = MultilingualBinaryReader.ReadInt32();
			multilingual.Kor = MultilingualBinaryReader.ReadString();
			multilingual.Eng = MultilingualBinaryReader.ReadString();
			multilingual.Jpn = MultilingualBinaryReader.ReadString();
		}
		multilingualBinaryReader.Close();

		// PR
		Table.PR = new Dictionary<int, PR>();
		BinaryReader prBinaryReader = this.GetBinaryReader("PR_Client");

		for (int i = 0; i < 19; i++)
		{
			PR pr = new PR();

			pr.Num = PRBinaryReader.ReadInt32();
			pr.Type = PRBinaryReader.ReadString();
			pr.Key = PRBinaryReader.ReadString();
			pr.Value = PRBinaryReader.ReadString();
			pr.Comment = PRBinaryReader.ReadString();
		}
		prBinaryReader.Close();

		// NPC
		Table.NPC = new Dictionary<int, NPC>();
		BinaryReader npcBinaryReader = this.GetBinaryReader("NPC_Client");

		for (int i = 0; i < 23; i++)
		{
			NPC npc = new NPC();

			npc.Num = NPCBinaryReader.ReadInt32();
			npc.Property = NPCBinaryReader.ReadInt32();
			npc.Weapon = (TagManager.Table.PR.Weapon) Enum.Parse(typeof(TagManager.Table.PR.Weapon), NPCBinaryReader.ReadString());
			npc.Race = NPCBinaryReader.ReadInt32();
			npc.Grade = NPCBinaryReader.ReadByte();
			npc.Level = NPCBinaryReader.ReadByte();
			npc.HP = NPCBinaryReader.ReadInt32();
			npc.Exp = NPCBinaryReader.ReadInt32();
			npc.CLevelExp = NPCBinaryReader.ReadInt32();
			npc.AddSkill01 = NPCBinaryReader.ReadInt32();
			npc.RateSkill01 = NPCBinaryReader.ReadInt32();
			npc.AddSkill02 = NPCBinaryReader.ReadInt32();
			npc.RateSkill02 = NPCBinaryReader.ReadInt32();
			npc.AddSkill03 = NPCBinaryReader.ReadInt32();
			npc.RateSkill03 = NPCBinaryReader.ReadInt32();
			npc.AddSkill04 = NPCBinaryReader.ReadInt32();
			npc.RateSkill04 = NPCBinaryReader.ReadInt32();
			npc.AddSkill05 = NPCBinaryReader.ReadInt32();
			npc.RateSkill05 = NPCBinaryReader.ReadInt32();
			npc.MinGold = NPCBinaryReader.ReadInt32();
			npc.MaxGold = NPCBinaryReader.ReadInt32();
			npc.DropPosibility = NPCBinaryReader.ReadInt32();
			npc.Bag01 = NPCBinaryReader.ReadInt32();
			npc.Rate01 = NPCBinaryReader.ReadInt32();
			npc.Bag02 = NPCBinaryReader.ReadInt32();
			npc.Rate02 = NPCBinaryReader.ReadInt32();
			npc.Bag03 = NPCBinaryReader.ReadInt32();
			npc.Rate03 = NPCBinaryReader.ReadInt32();
			npc.Bag04 = NPCBinaryReader.ReadInt32();
			npc.Rate04 = NPCBinaryReader.ReadInt32();
			npc.Bag05 = NPCBinaryReader.ReadInt32();
			npc.Rate05 = NPCBinaryReader.ReadInt32();
			npc.NPC_FileName = NPCBinaryReader.ReadString();
			npc.NPC_Prefab = NPCBinaryReader.ReadString();
		}
		npcBinaryReader.Close();

		// Enchant
		Table.Enchant = new Dictionary<int, Enchant>();
		BinaryReader enchantBinaryReader = this.GetBinaryReader("Enchant_Client");

		for (int i = 0; i < 21; i++)
		{
			Enchant enchant = new Enchant();

			enchant.Num = EnchantBinaryReader.ReadInt32();
			enchant.Value = EnchantBinaryReader.ReadInt16();
			enchant.Grade = EnchantBinaryReader.ReadInt32();
			enchant.Rate = EnchantBinaryReader.ReadInt32();
			enchant.AtkPercentage = EnchantBinaryReader.ReadInt32();
			enchant.DefPercentage = EnchantBinaryReader.ReadInt32();
			enchant.AddCardOption = EnchantBinaryReader.ReadInt32();
			enchant.GamePrice = EnchantBinaryReader.ReadInt32();
			enchant.CashPrice = EnchantBinaryReader.ReadInt32();
		}
		enchantBinaryReader.Close();

		// Grade
		Table.Grade = new Dictionary<int, Grade>();
		BinaryReader gradeBinaryReader = this.GetBinaryReader("Grade_Client");

		for (int i = 0; i < 7; i++)
		{
			Grade grade = new Grade();

			grade.Num = GradeBinaryReader.ReadInt32();
			grade.Color_R = GradeBinaryReader.ReadByte();
			grade.Color_G = GradeBinaryReader.ReadByte();
			grade.Color_B = GradeBinaryReader.ReadByte();
			grade.Hex = GradeBinaryReader.ReadString();
			grade.Sounds = GradeBinaryReader.ReadString();
		}
		gradeBinaryReader.Close();

		// Tag
		Table.Tag = new Dictionary<int, Tag>();
		BinaryReader tagBinaryReader = this.GetBinaryReader("Tag_Client");

		for (int i = 0; i < 15; i++)
		{
			Tag tag = new Tag();

			tag.Num = TagBinaryReader.ReadInt32();
			tag.Type = TagBinaryReader.ReadString();
			tag.Item = TagBinaryReader.ReadString();
			tag.Weapon = TagBinaryReader.ReadString();
			tag.Armor = TagBinaryReader.ReadString();
		}
		tagBinaryReader.Close();

		// Map
		Table.Map = new Dictionary<int, Map>();
		BinaryReader mapBinaryReader = this.GetBinaryReader("Map_Client");

		for (int i = 0; i < 4; i++)
		{
			Map map = new Map();

			map.Num = MapBinaryReader.ReadInt32();
			map.ChapterNum = MapBinaryReader.ReadInt32();
			map.MapNum = MapBinaryReader.ReadInt32();
			map.Difficulty = MapBinaryReader.ReadByte();
			map.Attribute = MapBinaryReader.ReadInt32();
			map.NeedAction = MapBinaryReader.ReadByte();
			map.WeaponShopNum = MapBinaryReader.ReadInt32();
			map.ArmorShopNum = MapBinaryReader.ReadInt32();
			map.PotionShopNum = MapBinaryReader.ReadInt32();
			map.NPCBag01 = MapBinaryReader.ReadInt32();
			map.NPCBag02 = MapBinaryReader.ReadInt32();
			map.NPCBag03 = MapBinaryReader.ReadInt32();
		}
		mapBinaryReader.Close();

    }
}
