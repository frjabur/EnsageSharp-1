using System;
using System.Linq;
using Ensage;
using Ensage.Common;
using SharpDX;
using Ensage.Common.Menu;

namespace BeAwarePlus
{
    class BeAwarePlus
    {
        private static Hero me;
        private static System.Collections.Generic.List<Ensage.Hero> enemies;
        private static SideMessage informationmessage;
        private static readonly Menu Menu = new Menu("BeAwarePlus", "BeAwarePlus", true);
        private static void Main(string[] args)
        {
            Menu.AddItem(new MenuItem("enable", "Sound").SetValue(false));
            Menu.AddToMainMenu();

            Game.OnUpdate += Tick;
            PrintSuccess(">>>>>> BeAwarePlus Loaded!");
            var sList = new StringList()
            {
                SList = LangName,
                SelectedIndex = 0
            };
            var language = new MenuItem("lang", "Language").SetValue(sList);
            language.ValueChanged += Item_ValueChanged;
            Menu.AddItem(language);
        }
        public static int GetLangId
        {
            get { return Menu.Item("lang").GetValue<StringList>().SelectedIndex; }
            set { throw new NotImplementedException(); }
        }

        private static readonly string[] Addition =
        {
        "en.wav",
        "ru.wav"
        };
        private static readonly string[] LangName =
        {
        "EN",
        "RU"
        };
        // Ses çalmak için metod bu.
        // PlaySound("sesdosyasi.wav);
        // şeklinde kullanılıyor
        private static void PlaySound(string path)
        {
            if (!Menu.Item("enable").GetValue<bool>()) return;
            var player =
                new System.Media.SoundPlayer();
            var fullpath = Environment.CurrentDirectory;
            fullpath = fullpath.Remove(fullpath.Length - 10);
            fullpath += @"\dota\materials\ensage_ui\sounds\" + path;
            player.SoundLocation = fullpath;
            player.Load();
            player.Play();
        }

