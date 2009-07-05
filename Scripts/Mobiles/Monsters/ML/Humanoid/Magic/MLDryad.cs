using System;
using Server;
using System.Collections;
using Server.Items;
using Server.Targeting;
using Server.Misc;

namespace Server.Mobiles
{
	[CorpseName("a dryad corpse")]
	public class MLDryad : BaseCreature
	{
        public override bool InitialInnocent { get { return true; } }
        public virtual int PeaceMinDelay { get { return 50; } }
        public virtual int PeaceMaxDelay { get { return 75; } }

		public override WeaponAbility GetWeaponAbility()
		{
			return WeaponAbility.Disrobe;
		}

		[Constructable]
		public MLDryad()
			: base(AIType.AI_Mage, FightMode.Aggressor, 10, 1, 0.2, 0.4) // NEED TO CHECK
		{
			Name = "a dryad";
			Body = 266;
			BaseSoundID = 0x57B;

			SetStr(132, 149);
			SetDex(152, 168);
			SetInt(251, 280);

			SetHits(304, 321);

			SetDamage(11, 20);

			SetDamageType(ResistanceType.Physical, 100);

			SetResistance(ResistanceType.Physical, 40, 50);
			SetResistance(ResistanceType.Fire, 15, 25);
			SetResistance(ResistanceType.Cold, 40, 45);
			SetResistance(ResistanceType.Poison, 30, 40);
			SetResistance(ResistanceType.Energy, 25, 35);

			SetSkill(SkillName.Meditation, 80.9, 89.9);
			SetSkill(SkillName.EvalInt, 70.3, 78.7);
			SetSkill(SkillName.Magery, 70.7, 75.7);
			SetSkill(SkillName.Anatomy, 0);
			SetSkill(SkillName.MagicResist, 101.7, 117.1);
			SetSkill(SkillName.Tactics, 71.7, 79.8);
			SetSkill(SkillName.Wrestling, 72.5, 79.5);

			Fame = 5000;
			Karma = 5000;

			VirtualArmor = 28; // Don't know what it should be

            m_NextPeaceTime = DateTime.Now;
		}

		public override void GenerateLoot()
		{
			AddLoot(LootPack.AosRich, 3);  // Need to verify
		}

		public override int Meat { get { return 1; } }

        public override void OnThink()
        {
            if (m_NextPeaceTime <= DateTime.Now)
                AreaPeace();

            base.OnThink();
        }

        private DateTime m_NextPeaceTime;
        public virtual int PeaceRange { get { return 5; } }
        public virtual TimeSpan PeaceDuration { get { return TimeSpan.FromMinutes(1); } }

        public virtual void AreaPeace()
        {
            IPooledEnumerable eable = Map.GetClientsInRange(Location, PeaceRange);

            foreach (Server.Network.NetState state in eable)
            {
                if (state.Mobile is PlayerMobile && state.Mobile.CanSee(this))
                {
                    PlayerMobile player = (PlayerMobile)state.Mobile;

                    if (player.PeacedUntil < DateTime.Now)
                    {
                        if (Utility.RandomDouble() < 0.2)
                        {
                            PlaySound(0x5C4);
                            player.PeacedUntil = DateTime.Now + PeaceDuration;
                            player.SendLocalizedMessage(1072065); // You gaze upon the dryad's beauty, and forget to continue battling!
                        }
                        else
                            PlaySound(0x5C5);
                    }
                }
            }
            m_NextPeaceTime = DateTime.Now + TimeSpan.FromSeconds(PeaceMinDelay + Utility.RandomDouble() * PeaceMaxDelay);
        }

		public MLDryad(Serial serial) : base(serial)
		{
		}

        public override OppositionGroup OppositionGroup
        {
            get { return OppositionGroup.FeyAndUndead; }
        }

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int)0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();
            m_NextPeaceTime = DateTime.Now;
		}
	}
}
