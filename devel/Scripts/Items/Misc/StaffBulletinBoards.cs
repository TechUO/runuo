using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Server;
using Server.Network;

namespace Server.Items
{
	[Flipable( 0x1E5E, 0x1E5F )]
	public class StaffBulletinBoard : BaseBulletinBoard
	{
		[Constructable]
		public StaffBulletinBoard() : base( 0x1E5E )
		{
            this.Movable = false;
            this.BoardName = "Staff Board";
            this.Hue = 2214;
        }

		public StaffBulletinBoard( Serial serial ) : base( serial )
		{
		}

        public override void PostMessage(Mobile from, BulletinMessage thread, string subject, string[] lines)
        {
            if (from.AccessLevel >= AccessLevel.GameMaster)
                from.SendMessage("You have acces");
            else
                from.SendMessage("You do not have access");

            if (thread != null)
               thread.LastPostTime = DateTime.Now;

            AddItem(new BulletinMessage(from, thread, subject, lines));
                
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