using System;

namespace Server.Items
{
    public class NecromancerShroud : BaseArmor
    {
        public override int LabelNumber { get { return 1094913; } } // A Necromancer Shroud [Replica]

        public override int BaseColdResistance { get { return 5; } }

        public override int InitMinHits { get { return 150; } }
        public override int InitMaxHits { get { return 150; } }

        public override int AosStrReq { get { return 10; } }

        public override ArmorMaterialType MaterialType { get { return ArmorMaterialType.Leather; } }
        public override ArmorMeditationAllowance DefMedAllowance { get { return ArmorMeditationAllowance.All; } }

		[Constructable]
        public NecromancerShroud()
            : base(0x1F03)
		{
            Hue = 0x7E3;
		}

        public NecromancerShroud(Serial serial)
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