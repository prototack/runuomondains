using System;
using Server.Network;
using Server.Items;

namespace Server.Items
{
    public class Calm : Halberd, ITokunoDyable
    {
        public override int LabelNumber { get { return 1094915; } } // Calm [Replica]

		public override int InitMinHits{ get{ return 150; } }
		public override int InitMaxHits{ get{ return 150; } }

		public override bool CanFortify{ get{ return false; } }

		[Constructable]
		public Calm()
        {
            WeaponAttributes.HitLeechMana = 100;
            Attributes.WeaponSpeed = 20;
            Attributes.SpellChanneling = 1;
            Attributes.WeaponDamage = 50;
            WeaponAttributes.UseBestSkill = 1;
		}

        public Calm(Serial serial)
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