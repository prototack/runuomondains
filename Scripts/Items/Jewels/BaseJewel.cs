using System;
using System.Collections.Generic;
using Server.ContextMenus;
using Server.Engines.Craft;

namespace Server.Items
{
	public enum GemType
	{
		None,
		StarSapphire,
		Emerald,
		Sapphire,
		Ruby,
		Citrine,
		Amethyst,
		Tourmaline,
		Amber,
		Diamond
	}

	public abstract class BaseJewel : Item, ICraftable, ISetItem
	{
		private int m_MaxHitPoints;
		private int m_HitPoints;

		private AosAttributes m_AosAttributes;
		private AosElementAttributes m_AosResistances;
		private AosSkillBonuses m_AosSkillBonuses;
		private CraftResource m_Resource;
		private GemType m_GemType;

		#region Personal Bless Deed
		private Mobile m_BlessedBy;

		[CommandProperty( AccessLevel.GameMaster )]
		public Mobile BlessedBy
		{
			get { return m_BlessedBy; }
			set { m_BlessedBy = value;InvalidateProperties();}
		}

		public override void GetContextMenuEntries( Mobile from, List<ContextMenuEntry> list )
		{
			base.GetContextMenuEntries( from, list );

			if ( BlessedFor == from && BlessedBy == from && RootParent == from )
			{
				list.Add( new UnBlessEntry( from, this ) );
			}
		}

		private class UnBlessEntry : ContextMenuEntry
		{
			private Mobile m_From;
			private BaseJewel m_Item;

			public UnBlessEntry( Mobile from, BaseJewel item ) : base( 6208, -1 )
			{
				m_From = from;
				m_Item = item;
			}

			public override void OnClick()
			{
				m_Item.BlessedFor = null;
				m_Item.BlessedBy = null;

				Container pack = m_From.Backpack;

				if ( pack != null )
				{
					pack.DropItem( new PersonalBlessDeed( m_From ) );
					m_From.SendLocalizedMessage( 1062200 ); // A personal bless deed has been placed in your backpack.
				}
			}
		}
		#endregion

