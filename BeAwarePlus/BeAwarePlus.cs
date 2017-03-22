// credits: beminee, Magmaring(bruninjaman), spyware293 and Jumpering
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
        private static bool ancient_apparition_IsHere;
        private static bool nyx_assassin_IsHere;
        private static bool bounty_hunter_IsHere;
        private static Hero me;
        private static System.Collections.Generic.List<Ensage.Hero> enemies, allies;
        private static SideMessage informationmessage;
        private static readonly Menu Menu = new Menu("BeAwarePlus", "BeAwarePlus", true, "beawareplus", true);
        private static bool UseDefSound => Menu.Item("default sound").GetValue<bool>();
        private static void Main(string[] args)
        {
            Menu.AddItem(new MenuItem("enable", "Sound").SetValue(true));
            Menu.AddItem(new MenuItem("default sound", "Disable the sound heroes").SetValue(false)).SetTooltip("All sounds becomes default");
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

            Events.OnLoad += (sender, eventArgs) =>
            {
                nyx_assassin_IsHere = ObjectManager.GetEntities<Hero>()
                    .Any(
                        x => x.ClassID == ClassID.CDOTA_Unit_Hero_Nyx_Assassin && x.Team != ObjectManager.LocalHero.Team);
            };

            {
                bounty_hunter_IsHere = ObjectManager.GetEntities<Hero>()
                    .Any(
                        x => x.ClassID == ClassID.CDOTA_Unit_Hero_BountyHunter && x.Team != ObjectManager.LocalHero.Team);
            };

            {
                ancient_apparition_IsHere = ObjectManager.GetEntities<Hero>()
                    .Any(
                        x => x.ClassID == ClassID.CDOTA_Unit_Hero_AncientApparition && x.Team != ObjectManager.LocalHero.Team);
            };


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

            if (UseDefSound)
            {
                fullpath += @"\dota\materials\ensage_ui\sounds\default.wav";
            }
            else
            {
                fullpath += @"\dota\materials\ensage_ui\sounds\" + path;
            }
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

                //Bloodseeker
                if (mod.Any(x => x.Name == "modifier_bloodseeker_thirst_speed") && Utils.SleepCheck("BeAwarePlus.bloodseeker")) 
                {
                    MessageCreator("bloodseeker", "bloodseeker_thirst");
                    PlaySound("bloodseeker_thirst_" + Addition[GetLangId]);
                    Utils.Sleep(15000, "BeAwarePlus.bloodseeker");
                }

                //Rune Haste
                if (mod.Any(x => x.Name == "modifier_rune_haste") && Utils.SleepCheck("BeAwarePlus.rune_haste")) 
                {
                    index = v.Name.Remove(0, 14);
                    MessageRuneCreator(index, "rune_haste");
                    PlaySound("rune_haste_" + Addition[GetLangId]);
                    Utils.Sleep(15000, "BeAwarePlus.rune_haste");
                }

                //Rune Regen
                if (mod.Any(x => x.Name == "modifier_rune_regen") && Utils.SleepCheck("BeAwarePlus.rune_regen")) 
                {
                    index = v.Name.Remove(0, 14);
                    MessageRuneCreator(index, "rune_regen");
                    PlaySound("rune_regen_" + Addition[GetLangId]);
                    Utils.Sleep(15000, "BeAwarePlus.rune_regen");
                }

                //Rune Arcane
                if (mod.Any(x => x.Name == "modifier_rune_arcane") && Utils.SleepCheck("BeAwarePlus.rune_arcane")) 
                {
                    index = v.Name.Remove(0, 14);
                    MessageRuneCreator(index, "rune_arcane");
                    PlaySound("rune_arcane_" + Addition[GetLangId]);
                    Utils.Sleep(15000, "BeAwarePlus.rune_arcane");
                }

                //Rune Doubledamage
                if (mod.Any(x => x.Name == "modifier_rune_doubledamage") && Utils.SleepCheck("BeAwarePlus.rune_doubledamage")) 
                {
                    index = v.Name.Remove(0, 14);
                    MessageRuneCreator(index, "rune_doubledamage");
                    PlaySound("rune_doubledamage_" + Addition[GetLangId]);
                    Utils.Sleep(15000, "BeAwarePlus.rune_doubledamage");
                }

                //Rune Invis
                if (mod.Any(x => x.Name == "modifier_rune_invis") && Utils.SleepCheck("BeAwarePlus.rune_invis"))
                {
                    index = v.Name.Remove(0, 14);
                    MessageRuneCreator(index, "rune_invis");
                    PlaySound("rune_invis_" + Addition[GetLangId]);
                    Utils.Sleep(15000, "BeAwarePlus.rune_invis");
                }

                //Shadow Blade
                if (mod.Any(x => x.Name == "modifier_item_invisibility_edge_windwalk") && Utils.SleepCheck("BeAwarePlus.shadowblade"))
                {
                    index = v.Name.Remove(0, 14);
                    MessageItemCreator(index, "invis_sword");
                    PlaySound("invis_sword_" + Addition[GetLangId]);
                    Utils.Sleep(15000, "BeAwarePlus.shadowblade");
                }

                //Shadow Amulet
                if (mod.Any(x => x.Name == "modifier_item_shadow_amulet_fade") && Utils.SleepCheck("BeAwarePlus.shadow_amulet")) 
                {
                    index = v.Name.Remove(0, 14);
                    MessageItemCreator(index, "shadow_amulet");
                    PlaySound("shadow_amulet_" + Addition[GetLangId]);
                    Utils.Sleep(10000, "BeAwarePlus.shadow_amulet");
                }

                //Glimmer Cape
                if (mod.Any(x => x.Name == "modifier_item_glimmer_cape_fade") && Utils.SleepCheck("BeAwarePlus.glimmer_cape")) 
                {
                    index = v.Name.Remove(0, 14);
                    MessageItemCreator(index, "glimmer_cape");
                    PlaySound("glimmer_cape_" + Addition[GetLangId]);
                    Utils.Sleep(15000, "BeAwarePlus.glimmer_cape");
                }

                //Silver Edge
                if (mod.Any(x => x.Name == "modifier_item_silver_edge_windwalk") && Utils.SleepCheck("BeAwarePlus.silver_edge")) 
                {
                    index = v.Name.Remove(0, 14);
                    MessageItemCreator(index, "silver_edge");
                    PlaySound("silver_edge_" + Addition[GetLangId]);
                    Utils.Sleep(15000, "BeAwarePlus.silver_edge");
                }             
            }

            allies = ObjectManager.GetEntitiesParallel<Hero>().Where(x => me.Team == x.Team && x.IsValid && !x.IsIllusion && x.IsAlive).ToList();
            if (allies == null) return;

            foreach (var ally in allies)
            {
                System.Collections.Generic.IEnumerable<Modifier> modA = ally.Modifiers.ToList();

                //Spirit Breaker
                if (modA.Any(x => x.Name.Contains("spirit_breaker_charge_of_darkness_vision")) && Utils.SleepCheck("BeAwarePlus.spirit_breaker")) 
                {
                    MessageAllyCreator(ally.Name.Substring(14), "spirit_breaker_charge_of_darkness");
                    PlaySound("spirit_breaker_charge_of_darkness_" + Addition[GetLangId]);
                    Utils.Sleep(12000, "BeAwarePlus.spirit_breaker");
                }

                //Sniper
                if (modA.Any(x => x.Name.Contains("sniper_assassinate")) && Utils.SleepCheck("BeAwarePlus.sniper")) 
                {
                    MessageAllyCreator(ally.Name.Substring(14), "sniper_assassinate");
                    PlaySound("sniper_assassinate_" + Addition[GetLangId]);
                    Utils.Sleep(3000, "BeAwarePlus.sniper");
                }

                //Nevermore
                if (modA.Any(x => x.Name.Contains("modifier_nevermore_presence")) && Utils.SleepCheck("BeAwarePlus.nevermore"))
                {
                    MessageAllyCreator(ally.Name.Substring(14), "nevermore_dark_lord"); 
                    PlaySound("nevermore_dark_lord_" + Addition[GetLangId]);
                    Utils.Sleep(10000, "BeAwarePlus.nevermore");
                }

                //Bounty Hunter ULT
                if (modA.Any(x => x.Name == "modifier_bounty_hunter_track") && Utils.SleepCheck("BeAwarePlus.bounty_hunter" + ally.Handle)) 
                {
                    MessageAllyCreator(ally.Name.Substring(14), "bounty_hunter_track");
                    PlaySound("bounty_hunter_track_" + Addition[GetLangId]);
                    Utils.Sleep(30000, "BeAwarePlus.bounty_hunter" + ally.Handle);
                }

                //Invoker
                if (modA.Any(x => x.Name.Contains("modifier_invoker_ghost_walk")) && Utils.SleepCheck("BeAwarePlus.invoker")) 
                {
                    MessageAllyCreator(ally.Name.Substring(14), "invoker_ghost_walk");
                    PlaySound("invoker_ghost_walk_" + Addition[GetLangId]);
                    Utils.Sleep(15000, "BeAwarePlus.invoker");
                }
            }
        }

        public static void OnParticleEvent(Entity hero, ParticleEffectAddedEventArgs args)
        {
            //Smoke of Deceit
            if (args.Name.Contains("smoke_of_deceit") && Utils.SleepCheck("BeAwarePlus.smoke_of_deceit")) 
            {
                MessageItemCreator("default2", "smoke_of_deceit");
                PlaySound("item_smoke_of_deceit_" + Addition[GetLangId]);
                Utils.Sleep(3000, "BeAwarePlus.smoke_of_deceit");
            }

            //Ancient Apparition
            if (args.Name.Contains("ancient_apparition_ice_blast") && ancient_apparition_IsHere && Utils.SleepCheck("BeAwarePlus.ancient_apparition")) 
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

            //Sandking
            if (args.Name.Contains("sandking_epicenter") && args.ParticleEffect.Owner.Team != me.Team && Utils.SleepCheck("BeAwarePlus.sand_king"))
            {
                MessageCreator("sand_king", "sandking_epicenter");
                PlaySound("sandking_epicenter_" + Addition[GetLangId]);
                Utils.Sleep(15000, "BeAwarePlus.sand_king");
            }

            //Furion Teleport
            if (args.Name.Contains("furion_teleport") && args.ParticleEffect.Owner.Team != me.Team && Utils.SleepCheck("BeAwarePlus.furion"))
            {
                MessageCreator("furion", "furion_teleportation");
                PlaySound("furion_teleportation_" + Addition[GetLangId]);
                Utils.Sleep(1000, "BeAwarePlus.furion");
            }

            //Furion ULT
            if (args.Name.Contains("furion_wrath_of_nature") && args.ParticleEffect.Owner.Team != me.Team && Utils.SleepCheck("BeAwarePlus.furion"))
            {
                MessageCreator("furion", "furion_wrath_of_nature");
                PlaySound("furion_wrath_of_nature_" + Addition[GetLangId]);
                Utils.Sleep(1000, "BeAwarePlus.furion");
            }

            //Alchemist
            if (args.Name.Contains("alchemist_unstableconc") && args.ParticleEffect.Owner.Team != me.Team && Utils.SleepCheck("BeAwarePlus.alchemist"))
            {
                MessageCreator("alchemist", "alchemist_unstable_concoction");
                PlaySound("unstable_concoction_" + Addition[GetLangId]);
                Utils.Sleep(15000, "BeAwarePlus.alchemist");
            }

            //Bounty Hunter
            if (args.Name.Contains("bounty_hunter_windwalk") && bounty_hunter_IsHere && Utils.SleepCheck("BeAwarePlus.bounty_hunter"))
            {
                MessageCreator("bounty_hunter", "bounty_hunter_wind_walk");
                PlaySound("bounty_hunter_wind_walk_" + Addition[GetLangId]);
                Utils.Sleep(1000, "BeAwarePlus.bounty_hunter");
            }

            //Clinkz
            if (args.Name.Contains("clinkz_windwalk") && args.ParticleEffect.Owner.Team != me.Team && Utils.SleepCheck("BeAwarePlus.clinkz"))
            {
                MessageCreator("clinkz", "clinkz_wind_walk");
                PlaySound("clinkz_wind_walk_" + Addition[GetLangId]);
                Utils.Sleep(1000, "BeAwarePlus.clinkz");
            }

            //Nyx Assassin
            if (args.Name.Contains("nyx_assassin_vendetta") && nyx_assassin_IsHere && Utils.SleepCheck("BeAwarePlus.nyx_assassin"))
            {
                MessageCreator("nyx_assassin", "nyx_assassin_vendetta");
                PlaySound("nyx_assassin_vendetta_" + Addition[GetLangId]);
                Utils.Sleep(1000, "BeAwarePlus.nyx_assassin");
            }
         
            //Wisp
            if (args.Name.Contains("wisp_relocate_channel") && args.ParticleEffect.Owner.Team != me.Team && Utils.SleepCheck("BeAwarePlus.wisp"))
            {
                MessageCreator("wisp", "wisp_relocate");
                PlaySound("wisp_relocate_" + Addition[GetLangId]);
                Utils.Sleep(1000, "BeAwarePlus.wisp");
            }

            //Morphling
            if (args.Name.Contains("morphling_replicate") && args.ParticleEffect.Owner.Team != me.Team && Utils.SleepCheck("BeAwarePlus.morphling"))
            {
                MessageCreator("morphling", "morphling_replicate");
                PlaySound("morphling_replicate_" + Addition[GetLangId]);
                Utils.Sleep(15000, "BeAwarePlus.morphling");
            }

            //Troll Warlord
            if (args.Name.Contains("troll_warlord_battletrance_cast") && args.ParticleEffect.Owner.Team != me.Team && Utils.SleepCheck("BeAwarePlus.troll_warlord"))
            {
                MessageCreator("troll_warlord", "troll_warlord_battle_trance");
                PlaySound("troll_warlord_battle_trance_" + Addition[GetLangId]);
                Utils.Sleep(15000, "BeAwarePlus.troll_warlord");
            }

            //Ursa
            if (args.Name.Contains("ursa_enrage_buff") && args.ParticleEffect.Owner.Team != me.Team && Utils.SleepCheck("BeAwarePlus.ursa"))
            {
                MessageCreator("ursa", "ursa_enrage");
                PlaySound("ursa_enrage_" + Addition[GetLangId]);
                Utils.Sleep(15000, "BeAwarePlus.ursa");
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
