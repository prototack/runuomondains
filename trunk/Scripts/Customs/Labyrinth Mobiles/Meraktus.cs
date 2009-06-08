using System;
using System.Collections;
using Server.Items;
using Server.Targeting;
using System.Collections.Generic;

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
		public Meraktus() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "Meraktus the Tormented";
			Body = 263;
			BaseSoundID = 680;
			Hue = 0x835;

			SetStr( 1550 );
			SetDex( 339 );
			SetInt( 127 );

			SetHits( 4122 );

			SetDamage( 15, 100 );

			SetDamageType( ResistanceType.Physical, 100 );			

			SetResistance( ResistanceType.Physical, 73 );
			SetResistance( ResistanceType.Cold, 49 );
			SetResistance( ResistanceType.Poison, 59 );
			SetResistance( ResistanceType.Energy, 57 );
			SetResistance( ResistanceType.Fire, 60, 70 );

			SetSkill( SkillName.MagicResist, 111.5 );
			SetSkill( SkillName.Tactics, 104.9 );
			SetSkill( SkillName.Wrestling, 105.0 );

			Fame = 17000;
			Karma = -17000;

			VirtualArmor = 55;
			
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
			AddLoot( LootPack.AosSuperBoss, 5 );	
		}
		
		
			public override int GetAttackSound()
		{
			return 682;
		}

		public override int GetAngerSound()
		{
			return 681;
		}

		public override int GetDeathSound()
		{
			return 684;
		}

		public override int GetHurtSound()
		{
			return 683;
		}

		public override int GetIdleSound()
		{
			return 680;
		}

		public override int Meat { get { return 2; } }
		public override int Hides { get { return 10; } }
		public override HideType HideType { get { return HideType.Regular; } }
		public override Poison PoisonImmune{ get{ return Poison.Regular; } }
		public override int TreasureMapLevel{ get{ return 3; } }
		public override bool BardImmune{ get{ return true; } }
		public override bool Unprovokable{ get{ return true; } }
		public override bool Uncalmable{get{return true;} }

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
	}
}