// credits: beminee, Magmaring(bruninjaman), spyware293 and Jumpering
using Ensage;
using Ensage.Common;
using Ensage.Common.Extensions;
using Ensage.Common.Menu;
using Ensage.Common.Objects;
using SharpDX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;

namespace BeAwarePlus
{
    public static class BeAwarePlus
    {
        private static bool IgnorAllyTP = true;
        private static bool FurionFix;
        internal static Player player;
        private static Vector3 TPID;
        internal static string HeroColor;
        private static bool Team;
        internal static Vector2 MiniMapPosition;
        private static bool Roshan_Dead;
        private static int Roshan_Respawn_Min_Time;
        private static int Roshan_Respawn_Max_Time;
        private static Timer A_Timer;
        internal static Hero me;
        private static void Main(string[] args)
        {
            Events.OnLoad += EventsOnOnLoad;
        }
        private static void EventsOnOnLoad(object sender, EventArgs eventArgs)
        {         
            MenuManager.Init();
            HeroChecker.Init();
            DrawingMiniMap.Init();
            Others.Init();
            Resolution.Init();
                       
            Roshan_Dead = false;
            Roshan_Respawn_Min_Time = 480;
            Roshan_Respawn_Max_Time = 660;
            A_Timer = new Timer(1000);
            A_Timer.Elapsed += OnTimedEvent;
            A_Timer.AutoReset = true;
            A_Timer.Enabled = true;

            me = ObjectManager.LocalHero;
            Entity.OnParticleEffectAdded += OnParticleEvent;
            Unit.OnModifierAdded += HeroOnOnModifierAdded;            
            Events.OnLoad -= EventsOnOnLoad;
            Game.OnFireEvent += Game_OnGameEvent;
        }               
        internal static int GetLangId
        {
            get { return MenuManager.Menu.Item("lang").GetValue<StringList>().SelectedIndex; }
            set { throw new NotImplementedException(); }
        }        
        internal static readonly string[] Addition =    
        {
            "en",
            "ru"
        };
        internal static readonly string[] LangName =     
        {
            "EN",
            "RU"
        };        
        private static void HeroOnOnModifierAdded(Unit sender, ModifierChangedEventArgs args)
        {

            //Invoker Sun Strike    
            if (args.Modifier.Name.Contains("modifier_invoker_sun_strike") && args.Modifier.Owner.Team != me.Team && MenuManager.Menu.Item("invoker_sun_strike").GetValue<bool>())
            {
                if (MenuManager.Menu.Item("invoker_sun_strike_msg").GetValue<bool>())
                {
                    MessageCreator.MessageEnemyCreator("invoker", "invoker_sun_strike");
                }
                if (MenuManager.Menu.Item("invoker_sun_strike_sound").GetValue<bool>())
                {
                    Sound.PlaySound("invoker_sun_strike_" + Addition[GetLangId] + ".wav");
                }
                if (MenuManager.Menu.Item("invoker_sun_strike_minimap").GetValue<bool>())
                {
                    MiniMapPosition = HUDInfo.WorldToMinimap(args.Modifier.Owner.Position);
                    HeroColor = "npc_dota_hero_invoker";
                    Helper.HeroSpells();
                }                                               
            }

            //Kunkka Torrent
            if (args.Modifier.Name.Contains("modifier_kunkka_torrent_thinker") && args.Modifier.Owner.Team != me.Team && MenuManager.Menu.Item("kunkka_torrent").GetValue<bool>())
            {
                if (MenuManager.Menu.Item("kunkka_torrent_msg").GetValue<bool>())
                {
                    MessageCreator.MessageEnemyCreator("kunkka", "kunkka_torrent");
                }
                if (MenuManager.Menu.Item("kunkka_torrent_sound").GetValue<bool>())
                {
                    Sound.PlaySound("default_" + Addition[GetLangId] + ".wav");
                }
                if (MenuManager.Menu.Item("kunkka_torrent_minimap").GetValue<bool>())
                {
                    MiniMapPosition = HUDInfo.WorldToMinimap(args.Modifier.Owner.Position);
                    HeroColor = "npc_dota_hero_kunkka";
                    Helper.HeroSpells();
                }                                                
            }

            //Monkey King Primal Spring
            if (args.Modifier.Name.Contains("modifier_monkey_king_spring_thinker") && args.Modifier.Owner.Team != me.Team && MenuManager.Menu.Item("monkey_king_primal_spring").GetValue<bool>())
            {
                if (MenuManager.Menu.Item("monkey_king_primal_spring_msg").GetValue<bool>())
                {
                    MessageCreator.MessageEnemyCreator("monkey_king", "monkey_king_primal_spring");
                }
                if (MenuManager.Menu.Item("monkey_king_primal_spring_sound").GetValue<bool>())
                {
                    Sound.PlaySound("default_" + Addition[GetLangId] + ".wav");
                }
                if (MenuManager.Menu.Item("monkey_king_primal_spring_minimap").GetValue<bool>())
                {
                    MiniMapPosition = HUDInfo.WorldToMinimap(args.Modifier.Owner.Position);
                    HeroColor = "npc_dota_hero_monkey_king";
                    Helper.HeroSpells();
                }         
            }

            //Radar
            if (args.Modifier.Name.Contains("modifier_radar_thinker") && args.Modifier.Owner.Team != me.Team && MenuManager.Menu.Item("radar_scan").GetValue<bool>())
            {
                if (MenuManager.Menu.Item("radar_scan_msg").GetValue<bool>())
                {
                    MessageCreator.MessageEnemyCreator("radar", "radar_scan");
                }
                if (MenuManager.Menu.Item("radar_scan_sound").GetValue<bool>())
                {
                    Sound.PlaySound("radar_scan_" + Addition[GetLangId] + ".wav");
                }
                if (MenuManager.Menu.Item("radar_scan_minimap").GetValue<bool>())
                {
                    MiniMapPosition = HUDInfo.WorldToMinimap(args.Modifier.Owner.Position);
                    Helper.Item();
                }          
            }

            if (!(sender is Hero))
                return;
            if (sender.IsIllusion)
                return;
            string index;
            if (sender.Team == me.Team)
            {
                //Ignor Ally TP
                if (args.Modifier.Name.Contains("modifier_teleporting"))
                {
                    IgnorAllyTP = false;
                }

                //Spirit Breaker Charge of Darkness
                if (args.Modifier.Name.Contains("modifier_spirit_breaker_charge_of_darkness_vision") && MenuManager.Menu.Item("spirit_breaker_charge_of_darkness").GetValue<bool>())
                {
                    if (MenuManager.Menu.Item("spirit_breaker_charge_of_darkness_msg").GetValue<bool>())
                    {
                        MessageCreator.MessageAllyCreator(sender.Name.Substring(14), "spirit_breaker_charge_of_darkness");
                    }
                    if (MenuManager.Menu.Item("spirit_breaker_charge_of_darkness_sound").GetValue<bool>())
                    {
                        Sound.PlaySound("spirit_breaker_charge_of_darkness_" + Addition[GetLangId] + ".wav");
                    }
                    if (MenuManager.Menu.Item("spirit_breaker_charge_of_darkness_minimap_end").GetValue<bool>())
                    {
                        MiniMapPosition = HUDInfo.WorldToMinimap(args.Modifier.Owner.Position);
                        HeroColor = "npc_dota_hero_spirit_breaker";
                        Helper.HeroSpells();
                    }       
                }

                //Shadow Fiend Dark Lord
                if (args.Modifier.Name.Contains("modifier_nevermore_presence") && HeroChecker.Nevermore_IsHere && Utils.SleepCheck("nevermore_dark_lord") && MenuManager.Menu.Item("nevermore_dark_lord").GetValue<bool>())
                {
                    if (MenuManager.Menu.Item("nevermore_dark_lord_msg").GetValue<bool>())
                    {
                        MessageCreator.MessageAllyCreator(sender.Name.Substring(14), "nevermore_dark_lord");
                    }
                    if (MenuManager.Menu.Item("nevermore_dark_lord_sound").GetValue<bool>())
                    {
                        Sound.PlaySound("default_" + Addition[GetLangId] + ".wav");
                    }                  
                    Utils.Sleep(5000, "nevermore_dark_lord");
                }

                //Sniper Assassinate
                if (args.Modifier.Name.Contains("modifier_sniper_assassinate") && MenuManager.Menu.Item("sniper_assassinate").GetValue<bool>())
                {
                    if (MenuManager.Menu.Item("sniper_assassinate_msg").GetValue<bool>())
                    {
                        MessageCreator.MessageAllyCreator(sender.Name.Substring(14), "sniper_assassinate");
                    }
                    if (MenuManager.Menu.Item("sniper_assassinate_sound").GetValue<bool>())
                    {
                        Sound.PlaySound("sniper_assassinate_" + Addition[GetLangId] + ".wav");
                    }
                    if (MenuManager.Menu.Item("sniper_assassinate_minimap").GetValue<bool>())
                    {
                        MiniMapPosition = HUDInfo.WorldToMinimap(args.Modifier.Owner.Position);
                        HeroColor = "npc_dota_hero_sniper";
                        Helper.HeroSpells();
                    }              
                }

                //Bounty Hunter Track
                if (args.Modifier.Name.Contains("modifier_bounty_hunter_track") && HeroChecker.Bounty_Hunter_IsHere && MenuManager.Menu.Item("bounty_hunter_track").GetValue<bool>())
                {
                    if (MenuManager.Menu.Item("bounty_hunter_track_msg").GetValue<bool>())
                    {
                        MessageCreator.MessageAllyCreator(sender.Name.Substring(14), "bounty_hunter_track");
                    }
                    if (MenuManager.Menu.Item("bounty_hunter_track_sound").GetValue<bool>())
                    {
                        Sound.PlaySound("bounty_hunter_track_" + Addition[GetLangId] + ".wav");
                    }                                      
                }

                //Invoker Ghost Walk
                if (args.Modifier.Name.Contains("modifier_invoker_ghost_walk_enemy") && Utils.SleepCheck("invoker_ghost_walk") && MenuManager.Menu.Item("invoker_ghost_walk").GetValue<bool>())
                {
                    if (MenuManager.Menu.Item("invoker_ghost_walk_msg").GetValue<bool>())
                    {
                        MessageCreator.MessageAllyCreator(sender.Name.Substring(14), "invoker_ghost_walk");
                    }
                    if (MenuManager.Menu.Item("invoker_ghost_walk_sound").GetValue<bool>())
                    {
                        Sound.PlaySound("invoker_ghost_walk_" + Addition[GetLangId] + ".wav");
                    }                   
                    Utils.Sleep(3000, "invoker_ghost_walk");
                }

                //Bloodseeker Thirst
                if (args.Modifier.Name.Contains("modifier_bloodseeker_thirst_vision") && Utils.SleepCheck("bloodseeker_thirst") && MenuManager.Menu.Item("bloodseeker_thirst").GetValue<bool>())
                {
                    if (MenuManager.Menu.Item("bloodseeker_thirst_msg").GetValue<bool>())
                    {
                        MessageCreator.MessageAllyCreator(sender.Name.Substring(14), "bloodseeker_thirst");
                    }
                    if (MenuManager.Menu.Item("bloodseeker_thirst_sound").GetValue<bool>())
                    {
                        Sound.PlaySound("bloodseeker_thirst_" + Addition[GetLangId] + ".wav");
                    }
                    if (MenuManager.Menu.Item("bloodseeker_thirst_minimap").GetValue<bool>())
                    {
                        MiniMapPosition = HUDInfo.WorldToMinimap(args.Modifier.Owner.Position);
                        HeroColor = "npc_dota_hero_bloodseeker";
                        Helper.HeroSpells();
                    }                                       
                    Utils.Sleep(10000, "bloodseeker_thirst");
                }
            }
            else
            {

                //Rune Haste
                if (args.Modifier.Name.Contains("modifier_rune_haste") && Utils.SleepCheck("rune_haste") && MenuManager.Menu.Item("rune_haste").GetValue<bool>())
                {
                    if (MenuManager.Menu.Item("rune_haste_msg").GetValue<bool>())
                    {
                        index = sender.Name.Remove(0, 14);
                        MessageCreator.MessageRuneCreator(index, "rune_haste");
                    }
                    if (MenuManager.Menu.Item("rune_haste_sound").GetValue<bool>())
                    {
                        Sound.PlaySound("rune_haste_" + Addition[GetLangId] + ".wav");
                    }                                        
                    Utils.Sleep(10000, "rune_haste");
                }

                //Rune Regen
                if (args.Modifier.Name.Contains("modifier_rune_regen") && Utils.SleepCheck("rune_regen") && MenuManager.Menu.Item("rune_regen").GetValue<bool>())
                {
                    if (MenuManager.Menu.Item("rune_regen_msg").GetValue<bool>())
                    {
                        index = sender.Name.Remove(0, 14);
                        MessageCreator.MessageRuneCreator(index, "rune_regen");
                    }
                    if (MenuManager.Menu.Item("rune_regen_sound").GetValue<bool>())
                    {
                        Sound.PlaySound("rune_regen_" + Addition[GetLangId] + ".wav");
                    }                                        
                    Utils.Sleep(10000, "rune_regen");
                }

                //Rune Arcane
                if (args.Modifier.Name.Contains("modifier_rune_arcane") && Utils.SleepCheck("rune_arcane") && MenuManager.Menu.Item("rune_arcane").GetValue<bool>())
                {
                    if (MenuManager.Menu.Item("rune_arcane_msg").GetValue<bool>())
                    {
                        index = sender.Name.Remove(0, 14);
                        MessageCreator.MessageRuneCreator(index, "rune_arcane");
                    }
                    if (MenuManager.Menu.Item("rune_arcane_sound").GetValue<bool>())
                    {
                        Sound.PlaySound("default_" + Addition[GetLangId] + ".wav");
                    }                                        
                    Utils.Sleep(10000, "rune_arcane");
                }

                //Rune Doubledamage
                if (args.Modifier.Name.Contains("modifier_rune_doubledamage") && Utils.SleepCheck("rune_doubledamage") && MenuManager.Menu.Item("rune_doubledamage").GetValue<bool>())
                {
                    if (MenuManager.Menu.Item("rune_doubledamage_msg").GetValue<bool>())
                    {
                        index = sender.Name.Remove(0, 14);
                        MessageCreator.MessageRuneCreator(index, "rune_doubledamage");
                    }
                    if (MenuManager.Menu.Item("rune_doubledamage_sound").GetValue<bool>())
                    {
                        Sound.PlaySound("rune_doubledamage_" + Addition[GetLangId] + ".wav");
                    }                                        
                    Utils.Sleep(10000, "rune_doubledamage");
                }

                //Rune Invis
                if (args.Modifier.Name.Contains("modifier_rune_invis") && Utils.SleepCheck("rune_invis") && MenuManager.Menu.Item("rune_invis").GetValue<bool>())
                {
                    if (MenuManager.Menu.Item("rune_invis_msg").GetValue<bool>())
                    {
                        index = sender.Name.Remove(0, 14);
                        MessageCreator.MessageRuneCreator(index, "rune_invis");
                    }
                    if (MenuManager.Menu.Item("rune_invis_sound").GetValue<bool>())
                    {
                        Sound.PlaySound("rune_invis_" + Addition[GetLangId] + ".wav");
                    }                    
                    Utils.Sleep(3000, "rune_invis");
                }

                //Shadow Blade
                if (args.Modifier.Name.Contains("modifier_item_invisibility_edge_windwalk") && Utils.SleepCheck("invis_sword") && MenuManager.Menu.Item("invis_sword").GetValue<bool>())
                {
                    if (MenuManager.Menu.Item("invis_sword_msg").GetValue<bool>())
                    {
                        index = sender.Name.Remove(0, 14);
                        MessageCreator.MessageItemCreator(index, "invis_sword");
                    }
                    if (MenuManager.Menu.Item("invis_sword_sound").GetValue<bool>())
                    {
                        Sound.PlaySound("invis_sword_" + Addition[GetLangId] + ".wav");
                    }
                    if (MenuManager.Menu.Item("invis_sword_minimap").GetValue<bool>())
                    {
                        MiniMapPosition = HUDInfo.WorldToMinimap(args.Modifier.Owner.Position);
                        Helper.Item();
                    }                                       
                    Utils.Sleep(3000, "invis_sword");
                }

                //Shadow Amulet
                if (args.Modifier.Name.Contains("modifier_item_shadow_amulet_fade") && Utils.SleepCheck("shadow_amulet") && MenuManager.Menu.Item("shadow_amulet").GetValue<bool>())
                {
                    if (MenuManager.Menu.Item("shadow_amulet_msg").GetValue<bool>())
                    {
                        index = sender.Name.Remove(0, 14);
                        MessageCreator.MessageItemCreator(index, "shadow_amulet");
                    }
                    if (MenuManager.Menu.Item("shadow_amulet_sound").GetValue<bool>())
                    {
                        Sound.PlaySound("shadow_amulet_" + Addition[GetLangId] + ".wav");
                    }
                    if (MenuManager.Menu.Item("shadow_amulet_minimap").GetValue<bool>())
                    {
                        MiniMapPosition = HUDInfo.WorldToMinimap(args.Modifier.Owner.Position);
                        Helper.Item();
                    }                                   
                    Utils.Sleep(3000, "shadow_amulet");                    
                }

                //Glimmer Cape
                if (args.Modifier.Name.Contains("modifier_item_glimmer_cape_fade") && Utils.SleepCheck("glimmer_cape") && MenuManager.Menu.Item("glimmer_cape").GetValue<bool>())
                {
                    if (MenuManager.Menu.Item("glimmer_cape_msg").GetValue<bool>())
                    {
                        index = sender.Name.Remove(0, 14);
                        MessageCreator.MessageItemCreator(index, "glimmer_cape");
                    }
                    if (MenuManager.Menu.Item("glimmer_cape_sound").GetValue<bool>())
                    {
                        Sound.PlaySound("glimmer_cape_" + Addition[GetLangId] + ".wav");
                    }
                    if (MenuManager.Menu.Item("glimmer_cape_minimap").GetValue<bool>())
                    {                        
                        MiniMapPosition = HUDInfo.WorldToMinimap(args.Modifier.Owner.Position);
                        Helper.Item();
                    }                                                           
                    Utils.Sleep(3000, "glimmer_cape");
                }

                //Silver Edge
                if (args.Modifier.Name.Contains("modifier_item_silver_edge_windwalk") && Utils.SleepCheck("silver_edge") && MenuManager.Menu.Item("silver_edge").GetValue<bool>())
                {
                    if (MenuManager.Menu.Item("silver_edge_msg").GetValue<bool>())
                    {
                        index = sender.Name.Remove(0, 14);
                        MessageCreator.MessageItemCreator(index, "silver_edge");
                    }
                    if (MenuManager.Menu.Item("silver_edge_sound").GetValue<bool>())
                    {
                        Sound.PlaySound("silver_edge_" + Addition[GetLangId] + ".wav");
                    }
                    if (MenuManager.Menu.Item("silver_edge_minimap").GetValue<bool>())
                    {
                        MiniMapPosition = HUDInfo.WorldToMinimap(args.Modifier.Owner.Position);
                        Helper.Item();
                    }                                                           
                    Utils.Sleep(3000, "silver_edge");
                }

                //Gem of True Sight
                if (args.Modifier.Name.Contains("modifier_item_gem_of_true_sight") && Utils.SleepCheck("gem") && MenuManager.Menu.Item("gem").GetValue<bool>())
                {
                    if (MenuManager.Menu.Item("gem_msg").GetValue<bool>())
                    {
                        index = sender.Name.Remove(0, 14);
                        MessageCreator.MessageItemCreator(index, "gem");
                    }
                    if (MenuManager.Menu.Item("gem_sound").GetValue<bool>())
                    {
                        Sound.PlaySound("default_" + Addition[GetLangId] + ".wav");
                    }                                       
                    Utils.Sleep(15000, "gem");
                }

                //Divine Rapier
                if (args.Modifier.Name.Contains("modifier_item_divine_rapier") && Utils.SleepCheck("rapier") && MenuManager.Menu.Item("rapier").GetValue<bool>())
                {
                    if (MenuManager.Menu.Item("rapier_msg").GetValue<bool>())
                    {
                        index = sender.Name.Remove(0, 14);
                        MessageCreator.MessageItemCreator(index, "rapier");
                    }
                    if (MenuManager.Menu.Item("rapier_sound").GetValue<bool>())
                    {
                        Sound.PlaySound("default_" + Addition[GetLangId] + ".wav");
                    }                                     
                    Utils.Sleep(15000, "rapier");
                }

            }
        }