        public static void Tick(EventArgs args)
        {
            if (!Game.IsInGame)
                return;
            me = ObjectManager.LocalHero;
            if (me == null)
                return;
            enemies = ObjectManager.GetEntitiesParallel<Hero>().Where(x => me.Team != x.Team && x.IsVisible && !x.IsIllusion && x.IsAlive).ToList();
            if (enemies == null)
                return;
            foreach (var v in enemies)
            {
                string index;
                System.Collections.Generic.IEnumerable<Modifier> mod = v.Modifiers.ToList();
                if (mod.Any(x => x.Name == "modifier_mirana_moonlight_shadow") && Utils.SleepCheck("mirana")) //mirana
                {
                    MessageCreator("mirana", "mirana_invis");
                    // Burada çalınacak dosyayı seçiyoruz
                    // Sleep'in sonrada olması önemli
                    PlaySound("moonlight_shadow_" + Addition[GetLangId]);
                    Utils.Sleep(15000,"mirana");
                }
                if (mod.Any(x => x.Name == "modifier_alchemist_unstable_concoction") && Utils.SleepCheck("alch")) //alch
                {
                    MessageCreator("alchemist", "alchemist_unstable_concoction");
                    PlaySound("unstable_concoction_" + Addition[GetLangId]);
                    Utils.Sleep(15000, "alch");
                }
                if (mod.Any(x => x.Name == "modifier_morphling_replicate_timer") && Utils.SleepCheck("morph")) //morph
                {
                    MessageCreator("morphling", "morphling_replicate");
                    PlaySound("morphling_replicate_" + Addition[GetLangId]);
                    Utils.Sleep(15000, "morph");
                }
                if (mod.Any(x => x.Name == "modifier_bloodseeker_thirst_speed") && Utils.SleepCheck("bloodseeker")) //bloodseeker
                {
                    MessageCreator("bloodseeker", "bloodseeker_thirst");
                    PlaySound("bloodseeker_thirst_" + Addition[GetLangId]);
                    Utils.Sleep(15000, "bloodseeker");
                }
                //if (mod.Any(x => x.Name == "modifier_invoker_ghost_walk_enemy") && Utils.SleepCheck("invoker")) //invoker
                //{
                //    MessageCreator("invoker", "invoker_ghost_walk");
                //    Utils.Sleep(15000, "invoker");
                //}
                if (mod.Any(x => x.Name == "modifier_item_invisibility_edge_windwalk") && Utils.SleepCheck("shadowblade")) //shadow blade
                {
                    index = v.Name.Remove(0, 14);
                    MessageItemCreator(index, "invis_sword");
                    PlaySound("invis_sword_" + Addition[GetLangId]);
                    Utils.Sleep(15000, "shadowblade");
                }
                if (mod.Any(x => x.Name == "modifier_rune_invis") && Utils.SleepCheck("modifier_rune_invis")) // InvisRune
                {
                    index = v.Name.Remove(0, 14);
                    MessageRuneCreator(index, "rune_invis");
                    PlaySound("rune_invis_" + Addition[GetLangId]);
                    Utils.Sleep(15000, "modifier_rune_invis");
                }
                if (mod.Any(x => x.Name == "modifier_rune_haste") && Utils.SleepCheck("modifier_rune_haste")) // hasteRune
                {
                    index = v.Name.Remove(0, 14);
                    MessageRuneCreator(index, "rune_haste");
                    PlaySound("rune_haste_" + Addition[GetLangId]);
                    Utils.Sleep(15000, "modifier_rune_haste");
                }
                if (mod.Any(x => x.Name == "modifier_rune_regen") && Utils.SleepCheck("modifier_rune_regen")) // RegenRune
                {
                    index = v.Name.Remove(0, 14);
                    MessageRuneCreator(index, "rune_regen");
                    PlaySound("rune_regen_" + Addition[GetLangId]);
                    Utils.Sleep(15000, "modifier_rune_regen");
                }
                if (mod.Any(x => x.Name == "modifier_rune_arcane") && Utils.SleepCheck("modifier_rune_arcane")) // ArcaneRune
                {
                    index = v.Name.Remove(0, 14);
                    MessageRuneCreator(index, "rune_arcane");
                    PlaySound("rune_arcane_" + Addition[GetLangId]);
                    Utils.Sleep(15000, "modifier_rune_arcane");
                }
                if (mod.Any(x => x.Name == "modifier_rune_doubledamage") && Utils.SleepCheck("modifier_doubledamage")) // doubledamageRune
                {
                    index = v.Name.Remove(0, 14);
                    MessageRuneCreator(index, "rune_doubledamage");
                    PlaySound("rune_doubledamage_" + Addition[GetLangId]);
                    Utils.Sleep(15000, "modifier_doubledamage");
                }
                if (mod.Any(x => x.Name == "modifier_item_shadow_amulet_fade") && Utils.SleepCheck("modifier_item_shadow_amulet_fade")) // shadow amulet
                {
                    index = v.Name.Remove(0, 14);
                    MessageItemCreator(index, "shadow_amulet");
                    PlaySound("shadow_amulet_" + Addition[GetLangId]);
                    Utils.Sleep(15000, "modifier_item_shadow_amulet_fade");
                }
                if (mod.Any(x => x.Name == "modifier_invisible") && Utils.SleepCheck("modifier_invisible")) // glimmer cape
                {
                    index = v.Name.Remove(0, 14);
                    MessageItemCreator(index, "glimmer_cape");
                    PlaySound("glimmer_cape_" + Addition[GetLangId]);
                    Utils.Sleep(15000, "modifier_invisible");
                }
                if (mod.Any(x => x.Name == "modifier_treant_natures_guise") && Utils.SleepCheck("modifier_treant_natures_guise")) //treant invis
                {
                    index = v.Name.Remove(0, 14);
                    MessageCreator(index, "treant_natures_guise");
                    PlaySound("treant_natures_guise_" + Addition[GetLangId]);
                    Utils.Sleep(15000, "modifier_treant_natures_guise");
                }
                if (mod.Any(x => x.Name == "modifier_item_silver_edge_windwalk") && Utils.SleepCheck("silveredge")) // silver edge
                {
                    index = v.Name.Remove(0, 14);
                    MessageItemCreator(index, "silver_edge");
                    PlaySound("silver_edge_" + Addition[GetLangId]);
                    Utils.Sleep(15000, "silveredge");
                }
                if (mod.Any(x => x.Name == "modifier_clinkz_wind_walk") && Utils.SleepCheck("clinkz")) // Clinkz
                {
                    MessageCreator("clinkz", "clinkz_wind_walk");
                    PlaySound("clinkz_wind_walk_" + Addition[GetLangId]);
                    Utils.Sleep(15000, "clinkz");
                }
                if (mod.Any(x => x.Name == "modifier_teleporting") && v.ClassID == ClassID.CDOTA_Unit_Hero_Wisp && Utils.SleepCheck("wisp")) // IO
                {
                    MessageCreator("wisp", "wisp_relocate");
                    PlaySound("wisp_relocate_" + Addition[GetLangId]);
                    Utils.Sleep(15000, "wisp");
                }
                if (mod.Any(x => x.Name == "modifier_bounty_hunter_wind_walk") && Utils.SleepCheck("bounty")) // BOUNTY
                {
                    MessageCreator("bounty_hunter", "bounty_hunter_wind_walk");
                    PlaySound("bounty_hunter_wind_walk_" + Addition[GetLangId]);
                    Utils.Sleep(15000, "bounty");
                }
                if (mod.Any(x => x.Name == "modifier_nyx_assassin_vendetta") && Utils.SleepCheck("nyx")) // NYX ASSASSIN
                {
                    MessageCreator("nyx_assassin", "nyx_assassin_vendetta");
                    PlaySound("nyx_assassin_vendetta_" + Addition[GetLangId]);
                    Utils.Sleep(15000, "nyx");
                }
                if (mod.Any(x => x.Name == "modifier_teleporting") && v.ClassID == ClassID.CDOTA_Unit_Hero_Furion && Utils.SleepCheck("furion")) // furion
                {
                    MessageCreator("furion", "furion_teleportation");
                    PlaySound("furion_teleportation_" + Addition[GetLangId]);
                    Utils.Sleep(15000, "furion");
                }
                if (v.Spellbook.SpellR.IsInAbilityPhase && v.ClassID == ClassID.CDOTA_Unit_Hero_Furion && Utils.SleepCheck("furion")) // furion ULT
                {
                    MessageCreator("furion", "furion_wrath_of_nature");
                    PlaySound("furion_wrath_of_nature_" + Addition[GetLangId]);
                    Utils.Sleep(2300, "furion");
                }
            }
            // AA
            // BARA
            // SNIPER
            // BOUNTY ULT
        }

