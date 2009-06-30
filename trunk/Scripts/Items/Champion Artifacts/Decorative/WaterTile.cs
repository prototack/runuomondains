using System;
using Server.Network;
using Server.Items;

namespace Server.Items
{
	public class WaterTile : Item
	{
		[Constructable]
		public WaterTile() : base( 0x346E )
        {
            ItemID = Utility.RandomList(0x346E, 0x3486, 0x348B, 0x3226, 0x3213, 0x3220);
		}

		public WaterTile( Serial serial ) : base( serial )
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
