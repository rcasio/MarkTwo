using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

  public class Table
  {
    public static Dictionary<int, Multilingual> Data_Multilingual;
    public static Dictionary<int, PR> Data_PR;
    public static Dictionary<int, Tag> Data_Tag;
    public static Dictionary<int, NPC> Data_NPC;
  }

  public class Table_Load : Singleton<Table_Load>
  {
    BinaryReader m_TableDataBinary;
    TextAsset m_Textasset;

    Multilingual row_Multilingual;
    PR row_PR;
    Tag row_Tag;
    NPC row_NPC;

    public void Load_TableDB(string setLanguage = null, byte[] bytes = null)
    {
        MemoryStream stream;

        if (bytes != null)
        {
            stream = new MemoryStream(bytes);
        }
        else
        {
            if (!(Table.Data_PR == null)) return;

            m_Textasset = Resources.Load("TableDB") as TextAsset;
            stream = new MemoryStream(m_Textasset.bytes);
        }

       m_TableDataBinary = new BinaryReader(stream);

      Table.Data_Multilingual = new Dictionary<int, Multilingual>(100);

      for ( int i = 0; i < 100 ; i++ )
      {
          row_Multilingual = new Multilingual();

          row_Multilingual.Num = m_TableDataBinary.ReadInt32();
          row_Multilingual.Kor = m_TableDataBinary.ReadString();
          row_Multilingual.Eng = m_TableDataBinary.ReadString();
          row_Multilingual.Jpn = m_TableDataBinary.ReadString();

          Table.Data_Multilingual.Add(row_Multilingual.Num, row_Multilingual);

      }

      Table.Data_PR = new Dictionary<int, PR>(19);

      for ( int i = 0; i < 19 ; i++ )
      {
          row_PR = new PR();

          row_PR.Num = m_TableDataBinary.ReadInt32();
          row_PR.Type = m_TableDataBinary.ReadString();
          row_PR.Key = m_TableDataBinary.ReadString();
          row_PR.Value = m_TableDataBinary.ReadString();
          row_PR.Comment = m_TableDataBinary.ReadString();

          Table.Data_PR.Add(row_PR.Num, row_PR);

      }

      Table.Data_Tag = new Dictionary<int, Tag>(15);

      for ( int i = 0; i < 15 ; i++ )
      {
          row_Tag = new Tag();

          row_Tag.Num = m_TableDataBinary.ReadInt32();
          row_Tag.Type = m_TableDataBinary.ReadString();
          row_Tag.Item = m_TableDataBinary.ReadString();
          row_Tag.Weapon = m_TableDataBinary.ReadString();
          row_Tag.Armor = m_TableDataBinary.ReadString();

          Table.Data_Tag.Add(row_Tag.Num, row_Tag);

      }

      Table.Data_NPC = new Dictionary<int, NPC>(23);

      for ( int i = 0; i < 23 ; i++ )
      {
          row_NPC = new NPC();

          row_NPC.Num = m_TableDataBinary.ReadInt32();
          row_NPC.Property = m_TableDataBinary.ReadInt32();
          row_NPC.Race = m_TableDataBinary.ReadInt32();
          row_NPC.Grade = m_TableDataBinary.ReadByte();
          row_NPC.Level = m_TableDataBinary.ReadByte();
          row_NPC.HP = m_TableDataBinary.ReadInt32();
          row_NPC.Exp = m_TableDataBinary.ReadInt32();
          row_NPC.CLevelExp = m_TableDataBinary.ReadInt32();
          row_NPC.AddSkill01 = m_TableDataBinary.ReadInt32();
          row_NPC.RateSkill01 = m_TableDataBinary.ReadInt32();
          row_NPC.AddSkill02 = m_TableDataBinary.ReadInt32();
          row_NPC.RateSkill02 = m_TableDataBinary.ReadInt32();
          row_NPC.AddSkill03 = m_TableDataBinary.ReadInt32();
          row_NPC.RateSkill03 = m_TableDataBinary.ReadInt32();
          row_NPC.AddSkill04 = m_TableDataBinary.ReadInt32();
          row_NPC.RateSkill04 = m_TableDataBinary.ReadInt32();
          row_NPC.AddSkill05 = m_TableDataBinary.ReadInt32();
          row_NPC.RateSkill05 = m_TableDataBinary.ReadInt32();
          row_NPC.MinGold = m_TableDataBinary.ReadInt32();
          row_NPC.MaxGold = m_TableDataBinary.ReadInt32();
          row_NPC.DropPosibility = m_TableDataBinary.ReadInt32();
          row_NPC.Bag01 = m_TableDataBinary.ReadInt32();
          row_NPC.Rate01 = m_TableDataBinary.ReadInt32();
          row_NPC.Bag02 = m_TableDataBinary.ReadInt32();
          row_NPC.Rate02 = m_TableDataBinary.ReadInt32();
          row_NPC.Bag03 = m_TableDataBinary.ReadInt32();
          row_NPC.Rate03 = m_TableDataBinary.ReadInt32();
          row_NPC.Bag04 = m_TableDataBinary.ReadInt32();
          row_NPC.Rate04 = m_TableDataBinary.ReadInt32();
          row_NPC.Bag05 = m_TableDataBinary.ReadInt32();
          row_NPC.Rate05 = m_TableDataBinary.ReadInt32();
          row_NPC.NPC_FileName = m_TableDataBinary.ReadString();
          row_NPC.NPC_Prefab = m_TableDataBinary.ReadString();

          Table.Data_NPC.Add(row_NPC.Num, row_NPC);

      }

        m_TableDataBinary.Close();
    }
}
