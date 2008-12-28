using System;

using Server;
using Server.Items;

namespace Server.Engines.Quests
{
	public class EscortToYewQuest : BaseQuest
	{   			
		/* An escort to Yew */
		public override object Title{ get{ return 1072275; } }
		
		/* I seek a worthy escort.  I can offer some small pay to any able bodied adventurer who can assist me.  
		 * It is imperative that I reach my destination. */ 
		public override object Description{ get{ return 1072287; } }
		
		/* I wish you would reconsider my offer.  I'll be waiting right here for someone brave enough to assist me. */
		public override object Refuse{ get{ return 1072288; } }
		
		/* We have not yet arrived in Yew.  Let's keep going. */
		public override object Uncomplete{ get{ return 1072289; } }
		
		public EscortToYewQuest() : base()
		{ 
			AddObjective( new EscortObjective( "Yew" ) );		  
			AddReward( new BaseReward( typeof( Gold ), 500, 1062577 ) ); 
		}
		
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			
			writer.WriteEncodedInt( 0 ); // version
		}
		
		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			
			int version = reader.ReadEncodedInt();
		}
	}

	public class EscortToVesperQuest : BaseQuest
	{   			
		/* An escort to Vesper */
		public override object Title{ get{ return 1072276; } }
		
		/* I seek a worthy escort.  I can offer some small pay to any able bodied adventurer who can assist me.  
		 * It is imperative that I reach my destination. */ 
		public override object Description{ get{ return 1072287; } }
		
		/* I wish you would reconsider my offer.  I'll be waiting right here for someone brave enough to assist me. */
		public override object Refuse{ get{ return 1072288; } }
		
		/* We have not yet arrived in Vesper.  Let's keep going. */
		public override object Uncomplete{ get{ return 1072290; } }
		
		public EscortToVesperQuest() : base()
		{ 
			AddObjective( new EscortObjective( "Vesper" ) );		  
			AddReward( new BaseReward( typeof( Gold ), 500, 1062577 ) ); 
		}
		
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			
			writer.WriteEncodedInt( 0 ); // version
		}
		
		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			
			int version = reader.ReadEncodedInt();
		}
	}

	public class EscortToTrinsicQuest : BaseQuest
	{   			
		/* An escort to Trinsic */
		public override object Title{ get{ return 1072277; } }
		
		/* I seek a worthy escort.  I can offer some small pay to any able bodied adventurer who can assist me.  
		 * It is imperative that I reach my destination. */ 
		public override object Description{ get{ return 1072287; } }
		
		/* I wish you would reconsider my offer.  I'll be waiting right here for someone brave enough to assist me. */
		public override object Refuse{ get{ return 1072288; } }
		
		/* We have not yet arrived in Trinsic.  Let's keep going. */
		public override object Uncomplete{ get{ return 1072291; } }
		
		public EscortToTrinsicQuest() : base()
		{ 
			AddObjective( new EscortObjective( "Trinsic" ) );		  
			AddReward( new BaseReward( typeof( Gold ), 500, 1062577 ) ); 
		}
		
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			
			writer.WriteEncodedInt( 0 ); // version
		}
		
		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			
			int version = reader.ReadEncodedInt();
		}
	}

	public class EscortToSkaraQuest : BaseQuest
	{   			
		/* An escort to Skara */
		public override object Title{ get{ return 1072278; } }
		
		/* I seek a worthy escort.  I can offer some small pay to any able bodied adventurer who can assist me.  
		 * It is imperative that I reach my destination. */ 
		public override object Description{ get{ return 1072287; } }
		
		/* I wish you would reconsider my offer.  I'll be waiting right here for someone brave enough to assist me. */
		public override object Refuse{ get{ return 1072288; } }
		
		/* We have not yet arrived in Skara.  Let's keep going. */
		public override object Uncomplete{ get{ return 1072292; } }
		
		public EscortToSkaraQuest() : base()
		{ 
			AddObjective( new EscortObjective( "Skara Brae" ) );		  
			AddReward( new BaseReward( typeof( Gold ), 500, 1062577 ) ); 
		}
		
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			
			writer.WriteEncodedInt( 0 ); // version
		}
		
		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			
			int version = reader.ReadEncodedInt();
		}
	}

	public class EscortToSerpentsHoldQuest : BaseQuest
	{   			
		/* An escort to Serpent's Hold */
		public override object Title{ get{ return 1072279; } }
		
		/* I seek a worthy escort.  I can offer some small pay to any able bodied adventurer who can assist me.  
		 * It is imperative that I reach my destination. */ 
		public override object Description{ get{ return 1072287; } }
		
		/* I wish you would reconsider my offer.  I'll be waiting right here for someone brave enough to assist me. */
		public override object Refuse{ get{ return 1072288; } }
		
		/* We have not yet arrived in Serpent's Hold.  Let's keep going. */
		public override object Uncomplete{ get{ return 1072293; } }
		
		public EscortToSerpentsHoldQuest() : base()
		{ 
			AddObjective( new EscortObjective( "Seprent's Hold" ) );		  
			AddReward( new BaseReward( typeof( Gold ), 500, 1062577 ) ); 
		}
		
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			
			writer.WriteEncodedInt( 0 ); // version
		}
		
		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			
			int version = reader.ReadEncodedInt();
		}
	}

	public class EscortToNujelmQuest : BaseQuest
	{   			
		/* An escort to Nujel'm */
		public override object Title{ get{ return 1072280; } }
		
		/* I seek a worthy escort.  I can offer some small pay to any able bodied adventurer who can assist me.  
		 * It is imperative that I reach my destination. */ 
		public override object Description{ get{ return 1072287; } }
		
		/* I wish you would reconsider my offer.  I'll be waiting right here for someone brave enough to assist me. */
		public override object Refuse{ get{ return 1072288; } }
		
		/* We have not yet arrived in Nujel'm.  Let's keep going. */
		public override object Uncomplete{ get{ return 1072294; } }
		
		public EscortToNujelmQuest() : base()
		{ 
			AddObjective( new EscortObjective( "Nujel'm" ) );		  
			AddReward( new BaseReward( typeof( Gold ), 500, 1062577 ) ); 
		}
		
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			
			writer.WriteEncodedInt( 0 ); // version
		}
		
		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			
			int version = reader.ReadEncodedInt();
		}
	}

	public class EscortToMoonglowQuest : BaseQuest
	{   			
		/* An escort to Moonglow */
		public override object Title{ get{ return 1072281; } }
		
		/* I seek a worthy escort.  I can offer some small pay to any able bodied adventurer who can assist me.  
		 * It is imperative that I reach my destination. */ 
		public override object Description{ get{ return 1072287; } }
		
		/* I wish you would reconsider my offer.  I'll be waiting right here for someone brave enough to assist me. */
		public override object Refuse{ get{ return 1072288; } }
		
		/* We have not yet arrived in Moonglow.  Let's keep going. */
		public override object Uncomplete{ get{ return 1072295; } }
		
		public EscortToMoonglowQuest() : base()
		{ 
			AddObjective( new EscortObjective( "Moonglow" ) );		  
			AddReward( new BaseReward( typeof( Gold ), 500, 1062577 ) ); 
		}
		
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			
			writer.WriteEncodedInt( 0 ); // version
		}
		
		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			
			int version = reader.ReadEncodedInt();
		}
	}

	public class EscortToMinocQuest : BaseQuest
	{   			
		/* An escort to Minoc */
		public override object Title{ get{ return 1072282; } }
		
		/* I seek a worthy escort.  I can offer some small pay to any able bodied adventurer who can assist me.  
		 * It is imperative that I reach my destination. */ 
		public override object Description{ get{ return 1072287; } }
		
		/* I wish you would reconsider my offer.  I'll be waiting right here for someone brave enough to assist me. */
		public override object Refuse{ get{ return 1072288; } }
		
		/* We have not yet arrived in Minoc.  Let's keep going. */
		public override object Uncomplete{ get{ return 1072296; } }
		
		public EscortToMinocQuest() : base()
		{ 
			AddObjective( new EscortObjective( "Minoc" ) );		  
			AddReward( new BaseReward( typeof( Gold ), 500, 1062577 ) ); 
		}
		
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			
			writer.WriteEncodedInt( 0 ); // version
		}
		
		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			
			int version = reader.ReadEncodedInt();
		}
	}

	public class EscortToMaginciaQuest : BaseQuest
	{   			
		/* An escort to Magincia */
		public override object Title{ get{ return 1072283; } }
		
		/* I seek a worthy escort.  I can offer some small pay to any able bodied adventurer who can assist me.  
		 * It is imperative that I reach my destination. */ 
		public override object Description{ get{ return 1072287; } }
		
		/* I wish you would reconsider my offer.  I'll be waiting right here for someone brave enough to assist me. */
		public override object Refuse{ get{ return 1072288; } }
		
		/* We have not yet arrived in Magincia.  Let's keep going. */
		public override object Uncomplete{ get{ return 1072297; } }
		
		public EscortToMaginciaQuest() : base()
		{ 
			AddObjective( new EscortObjective( "Magincia" ) );		  
			AddReward( new BaseReward( typeof( Gold ), 500, 1062577 ) ); 
		}
		
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			
			writer.WriteEncodedInt( 0 ); // version
		}
		
		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			
			int version = reader.ReadEncodedInt();
		}
	}

	public class EscortToJhelomQuest : BaseQuest
	{   			
		/* An escort to Jhelom */
		public override object Title{ get{ return 1072284; } }
		
		/* I seek a worthy escort.  I can offer some small pay to any able bodied adventurer who can assist me.  
		 * It is imperative that I reach my destination. */ 
		public override object Description{ get{ return 1072287; } }
		
		/* I wish you would reconsider my offer.  I'll be waiting right here for someone brave enough to assist me. */
		public override object Refuse{ get{ return 1072288; } }
		
		/* We have not yet arrived in Jhelom.  Let's keep going. */
		public override object Uncomplete{ get{ return 1072298; } }
		
		public EscortToJhelomQuest() : base()
		{ 
			AddObjective( new EscortObjective( "Jhelom" ) );		  
			AddReward( new BaseReward( typeof( Gold ), 500, 1062577 ) ); 
		}
		
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			
			writer.WriteEncodedInt( 0 ); // version
		}
		
		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			
			int version = reader.ReadEncodedInt();
		}
	}

	public class EscortToCoveQuest : BaseQuest
	{   			
		/* An escort to Cove */
		public override object Title{ get{ return 1072285; } }
		
		/* I seek a worthy escort.  I can offer some small pay to any able bodied adventurer who can assist me.  
		 * It is imperative that I reach my destination. */ 
		public override object Description{ get{ return 1072287; } }
		
		/* I wish you would reconsider my offer.  I'll be waiting right here for someone brave enough to assist me. */
		public override object Refuse{ get{ return 1072288; } }
		
		/* We have not yet arrived in Cove.  Let's keep going. */
		public override object Uncomplete{ get{ return 1072299; } }
		
		public EscortToCoveQuest() : base()
		{ 
			AddObjective( new EscortObjective( "Cove" ) );		  
			AddReward( new BaseReward( typeof( Gold ), 500, 1062577 ) ); 
		}
		
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			
			writer.WriteEncodedInt( 0 ); // version
		}
		
		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			
			int version = reader.ReadEncodedInt();
		}
	}

	public class EscortToBritainQuest : BaseQuest
	{   			
		/* An escort to Britain */
		public override object Title{ get{ return 1072286; } }
		
		/* I seek a worthy escort.  I can offer some small pay to any able bodied adventurer who can assist me.  
		 * It is imperative that I reach my destination. */ 
		public override object Description{ get{ return 1072287; } }
		
		/* I wish you would reconsider my offer.  I'll be waiting right here for someone brave enough to assist me. */
		public override object Refuse{ get{ return 1072288; } }
		
		/* We have not yet arrived in Britain.  Let's keep going. */
		public override object Uncomplete{ get{ return 1072300; } }
		
		public EscortToBritainQuest() : base()
		{ 
			AddObjective( new EscortObjective( "Britain" ) );		  
			AddReward( new BaseReward( typeof( Gold ), 500, 1062577 ) ); 
		}
		
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			
			writer.WriteEncodedInt( 0 ); // version
		}
		
		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			
			int version = reader.ReadEncodedInt();
		}
	}

	public class NewBaseEscortable : BaseEscort
	{
		public override Type[] Quests{ get{ return new Type[] {	m_Quest }; } }

		private Type m_Quest;
	
		[Constructable]
		public NewBaseEscortable() : base()
		{			
			switch ( Utility.Random( 12 ) )
			{
				case 0: m_Quest = typeof( EscortToYewQuest ); break;
				case 1: m_Quest = typeof( EscortToVesperQuest ); break;
				case 2: m_Quest = typeof( EscortToTrinsicQuest ); break;
				case 3: m_Quest = typeof( EscortToSkaraQuest ); break;
				case 4: m_Quest = typeof( EscortToSerpentsHoldQuest ); break;
				case 5: m_Quest = typeof( EscortToNujelmQuest ); break;
				case 6: m_Quest = typeof( EscortToMoonglowQuest ); break;
				case 7: m_Quest = typeof( EscortToMinocQuest ); break;
				case 8: m_Quest = typeof( EscortToMaginciaQuest ); break;
				case 9: m_Quest = typeof( EscortToJhelomQuest ); break;
				case 10: m_Quest = typeof( EscortToCoveQuest ); break;
				case 11: m_Quest = typeof( EscortToBritainQuest ); break;
			}
		}
		
		public NewBaseEscortable( Serial serial ) : base( serial )
		{
		}
		
		public override bool CanBeDamaged()
		{
			return true;
		}
		
		public override void Advertise()
		{
			Say( Utility.RandomMinMax( 1072301, 1072303 ) );
		}
		
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.WriteEncodedInt( 0 ); // version
			
			QuestWriter.Type( writer, m_Quest );
		}
		
		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadEncodedInt();

			m_Quest = QuestReader.Type( reader );
		}
	}

	public class NewMerchant : NewBaseEscortable
	{
		public override bool CanTeach { get { return true; } }
		public override bool ClickTitle { get { return false; } }

		[Constructable]
		public NewMerchant()
		{
			Title = "the merchant";
			SetSkill( SkillName.ItemID, 55.0, 78.0 );
			SetSkill( SkillName.ArmsLore, 55, 78 );
		}

		public NewMerchant( Serial serial ) : base( serial )
		{
		}

		public override void InitOutfit()
		{
			if( Female )
				AddItem( new PlainDress() );
			else
				AddItem( new Shirt( GetRandomHue() ) );

			int lowHue = GetRandomHue();

			AddItem( new ThighBoots() );

			if( Female )
				AddItem( new FancyDress( lowHue ) );
			else
				AddItem( new FancyShirt( lowHue ) );
			AddItem( new LongPants( lowHue ) );

			if( !Female )
				AddItem( new BodySash( lowHue ) );

			Utility.AssignRandomHair( this );

			PackGold( 200, 250 );
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.WriteEncodedInt( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadEncodedInt();
		}
	}	
	
	public class NewEscortableMage : NewBaseEscortable
	{
		public override bool CanTeach{ get{ return true; } }
		public override bool ClickTitle{ get{ return false; } }

		[Constructable]
		public NewEscortableMage()
		{
			Title = "the mage";

			SetSkill( SkillName.EvalInt, 80.0, 100.0 );
			SetSkill( SkillName.Inscribe, 80.0, 100.0 );
			SetSkill( SkillName.Magery, 80.0, 100.0 );
			SetSkill( SkillName.Meditation, 80.0, 100.0 );
			SetSkill( SkillName.MagicResist, 80.0, 100.0 );
		}

		public NewEscortableMage( Serial serial ) : base( serial )
		{
		}

		public override void InitOutfit()
		{
			AddItem( new Robe( GetRandomHue() ) );

			int lowHue = GetRandomHue();

			AddItem( new ShortPants( lowHue ) );

			if ( Female )
				AddItem( new ThighBoots( lowHue ) );
			else
				AddItem( new Boots( lowHue ) );

			Utility.AssignRandomHair( this );

			PackGold( 200, 250 );
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.WriteEncodedInt( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadEncodedInt();
		}
	}
	
	public class NewMessenger : NewBaseEscortable
	{
		public override bool CanTeach{ get{ return true; } }
		public override bool ClickTitle{ get{ return false; } }

		[Constructable]
		public NewMessenger()
		{
			Title = "the messenger";
		}

		public NewMessenger( Serial serial ) : base( serial )
		{
		}

		public override void InitOutfit()
		{
			if ( Female )
				AddItem( new PlainDress() );
			else
				AddItem( new Shirt( GetRandomHue() ) );

			int lowHue = GetRandomHue();

			AddItem( new ShortPants( lowHue ) );

			if ( Female )
				AddItem( new Boots( lowHue ) );
			else
				AddItem( new Shoes( lowHue ) );

			switch ( Utility.Random( 4 ) )
			{
				case 0: AddItem( new ShortHair( Utility.RandomHairHue() ) ); break;
				case 1: AddItem( new TwoPigTails( Utility.RandomHairHue() ) ); break;
				case 2: AddItem( new ReceedingHair( Utility.RandomHairHue() ) ); break;
				case 3: AddItem( new KrisnaHair( Utility.RandomHairHue() ) ); break;
			}

			PackGold( 200, 250 );
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.WriteEncodedInt( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadEncodedInt();
		}
	}
	
	public class NewNoble : NewBaseEscortable
	{
		public override bool CanTeach{ get{ return true; } }
		public override bool ClickTitle{ get{ return false; } }

		[Constructable]
		public NewNoble()
		{
			Title = "the noble";

			SetSkill( SkillName.Parry, 80.0, 100.0 );
			SetSkill( SkillName.Swords, 80.0, 100.0 );
			SetSkill( SkillName.Tactics, 80.0, 100.0 );
		}

		public NewNoble( Serial serial ) : base( serial )
		{
		}

		public override void InitOutfit()
		{
			if ( Female )
				AddItem( new FancyDress() );
			else
				AddItem( new FancyShirt( GetRandomHue() ) );

			int lowHue = GetRandomHue();

			AddItem( new ShortPants( lowHue ) );

			if ( Female )
				AddItem( new ThighBoots( lowHue ) );
			else
				AddItem( new Boots( lowHue ) );

			if ( !Female )
				AddItem( new BodySash( lowHue ) );

			AddItem( new Cloak( GetRandomHue() ) );

			if ( !Female )
				AddItem( new Longsword() );

			Utility.AssignRandomHair( this );

			PackGold( 200, 250 );
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.WriteEncodedInt( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadEncodedInt();
		}
	}
	
	public class NewBrideGroom : NewBaseEscortable
	{
		public override bool CanTeach{ get{ return true; } }
		public override bool ClickTitle{ get{ return false; } }

		[Constructable]
		public NewBrideGroom()
		{
			if ( Female )
				Title = "the bride";
			else
				Title = "the groom";	
		}

		public NewBrideGroom( Serial serial ) : base( serial )
		{
		}

		public override void InitOutfit()
		{
			if ( Female )				
				AddItem( new FancyDress() );
			else
				AddItem( new FancyShirt() );

			int lowHue = GetRandomHue();

			AddItem( new LongPants( lowHue ) );

			if ( Female )
				AddItem( new Shoes( lowHue ) );
			else
				AddItem( new Boots( lowHue ) );

			if( Utility.RandomBool() )
				HairItemID = 0x203B;
			else
				HairItemID = 0x203C;

			HairHue = this.Race.RandomHairHue();

			PackGold( 200, 250 );
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.WriteEncodedInt( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadEncodedInt();
		}
	}
}
  