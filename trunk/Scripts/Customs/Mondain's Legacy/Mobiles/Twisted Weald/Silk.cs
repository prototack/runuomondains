using System;
using Server;
using Server.Items;

namespace Server.Mobiles
{
	[CorpseName( "a Silk corpse" )] 
	public class Silk : BaseCreature
	{
		[Constructable]
		public Silk() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "a Silk";
			Body =  0x9D;
			Hue = 0x481;
			BaseSoundID = 0x388; 

			SetStr( 80, 131 );
			SetDex( 126, 156 );
			SetInt( 63, 102 );

			SetHits( 279, 378 );
			SetStam( 126, 156 );
			SetMana( 63, 102 );

			SetDamage( 20, 25 );

			SetDamageType( ResistanceType.Physical, 100 );

            SetResistance(ResistanceType.Physical, 50, 60);
            SetResistance(ResistanceType.Fire, 34, 44);
            SetResistance(ResistanceType.Cold, 36, 46);
            SetResistance(ResistanceType.Poison, 80, 90);
            SetResistance(ResistanceType.Energy, 38, 48);

			SetSkill( SkillName.Wrestling, 114.1, 123.7 );
			SetSkill( SkillName.Tactics, 102.6, 118.3 );
			SetSkill( SkillName.MagicResist, 78.6, 94.8 );
			SetSkill( SkillName.Anatomy, 81.3, 105.7 );
			SetSkill( SkillName.Poisoning, 106.0, 119.2 );

            Fame = 7500;
            Karma = -7500;

            VirtualArmor = 54;

            PackItem(new SpidersSilk(5));
            PackItem(new GreaterPoisonPotion());
			PackItem( new LesserPoisonPotion() );
		}

		public Silk( Serial serial ) : base( serial )
		{
		}
		
		public override void GenerateLoot()
		{
			AddLoot( LootPack.AosUltraRich, 3 );
		}
		
		public override WeaponAbility GetWeaponAbility()
		{
			return WeaponAbility.ParalyzingBlow;
		}

		public override bool GivesMinorArtifact{ get{ return true; } }
		public override Poison PoisonImmune{ get{ return Poison.Lethal; } }
		public override Poison HitPoison{ get{ return Poison.Lethal; } }
            
            public override OppositionGroup OppositionGroup
		{
			get{ return OppositionGroup.FeyAndUndead; }
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			
			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			
			int version = reader.ReadInt();
		}
	}
}