        public static void OnParticleEvent(Entity hero, ParticleEffectAddedEventArgs args)
        {

            //Smoke of Deceit  
            if (args.Name.Contains("smoke_of_deceit") && Utils.SleepCheck("smoke_of_deceit") && MenuManager.Menu.Item("smoke_of_deceit").GetValue<bool>())
            {
                DelayAction.Add(50, () =>
                {
                    var anyAllyWithSmokeEffect =
                    Heroes.GetByTeam(me.Team).Any(x => x.HasModifier("modifier_smoke_of_deceit"));
                    if (!anyAllyWithSmokeEffect)
                    {
                        if (MenuManager.Menu.Item("smoke_of_deceit_msg").GetValue<bool>())
                        {
                            MessageCreator.MessageItemCreator("default2", "smoke_of_deceit");
                        }
                        if (MenuManager.Menu.Item("smoke_of_deceit_sound").GetValue<bool>())
                        {
                            Sound.PlaySound("item_smoke_of_deceit_" + Addition[GetLangId] + ".wav");
                        }
                        if (MenuManager.Menu.Item("smoke_of_deceit_minimap").GetValue<bool>())
                        {
                            MiniMapPosition = HUDInfo.WorldToMinimap(args.ParticleEffect.GetControlPoint(0));
                            Helper.Item();
                        }
                        Utils.Sleep(5000, "smoke_of_deceit");
                    }
                });
            }
           
            //Ancient Apparition Ice Blast
            if (args.Name.Contains("ancient_apparition_ice_blast_final") && HeroChecker.Ancient_Apparition_IsHere && MenuManager.Menu.Item("ancient_apparition_ice_blast").GetValue<bool>())
            {
                if (MenuManager.Menu.Item("ancient_apparition_ice_blast_msg").GetValue<bool>())
                {
                    MessageCreator.MessageEnemyCreator("ancient_apparition", "ancient_apparition_ice_blast");
                }
                if (MenuManager.Menu.Item("ancient_apparition_ice_blast_sound").GetValue<bool>())
                {
                    Sound.PlaySound("ancient_apparition_ice_blast_" + Addition[GetLangId] + ".wav");
                }
                DelayAction.Add(50, () =>
                {
                    if (MenuManager.Menu.Item("ancient_apparition_ice_blast_minimap").GetValue<bool>())
                    {
                        MiniMapPosition = HUDInfo.WorldToMinimap(args.ParticleEffect.GetControlPoint(0));
                        HeroColor = "npc_dota_hero_ancient_apparation";
                        Helper.HeroSpells();
                    }
                });
            }

            //Mirana Moonlight Shadow
            if (args.Name.Contains("mirana_moonlight_cast") && args.ParticleEffect.Owner.Team != me.Team && MenuManager.Menu.Item("mirana_invis").GetValue<bool>())
            {
                if (MenuManager.Menu.Item("mirana_invis_msg").GetValue<bool>())
                {
                    MessageCreator.MessageEnemyCreator("mirana", "mirana_invis");
                }
                if (MenuManager.Menu.Item("mirana_invis_sound").GetValue<bool>())
                {
                    Sound.PlaySound("moonlight_shadow_" + Addition[GetLangId] + ".wav");
                }
                DelayAction.Add(50, () =>
                {
                    if (MenuManager.Menu.Item("mirana_invis_minimap_mirana").GetValue<bool>())
                    {
                        MiniMapPosition = HUDInfo.WorldToMinimap(args.ParticleEffect.GetControlPoint(0));
                        HeroColor = "npc_dota_hero_mirana";
                        Helper.HeroSpells();
                    }
                });
            }

            //Mirana Moonlight Shadow All Mini Map Heroes
            if (args.Name.Contains("mirana_moonlight_recipient") && args.ParticleEffect.Owner.Team != me.Team && MenuManager.Menu.Item("mirana_invis").GetValue<bool>())
            {
                DelayAction.Add(50, () =>
                {
                    if (MenuManager.Menu.Item("mirana_invis_minimap_all_heroes").GetValue<bool>())
                    {
                        MiniMapPosition = HUDInfo.WorldToMinimap(args.ParticleEffect.GetControlPoint(0));
                        HeroColor = "npc_dota_hero_mirana";
                        Helper.HeroSpells();
                    }
                });
            }

            //Sandking Epicenter
            if (args.Name.Contains("sandking_epicenter") && args.ParticleEffect.Owner.Team != me.Team && MenuManager.Menu.Item("sandking_epicenter").GetValue<bool>())
            {
                if (MenuManager.Menu.Item("sandking_epicenter_msg").GetValue<bool>())
                {
                    MessageCreator.MessageEnemyCreator("sand_king", "sandking_epicenter");
                }
                if (MenuManager.Menu.Item("sandking_epicenter_sound").GetValue<bool>())
                {
                    Sound.PlaySound("default_" + Addition[GetLangId] + ".wav");
                }
                DelayAction.Add(50, () =>
                {
                    if (MenuManager.Menu.Item("sandking_epicenter_minimap").GetValue<bool>())
                    {
                        MiniMapPosition = HUDInfo.WorldToMinimap(args.ParticleEffect.GetControlPoint(0));
                        HeroColor = "npc_dota_hero_sand_king";
                        Helper.HeroSpells();
                    }
                });
            }

            //Nature's Prophet Teleportation Start
            if (args.Name.Contains("furion_teleport") && args.ParticleEffect.Owner.Team != me.Team && MenuManager.Menu.Item("furion_teleportation").GetValue<bool>())
            {
                FurionFix = true;
                if (MenuManager.Menu.Item("furion_teleportation_msg").GetValue<bool>())
                {
                    MessageCreator.MessageEnemyCreator("furion", "furion_teleportation");
                }
                if (MenuManager.Menu.Item("furion_teleportation_sound").GetValue<bool>())
                {
                    Sound.PlaySound("furion_teleportation_" + Addition[GetLangId] + ".wav");
                }
                DelayAction.Add(50, () =>
                {
                    if (MenuManager.Menu.Item("furion_teleportation_minimap_start").GetValue<bool>())
                    {
                        MiniMapPosition = HUDInfo.WorldToMinimap(args.ParticleEffect.GetControlPoint(0));
                        HeroColor = ("npc_dota_hero_furion");                        
                        Helper.HeroSpells();
                        FurionFix = false;
                    }
                });
            }

            //Nature's Prophet Teleportation End
            if (args.Name.Contains("furion_teleport_end") && HeroChecker.Furion_IsHere && MenuManager.Menu.Item("furion_teleportation").GetValue<bool>())
            {
                DelayAction.Add(50, () =>
                {
                    if (MenuManager.Menu.Item("furion_teleportation_minimap_end").GetValue<bool>())
                    {
                        MiniMapPosition = HUDInfo.WorldToMinimap(args.ParticleEffect.GetControlPoint(1));
                        HeroColor = "npc_dota_hero_furion";
                        DrawingMiniMap.NamePositionFurion.Add(MiniMapPosition);
                        DrawingMiniMap.HeroNameFurion = ("Nature's Prophet");
                        DrawingMiniMap.HeroNamePosFurion = (int)(("Nature's Prophet").Length * 3.84f);
                        DrawingMiniMap.HeroNameColorFurion = Color.Red;
                        DrawingMiniMap.Remover2(MiniMapPosition);
                        Helper.HeroSpells();
                    }
                });
            }

            //Nature's Prophet Wrath of Nature
            if (args.Name.Contains("furion_wrath_of_nature") && args.ParticleEffect.Owner.Team != me.Team && MenuManager.Menu.Item("furion_wrath_of_nature").GetValue<bool>())
            {
                if (MenuManager.Menu.Item("furion_wrath_of_nature_msg").GetValue<bool>())
                {
                    MessageCreator.MessageEnemyCreator("furion", "furion_wrath_of_nature");
                }
                if (MenuManager.Menu.Item("furion_wrath_of_nature_sound").GetValue<bool>())
                {
                    Sound.PlaySound("furion_wrath_of_nature_" + Addition[GetLangId] + ".wav");
                }
                DelayAction.Add(50, () =>
                {
                    if (MenuManager.Menu.Item("furion_wrath_of_nature_minimap").GetValue<bool>())
                    {
                        MiniMapPosition = HUDInfo.WorldToMinimap(args.ParticleEffect.GetControlPoint(0));
                        HeroColor = "npc_dota_hero_furion";
                        Helper.HeroSpells();
                    }
                });
            }

            //Alchemist Unstable Concoction
            if (args.Name.Contains("alchemist_unstableconc") && args.ParticleEffect.Owner.Team != me.Team && MenuManager.Menu.Item("alchemist_unstable_concoction").GetValue<bool>())
            {
                if (MenuManager.Menu.Item("alchemist_unstable_concoction_msg").GetValue<bool>())
                {
                    MessageCreator.MessageEnemyCreator("alchemist", "alchemist_unstable_concoction");
                }
                if (MenuManager.Menu.Item("alchemist_unstable_concoction_sound").GetValue<bool>())
                {
                    Sound.PlaySound("unstable_concoction_" + Addition[GetLangId] + ".wav");
                }
                DelayAction.Add(50, () =>
                {
                    if (MenuManager.Menu.Item("alchemist_unstable_concoction_minimap").GetValue<bool>())
                    {
                        MiniMapPosition = HUDInfo.WorldToMinimap(args.ParticleEffect.GetControlPoint(0));
                        HeroColor = "npc_dota_hero_alchemist";
                        Helper.HeroSpells();
                    }
                });
            }

            //Bounty Hunter Shadow Walk
            if (args.Name.Contains("bounty_hunter_windwalk") && HeroChecker.Bounty_Hunter_IsHere && MenuManager.Menu.Item("bounty_hunter_wind_walk").GetValue<bool>())
            {
                if (MenuManager.Menu.Item("bounty_hunter_wind_walk_msg").GetValue<bool>())
                {
                    MessageCreator.MessageEnemyCreator("bounty_hunter", "bounty_hunter_wind_walk");
                }
                if (MenuManager.Menu.Item("bounty_hunter_wind_walk_sound").GetValue<bool>())
                {
                    Sound.PlaySound("bounty_hunter_wind_walk_" + Addition[GetLangId] + ".wav");
                }
                DelayAction.Add(50, () =>
                {
                    if (MenuManager.Menu.Item("bounty_hunter_wind_walk_minimap").GetValue<bool>())
                    {
                        MiniMapPosition = HUDInfo.WorldToMinimap(args.ParticleEffect.GetControlPoint(0));
                        HeroColor = "npc_dota_hero_bounty_hunter";
                        Helper.HeroSpells();
                    }
                });
            }

            //Clinkz Skeleton Walk
            if (args.Name.Contains("clinkz_windwalk") && HeroChecker.Clinkz_IsHere && MenuManager.Menu.Item("clinkz_wind_walk").GetValue<bool>())
            {
                if (MenuManager.Menu.Item("clinkz_wind_walk_msg").GetValue<bool>())
                {
                    MessageCreator.MessageEnemyCreator("clinkz", "clinkz_wind_walk");
                }
                if (MenuManager.Menu.Item("clinkz_wind_walk_sound").GetValue<bool>())
                {
                    Sound.PlaySound("clinkz_wind_walk_" + Addition[GetLangId] + ".wav");
                }
                DelayAction.Add(50, () =>
                {
                    if (MenuManager.Menu.Item("clinkz_wind_walk_minimap").GetValue<bool>())
                    {
                        MiniMapPosition = HUDInfo.WorldToMinimap(args.ParticleEffect.GetControlPoint(0));
                        HeroColor = "npc_dota_hero_clinkz";
                        Helper.HeroSpells();
                    }
                });
            }

            //Nyx Assassin Vendetta
            if (args.Name.Contains("nyx_assassin_vendetta_start") && HeroChecker.Nyx_Assassin_IsHere && MenuManager.Menu.Item("nyx_assassin_vendetta").GetValue<bool>())
            {
                if (MenuManager.Menu.Item("nyx_assassin_vendetta_msg").GetValue<bool>())
                {
                    MessageCreator.MessageEnemyCreator("nyx_assassin", "nyx_assassin_vendetta");
                }
                if (MenuManager.Menu.Item("nyx_assassin_vendetta_sound").GetValue<bool>())
                {
                    Sound.PlaySound("nyx_assassin_vendetta_" + Addition[GetLangId] + ".wav");
                }
                DelayAction.Add(50, () =>
                {
                    if (MenuManager.Menu.Item("nyx_assassin_vendetta_minimap").GetValue<bool>())
                    {
                        MiniMapPosition = HUDInfo.WorldToMinimap(args.ParticleEffect.GetControlPoint(0));
                        HeroColor = "npc_dota_hero_nyx_assassin";
                        Helper.HeroSpells();
                    }
                });
            }

            //Wisp Relocate Start
            if (args.Name.Contains("wisp_relocate_channel") && args.ParticleEffect.Owner.Team != me.Team && MenuManager.Menu.Item("wisp_relocate").GetValue<bool>())
            {
                if (MenuManager.Menu.Item("wisp_relocate_msg").GetValue<bool>())
                {
                    MessageCreator.MessageEnemyCreator("wisp", "wisp_relocate");
                }
                if (MenuManager.Menu.Item("wisp_relocate_sound").GetValue<bool>())
                {
                    Sound.PlaySound("wisp_relocate_" + Addition[GetLangId] + ".wav");
                }
                DelayAction.Add(50, () =>
                {
                    if (MenuManager.Menu.Item("wisp_relocate_minimap_start").GetValue<bool>())
                    {
                        MiniMapPosition = HUDInfo.WorldToMinimap(args.ParticleEffect.GetControlPoint(0));
                        HeroColor = "npc_dota_hero_wisp";
                        Helper.HeroSpells();
                    }
                });
            }

            //Wisp Relocate End
            if (args.Name.Contains("wisp_relocate_marker_endpoint") && HeroChecker.Wisp_IsHere && MenuManager.Menu.Item("wisp_relocate").GetValue<bool>())
            {
                DelayAction.Add(50, () =>
                {
                    if (MenuManager.Menu.Item("wisp_relocate_minimap_end").GetValue<bool>())
                    {
                        MiniMapPosition = HUDInfo.WorldToMinimap(args.ParticleEffect.GetControlPoint(0));
                        HeroColor = "npc_dota_hero_wisp";
                        Helper.HeroSpells();
                    }
                });
            }

            //Morphling Replicate
            if (args.Name.Contains("morphling_replicate") && HeroChecker.Morphling_IsHere && MenuManager.Menu.Item("morphling_replicate").GetValue<bool>())
            {
                if (MenuManager.Menu.Item("morphling_replicate_msg").GetValue<bool>())
                {
                    MessageCreator.MessageEnemyCreator("morphling", "morphling_replicate");
                }
                if (MenuManager.Menu.Item("morphling_replicate_sound").GetValue<bool>())
                {
                    Sound.PlaySound("morphling_replicate_" + Addition[GetLangId] + ".wav");
                }
                DelayAction.Add(50, () =>
                {
                    if (MenuManager.Menu.Item("morphling_replicate_minimap").GetValue<bool>())
                    {
                        MiniMapPosition = HUDInfo.WorldToMinimap(args.ParticleEffect.GetControlPoint(0));
                        HeroColor = "npc_dota_hero_wisp";
                        Helper.HeroSpells();
                    }
                });
            }

            //Troll Warlord Battle Trance
            if (args.Name.Contains("troll_warlord_battletrance_cast") && args.ParticleEffect.Owner.Team != me.Team && MenuManager.Menu.Item("troll_warlord_battle_trance").GetValue<bool>())
            {
                if (MenuManager.Menu.Item("troll_warlord_battle_trance_msg").GetValue<bool>())
                {
                    MessageCreator.MessageEnemyCreator("troll_warlord", "troll_warlord_battle_trance");
                }
                if (MenuManager.Menu.Item("troll_warlord_battle_trance_sound").GetValue<bool>())
                {
                    Sound.PlaySound("troll_warlord_battle_trance_" + Addition[GetLangId] + ".wav");
                }
                DelayAction.Add(50, () =>
                {
                    if (MenuManager.Menu.Item("troll_warlord_battle_trance_minimap").GetValue<bool>())
                    {
                        MiniMapPosition = HUDInfo.WorldToMinimap(args.ParticleEffect.GetControlPoint(0));
                        HeroColor = "npc_dota_hero_troll_warlord";
                        Helper.HeroSpells();
                    }
                });
            }

            //Ursa Enrage
            if (args.Name.Contains("ursa_enrage_buff") && args.ParticleEffect.Owner.Team != me.Team && MenuManager.Menu.Item("ursa_enrage").GetValue<bool>())
            {
                if (MenuManager.Menu.Item("ursa_enrage_msg").GetValue<bool>())
                {
                    MessageCreator.MessageEnemyCreator("ursa", "ursa_enrage");
                }
                if (MenuManager.Menu.Item("ursa_enrage_sound").GetValue<bool>())
                {
                    Sound.PlaySound("ursa_enrage_" + Addition[GetLangId] + ".wav");
                }
                DelayAction.Add(50, () =>
                {
                    if (MenuManager.Menu.Item("ursa_enrage_minimap").GetValue<bool>())
                    {
                        MiniMapPosition = HUDInfo.WorldToMinimap(args.ParticleEffect.GetControlPoint(0));
                        HeroColor = "npc_dota_hero_ursa";
                        Helper.HeroSpells();
                    }
                });
            }

            //Spirit Breaker Charge Start
            if (args.Name.Contains("spirit_breaker_charge_start") && args.ParticleEffect.Owner.Team != me.Team && MenuManager.Menu.Item("spirit_breaker_charge_of_darkness").GetValue<bool>())
            {
                DelayAction.Add(50, () =>
                {
                    if (MenuManager.Menu.Item("spirit_breaker_charge_of_darkness_minimap_start").GetValue<bool>())
                    {
                        MiniMapPosition = HUDInfo.WorldToMinimap(args.ParticleEffect.GetControlPoint(0));
                        HeroColor = "npc_dota_hero_spirit_breaker";
                        Helper.HeroSpells();
                    }
                });
            }

            //Monkey King Tree Dance
            if (args.Name.Contains("monkey_king_jump_trail") && args.ParticleEffect.Owner.Team != me.Team && MenuManager.Menu.Item("monkey_king_tree_dance").GetValue<bool>())
            {
                if (MenuManager.Menu.Item("monkey_king_tree_dance_msg").GetValue<bool>())
                {
                    MessageCreator.MessageEnemyCreator("monkey_king", "monkey_king_tree_dance");
                }
                if (MenuManager.Menu.Item("monkey_king_tree_dance_sound").GetValue<bool>())
                {
                    Sound.PlaySound("default_" + Addition[GetLangId] + ".wav");
                }
                DelayAction.Add(50, () =>
                {
                    if (MenuManager.Menu.Item("monkey_king_tree_dance_minimap").GetValue<bool>())
                    {
                        MiniMapPosition = HUDInfo.WorldToMinimap(args.ParticleEffect.GetControlPoint(1));
                        HeroColor = "npc_dota_hero_monkey_king";
                        Helper.HeroSpells();
                    }
                });
            }

            //Town Portall Scrol Teleport End          
            if (args.Name.Contains("teleport_end"))
            {
                DelayAction.Add(30, () =>
                {
                    TPID = args.ParticleEffect.GetControlPoint(6);
                    player = ObjectManager.GetPlayerById((uint)ColorList.FindIndex(x => x == new Vector3(TPID.X, TPID.Y, TPID.Z)));
                    try
                    {
                        Team = (player.Hero.Owner.Team != me.Team);
                    }
                    catch
                    {
                        return;
                    }

                    //TP Ally
                    if (!Team && MenuManager.Menu.Item("tpscroll_teleport_ally").GetValue<bool>())
                    {
                        MiniMapPosition = HUDInfo.WorldToMinimap(args.ParticleEffect.GetControlPoint(0));
                        Helper.TPTeleportEnd();
                        if (MenuManager.Menu.Item("tpscroll_teleport_ally_msg").GetValue<bool>())
                        {
                            MessageCreator.MessageItemCreator(player.Hero.Name.Substring(14), "tpscroll");
                        }
                        if (MenuManager.Menu.Item("tpscroll_teleport_ally_sound").GetValue<bool>())
                        {
                            Sound.PlaySound("default_" + Addition[GetLangId] + ".wav");
                        }                        
                    }

                    //TP Enemy                       
                    if (Team && MenuManager.Menu.Item("tpscroll_teleport_enemy").GetValue<bool>())
                    {
                        MiniMapPosition = HUDInfo.WorldToMinimap(args.ParticleEffect.GetControlPoint(0));
                        Helper.TPTeleportEnd();
                        if (MenuManager.Menu.Item("tpscroll_teleport_enemy_msg").GetValue<bool>())
                        {
                            MessageCreator.MessageItemCreator(player.Hero.Name.Substring(14), "tpscroll");
                        }
                        if (MenuManager.Menu.Item("tpscroll_teleport_enemy_sound").GetValue<bool>())
                        {
                            Sound.PlaySound("default_" + Addition[GetLangId] + ".wav");
                        }                        
                    }
                });
            }

            //Town Portall Scrol Teleport Start 
            if (args.Name.Contains("teleport_start") && MenuManager.Menu.Item("tpscroll_teleport_start").GetValue<bool>())
            {
                DelayAction.Add(50, () =>
                {
                    try
                    {
                        //TP Ally
                        if (!Team && MenuManager.Menu.Item("tpscroll_teleport_ally").GetValue<bool>())
                        {
                            MiniMapPosition = HUDInfo.WorldToMinimap(args.ParticleEffect.GetControlPoint(0));
                            Helper.TPTeleportStart();
                        }
                        //TP Enemy                       
                        if (Team && MenuManager.Menu.Item("tpscroll_teleport_enemy").GetValue<bool>())
                        {
                            MiniMapPosition = HUDInfo.WorldToMinimap(args.ParticleEffect.GetControlPoint(0));
                            Helper.TPTeleportStart();
                        }
                    }
                    catch
                    {
                        return;
                    }
                });
            }

            //Boots Of Travel Teleport Start == End               
            //TP Enemy 
            //===========================================================================================//  
                                                                                 
            //TP Start
            if (args.Name.Contains("teleport_start")                  
                && MenuManager.Menu.Item("bt_teleport_enemy").GetValue<bool>()          
                && !MenuManager.Menu.Item("bt_teleport_all").GetValue<bool>())
            {
                DelayAction.Add(60, () =>
                {                    
                    if (new Vector3(TPID.X, TPID.Y, TPID.Z) == (new Vector3(0, 0, 0)))
                    {
                        if (IgnorAllyTP)
                        {
                            MiniMapPosition = HUDInfo.WorldToMinimap(args.ParticleEffect.GetControlPoint(0));
                            Helper.BTTeleportStartEnemy();
                            if (MenuManager.Menu.Item("bt_teleport_enemy_msg").GetValue<bool>())
                            {
                                MessageCreator.MessageItemCreator("default2", "travel_boots");
                            }
                            if (MenuManager.Menu.Item("bt_teleport_enemy_sound").GetValue<bool>())
                            {
                                Sound.PlaySound("default_" + Addition[GetLangId] + ".wav");
                            }
                        }
                        DelayAction.Add(50, () =>
                        {
                            IgnorAllyTP = true;
                        });                       
                    }
                });            
            }                    

            //TP End                           
            if ((args.Name.Contains("teleport_end") && !FurionFix)              
                && MenuManager.Menu.Item("bt_teleport_enemy").GetValue<bool>()    
                && !MenuManager.Menu.Item("bt_teleport_all").GetValue<bool>())
            {
                DelayAction.Add(60, () =>
                {
                    if (new Vector3(TPID.X, TPID.Y, TPID.Z) == (new Vector3(0, 0, 0)))
                    {
                        if (IgnorAllyTP)
                        {
                            MiniMapPosition = HUDInfo.WorldToMinimap(args.ParticleEffect.GetControlPoint(0));
                            Helper.BTTeleportStartEnemy();
                        }
                    }
                });                                
            }

            //TP All
            //===========================================================================================//

            //TP Start
            if (args.Name.Contains("teleport_start") && MenuManager.Menu.Item("bt_teleport_all").GetValue<bool>())
            {
                DelayAction.Add(60, () =>
                {
                    if (new Vector3(TPID.X, TPID.Y, TPID.Z) == (new Vector3(0, 0, 0)))
                    {
                        if (IgnorAllyTP)
                        {
                            MiniMapPosition = HUDInfo.WorldToMinimap(args.ParticleEffect.GetControlPoint(0));
                            Helper.BTTeleportStartEnemy();
                        }
                        if (IgnorAllyTP && MenuManager.Menu.Item("bt_teleport_enemy_msg").GetValue<bool>())
                        {
                            MessageCreator.MessageItemCreator("default2", "travel_boots");
                        }
                        if (IgnorAllyTP && MenuManager.Menu.Item("bt_teleport_enemy_sound").GetValue<bool>())
                        {
                            Sound.PlaySound("default_" + Addition[GetLangId] + ".wav");
                        }
                        if (!IgnorAllyTP)
                        {
                            MiniMapPosition = HUDInfo.WorldToMinimap(args.ParticleEffect.GetControlPoint(0));
                            Helper.BTTeleportStartAlly();
                        }
                    }
                    DelayAction.Add(50, () =>
                    {
                        IgnorAllyTP = true;
                    });
                });                             
            }

            //TP End
            if (args.Name.Contains("teleport_end") && MenuManager.Menu.Item("bt_teleport_all").GetValue<bool>())
            {
                DelayAction.Add(60, () =>
                {
                    if (new Vector3(TPID.X, TPID.Y, TPID.Z) == (new Vector3(0, 0, 0)))
                    {

                        if (IgnorAllyTP)
                        {
                            MiniMapPosition = HUDInfo.WorldToMinimap(args.ParticleEffect.GetControlPoint(0));
                            Helper.BTTeleportStartEnemy();
                        }
                        if (!IgnorAllyTP)
                        {
                            MiniMapPosition = HUDInfo.WorldToMinimap(args.ParticleEffect.GetControlPoint(0));
                            Helper.BTTeleportStartAlly();
                        }
                    }
                });
            }
        }                                   
        public static readonly List<Vector3> ColorList = new List<Vector3>()
        {
            new Vector3(0.2f, 0.4588236f, 1),
            new Vector3(0.4f, 1, 0.7490196f),
            new Vector3(0.7490196f, 0, 0.7490196f),
            new Vector3(0.9529412f, 0.9411765f, 0.04313726f),
            new Vector3(1, 0.4196079f, 0),
            new Vector3(0.9960785f, 0.5254902f, 0.7607844f),
            new Vector3(0.6313726f, 0.7058824f, 0.2784314f),
            new Vector3(0.3960785f, 0.8509805f, 0.9686275f),
            new Vector3(0, 0.5137255f, 0.1294118f),
            new Vector3(0.6431373f, 0.4117647f, 0)
        };

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

