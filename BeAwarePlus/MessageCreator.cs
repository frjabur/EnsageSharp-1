using Ensage;
using Ensage.Common;
using SharpDX;

namespace BeAwarePlus
{
    class MessageCreator
    {
        private static SideMessage informationmessage;
        internal static float msgX;
        internal static float msgY;
        internal static float sizeheroX;
        internal static float sizeheroYspell;
        internal static float sizeitemX;
        internal static float herospellY;
        internal static float heroallyX;
        internal static float heroX;
        internal static float spellX;
        internal static float itemX;
        
        internal static void MessageAllyCreator(string hero, string spell)
        {
            if (MenuManager.Menu.Item("enable_msg").GetValue<bool>())
            {
                informationmessage = new SideMessage(hero + spell, new Vector2(msgX, msgY), stayTime: 5000);
                informationmessage.AddElement(new Vector2(0, 0), new Vector2(msgX, msgY), Drawing.GetTexture("ensage_ui/other/msg0_" + BeAwarePlus.Addition[BeAwarePlus.GetLangId]));
                informationmessage.AddElement(new Vector2(heroallyX, herospellY), new Vector2(sizeheroX, sizeheroYspell), Drawing.GetTexture("ensage_ui/heroes_horizontal/" + hero));
                informationmessage.AddElement(new Vector2(heroX, herospellY), new Vector2(sizeheroYspell, sizeheroYspell), Drawing.GetTexture("ensage_ui/spellicons/" + spell));
                informationmessage.CreateMessage();
            }
        }
        internal static void MessageEnemyCreator(string hero, string spell)
        {
            if (MenuManager.Menu.Item("enable_msg").GetValue<bool>())
            {
                informationmessage = new SideMessage(hero + spell, new Vector2(msgX, msgY), stayTime: 5000);
                informationmessage.AddElement(new Vector2(0, 0), new Vector2(msgX, msgY), Drawing.GetTexture("ensage_ui/other/msg1_" + BeAwarePlus.Addition[BeAwarePlus.GetLangId]));
                informationmessage.AddElement(new Vector2(heroX, herospellY), new Vector2(sizeheroX, sizeheroYspell), Drawing.GetTexture("ensage_ui/heroes_horizontal/" + hero));
                informationmessage.AddElement(new Vector2(spellX, herospellY), new Vector2(sizeheroYspell, sizeheroYspell), Drawing.GetTexture("ensage_ui/spellicons/" + spell));
                informationmessage.CreateMessage();
            }
        }
        internal static void MessageRuneCreator(string hero, string rune)
        {
            if (MenuManager.Menu.Item("enable_msg").GetValue<bool>())
            {
                informationmessage = new SideMessage(hero + rune, new Vector2(msgX, msgY), stayTime: 5000);
                informationmessage.AddElement(new Vector2(0, 0), new Vector2(msgX, msgY), Drawing.GetTexture("ensage_ui/other/msg2_" + BeAwarePlus.Addition[BeAwarePlus.GetLangId]));
                informationmessage.AddElement(new Vector2(heroX, herospellY), new Vector2(sizeheroX, sizeheroYspell), Drawing.GetTexture("ensage_ui/heroes_horizontal/" + hero));
                informationmessage.AddElement(new Vector2(spellX, herospellY), new Vector2(sizeheroYspell, sizeheroYspell), Drawing.GetTexture("ensage_ui/modifier_textures/" + rune));
                informationmessage.CreateMessage();
            }
        }
        internal static void MessageItemCreator(string hero, string item)
        {
            if (MenuManager.Menu.Item("enable_msg").GetValue<bool>())
            {
                informationmessage = new SideMessage(hero + item, new Vector2(msgX, msgY), stayTime: 5000);
                informationmessage.AddElement(new Vector2(0, 0), new Vector2(msgX, msgY), Drawing.GetTexture("ensage_ui/other/msg3_" + BeAwarePlus.Addition[BeAwarePlus.GetLangId]));
                informationmessage.AddElement(new Vector2(heroX, herospellY), new Vector2(sizeheroX, sizeheroYspell), Drawing.GetTexture("ensage_ui/heroes_horizontal/" + hero));
                informationmessage.AddElement(new Vector2(itemX, herospellY), new Vector2(sizeitemX, sizeheroYspell), Drawing.GetTexture("ensage_ui/items/" + item));
                informationmessage.CreateMessage();
            }
        }
        internal static void MessageCheckRuneCreator(string check_rune)
        {
            if (MenuManager.Menu.Item("enable_msg").GetValue<bool>())
            {
                informationmessage = new SideMessage("check_rune", new Vector2(msgX, msgY), stayTime: 5000);
                informationmessage.AddElement(new Vector2(0, 0), new Vector2(msgX, msgY), Drawing.GetTexture("ensage_ui/other/msg4_" + BeAwarePlus.Addition[BeAwarePlus.GetLangId]));
                informationmessage.CreateMessage();
            }
        }
        internal static void MessageUseMidasCreator(string use_midas)
        {
            if (MenuManager.Menu.Item("enable_msg").GetValue<bool>())
            {
                informationmessage = new SideMessage("use_midas", new Vector2(msgX, msgY), stayTime: 5000);
                informationmessage.AddElement(new Vector2(0, 0), new Vector2(msgX, msgY), Drawing.GetTexture("ensage_ui/other/msg5_" + BeAwarePlus.Addition[BeAwarePlus.GetLangId]));
                informationmessage.CreateMessage();
            }
        }
        internal static void MessageRoshanAliveCreator(string roshan_alive)
        {
            if (MenuManager.Menu.Item("enable_msg").GetValue<bool>())
            {
                informationmessage = new SideMessage("rosha_nalive", new Vector2(msgX, msgY), stayTime: 5000);
                informationmessage.AddElement(new Vector2(0, 0), new Vector2(msgX, msgY), Drawing.GetTexture("ensage_ui/other/msg6_" + BeAwarePlus.Addition[BeAwarePlus.GetLangId]));
                informationmessage.CreateMessage();
            }
        }
        internal static void MessageRoshanMBAliveCreator(string roshan_mb_alive)
        {
            if (MenuManager.Menu.Item("enable_msg").GetValue<bool>())
            {
                informationmessage = new SideMessage("roshan_mb_alive", new Vector2(msgX, msgY), stayTime: 5000);
                informationmessage.AddElement(new Vector2(0, 0), new Vector2(msgX, msgY), Drawing.GetTexture("ensage_ui/other/msg7_" + BeAwarePlus.Addition[BeAwarePlus.GetLangId]));
                informationmessage.CreateMessage();
            }
        }
    }
}
