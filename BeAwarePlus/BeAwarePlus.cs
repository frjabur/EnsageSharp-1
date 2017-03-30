// credits: beminee, Magmaring(bruninjaman), spyware293 and Jumpering
using System;
using System.Linq;
using System.Timers;
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
        private static bool Roshan_Dead;
        private static int Roshan_Respawn_Min_Time;
        private static int Roshan_Respawn_Max_Time;
        private static bool Ancient_Apparition_IsHere;
        private static bool Nyx_Assassin_IsHere;
        private static bool Bounty_Hunter_IsHere;
        private static bool Morphling_IsHere;
        private static bool Clinkz_IsHere;
        private static bool _loaded = false;
        private static readonly Sleeper HeroChecker = new Sleeper();
        private static Timer A_Timer;
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
            Menu.AddItem(new MenuItem("default sound", "Disable the Sound Heroes").SetValue(false)).SetTooltip("All Sounds Becomes Default");           
            Menu.AddItem(new MenuItem("rune", "Time per Sec Rune").SetValue(new Slider(10, 0, 30)));
            Menu.AddItem(new MenuItem("hand_of_midas", "Time per Sec Midas").SetValue(new Slider(5, 0, 10)));
            Menu.AddToMainMenu();

            Roshan_Dead = false;
            Roshan_Respawn_Min_Time = 480;
            Roshan_Respawn_Max_Time = 660;
            A_Timer = new Timer(1000);
            A_Timer.Elapsed += OnTimedEvent;
            A_Timer.AutoReset = true;
            A_Timer.Enabled = true;

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
            Game.OnFireEvent += Game_OnGameEvent;
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

            if (!Nyx_Assassin_IsHere)
                Nyx_Assassin_IsHere =
                enemyTeam.Any(x => x.ClassID == ClassID.CDOTA_Unit_Hero_Nyx_Assassin);

            if (!Bounty_Hunter_IsHere)
                Bounty_Hunter_IsHere =
                enemyTeam.Any(x => x.ClassID == ClassID.CDOTA_Unit_Hero_BountyHunter);

            if (!Ancient_Apparition_IsHere)
                Ancient_Apparition_IsHere =
                enemyTeam.Any(x => x.ClassID == ClassID.CDOTA_Unit_Hero_AncientApparition);

            if (!Morphling_IsHere)
                Morphling_IsHere =
                enemyTeam.Any(x => x.ClassID == ClassID.CDOTA_Unit_Hero_Morphling);

            if (!Clinkz_IsHere)
                Clinkz_IsHere =
                enemyTeam.Any(x => x.ClassID == ClassID.CDOTA_Unit_Hero_Clinkz);

          
        }        
        public static int GetLangId
        {
            get { return Menu.Item("lang").GetValue<StringList>().SelectedIndex; }
            set { throw new NotImplementedException(); }
        }
        private static readonly string[] Addition =
        {
            "en",
            "ru"
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
                MessageEnemyCreator("invoker", "invoker_sun_strike");
                PlaySound("invoker_sun_strike_" + Addition[GetLangId] + ".wav");
            }

            //Kunkka Torrent
            if (args.Modifier.Name.Contains("modifier_kunkka_torrent_thinker") && args.Modifier.Owner.Team != me.Team)
            {
                MessageEnemyCreator("kunkka", "kunkka_torrent");
                PlaySound("default_" + Addition[GetLangId] + ".wav");
            }

            //Monkey King Primal Spring
            if (args.Modifier.Name.Contains("modifier_monkey_king_spring_thinker") && args.Modifier.Owner.Team != me.Team)
            {
                MessageEnemyCreator("monkey_king", "monkey_king_primal_spring");
                PlaySound("default_" + Addition[GetLangId] + ".wav");
            }

            //Radar
            if (args.Modifier.Name.Contains("modifier_radar_thinker") && args.Modifier.Owner.Team != me.Team)
            {
                MessageEnemyCreator("radar", "radars_scan");
                PlaySound("radars_scan_" + Addition[GetLangId] + ".wav");
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
                    MessageAllyCreator(sender.Name.Substring(14), "spirit_breaker_charge_of_darkness");
                    PlaySound("spirit_breaker_charge_of_darkness_" + Addition[GetLangId] + ".wav");
                }

                //Nevermore Dark Lord
                if (args.Modifier.Name.Contains("modifier_nevermore_presence") && Utils.SleepCheck("nevermore_dark_lord"))
                {
                    MessageAllyCreator(sender.Name.Substring(14), "nevermore_dark_lord");
                    PlaySound("default_" + Addition[GetLangId] + ".wav");
                    Utils.Sleep(5000, "nevermore_dark_lord");
                }

                //Sniper Assassinate
                if (args.Modifier.Name.Contains("modifier_sniper_assassinate"))
                {
                    MessageAllyCreator(sender.Name.Substring(14), "sniper_assassinate");
                    PlaySound("sniper_assassinate_" + Addition[GetLangId] + ".wav");
                }

                //Bounty Hunter Track
                if (args.Modifier.Name.Contains("modifier_bounty_hunter_track"))
                {
                    MessageAllyCreator(sender.Name.Substring(14), "bounty_hunter_track");
                    PlaySound("bounty_hunter_track_" + Addition[GetLangId] + ".wav");
                }

                //Invoker Ghost Walk
                if (args.Modifier.Name.Contains("modifier_invoker_ghost_walk_enemy") && Utils.SleepCheck("invoker_ghost_walk"))
                {
                    MessageAllyCreator(sender.Name.Substring(14), "invoker_ghost_walk");
                    PlaySound("invoker_ghost_walk_" + Addition[GetLangId] + ".wav");
                    Utils.Sleep(3000, "invoker_ghost_walk");
                }

                //Bloodseeker Thirst
                if (args.Modifier.Name.Contains("modifier_bloodseeker_thirst_vision") && Utils.SleepCheck("bloodseeker_thirst"))
                {
                    MessageAllyCreator(sender.Name.Substring(14), "bloodseeker_thirst");
                    PlaySound("bloodseeker_thirst_" + Addition[GetLangId] + ".wav");
                    Utils.Sleep(10000, "bloodseeker_thirst");
                }

            }
            else
            {

                //Rune Haste
                if (args.Modifier.Name.Contains("modifier_rune_haste") && Utils.SleepCheck("rune_haste"))
                {
                    index = sender.Name.Remove(0, 14);
                    MessageRuneCreator(index, "rune_haste");
                    PlaySound("rune_haste_" + Addition[GetLangId] + ".wav");
                    Utils.Sleep(10000, "rune_haste");
                }

                //Rune Regen
                if (args.Modifier.Name.Contains("modifier_rune_regen") && Utils.SleepCheck("rune_regen"))
                {
                    index = sender.Name.Remove(0, 14);
                    MessageRuneCreator(index, "rune_regen");
                    PlaySound("rune_regen_" + Addition[GetLangId] + ".wav");
                    Utils.Sleep(10000, "rune_regen");
                }

                //Rune Arcane
                if (args.Modifier.Name.Contains("modifier_rune_arcane") && Utils.SleepCheck("rune_arcane"))
                {
                    index = sender.Name.Remove(0, 14);
                    MessageRuneCreator(index, "rune_arcane");
                    PlaySound("default_" + Addition[GetLangId] + ".wav");
                    Utils.Sleep(10000, "rune_arcane");
                }

                //Rune Doubledamage
                if (args.Modifier.Name.Contains("modifier_rune_doubledamage") && Utils.SleepCheck("rune_doubledamage"))
                {
                    index = sender.Name.Remove(0, 14);
                    MessageRuneCreator(index, "rune_doubledamage");
                    PlaySound("rune_doubledamage_" + Addition[GetLangId] + ".wav");
                    Utils.Sleep(10000, "rune_doubledamage");
                }

                //Rune Invis
                if (args.Modifier.Name.Contains("modifier_rune_invis") && Utils.SleepCheck("rune_invis"))
                {
                    index = sender.Name.Remove(0, 14);
                    MessageRuneCreator(index, "rune_invis");
                    PlaySound("rune_invis_" + Addition[GetLangId] + ".wav");
                    Utils.Sleep(3000, "rune_invis");
                }

                //Shadow Blade
                if (args.Modifier.Name.Contains("modifier_item_invisibility_edge_windwalk") && Utils.SleepCheck("invis_sword"))
                {
                    index = sender.Name.Remove(0, 14);
                    MessageItemCreator(index, "invis_sword");
                    PlaySound("invis_sword_" + Addition[GetLangId] + ".wav");
                    Utils.Sleep(3000, "invis_sword");
                }

                //Shadow Amulet
                if (args.Modifier.Name.Contains("modifier_item_shadow_amulet_fade") && Utils.SleepCheck("shadow_amulet"))
                {
                    index = sender.Name.Remove(0, 14);
                    MessageItemCreator(index, "shadow_amulet");
                    PlaySound("shadow_amulet_" + Addition[GetLangId] + ".wav");
                    Utils.Sleep(3000, "shadow_amulet");
                }

                //Glimmer Cape
                if (args.Modifier.Name.Contains("modifier_item_glimmer_cape_fade") && Utils.SleepCheck("glimmer_cape"))
                {
                    index = sender.Name.Remove(0, 14);
                    MessageItemCreator(index, "glimmer_cape");
                    PlaySound("glimmer_cape_" + Addition[GetLangId] + ".wav");
                    Utils.Sleep(3000, "glimmer_cape");
                }

                //Silver Edge
                if (args.Modifier.Name.Contains("modifier_item_silver_edge_windwalk") && Utils.SleepCheck("silver_edge"))
                {
                    index = sender.Name.Remove(0, 14);
                    MessageItemCreator(index, "silver_edge");
                    PlaySound("silver_edge_" + Addition[GetLangId] + ".wav");
                    Utils.Sleep(3000, "silver_edge");
                }

                //Gem of True Sight
                if (args.Modifier.Name.Contains("modifier_item_gem_of_true_sight") && Utils.SleepCheck("gem"))
                {
                    index = sender.Name.Remove(0, 14);
                    MessageItemCreator(index, "gem");
                    PlaySound("default_" + Addition[GetLangId] + ".wav");
                    Utils.Sleep(15000, "gem");
                }

                //Divine Rapier
                if (args.Modifier.Name.Contains("modifier_item_divine_rapier") && Utils.SleepCheck("rapier"))
                {
                    index = sender.Name.Remove(0, 14);
                    MessageItemCreator(index, "rapier");
                    PlaySound("default_" + Addition[GetLangId] + ".wav");
                    Utils.Sleep(15000, "rapier");
                }

            }
        }
        public static void OnParticleEvent(Entity hero, ParticleEffectAddedEventArgs args)
        {

            //Smoke of Deceit  
            if (args.Name.Contains("smoke_of_deceit"))
            {
                DelayAction.Add(150, () =>
                {
                    var anyAllyWithSmokeEffect =
                    Heroes.GetByTeam(me.Team).Any(x => x.HasModifier("modifier_smoke_of_deceit"));
                    if (!anyAllyWithSmokeEffect)
                    {
                        MessageItemCreator("default2", "smoke_of_deceit");
                        PlaySound("item_smoke_of_deceit_" + Addition[GetLangId] + ".wav");
                    }
                });
            }

            //Ancient Apparition Ice Blast
            if (args.Name.Contains("ancient_apparition_ice_blast") && Ancient_Apparition_IsHere)
            {
                MessageEnemyCreator("ancient_apparition", "ancient_apparition_ice_blast");
                PlaySound("ancient_apparition_ice_blast_" + Addition[GetLangId] + ".wav");
            }

            //Mirana Moonlight
            if (args.Name.Contains("mirana_moonlight_cast") && args.ParticleEffect.Owner.Team != me.Team)
            {
                MessageEnemyCreator("mirana", "mirana_invis");
                PlaySound("moonlight_shadow_" + Addition[GetLangId] + ".wav");
            }

            //Sandking Epicenter
            if (args.Name.Contains("sandking_epicenter") && args.ParticleEffect.Owner.Team != me.Team)
            {
                MessageEnemyCreator("sand_king", "sandking_epicenter");
                PlaySound("sandking_epicenter_" + Addition[GetLangId] + ".wav");
            }

            //Furion Teleport
            if (args.Name.Contains("furion_teleport") && args.ParticleEffect.Owner.Team != me.Team)
            {
                MessageEnemyCreator("furion", "furion_teleportation");
                PlaySound("furion_teleportation_" + Addition[GetLangId] + ".wav");
            }

            //Furion Wrath of Nature
            if (args.Name.Contains("furion_wrath_of_nature") && args.ParticleEffect.Owner.Team != me.Team)
            {
                MessageEnemyCreator("furion", "furion_wrath_of_nature");
                PlaySound("furion_wrath_of_nature_" + Addition[GetLangId] + ".wav");
            }

            //Alchemist Unstable Concoction
            if (args.Name.Contains("alchemist_unstableconc") && args.ParticleEffect.Owner.Team != me.Team)
            {
                MessageEnemyCreator("alchemist", "alchemist_unstable_concoction");
                PlaySound("unstable_concoction_" + Addition[GetLangId] + ".wav");
            }

            //Bounty Hunter Wind Walk
            if (args.Name.Contains("bounty_hunter_windwalk") && Bounty_Hunter_IsHere)
            {
                MessageEnemyCreator("bounty_hunter", "bounty_hunter_wind_walk");
                PlaySound("bounty_hunter_wind_walk_" + Addition[GetLangId] + ".wav");
            }

            //Clinkz Wind Walk
            if (args.Name.Contains("clinkz_windwalk") && args.ParticleEffect.Owner.Team != me.Team)
            {
                MessageEnemyCreator("clinkz", "clinkz_wind_walk");
                PlaySound("clinkz_wind_walk_" + Addition[GetLangId] + ".wav");
            }

            //Nyx Assassin Vendetta
            if (args.Name.Contains("nyx_assassin_vendetta_start") && Nyx_Assassin_IsHere)
            {
                MessageEnemyCreator("nyx_assassin", "nyx_assassin_vendetta");
                PlaySound("nyx_assassin_vendetta_" + Addition[GetLangId] + ".wav");
            }

            //Wisp Relocate
            if (args.Name.Contains("wisp_relocate_channel") && args.ParticleEffect.Owner.Team != me.Team)
            {
                MessageEnemyCreator("wisp", "wisp_relocate");
                PlaySound("wisp_relocate_" + Addition[GetLangId] + ".wav");
            }

            //Morphling Replicate
            if (args.Name.Contains("morphling_replicate") && Morphling_IsHere)
            {
                MessageEnemyCreator("morphling", "morphling_replicate");
                PlaySound("morphling_replicate_" + Addition[GetLangId] + ".wav");
            }

            //Troll Warlord Battle Trance
            if (args.Name.Contains("troll_warlord_battletrance_cast") && args.ParticleEffect.Owner.Team != me.Team)
            {
                MessageEnemyCreator("troll_warlord", "troll_warlord_battle_trance");
                PlaySound("troll_warlord_battle_trance_" + Addition[GetLangId] + ".wav");
            }

            //Ursa Enrage
            if (args.Name.Contains("ursa_enrage_buff") && args.ParticleEffect.Owner.Team != me.Team)
            {
                MessageEnemyCreator("ursa", "ursa_enrage");
                PlaySound("ursa_enrage_" + Addition[GetLangId] + ".wav");
            }

        }
        static void Game_OnGameEvent(FireEventEventArgs args)
        {
            if (args.GameEvent.Name == "dota_roshan_kill")
            {
                Roshan_Dead = true;
                Roshan_Respawn_Min_Time = 480;
                Roshan_Respawn_Max_Time = 660;
            }
        }
        private static void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            if (!Game.IsInGame) return;
            var me = ObjectManager.LocalPlayer;
            if (me == null || me.Hero == null) return;

            //Rune
            if (((Math.Round(Game.GameTime) + Menu.Item("rune").GetValue<Slider>().Value) % 120) == 0 && Utils.SleepCheck("check_rune"))
            {
                MessageCheckRuneCreator(null);
                PlaySound("check_rune_" + Addition[GetLangId] + ".wav");
                Utils.Sleep(5000, "check_rune");
            }

            //Hand of Midas
            var Midas = me.Hero.FindItem("item_hand_of_midas");
            if (Midas != null && Math.Round(Midas.Cooldown) == Menu.Item("hand_of_midas").GetValue<Slider>().Value && Utils.SleepCheck("use_midas"))
            {
                MessageUseMidasCreator(null);
                PlaySound("use_midas_" + Addition[GetLangId] + ".wav");
                Utils.Sleep(5000, "use_midas");
            }

            //Roshan MB Alive
            if (Roshan_Dead)
                if (--Roshan_Respawn_Min_Time + 5 == 0 && Utils.SleepCheck("roshan_mb_alive"))
                {
                    MessageRoshanMBAliveCreator(null);
                    PlaySound("roshan_mb_alive_" + Addition[GetLangId] + ".wav");
                    Utils.Sleep(5000, "roshan_mb_alive");
                }

            //Roshan Alive
            if (Roshan_Dead)
                if (--Roshan_Respawn_Max_Time + 5 == 0 && Utils.SleepCheck("roshan_alive"))
                {
                    MessageRoshanAliveCreator(null);
                    PlaySound("roshan_alive_" + Addition[GetLangId] + ".wav");
                    Utils.Sleep(5000, "roshan_alive");
                    Roshan_Dead = false;
                }
        }
        static void MessageAllyCreator(string hero, string spell)
        {
            informationmessage = new SideMessage(hero + spell, new Vector2(256, 128), stayTime: 5000);
            informationmessage.AddElement(new Vector2(0, 0), new Vector2(256, 128), Drawing.GetTexture("ensage_ui/other/msg0_" + Addition[GetLangId]));
            informationmessage.AddElement(new Vector2(152, 62), new Vector2(97, 55), Drawing.GetTexture("ensage_ui/heroes_horizontal/" + hero));
            informationmessage.AddElement(new Vector2(9, 62), new Vector2(55, 55), Drawing.GetTexture("ensage_ui/spellicons/" + spell));
            informationmessage.CreateMessage(); 
        }
        static void MessageEnemyCreator(string hero, string spell)
        {
            informationmessage = new SideMessage(hero + spell, new Vector2(256, 128), stayTime: 5000);
            informationmessage.AddElement(new Vector2(0, 0), new Vector2(256, 128), Drawing.GetTexture("ensage_ui/other/msg1_" + Addition[GetLangId]));
            informationmessage.AddElement(new Vector2(9, 62), new Vector2(97, 55), Drawing.GetTexture("ensage_ui/heroes_horizontal/" + hero));
            informationmessage.AddElement(new Vector2(193, 62), new Vector2(55, 55), Drawing.GetTexture("ensage_ui/spellicons/" + spell));
            informationmessage.CreateMessage();
        }
        static void MessageRuneCreator(string hero, string rune)
        {
            informationmessage = new SideMessage(hero + rune, new Vector2(256, 128), stayTime: 5000);
            informationmessage.AddElement(new Vector2(0, 0), new Vector2(256, 128), Drawing.GetTexture("ensage_ui/other/msg2_" + Addition[GetLangId]));
            informationmessage.AddElement(new Vector2(9, 62), new Vector2(97, 55), Drawing.GetTexture("ensage_ui/heroes_horizontal/" + hero));
            informationmessage.AddElement(new Vector2(193, 62), new Vector2(55, 55), Drawing.GetTexture("ensage_ui/modifier_textures/" + rune));
            informationmessage.CreateMessage();
        }
        static void MessageItemCreator(string hero, string item)
        {
            informationmessage = new SideMessage(hero + item, new Vector2(256, 128), stayTime: 5000);
            informationmessage.AddElement(new Vector2(0, 0), new Vector2(256, 128), Drawing.GetTexture("ensage_ui/other/msg3_" + Addition[GetLangId]));
            informationmessage.AddElement(new Vector2(9, 62), new Vector2(97, 55), Drawing.GetTexture("ensage_ui/heroes_horizontal/" + hero));
            informationmessage.AddElement(new Vector2(170, 62), new Vector2(113, 55), Drawing.GetTexture("ensage_ui/items/" + item));
            informationmessage.CreateMessage();
        }
        static void MessageCheckRuneCreator(string check_rune)
        {
            informationmessage = new SideMessage("check_rune", new Vector2(256, 128), stayTime: 5000);
            informationmessage.AddElement(new Vector2(0, 0), new Vector2(256, 128), Drawing.GetTexture("ensage_ui/other/msg4_" + Addition[GetLangId]));
            informationmessage.CreateMessage();
        }
        static void MessageUseMidasCreator(string use_midas)
        {
            informationmessage = new SideMessage("use_midas", new Vector2(256, 128), stayTime: 5000);
            informationmessage.AddElement(new Vector2(0, 0), new Vector2(256, 128), Drawing.GetTexture("ensage_ui/other/msg5_" + Addition[GetLangId]));
            informationmessage.CreateMessage();
        }
        static void MessageRoshanAliveCreator(string roshan_alive)
        {
            informationmessage = new SideMessage("rosha_nalive", new Vector2(256, 128), stayTime: 5000);
            informationmessage.AddElement(new Vector2(0, 0), new Vector2(256, 128), Drawing.GetTexture("ensage_ui/other/msg6_" + Addition[GetLangId]));
            informationmessage.CreateMessage();
        }
        static void MessageRoshanMBAliveCreator(string roshan_mb_alive)
        {
            informationmessage = new SideMessage("roshan_mb_alive", new Vector2(256, 128), stayTime: 5000);
            informationmessage.AddElement(new Vector2(0, 0), new Vector2(256, 128), Drawing.GetTexture("ensage_ui/other/msg7_" + Addition[GetLangId]));
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
