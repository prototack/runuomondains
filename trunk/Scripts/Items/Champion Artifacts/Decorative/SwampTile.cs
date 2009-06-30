using System;
using Server.Network;
using Server.Items;

namespace Server.Items
{
	public class SwampTile : Item
	{
		[Constructable]
		public SwampTile() : base( 0x320D )
        {
            ItemID = Utility.RandomList(0x320D, 0x3236, 0x3241, 0x320D, 0x3226, 0x3213, 0x3220);
		}

		public SwampTile( Serial serial ) : base( serial )
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
