using System;
using System.Collections;
using Server.Items;
using Server.Targeting;

namespace Server.Mobiles
{
	[CorpseName( "a tormented minotaur corpse" )]
	
	public class TormentedMinotaur : BaseCreature
    {
        public override WeaponAbility GetWeaponAbility()
        {
            return WeaponAbility.Dismount;
        }

		[Constructable]
		public TormentedMinotaur() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "a Tormented Minotaur";
			Body = 262;
			BaseSoundID = 427;

			SetStr( 822, 930 );
			SetDex( 401, 415 );
			SetInt( 128, 138 );

			SetHits( 4000, 4200 );

			SetDamage( 16, 30 );

			SetDamageType( ResistanceType.Physical, 100 );

            SetResistance(ResistanceType.Physical, 62);
            SetResistance(ResistanceType.Fire, 74);
			SetResistance( ResistanceType.Cold, 54 );
			SetResistance( ResistanceType.Poison, 56 );
			SetResistance( ResistanceType.Energy, 54 );

			SetSkill( SkillName.MagicResist, 104.3, 116.3 );
			SetSkill( SkillName.Tactics, 100.7, 102.8 );
			SetSkill( SkillName.Wrestling, 110.1, 111.0 );

			Fame = 15000;
			Karma = -15000;

			VirtualArmor = 60;

			PackItem( new Club() );
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.FilthyRich );
		}

		public override Poison PoisonImmune{ get{ return Poison.Regular; } }
		public override int TreasureMapLevel{ get{ return 3; } }

		public TormentedMinotaur( Serial serial ) : base( serial )
		{
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