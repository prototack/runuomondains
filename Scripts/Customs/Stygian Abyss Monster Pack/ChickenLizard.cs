using System;
using Server.Items;
using Server.Targeting;
using System.Collections;

namespace Server.Mobiles
{
	[CorpseName( "a chicken lizard corpse" )]
	public class ChickenLizard : BaseCreature
	{
		[Constructable]
		public ChickenLizard() : base( AIType.AI_Animal, FightMode.Aggressor, 10, 1, 0.2, 0.4 )
		{
			Name = "a chicken lizard";
			Body = 716;

			SetStr( 77, 95 );
			SetDex( 78, 95 );
			SetInt( 6, 10 );

			SetHits( 75, 85 );

			SetDamage( 2, 5 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 15, 20 );
			SetResistance( ResistanceType.Fire, 5, 14 );

			SetSkill( SkillName.MagicResist, 25.1, 29.6 );
			SetSkill( SkillName.Tactics, 30.1, 44.9 );
			SetSkill( SkillName.Wrestling, 26.2, 38.2 );

            Fame = 300;
            Karma = 300;
		}

		public override int Meat{ get{ return 3; } }
		public override MeatType MeatType{ get{ return MeatType.Bird; } }
        public override FoodType FavoriteFood { get { return FoodType.GrainsAndHay; } }

		public override int GetIdleSound() { return 1511; } 
		public override int GetAngerSound() { return 1508; } 
		public override int GetHurtSound() { return 1510; } 
		public override int GetDeathSound()	{ return 1509; }

		public ChickenLizard( Serial serial ) : base( serial )
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