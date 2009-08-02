using System;
using Server;
using Server.Engines.Harvest;

namespace Server.Items
{
	public class GargoylesPickaxe : BaseAxe, IUsesRemaining
	{
		public override int LabelNumber{ get{ return 1041281; } } // a gargoyle's pickaxe
		public override HarvestSystem HarvestSystem{ get{ return Mining.System; } }

		public override WeaponAbility PrimaryAbility{ get{ return WeaponAbility.DoubleStrike; } }
		public override WeaponAbility SecondaryAbility{ get{ return WeaponAbility.Disarm; } }

		public override int AosStrengthReq{ get{ return 50; } }
		public override int AosMinDamage{ get{ return 13; } }
		public override int AosMaxDamage{ get{ return 15; } }
		public override int AosSpeed{ get{ return 35; } }
		
		#region Mondain's Legacy
		public override float MlSpeed{ get{ return 3.00f; } }
		#endregion

		public override int OldStrengthReq{ get{ return 25; } }
		public override int OldMinDamage{ get{ return 1; } }
		public override int OldMaxDamage{ get{ return 15; } }
		public override int OldSpeed{ get{ return 35; } }

		public override WeaponAnimation DefAnimation{ get{ return WeaponAnimation.Slash1H; } }

		[Constructable]
		public GargoylesPickaxe() : this( 100 )
		{
		}

		[Constructable]
		public GargoylesPickaxe( int uses ) : base( 0xE86 )
		{
			Weight = 11.0;
			Hue = 0x973;
			UsesRemaining = uses;
			ShowUsesRemaining = true;
		}

		public GargoylesPickaxe( Serial serial ) : base( serial )
		{
		}
		
		public override void OnDoubleClick( Mobile from )
		{
			Point3D loc = this.GetWorldLocation();
			
			if ( HarvestSystem == null || Deleted )
				return;
			
			if ( !from.InLOS( loc ) || !from.InRange( loc, 2 ) )
			{
				from.LocalOverheadMessage( Server.Network.MessageType.Regular, 0x3E9, 1019045 ); // I can't reach that
				return;
			}
			else if ( !this.IsAccessibleTo( from ) )
			{
				this.PublicOverheadMessage( Server.Network.MessageType.Regular, 0x3E9, 1061637 ); // You are not allowed to access this.
				return;
			}

			HarvestSystem.BeginHarvesting( from, this );
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