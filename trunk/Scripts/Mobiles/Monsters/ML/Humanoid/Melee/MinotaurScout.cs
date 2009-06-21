using System;
using Server;
using System.Collections;
using Server.Items;
using Server.Targeting;
using Server.Misc;

namespace Server.Mobiles
{
	[CorpseName( "a minotaur scout corpse" )]	
	public class MinotaurScout : Minotaur
	{

		[Constructable]
		public MinotaurScout() : base()
		{
			Name = "a minotaur scout";
			Body = 0x119;

			SetStr( 352, 375 );
			SetDex( 111, 130 );
			SetInt( 32, 50 );

			SetHits( 354, 383 );

			SetDamage( 4, 7 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 55, 65 );
			SetResistance( ResistanceType.Fire, 25, 35 );
			SetResistance( ResistanceType.Cold, 30, 40 );
			SetResistance( ResistanceType.Poison, 30, 40 );
			SetResistance( ResistanceType.Energy, 30, 40 );

			//SetSkill( SkillName.Meditation, Unknown );
			//SetSkill( SkillName.EvalInt, Unknown );
			//SetSkill( SkillName.Magery, Unknown );
			//SetSkill( SkillName.Poisoning, Unknown );
			SetSkill( SkillName.Anatomy, 0 );
			SetSkill( SkillName.MagicResist, 60.6, 69.5 );
			SetSkill( SkillName.Tactics, 86.9, 103.9 );
			SetSkill( SkillName.Wrestling, 85.6, 104.5 );

			Fame = 5000;
			Karma = -5000;

			VirtualArmor = 28; // Don't know what it should be
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Rich );  // Need to verify
		}

		public MinotaurScout( Serial serial ) : base( serial )
		{
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