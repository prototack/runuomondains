using System;
using Server;
using Server.Items;

namespace Server.Mobiles
{
    [CorpseName("a tangle corpse")]
    public class Tangle : BogThing
    {
        [Constructable]
        public Tangle()
            : base()
        {
            Name = "a tangle";
            Hue = 0x21;

            SetStr(843, 943);
            SetDex(58, 74);
            SetInt(46, 58);

            SetHits(2468, 2733);

            SetDamage(15, 28);

            SetDamageType(ResistanceType.Physical, 60);
            SetDamageType(ResistanceType.Poison, 40);

            SetResistance(ResistanceType.Physical, 50, 57);
            SetResistance(ResistanceType.Fire, 40, 43);
            SetResistance(ResistanceType.Cold, 30, 35);
            SetResistance(ResistanceType.Poison, 61, 69);
            SetResistance(ResistanceType.Energy, 41, 45);

            SetSkill(SkillName.Wrestling, 80.8, 94.6);
            SetSkill(SkillName.Tactics, 90.6, 100.4);
            SetSkill(SkillName.MagicResist, 108.4, 114.0);

            Fame = 16000;
            Karma = -16000;

            VirtualArmor = 54;
        }

        public override void GenerateLoot()
        {
            AddLoot(LootPack.AosUltraRich, 3);
        }

        public override void OnDeath(Container c)
        {
            base.OnDeath(c);

            if (Utility.RandomDouble() < 0.3)
                c.DropItem(new TaintedSeeds());
        }

        public override bool BardImmune { get { return !Core.AOS; } }
        public override Poison PoisonImmune { get { return Poison.Lethal; } }

        public Tangle(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }

        public void SpawnBogling(Mobile m)
        {
            Map map = this.Map;

            if (map == null)
                return;

            Bogling spawned = new Bogling();

            spawned.Team = this.Team;

            bool validLocation = false;
            Point3D loc = this.Location;

            for (int j = 0; !validLocation && j < 10; ++j)
            {
                int x = X + Utility.Random(3) - 1;
                int y = Y + Utility.Random(3) - 1;
                int z = map.GetAverageZ(x, y);

                if (validLocation = map.CanFit(x, y, this.Z, 16, false, false))
                    loc = new Point3D(x, y, Z);
                else if (validLocation = map.CanFit(x, y, z, 16, false, false))
                    loc = new Point3D(x, y, z);
            }

            spawned.MoveToWorld(loc, map);
            spawned.Combatant = m;
        }

        public void EatBoglings()
        {
            ArrayList toEat = new ArrayList();

            foreach (Mobile m in this.GetMobilesInRange(2))
            {
                if (m is Bogling)
                    toEat.Add(m);
            }

            if (toEat.Count > 0)
            {
                PlaySound(Utility.Random(0x3B, 2)); // Eat sound

                foreach (Mobile m in toEat)
                {
                    Hits += (m.Hits / 2);
                    m.Delete();
                }
            }
        }

        public override void OnGotMeleeAttack(Mobile attacker)
        {
            base.OnGotMeleeAttack(attacker);

            if (this.Hits > (this.HitsMax / 4))
            {
                if (0.25 >= Utility.RandomDouble())
                    SpawnBogling(attacker);
            }
            else if (0.25 >= Utility.RandomDouble())
            {
                EatBoglings();
            }
        }
    }
}
