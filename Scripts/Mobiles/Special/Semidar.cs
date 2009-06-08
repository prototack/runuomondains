using System;
using System.Collections;
using Server.Items;
using Server.Engines.CannedEvil;
using System.Collections.Generic;

namespace Server.Mobiles
{
	public class Semidar : BaseChampion
	{
		public override ChampionSkullType SkullType{ get{ return ChampionSkullType.Pain; } }

        public static Type[] UniqueArtifacts { get { return m_UniqueArtifacts; } }

        private static Type[] m_UniqueArtifacts = new Type[]
		{
			// Unique Artifacts
			typeof( GladiatorsCollar )
		};

        public static Type[] SharedArtifacts { get { return m_SharedArtifacts; } }

        private static Type[] m_SharedArtifacts = new Type[]
		{
			// Shared Artifacts
			typeof( RoyalGuardBodySash )
		};

        public static Type[] DecorationArtifacts { get { return m_DecorationArtifacts; } }

        private static Type[] m_DecorationArtifacts = new Type[]
		{
			// Decoration Artifacts
            typeof( DemonSkull ),
			typeof( LavaTile )
		};

		[Constructable]
		public Semidar() : base( AIType.AI_Mage )
		{
			Name = "Semidar";
			Body = 174;
			BaseSoundID = 0x4B0;

			SetStr( 502, 600 );
			SetDex( 102, 200 );
			SetInt( 601, 750 );

			SetHits( 1500 );
			SetStam( 103, 250 );

			SetDamage( 29, 35 );

			SetDamageType( ResistanceType.Physical, 75 );
			SetDamageType( ResistanceType.Fire, 25 );

			SetResistance( ResistanceType.Physical, 20, 30 );
			SetResistance( ResistanceType.Fire, 50, 60 );
			SetResistance( ResistanceType.Cold, 20, 30 );
			SetResistance( ResistanceType.Poison, 20, 30 );
			SetResistance( ResistanceType.Energy, 10, 20 );

			SetSkill( SkillName.EvalInt, 95.1, 100.0 );
			SetSkill( SkillName.Magery, 90.1, 105.0 );
			SetSkill( SkillName.Meditation, 95.1, 100.0 );
			SetSkill( SkillName.MagicResist, 120.2, 140.0 );
			SetSkill( SkillName.Tactics, 90.1, 105.0 );
			SetSkill( SkillName.Wrestling, 90.1, 105.0 );

			Fame = 24000;
			Karma = -24000;

			VirtualArmor = 20;
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.UltraRich, 4 );
			AddLoot( LootPack.FilthyRich );
		}

		public override bool Unprovokable{ get{ return true; } }
		public override Poison PoisonImmune{ get{ return Poison.Lethal; } }

		public override void CheckReflect( Mobile caster, ref bool reflect )
		{
			if ( caster.Body.IsMale )
				reflect = true; // Always reflect if caster isn't female
		}

		public override void AlterDamageScalarFrom( Mobile caster, ref double scalar )
		{
			if ( caster.Body.IsMale )
				scalar = 20; // Male bodies always reflect.. damage scaled 20x
		}

