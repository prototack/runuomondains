using System;
using System.Collections;
using Server.Items;
using Server.ContextMenus;
using Server.Misc;
using Server.Network;

namespace Server.Mobiles
{
	[CorpseName( "the remains of Grim" )]
	public class Grim : BaseCreature
	{

		public override WeaponAbility GetWeaponAbility()
		{
			return WeaponAbility.CrushingBlow;
		}

		[Constructable]
		public Grim () : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "Grim";
			Body = Utility.RandomList( 60, 61 );
			BaseSoundID = 362;
			Hue = 1744;

			SetStr( 527, 580 );
			SetDex( 284, 322 );
			SetInt( 249, 386 );

			//SetHits( 241, 258 );
			SetHits( 1762, 2502 );

			SetDamage( 11, 17 );

			SetDamageType( ResistanceType.Physical, 80 );
			SetDamageType( ResistanceType.Fire, 20 );

			SetResistance( ResistanceType.Physical, 55, 60 );
			SetResistance( ResistanceType.Fire, 62, 68 );
			SetResistance( ResistanceType.Cold, 52, 57 );
			SetResistance( ResistanceType.Poison, 30, 40 );
			SetResistance( ResistanceType.Energy, 40, 44 );

			SetSkill( SkillName.MagicResist, 105.8, 115.6 );
			SetSkill( SkillName.Tactics, 102.8, 120.8 );
			SetSkill( SkillName.Wrestling, 111.7, 119.2 );
			SetSkill( SkillName.Anatomy, 105.0, 128.4 );

			Fame = 5500;
			Karma = -5500;

			VirtualArmor = 46;

			PackReg( 3 );
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Rich, 5 );
			AddLoot( LootPack.MedScrolls, 2 );
		}

		public override bool ReacquireOnMovement{ get{ return true; } }
		public override bool HasBreath{ get{ return true; } } // fire breath enabled
		public override int TreasureMapLevel{ get{ return 2; } }
		public override int Meat{ get{ return 10; } }
		public override int Hides{ get{ return 20; } }
		public override HideType HideType{ get{ return HideType.Horned; } }
		public override int Scales{ get{ return 2; } }
		public override ScaleType ScaleType{ get{ return ( Body == 60 ? ScaleType.Yellow : ScaleType.Red ); } }
		public override FoodType FavoriteFood{ get{ return FoodType.Meat | FoodType.Fish; } }

		public Grim( Serial serial ) : base( serial )
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