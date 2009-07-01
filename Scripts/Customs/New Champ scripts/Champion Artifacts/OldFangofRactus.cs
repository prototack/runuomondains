using System;
using Server.Network;
using Server.Items;

namespace Server.Items
{
	public class FangofRactus : Kryss
    {
        public override int LabelNumber { get { return 1094892; } } // Fang of Ractus [Replica]

        public override int InitMinHits { get { return 150; } }
        public override int InitMaxHits { get { return 150; } }

		public override bool CanFortify{ get{ return false; } }

		[Constructable]
		public FangofRactus()
		{
            Hue = 0xB3;

            Attributes.SpellChanneling = 1;
            WeaponAttributes.HitPoisonArea = 20;
            Attributes.AttackChance = 5;
            Attributes.DefendChance = 5;
            Attributes.WeaponDamage = 35;
            WeaponAttributes.ResistPoisonBonus = 15;
		}

        public FangofRactus(Serial serial)
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