		public void DrainLife()
		{
			if( this.Map == null )
				return;

			ArrayList list = new ArrayList();

			foreach ( Mobile m in this.GetMobilesInRange( 2 ) )
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

				m.SendMessage( "You feel the life drain out of you!" );

				int toDrain = Utility.RandomMinMax( 10, 40 );

				Hits += toDrain;
				m.Damage( toDrain, this );
			}
		}

		public override void OnGaveMeleeAttack( Mobile defender )
		{
			base.OnGaveMeleeAttack( defender );

			if ( 0.25 >= Utility.RandomDouble() )
				DrainLife();
		}

		public override void OnGotMeleeAttack( Mobile attacker )
		{
			base.OnGotMeleeAttack( attacker );

			if ( 0.25 >= Utility.RandomDouble() )
				DrainLife();
		}

        public override void OnDeath(Container c)
        {
            base.OnDeath(c);

            if (Utility.RandomDouble() < 0.30)
            {
                double random = Utility.Random(29);

                if (random <= 4)
                    GiveUniqueArtifact();
                else if (random >= 5 && random <= 14)
                    GiveSharedArtifact();
                else
                    GiveDecorationArtifact();
            }
        }

        #region Unique Artifact
        public void GiveUniqueArtifact()
        {
            List<Mobile> toGive = new List<Mobile>();
            List<DamageStore> rights = BaseCreature.GetLootingRights(this.DamageEntries, this.HitsMax);

            for (int i = rights.Count - 1; i >= 0; --i)
            {
                DamageStore ds = rights[i];

                if (ds.m_HasRight)
                    toGive.Add(ds.m_Mobile);
            }

            if (toGive.Count == 0)
                return;

            // Randomize
            for (int i = 0; i < toGive.Count; ++i)
            {
                int rand = Utility.Random(toGive.Count);
                Mobile hold = toGive[i];
                toGive[i] = toGive[rand];
                toGive[rand] = hold;
            }

            for (int i = 0; i < 1; ++i)
            {
                Mobile m = toGive[i % toGive.Count];
                GiveUniqueArtifactTo(m);
            }
        }

        public static void GiveUniqueArtifactTo(Mobile m)
        {
            Item item = Loot.Construct(m_UniqueArtifacts);

            if (item == null || m == null)	//sanity
                return;

            // TODO: Confirm messages
            if (m.AddToBackpack(item))
                m.SendLocalizedMessage(1062317); // For your valor in combating the fallen beast, a special artifact has been bestowed on you.
            else
                m.SendMessage("As your backpack is full, your reward for valor in combating the fallen beast, has been placed at your feet.");
        }
        #endregion

        #region Shared Artifact
        public void GiveSharedArtifact()
        {
            List<Mobile> toGive = new List<Mobile>();
            List<DamageStore> rights = BaseCreature.GetLootingRights(this.DamageEntries, this.HitsMax);

            for (int i = rights.Count - 1; i >= 0; --i)
            {
                DamageStore ds = rights[i];

                if (ds.m_HasRight)
                    toGive.Add(ds.m_Mobile);
            }

            if (toGive.Count == 0)
                return;

            // Randomize
            for (int i = 0; i < toGive.Count; ++i)
            {
                int rand = Utility.Random(toGive.Count);
                Mobile hold = toGive[i];
                toGive[i] = toGive[rand];
                toGive[rand] = hold;
            }

            for (int i = 0; i < 1; ++i)
            {
                Mobile m = toGive[i % toGive.Count];
                GiveSharedArtifactTo(m);
            }
        }

        public static void GiveSharedArtifactTo(Mobile m)
        {
            Item item = Loot.Construct(m_SharedArtifacts);

            if (item == null || m == null)	//sanity
                return;

            // TODO: Confirm messages
            if (m.AddToBackpack(item))
                m.SendLocalizedMessage(1062317); // For your valor in combating the fallen beast, a special artifact has been bestowed on you.
            else
                m.SendMessage("As your backpack is full, your reward for valor in combating the fallen beast, has been placed at your feet.");
        }
        #endregion

        #region Decoration Artifact
        public void GiveDecorationArtifact()
        {
            List<Mobile> toGive = new List<Mobile>();
            List<DamageStore> rights = BaseCreature.GetLootingRights(this.DamageEntries, this.HitsMax);

            for (int i = rights.Count - 1; i >= 0; --i)
            {
                DamageStore ds = rights[i];

                if (ds.m_HasRight)
                    toGive.Add(ds.m_Mobile);
            }

            if (toGive.Count == 0)
                return;

            // Randomize
            for (int i = 0; i < toGive.Count; ++i)
            {
                int rand = Utility.Random(toGive.Count);
                Mobile hold = toGive[i];
                toGive[i] = toGive[rand];
                toGive[rand] = hold;
            }

            for (int i = 0; i < 1; ++i)
            {
                Mobile m = toGive[i % toGive.Count];
                GiveDecorationArtifactTo(m);
            }
        }

        public static void GiveDecorationArtifactTo(Mobile m)
        {
            Item item = Loot.Construct(m_DecorationArtifacts);

            if (item == null || m == null)	//sanity
                return;

            // TODO: Confirm messages
            if (m.AddToBackpack(item))
                m.SendLocalizedMessage(1062317); // For your valor in combating the fallen beast, a special artifact has been bestowed on you.
            else
                m.SendMessage("As your backpack is full, your reward for valor in combating the fallen beast, has been placed at your feet.");
        }
        #endregion

		public Semidar( Serial serial ) : base( serial )
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
