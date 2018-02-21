using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

  public class Table
  {
    public static Dictionary<int, Multilingual> Data_Multilingual;
    public static Dictionary<int, PR> Data_PR;
    public static Dictionary<int, Tag> Data_Tag;
    public static Dictionary<int, Quest> Data_Quest;
  }




  public class Table_Load
  {     
    private BinaryReader GetBinaryReader(string fileName)
    {
        TextAsset textasset = Resources.Load(fileName) as TextAsset;
        MemoryStream stream = new MemoryStream(textasset.bytes);
        BinaryReader binaryReader = new BinaryReader(stream);
        stream.Close();

        return binaryReader;
    }

    //BinaryReader m_TableDataBinary;
    //TextAsset m_Textasset;

    //Multilingual row_Multilingual;
    //PR row_PR;
    //Tag row_Tag;
    //Quest row_Quest;

    public Table_Load()
    { 
        Table.Data_Multilingual = new Dictionary<int, Multilingual>();
        BinaryReader multilingualBinaryReader = GetBinaryReader("multilingual_Multilingual");

        for ( int i = 0; i < 26 ; i++ )
        {
            Multilingual multilingual = new Multilingual();

            multilingual.Num = multilingualBinaryReader.ReadInt32();
            multilingual.Kor = multilingualBinaryReader.ReadString();
            multilingual.Eng = multilingualBinaryReader.ReadString();
            multilingual.Jpn = multilingualBinaryReader.ReadString();

            Table.Data_Multilingual.Add(multilingual.Num, multilingual);
        }

        multilingualBinaryReader.Close();

      Table.Data_PR = new Dictionary<int, PR>(11);

      for ( int i = 0; i < 11 ; i++ )
      {
          row_PR = new PR();

          row_PR.Num = m_TableDataBinary.ReadInt32();
          row_PR.Type = m_TableDataBinary.ReadString();
          row_PR.Key = m_TableDataBinary.ReadString();
          row_PR.Value = m_TableDataBinary.ReadString();
          row_PR.Comment = m_TableDataBinary.ReadString();

          Table.Data_PR.Add(row_PR.Num, row_PR);

      }

      Table.Data_Tag = new Dictionary<int, Tag>(13);

      for ( int i = 0; i < 13 ; i++ )
      {
          row_Tag = new Tag();

          row_Tag.Num = m_TableDataBinary.ReadInt32();
          row_Tag.Type = m_TableDataBinary.ReadString();
          row_Tag.Item = m_TableDataBinary.ReadString();
          row_Tag.Weapon = m_TableDataBinary.ReadString();
          row_Tag.Armor = m_TableDataBinary.ReadString();

          Table.Data_Tag.Add(row_Tag.Num, row_Tag);

      }

      Table.Data_Quest = new Dictionary<int, Quest>(25);

      for ( int i = 0; i < 25 ; i++ )
      {
          row_Quest = new Quest();

          row_Quest.Num = m_TableDataBinary.ReadInt32();
          row_Quest.NeedQuest = m_TableDataBinary.ReadInt32();
          row_Quest.OpenPrice = m_TableDataBinary.ReadString();
          row_Quest.UpgradePrice = m_TableDataBinary.ReadString();
          row_Quest.RewardGold = m_TableDataBinary.ReadString();
          row_Quest.RewardGoldIncrease = m_TableDataBinary.ReadString();
          row_Quest.Time = m_TableDataBinary.ReadSingle();
          row_Quest.FailRate = m_TableDataBinary.ReadSingle();
          row_Quest.RewardGoldFailRate = m_TableDataBinary.ReadSingle();
          row_Quest.SuccessRate = m_TableDataBinary.ReadSingle();
          row_Quest.RewardGoldSuccessRate = m_TableDataBinary.ReadSingle();
          row_Quest.DoubleRate = m_TableDataBinary.ReadSingle();
          row_Quest.RewardGoldDoubleRate = m_TableDataBinary.ReadSingle();
          row_Quest.GreateRate = m_TableDataBinary.ReadSingle();
          row_Quest.GreateRateDoubleRate = m_TableDataBinary.ReadSingle();
          row_Quest.IconName = m_TableDataBinary.ReadString();

          Table.Data_Quest.Add(row_Quest.Num, row_Quest);

      }

        m_TableDataBinary.Close();
    }
}
