using System;
using Server;
using System.Collections;
using Server.Items;
using Server.Targeting;
using Server.Misc;

namespace Server.Mobiles
{
	[CorpseName( "a troglodyte corpse" )]
	public class Troglodyte : BaseCreature
	{
		[Constructable]
		public Troglodyte() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 ) // NEED TO CHECK
		{
			Name = "a troglodyte";
			Body = 0x10B;

			BaseSoundID = 0x59F; 

			SetStr( 148, 217 );
			SetDex( 91, 120 );
			SetInt( 51, 70 );

			SetHits( 302, 340 );

			SetDamage( 5, 6 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 30, 35 );
			SetResistance( ResistanceType.Fire, 20, 30 );
			SetResistance( ResistanceType.Cold, 35, 40 );
			SetResistance( ResistanceType.Poison, 30, 40 );
			SetResistance( ResistanceType.Energy, 30, 40 );

			SetSkill( SkillName.Anatomy, 70.5, 94.8 );
			SetSkill( SkillName.MagicResist, 51.8, 65.0 );
			SetSkill( SkillName.Tactics, 80.4, 94.7 );
			SetSkill( SkillName.Wrestling, 70.2, 93.5 );

			Fame = 5000;
			Karma = -5000;

			VirtualArmor = 28; // Don't know what it should be

			PackItem( new Bandage( Utility.RandomMinMax( 6, 17 ) ) );  // How many?
			PackItem( new Ribs() );

			PackItem( new TreasureMap( 2, Map.Trammel ) );
		}

		public override bool CanHeal{ get{ return true; } }		
		public override double MinHealDelay{ get{ return 4.0; } }
		public override int Meat{ get{ return 1; } }

		public override void GenerateLoot()
		{
			AddLoot( LootPack.AosRich, 3 );  // Need to verify
		}

		public Troglodyte( Serial serial ) : base( serial )
		{
		}

		public override void OnDeath( Container c )
		{
			base.OnDeath( c );	
			
			if ( Utility.RandomDouble() < 0.25 )
				c.DropItem( new PrimitiveFetish() );
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