using System;
using Server;

namespace Server.Items
{
    public class CaptainJohnsHat : TricorneHat
    {
        public override int LabelNumber { get { return 1094911; } } // Captain John's Hat [Replica]

        public override int BasePhysicalResistance { get { return 2; } }
        public override int BaseFireResistance { get { return 6; } }
        public override int BaseColdResistance { get { return 9; } }
        public override int BasePoisonResistance { get { return 7; } }
        public override int BaseEnergyResistance { get { return 23; } }

        public override int InitMinHits { get { return 255; } }
        public override int InitMaxHits { get { return 255; } }

        [Constructable]
        public CaptainJohnsHat()
        {
            Hue = 0x497;

            SkillBonuses.SetValues(0, SkillName.Swords, 20.0);

            Attributes.BonusDex = 8;
            Attributes.AttackChance = 15;
            Attributes.NightSight = 1;
        }

        public CaptainJohnsHat(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)3);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();

            if (version < 3)
            {
                Resistances.Cold = 0;
                Resistances.Poison = 0;
            }

            if (version < 1)
            {
                Attributes.Luck = 0;
                Attributes.AttackChance = 10;
                Attributes.NightSight = 1;
                SkillBonuses.SetValues(0, Utility.RandomCombatSkill(), 10.0);
                SkillBonuses.SetBonus(1, 0);
            }
        }
    }
}