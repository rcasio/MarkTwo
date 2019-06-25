// 테이블 태그를 사용하기 위한 클래스입니다.
// 툴에의해 자동으로 생성됩니다.
// 2017년 09월 26일 고성규

namespace TagManager
{
	public class Table
	{
		// 다국어 대응 열거자 입니다. Multilingual 테이블을 기반으로 작성됩니다.
		public enum Multilingual
		{
			Kor,
			Eng,
			Jpn
		};

		// PR 테이블 대응 클래스 입니다.
		// PR 테이블은 개별 변수를 정의하기 위한 테이블입니다.
		public class PR
		{
			public const float TableVersion = 0.332f; // 테이블버전 ( Num : 1 )
			public const int TestNum = 1; // 테스트 ( Num : 2 )
			public const int InitSkillPoint = 0; // 스킬 초기화 ( Num : 100 )

			public struct LevelPenalty // 레벨 구조체
			{
				public const int Level10 = 10; // 10레벨 패널티 ( Num : 102 )
				public const int Level20 = 10; // 20레벨 패널티 ( Num : 103 )
				public const int Level30 = 20; // 30레벨 패널티 ( Num : 104 )
				public const int Level50 = 50; // 50레벨 패널티 ( Num : 105 )
				public const int Level100 = 100; // 100레벨 패널티 ( Num : 106 )
				public const int Level110 = 110; // 110레벨 패널티 ( Num : 107 )
			}

			public const string MobNormal = "notmal"; // 일반 몬스터 ( Num : 1101 )
			public const string MobNamed = "named"; // 네임드 몬스터 ( Num : 1102 )
			public const string MobBoss = "boss"; // 보스 몬스터 ( Num : 1103 )
			public const int crystalPrice = 2; // 골드대비 2배 크리스탈 비용 ( Num : 1303 )

			public struct PlayerProperty // 레벨 구조체
			{
				public const int Lift = 300; // 생명력 ( Num : 2102 )
				public const int InvenCount = 30; // 인벤토리 용량 ( Num : 2103 )
			}

        }

        public class Tag
        {
            public enum Type
            {
                Bit,
                TinyInt,
                SmallInt,
                Int,
                BigInt,
                Float,
                Double,
                Char50,
                Char100,
                Char150,
                VarChar50,
                VarChar100,
                VarChar150,
                StructStart,
                StructEnd,
            }

            public enum Item
            {
                Weapon,
                Armor,
                Etc,
            }

            public enum Weapon
            {
                OneHanded,
                TwoHanded,
                Ranged,
            }

            public enum Armor
            {
                Head,
                Shoulder,
                Torso,
                Wrists,
                Hands,
                Waist,
                Legs,
                Feet,
                OffHand,
                Jewelry,
            }

        }
    }
}
