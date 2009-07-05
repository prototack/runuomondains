using System;
using Server.Items;

namespace Server.Items
{
	public class RobeofBritanniaAri : BaseArmor
    {
        public override int LabelNumber { get { return 1094931; } } // The Robe of Britannia "Ari" [Replica]

        public override int BasePhysicalResistance { get { return 10; } }

        public override int InitMinHits { get { return 150; } }
        public override int InitMaxHits { get { return 150; } }

		public override bool CanFortify{ get{ return false; } }

        public override ArmorMaterialType MaterialType { get { return ArmorMaterialType.Leather; } }
        public override ArmorMeditationAllowance DefMedAllowance { get { return ArmorMeditationAllowance.All; } }

		[Constructable]
        public RobeofBritanniaAri()
            : base(0x2683)
		{
            Hue = 0x138;
            Layer = Layer.OuterTorso;
            Weight = 1;
		}

        public RobeofBritanniaAri(Serial serial)
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