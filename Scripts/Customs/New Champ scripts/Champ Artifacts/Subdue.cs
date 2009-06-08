using System;
using Server.Network;
using Server.Items;
using Server.Engines.Harvest;

namespace Server.Items
{
	public class Subdue : Scythe
    {
        public override int LabelNumber { get { return 1094930; } } // Subdue [Replica]

		public override int InitMinHits{ get{ return 255; } }
		public override int InitMaxHits{ get{ return 255; } }

		[Constructable]
		public Subdue()
		{
            ItemID = 0x26BA;

            Attributes.SpellChanneling = 1;
            WeaponAttributes.HitLeechMana = 100;
            Attributes.AttackChance = 10;
            Attributes.WeaponSpeed = 20;
            Attributes.WeaponDamage = 50;
            WeaponAttributes.UseBestSkill = 1;
		}

        public Subdue(Serial serial)
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

			if ( Weight == 15.0 )
				Weight = 5.0;
		}
	}
}