
using Ensage.Common.Menu;

namespace LoneDruidSharpRewrite.Utilities
{
    public class MenuManager
    {
        public readonly MenuItem OnlyBearLastHitMenu;

        public readonly MenuItem CombinedLastHitMenu;

        public readonly MenuItem AutoTalonMenu;

        public readonly MenuItem AutoMidasMenu;

        public readonly MenuItem BearChaseMenu;

        public Menu Menu { get; private set; }

        public MenuManager(string heroName)
        {
            Menu = new Menu("LoneDruidSharp", "LoneDruidSharp", true, "npc_dota_hero_lone_druid", true);
            OnlyBearLastHitMenu = new MenuItem("OnlyBearLastHitMenu", "OnlyBearLastHitMenu").SetValue(new KeyBind('W', KeyBindType.Press)).SetTooltip("only bear will go last hit");
            CombinedLastHitMenu = new MenuItem("CombinedLastHitMenu", "CombinedLastHitMenu").SetValue(new KeyBind('X', KeyBindType.Press)).SetTooltip("both hero and bear last hit");
            AutoTalonMenu = new MenuItem("Auto Iron Talon", "Auto Iron Talon").SetValue(new KeyBind('Z', KeyBindType.Toggle));
            AutoMidasMenu = new MenuItem("Auto Midas", "Auto Midas").SetValue(new KeyBind('Z', KeyBindType.Toggle));
            BearChaseMenu = new MenuItem("BearChaseMenu", "BearChaseMenu").SetValue(new KeyBind('D', KeyBindType.Press)).SetTooltip("press it and rightclick enemy, bear will keep chasing until you control bear again");
            Menu.AddItem(AutoMidasMenu);
            Menu.AddItem(OnlyBearLastHitMenu);
            Menu.AddItem(CombinedLastHitMenu);
            Menu.AddItem(AutoTalonMenu);
            Menu.AddItem(BearChaseMenu);
        }

        public bool AutoTalonActive
        {
            get
            {
                return AutoTalonMenu.GetValue<KeyBind>().Active;
            }
        }

        public bool OnlyBearLastHitModeOn
        {
            get
            {
                return OnlyBearLastHitMenu.GetValue<KeyBind>().Active;
            }
        }

        public bool CombineLastHitModeOn
        {
            get
            {
                return CombinedLastHitMenu.GetValue<KeyBind>().Active;
            }
        }

        public bool AutoMidasModeOn
        {
            get
            {
                return AutoMidasMenu.GetValue<KeyBind>().Active; ;
            }
        }

        public bool BearChaseModeOn
        {
            get
            {
                return BearChaseMenu.GetValue<KeyBind>().Active;
            }
        }




    }
}
