using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine.Networking;
using System;
using TableDB;

public class Table
{
	public static Dictionary<int, TableDB.Multilingual> Multilingual;
	public static Dictionary<int, TableDB.PR> PR;
	public static Dictionary<int, TableDB.Tag> Tag;
	public static Dictionary<int, TableDB.NPC> NPC;
	public static Dictionary<int, TableDB.Map> Map;
	public static Dictionary<int, TableDB.Enchant> Enchant;
	public static Dictionary<int, TableDB.Grade> Grade;
	public static Dictionary<int, TableDB.ConquerorLevel> ConquerorLevel;
	public static Dictionary<int, TableDB.Test01> Test01;
	public static Dictionary<int, TableDB.Test02> Test02;
	public static Dictionary<int, TableDB.Test03> Test03;
}

public class TableLoad
{
	public bool isLoad = false;

	private string SetPath(string fileName)
	{
		string tableDBPath = null;

		if (Application.platform == RuntimePlatform.Android) { tableDBPath = "jar:file://" + Application.dataPath + "!/assets/" + fileName + ".bytes"; } // Android Path
		else if (Application.platform == RuntimePlatform.IPhonePlayer) { tableDBPath = "file://" + Application.dataPath + "/Raw/" + fileName + ".bytes"; } // IOS Path
		else { tableDBPath = Path.Combine(Application.streamingAssetsPath, fileName + ".bytes"); } // Editor PAth

		return tableDBPath;
	}

