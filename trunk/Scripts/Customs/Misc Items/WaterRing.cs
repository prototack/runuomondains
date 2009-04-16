using System;
using Server;

namespace Server.Items
{
    public class WaterRing : GoldRing
    {

        public override int ArtifactRarity { get { return 11; } }

        [Constructable]
        public WaterRing()
        {
            Hue = 48;
            Name = "WaterRing";
        }

        public WaterRing(Serial serial)
            : base(serial)
        {
        }
        // Just uncomment it if you want it active.
        /*public override void OnAdded(object parent)
        {
            base.OnAdded(parent);

            if (parent is Mobile)
            {
                ((Mobile)parent).CanSwim = true;
            }
        }*/

        /*I left the below as is just incase anyone was wearing it before change took effect.  
        Once they take it off it will no longer allow them to swim with mounts.*/
        public override void OnRemoved(object parent)
        {
            base.OnRemoved(parent);

            if (parent is Mobile)
            {
                ((Mobile)parent).CanSwim = false;
            }
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }
}