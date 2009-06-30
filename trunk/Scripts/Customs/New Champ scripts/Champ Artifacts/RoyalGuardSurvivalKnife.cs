using System;
using Server.Network;
using Server.Items;

namespace Server.Items
{
    public class RoyalGuardSurvivalKnife : ButcherKnife
    {
        public override int LabelNumber { get { return 1094918; } } // Royal Guard Survival Knife [Replica]

		public override int InitMinHits{ get{ return 150; } }
		public override int InitMaxHits{ get{ return 150; } }

		[Constructable]
		public RoyalGuardSurvivalKnife()
        {
            Attributes.SpellChanneling = 1;
            Attributes.Luck = 140;
            Attributes.EnhancePotions = 25;
            WeaponAttributes.UseBestSkill = 1;
            WeaponAttributes.LowerStatReq = 50;
		}

        public RoyalGuardSurvivalKnife(Serial serial)
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