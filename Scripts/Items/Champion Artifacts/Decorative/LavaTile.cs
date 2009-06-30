using System;
using Server.Network;
using Server.Items;

namespace Server.Items
{
	public class LavaTile : Item
	{
		[Constructable]
		public LavaTile() : base( 0x12EE )
        {
            ItemID = Utility.RandomList(0x12EE, 0x343B, 0x3447, 0x344E, 0x3462, 0x3468);
		}

		public LavaTile( Serial serial ) : base( serial )
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
