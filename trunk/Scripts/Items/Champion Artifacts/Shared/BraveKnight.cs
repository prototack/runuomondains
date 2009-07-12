using System;
using Server.Items;

namespace Server.Items
{
    public class BraveKnight : Katana, ITokunoDyable
    {
        public override int LabelNumber { get { return 1094909; } } // Brave Knight of The Britannia [Replica]

		public override int InitMinHits{ get{ return 150; } }
		public override int InitMaxHits{ get{ return 150; } }

		public override bool CanFortify{ get{ return false; } }

		[Constructable]
		public BraveKnight()
		{
            Hue = 1150;

            WeaponAttributes.HitLeechStam = 48;
            WeaponAttributes.HitHarm = 26;
            WeaponAttributes.HitLeechHits = 22;
            Attributes.WeaponSpeed = 30;
            Attributes.WeaponDamage = 35;
		}

        #region Mondain's Legacy
        public override void GetDamageTypes(Mobile wielder, out int phys, out int fire, out int cold, out int pois, out int nrgy, out int chaos, out int direct)
        {
            phys = chaos = direct = 0;
            fire = 40;
            cold = 30;
            pois = 10;
            nrgy = 20;
        }
        #endregion

        public BraveKnight(Serial serial)
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