using System;
using Server;
using Server.Gumps;
using Server.Network;
using System.IO;
using Server.Commands;
using Server.Mobiles;


namespace Server.Gumps
{
	public class MotdGump : Gump
	{

        private const string StaffMotdPath = @"Data/staffmotd.txt";
        private const string MotdPath = @"Data/motd.txt";

        public static void Initialize()
        {
            CommandSystem.Register("Motd", AccessLevel.Player, new CommandEventHandler(Motd_OnCommand));
            EventSink.Login += new LoginEventHandler(Motd_OnConnected);
        }

		public MotdGump(AccessLevel Access, Boolean OverrideAccess)
			: base( 0, 0 )
		{
			this.Closable=true;
			this.Disposable=true;
			this.Dragable=true;
			this.Resizable=false;
			this.AddPage(0);
			this.AddBackground(0, 0, 375, 225, 9200);
            this.AddLabel(9, 13, 0, @"Welcome to TechUO!");
            this.AddButton(140, 10, (int)0x2A58, (int)0X2A44, (int)Buttons.Rules, GumpButtonType.Reply, 0); //Rules
            this.AddLabel(180, 14, 2049, @"Rules");
            this.AddButton(260, 10, (int)0x2A30, (int)0X2A44, (int)Buttons.Changelog, GumpButtonType.Reply, 0); //Chanelog
            this.AddLabel(290, 14, 2049, @"Changelog");
			this.AddCheck(12, 202, 210, 211, false, 0);
			this.AddLabel(37, 202, 0, @"Don't show me this again.");
			this.AddButton(300, 195, 247, 248, (int)Buttons.Close, GumpButtonType.Reply, 0); //Close
            this.AddHtml(10, 40, 355, 146, GetMotdMessage(Access, OverrideAccess), (bool)true, (bool)true);
		}
		
		public enum Buttons
		{
            ShowAgain,
			Rules,
			Changelog,
			Close,
		}

        public string GetMotdMessage(AccessLevel Access, Boolean OverrideStaff)
        {
            if (Access > AccessLevel.Player && !OverrideStaff)
            {
                try
                {
                    return File.ReadAllText(StaffMotdPath);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(string.Concat("Error retrieving Staff Motd : ", ex.Message));
                }
            }
            else
            {
                try
                {
                    return File.ReadAllText(MotdPath);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(string.Concat("Error retrieving Player Motd : ", ex.Message));
                }
            }
           
              

            return @"";
        }

        public override void OnResponse(NetState state, RelayInfo info)
        {
            Mobile from = state.Mobile;
           
            switch (info.ButtonID)
            {
                case (int)Buttons.Rules:
                    {
                        from.SendMessage("Rules was pressed, the gump for this will be implimented later!");
                        break;
                    }
                case (int)Buttons.Changelog:
                    {
                        from.SendMessage("Visit: http://github.com/TechUO/RunUO to view the in depth change log.");
                        break;
                    }
                case (int)Buttons.Close:
                    {
                        break;
                    }
            }
            for( int i = 0; i < info.Switches.Length; i++ ){
                switch (info.Switches[i])
                {
                    case 0:
                        {
                            from.SendMessage("Toggling.");
                            ToggleShowAgain(from);
                            break;
                        }
                }
            }
            
            
        }

        public static void Motd_OnCommand(CommandEventArgs e)
        {
            if (e.GetString(0) == "player")
            {
                e.Mobile.SendMessage(e.GetString(0));
                e.Mobile.SendMessage(e.GetString(0).Contains("player").ToString());
                e.Mobile.SendMessage("YES");
                e.Mobile.SendGump(new MotdGump(e.Mobile.AccessLevel, true));
            }
            else
            {
                e.Mobile.SendMessage(e.GetString(0));
                e.Mobile.SendMessage(e.GetString(0).Contains("player").ToString());
                e.Mobile.SendMessage("NO");
                e.Mobile.SendGump(new MotdGump(e.Mobile.AccessLevel, false));
            }
        }

        public static void Motd_OnConnected(LoginEventArgs e)
        {
            if (e.Mobile.ShowMotd)
            e.Mobile.SendGump(new MotdGump(e.Mobile.AccessLevel, false));
        }

        private static void ToggleShowAgain(Mobile from)
        {
            if (from.ShowMotd)
            {
                from.SendMessage("False.");
                from.ShowMotd = false;
            }
            else
            {
                from.SendMessage("True.");
                from.ShowMotd = true;
            }
            from.SendMessage(string.Concat("Toggled to:", from.ShowMotd));
        }

	}
}
