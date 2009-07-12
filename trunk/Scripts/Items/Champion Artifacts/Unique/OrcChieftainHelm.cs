using System;
using Server;

namespace Server.Items
{
    public class OrcChieftainHelm : OrcHelm, ITokunoDyable
    {
        public override int LabelNumber { get { return 1094924; } } // Orc Chieftain Helm [Replica]

		public override int BasePhysicalResistance{ get{ return 23; } }
		public override int BaseColdResistance{ get{ return 23; } }

		public override int InitMinHits{ get{ return 150; } }
		public override int InitMaxHits{ get{ return 150; } }

		public override bool CanFortify{ get{ return false; } }

        [Constructable]
        public OrcChieftainHelm()
        {
            Hue = 0x8A4;
            Attributes.Luck = 100;
            Attributes.BonusHits = 30;
            Attributes.RegenHits = 3;
        }

        public OrcChieftainHelm(Serial serial)
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

            if (Weight == 1.0)
                Weight = 5.0;
		}
	}
}