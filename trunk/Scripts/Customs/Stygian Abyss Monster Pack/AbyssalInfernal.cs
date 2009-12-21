using System;
using Server.Items;
using Server.Targeting;
using System.Collections;

namespace Server.Mobiles
{
	[CorpseName( "an abyssal infernal corpse" )]
	public class AbyssalInfernal : BaseCreature
	{
		[Constructable]
		public AbyssalInfernal() : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "The Abyssal Infernal";
			Body = 713; 

			SetStr( 1234 );
			SetDex( 110 );
			SetInt( 623 );

			SetDamage( 11, 18 );

			SetDamageType( ResistanceType.Physical, 60 );
			SetDamageType( ResistanceType.Fire, 20 );
			SetDamageType( ResistanceType.Energy, 20 );

			SetResistance( ResistanceType.Physical, 54 );
			SetResistance( ResistanceType.Fire, 72 );
			SetResistance( ResistanceType.Cold, 58 );
			SetResistance( ResistanceType.Poison, 39 );
			SetResistance( ResistanceType.Energy, 72 );

			SetSkill( SkillName.Anatomy, 113.3 );
			SetSkill( SkillName.EvalInt, 123.9 );
			SetSkill( SkillName.Magery, 132.2 );
			SetSkill( SkillName.Meditation, 100.0 );
			SetSkill( SkillName.MagicResist, 120.0 );
			SetSkill( SkillName.Tactics, 119.8 );
			SetSkill( SkillName.Wrestling, 113.8 );

		}

		public override int GetIdleSound() { return 1495; } 
		public override int GetAngerSound() { return 1492; } 
		public override int GetHurtSound() { return 1494; } 
		public override int GetDeathSound()	{ return 1493; }

		public AbyssalInfernal( Serial serial ) : base( serial )
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