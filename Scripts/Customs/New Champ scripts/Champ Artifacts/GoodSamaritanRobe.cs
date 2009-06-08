using System;

namespace Server.Items
{
	public class GoodSamaritanRobe : Robe
    {
        public override int LabelNumber { get { return 1094926; } } // Good Samaritan of Britannia [Replica]

        public override int BasePhysicalResistance { get { return 5; } }

		[Constructable]
		public GoodSamaritanRobe()
		{
            Hue = 0x232;
		}

        public GoodSamaritanRobe(Serial serial)
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
}