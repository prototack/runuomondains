using System;
using Server.Network;
using Server.Items;

namespace Server.Items
{
	[FlipableAttribute( 0xF4D, 0xF4E )]
    public class Quell : Bardiche
	{
        public override int LabelNumber { get { return 1094928; } } // Quell [Replica] 

		public override int InitMinHits{ get{ return 150; } }
		public override int InitMaxHits{ get{ return 150; } }

		[Constructable]
		public Quell()
		{
            Weight = 7.0;
            WeaponAttributes.HitLeechMana = 100;
            Attributes.SpellChanneling = 1;
            Attributes.AttackChance = 10;
            Attributes.WeaponSpeed = 20;
            Attributes.WeaponDamage = 50;
            WeaponAttributes.UseBestSkill = 1;
		}

        public Quell(Serial serial)
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