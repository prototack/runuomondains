using System;
using Server;
using Server.Mobiles;
using Server.Items;

namespace Server.Items
{
	public class RandomTalisman : BaseTalisman
	{
		public RandomTalisman() : base( GetRandomItemID() )
		{
			MaxCharges = GetRandomCharges();		
			Summoner = GetRandomSummoner();
			
			if ( Summoner == null )
			{
				Removal = GetRandomRemoval();
				
				if ( Removal != TalismanRemoval.None )
					MaxChargeTime = 1200;
			}
			else if ( MaxCharges > 0 )
			{
				if (Summoner.Type == typeof( IronIngot ) ||
					Summoner.Type == typeof( Board ) ||
					Summoner.Type == typeof( Bandage ) 
				)
					MaxChargeTime = 60;
				else
					MaxChargeTime = 1800;
			}	
			else
				MaxChargeTime = 1800;
				
			Blessed = GetRandomBlessed();
			Slayer = GetRandomSlayer();	
			Protection = GetRandomProtection();
			Killer = GetRandomKiller();
			Skill = GetRandomSkill(); 
			ExceptionalBonus = GetRandomExceptional();	
			SuccessBonus = GetRandomSuccessful();	
			
			if ( Summoner == null && Removal == TalismanRemoval.None )
			{
				MaxCharges = 0;
				MaxChargeTime = 0;
			}
			else
				Charges = MaxCharges;
				
		}
		
		public RandomTalisman( Serial serial ) :  base( serial )
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