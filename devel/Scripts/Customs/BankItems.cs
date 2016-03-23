using System;
using System.Collections.Generic;
using System.Text;
using Server;
using Server.Items;

namespace Customs
{
    class BankItems
    {
        public static void Initialize()
        {
            EventSink.CharacterCreated += new CharacterCreatedEventHandler(NewPlayerItems);
        }
        
        public static void NewPlayerItems(CharacterCreatedEventArgs e)
        {
            Mobile m_mobile = e.Mobile;
            m_mobile.BankBox.DropItem(new BankCheck(5000));
            m_mobile.BankBox.DropItem(new BagOfAllReagents(10));
        }

    }
}
