using System;
using System.Collections;
using Server;
using Server.Items;

namespace Server.Mobiles
{
	[CorpseName( "a Interred Grizzle corpse" )]
	public class InterredGrizzle : BaseCreature
	{
		[Constructable]
		public InterredGrizzle () : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "a Interred Grizzle";
            Body = 0x103;			
			BaseSoundID = 589;

            SetStr(459, 475);
			SetDex( 90, 100 );
			SetInt( 50, 60 );

			SetHits( 450, 480 );

			SetDamage( 16, 19 );

			SetDamageType( ResistanceType.Physical, 25 );
			SetDamageType( ResistanceType.Energy, 75 );

			SetResistance( ResistanceType.Physical, 45, 55 );
			SetResistance( ResistanceType.Fire, 55, 65 );
			SetResistance( ResistanceType.Cold, 55, 65 );
			SetResistance( ResistanceType.Poison, 30, 35 );
			SetResistance( ResistanceType.Energy, 65, 70 );

            SetSkill(SkillName.EvalInt, 72.2, 79.6);
            SetSkill(SkillName.Magery, 83.7, 89.6);
			SetSkill( SkillName.Meditation, 80.4, 84.0 );
			SetSkill( SkillName.MagicResist, 80.2, 87.3 );
			SetSkill( SkillName.Tactics, 403.0, 105.0 );
			SetSkill( SkillName.Wrestling, 105.1, 109.4 );

			Fame = 18000;
			Karma = -18000;

			VirtualArmor = 60;
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.FilthyRich, 2 );
			AddLoot( LootPack.MedScrolls, 2 );
		}

		public override int Meat{ get{ return 1; } }
		public override int TreasureMapLevel{ get{ return 5; } }

		public void DrainLife()
		{
			ArrayList list = new ArrayList();

			foreach ( Mobile m in this.GetMobilesInRange( 10 ) )
			{
				if ( m == this || !CanBeHarmful( m ) )
					continue;

				if ( m is BaseCreature && (((BaseCreature)m).Controlled || ((BaseCreature)m).Summoned || ((BaseCreature)m).Team != this.Team) )
					list.Add( m );
				else if ( m.Player )
					list.Add( m );
			}

			foreach ( Mobile m in list )
			{
				DoHarmful( m );

				m.FixedParticles( 0x374A, 10, 15, 5013, 0x496, 0, EffectLayer.Waist );
				m.PlaySound( 0x231 );

				m.SendMessage( "Your Life is Mine to feed on!" );

				int toDrain = Utility.RandomMinMax( 10, 40 );

				Hits += toDrain;
				m.Damage( toDrain, this );
			}
		}

		public override void OnGaveMeleeAttack( Mobile defender )
		{
			base.OnGaveMeleeAttack( defender );

			if ( 0.1 >= Utility.RandomDouble() )
				DrainLife();
		}

		public override void OnGotMeleeAttack( Mobile attacker )
		{
			base.OnGotMeleeAttack( attacker );

			if ( 0.1 >= Utility.RandomDouble() )
				DrainLife();
		}

		public InterredGrizzle( Serial serial ) : base( serial )
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