// credits: beminee, Magmaring(bruninjaman), spyware293 and Jumpering
using System;
using System.Linq;
using Ensage;
using Ensage.Common;
using Ensage.Common.Extensions;
using Ensage.Common.Menu;
using Ensage.Common.Objects;
using Ensage.Common.Objects.UtilityObjects;
using SharpDX;

namespace BeAwarePlus
{
    class BeAwarePlus
    {       
        private static bool ancient_apparition_IsHere;
        private static bool nyx_assassin_IsHere;
        private static bool bounty_hunter_IsHere;
        private static bool morphling_IsHere;
        private static bool _loaded = false;
        private static readonly Sleeper HeroChecker = new Sleeper();
        private static Hero me;
        private static SideMessage informationmessage;
        private static readonly Menu Menu = new Menu("BeAwarePlus", "BeAwarePlus", true, "beawareplus", true);
        private static bool UseDefSound => Menu.Item("default sound").GetValue<bool>();
        private static void Main(string[] args)
        {
            Events.OnLoad += EventsOnOnLoad;
        }
        private static void EventsOnOnLoad(object sender, EventArgs eventArgs)
        {
            Menu.AddItem(new MenuItem("enable", "Sound").SetValue(true));
            Menu.AddItem(new MenuItem("default sound", "Disable the sound heroes").SetValue(false)).SetTooltip("All sounds becomes default");
            Menu.AddToMainMenu();

            PrintSuccess(">>>>>> BeAwarePlus Loaded!");
            var sList = new StringList()
            {
                SList = LangName,
                SelectedIndex = 0
            };
            var language = new MenuItem("lang", "Language").SetValue(sList);
            Menu.AddItem(language);

            me = ObjectManager.LocalHero;
            Entity.OnParticleEffectAdded += OnParticleEvent;
            Unit.OnModifierAdded += HeroOnOnModifierAdded;
            Game.OnUpdate += GameOnOnUpdate;
            Events.OnLoad -= EventsOnOnLoad;
        }
        private static void GameOnOnUpdate(EventArgs args)
        {
            if (_loaded && Heroes.All.Count >= 10)
                return;
            if (HeroChecker.Sleeping)
                return;
            if (Heroes.All.Count >= 10)
                _loaded = true;
            HeroChecker.Sleep(5000);
            if (me == null || !me.IsValid)
                me = ObjectManager.LocalHero;
            var enemyTeam = Heroes.GetByTeam(me.GetEnemyTeam());

            if (!nyx_assassin_IsHere)
                nyx_assassin_IsHere =
                enemyTeam.Any(x => x.ClassID == ClassID.CDOTA_Unit_Hero_Nyx_Assassin);

            if (!bounty_hunter_IsHere)
                bounty_hunter_IsHere =
                enemyTeam.Any(x => x.ClassID == ClassID.CDOTA_Unit_Hero_BountyHunter);

            if (!ancient_apparition_IsHere)
                ancient_apparition_IsHere =
                enemyTeam.Any(x => x.ClassID == ClassID.CDOTA_Unit_Hero_AncientApparition);

            if (!morphling_IsHere)
                morphling_IsHere =
                enemyTeam.Any(x => x.ClassID == ClassID.CDOTA_Unit_Hero_Morphling);
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
        private static void HeroOnOnModifierAdded(Unit sender, ModifierChangedEventArgs args)
        {

                //Invoker Sun Strike    
                if (args.Modifier.Name.Contains("modifier_invoker_sun_strike") && args.Modifier.Owner.Team != me.Team)
                {
                    MessageCreator("invoker", "invoker_sun_strike");
                    PlaySound("invoker_sun_strike_" + Addition[GetLangId]);
                }

                //Kunkka Torrent
                if (args.Modifier.Name.Contains("modifier_kunkka_torrent_thinker") && args.Modifier.Owner.Team != me.Team)
                {
                    MessageCreator("kunkka", "kunkka_torrent");
                    PlaySound("kunkka_torrent_" + Addition[GetLangId]);
                }

                //Monkey King Primal Spring
                if (args.Modifier.Name.Contains("modifier_monkey_king_spring_thinker") && args.Modifier.Owner.Team != me.Team)
                {
                    MessageCreator("monkey_king", "monkey_king_primal_spring");
                    PlaySound("monkey_king_primal_spring_" + Addition[GetLangId]);
                }
         
                //Radar
                if (args.Modifier.Name.Contains("modifier_radar_thinker") && args.Modifier.Owner.Team != me.Team)
                {
                    MessageCreator("radar", "radars_scan");
                    PlaySound("radars_scan_" + Addition[GetLangId]);
                }  
                      
            if (!(sender is Hero))
                return;
            if (sender.IsIllusion)
                return;            
            string index;
            if (sender.Team == me.Team)
            {

                //Spirit Breaker Charge of Darkness
                if (args.Modifier.Name.Contains("modifier_spirit_breaker_charge_of_darkness_vision"))
                {
                    MessageAllyCreator(sender.Name.Substring(14), "sniper_assassinate");
                    PlaySound("spirit_breaker_charge_of_darkness_" + Addition[GetLangId]);                   
                }

                //Sniper Assassinate
                if (args.Modifier.Name.Contains("modifier_sniper_assassinate"))
                {
                    MessageAllyCreator(sender.Name.Substring(14), "sniper_assassinate");
                    PlaySound("sniper_assassinate_" + Addition[GetLangId]);
                }

                //Nevermore Dark Lord
                if (args.Modifier.Name.Contains("modifier_nevermore_presence") && Utils.SleepCheck("nevermore_dark_lord"))
                {
                    MessageAllyCreator(sender.Name.Substring(14), "nevermore_dark_lord");
                    PlaySound("nevermore_dark_lord_" + Addition[GetLangId]);
                    Utils.Sleep(5000, "nevermore_dark_lord");
                }

                //Bounty Hunter Track
                if (args.Modifier.Name.Contains("modifier_bounty_hunter_track"))
                {
                    MessageAllyCreator(sender.Name.Substring(14), "bounty_hunter_track");
                    PlaySound("bounty_hunter_track_" + Addition[GetLangId]);
                }

                //Invoker Ghost Walk
                if (args.Modifier.Name.Contains("modifier_invoker_ghost_walk_enemy") && Utils.SleepCheck("invoker_ghost_walk"))
                {
                    MessageAllyCreator(sender.Name.Substring(14), "invoker_ghost_walk");
                    PlaySound("invoker_ghost_walk_" + Addition[GetLangId]);
                    Utils.Sleep(3000, "invoker_ghost_walk");
                }

            }
            else
            {

                //Bloodseeker Thirst
                if (args.Modifier.Name.Contains("modifier_bloodseeker_thirst_speed") && Utils.SleepCheck("bloodseeker_thirst"))
                {
                    MessageCreator("bloodseeker", "bloodseeker_thirst");
                    PlaySound("bloodseeker_thirst_" + Addition[GetLangId]);
                    Utils.Sleep(10000, "bloodseeker_thirst");
                }

                //Rune Haste
                if (args.Modifier.Name.Contains("modifier_rune_haste") && Utils.SleepCheck("rune_haste"))
                {
                    index = sender.Name.Remove(0, 14);
                    MessageRuneCreator(index, "rune_haste");
                    PlaySound("rune_haste_" + Addition[GetLangId]);
                    Utils.Sleep(10000, "rune_haste");
                }

                //Rune Regen
                if (args.Modifier.Name.Contains("modifier_rune_regen") && Utils.SleepCheck("rune_regen"))
                {
                    index = sender.Name.Remove(0, 14);
                    MessageRuneCreator(index, "rune_haste");
                    PlaySound("rune_regen_" + Addition[GetLangId]);
                    Utils.Sleep(10000, "rune_regen");
                }

                //Rune Arcane
                if (args.Modifier.Name.Contains("modifier_rune_arcane") && Utils.SleepCheck("rune_arcane"))
                {
                    index = sender.Name.Remove(0, 14);
                    MessageRuneCreator(index, "rune_arcane");
                    PlaySound("rune_arcane_" + Addition[GetLangId]);
                    Utils.Sleep(10000, "rune_arcane");
                }

                //Rune Doubledamage
                if (args.Modifier.Name.Contains("modifier_rune_doubledamage") && Utils.SleepCheck("rune_doubledamage"))
                {
                    index = sender.Name.Remove(0, 14);
                    MessageRuneCreator(index, "rune_doubledamage");
                    PlaySound("rune_doubledamage_" + Addition[GetLangId]);
                    Utils.Sleep(10000, "rune_doubledamage");              
                }

                //Rune Invis
                if (args.Modifier.Name.Contains("modifier_rune_invis") && Utils.SleepCheck("rune_invis"))
                {
                    index = sender.Name.Remove(0, 14);
                    MessageRuneCreator(index, "rune_invis");
                    PlaySound("rune_invis_" + Addition[GetLangId]);
                    Utils.Sleep(3000, "rune_invis");
                }

                //Shadow Blade
                if (args.Modifier.Name.Contains("modifier_item_invisibility_edge_windwalk") && Utils.SleepCheck("invis_sword"))
                {
                    index = sender.Name.Remove(0, 14);
                    MessageItemCreator(index, "invis_sword");
                    PlaySound("invis_sword_" + Addition[GetLangId]);
                    Utils.Sleep(3000, "invis_sword");
                }

                //Shadow Amulet
                if (args.Modifier.Name.Contains("modifier_item_shadow_amulet_fade") && Utils.SleepCheck("shadow_amulet"))
                {
                    index = sender.Name.Remove(0, 14);
                    MessageItemCreator(index, "shadow_amulet");
                    PlaySound("shadow_amulet_" + Addition[GetLangId]);
                    Utils.Sleep(3000, "shadow_amulet");
                }

                //Glimmer Cape
                if (args.Modifier.Name.Contains("modifier_item_glimmer_cape_fade") && Utils.SleepCheck("glimmer_cape"))
                {
                    index = sender.Name.Remove(0, 14);
                    MessageItemCreator(index, "glimmer_cape");
                    PlaySound("glimmer_cape_" + Addition[GetLangId]);
                    Utils.Sleep(3000, "glimmer_cape");
                }

                //Silver Edge
                if (args.Modifier.Name.Contains("modifier_item_silver_edge_windwalk") && Utils.SleepCheck("silver_edge"))
                {
                    index = sender.Name.Remove(0, 14);
                    MessageItemCreator(index, "silver_edge");
                    PlaySound("silver_edge_" + Addition[GetLangId]);
                    Utils.Sleep(3000, "silver_edge");
                }  
                              
            }
        }
        public static void OnParticleEvent(Entity hero, ParticleEffectAddedEventArgs args)
        {

                //Smoke of Deceit
                if (args.Name.Contains("smoke_of_deceit"))
                {
                    MessageItemCreator("default2", "smoke_of_deceit");
                    PlaySound("item_smoke_of_deceit_" + Addition[GetLangId]);
                }

                //Ancient Apparition Ice Blast
                if (args.Name.Contains("ancient_apparition_ice_blast") && ancient_apparition_IsHere) 
                {
                    MessageCreator("ancient_apparition", "ancient_apparition_ice_blast");
                    PlaySound("ancient_apparition_ice_blast_" + Addition[GetLangId]);
                }

                //Mirana Moonlight
                if (args.Name.Contains("mirana_moonlight_cast") && args.ParticleEffect.Owner.Team != me.Team) 
                {
                    MessageCreator("mirana", "mirana_invis");
                    PlaySound("moonlight_shadow_" + Addition[GetLangId]);
                }

                //Sandking Epicenter
                if (args.Name.Contains("sandking_epicenter") && args.ParticleEffect.Owner.Team != me.Team)
                {
                    MessageCreator("sand_king", "sandking_epicenter");
                    PlaySound("sandking_epicenter_" + Addition[GetLangId]);
                }

                //Furion Teleport
                if (args.Name.Contains("furion_teleport") && args.ParticleEffect.Owner.Team != me.Team)
                {
                    MessageCreator("furion", "furion_teleportation");
                    PlaySound("furion_teleportation_" + Addition[GetLangId]);               
                }

                //Furion Wrath of Nature
                if (args.Name.Contains("furion_wrath_of_nature") && args.ParticleEffect.Owner.Team != me.Team)
                {
                    MessageCreator("furion", "furion_wrath_of_nature");
                    PlaySound("furion_wrath_of_nature_" + Addition[GetLangId]);               
                }

                //Alchemist Unstable Concoction
                if (args.Name.Contains("alchemist_unstableconc") && args.ParticleEffect.Owner.Team != me.Team)
                {
                    MessageCreator("alchemist", "alchemist_unstable_concoction");
                    PlaySound("unstable_concoction_" + Addition[GetLangId]);
                }

                //Bounty Hunter Wind Walk
                if (args.Name.Contains("bounty_hunter_windwalk") && bounty_hunter_IsHere)
                {
                    MessageCreator("bounty_hunter", "bounty_hunter_wind_walk");
                    PlaySound("bounty_hunter_wind_walk_" + Addition[GetLangId]);
                }

                //Clinkz Wind Walk
                if (args.Name.Contains("clinkz_windwalk") && args.ParticleEffect.Owner.Team != me.Team)
                {
                    MessageCreator("clinkz", "clinkz_wind_walk");
                    PlaySound("clinkz_wind_walk_" + Addition[GetLangId]);
                }

                //Nyx Assassin Vendetta
                if (args.Name.Contains("nyx_assassin_vendetta_start") && nyx_assassin_IsHere)
                {
                    MessageCreator("nyx_assassin", "nyx_assassin_vendetta");
                    PlaySound("nyx_assassin_vendetta_" + Addition[GetLangId]);
                }

                //Wisp Relocate
                if (args.Name.Contains("wisp_relocate_channel") && args.ParticleEffect.Owner.Team != me.Team)
                {
                    MessageCreator("wisp", "wisp_relocate");
                    PlaySound("wisp_relocate_" + Addition[GetLangId]);
                }

                //Morphling Replicate
                if (args.Name.Contains("morphling_replicate") && morphling_IsHere)
                {
                    MessageCreator("morphling", "morphling_replicate");
                    PlaySound("morphling_replicate_" + Addition[GetLangId]);
                }

                //Troll Warlord Battle Trance
                if (args.Name.Contains("troll_warlord_battletrance_cast") && args.ParticleEffect.Owner.Team != me.Team)
                {
                    MessageCreator("troll_warlord", "troll_warlord_battle_trance");
                    PlaySound("troll_warlord_battle_trance_" + Addition[GetLangId]);
                }

                //Ursa Enrage
                if (args.Name.Contains("ursa_enrage_buff") && args.ParticleEffect.Owner.Team != me.Team)
                {
                    MessageCreator("ursa", "ursa_enrage");
                    PlaySound("ursa_enrage_" + Addition[GetLangId]);
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
