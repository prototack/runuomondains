using System;
using System.Collections;
using System.Collections.Generic;
using Server.Items;
using Server.Targeting;
using Server.Misc;

namespace Server.Mobiles
{
	[CorpseName( "the remains of Meraktus" )]
	
	public class Meraktus : BaseCreature
    {
        public static Type[] UniqueArtifacts { get { return m_UniqueArtifacts; } }

        private static Type[] m_UniqueArtifacts = new Type[]
		{
			// Unique Artifacts
			typeof( Subdue )
		};

        public static Type[] DecorationArtifacts { get { return m_DecorationArtifacts; } }

        private static Type[] m_DecorationArtifacts = new Type[]
		{
			// Decoration Artifacts
            typeof( MinotaurStatuette ),
            typeof( DecorationLargeVase ),
            typeof( DecorationSmallVase )
		};

		public override WeaponAbility GetWeaponAbility()
		{
			return WeaponAbility.Dismount;
		}

		[Constructable]
		public Meraktus() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 ) // NEED TO CHECK
		{
			Name = "Meraktus";
			Title = "the Tormented";
			Body = 263;
			BaseSoundID = 680;
			Hue = 0x835;

			SetStr( 1419, 1438 );
			SetDex( 309, 413 );
			SetInt( 129, 131 );

			SetHits( 4100, 4200 );

			SetDamage( 3, 5 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 65, 90 );
			SetResistance( ResistanceType.Fire, 65, 70 );
			SetResistance( ResistanceType.Cold, 50, 60 );
			SetResistance( ResistanceType.Poison, 40, 60 );
			SetResistance( ResistanceType.Energy, 50, 55 );

			//SetSkill( SkillName.Meditation, Unknown );
			//SetSkill( SkillName.EvalInt, Unknown );
			//SetSkill( SkillName.Magery, Unknown );
			//SetSkill( SkillName.Poisoning, Unknown );
			SetSkill( SkillName.Anatomy, 0);
			SetSkill( SkillName.MagicResist, 107.0, 111.3 );
			SetSkill( SkillName.Tactics, 107.0, 117.0 );
			SetSkill( SkillName.Wrestling, 100.0, 105.0 );

			Fame = 70000;
			Karma = -70000;

			VirtualArmor = 28; // Don't know what it should be

            PackResources(8);
            PackTalismans(5);

            Timer.DelayCall(TimeSpan.FromSeconds(1), new TimerCallback(SpawnTormented));
        }

        public virtual void PackResources(int amount)
        {
            for (int i = 0; i < amount; i++)
                switch (Utility.Random(6))
                {
                    case 0: PackItem(new Blight()); break;
                    case 1: PackItem(new Scourge()); break;
                    case 2: PackItem(new Taint()); break;
                    case 3: PackItem(new Putrefication()); break;
                    case 4: PackItem(new Corruption()); break;
                    case 5: PackItem(new Muculent()); break;
                }
        }

        public virtual void PackTalismans(int amount)
        {
            int count = Utility.Random(amount);

            for (int i = 0; i < count; i++)
                PackItem(new RandomTalisman());
        }

		public override void OnDeath( Container c )
		{
			base.OnDeath( c );		
			
			c.DropItem( new MalletAndChisel() );
			
			switch ( Utility.Random( 3 ) )
			{
				case 0: c.DropItem( new MinotaurHedge() ); break;
				case 1: c.DropItem( new BonePile() ); break;
				case 2: c.DropItem( new LightYarn() ); break;
			}
			
			if ( Utility.RandomBool() )
				c.DropItem( new TormentedChains() );
				
			if ( Utility.RandomDouble() < 0.025 )
                c.DropItem(new CrimsonCincture());

            if (Utility.RandomDouble() < 0.30)
            {
                double random = Utility.Random(29);

                if (random <= 4)
                    GiveUniqueArtifact();
                else if (random >= 15 && random <= 29)
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

		public override void GenerateLoot()
		{
			AddLoot( LootPack.AosSuperBoss, 5 );  // Need to verify
		}
		
		public override int GetAngerSound()
		{
			return 0x597;
		}

		public override int GetIdleSound()
		{
			return 0x596;
		}

		public override int GetAttackSound()
		{
			return 0x599;
		}

		public override int GetHurtSound()
		{
			return 0x59a;
		}

		public override int GetDeathSound()
		{
			return 0x59c;
		}

		public override int Meat { get { return 2; } }
		public override int Hides { get { return 10; } }
		public override HideType HideType { get { return HideType.Regular; } }
		public override Poison PoisonImmune{ get{ return Poison.Regular; } }
		public override int TreasureMapLevel{ get{ return 3; } }
		public override bool BardImmune{ get{ return true; } }
		public override bool Unprovokable{ get{ return true; } }
        public override bool Uncalmable { get { return true; } }

        public override void OnGaveMeleeAttack(Mobile defender)
        {
            base.OnGaveMeleeAttack(defender);
            if (0.2 >= Utility.RandomDouble())
                Earthquake();
        }

        public void Earthquake()
        {
            Map map = this.Map;
            if (map == null)
                return;
            ArrayList targets = new ArrayList();
            foreach (Mobile m in this.GetMobilesInRange(8))
            {
                if (m == this || !CanBeHarmful(m))
                    continue;
                if (m is BaseCreature && (((BaseCreature)m).Controlled || ((BaseCreature)m).Summoned || ((BaseCreature)m).Team != this.Team))
                    targets.Add(m);
                else if (m.Player)
                    targets.Add(m);
            }
            PlaySound(0x2F3);
            for (int i = 0; i < targets.Count; ++i)
            {
                Mobile m = (Mobile)targets[i];
                double damage = m.Hits * 0.6;//was .6
                if (damage < 10.0)
                    damage = 10.0;
                else if (damage > 75.0)
                    damage = 75.0;
                DoHarmful(m);
                AOS.Damage(m, this, (int)damage, 100, 0, 0, 0, 0);
                if (m.Alive && m.Body.IsHuman && !m.Mounted)
                    m.Animate(20, 7, 1, true, false, 0); // take hit
            }
        }

		public Meraktus( Serial serial ) : base( serial )
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
        #region SpawnHelpers
        public void SpawnTormented()
        {
            BaseCreature spawna = new TormentedMinotaur();
            spawna.MoveToWorld(Location, Map);

            BaseCreature spawnb = new TormentedMinotaur();
            spawnb.MoveToWorld(Location, Map);

            BaseCreature spawnc = new TormentedMinotaur();
            spawnc.MoveToWorld(Location, Map);

            BaseCreature spawnd = new TormentedMinotaur();
            spawnd.MoveToWorld(Location, Map);
        }
        #endregion
    }
}

        