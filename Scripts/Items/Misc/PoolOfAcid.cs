using System;
using Server;
using Server.Mobiles;
using Server.Spells;
using System.Collections;
using System.Collections.Generic;

namespace Server.Items
{
	public class PoolOfAcid : Item
	{
		private TimeSpan m_Duration;
		private int m_MinDamage;
		private int m_MaxDamage;

        private bool m_AOSDmg;
		private int m_dmg_fire;
		private int m_dmg_phys;
		private int m_dmg_cold;
		private int m_dmg_pois;
		private int m_dmg_nrgy;
		private DateTime m_Created;

		private bool m_Drying;

		private Timer m_Timer;

		[CommandProperty( AccessLevel.GameMaster )]
		public bool Drying
		{
			get
			{
				return m_Drying;
			}
			set
			{
				m_Drying = value;

 				if( m_Drying )
					ItemID = 0x122B;
				else
 					ItemID = 0x122A;
			}
		}


		[CommandProperty( AccessLevel.GameMaster )]
		public TimeSpan Duration{ get{ return m_Duration; } set{ m_Duration = value; } }

		[CommandProperty( AccessLevel.GameMaster )]
		public int MinDamage
		{
			get
			{
				return m_MinDamage;
			}
			set
			{
				if ( value < 1 )
					value = 1;

				m_MinDamage = value;
			}
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public int MaxDamage
		{
			get
			{
				return m_MaxDamage;
			}
			set
			{
				if ( value < 1 )
					value = 1;

				if ( value < MinDamage )
					value = MinDamage;

				m_MaxDamage = value;
			}
 		}
 
		[CommandProperty( AccessLevel.GameMaster )]
		public int dmg_phys
		{
			get
			{
				return m_dmg_phys;
			}
			set
			{
				if ( value > 100 )
					value = 100;
				m_dmg_phys= value;
			}
		}
		[CommandProperty( AccessLevel.GameMaster )]
		public int dmg_fire
		{
			get
			{
				return m_dmg_fire;
			}
			set
			{
				if ( value > 100 )
					value = 100;
				m_dmg_fire= value;
			}
		}
		[CommandProperty( AccessLevel.GameMaster )]
		public int dmg_cold
		{
			get
			{
				return m_dmg_cold;
			}
			set
			{
				if ( value > 100 )
					value = 100;
				m_dmg_cold = value;
			}
		}
		[CommandProperty( AccessLevel.GameMaster )]
		public int dmg_pois
		{
			get
			{
				return m_dmg_pois;
			}
			set
			{
				if ( value > 100 )
					value = 100;
				m_dmg_pois= value;
			}
		}
		[CommandProperty( AccessLevel.GameMaster )]
		public int dmg_nrgy
		{
			get
			{
				return m_dmg_nrgy;
			}
			set
			{
				if ( value > 100 )
					value = 100;
				m_dmg_nrgy= value;
			}
		}
		[CommandProperty( AccessLevel.GameMaster )]
		public bool AOSDmg // AOS Dmg is elemental/resistable 
		{
			get
			{
				return m_AOSDmg;
			}
			set
			{
				m_AOSDmg= value;
			}
		}

		[Constructable]
		public PoolOfAcid() : this( TimeSpan.FromSeconds( 10.0 ), 2, 5 )
		{
		}

        [Constructable]
		public PoolOfAcid( TimeSpan duration, int minDamage, int maxDamage ) : this( duration, minDamage, maxDamage, 0, false )
		{
        }

		public override string DefaultName { get { return "a pool of acid"; } }

        /* damage type inputs for AOS damage: 			*/
		/* 0 = phys, 1 = fire, 2 = cold, 3 = poison, 4 = energy */

		[Constructable]
        public PoolOfAcid(TimeSpan duration, int minDamage, int maxDamage, int DmgType, bool AOSDmg)
		{
            Drying = false;
			Hue = 0x3F;
			Movable = false;

			m_MinDamage = minDamage;
			m_MaxDamage = maxDamage;
			m_Created = DateTime.Now;
			m_Duration = duration;

			m_Timer = Timer.DelayCall( TimeSpan.Zero, TimeSpan.FromSeconds( 1 ), new TimerCallback( OnTick ) );
            m_AOSDmg = AOSDmg;
			if( m_AOSDmg )
			{
				if ( DmgType == 0 )
					m_dmg_phys = 100;
				else if ( DmgType == 1 )
					m_dmg_fire = 100;
				else if ( DmgType == 2 )
					m_dmg_cold = 100;
				else if ( DmgType == 3 )
					m_dmg_pois = 100;
				else if ( DmgType == 4 )
					m_dmg_nrgy = 100;
				else 
					m_AOSDmg = false;
			}
		}

		public override void OnAfterDelete()
		{
			if( m_Timer != null )
				m_Timer.Stop();
		}

		private void OnTick()
		{
			DateTime now = DateTime.Now;
			TimeSpan age = now - m_Created;

			if( age > m_Duration )
				Delete();
			else
			{
				if( !Drying && age > (m_Duration - age) )
					Drying = true;

				List<Mobile> toDamage = new List<Mobile>();

				foreach( Mobile m in GetMobilesInRange( 0 ) )
				{
					BaseCreature bc = m as BaseCreature;

					if( m.Alive && !m.IsDeadBondedPet && (bc == null || bc.Controlled || bc.Summoned) )
					{
						toDamage.Add( m );
					}
				}

				for( int i = 0; i < toDamage.Count; i++ )
					Damage( toDamage[i] );
			}
		}


		public override bool OnMoveOver( Mobile m )
		{
			Damage( m );
			return true;
		}

		public void Damage( Mobile m )
		{
			if ( Core.AOS && m_AOSDmg )
			{
				AOS.Damage( m, Utility.RandomMinMax( MinDamage, MaxDamage ), m_dmg_phys, m_dmg_fire, m_dmg_cold, m_dmg_pois, m_dmg_nrgy );
			}
			else
			{
				m.Damage( Utility.RandomMinMax( MinDamage, MaxDamage ) );
			}
		}

		public PoolOfAcid( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			//Don't serialize these
		}

		public override void Deserialize( GenericReader reader )
		{
		}
	}
}