		[CommandProperty( AccessLevel.GameMaster )]
		public int MaxHitPoints
		{
			get{ return m_MaxHitPoints; }
			set{ m_MaxHitPoints = value; InvalidateProperties(); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public int HitPoints
		{
			get 
			{
				return m_HitPoints;
			}
			set 
			{
				if ( value != m_HitPoints && MaxHitPoints > 0 )
				{
					m_HitPoints = value;

					if ( m_HitPoints < 0 )
						Delete();
					else if ( m_HitPoints > MaxHitPoints )
						m_HitPoints = MaxHitPoints;

					InvalidateProperties();
				}
			}
		}

		[CommandProperty( AccessLevel.Player )]
		public AosAttributes Attributes
		{
			get{ return m_AosAttributes; }
			set{}
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public AosElementAttributes Resistances
		{
			get{ return m_AosResistances; }
			set{}
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public AosSkillBonuses SkillBonuses
		{
			get{ return m_AosSkillBonuses; }
			set{}
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public CraftResource Resource
		{
			get{ return m_Resource; }
			set{ m_Resource = value; Hue = CraftResources.GetHue( m_Resource ); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public GemType GemType
		{
			get{ return m_GemType; }
			set{ m_GemType = value; InvalidateProperties(); }
		}

		public override int PhysicalResistance{ get{ return m_AosResistances.Physical; } }
		public override int FireResistance{ get{ return m_AosResistances.Fire; } }
		public override int ColdResistance{ get{ return m_AosResistances.Cold; } }
		public override int PoisonResistance{ get{ return m_AosResistances.Poison; } }
		public override int EnergyResistance{ get{ return m_AosResistances.Energy; } }
		public virtual int BaseGemTypeNumber{ get{ return 0; } }

		public virtual int InitMinHits{ get{ return 0; } }
		public virtual int InitMaxHits{ get{ return 0; } }

		public override int LabelNumber
		{
			get
			{
				if ( m_GemType == GemType.None )
					return base.LabelNumber;

				return BaseGemTypeNumber + (int)m_GemType - 1;
			}
		}

		public override void OnAfterDuped( Item newItem )
		{
			BaseJewel jewel = newItem as BaseJewel;

			if ( jewel == null )
				return;

			jewel.m_AosAttributes = new AosAttributes( newItem, m_AosAttributes );
			jewel.m_AosResistances = new AosElementAttributes( newItem, m_AosResistances );
			jewel.m_AosSkillBonuses = new AosSkillBonuses( newItem, m_AosSkillBonuses );

			#region Mondain's Legacy
			jewel.m_SetAttributes = new AosAttributes( newItem, m_SetAttributes );
			jewel.m_SetSkillBonuses = new AosSkillBonuses( newItem, m_SetSkillBonuses );
			#endregion
		}

		public virtual int ArtifactRarity{ get{ return 0; } }

		#region Mondain's Legacy
		private Mobile m_Crafter;
		private ArmorQuality m_Quality;

		[CommandProperty( AccessLevel.GameMaster )]
		public Mobile Crafter
		{
			get{ return m_Crafter; }
			set{ m_Crafter = value; InvalidateProperties(); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public ArmorQuality Quality
		{
			get{ return m_Quality; }
			set{ m_Quality = value; InvalidateProperties(); }
		}
		#endregion

		public BaseJewel( int itemID, Layer layer ) : base( itemID )
		{
			m_AosAttributes = new AosAttributes( this );
			m_AosResistances = new AosElementAttributes( this );
			m_AosSkillBonuses = new AosSkillBonuses( this );
			m_Resource = CraftResource.Iron;
			m_GemType = GemType.None;

			Layer = layer;

			m_HitPoints = m_MaxHitPoints = Utility.RandomMinMax( InitMinHits, InitMaxHits );

			#region Mondain's Legacy Sets
			m_SetAttributes = new AosAttributes( this );
			m_SetSkillBonuses = new AosSkillBonuses( this );
			#endregion
        }

        #region Personal Bless Deed
        public override bool CanEquip(Mobile from)
        {
            if (BlessedBy != null && BlessedBy != from)
            {
                from.SendLocalizedMessage(1075277); // That item is blessed by another player.

                return false;
            }
            else
            {
                return base.CanEquip(from);
            }
        }
        #endregion

		public override void OnAdded( object parent )
		{
			if ( Core.AOS && parent is Mobile )
			{
				Mobile from = (Mobile)parent;

				m_AosSkillBonuses.AddTo( from );

				int strBonus = m_AosAttributes.BonusStr;
				int dexBonus = m_AosAttributes.BonusDex;
				int intBonus = m_AosAttributes.BonusInt;

				if ( strBonus != 0 || dexBonus != 0 || intBonus != 0 )
				{
					string modName = this.Serial.ToString();

					if ( strBonus != 0 )
						from.AddStatMod( new StatMod( StatType.Str, modName + "Str", strBonus, TimeSpan.Zero ) );

					if ( dexBonus != 0 )
						from.AddStatMod( new StatMod( StatType.Dex, modName + "Dex", dexBonus, TimeSpan.Zero ) );

					if ( intBonus != 0 )
						from.AddStatMod( new StatMod( StatType.Int, modName + "Int", intBonus, TimeSpan.Zero ) );
				}

				from.CheckStatTimers();

				#region Mondain's Legacy Sets
				if ( IsSetItem )
				{
					m_SetEquipped = SetHelper.FullSetEquipped( from, SetID, Pieces );

					if ( m_SetEquipped )
					{
						m_LastEquipped = true;
						SetHelper.AddSetBonus( from, SetID );
					}
				}
				#endregion
			}
		}

		public override void OnRemoved( object parent )
		{
			if ( Core.AOS && parent is Mobile )
			{
				Mobile from = (Mobile)parent;

				m_AosSkillBonuses.Remove();

				string modName = this.Serial.ToString();

				from.RemoveStatMod( modName + "Str" );
				from.RemoveStatMod( modName + "Dex" );
				from.RemoveStatMod( modName + "Int" );

				from.CheckStatTimers();

				#region Mondain's Legacy Sets
				if ( IsSetItem && m_SetEquipped )
					SetHelper.RemoveSetBonus( from, SetID, this );
				#endregion
			}
		}

		public BaseJewel( Serial serial ) : base( serial )
		{
		}

		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );

			#region Mondain's Legacy
			if ( m_Quality == ArmorQuality.Exceptional )
				list.Add( 1063341 ); // exceptional

			if ( m_Crafter != null )
				list.Add( 1050043, m_Crafter.Name ); // crafted by ~1_NAME~
			#endregion

			#region Mondain's Legacy Sets
			if ( IsSetItem )
			{
				list.Add( 1080240, Pieces.ToString() ); // Part of a Jewelry Set (~1_val~ pieces)

				if ( m_SetEquipped )
				{
					list.Add( 1080241 ); // Full Jewelry Set Present
					SetHelper.GetSetProperties( list, this );
				}
			}
			#endregion

			m_AosSkillBonuses.GetProperties( list );

			int prop;

			if ( (prop = ArtifactRarity) > 0 )
				list.Add( 1061078, prop.ToString() ); // artifact rarity ~1_val~

			if ( (prop = m_AosAttributes.WeaponDamage) != 0 )
				list.Add( 1060401, prop.ToString() ); // damage increase ~1_val~%

			if ( (prop = m_AosAttributes.DefendChance) != 0 )
				list.Add( 1060408, prop.ToString() ); // defense chance increase ~1_val~%

			if ( (prop = m_AosAttributes.BonusDex) != 0 )
				list.Add( 1060409, prop.ToString() ); // dexterity bonus ~1_val~

			if ( (prop = m_AosAttributes.EnhancePotions) != 0 )
				list.Add( 1060411, prop.ToString() ); // enhance potions ~1_val~%

			if ( (prop = m_AosAttributes.CastRecovery) != 0 )
				list.Add( 1060412, prop.ToString() ); // faster cast recovery ~1_val~

			if ( (prop = m_AosAttributes.CastSpeed) != 0 )
				list.Add( 1060413, prop.ToString() ); // faster casting ~1_val~

			if ( (prop = m_AosAttributes.AttackChance) != 0 )
				list.Add( 1060415, prop.ToString() ); // hit chance increase ~1_val~%

			if ( (prop = m_AosAttributes.BonusHits) != 0 )
				list.Add( 1060431, prop.ToString() ); // hit point increase ~1_val~

			if ( (prop = m_AosAttributes.BonusInt) != 0 )
				list.Add( 1060432, prop.ToString() ); // intelligence bonus ~1_val~

			if ( (prop = m_AosAttributes.LowerManaCost) != 0 )
				list.Add( 1060433, prop.ToString() ); // lower mana cost ~1_val~%

			if ( (prop = m_AosAttributes.LowerRegCost) != 0 )
				list.Add( 1060434, prop.ToString() ); // lower reagent cost ~1_val~%

			if ( (prop = m_AosAttributes.Luck) != 0 )
				list.Add( 1060436, prop.ToString() ); // luck ~1_val~

			if ( (prop = m_AosAttributes.BonusMana) != 0 )
				list.Add( 1060439, prop.ToString() ); // mana increase ~1_val~

			if ( (prop = m_AosAttributes.RegenMana) != 0 )
				list.Add( 1060440, prop.ToString() ); // mana regeneration ~1_val~

			if ( (prop = m_AosAttributes.NightSight) != 0 )
				list.Add( 1060441 ); // night sight

			if ( (prop = m_AosAttributes.ReflectPhysical) != 0 )
				list.Add( 1060442, prop.ToString() ); // reflect physical damage ~1_val~%

			if ( (prop = m_AosAttributes.RegenStam) != 0 )
				list.Add( 1060443, prop.ToString() ); // stamina regeneration ~1_val~

			if ( (prop = m_AosAttributes.RegenHits) != 0 )
				list.Add( 1060444, prop.ToString() ); // hit point regeneration ~1_val~

			if ( (prop = m_AosAttributes.SpellChanneling) != 0 )
				list.Add( 1060482 ); // spell channeling

			if ( (prop = m_AosAttributes.SpellDamage) != 0 )
				list.Add( 1060483, prop.ToString() ); // spell damage increase ~1_val~%

			if ( (prop = m_AosAttributes.BonusStam) != 0 )
				list.Add( 1060484, prop.ToString() ); // stamina increase ~1_val~

			if ( (prop = m_AosAttributes.BonusStr) != 0 )
				list.Add( 1060485, prop.ToString() ); // strength bonus ~1_val~

			if ( (prop = m_AosAttributes.WeaponSpeed) != 0 )
				list.Add( 1060486, prop.ToString() ); // swing speed increase ~1_val~%

			base.AddResistanceProperties( list );

			if ( m_HitPoints >= 0 && m_MaxHitPoints > 0 )
				list.Add( 1060639, "{0}\t{1}", m_HitPoints, m_MaxHitPoints ); // durability ~1_val~ / ~2_val~
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 6 ); // version

			writer.WriteEncodedInt( (int) m_MaxHitPoints );
			writer.WriteEncodedInt( (int) m_HitPoints );

			writer.Write( (Mobile)m_BlessedBy ); // Personal Bless Deed

			#region Mondain's Legacy Sets version 4
			writer.Write( (bool) m_LastEquipped );
			writer.Write( (bool) m_SetEquipped );
			writer.WriteEncodedInt( (int) m_SetHue );

			m_SetAttributes.Serialize( writer );
			m_SetSkillBonuses.Serialize( writer );
			#endregion

			#region Mondain's Legacy version 3
			writer.Write( (Mobile) m_Crafter );
			writer.Write( (int) m_Quality );
			#endregion

			writer.WriteEncodedInt( (int) m_Resource );
			writer.WriteEncodedInt( (int) m_GemType );

			m_AosAttributes.Serialize( writer );
			m_AosResistances.Serialize( writer );
			m_AosSkillBonuses.Serialize( writer );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			switch ( version )
			{
				case 6:
				{
					m_MaxHitPoints = reader.ReadEncodedInt();
					m_HitPoints = reader.ReadEncodedInt();

					goto case 5;
				}
				//personal bless deed
				case 5:
				{
					m_BlessedBy = reader.ReadMobile();
					goto case 4;
				}
				#region Mondain's Legacy Sets
				case 4:
				{
					m_LastEquipped = reader.ReadBool();
					m_SetEquipped = reader.ReadBool();
					m_SetHue = reader.ReadEncodedInt();

					m_SetAttributes = new AosAttributes( this, reader );
					m_SetSkillBonuses = new AosSkillBonuses( this, reader );

					goto case 3;
				}
				#endregion
				#region Mondain's Legacy
				case 3:
				{
					m_Crafter = reader.ReadMobile();
					m_Quality = (ArmorQuality) reader.ReadInt();

					goto case 2;
				}
				#endregion
				case 2:
				{
					m_Resource = (CraftResource)reader.ReadEncodedInt();
					m_GemType = (GemType)reader.ReadEncodedInt();

					goto case 1;
				}
				case 1:
				{
					m_AosAttributes = new AosAttributes( this, reader );
					m_AosResistances = new AosElementAttributes( this, reader );
					m_AosSkillBonuses = new AosSkillBonuses( this, reader );

					if ( Core.AOS && Parent is Mobile )
						m_AosSkillBonuses.AddTo( (Mobile)Parent );

					int strBonus = m_AosAttributes.BonusStr;
					int dexBonus = m_AosAttributes.BonusDex;
					int intBonus = m_AosAttributes.BonusInt;

					if ( Parent is Mobile && (strBonus != 0 || dexBonus != 0 || intBonus != 0) )
					{
						Mobile m = (Mobile)Parent;

						string modName = Serial.ToString();

						if ( strBonus != 0 )
							m.AddStatMod( new StatMod( StatType.Str, modName + "Str", strBonus, TimeSpan.Zero ) );

						if ( dexBonus != 0 )
							m.AddStatMod( new StatMod( StatType.Dex, modName + "Dex", dexBonus, TimeSpan.Zero ) );

						if ( intBonus != 0 )
							m.AddStatMod( new StatMod( StatType.Int, modName + "Int", intBonus, TimeSpan.Zero ) );
					}

					if ( Parent is Mobile )
						((Mobile)Parent).CheckStatTimers();

					break;
				}
				case 0:
				{
					m_AosAttributes = new AosAttributes( this );
					m_AosResistances = new AosElementAttributes( this );
					m_AosSkillBonuses = new AosSkillBonuses( this );

					break;
				}
			}

			#region Mondain's Legacy Sets
			if ( m_SetAttributes == null )
				m_SetAttributes = new AosAttributes( this );

			if ( m_SetSkillBonuses == null )
				m_SetSkillBonuses = new AosSkillBonuses( this );
			#endregion

			if ( version < 2 )
			{
				m_Resource = CraftResource.Iron;
				m_GemType = GemType.None;
			}
		}
		#region ICraftable Members

		public int OnCraft( int quality, bool makersMark, Mobile from, CraftSystem craftSystem, Type typeRes, BaseTool tool, CraftItem craftItem, int resHue )
		{
			Type resourceType = typeRes;

			if ( resourceType == null )
				resourceType = craftItem.Resources.GetAt( 0 ).ItemType;

			#region Mondain's Legacy
			if ( !craftItem.ForceNonExceptional )
				Resource = CraftResources.GetFromType( resourceType );
			#endregion

			CraftContext context = craftSystem.GetContext( from );

			if ( context != null && context.DoNotColor )
				Hue = 0;

			if ( 1 < craftItem.Resources.Count )
			{
				resourceType = craftItem.Resources.GetAt( 1 ).ItemType;

				if ( resourceType == typeof( StarSapphire ) )
					GemType = GemType.StarSapphire;
				else if ( resourceType == typeof( Emerald ) )
					GemType = GemType.Emerald;
				else if ( resourceType == typeof( Sapphire ) )
					GemType = GemType.Sapphire;
				else if ( resourceType == typeof( Ruby ) )
					GemType = GemType.Ruby;
				else if ( resourceType == typeof( Citrine ) )
					GemType = GemType.Citrine;
				else if ( resourceType == typeof( Amethyst ) )
					GemType = GemType.Amethyst;
				else if ( resourceType == typeof( Tourmaline ) )
					GemType = GemType.Tourmaline;
				else if ( resourceType == typeof( Amber ) )
					GemType = GemType.Amber;
				else if ( resourceType == typeof( Diamond ) )
					GemType = GemType.Diamond;
			}

			#region Mondain's Legacy
			m_Quality = (ArmorQuality) quality;

			if ( makersMark )
				m_Crafter = from;
			#endregion

			return quality;
		}

		#endregion

		#region Mondain's Legacy Sets
		public override bool OnDragLift( Mobile from )
		{
			if ( Parent is Mobile && from == Parent )
			{
				if ( IsSetItem && m_SetEquipped )
					SetHelper.RemoveSetBonus( from, SetID, this );
			}

			return base.OnDragLift( from );
		}

		public virtual SetItem SetID{ get{ return SetItem.None; } }
		public virtual int Pieces{ get{ return 0; } }
		public virtual bool MixedSet{ get{ return false; } }

		public bool IsSetItem{ get{ return SetID == SetItem.None ? false : true; } }

		private int m_SetHue;
		private bool m_SetEquipped;
		private bool m_LastEquipped;

		[CommandProperty( AccessLevel.GameMaster )]
		public int SetHue
		{
			get{ return m_SetHue; }
			set{ m_SetHue = value; InvalidateProperties(); }
		}

		public bool SetEquipped
		{
			get{ return m_SetEquipped; }
			set{ m_SetEquipped = value; }
		}

		public bool LastEquipped
		{
			get{ return m_LastEquipped; }
			set{ m_LastEquipped = value; }
		}

		private AosAttributes m_SetAttributes;
		private AosSkillBonuses m_SetSkillBonuses;

		[CommandProperty( AccessLevel.GameMaster )]
		public AosAttributes SetAttributes
		{
			get{ return m_SetAttributes; }
			set{}
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public AosSkillBonuses SetSkillBonuses
		{
			get{ return m_SetSkillBonuses; }
			set{}
		}
		#endregion
	}
}