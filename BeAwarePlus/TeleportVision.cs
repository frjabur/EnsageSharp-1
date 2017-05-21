using Ensage;
using Ensage.Common;
using Ensage.Common.Extensions;
using SharpDX;
using System.Collections.Generic;

namespace BeAwarePlus
{
    public static class TeleportVision
    {
        private static Player player;
        private static Vector3 BTID;
        private static bool Team;
        public static void Init()
        {
            Entity.OnParticleEffectAdded += OnParticleEvent;
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
        public static void OnParticleEvent(Entity hero, ParticleEffectAddedEventArgs args)
        {
            //Town Portall Scrol Teleport End          
            if (args.Name.Contains("teleport_end"))
            {
                DelayAction.Add(30, () =>
                {                   
                    var TPID = args.ParticleEffect.GetControlPoint(6);                    
                    player = ObjectManager.GetPlayerById((uint)ColorList.FindIndex(x => x == new Vector3(TPID.X, TPID.Y, TPID.Z)));

                    if (player == null) return;
                    Team = (player.Hero.Owner.Team != BeAwarePlus.me.Team);
                          
                    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////                                       
                    if (0 == player.Id)
                    {
                        //TP Ally
                        if (!Team && MenuManager.Menu.Item("tpscroll_teleport_ally").GetValue<bool>())
                        {
                            var MiniMapPosition = HUDInfo.WorldToMinimap(args.ParticleEffect.GetControlPoint(0));
                            if (MenuManager.Menu.Item("tpscroll_teleport_ally_msg").GetValue<bool>())
                            {
                                MessageCreator.MessageItemCreator(player.Hero.Name.Substring(14), "tpscroll");
                            }
                            if (MenuManager.Menu.Item("tpscroll_teleport_ally_sound").GetValue<bool>())
                            {
                                Sound.PlaySound("default_" + BeAwarePlus.Addition[BeAwarePlus.GetLangId] + ".wav");
                            }
                            if (MenuManager.Menu.Item("tpscroll_teleport_hero_name").GetValue<bool>())
                            {
                                DrawingMiniMap.NamePosition_0.Add(MiniMapPosition);
                            }
                            DrawingMiniMap.Position_0.Add(MiniMapPosition);
                            DrawingMiniMap.HeroName_0 = (player.Hero.GetRealName());
                            DrawingMiniMap.HeroNamePos_0 = (int)(player.Hero.GetRealName().Length * 3.84f);
                            DrawingMiniMap.HeroNameColor_0 = (Color)new Vector3(0.2f, 0.4588236f, 1);
                            DrawingMiniMap.HeroColor_0 = (Color)new Vector3(0.2f, 0.4588236f, 1);
                            DrawingMiniMap.Remover(MiniMapPosition);
                        }

                        //TP Enemy                       
                        if (Team && MenuManager.Menu.Item("tpscroll_teleport_enemy").GetValue<bool>())
                        {
                            var MiniMapPosition = HUDInfo.WorldToMinimap(args.ParticleEffect.GetControlPoint(0));                            
                            if (MenuManager.Menu.Item("tpscroll_teleport_enemy_msg").GetValue<bool>())
                            {
                                MessageCreator.MessageItemCreator(player.Hero.Name.Substring(14), "tpscroll");
                            }
                            if (MenuManager.Menu.Item("tpscroll_teleport_enemy_sound").GetValue<bool>())
                            {
                                Sound.PlaySound("default_" + BeAwarePlus.Addition[BeAwarePlus.GetLangId] + ".wav");
                            }
                            if (MenuManager.Menu.Item("tpscroll_teleport_hero_name").GetValue<bool>())
                            {
                                DrawingMiniMap.NamePosition_0.Add(MiniMapPosition);
                            }
                            DrawingMiniMap.Position_0.Add(MiniMapPosition);
                            DrawingMiniMap.HeroName_0 = (player.Hero.GetRealName());
                            DrawingMiniMap.HeroNamePos_0 = (int)(player.Hero.GetRealName().Length * 3.84f);
                            DrawingMiniMap.HeroNameColor_0 = (Color)new Vector3(0.2f, 0.4588236f, 1);
                            DrawingMiniMap.HeroColor_0 = (Color)new Vector3(0.2f, 0.4588236f, 1);
                            DrawingMiniMap.Remover(MiniMapPosition);
                        }
                    }
                    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    if (1 == player.Id)
                    {
                        //TP Ally
                        if (!Team && MenuManager.Menu.Item("tpscroll_teleport_ally").GetValue<bool>())
                        {
                            var MiniMapPosition = HUDInfo.WorldToMinimap(args.ParticleEffect.GetControlPoint(0));
                            if (MenuManager.Menu.Item("tpscroll_teleport_ally_msg").GetValue<bool>())
                            {
                                MessageCreator.MessageItemCreator(player.Hero.Name.Substring(14), "tpscroll");
                            }
                            if (MenuManager.Menu.Item("tpscroll_teleport_ally_sound").GetValue<bool>())
                            {
                                Sound.PlaySound("default_" + BeAwarePlus.Addition[BeAwarePlus.GetLangId] + ".wav");
                            }
                            if (MenuManager.Menu.Item("tpscroll_teleport_hero_name").GetValue<bool>())
                            {
                                DrawingMiniMap.NamePosition_1.Add(MiniMapPosition);
                            }
                            DrawingMiniMap.Position_1.Add(MiniMapPosition);
                            DrawingMiniMap.HeroName_1 = (player.Hero.GetRealName());
                            DrawingMiniMap.HeroNamePos_1 = (int)(player.Hero.GetRealName().Length * 3.84f);
                            DrawingMiniMap.HeroNameColor_1 = (Color)new Vector3(0.4f, 1, 0.7490196f);
                            DrawingMiniMap.HeroColor_1 = (Color)new Vector3(0.4f, 1, 0.7490196f);
                            DrawingMiniMap.Remover(MiniMapPosition);
                        }

                        //TP Enemy                       
                        if (Team && MenuManager.Menu.Item("tpscroll_teleport_enemy").GetValue<bool>())
                        {
                            var MiniMapPosition = HUDInfo.WorldToMinimap(args.ParticleEffect.GetControlPoint(0));                           
                            if (MenuManager.Menu.Item("tpscroll_teleport_enemy_msg").GetValue<bool>())
                            {
                                MessageCreator.MessageItemCreator(player.Hero.Name.Substring(14), "tpscroll");
                            }
                            if (MenuManager.Menu.Item("tpscroll_teleport_enemy_sound").GetValue<bool>())
                            {
                                Sound.PlaySound("default_" + BeAwarePlus.Addition[BeAwarePlus.GetLangId] + ".wav");
                            }
                            if (MenuManager.Menu.Item("tpscroll_teleport_hero_name").GetValue<bool>())
                            {
                                DrawingMiniMap.NamePosition_1.Add(MiniMapPosition);
                            }
                            DrawingMiniMap.Position_1.Add(MiniMapPosition);
                            DrawingMiniMap.HeroName_1 = (player.Hero.GetRealName());
                            DrawingMiniMap.HeroNamePos_1 = (int)(player.Hero.GetRealName().Length * 3.84f);
                            DrawingMiniMap.HeroNameColor_1 = (Color)new Vector3(0.4f, 1, 0.7490196f);
                            DrawingMiniMap.HeroColor_1 = (Color)new Vector3(0.4f, 1, 0.7490196f);
                            DrawingMiniMap.Remover(MiniMapPosition);
                        }
                    }
                    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    if (2 == player.Id)
                    {
                        //TP Ally
                        if (!Team && MenuManager.Menu.Item("tpscroll_teleport_ally").GetValue<bool>())
                        {
                            var MiniMapPosition = HUDInfo.WorldToMinimap(args.ParticleEffect.GetControlPoint(0));
                            if (MenuManager.Menu.Item("tpscroll_teleport_ally_msg").GetValue<bool>())
                            {
                                MessageCreator.MessageItemCreator(player.Hero.Name.Substring(14), "tpscroll");
                            }
                            if (MenuManager.Menu.Item("tpscroll_teleport_ally_sound").GetValue<bool>())
                            {
                                Sound.PlaySound("default_" + BeAwarePlus.Addition[BeAwarePlus.GetLangId] + ".wav");
                            }
                            if (MenuManager.Menu.Item("tpscroll_teleport_hero_name").GetValue<bool>())
                            {
                                DrawingMiniMap.NamePosition_2.Add(MiniMapPosition);
                            }
                            DrawingMiniMap.Position_2.Add(MiniMapPosition);
                            DrawingMiniMap.HeroName_2 = (player.Hero.GetRealName());
                            DrawingMiniMap.HeroNamePos_2 = (int)(player.Hero.GetRealName().Length * 3.84f);
                            DrawingMiniMap.HeroNameColor_2 = (Color)new Vector3(0.7490196f, 0, 0.7490196f);
                            DrawingMiniMap.HeroColor_2 = (Color)new Vector3(0.7490196f, 0, 0.7490196f);
                            DrawingMiniMap.Remover(MiniMapPosition);
                        }

                        //TP Enemy                       
                        if (Team && MenuManager.Menu.Item("tpscroll_teleport_enemy").GetValue<bool>())
                        {
                            var MiniMapPosition = HUDInfo.WorldToMinimap(args.ParticleEffect.GetControlPoint(0));
                            if (MenuManager.Menu.Item("tpscroll_teleport_enemy_msg").GetValue<bool>())
                            {
                                MessageCreator.MessageItemCreator(player.Hero.Name.Substring(14), "tpscroll");
                            }
                            if (MenuManager.Menu.Item("tpscroll_teleport_enemy_sound").GetValue<bool>())
                            {
                                Sound.PlaySound("default_" + BeAwarePlus.Addition[BeAwarePlus.GetLangId] + ".wav");
                            }
                            if (MenuManager.Menu.Item("tpscroll_teleport_hero_name").GetValue<bool>())
                            {
                                DrawingMiniMap.NamePosition_2.Add(MiniMapPosition);
                            }
                            DrawingMiniMap.Position_2.Add(MiniMapPosition);
                            DrawingMiniMap.HeroName_2 = (player.Hero.GetRealName());
                            DrawingMiniMap.HeroNamePos_2 = (int)(player.Hero.GetRealName().Length * 3.84f);
                            DrawingMiniMap.HeroNameColor_2 = (Color)new Vector3(0.7490196f, 0, 0.7490196f);
                            DrawingMiniMap.HeroColor_2 = (Color)new Vector3(0.7490196f, 0, 0.7490196f);
                            DrawingMiniMap.Remover(MiniMapPosition);
                        }
                    }
                    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    if (3 == player.Id)
                    {
                        //TP Ally
                        if (!Team && MenuManager.Menu.Item("tpscroll_teleport_ally").GetValue<bool>())
                        {
                            var MiniMapPosition = HUDInfo.WorldToMinimap(args.ParticleEffect.GetControlPoint(0));
                            if (MenuManager.Menu.Item("tpscroll_teleport_ally_msg").GetValue<bool>())
                            {
                                MessageCreator.MessageItemCreator(player.Hero.Name.Substring(14), "tpscroll");
                            }
                            if (MenuManager.Menu.Item("tpscroll_teleport_ally_sound").GetValue<bool>())
                            {
                                Sound.PlaySound("default_" + BeAwarePlus.Addition[BeAwarePlus.GetLangId] + ".wav");
                            }
                            if (MenuManager.Menu.Item("tpscroll_teleport_hero_name").GetValue<bool>())
                            {
                                DrawingMiniMap.NamePosition_3.Add(MiniMapPosition);
                            }
                            DrawingMiniMap.Position_3.Add(MiniMapPosition);
                            DrawingMiniMap.HeroName_3 = (player.Hero.GetRealName());
                            DrawingMiniMap.HeroNamePos_3 = (int)(player.Hero.GetRealName().Length * 3.84f);
                            DrawingMiniMap.HeroNameColor_3 = (Color)new Vector3(0.9529412f, 0.9411765f, 0.04313726f);
                            DrawingMiniMap.HeroColor_3 = (Color)new Vector3(0.9529412f, 0.9411765f, 0.04313726f);
                            DrawingMiniMap.Remover(MiniMapPosition);
                        }

                        //TP Enemy                       
                        if (Team && MenuManager.Menu.Item("tpscroll_teleport_enemy").GetValue<bool>())
                        {
                            var MiniMapPosition = HUDInfo.WorldToMinimap(args.ParticleEffect.GetControlPoint(0));
                            if (MenuManager.Menu.Item("tpscroll_teleport_enemy_msg").GetValue<bool>())
                            {
                                MessageCreator.MessageItemCreator(player.Hero.Name.Substring(14), "tpscroll");
                            }
                            if (MenuManager.Menu.Item("tpscroll_teleport_enemy_sound").GetValue<bool>())
                            {
                                Sound.PlaySound("default_" + BeAwarePlus.Addition[BeAwarePlus.GetLangId] + ".wav");
                            }
                            if (MenuManager.Menu.Item("tpscroll_teleport_hero_name").GetValue<bool>())
                            {
                                DrawingMiniMap.NamePosition_3.Add(MiniMapPosition);
                            }
                            DrawingMiniMap.Position_3.Add(MiniMapPosition);
                            DrawingMiniMap.HeroName_3 = (player.Hero.GetRealName());
                            DrawingMiniMap.HeroNamePos_3 = (int)(player.Hero.GetRealName().Length * 3.84f);
                            DrawingMiniMap.HeroNameColor_3 = (Color)new Vector3(0.9529412f, 0.9411765f, 0.04313726f);
                            DrawingMiniMap.HeroColor_3 = (Color)new Vector3(0.9529412f, 0.9411765f, 0.04313726f);
                            DrawingMiniMap.Remover(MiniMapPosition);
                        }
                    }
                    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    if (4 == player.Id)
                    {
                        //TP Ally
                        if (!Team && MenuManager.Menu.Item("tpscroll_teleport_ally").GetValue<bool>())
                        {
                            var MiniMapPosition = HUDInfo.WorldToMinimap(args.ParticleEffect.GetControlPoint(0));
                            if (MenuManager.Menu.Item("tpscroll_teleport_ally_msg").GetValue<bool>())
                            {
                                MessageCreator.MessageItemCreator(player.Hero.Name.Substring(14), "tpscroll");
                            }
                            if (MenuManager.Menu.Item("tpscroll_teleport_ally_sound").GetValue<bool>())
                            {
                                Sound.PlaySound("default_" + BeAwarePlus.Addition[BeAwarePlus.GetLangId] + ".wav");
                            }
                            if (MenuManager.Menu.Item("tpscroll_teleport_hero_name").GetValue<bool>())
                            {
                                DrawingMiniMap.NamePosition_4.Add(MiniMapPosition);
                            }
                            DrawingMiniMap.Position_4.Add(MiniMapPosition);
                            DrawingMiniMap.HeroName_4 = (player.Hero.GetRealName());
                            DrawingMiniMap.HeroNamePos_4 = (int)(player.Hero.GetRealName().Length * 3.84f);
                            DrawingMiniMap.HeroNameColor_4 = (Color)new Vector3(1, 0.4196079f, 0);
                            DrawingMiniMap.HeroColor_4 = (Color)new Vector3(1, 0.4196079f, 0);
                            DrawingMiniMap.Remover(MiniMapPosition);
                        }

                        //TP Enemy                       
                        if (Team && MenuManager.Menu.Item("tpscroll_teleport_enemy").GetValue<bool>())
                        {
                            var MiniMapPosition = HUDInfo.WorldToMinimap(args.ParticleEffect.GetControlPoint(0));
                            if (MenuManager.Menu.Item("tpscroll_teleport_enemy_msg").GetValue<bool>())
                            {
                                MessageCreator.MessageItemCreator(player.Hero.Name.Substring(14), "tpscroll");
                            }
                            if (MenuManager.Menu.Item("tpscroll_teleport_enemy_sound").GetValue<bool>())
                            {
                                Sound.PlaySound("default_" + BeAwarePlus.Addition[BeAwarePlus.GetLangId] + ".wav");
                            }
                            if (MenuManager.Menu.Item("tpscroll_teleport_hero_name").GetValue<bool>())
                            {
                                DrawingMiniMap.NamePosition_4.Add(MiniMapPosition);
                            }
                            DrawingMiniMap.Position_4.Add(MiniMapPosition);
                            DrawingMiniMap.HeroName_4 = (player.Hero.GetRealName());
                            DrawingMiniMap.HeroNamePos_4 = (int)(player.Hero.GetRealName().Length * 3.84f);
                            DrawingMiniMap.HeroNameColor_4 = (Color)new Vector3(1, 0.4196079f, 0);
                            DrawingMiniMap.HeroColor_4 = (Color)new Vector3(1, 0.4196079f, 0);
                            DrawingMiniMap.Remover(MiniMapPosition);
                        }
                    }
                    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    if (5 == player.Id)
                    {
                        //TP Ally
                        if (!Team && MenuManager.Menu.Item("tpscroll_teleport_ally").GetValue<bool>())
                        {
                            var MiniMapPosition = HUDInfo.WorldToMinimap(args.ParticleEffect.GetControlPoint(0));
                            if (MenuManager.Menu.Item("tpscroll_teleport_ally_msg").GetValue<bool>())
                            {
                                MessageCreator.MessageItemCreator(player.Hero.Name.Substring(14), "tpscroll");
                            }
                            if (MenuManager.Menu.Item("tpscroll_teleport_ally_sound").GetValue<bool>())
                            {
                                Sound.PlaySound("default_" + BeAwarePlus.Addition[BeAwarePlus.GetLangId] + ".wav");
                            }
                            if (MenuManager.Menu.Item("tpscroll_teleport_hero_name").GetValue<bool>())
                            {
                                DrawingMiniMap.NamePosition_5.Add(MiniMapPosition);
                            }
                            DrawingMiniMap.Position_5.Add(MiniMapPosition);
                            DrawingMiniMap.HeroName_5 = (player.Hero.GetRealName());
                            DrawingMiniMap.HeroNamePos_5 = (int)(player.Hero.GetRealName().Length * 3.84f);
                            DrawingMiniMap.HeroNameColor_5 = (Color)new Vector3(0.9960785f, 0.5254902f, 0.7607844f);
                            DrawingMiniMap.HeroColor_5 = (Color)new Vector3(0.9960785f, 0.5254902f, 0.7607844f);
                            DrawingMiniMap.Remover(MiniMapPosition);
                        }

                        //TP Enemy                       
                        if (Team && MenuManager.Menu.Item("tpscroll_teleport_enemy").GetValue<bool>())
                        {
                            var MiniMapPosition = HUDInfo.WorldToMinimap(args.ParticleEffect.GetControlPoint(0));
                            if (MenuManager.Menu.Item("tpscroll_teleport_enemy_msg").GetValue<bool>())
                            {
                                MessageCreator.MessageItemCreator(player.Hero.Name.Substring(14), "tpscroll");
                            }
                            if (MenuManager.Menu.Item("tpscroll_teleport_enemy_sound").GetValue<bool>())
                            {
                                Sound.PlaySound("default_" + BeAwarePlus.Addition[BeAwarePlus.GetLangId] + ".wav");
                            }
                            if (MenuManager.Menu.Item("tpscroll_teleport_hero_name").GetValue<bool>())
                            {
                                DrawingMiniMap.NamePosition_5.Add(MiniMapPosition);
                            }
                            DrawingMiniMap.Position_5.Add(MiniMapPosition);
                            DrawingMiniMap.HeroName_5 = (player.Hero.GetRealName());
                            DrawingMiniMap.HeroNamePos_5 = (int)(player.Hero.GetRealName().Length * 3.84f);
                            DrawingMiniMap.HeroNameColor_5 = (Color)new Vector3(0.9960785f, 0.5254902f, 0.7607844f);
                            DrawingMiniMap.HeroColor_5 = (Color)new Vector3(0.9960785f, 0.5254902f, 0.7607844f);
                            DrawingMiniMap.Remover(MiniMapPosition);
                        }
                    }
                    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    if (6 == player.Id)
                    {
                        //TP Ally
                        if (!Team && MenuManager.Menu.Item("tpscroll_teleport_ally").GetValue<bool>())
                        {
                            var MiniMapPosition = HUDInfo.WorldToMinimap(args.ParticleEffect.GetControlPoint(0));
                            if (MenuManager.Menu.Item("tpscroll_teleport_ally_msg").GetValue<bool>())
                            {
                                MessageCreator.MessageItemCreator(player.Hero.Name.Substring(14), "tpscroll");
                            }
                            if (MenuManager.Menu.Item("tpscroll_teleport_ally_sound").GetValue<bool>())
                            {
                                Sound.PlaySound("default_" + BeAwarePlus.Addition[BeAwarePlus.GetLangId] + ".wav");
                            }
                            if (MenuManager.Menu.Item("tpscroll_teleport_hero_name").GetValue<bool>())
                            {
                                DrawingMiniMap.NamePosition_6.Add(MiniMapPosition);
                            }
                            DrawingMiniMap.Position_6.Add(MiniMapPosition);
                            DrawingMiniMap.HeroName_6 = (player.Hero.GetRealName());
                            DrawingMiniMap.HeroNamePos_6 = (int)(player.Hero.GetRealName().Length * 3.84f);
                            DrawingMiniMap.HeroNameColor_6 = (Color)new Vector3(0.6313726f, 0.7058824f, 0.2784314f);
                            DrawingMiniMap.HeroColor_6 = (Color)new Vector3(0.6313726f, 0.7058824f, 0.2784314f);
                            DrawingMiniMap.Remover(MiniMapPosition);
                        }

                        //TP Enemy                       
                        if (Team && MenuManager.Menu.Item("tpscroll_teleport_enemy").GetValue<bool>())
                        {
                            var MiniMapPosition = HUDInfo.WorldToMinimap(args.ParticleEffect.GetControlPoint(0));
                            if (MenuManager.Menu.Item("tpscroll_teleport_enemy_msg").GetValue<bool>())
                            {
                                MessageCreator.MessageItemCreator(player.Hero.Name.Substring(14), "tpscroll");
                            }
                            if (MenuManager.Menu.Item("tpscroll_teleport_enemy_sound").GetValue<bool>())
                            {
                                Sound.PlaySound("default_" + BeAwarePlus.Addition[BeAwarePlus.GetLangId] + ".wav");
                            }
                            if (MenuManager.Menu.Item("tpscroll_teleport_hero_name").GetValue<bool>())
                            {
                                DrawingMiniMap.NamePosition_6.Add(MiniMapPosition);
                            }
                            DrawingMiniMap.Position_6.Add(MiniMapPosition);
                            DrawingMiniMap.HeroName_6 = (player.Hero.GetRealName());
                            DrawingMiniMap.HeroNamePos_6 = (int)(player.Hero.GetRealName().Length * 3.84f);
                            DrawingMiniMap.HeroNameColor_6 = (Color)new Vector3(0.6313726f, 0.7058824f, 0.2784314f);
                            DrawingMiniMap.HeroColor_6 = (Color)new Vector3(0.6313726f, 0.7058824f, 0.2784314f);
                            DrawingMiniMap.Remover(MiniMapPosition);
                        }
                    }
                    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    if (7 == player.Id)
                    {
                        //TP Ally
                        if (!Team && MenuManager.Menu.Item("tpscroll_teleport_ally").GetValue<bool>())
                        {
                            var MiniMapPosition = HUDInfo.WorldToMinimap(args.ParticleEffect.GetControlPoint(0));
                            if (MenuManager.Menu.Item("tpscroll_teleport_ally_msg").GetValue<bool>())
                            {
                                MessageCreator.MessageItemCreator(player.Hero.Name.Substring(14), "tpscroll");
                            }
                            if (MenuManager.Menu.Item("tpscroll_teleport_ally_sound").GetValue<bool>())
                            {
                                Sound.PlaySound("default_" + BeAwarePlus.Addition[BeAwarePlus.GetLangId] + ".wav");
                            }
                            if (MenuManager.Menu.Item("tpscroll_teleport_hero_name").GetValue<bool>())
                            {
                                DrawingMiniMap.NamePosition_7.Add(MiniMapPosition);
                            }
                            DrawingMiniMap.Position_7.Add(MiniMapPosition);
                            DrawingMiniMap.HeroName_7 = (player.Hero.GetRealName());
                            DrawingMiniMap.HeroNamePos_7 = (int)(player.Hero.GetRealName().Length * 3.84f);
                            DrawingMiniMap.HeroNameColor_7 = (Color)new Vector3(0.3960785f, 0.8509805f, 0.9686275f);
                            DrawingMiniMap.HeroColor_7 = (Color)new Vector3(0.3960785f, 0.8509805f, 0.9686275f);
                            DrawingMiniMap.Remover(MiniMapPosition);
                        }

                        //TP Enemy                       
                        if (Team && MenuManager.Menu.Item("tpscroll_teleport_enemy").GetValue<bool>())
                        {
                            var MiniMapPosition = HUDInfo.WorldToMinimap(args.ParticleEffect.GetControlPoint(0));
                            if (MenuManager.Menu.Item("tpscroll_teleport_enemy_msg").GetValue<bool>())
                            {
                                MessageCreator.MessageItemCreator(player.Hero.Name.Substring(14), "tpscroll");
                            }
                            if (MenuManager.Menu.Item("tpscroll_teleport_enemy_sound").GetValue<bool>())
                            {
                                Sound.PlaySound("default_" + BeAwarePlus.Addition[BeAwarePlus.GetLangId] + ".wav");
                            }
                            if (MenuManager.Menu.Item("tpscroll_teleport_hero_name").GetValue<bool>())
                            {
                                DrawingMiniMap.NamePosition_7.Add(MiniMapPosition);
                            }
                            DrawingMiniMap.Position_7.Add(MiniMapPosition);
                            DrawingMiniMap.HeroName_7 = (player.Hero.GetRealName());
                            DrawingMiniMap.HeroNamePos_7 = (int)(player.Hero.GetRealName().Length * 3.84f);
                            DrawingMiniMap.HeroNameColor_7 = (Color)new Vector3(0.3960785f, 0.8509805f, 0.9686275f);
                            DrawingMiniMap.HeroColor_7 = (Color)new Vector3(0.3960785f, 0.8509805f, 0.9686275f);
                            DrawingMiniMap.Remover(MiniMapPosition);
                        }
                    }
                    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    if (8 == player.Id)
                    {
                        //TP Ally
                        if (!Team && MenuManager.Menu.Item("tpscroll_teleport_ally").GetValue<bool>())
                        {
                            var MiniMapPosition = HUDInfo.WorldToMinimap(args.ParticleEffect.GetControlPoint(0));
                            if (MenuManager.Menu.Item("tpscroll_teleport_ally_msg").GetValue<bool>())
                            {
                                MessageCreator.MessageItemCreator(player.Hero.Name.Substring(14), "tpscroll");
                            }
                            if (MenuManager.Menu.Item("tpscroll_teleport_ally_sound").GetValue<bool>())
                            {
                                Sound.PlaySound("default_" + BeAwarePlus.Addition[BeAwarePlus.GetLangId] + ".wav");
                            }
                            if (MenuManager.Menu.Item("tpscroll_teleport_hero_name").GetValue<bool>())
                            {
                                DrawingMiniMap.NamePosition_8.Add(MiniMapPosition);
                            }
                            DrawingMiniMap.Position_8.Add(MiniMapPosition);
                            DrawingMiniMap.HeroName_8 = (player.Hero.GetRealName());
                            DrawingMiniMap.HeroNamePos_8 = (int)(player.Hero.GetRealName().Length * 3.84f);
                            DrawingMiniMap.HeroNameColor_8 = (Color)new Vector3(0, 0.5137255f, 0.1294118f);
                            DrawingMiniMap.HeroColor_8 = (Color)new Vector3(0, 0.5137255f, 0.1294118f);
                            DrawingMiniMap.Remover(MiniMapPosition);
                        }

                        //TP Enemy                       
                        if (Team && MenuManager.Menu.Item("tpscroll_teleport_enemy").GetValue<bool>())
                        {
                            var MiniMapPosition = HUDInfo.WorldToMinimap(args.ParticleEffect.GetControlPoint(0));
                            if (MenuManager.Menu.Item("tpscroll_teleport_enemy_msg").GetValue<bool>())
                            {
                                MessageCreator.MessageItemCreator(player.Hero.Name.Substring(14), "tpscroll");
                            }
                            if (MenuManager.Menu.Item("tpscroll_teleport_enemy_sound").GetValue<bool>())
                            {
                                Sound.PlaySound("default_" + BeAwarePlus.Addition[BeAwarePlus.GetLangId] + ".wav");
                            }
                            if (MenuManager.Menu.Item("tpscroll_teleport_hero_name").GetValue<bool>())
                            {
                                DrawingMiniMap.NamePosition_8.Add(MiniMapPosition);
                            }
                            DrawingMiniMap.Position_8.Add(MiniMapPosition);
                            DrawingMiniMap.HeroName_8 = (player.Hero.GetRealName());
                            DrawingMiniMap.HeroNamePos_8 = (int)(player.Hero.GetRealName().Length * 3.84f);
                            DrawingMiniMap.HeroNameColor_8 = (Color)new Vector3(0, 0.5137255f, 0.1294118f);
                            DrawingMiniMap.HeroColor_8 = (Color)new Vector3(0, 0.5137255f, 0.1294118f);
                            DrawingMiniMap.Remover(MiniMapPosition);
                        }
                    }
                    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    if (9 == player.Id)
                    {
                        //TP Ally
                        if (!Team && MenuManager.Menu.Item("tpscroll_teleport_ally").GetValue<bool>())
                        {
                            var MiniMapPosition = HUDInfo.WorldToMinimap(args.ParticleEffect.GetControlPoint(0));
                            if (MenuManager.Menu.Item("tpscroll_teleport_ally_msg").GetValue<bool>())
                            {
                                MessageCreator.MessageItemCreator(player.Hero.Name.Substring(14), "tpscroll");
                            }
                            if (MenuManager.Menu.Item("tpscroll_teleport_ally_sound").GetValue<bool>())
                            {
                                Sound.PlaySound("default_" + BeAwarePlus.Addition[BeAwarePlus.GetLangId] + ".wav");
                            }
                            if (MenuManager.Menu.Item("tpscroll_teleport_hero_name").GetValue<bool>())
                            {
                                DrawingMiniMap.NamePosition_9.Add(MiniMapPosition);
                            }
                            DrawingMiniMap.Position_9.Add(MiniMapPosition);
                            DrawingMiniMap.HeroName_9 = (player.Hero.GetRealName());
                            DrawingMiniMap.HeroNamePos_9 = (int)(player.Hero.GetRealName().Length * 3.84f);
                            DrawingMiniMap.HeroNameColor_9 = (Color)new Vector3(0.6431373f, 0.4117647f, 0);
                            DrawingMiniMap.HeroColor_9 = (Color)new Vector3(0.6431373f, 0.4117647f, 0);
                            DrawingMiniMap.Remover(MiniMapPosition);
                        }

                        //TP Enemy                       
                        if (Team && MenuManager.Menu.Item("tpscroll_teleport_enemy").GetValue<bool>())
                        {
                            var MiniMapPosition = HUDInfo.WorldToMinimap(args.ParticleEffect.GetControlPoint(0));
                            if (MenuManager.Menu.Item("tpscroll_teleport_enemy_msg").GetValue<bool>())
                            {
                                MessageCreator.MessageItemCreator(player.Hero.Name.Substring(14), "tpscroll");
                            }
                            if (MenuManager.Menu.Item("tpscroll_teleport_enemy_sound").GetValue<bool>())
                            {
                                Sound.PlaySound("default_" + BeAwarePlus.Addition[BeAwarePlus.GetLangId] + ".wav");
                            }
                            if (MenuManager.Menu.Item("tpscroll_teleport_hero_name").GetValue<bool>())
                            {
                                DrawingMiniMap.NamePosition_9.Add(MiniMapPosition);
                            }
                            DrawingMiniMap.Position_9.Add(MiniMapPosition);
                            DrawingMiniMap.HeroName_9 = (player.Hero.GetRealName());
                            DrawingMiniMap.HeroNamePos_9 = (int)(player.Hero.GetRealName().Length * 3.84f);
                            DrawingMiniMap.HeroNameColor_9 = (Color)new Vector3(0.6431373f, 0.4117647f, 0);
                            DrawingMiniMap.HeroColor_9 = (Color)new Vector3(0.6431373f, 0.4117647f, 0);
                            DrawingMiniMap.Remover(MiniMapPosition);
                        }
                    }
                });
            }

            //Town Portall Scrol Teleport Start 
            if (args.Name.Contains("teleport_start") && MenuManager.Menu.Item("tpscroll_teleport_start").GetValue<bool>())
            {
                DelayAction.Add(60, () =>
                {
                    if (player == null) return;
                    if (0 == player.Id)
                    {
                        //TP Ally
                        if (!Team && MenuManager.Menu.Item("tpscroll_teleport_ally").GetValue<bool>())
                        {
                            var MiniMapPosition = HUDInfo.WorldToMinimap(args.ParticleEffect.GetControlPoint(0));
                            DrawingMiniMap.Position_0.Add(MiniMapPosition);
                            DrawingMiniMap.HeroColor_0 = (Color)new Vector3(0.2f, 0.4588236f, 1);
                            DrawingMiniMap.Remover(MiniMapPosition);
                        }
                        //TP Enemy                       
                        if (Team && MenuManager.Menu.Item("tpscroll_teleport_enemy").GetValue<bool>())
                        {
                            var MiniMapPosition = HUDInfo.WorldToMinimap(args.ParticleEffect.GetControlPoint(0));
                            DrawingMiniMap.Position_0.Add(MiniMapPosition);
                            DrawingMiniMap.HeroColor_0 = (Color)new Vector3(0.2f, 0.4588236f, 1);
                            DrawingMiniMap.Remover(MiniMapPosition);
                        }                        
                    }
                    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    if (1 == player.Id)
                    {
                        //TP Ally
                        if (!Team && MenuManager.Menu.Item("tpscroll_teleport_ally").GetValue<bool>())
                        {
                            var MiniMapPosition = HUDInfo.WorldToMinimap(args.ParticleEffect.GetControlPoint(0));
                            DrawingMiniMap.Position_1.Add(MiniMapPosition);
                            DrawingMiniMap.HeroColor_1 = (Color)new Vector3(0.4f, 1, 0.7490196f);
                            DrawingMiniMap.Remover(MiniMapPosition);
                        }
                        //TP Enemy                       
                        if (Team && MenuManager.Menu.Item("tpscroll_teleport_enemy").GetValue<bool>())
                        {
                            var MiniMapPosition = HUDInfo.WorldToMinimap(args.ParticleEffect.GetControlPoint(0));
                            DrawingMiniMap.Position_1.Add(MiniMapPosition);
                            DrawingMiniMap.HeroColor_1 = (Color)new Vector3(0.4f, 1, 0.7490196f);
                            DrawingMiniMap.Remover(MiniMapPosition);
                        }
                    }
                    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    if (2 == player.Id)
                    {
                        //TP Ally
                        if (!Team && MenuManager.Menu.Item("tpscroll_teleport_ally").GetValue<bool>())
                        {
                            var MiniMapPosition = HUDInfo.WorldToMinimap(args.ParticleEffect.GetControlPoint(0));
                            DrawingMiniMap.Position_2.Add(MiniMapPosition);
                            DrawingMiniMap.HeroColor_2 = (Color)new Vector3(0.7490196f, 0, 0.7490196f);
                            DrawingMiniMap.Remover(MiniMapPosition);
                        }
                        //TP Enemy                       
                        if (Team && MenuManager.Menu.Item("tpscroll_teleport_enemy").GetValue<bool>())
                        {
                            var MiniMapPosition = HUDInfo.WorldToMinimap(args.ParticleEffect.GetControlPoint(0));
                            DrawingMiniMap.Position_2.Add(MiniMapPosition);
                            DrawingMiniMap.HeroColor_2 = (Color)new Vector3(0.7490196f, 0, 0.7490196f);
                            DrawingMiniMap.Remover(MiniMapPosition);
                        }
                    }
                    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    if (3 == player.Id)
                    {
                        //TP Ally
                        if (!Team && MenuManager.Menu.Item("tpscroll_teleport_ally").GetValue<bool>())
                        {
                            var MiniMapPosition = HUDInfo.WorldToMinimap(args.ParticleEffect.GetControlPoint(0));
                            DrawingMiniMap.Position_3.Add(MiniMapPosition);
                            DrawingMiniMap.HeroColor_3 = (Color)new Vector3(0.9529412f, 0.9411765f, 0.04313726f);
                            DrawingMiniMap.Remover(MiniMapPosition);
                        }
                        //TP Enemy                       
                        if (Team && MenuManager.Menu.Item("tpscroll_teleport_enemy").GetValue<bool>())
                        {
                            var MiniMapPosition = HUDInfo.WorldToMinimap(args.ParticleEffect.GetControlPoint(0));
                            DrawingMiniMap.Position_3.Add(MiniMapPosition);
                            DrawingMiniMap.HeroColor_3 = (Color)new Vector3(0.9529412f, 0.9411765f, 0.04313726f);
                            DrawingMiniMap.Remover(MiniMapPosition);
                        }
                    }
                    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    if (4 == player.Id)
                    {
                        //TP Ally
                        if (!Team && MenuManager.Menu.Item("tpscroll_teleport_ally").GetValue<bool>())
                        {
                            var MiniMapPosition = HUDInfo.WorldToMinimap(args.ParticleEffect.GetControlPoint(0));
                            DrawingMiniMap.Position_4.Add(MiniMapPosition);
                            DrawingMiniMap.HeroColor_4 = (Color)new Vector3(1, 0.4196079f, 0);
                            DrawingMiniMap.Remover(MiniMapPosition);
                        }
                        //TP Enemy                       
                        if (Team && MenuManager.Menu.Item("tpscroll_teleport_enemy").GetValue<bool>())
                        {
                            var MiniMapPosition = HUDInfo.WorldToMinimap(args.ParticleEffect.GetControlPoint(0));
                            DrawingMiniMap.Position_4.Add(MiniMapPosition);
                            DrawingMiniMap.HeroColor_4 = (Color)new Vector3(1, 0.4196079f, 0);
                            DrawingMiniMap.Remover(MiniMapPosition);
                        }
                    }
                    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    if (5 == player.Id)
                    {
                        //TP Ally
                        if (!Team && MenuManager.Menu.Item("tpscroll_teleport_ally").GetValue<bool>())
                        {
                            var MiniMapPosition = HUDInfo.WorldToMinimap(args.ParticleEffect.GetControlPoint(0));
                            DrawingMiniMap.Position_5.Add(MiniMapPosition);
                            DrawingMiniMap.HeroColor_5 = (Color)new Vector3(0.9960785f, 0.5254902f, 0.7607844f);
                            DrawingMiniMap.Remover(MiniMapPosition);
                        }
                        //TP Enemy                       
                        if (Team && MenuManager.Menu.Item("tpscroll_teleport_enemy").GetValue<bool>())
                        {
                            var MiniMapPosition = HUDInfo.WorldToMinimap(args.ParticleEffect.GetControlPoint(0));
                            DrawingMiniMap.Position_5.Add(MiniMapPosition);
                            DrawingMiniMap.HeroColor_5 = (Color)new Vector3(0.9960785f, 0.5254902f, 0.7607844f);
                            DrawingMiniMap.Remover(MiniMapPosition);
                        }
                    }
                    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    if (6 == player.Id)
                    {
                        //TP Ally
                        if (!Team && MenuManager.Menu.Item("tpscroll_teleport_ally").GetValue<bool>())
                        {
                            var MiniMapPosition = HUDInfo.WorldToMinimap(args.ParticleEffect.GetControlPoint(0));
                            DrawingMiniMap.Position_6.Add(MiniMapPosition);
                            DrawingMiniMap.HeroColor_6 = (Color)new Vector3(0.6313726f, 0.7058824f, 0.2784314f);
                            DrawingMiniMap.Remover(MiniMapPosition);
                        }
                        //TP Enemy                       
                        if (Team && MenuManager.Menu.Item("tpscroll_teleport_enemy").GetValue<bool>())
                        {
                            var MiniMapPosition = HUDInfo.WorldToMinimap(args.ParticleEffect.GetControlPoint(0));
                            DrawingMiniMap.Position_6.Add(MiniMapPosition);
                            DrawingMiniMap.HeroColor_6 = (Color)new Vector3(0.6313726f, 0.7058824f, 0.2784314f);
                            DrawingMiniMap.Remover(MiniMapPosition);
                        }
                    }
                    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    if (7 == player.Id)
                    {
                        //TP Ally
                        if (!Team && MenuManager.Menu.Item("tpscroll_teleport_ally").GetValue<bool>())
                        {
                            var MiniMapPosition = HUDInfo.WorldToMinimap(args.ParticleEffect.GetControlPoint(0));
                            DrawingMiniMap.Position_7.Add(MiniMapPosition);
                            DrawingMiniMap.HeroColor_7 = (Color)new Vector3(0.3960785f, 0.8509805f, 0.9686275f);
                            DrawingMiniMap.Remover(MiniMapPosition);
                        }
                        //TP Enemy                       
                        if (Team && MenuManager.Menu.Item("tpscroll_teleport_enemy").GetValue<bool>())
                        {
                            var MiniMapPosition = HUDInfo.WorldToMinimap(args.ParticleEffect.GetControlPoint(0));
                            DrawingMiniMap.Position_7.Add(MiniMapPosition);
                            DrawingMiniMap.HeroColor_7 = (Color)new Vector3(0.3960785f, 0.8509805f, 0.9686275f);
                            DrawingMiniMap.Remover(MiniMapPosition);
                        }
                    }
                    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    if (8 == player.Id)
                    {
                        //TP Ally
                        if (!Team && MenuManager.Menu.Item("tpscroll_teleport_ally").GetValue<bool>())
                        {
                            var MiniMapPosition = HUDInfo.WorldToMinimap(args.ParticleEffect.GetControlPoint(0));
                            DrawingMiniMap.Position_8.Add(MiniMapPosition);
                            DrawingMiniMap.HeroColor_8 = (Color)new Vector3(0, 0.5137255f, 0.1294118f);
                            DrawingMiniMap.Remover(MiniMapPosition);
                        }
                        //TP Enemy                       
                        if (Team && MenuManager.Menu.Item("tpscroll_teleport_enemy").GetValue<bool>())
                        {
                            var MiniMapPosition = HUDInfo.WorldToMinimap(args.ParticleEffect.GetControlPoint(0));
                            DrawingMiniMap.Position_8.Add(MiniMapPosition);
                            DrawingMiniMap.HeroColor_8 = (Color)new Vector3(0, 0.5137255f, 0.1294118f);
                            DrawingMiniMap.Remover(MiniMapPosition);
                        }
                    }
                    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    if (9 == player.Id)
                    {
                        //TP Ally
                        if (!Team && MenuManager.Menu.Item("tpscroll_teleport_ally").GetValue<bool>())
                        {
                            var MiniMapPosition = HUDInfo.WorldToMinimap(args.ParticleEffect.GetControlPoint(0));
                            DrawingMiniMap.Position_9.Add(MiniMapPosition);
                            DrawingMiniMap.HeroColor_9 = (Color)new Vector3(0.6431373f, 0.4117647f, 0);
                            DrawingMiniMap.Remover(MiniMapPosition);
                        }
                        //TP Enemy                       
                        if (Team && MenuManager.Menu.Item("tpscroll_teleport_enemy").GetValue<bool>())
                        {
                            var MiniMapPosition = HUDInfo.WorldToMinimap(args.ParticleEffect.GetControlPoint(0));
                            DrawingMiniMap.Position_9.Add(MiniMapPosition);
                            DrawingMiniMap.HeroColor_9 = (Color)new Vector3(0.6431373f, 0.4117647f, 0);
                            DrawingMiniMap.Remover(MiniMapPosition);
                        }
                    }
                });
            }

            //Boots Of Travel Teleport Start == End               
            //TP Enemy 
            //===========================================================================================//  
            //TP End                           
            if ((args.Name.Contains("teleport_end") && !BeAwarePlus.FurionFix)
                && MenuManager.Menu.Item("bt_teleport_enemy").GetValue<bool>())
            {
                DelayAction.Add(30, () =>
                {                 
                    BTID = args.ParticleEffect.GetControlPoint(6);
                    if (new Vector3(BTID.X, BTID.Y, BTID.Z) == (new Vector3(0, 0, 0)))
                    {
                        if (BeAwarePlus.IgnorAllyTP)
                        { 
                            var MiniMapPosition = HUDInfo.WorldToMinimap(args.ParticleEffect.GetControlPoint(0));
                            DrawingMiniMap.Position_BT_Enemy.Add(MiniMapPosition);
                            DrawingMiniMap.Remover2(MiniMapPosition);
                        }
                    }
                });
            }

            //TP Start
            if (args.Name.Contains("teleport_start")
                && MenuManager.Menu.Item("bt_teleport_enemy").GetValue<bool>())
            {
                DelayAction.Add(60, () =>
                {
                    if (new Vector3(BTID.X, BTID.Y, BTID.Z) == (new Vector3(0, 0, 0)))
                    {
                        if (BeAwarePlus.IgnorAllyTP)
                        {
                            var MiniMapPosition = HUDInfo.WorldToMinimap(args.ParticleEffect.GetControlPoint(0));
                            DrawingMiniMap.Position_BT_Enemy.Add(MiniMapPosition);
                            DrawingMiniMap.Remover2(MiniMapPosition);
                            if (MenuManager.Menu.Item("bt_teleport_enemy_msg").GetValue<bool>())
                            {
                                MessageCreator.MessageItemCreator("default2", "travel_boots");
                            }
                            if (MenuManager.Menu.Item("bt_teleport_enemy_sound").GetValue<bool>())
                            {
                                Sound.PlaySound("default_" + BeAwarePlus.Addition[BeAwarePlus.GetLangId] + ".wav");
                            }
                        }
                        DelayAction.Add(50, () =>
                        {
                            BeAwarePlus.IgnorAllyTP = true;
                        });
                   }
                });
            }          
        }
    }
}
