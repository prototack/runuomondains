using System;

namespace Server.Items
{

    public class SmallRockWater : Item
    {
        [Constructable]
        public SmallRockWater()
            : base(0x3486)
        {
        }

        public SmallRockWater(Serial serial)
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

    public class SmallRocksWater : Item
    {
        [Constructable]
        public SmallRocksWater()
            : base(0x348B)
        {
        }

        public SmallRocksWater(Serial serial)
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