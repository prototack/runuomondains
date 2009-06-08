using System;
using System.Collections;
using Server.Items;
using Server.Targeting;

namespace Server.Mobiles
{
    [CorpseName("a Pestilent Bandage corpse")]
	public class PestilentBandage : BaseCreature
	{
		[Constructable]
		public PestilentBandage() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.4, 0.8 )
		{
            Name = "a Pestilent Bandage";
			Body = 154;
			BaseSoundID = 471;

			SetStr( 691, 740 );
			SetDex( 141, 180 );
			SetInt( 51, 80 );

            SetHits(415, 444);
            SetStam(141, 180);
            SetHits(51, 80);

			SetDamage( 13, 23 );

			SetDamageType( ResistanceType.Physical, 40 );
            SetDamageType(ResistanceType.Cold, 20);
            SetDamageType(ResistanceType.Poison, 20);

			SetResistance( ResistanceType.Physical, 45, 55 );
			SetResistance( ResistanceType.Fire, 10, 20 );
			SetResistance( ResistanceType.Cold, 50, 60 );
			SetResistance( ResistanceType.Poison, 20, 30 );
			SetResistance( ResistanceType.Energy, 20, 30 );

			SetSkill( SkillName.MagicResist, 75.3 );
			SetSkill( SkillName.Tactics, 80.1 );
            SetSkill(SkillName.Wrestling, 72.3);
            SetSkill(SkillName.Poisoning, 8.1);

			Fame = 4000;
			Karma = -4000;

			VirtualArmor = 50;

			PackItem( new Garlic( 5 ) );
			PackItem( new Bandage( 10 ) );
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Rich );
			AddLoot( LootPack.Gems );
			AddLoot( LootPack.Potions );
		}

		public override bool BleedImmune{ get{ return true; } }
		public override Poison PoisonImmune{ get{ return Poison.Lesser; } }

        public PestilentBandage(Serial serial)
            : base(serial)
		{
		}

		public override OppositionGroup OppositionGroup
		{
			get{ return OppositionGroup.FeyAndUndead; }
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
}