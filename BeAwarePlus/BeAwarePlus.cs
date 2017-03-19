// credits: beminee, Magmaring(bruninjaman) and spyware293
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
        private static System.Collections.Generic.List<Ensage.Hero> enemies, allies;
        private static SideMessage informationmessage;
        private static readonly Menu Menu = new Menu("BeAwarePlus", "BeAwarePlus", true, "beawareplus", true);
        private static void Main(string[] args)
        {
            Menu.AddItem(new MenuItem("enable", "Sound").SetValue(false));
            Menu.AddToMainMenu();

            Game.OnUpdate += Tick;
            Entity.OnParticleEffectAdded += OnParticleEvent;
            PrintSuccess(">>>>>> BeAwarePlus Loaded!");
            var sList = new StringList()
            {
                SList = LangName,
                SelectedIndex = 0
            };
            var language = new MenuItem("lang", "Language").SetValue(sList);
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

                // alchemist
                if (mod.Any(x => x.Name == "modifier_alchemist_unstable_concoction") && Utils.SleepCheck("BeAwarePlus.alch")) 
                {
                    MessageCreator("alchemist", "alchemist_unstable_concoction");
                    PlaySound("unstable_concoction_" + Addition[GetLangId]);
                    Utils.Sleep(15000, "BeAwarePlus.alch");
                }

                // morphling
                if (mod.Any(x => x.Name == "modifier_morphling_replicate_timer") && Utils.SleepCheck("BeAwarePlus.morph")) 
                {
                    MessageCreator("morphling", "morphling_replicate");
                    PlaySound("morphling_replicate_" + Addition[GetLangId]);
                    Utils.Sleep(15000, "BeAwarePlus.morph");
                }

                // bloodseeker
                if (mod.Any(x => x.Name == "modifier_bloodseeker_thirst_speed") && Utils.SleepCheck("BeAwarePlus.bloodseeker")) 
                {
                    MessageCreator("bloodseeker", "bloodseeker_thirst");
                    PlaySound("bloodseeker_thirst_" + Addition[GetLangId]);
                    Utils.Sleep(15000, "BeAwarePlus.bloodseeker");
                }

                // shadow blade
                if (mod.Any(x => x.Name == "modifier_item_invisibility_edge_windwalk") && Utils.SleepCheck("BeAwarePlus.shadowblade")) 
                {
                    index = v.Name.Remove(0, 14);
                    MessageItemCreator(index, "invis_sword");
                    PlaySound("invis_sword_" + Addition[GetLangId]);
                    Utils.Sleep(15000, "BeAwarePlus.shadowblade");
                }

                // InvisRune
                if (mod.Any(x => x.Name == "modifier_rune_invis") && Utils.SleepCheck("BeAwarePlus.modifier_rune_invis")) 
                {
                    index = v.Name.Remove(0, 14);
                    MessageRuneCreator(index, "rune_invis");
                    PlaySound("rune_invis_" + Addition[GetLangId]);
                    Utils.Sleep(15000, "BeAwarePlus.modifier_rune_invis");
                }

                // hasteRune
                if (mod.Any(x => x.Name == "modifier_rune_haste") && Utils.SleepCheck("BeAwarePlus.modifier_rune_haste")) 
                {
                    index = v.Name.Remove(0, 14);
                    MessageRuneCreator(index, "rune_haste");
                    PlaySound("rune_haste_" + Addition[GetLangId]);
                    Utils.Sleep(15000, "BeAwarePlus.modifier_rune_haste");
                }

                // RegenRune
                if (mod.Any(x => x.Name == "modifier_rune_regen") && Utils.SleepCheck("BeAwarePlus.modifier_rune_regen")) 
                {
                    index = v.Name.Remove(0, 14);
                    MessageRuneCreator(index, "rune_regen");
                    PlaySound("rune_regen_" + Addition[GetLangId]);
                    Utils.Sleep(15000, "BeAwarePlus.modifier_rune_regen");
                }

                // ArcaneRune
                if (mod.Any(x => x.Name == "modifier_rune_arcane") && Utils.SleepCheck("BeAwarePlus.modifier_rune_arcane")) 
                {
                    index = v.Name.Remove(0, 14);
                    MessageRuneCreator(index, "rune_arcane");
                    PlaySound("rune_arcane_" + Addition[GetLangId]);
                    Utils.Sleep(15000, "BeAwarePlus.modifier_rune_arcane");
                }

                // doubledamageRune
                if (mod.Any(x => x.Name == "modifier_rune_doubledamage") && Utils.SleepCheck("BeAwarePlus.modifier_doubledamage")) 
                {
                    index = v.Name.Remove(0, 14);
                    MessageRuneCreator(index, "rune_doubledamage");
                    PlaySound("rune_doubledamage_" + Addition[GetLangId]);
                    Utils.Sleep(15000, "BeAwarePlus.modifier_doubledamage");
                }

                // shadow amulet
                if (mod.Any(x => x.Name == "modifier_item_shadow_amulet_fade") && Utils.SleepCheck("BeAwarePlus.modifier_item_shadow_amulet_fade")) 
                {
                    index = v.Name.Remove(0, 14);
                    MessageItemCreator(index, "shadow_amulet");
                    PlaySound("shadow_amulet_" + Addition[GetLangId]);
                    Utils.Sleep(15000, "BeAwarePlus.modifier_item_shadow_amulet_fade");
                }

                // glimmer cape
                if (mod.Any(x => x.Name == "modifier_invisible") && Utils.SleepCheck("BeAwarePlus.modifier_invisible")) 
                {
                    index = v.Name.Remove(0, 14);
                    MessageItemCreator(index, "glimmer_cape");
                    PlaySound("glimmer_cape_" + Addition[GetLangId]);
                    Utils.Sleep(15000, "BeAwarePlus.modifier_invisible");
                }

                //treant invis
                if (mod.Any(x => x.Name == "modifier_treant_natures_guise") && Utils.SleepCheck("BeAwarePlus.modifier_treant_natures_guise")) 
                {
                    index = v.Name.Remove(0, 14);
                    MessageCreator(index, "treant_natures_guise");
                    PlaySound("treant_natures_guise_" + Addition[GetLangId]);
                    Utils.Sleep(15000, "BeAwarePlus.modifier_treant_natures_guise");
                }

                // silver edge
                if (mod.Any(x => x.Name == "modifier_item_silver_edge_windwalk") && Utils.SleepCheck("BeAwarePlus.silveredge")) 
                {
                    index = v.Name.Remove(0, 14);
                    MessageItemCreator(index, "silver_edge");
                    PlaySound("silver_edge_" + Addition[GetLangId]);
                    Utils.Sleep(15000, "BeAwarePlus.silveredge");
                }

                // Clinkz
                if (mod.Any(x => x.Name == "modifier_clinkz_wind_walk") && Utils.SleepCheck("BeAwarePlus.clinkz")) 
                {
                    MessageCreator("clinkz", "clinkz_wind_walk");
                    PlaySound("clinkz_wind_walk_" + Addition[GetLangId]);
                    Utils.Sleep(15000, "BeAwarePlus.clinkz");
                }

                // IO
                if (mod.Any(x => x.Name == "modifier_teleporting") && v.ClassID == ClassID.CDOTA_Unit_Hero_Wisp && Utils.SleepCheck("BeAwarePlus.wisp")) 
                {
                    MessageCreator("wisp", "wisp_relocate");
                    PlaySound("wisp_relocate_" + Addition[GetLangId]);
                    Utils.Sleep(15000, "BeAwarePlus.wisp");
                }

                // BOUNTY
                if (mod.Any(x => x.Name == "modifier_bounty_hunter_wind_walk") && Utils.SleepCheck("BeAwarePlus.bounty")) 
                {
                    MessageCreator("bounty_hunter", "bounty_hunter_wind_walk");
                    PlaySound("bounty_hunter_wind_walk_" + Addition[GetLangId]);
                    Utils.Sleep(15000, "BeAwarePlus.bounty");
                }

                // NYX ASSASSIN
                if (mod.Any(x => x.Name == "modifier_nyx_assassin_vendetta") && Utils.SleepCheck("BeAwarePlus.nyx")) 
                {
                    MessageCreator("nyx_assassin", "nyx_assassin_vendetta");
                    PlaySound("nyx_assassin_vendetta_" + Addition[GetLangId]);
                    Utils.Sleep(15000, "BeAwarePlus.nyx");
                }

                // furion
                if (mod.Any(x => x.Name == "modifier_teleporting") && v.ClassID == ClassID.CDOTA_Unit_Hero_Furion && Utils.SleepCheck("BeAwarePlus.furion")) 
                {
                    MessageCreator("furion", "furion_teleportation");
                    PlaySound("furion_teleportation_" + Addition[GetLangId]);
                    Utils.Sleep(15000, "BeAwarePlus.furion");
                }

                // furion ULT
                if (v.Spellbook.SpellR.IsInAbilityPhase && v.ClassID == ClassID.CDOTA_Unit_Hero_Furion && Utils.SleepCheck("BeAwarePlus.furion")) 
                {
                    MessageCreator("furion", "furion_wrath_of_nature");
                    PlaySound("furion_wrath_of_nature_" + Addition[GetLangId]);
                    Utils.Sleep(2300, "BeAwarePlus.furion");
                }                
            }

            allies = ObjectManager.GetEntitiesParallel<Hero>().Where(x => me.Team == x.Team && x.IsValid && !x.IsIllusion && x.IsAlive).ToList();
            if (allies == null) return;

            foreach (var ally in allies)
            {
                System.Collections.Generic.IEnumerable<Modifier> modA = ally.Modifiers.ToList();

                // Spirit Breaker
                if (modA.Any(x => x.Name.Contains("charge_of_darkness_vision")) && Utils.SleepCheck("BeAwarePlus.charge")) 
                {
                    MessageAllyCreator(ally.Name.Substring(14), "spirit_breaker_charge_of_darkness");
                    PlaySound("spirit_breaker_charge_of_darkness_" + Addition[GetLangId]);
                    Utils.Sleep(12000, "BeAwarePlus.charge");
                }

                // Sniper
                if (modA.Any(x => x.Name.Contains("assassinate")) && Utils.SleepCheck("BeAwarePlus.assassinate")) 
                {
                    MessageAllyCreator(ally.Name.Substring(14), "sniper_assassinate");
                    PlaySound("sniper_assassinate_" + Addition[GetLangId]);
                    Utils.Sleep(2500, "BeAwarePlus.assassinate");
                }

                // BOUNTY ULT
                if (modA.Any(x => x.Name == "modifier_bounty_hunter_track") && Utils.SleepCheck("BeAwarePlus.track" + ally.Handle)) 
                {
                    MessageAllyCreator(ally.Name.Substring(14), "bounty_hunter_track");
                    PlaySound("bounty_hunter_track_" + Addition[GetLangId]);
                    Utils.Sleep(30000, "BeAwarePlus.track" + ally.Handle);
                }

                // Invoker
                if (modA.Any(x => x.Name.Contains("modifier_invoker_ghost_walk")) && Utils.SleepCheck("BeAwarePlus.ghostwalk")) 
                {
                    MessageAllyCreator(ally.Name.Substring(14), "invoker_ghost_walk");
                    PlaySound("invoker_ghost_walk_" + Addition[GetLangId]);
                    Utils.Sleep(15000, "BeAwarePlus.ghostwalk");
                }
            }
        }

        public static void OnParticleEvent(Entity hero, ParticleEffectAddedEventArgs args)
        {
            // Smoke
            if (args.Name.Contains("smoke_of_deceit") && Utils.SleepCheck("BeAwarePlus.smoke")) 
            {
                MessageItemCreator("default2", "smoke_of_deceit");
                PlaySound("item_smoke_of_deceit_" + Addition[GetLangId]);
                Utils.Sleep(3000, "BeAwarePlus.smoke");
            }

            //Apparition
            if (args.Name.Contains("ancient_apparition_ice_blast") && Utils.SleepCheck("BeAwarePlus.ancient_apparition")) 
            {
                MessageCreator("ancient_apparition", "ancient_apparition_ice_blast");
                PlaySound("ancient_apparition_ice_blast_" + Addition[GetLangId]);
                Utils.Sleep(3000, "BeAwarePlus.ancient_apparition");
            }

            //Mirana
            if (args.Name.Contains("mirana_moonlight_cast") && args.ParticleEffect.Owner.Team != me.Team && Utils.SleepCheck("BeAwarePlus.mirana")) 
            {
                MessageCreator("mirana", "mirana_invis");
                PlaySound("moonlight_shadow_" + Addition[GetLangId]);
                Utils.Sleep(15000, "BeAwarePlus.mirana");
            }

        }
        static void MessageAllyCreator(string saitama, string onepunch)
        {
            informationmessage = new SideMessage("Skills", new Vector2(210, 60));
            informationmessage.AddElement(new Vector2(-48, -68), new Vector2(256, 128), Drawing.GetTexture("ensage_ui/other/beawareplus_msg0"));
            informationmessage.AddElement(new Vector2(125, 10), new Vector2(74, 40), Drawing.GetTexture("ensage_ui/heroes_horizontal/" + saitama));
            informationmessage.AddElement(new Vector2(10, 10), new Vector2(40, 40), Drawing.GetTexture("ensage_ui/spellicons/" + onepunch));
            informationmessage.CreateMessage(); 
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
