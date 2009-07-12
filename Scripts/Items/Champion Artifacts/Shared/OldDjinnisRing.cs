using System;
using Server.Items;

namespace Server.Items
{
    public class DjinnisRing : BaseArmor, ITokunoDyable
    {
        public override int LabelNumber { get { return 1094927; } } // Djinni's Ring [Replica]

		public override int InitMinHits{ get{ return 150; } }
		public override int InitMaxHits{ get{ return 150; } }

        public override ArmorMaterialType MaterialType { get { return ArmorMaterialType.Plate; } }
        public override ArmorMeditationAllowance DefMedAllowance { get { return ArmorMeditationAllowance.All; } }

		[Constructable]
        public DjinnisRing()
            : base(0x1F09)
        {
            Layer = Layer.Ring;
            Weight = 1;
            Attributes.SpellDamage = 10;
            Attributes.BonusInt = 5;
            Attributes.CastSpeed = 2;
		}

        public DjinnisRing(Serial serial)
            : base(serial)
		{
		}
		
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}
		
		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
}