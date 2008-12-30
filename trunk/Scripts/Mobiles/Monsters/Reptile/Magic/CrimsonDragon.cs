using System;
using Server;
using Server.Items;
using System.Collections;


namespace Server.Mobiles
{
	[CorpseName( "a dragon corpse" )]
	public class CrimsonDragon : BasePeerless
	{
		[Constructable]
		public CrimsonDragon () : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "a crimson dragon";
			Body = 197;
			BaseSoundID = 362;

			SetStr( 2034, 2133 );
			SetDex( 256, 256 );
			SetInt( 1067, 1116 );

			SetHits( 2658, 2711 );

			SetDamage( 29, 35 );

			SetDamageType( ResistanceType.Physical, 50 );
			SetDamageType( ResistanceType.Fire, 50 );

			SetResistance( ResistanceType.Physical, 80, 85 );
			SetResistance( ResistanceType.Fire, 100, 100 );
			SetResistance( ResistanceType.Cold, 50, 60 );
			SetResistance( ResistanceType.Poison, 80, 85 );
			SetResistance( ResistanceType.Energy, 80, 85 );

			SetSkill( SkillName.EvalInt, 111.1, 115.0 );
			SetSkill( SkillName.Magery, 120.1, 125.0 );
			SetSkill( SkillName.Meditation, 119.5, 125.0 );
			SetSkill( SkillName.MagicResist, 116.5, 120.0 );
			SetSkill( SkillName.Tactics, 126.6, 130.0 );
			SetSkill( SkillName.Wrestling, 130.0, 135.0 );

			// ingredients
  		    PackResources( 8 );


			Fame = 22500;
			Karma = -22500;

			VirtualArmor = 70;
		}

		public override void GenerateLoot()
		{
            AddLoot( LootPack.AosSuperBoss, 8 );
			AddLoot( LootPack.Gems, 5 );
		}

		public override int GetIdleSound()
		{
			return 0x2D3;
		}

		public override int GetHurtSound()
		{
			return 0x2D1;
		}

		public override bool ReacquireOnMovement{ get{ return true; } }
        public override bool BardImmune { get { return true; } }
        public override bool Unprovokable { get { return true; } }
        public override bool Uncalmable { get { return true; } }
		public override bool HasBreath{ get{ return true; } } // fire breath enabled
		public override bool AutoDispel{ get{ return true; } }
		public override HideType HideType{ get{ return HideType.Barbed; } }
		public override int Hides{ get{ return 40; } }
		public override int Meat{ get{ return 19; } }
		public override int Scales{ get{ return 12; } }
		public override ScaleType ScaleType{ get{ return (ScaleType)Utility.Random( 4 ); } }
		public override Poison PoisonImmune{ get{ return Poison.Lethal; } }
		public override Poison HitPoison{ get{ return Utility.RandomBool() ? Poison.Deadly : Poison.Lethal; } } 
		public override int TreasureMapLevel{ get{ return 5; } }
		public override bool GivesMinorArtifact{ get{ return true; } }


        public override void OnDamagedBySpell(Mobile caster)
        {
            if (this.Map != null && caster != this && 0.50 > Utility.RandomDouble())
            {
                Map = caster.Map;   
                Location = caster.Location;
                Combatant = caster;
            	Effects.PlaySound( this.Location, this.Map, 0x1FE );
            }

            base.OnDamagedBySpell(caster);
        }


        public override void OnGotMeleeAttack(Mobile attacker)
        {
            if (this.Map != null && attacker != this && 50 > Utility.RandomDouble())
            {
                if (attacker is BaseCreature)
                {
                    BaseCreature pet = (BaseCreature)attacker;
                    if (pet.ControlMaster != null && (attacker is Dragon || attacker is GreaterDragon || attacker is SkeletalDragon || attacker is WhiteWyrm || attacker is Drake) )
                    {
                        Combatant = null;
                        pet.Combatant = null;
                        Combatant = null;
                        pet.ControlMaster = null;
                        pet.Controlled = false;
                        attacker.Emote(String.Format("* {0} decided to go wild *", attacker.Name));
                    }
              
                   if (pet.ControlMaster != null && 50 > Utility.RandomDouble())
                   {
                        Combatant = null;
                        pet.Combatant = pet.ControlMaster; 
                        Combatant = null;
                        attacker.Emote(String.Format("* {0} is being angered *", attacker.Name));
                   }

                }
            } 

            base.OnGotMeleeAttack(attacker);
        }

		public override bool OnBeforeDeath()
        {
  			 return base.OnBeforeDeath();
        }
    
		public CrimsonDragon( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
}