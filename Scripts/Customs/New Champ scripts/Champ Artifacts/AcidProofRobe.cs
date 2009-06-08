using System;

namespace Server.Items
{
	[Flipable]
	public class AcidProofRobe : Robe
    {
        public override int LabelNumber { get { return 1095236; } } // Acid-Proof Robe [Replica]

        public override int BaseFireResistance { get { return 4; } }

		[Constructable]
		public AcidProofRobe()
		{
            Hue = 0x7E3;
            LootType = LootType.Blessed;
		}

        public AcidProofRobe(Serial serial)
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