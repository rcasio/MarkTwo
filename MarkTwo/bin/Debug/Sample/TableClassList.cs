[System.Serializable] 
public class Multilingual
{
	public int Num;
	public string Kor;
	public string Eng;
	public string Jpn;
}

[System.Serializable] 
public class PR
{
	public int Num;
	public string Type;
	public string Key;
	public string Value;
	public string Comment;
}

[System.Serializable] 
public class Tag
{
	public int Num;
	public string Type;
	public string Item;
	public string Weapon;
	public string Armor;
}

[System.Serializable] 
public class NPC
{
	public int Num;
	public int Property;
	public TagManager.Table.Tag.Weapon Weapon;
	public int Race;
	public byte Grade;
	public byte Level;
	public int HP;
	public int Exp;
	public int CLevelExp;
	public int AddSkill01;
	public int RateSkill01;
	public int AddSkill02;
	public int RateSkill02;
	public int AddSkill03;
	public int RateSkill03;
	public int AddSkill04;
	public int RateSkill04;
	public int AddSkill05;
	public int RateSkill05;
	public int MinGold;
	public int MaxGold;
	public int DropPosibility;
	public int Bag01;
	public int Rate01;
	public int Bag02;
	public int Rate02;
	public int Bag03;
	public int Rate03;
	public int Bag04;
	public int Rate04;
	public int Bag05;
	public int Rate05;
	public string NPC_FileName;
	public string NPC_Prefab;
}

[System.Serializable] 
public class Map
{
	public int Num;
	public int ChapterNum;
	public int MapNum;
	public byte Difficulty;
	public int Attribute;
	public byte NeedAction;
	public int WeaponShopNum;
	public int ArmorShopNum;
	public int PotionShopNum;
	public int NPCBag01;
	public int NPCBag02;
	public int NPCBag03;
}

[System.Serializable] 
public class Enchant
{
	public int Num;
	public short Value;
	public int Grade;
	public int Rate;
	public int AtkPercentage;
	public int DefPercentage;
	public int AddCardOption;
	public int GamePrice;
	public int CashPrice;
}

[System.Serializable] 
public class Grade
{
	public int Num;
	public byte Color_R;
	public byte Color_G;
	public byte Color_B;
	public string Hex;
	public string Sounds;
}

[System.Serializable] 
public class ConquerorLevel
{
	public int Num;
	public float Level;
	public int NeedCLevelExp;
	public byte Strength;
	public byte Dexterity;
	public byte Constitution;
	public byte Intelligent;
	public int HPBase;
	public int MPBase;
	public int MPRegen;
	public int NoneAtk;
	public int FireAtk;
	public int WaterAtk;
	public int NatureAtk;
	public int LightAtk;
	public int DarkAtk;
	public int NoneDef;
	public int FireDef;
	public int WaterDef;
	public int NatureDef;
	public int LightDef;
	public int DarkDef;
}

[System.Serializable] 
public class Test01
{
	public int Num;
	public float Test2;
	public int Test3;
	public int Test4;
	public int Test5;
	public int Test6;
}

[System.Serializable] 
public class Test02
{
	public int Num;
	public float Test2;
	public int Test3;
}

[System.Serializable] 
public class Test03
{
	public int Num;
	public float Test2;
	public int Test3;
}