	public IEnumerator Load()
	{
		MemoryStream stream;
		UnityWebRequest www;

		// Multilingual
		www = UnityWebRequest.Get(this.SetPath("Multilingual_Multilingual"));
		yield return www.SendWebRequest();

		stream = new MemoryStream(www.downloadHandler.data);
		BinaryReader multilingualBinaryReader = new BinaryReader(stream);

		Table.Multilingual = new Dictionary<int, TableDB.Multilingual > ();

		for (int i = 0; i < 100; i++)
		{
			TableDB.Multilingual multilingual = new TableDB.Multilingual();

			multilingual.Num = multilingualBinaryReader.ReadInt32();
			multilingual.Kor = multilingualBinaryReader.ReadString();
			multilingual.Eng = multilingualBinaryReader.ReadString();
			multilingual.Jpn = multilingualBinaryReader.ReadString();

			Table.Multilingual.Add(multilingual.Num, multilingual);
		}
		multilingualBinaryReader.Close();
		stream.Close();

		// PR
		www = UnityWebRequest.Get(this.SetPath("PR_Client"));
		yield return www.SendWebRequest();

		stream = new MemoryStream(www.downloadHandler.data);
		BinaryReader prBinaryReader = new BinaryReader(stream);

		Table.PR = new Dictionary<int, TableDB.PR > ();

		for (int i = 0; i < 19; i++)
		{
			TableDB.PR pr = new TableDB.PR();

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
		www = UnityWebRequest.Get(this.SetPath("Tag_Client"));
		yield return www.SendWebRequest();

		stream = new MemoryStream(www.downloadHandler.data);
		BinaryReader tagBinaryReader = new BinaryReader(stream);

		Table.Tag = new Dictionary<int, TableDB.Tag > ();

		for (int i = 0; i < 15; i++)
		{
			TableDB.Tag tag = new TableDB.Tag();

			tag.Num = tagBinaryReader.ReadInt32();
			tag.Type = tagBinaryReader.ReadString();
			tag.Item = tagBinaryReader.ReadString();
			tag.Weapon = tagBinaryReader.ReadString();
			tag.Armor = tagBinaryReader.ReadString();

			Table.Tag.Add(tag.Num, tag);
		}
		tagBinaryReader.Close();
		stream.Close();

		// NPC
		www = UnityWebRequest.Get(this.SetPath("NPC_Client"));
		yield return www.SendWebRequest();

		stream = new MemoryStream(www.downloadHandler.data);
		BinaryReader npcBinaryReader = new BinaryReader(stream);

		Table.NPC = new Dictionary<int, TableDB.NPC > ();

		for (int i = 0; i < 23; i++)
		{
			TableDB.NPC npc = new TableDB.NPC();

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

		// Map
		www = UnityWebRequest.Get(this.SetPath("Map_Client"));
		yield return www.SendWebRequest();

		stream = new MemoryStream(www.downloadHandler.data);
		BinaryReader mapBinaryReader = new BinaryReader(stream);

		Table.Map = new Dictionary<int, TableDB.Map > ();

		for (int i = 0; i < 4; i++)
		{
			TableDB.Map map = new TableDB.Map();

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

		// Enchant
		www = UnityWebRequest.Get(this.SetPath("Enchant_Client"));
		yield return www.SendWebRequest();

		stream = new MemoryStream(www.downloadHandler.data);
		BinaryReader enchantBinaryReader = new BinaryReader(stream);

		Table.Enchant = new Dictionary<int, TableDB.Enchant > ();

		for (int i = 0; i < 21; i++)
		{
			TableDB.Enchant enchant = new TableDB.Enchant();

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
		www = UnityWebRequest.Get(this.SetPath("Grade_Client"));
		yield return www.SendWebRequest();

		stream = new MemoryStream(www.downloadHandler.data);
		BinaryReader gradeBinaryReader = new BinaryReader(stream);

		Table.Grade = new Dictionary<int, TableDB.Grade > ();

		for (int i = 0; i < 7; i++)
		{
			TableDB.Grade grade = new TableDB.Grade();

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

		// ConquerorLevel
		www = UnityWebRequest.Get(this.SetPath("ConquerorLevel_Client"));
		yield return www.SendWebRequest();

		stream = new MemoryStream(www.downloadHandler.data);
		BinaryReader conquerorlevelBinaryReader = new BinaryReader(stream);

		Table.ConquerorLevel = new Dictionary<int, TableDB.ConquerorLevel > ();

		for (int i = 0; i < 20; i++)
		{
			TableDB.ConquerorLevel conquerorlevel = new TableDB.ConquerorLevel();

			conquerorlevel.Num = conquerorlevelBinaryReader.ReadInt32();
			conquerorlevel.Level = conquerorlevelBinaryReader.ReadSingle();
			conquerorlevel.NeedCLevelExp = conquerorlevelBinaryReader.ReadInt32();
			conquerorlevel.Strength = conquerorlevelBinaryReader.ReadByte();
			conquerorlevel.Dexterity = conquerorlevelBinaryReader.ReadByte();
			conquerorlevel.Constitution = conquerorlevelBinaryReader.ReadByte();
			conquerorlevel.Intelligent = conquerorlevelBinaryReader.ReadByte();
			conquerorlevel.HPBase = conquerorlevelBinaryReader.ReadInt32();
			conquerorlevel.MPBase = conquerorlevelBinaryReader.ReadInt32();
			conquerorlevel.MPRegen = conquerorlevelBinaryReader.ReadInt32();
			conquerorlevel.NoneAtk = conquerorlevelBinaryReader.ReadInt32();
			conquerorlevel.FireAtk = conquerorlevelBinaryReader.ReadInt32();
			conquerorlevel.WaterAtk = conquerorlevelBinaryReader.ReadInt32();
			conquerorlevel.NatureAtk = conquerorlevelBinaryReader.ReadInt32();
			conquerorlevel.LightAtk = conquerorlevelBinaryReader.ReadInt32();
			conquerorlevel.DarkAtk = conquerorlevelBinaryReader.ReadInt32();
			conquerorlevel.NoneDef = conquerorlevelBinaryReader.ReadInt32();
			conquerorlevel.FireDef = conquerorlevelBinaryReader.ReadInt32();
			conquerorlevel.WaterDef = conquerorlevelBinaryReader.ReadInt32();
			conquerorlevel.NatureDef = conquerorlevelBinaryReader.ReadInt32();
			conquerorlevel.LightDef = conquerorlevelBinaryReader.ReadInt32();
			conquerorlevel.DarkDef = conquerorlevelBinaryReader.ReadInt32();

			Table.ConquerorLevel.Add(conquerorlevel.Num, conquerorlevel);
		}
		conquerorlevelBinaryReader.Close();
		stream.Close();

		// Test01
		www = UnityWebRequest.Get(this.SetPath("Test01_Client"));
		yield return www.SendWebRequest();

		stream = new MemoryStream(www.downloadHandler.data);
		BinaryReader test01BinaryReader = new BinaryReader(stream);

		Table.Test01 = new Dictionary<int, TableDB.Test01 > ();

		for (int i = 0; i < 23; i++)
		{
			TableDB.Test01 test01 = new TableDB.Test01();

			test01.Num = test01BinaryReader.ReadInt32();
			test01.test = test01BinaryReader.ReadInt32();
			test01.test22 = test01BinaryReader.ReadInt32();
			test01.gashaponID_1_prob = test01BinaryReader.ReadInt32();
			test01.gashaponID_1_minSelectCount = test01BinaryReader.ReadInt32();
			test01.gashaponID_1_maxSelectCount = test01BinaryReader.ReadInt32();
			test01.gashaponID_2 = test01BinaryReader.ReadInt32();
			test01.gashaponID_2_prob = test01BinaryReader.ReadInt32();
			test01.gashaponID_2_minSelectCount = test01BinaryReader.ReadInt32();
			test01.gashaponID_2_maxSelectCount = test01BinaryReader.ReadInt32();
			test01.gashaponID_3 = test01BinaryReader.ReadInt32();
			test01.gashaponID_3_prob = test01BinaryReader.ReadInt32();
			test01.gashaponID_3_minSelectCount = test01BinaryReader.ReadInt32();
			test01.gashaponID_3_maxSelectCount = test01BinaryReader.ReadInt32();
			test01.gashaponID_4 = test01BinaryReader.ReadInt32();
			test01.gashaponID_4_prob = test01BinaryReader.ReadInt32();
			test01.gashaponID_4_minSelectCount = test01BinaryReader.ReadInt32();
			test01.gashaponID_4_maxSelectCount = test01BinaryReader.ReadInt32();
			test01.gashaponID_5 = test01BinaryReader.ReadInt32();
			test01.gashaponID_5_prob = test01BinaryReader.ReadInt32();
			test01.gashaponID_5_minSelectCount = test01BinaryReader.ReadInt32();
			test01.gashaponID_5_maxSelectCount = test01BinaryReader.ReadInt32();
			test01.gashaponID_6 = test01BinaryReader.ReadInt32();
			test01.gashaponID_6_prob = test01BinaryReader.ReadInt32();
			test01.gashaponID_6_minSelectCount = test01BinaryReader.ReadInt32();
			test01.gashaponID_6_maxSelectCount = test01BinaryReader.ReadInt32();
			test01.gashaponID_7 = test01BinaryReader.ReadInt32();
			test01.gashaponID_7_prob = test01BinaryReader.ReadInt32();
			test01.gashaponID_7_minSelectCount = test01BinaryReader.ReadInt32();
			test01.gashaponID_7_maxSelectCount = test01BinaryReader.ReadInt32();
			test01.gashaponID_8 = test01BinaryReader.ReadInt32();
			test01.gashaponID_8_prob = test01BinaryReader.ReadInt32();
			test01.gashaponID_8_minSelectCount = test01BinaryReader.ReadInt32();
			test01.gashaponID_8_maxSelectCount = test01BinaryReader.ReadInt32();
			test01.gashaponID_9 = test01BinaryReader.ReadInt32();
			test01.gashaponID_9_prob = test01BinaryReader.ReadInt32();
			test01.gashaponID_9_minSelectCount = test01BinaryReader.ReadInt32();
			test01.gashaponID_9_maxSelectCount = test01BinaryReader.ReadInt32();
			test01.gashaponID_10 = test01BinaryReader.ReadInt32();
			test01.gashaponID_10_prob = test01BinaryReader.ReadInt32();
			test01.gashaponID_10_minSelectCount = test01BinaryReader.ReadInt32();
			test01.gashaponID_10_maxSelectCount = test01BinaryReader.ReadInt32();

			Table.Test01.Add(test01.Num, test01);
		}
		test01BinaryReader.Close();
		stream.Close();

		// Test02
		www = UnityWebRequest.Get(this.SetPath("Test02_Client"));
		yield return www.SendWebRequest();

		stream = new MemoryStream(www.downloadHandler.data);
		BinaryReader test02BinaryReader = new BinaryReader(stream);

		Table.Test02 = new Dictionary<int, TableDB.Test02 > ();

		for (int i = 0; i < 3; i++)
		{
			TableDB.Test02 test02 = new TableDB.Test02();

			test02.Num = test02BinaryReader.ReadInt32();
			test02.Test2 = test02BinaryReader.ReadSingle();
			test02.Test3 = test02BinaryReader.ReadInt32();

			Table.Test02.Add(test02.Num, test02);
		}
		test02BinaryReader.Close();
		stream.Close();

		// Test03
		www = UnityWebRequest.Get(this.SetPath("Test03_Client"));
		yield return www.SendWebRequest();

		stream = new MemoryStream(www.downloadHandler.data);
		BinaryReader test03BinaryReader = new BinaryReader(stream);

		Table.Test03 = new Dictionary<int, TableDB.Test03 > ();

		for (int i = 0; i < 3; i++)
		{
			TableDB.Test03 test03 = new TableDB.Test03();

			test03.Num = test03BinaryReader.ReadInt32();
			test03.Test2 = test03BinaryReader.ReadSingle();
			test03.Test3 = test03BinaryReader.ReadInt32();

			Table.Test03.Add(test03.Num, test03);
		}
		test03BinaryReader.Close();
		stream.Close();

		this.isLoad = true;
    }
}