            //Check Rune
            if (((Math.Round(Game.GameTime) + MenuManager.Menu.Item("check_rune_sec").GetValue<Slider>().Value) % 120) == 0 && MenuManager.Menu.Item("check_rune").GetValue<bool>() && Utils.SleepCheck("check_rune"))
            {
                if (MenuManager.Menu.Item("check_rune_msg").GetValue<bool>())
                {
                    MessageCreator.MessageCheckRuneCreator(null);
                }
                if (MenuManager.Menu.Item("check_rune_sound").GetValue<bool>())
                {
                    Sound.PlaySound("check_rune_" + Addition[GetLangId] + ".wav");
                }                                
                Utils.Sleep(5000, "check_rune");
            }

            //Hand of Midas
            var Midas = me.Hero.FindItem("item_hand_of_midas");
            if (Midas != null && Math.Round(Midas.Cooldown) == MenuManager.Menu.Item("use_midas_sec").GetValue<Slider>().Value && MenuManager.Menu.Item("use_midas").GetValue<bool>() && Utils.SleepCheck("use_midas"))
            {
                if (MenuManager.Menu.Item("use_midas_msg").GetValue<bool>())
                {
                    MessageCreator.MessageUseMidasCreator(null);
                }
                if (MenuManager.Menu.Item("use_midas_sound").GetValue<bool>())
                {
                    Sound.PlaySound("use_midas_" + Addition[GetLangId] + ".wav");
                }                                
                Utils.Sleep(5000, "use_midas");
            }

