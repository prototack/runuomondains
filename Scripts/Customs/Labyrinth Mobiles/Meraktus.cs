using System;
using System.Collections;
using Server.Items;
using Server.Targeting;

namespace Server.Mobiles
{
	[CorpseName( "the remains of Meraktus" )]
	
	public class Meraktus : BaseCreature
	{
		public override WeaponAbility GetWeaponAbility()
		{
			return WeaponAbility.Dismount;
		}
		
		[Constructable]
		public Meraktus() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "Meraktus the Tormented";
			Body = 263;
			BaseSoundID = 680;
			Hue = 0x835;

			SetStr( 1550 );
			SetDex( 339 );
			SetInt( 127 );

			SetHits( 4122 );

			SetDamage( 15, 100 );

			SetDamageType( ResistanceType.Physical, 100 );			

			SetResistance( ResistanceType.Physical, 73 );
			SetResistance( ResistanceType.Cold, 49 );
			SetResistance( ResistanceType.Poison, 59 );
			SetResistance( ResistanceType.Energy, 57 );
			SetResistance( ResistanceType.Fire, 60, 70 );

			SetSkill( SkillName.MagicResist, 111.5 );
			SetSkill( SkillName.Tactics, 104.9 );
			SetSkill( SkillName.Wrestling, 105.0 );

			Fame = 17000;
			Karma = -17000;

			VirtualArmor = 55;
			
		}

		public override void OnDeath( Container c )
		{
			base.OnDeath( c );		
			
			c.DropItem( new MalletAndChisel() );
			
			switch ( Utility.Random( 3 ) )
			{
				case 0: c.DropItem( new MinotaurHedge() ); break;
				case 1: c.DropItem( new BonePile() ); break;
				case 2: c.DropItem( new LightYarn() ); break;
			}

			//c.DropItem( new InsertYourItemHere() );
			
			if ( Utility.RandomBool() )
				c.DropItem( new TormentedChains() );
				
			//if ( Utility.RandomDouble() < 0.05 )
				//c.DropItem( new TormentedMinotaurStatuette() );
				
			if ( Utility.RandomDouble() < 0.025 )
                c.DropItem(new CrimsonCincture());
				
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.AosSuperBoss, 5 );	
		}
		
		
			public override int GetAttackSound()
		{
			return 682;
		}

		public override int GetAngerSound()
		{
			return 681;
		}

		public override int GetDeathSound()
		{
			return 684;
		}

		public override int GetHurtSound()
		{
			return 683;
		}

		public override int GetIdleSound()
		{
			return 680;
		}

		public override int Meat { get { return 2; } }
		public override int Hides { get { return 10; } }
		public override HideType HideType { get { return HideType.Regular; } }
		public override Poison PoisonImmune{ get{ return Poison.Regular; } }
		public override int TreasureMapLevel{ get{ return 3; } }
		public override bool BardImmune{ get{ return true; } }
		public override bool Unprovokable{ get{ return true; } }
		public override bool Uncalmable{get{return true;} }

		public Meraktus( Serial serial ) : base( serial )
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