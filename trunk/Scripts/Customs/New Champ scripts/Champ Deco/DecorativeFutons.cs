using System;
using Server;

namespace Server.Items
{
    public class DecorativePinkFuton : Item
    {
		[Constructable]
		public DecorativePinkFuton() : base( 0x295C )
		{
			Weight = 5.0;			
		}

        public DecorativePinkFuton(Serial serial)
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

    public class DecorativeGoldFuton : Item
    {
        [Constructable]
        public DecorativeGoldFuton()
            : base(0x295E)
        {
            Weight = 5.0;
        }

        public DecorativeGoldFuton(Serial serial)
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

