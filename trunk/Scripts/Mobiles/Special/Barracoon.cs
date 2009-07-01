using System;
using Server;
using Server.Items;
using Server.Spells;
using Server.Spells.Seventh;
using Server.Spells.Fifth;
using Server.Engines.CannedEvil;
using System.Collections.Generic;

namespace Server.Mobiles
{
	public class Barracoon : BaseChampion
	{
		public override ChampionSkullType SkullType{ get{ return ChampionSkullType.Greed; } }

        public static Type[] UniqueArtifacts { get { return m_UniqueArtifacts; } }

        private static Type[] m_UniqueArtifacts = new Type[]
		{
			// Unique Artifacts
			typeof( FangOfRactus )
		};

        public static Type[] SharedArtifacts { get { return m_SharedArtifacts; } }

        private static Type[] m_SharedArtifacts = new Type[]
		{
			// Shared Artifacts
			typeof( NewEmbroideredOakLeafCloak ),
			typeof( NewDjinnisRing ),
			typeof( LegendaryDetectiveBoots ),
			typeof( ElderDetectiveBoots ),
			typeof( MythicalDetectiveBoots ),
			typeof( GauntletsOfAnger )
		};

        public static Type[] DecorationArtifacts { get { return m_DecorationArtifacts; } }

        private static Type[] m_DecorationArtifacts = new Type[]
		{
			// Decoration Artifacts
            typeof( SlimeStatuette ),
			typeof( SwampTile )
		};

		[Constructable]
		public Barracoon() : base( AIType.AI_Melee )
		{
			Name = "Barracoon";
			Title = "the piper";
			Body = 0x190;
			Hue = 0x83EC;

			SetStr( 305, 425 );
			SetDex( 72, 150 );
			SetInt( 505, 750 );

			SetHits( 4200 );
			SetStam( 102, 300 );

			SetDamage( 25, 35 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 60, 70 );
			SetResistance( ResistanceType.Fire, 50, 60 );
			SetResistance( ResistanceType.Cold, 50, 60 );
			SetResistance( ResistanceType.Poison, 40, 50 );
			SetResistance( ResistanceType.Energy, 40, 50 );

			SetSkill( SkillName.MagicResist, 100.0 );
			SetSkill( SkillName.Tactics, 97.6, 100.0 );
			SetSkill( SkillName.Wrestling, 97.6, 100.0 );

			Fame = 22500;
			Karma = -22500;

			VirtualArmor = 70;

			AddItem( new FancyShirt( Utility.RandomGreenHue() ) );
			AddItem( new LongPants( Utility.RandomYellowHue() ) );
			AddItem( new JesterHat( Utility.RandomPinkHue() ) );
			AddItem( new Cloak( Utility.RandomPinkHue() ) );
			AddItem( new Sandals() );

			HairItemID = 0x203B; // Short Hair
			HairHue = 0x94;
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.UltraRich, 3 );
		}

		public override bool AlwaysMurderer{ get{ return true; } }
		public override bool AutoDispel{ get{ return true; } }
		public override double AutoDispelChance{ get{ return 1.0; } }
		public override bool BardImmune{ get{ return !Core.SE; } }
		public override bool Unprovokable{ get{ return Core.SE; } }
		public override bool Uncalmable{ get{ return Core.SE; } }
		public override Poison PoisonImmune{ get{ return Poison.Deadly; } }

		public override bool ShowFameTitle{ get{ return false; } }
		public override bool ClickTitle{ get{ return false; } }

		public void Polymorph( Mobile m )
		{
			if ( !m.CanBeginAction( typeof( PolymorphSpell ) ) || !m.CanBeginAction( typeof( IncognitoSpell ) ) || m.IsBodyMod )
				return;

			IMount mount = m.Mount;

			if ( mount != null )
				mount.Rider = null;

			if ( m.Mounted )
				return;

			if ( m.BeginAction( typeof( PolymorphSpell ) ) )
			{
				Item disarm = m.FindItemOnLayer( Layer.OneHanded );

				if ( disarm != null && disarm.Movable )
					m.AddToBackpack( disarm );

				disarm = m.FindItemOnLayer( Layer.TwoHanded );

				if ( disarm != null && disarm.Movable )
					m.AddToBackpack( disarm );

				m.BodyMod = 42;
				m.HueMod = 0;

				new ExpirePolymorphTimer( m ).Start();
			}
		}

		private class ExpirePolymorphTimer : Timer
		{
			private Mobile m_Owner;

			public ExpirePolymorphTimer( Mobile owner ) : base( TimeSpan.FromMinutes( 3.0 ) )
			{
				m_Owner = owner;

				Priority = TimerPriority.OneSecond;
			}

			protected override void OnTick()
			{
				if ( !m_Owner.CanBeginAction( typeof( PolymorphSpell ) ) )
				{
					m_Owner.BodyMod = 0;
					m_Owner.HueMod = -1;
					m_Owner.EndAction( typeof( PolymorphSpell ) );
				}
			}
		}

		public void SpawnRatmen( Mobile target )
		{
			Map map = this.Map;

			if ( map == null )
				return;

			int rats = 0;

			foreach ( Mobile m in this.GetMobilesInRange( 10 ) )
			{
				if ( m is Ratman || m is RatmanArcher || m is RatmanMage )
					++rats;
			}

			if ( rats < 16 )
			{
				PlaySound( 0x3D );

				int newRats = Utility.RandomMinMax( 3, 6 );

				for ( int i = 0; i < newRats; ++i )
				{
					BaseCreature rat;

					switch ( Utility.Random( 5 ) )
					{
						default:
						case 0: case 1:	rat = new Ratman(); break;
						case 2: case 3:	rat = new RatmanArcher(); break;
						case 4:			rat = new RatmanMage(); break;
					}

					rat.Team = this.Team;

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

					rat.MoveToWorld( loc, map );
					rat.Combatant = target;
				}
			}
		}

		public void DoSpecialAbility( Mobile target )
		{
			if ( target == null || target.Deleted ) //sanity
				return;
			if ( 0.6 >= Utility.RandomDouble() ) // 60% chance to polymorph attacker into a ratman
				Polymorph( target );

			if ( 0.2 >= Utility.RandomDouble() ) // 20% chance to more ratmen
				SpawnRatmen( target );

			if ( Hits < 500 && !IsBodyMod ) // Baracoon is low on life, polymorph into a ratman
				Polymorph( this );
		}

		public override void OnGotMeleeAttack( Mobile attacker )
		{
			base.OnGotMeleeAttack( attacker );

			DoSpecialAbility( attacker );
		}

		public override void OnGaveMeleeAttack( Mobile defender )
		{
			base.OnGaveMeleeAttack( defender );

			DoSpecialAbility( defender );
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

        public Barracoon( Serial serial ) : base( serial )
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