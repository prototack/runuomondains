using System;
using Server;
using Server.Items;
using Server.Engines.CannedEvil;
using System.Collections.Generic;

namespace Server.Mobiles
{
	public class Mephitis : BaseChampion
	{
		public override ChampionSkullType SkullType{ get{ return ChampionSkullType.Venom; } }

        public static Type[] UniqueArtifacts { get { return m_UniqueArtifacts; } }

        private static Type[] m_UniqueArtifacts = new Type[]
		{
			// Unique Artifacts
			typeof( Calm )
		};

        public static Type[] SharedArtifacts { get { return m_SharedArtifacts; } }

        private static Type[] m_SharedArtifacts = new Type[]
		{
			// Shared Artifacts
			typeof( NecromancerShroud ),
			typeof( OblivionsNeedle )
		};

        public static Type[] DecorationArtifacts { get { return m_DecorationArtifacts; } }

        private static Type[] m_DecorationArtifacts = new Type[]
		{
			// Decoration Artifacts
            typeof( Spiderweb ),
            typeof( CocoonWebDeed ),
			typeof( DreadSpiderStatuette )
		};

		[Constructable]
		public Mephitis() : base( AIType.AI_Melee )
		{
			Body = 173;
			Name = "Mephitis";

			BaseSoundID = 0x183;

			SetStr( 505, 1000 );
			SetDex( 102, 300 );
			SetInt( 402, 600 );

			SetHits( 3000 );
			SetStam( 105, 600 );

			SetDamage( 21, 33 );

			SetDamageType( ResistanceType.Physical, 50 );
			SetDamageType( ResistanceType.Poison, 50 );

			SetResistance( ResistanceType.Physical, 75, 80 );
			SetResistance( ResistanceType.Fire, 60, 70 );
			SetResistance( ResistanceType.Cold, 60, 70 );
			SetResistance( ResistanceType.Poison, 100 );
			SetResistance( ResistanceType.Energy, 60, 70 );

			SetSkill( SkillName.MagicResist, 70.7, 140.0 );
			SetSkill( SkillName.Tactics, 97.6, 100.0 );
			SetSkill( SkillName.Wrestling, 97.6, 100.0 );

			Fame = 22500;
			Karma = -22500;

			VirtualArmor = 80;
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.UltraRich, 4 );
		}

		public override Poison PoisonImmune{ get{ return Poison.Lethal; } }
		public override Poison HitPoison{ get{ return Poison.Lethal; } }

		public override void OnGotMeleeAttack( Mobile attacker )
		{
			base.OnGotMeleeAttack( attacker );

			// TODO: Web ability
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

		public Mephitis( Serial serial ) : base( serial )
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