using System;
using Server;
using Server.Items;

namespace Server.Mobiles
{
	[CorpseName( "a fetid essence corpse" )]
	public class FetidEssence : BaseCreature
	{
		[Constructable]
		public FetidEssence() : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "a fetid essence";
			Body = 0x110;
			BaseSoundID = 0x56C;

			SetStr( 100, 120 );
			SetDex( 200, 250 );
			SetInt( 450, 550 );

			SetHits( 550, 650 );

			SetDamage( 21, 25 );

			SetDamageType( ResistanceType.Physical, 30 );
			SetDamageType( ResistanceType.Poison, 70 );

			SetResistance( ResistanceType.Physical, 40, 50 );
			SetResistance( ResistanceType.Fire, 40, 50 );
			SetResistance( ResistanceType.Cold, 40, 50 );
			SetResistance( ResistanceType.Poison, 75, 90 );
			SetResistance( ResistanceType.Energy, 75, 80 );

			SetSkill( SkillName.Wrestling, 80.0, 85.0 );
			SetSkill( SkillName.Tactics, 80.0, 85.0 );
			SetSkill( SkillName.MagicResist, 100.0, 115.0 );
			SetSkill( SkillName.Poisoning, 100.0 );
			SetSkill( SkillName.Magery, 90.0, 100.0 );
			SetSkill( SkillName.EvalInt, 80.0, 100.0 );
			SetSkill( SkillName.Meditation, 80.0, 100.0 );
			
			Fame = 14000;
			Karma = -14000;
		}
		
		public override void GenerateLoot()
		{
			AddLoot( LootPack.FilthyRich );
			AddLoot( LootPack.Rich );
		}
		
		#region Area Damage
		public override void AreaDamageEffect( Mobile m )
		{
			m.FixedParticles( 0x374A, 10, 15, 5038, 1181, 2, EffectLayer.Head );
			m.PlaySound( 0x213 );
		}
		
		public override bool CanAreaDamage{ get{ return true; } }
		public override TimeSpan AreaDamageDelay{ get{ return TimeSpan.FromSeconds( 20 ); } }		
		public override double AreaDamageScalar{ get{ return 0.5; } }		
		public override int AreaFireDamage{ get{ return 0; } }
		public override int AreaColdDamage{ get{ return 100; } }
		#endregion
		
		public override Poison HitPoison{ get{ return Poison.Deadly; } }
		public override Poison PoisonImmune{ get{ return Poison.Deadly; } }

		public FetidEssence( Serial serial ) : base( serial )
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
