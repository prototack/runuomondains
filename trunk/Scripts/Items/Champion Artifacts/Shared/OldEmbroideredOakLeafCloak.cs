using System;
using Server.Items;

namespace Server.Items
{
	public class EmbroideredOakLeafCloak : BaseArmor
    {
        public override int LabelNumber { get { return 1094901; } } // Embroidered Oak Leaf Cloak [Replica]

        public override int InitMinHits { get { return 150; } }
        public override int InitMaxHits { get { return 150; } }

        public override ArmorMaterialType MaterialType { get { return ArmorMaterialType.Leather; } }
        public override ArmorMeditationAllowance DefMedAllowance { get { return ArmorMeditationAllowance.All; } }

		[Constructable]
        public EmbroideredOakLeafCloak()
            : base(0x2684)
		{
            Hue = 0x483;
            Layer = Layer.OuterTorso;
            Weight = 1;
            SkillBonuses.SetValues(0, SkillName.Stealth, 5);
		}

        public EmbroideredOakLeafCloak(Serial serial)
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