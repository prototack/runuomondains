using System;

namespace Server.Items
{
    public class RoyalGuardBodySash : BaseArmor
    {
        public override int LabelNumber { get { return 1094910; } } // Lieutenant of the Britannian Royal Guard [Replica]

        public override int InitMinHits { get { return 150; } }
        public override int InitMaxHits { get { return 150; } }

        public override int AosStrReq { get { return 10; } }

        public override ArmorMaterialType MaterialType { get { return ArmorMaterialType.Leather; } }
        public override ArmorMeditationAllowance DefMedAllowance { get { return ArmorMeditationAllowance.All; } }

        [Constructable]
        public RoyalGuardBodySash()
            : base(0x1541)
        {
            Weight = 1.0;
            Hue = 0x84;
            Attributes.BonusInt = 5;
            Attributes.RegenMana = 2;
            Attributes.LowerRegCost = 10;
        }

        public RoyalGuardBodySash(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }
}