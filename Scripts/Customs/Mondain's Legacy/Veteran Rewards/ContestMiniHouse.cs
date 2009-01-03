using Server.Engines.VeteranRewards;

namespace Server.Items
{
	public class ContestMiniHouse : MiniHouseAddon, IRewardItem
	{
		private bool m_IsRewardItem;

		[CommandProperty( AccessLevel.GameMaster )]
		public bool IsRewardItem
		{
			get { return m_IsRewardItem; }
			set { m_IsRewardItem = value; }
		}

		public override BaseAddonDeed Deed
		{
			get
			{
				if (Type == MiniHouseType.MalasMountainPass)
				{
					MalasMountainPassDeed deed = new MalasMountainPassDeed();
					deed.IsRewardItem = IsRewardItem;
					return deed;
				}
				else if (Type == MiniHouseType.ChurchAtNight)
				{
					ChurchAtNightDeed deed = new ChurchAtNightDeed();
					deed.IsRewardItem = IsRewardItem;
					return deed;
				}
				else
					return new MiniHouseDeed( Type );
			}
		}

		[Constructable]
		public ContestMiniHouse() : this( MiniHouseType.ChurchAtNight )
		{
		}

		[Constructable]
		public ContestMiniHouse( MiniHouseType type ) : base( type )
		{
		}

        public ContestMiniHouse(Serial serial)
            : base(serial)
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.WriteEncodedInt( (int)0 ); // version

			writer.Write( m_IsRewardItem );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadEncodedInt();

			m_IsRewardItem = reader.ReadBool();
		}
	}

    public class ChurchAtNightDeed : MiniHouseDeed, IRewardItem
    {
        private bool m_IsRewardItem;

        [CommandProperty(AccessLevel.GameMaster)]
        public bool IsRewardItem
        {
            get { return m_IsRewardItem; }
            set { m_IsRewardItem = value; }
        }

        public override BaseAddon Addon
        {
            get
            {
                ContestMiniHouse addon = new ContestMiniHouse(Type);
                addon.IsRewardItem = IsRewardItem;
                return addon;
            }
        }

        [Constructable]
        public ChurchAtNightDeed()
            : base(MiniHouseType.ChurchAtNight)
        {
            LootType = LootType.Blessed;
        }

        public ChurchAtNightDeed(Serial serial)
            : base(serial)
        {
        }

        public override void GetProperties(ObjectPropertyList list)
        {
            base.GetProperties(list);

            if (m_IsRewardItem)
                list.Add(1076223); // 7th Year Veteran Reward
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.WriteEncodedInt((int)0); // version

            writer.Write(m_IsRewardItem);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadEncodedInt();

            m_IsRewardItem = reader.ReadBool();
        }
    }

    public class MalasMountainPassDeed : MiniHouseDeed, IRewardItem
    {
        private bool m_IsRewardItem;

        [CommandProperty(AccessLevel.GameMaster)]
        public bool IsRewardItem
        {
            get { return m_IsRewardItem; }
            set { m_IsRewardItem = value; }
        }

        public override BaseAddon Addon
        {
            get
            {
                ContestMiniHouse addon = new ContestMiniHouse(Type);
                addon.IsRewardItem = IsRewardItem;
                return addon;
            }
        }

        [Constructable]
        public MalasMountainPassDeed()
            : base(MiniHouseType.MalasMountainPass)
        {
            LootType = LootType.Blessed;
        }

        public override void GetProperties(ObjectPropertyList list)
        {
            base.GetProperties(list);

            if (m_IsRewardItem)
                list.Add(1076223); // 7th Year Veteran Reward
        }

        public MalasMountainPassDeed(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.WriteEncodedInt((int)0); // version

            writer.Write(m_IsRewardItem);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadEncodedInt();

            m_IsRewardItem = reader.ReadBool();
        }
    }
}