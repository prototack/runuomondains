using System;
using Server;
using Server.Items;
using Server.Engines.CannedEvil;
using System.Collections.Generic;

namespace Server.Mobiles
{
	public class LordOaks : BaseChampion
	{
		private Mobile m_Queen;
		private bool m_SpawnedQueen;

		public override ChampionSkullType SkullType{ get{ return ChampionSkullType.Enlightenment; } }

        public static Type[] UniqueArtifacts { get { return m_UniqueArtifacts; } }

        private static Type[] m_UniqueArtifacts = new Type[]
		{
			// Unique Artifacts
			typeof( OrcChieftainHelm )
		};

        public static Type[] SharedArtifacts { get { return m_SharedArtifacts; } }

        private static Type[] m_SharedArtifacts = new Type[]
		{
			// Shared Artifacts
			typeof( RoyalGuardSurvivalKnife ),
			typeof( DjinnisRing ),
			typeof( RoyalGuardBodySash ),
			typeof( LegendaryDetectiveBoots ),
			typeof( ElderDetectiveBoots ),
			typeof( MythicalDetectiveBoots ),
			typeof( MostKnowledgePerson ),
			typeof( GoodSamaritanRobe )
		};

        public static Type[] DecorationArtifacts { get { return m_DecorationArtifacts; } }

        private static Type[] m_DecorationArtifacts = new Type[]
		{
			// Decoration Artifacts
            typeof( Pier ),
            typeof( WindSpirit ),
			typeof( WaterTile )
		};

		[Constructable]
		public LordOaks() : base( AIType.AI_Mage, FightMode.Evil )
		{
			Body = 175;
			Name = "Lord Oaks";

			SetStr( 403, 850 );
			SetDex( 101, 150 );
			SetInt( 503, 800 );

			SetHits( 3000 );
			SetStam( 202, 400 );

			SetDamage( 21, 33 );

			SetDamageType( ResistanceType.Physical, 75 );
			SetDamageType( ResistanceType.Fire, 25 );

			SetResistance( ResistanceType.Physical, 85, 90 );
			SetResistance( ResistanceType.Fire, 60, 70 );
			SetResistance( ResistanceType.Cold, 60, 70 );
			SetResistance( ResistanceType.Poison, 80, 90 );
			SetResistance( ResistanceType.Energy, 80, 90 );

			SetSkill( SkillName.Anatomy, 75.1, 100.0 );
			SetSkill( SkillName.EvalInt, 120.1, 130.0 );
			SetSkill( SkillName.Magery, 120.0 );
			SetSkill( SkillName.Meditation, 120.1, 130.0 );
			SetSkill( SkillName.MagicResist, 100.5, 150.0 );
			SetSkill( SkillName.Tactics, 100.0 );
			SetSkill( SkillName.Wrestling, 100.0 );

			Fame = 22500;
			Karma = 22500;

			VirtualArmor = 100;
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.UltraRich, 5 );
		}
		
		public override bool AutoDispel{ get{ return true; } }

		public override bool BardImmune{ get{ return !Core.SE; } }
		public override bool Unprovokable{ get{ return Core.SE; } }
		public override bool Uncalmable{ get{ return Core.SE; } }
		public override OppositionGroup OppositionGroup
		{
			get{ return OppositionGroup.FeyAndUndead; }
		}
		
		public override Poison PoisonImmune{ get{ return Poison.Deadly; } }

		public void SpawnPixies( Mobile target )
		{
			Map map = this.Map;

			if ( map == null )
				return;

			this.Say( 1042154 ); // You shall never defeat me as long as I have my queen!

			int newPixies = Utility.RandomMinMax( 3, 6 );

			for ( int i = 0; i < newPixies; ++i )
			{
				Pixie pixie = new Pixie();

				pixie.Team = this.Team;
				pixie.FightMode = FightMode.Closest;

				bool validLocation = false;
				Point3D loc = this.Location;

				for ( int j = 0; !validLocation && j < 10; ++j )
				{
					int x = X + Utility.Random( 3 ) - 1;
					int y = Y + Utility.Random( 3 ) - 1;
					int z = map.GetAverageZ( x, y );

					if ( validLocation = map.CanFit( x, y, this.Z, 16, false, false ) )
						loc = new Point3D( x, y, Z );
					else if ( validLocation = map.CanFit( x, y, z, 16, false, false ) )
						loc = new Point3D( x, y, z );
				}

				pixie.MoveToWorld( loc, map );
				pixie.Combatant = target;
			}
		}

		public override int GetAngerSound()
		{
			return 0x2F8;
		}

		public override int GetIdleSound()
		{
			return 0x2F8;
		}

		public override int GetAttackSound()
		{
			return Utility.Random( 0x2F5, 2 );
		}

		public override int GetHurtSound()
		{
			return 0x2F9;
		}

		public override int GetDeathSound()
		{
			return 0x2F7;
		}

		public void CheckQueen()
		{
			if( this.Map == null )
				return;

			if ( !m_SpawnedQueen )
			{
				this.Say( 1042153 ); // Come forth my queen!

				m_Queen = new Silvani();

				((BaseCreature)m_Queen).Team = this.Team;

				m_Queen.MoveToWorld( this.Location, this.Map );

				m_SpawnedQueen = true;
			}
			else if ( m_Queen != null && m_Queen.Deleted )
			{
				m_Queen = null;
			}
		}

		public override void AlterDamageScalarFrom( Mobile caster, ref double scalar )
		{
			CheckQueen();

			if ( m_Queen != null )
			{
				scalar *= 0.1;

				if ( 0.1 >= Utility.RandomDouble() )
					SpawnPixies( caster );
			}
		}

		public override void OnGaveMeleeAttack( Mobile defender )
		{
			base.OnGaveMeleeAttack( defender );

			defender.Damage( Utility.Random( 20, 10 ), this );
			defender.Stam -= Utility.Random( 20, 10 );
			defender.Mana -= Utility.Random( 20, 10 );
		}

		public override void OnGotMeleeAttack( Mobile attacker )
		{
			base.OnGotMeleeAttack( attacker );

			CheckQueen();

			if ( m_Queen != null && 0.1 >= Utility.RandomDouble() )
				SpawnPixies( attacker );

			attacker.Damage( Utility.Random( 20, 10 ), this );
			attacker.Stam -= Utility.Random( 20, 10 );
			attacker.Mana -= Utility.Random( 20, 10 );
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

		public LordOaks( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version

			writer.Write( m_Queen );
			writer.Write( m_SpawnedQueen );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			switch ( version )
			{
				case 0:
				{
					m_Queen = reader.ReadMobile();
					m_SpawnedQueen = reader.ReadBool();

					break;
				}
			}
		}
	}
}