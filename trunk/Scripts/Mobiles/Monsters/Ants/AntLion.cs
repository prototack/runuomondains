using System;
using Server;
using Server.Items;

namespace Server.Mobiles
{
	[CorpseName( "an ant lion corpse" )]
	public class AntLion : BaseCreature
	{
		[Constructable]
		public AntLion() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "an ant lion";
			Body = 787;

			SetStr( 296, 320 );
			SetDex( 81, 105 );
			SetInt( 36, 60 );

			SetHits( 151, 162 );

			SetDamage( 7, 21 );

			SetDamageType( ResistanceType.Physical, 70 );
			SetDamageType( ResistanceType.Poison, 30 );

			SetResistance( ResistanceType.Physical, 45, 60 );
			SetResistance( ResistanceType.Fire, 25, 35 );
			SetResistance( ResistanceType.Cold, 30, 40 );
			SetResistance( ResistanceType.Poison, 40, 50 );
			SetResistance( ResistanceType.Energy, 30, 35 );

			SetSkill( SkillName.MagicResist, 70.0 );
			SetSkill( SkillName.Tactics, 90.0 );
			SetSkill( SkillName.Wrestling, 90.0 );

			Fame = 4500;
			Karma = -4500;

			VirtualArmor = 45;

			PackItem( new Bone( 3 ) );
			PackItem( new FertileDirt( Utility.RandomMinMax( 1, 5 ) ) );

			switch ( Utility.Random( 4 ) )
			{
				case 0: PackItem( new DullCopperOre( Utility.RandomMinMax( 1, 10 ) ) ); break;
				case 1: PackItem( new ShadowIronOre( Utility.RandomMinMax( 1, 10 ) ) ); break;
				case 2: PackItem( new CopperOre( Utility.RandomMinMax( 1, 10 ) ) ); break;
				case 3: PackItem( new BronzeOre( Utility.RandomMinMax( 1, 10 ) ) ); break;
			}

            // Todo.  Peculiar and Fragrant seeds.
		}

        public override int GetAngerSound()
        {
            return 0x5A;
        }

        public override int GetIdleSound()
        {
            return 0x5E;
        }

        public override int GetAttackSound()
        {
            return 0x164;
        }

        public override int GetHurtSound()
        {
            return 0x187;
        }

        public override int GetDeathSound()
        {
            return 0x1BA;
        }

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Average, 2 );
		}

		public override bool OnBeforeDeath()
		{
			if ( 0.02 > Utility.RandomDouble() )
				switch ( Utility.Random( 4 ) )
				{
					default:
					case 0: PackItem( new BardBones() ) ; break;
					case 1:	PackItem( new RogueBones() ) ; break;
					case 2: PackItem( new MageBones() ) ; break;
					case 3: PackItem( new WarriorBones() ) ; break;
				};

			return base.OnBeforeDeath();
		}

        public override void OnDamage(int amount, Mobile from, bool willKill)
        {
            Mobile combatant = Combatant;

            if (combatant == null || combatant.Deleted || combatant.Map != Map || !InRange(combatant, 12) || !CanBeHarmful(combatant) || !InLOS(combatant))
                return;
            if (Utility.Random(1, 100) < 11)
                PoisonAttack(combatant);
            base.OnDamage(amount, from, willKill);
        }

        public void PoisonAttack(Mobile m)
        {
            DoHarmful(m);
            this.MovingParticles(m, 0x36D4, 1, 0, false, false, 0x3F, 0, 0x1F73, 1, 0, (EffectLayer)255, 0x100);
            m.ApplyPoison(this, Poison.Regular);
            m.SendLocalizedMessage(1070821, this.Name); // %s spits a poisonous substance at you!
        }

		public AntLion( Serial serial ) : base( serial )
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