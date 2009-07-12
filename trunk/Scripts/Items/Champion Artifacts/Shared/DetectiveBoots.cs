using System;

namespace Server.Items
{
    public class LegendaryDetectiveBoots : Boots, ITokunoDyable
    {
        public override int LabelNumber { get { return 1094894; } } // Legendary Detective of the Royal Guard [Replica]

		[Constructable]
		public LegendaryDetectiveBoots()
        {
            Attributes.BonusInt = 2;
		}

        public LegendaryDetectiveBoots(Serial serial)
            : base(serial)
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

    public class ElderDetectiveBoots : Boots, ITokunoDyable
    {
        public override int LabelNumber { get { return 1094895; } } // Elder Detective of the Royal Guard [Replica]

        [Constructable]
        public ElderDetectiveBoots()
        {
            Attributes.BonusInt = 3;
        }

        public ElderDetectiveBoots(Serial serial)
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

    public class MythicalDetectiveBoots : Boots, ITokunoDyable
    {
        public override int LabelNumber { get { return 1094896; } } // Mythical Detective of the Royal Guard [Replica]

        [Constructable]
        public MythicalDetectiveBoots()
        {
            Attributes.BonusInt = 4;
        }

        public MythicalDetectiveBoots(Serial serial)
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