            //Roshan MB Alive
            if (Roshan_Dead && MenuManager.Menu.Item("roshan").GetValue<bool>())
            {
                if (--Roshan_Respawn_Min_Time + 5 == 0 && Utils.SleepCheck("roshan_mb_alive"))
                {
                    if (MenuManager.Menu.Item("roshan_msg").GetValue<bool>())
                    {
                        MessageCreator.MessageRoshanMBAliveCreator(null);
                    }
                    if (MenuManager.Menu.Item("roshan_sound").GetValue<bool>())
                    {
                        Sound.PlaySound("roshan_mb_alive_" + Addition[GetLangId] + ".wav");
                    }                                        
                    Utils.Sleep(5000, "roshan_mb_alive");
                }

            //Roshan Alive            
                if (--Roshan_Respawn_Max_Time + 5 == 0 && Utils.SleepCheck("roshan_alive"))
                {
                    if (MenuManager.Menu.Item("roshan_msg").GetValue<bool>())
                    {
                        MessageCreator.MessageRoshanAliveCreator(null);
                    }
                    if (MenuManager.Menu.Item("roshan_sound").GetValue<bool>())
                    {
                        Sound.PlaySound("roshan_alive_" + Addition[GetLangId] + ".wav");
                    }                                       
                    Utils.Sleep(5000, "roshan_alive");
                    Roshan_Dead = false;
                }
            }
        }                
    }
}
