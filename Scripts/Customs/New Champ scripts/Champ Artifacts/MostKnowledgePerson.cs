using System;
using Server.Items;

namespace Server.Items
{
    public class MostKnowledgePerson : BaseArmor, ITokunoDyable
    {
        public override int LabelNumber { get { return 1094893; } } // The Most Knowledge Person [Replica]

        public override int InitMinHits { get { return 150; } }
        public override int InitMaxHits { get { return 150; } }

        public override ArmorMaterialType MaterialType { get { return ArmorMaterialType.Leather; } }
        public override ArmorMeditationAllowance DefMedAllowance { get { return ArmorMeditationAllowance.All; } }

		[Constructable]
        public MostKnowledgePerson()
            : base(0x2683)
		{
            Hue = 0x117;
            Layer = Layer.OuterTorso;
            Weight = 1;
            Attributes.BonusHits = Utility.RandomMinMax(3, 5);
		}

        public MostKnowledgePerson(Serial serial)
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