        static void Item_ValueChanged(object sender, OnValueChangeEventArgs e)
        {
            var item = sender as MenuItem;
            if (item == null)
                return;
            if (item.Name == "lang")
            {
                var selectedIndex = e.GetNewValue<StringList>().SelectedIndex;
            }
        }
        static void MessageCreator(string saitama, string onepunch)
        {
            informationmessage = new SideMessage("Skills", new Vector2(210, 60));
            informationmessage.AddElement(new Vector2(-48, -68), new Vector2(256, 128), Drawing.GetTexture("ensage_ui/other/beawareplus_msg1"));
            informationmessage.AddElement(new Vector2(10, 10), new Vector2(74, 40), Drawing.GetTexture("ensage_ui/heroes_horizontal/" + saitama));
            informationmessage.AddElement(new Vector2(160, 10), new Vector2(40, 40), Drawing.GetTexture("ensage_ui/spellicons/" + onepunch));
            informationmessage.CreateMessage();
        }
        static void MessageRuneCreator(string saitama, string onepunch)
        {
            informationmessage = new SideMessage("Rune", new Vector2(210, 60));
            informationmessage.AddElement(new Vector2(-48, -68), new Vector2(256, 128), Drawing.GetTexture("ensage_ui/other/beawareplus_msg2"));
            informationmessage.AddElement(new Vector2(10, 10), new Vector2(74, 40), Drawing.GetTexture("ensage_ui/heroes_horizontal/" + saitama));
            informationmessage.AddElement(new Vector2(160, 10), new Vector2(40, 40), Drawing.GetTexture("ensage_ui/modifier_textures/" + onepunch));
            informationmessage.CreateMessage();
        }
        static void MessageItemCreator(string saitama, string punch)
        {
            informationmessage = new SideMessage("Items", new Vector2(210, 60));
            informationmessage.AddElement(new Vector2(-48, -68), new Vector2(256, 128), Drawing.GetTexture("ensage_ui/other/beawareplus_msg3"));
            informationmessage.AddElement(new Vector2(10, 10), new Vector2(74, 40), Drawing.GetTexture("ensage_ui/heroes_horizontal/" + saitama));
            informationmessage.AddElement(new Vector2(148, 10), new Vector2(78, 40), Drawing.GetTexture("ensage_ui/items/" + punch));
            informationmessage.CreateMessage();
        }
        private static void PrintSuccess(string text, params object[] arguments)
        {
            PrintEncolored(text, ConsoleColor.Green, arguments);
        }
        private static void PrintEncolored(string text, ConsoleColor color, params object[] arguments)
        {
            var clr = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.WriteLine(text, arguments);
            Console.ForegroundColor = clr;
        }
    }
}
