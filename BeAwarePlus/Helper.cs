using Ensage;
using Ensage.Common.Extensions;
using SharpDX;

namespace BeAwarePlus
{
    public static class Helper
    {
        public static void HeroSpells()
        {
            try
            {
                if (ObjectManager.GetPlayerById(0).Hero.Name == BeAwarePlus.HeroColor)
                {
                    DrawingMiniMap.PositionSpells_0.Add(BeAwarePlus.MiniMapPosition);
                    DrawingMiniMap.Remover(BeAwarePlus.MiniMapPosition);
                    DrawingMiniMap.HeroColorSpells_0 = (Color)new Vector3(0.2f, 0.4588236f, 1);
                }

                if (ObjectManager.GetPlayerById(1).Hero.Name == BeAwarePlus.HeroColor)
                {
                    DrawingMiniMap.PositionSpells_1.Add(BeAwarePlus.MiniMapPosition);
                    DrawingMiniMap.Remover(BeAwarePlus.MiniMapPosition);
                    DrawingMiniMap.HeroColorSpells_1 = (Color)new Vector3(0.4f, 1, 0.7490196f);
                }

                if (ObjectManager.GetPlayerById(2).Hero.Name == BeAwarePlus.HeroColor)
                {
                    DrawingMiniMap.PositionSpells_2.Add(BeAwarePlus.MiniMapPosition);
                    DrawingMiniMap.Remover(BeAwarePlus.MiniMapPosition);
                    DrawingMiniMap.HeroColorSpells_2 = (Color)new Vector3(0.7490196f, 0, 0.7490196f);
                }

                if (ObjectManager.GetPlayerById(3).Hero.Name == BeAwarePlus.HeroColor)
                {
                    DrawingMiniMap.PositionSpells_3.Add(BeAwarePlus.MiniMapPosition);
                    DrawingMiniMap.Remover(BeAwarePlus.MiniMapPosition);
                    DrawingMiniMap.HeroColorSpells_3 = (Color)new Vector3(0.9529412f, 0.9411765f, 0.04313726f);
                }

                if (ObjectManager.GetPlayerById(4).Hero.Name == BeAwarePlus.HeroColor)
                {
                    DrawingMiniMap.PositionSpells_4.Add(BeAwarePlus.MiniMapPosition);
                    DrawingMiniMap.Remover(BeAwarePlus.MiniMapPosition);
                    DrawingMiniMap.HeroColorSpells_4 = (Color)new Vector3(1, 0.4196079f, 0);
                }
            
                if (ObjectManager.GetPlayerById(5).Hero.Name == BeAwarePlus.HeroColor)
                {
                    DrawingMiniMap.PositionSpells_5.Add(BeAwarePlus.MiniMapPosition);
                    DrawingMiniMap.Remover(BeAwarePlus.MiniMapPosition);
                    DrawingMiniMap.HeroColorSpells_5 = (Color)new Vector3(0.9960785f, 0.5254902f, 0.7607844f);
                }

                if (ObjectManager.GetPlayerById(6).Hero.Name == BeAwarePlus.HeroColor)
                {
                    DrawingMiniMap.PositionSpells_6.Add(BeAwarePlus.MiniMapPosition);
                    DrawingMiniMap.Remover(BeAwarePlus.MiniMapPosition);
                    DrawingMiniMap.HeroColorSpells_6 = (Color)new Vector3(0.6313726f, 0.7058824f, 0.2784314f);
                }

                if (ObjectManager.GetPlayerById(7).Hero.Name == BeAwarePlus.HeroColor)
                {
                    DrawingMiniMap.PositionSpells_7.Add(BeAwarePlus.MiniMapPosition);
                    DrawingMiniMap.Remover(BeAwarePlus.MiniMapPosition);
                    DrawingMiniMap.HeroColorSpells_7 = (Color)new Vector3(0.3960785f, 0.8509805f, 0.9686275f);
                }

                if (ObjectManager.GetPlayerById(8).Hero.Name == BeAwarePlus.HeroColor)
                {
                    DrawingMiniMap.PositionSpells_8.Add(BeAwarePlus.MiniMapPosition);
                    DrawingMiniMap.Remover(BeAwarePlus.MiniMapPosition);
                    DrawingMiniMap.HeroColorSpells_8 = (Color)new Vector3(0, 0.5137255f, 0.1294118f);
                }

                if (ObjectManager.GetPlayerById(9).Hero.Name == BeAwarePlus.HeroColor)
                {
                    DrawingMiniMap.PositionSpells_9.Add(BeAwarePlus.MiniMapPosition);
                    DrawingMiniMap.Remover(BeAwarePlus.MiniMapPosition);
                    DrawingMiniMap.HeroColorSpells_9 = (Color)new Vector3(0.6431373f, 0.4117647f, 0);
                }
            }
            catch
            {
                return;
            }            
        }
        public static void TPTeleportEnd()
        {
            try
            {               
                if (0 == BeAwarePlus.player.Id)
                {
                    DrawingMiniMap.PositionTP_0.Add(BeAwarePlus.MiniMapPosition);
                    if (MenuManager.Menu.Item("tpscroll_teleport_hero_name").GetValue<bool>())
                    {
                        DrawingMiniMap.NamePosition_0.Add(BeAwarePlus.MiniMapPosition);
                    }
                    DrawingMiniMap.HeroName_0 = (BeAwarePlus.player.Hero.GetRealName());
                    DrawingMiniMap.HeroNamePos_0 = (int)(BeAwarePlus.player.Hero.GetRealName().Length * 3.84f);
                    DrawingMiniMap.HeroNameColor_0 = (Color)new Vector3(0.2f, 0.4588236f, 1);
                    DrawingMiniMap.HeroColorTP_0 = (Color)new Vector3(0.2f, 0.4588236f, 1);
                    DrawingMiniMap.Remover(BeAwarePlus.MiniMapPosition);                    
                }
                if (1 == BeAwarePlus.player.Id)
                {
                    DrawingMiniMap.PositionTP_1.Add(BeAwarePlus.MiniMapPosition);
                    if (MenuManager.Menu.Item("tpscroll_teleport_hero_name").GetValue<bool>())
                    {
                        DrawingMiniMap.NamePosition_1.Add(BeAwarePlus.MiniMapPosition);
                    }
                    DrawingMiniMap.HeroName_1 = (BeAwarePlus.player.Hero.GetRealName());
                    DrawingMiniMap.HeroNamePos_1 = (int)(BeAwarePlus.player.Hero.GetRealName().Length * 3.84f);
                    DrawingMiniMap.HeroNameColor_1 = (Color)new Vector3(0.4f, 1, 0.7490196f);
                    DrawingMiniMap.HeroColorTP_1 = (Color)new Vector3(0.4f, 1, 0.7490196f);
                    DrawingMiniMap.Remover(BeAwarePlus.MiniMapPosition);
                }
                if (2 == BeAwarePlus.player.Id)
                {
                    DrawingMiniMap.PositionTP_2.Add(BeAwarePlus.MiniMapPosition);
                    if (MenuManager.Menu.Item("tpscroll_teleport_hero_name").GetValue<bool>())
                    {
                        DrawingMiniMap.NamePosition_2.Add(BeAwarePlus.MiniMapPosition);
                    }
                    DrawingMiniMap.HeroName_2 = (BeAwarePlus.player.Hero.GetRealName());
                    DrawingMiniMap.HeroNamePos_2 = (int)(BeAwarePlus.player.Hero.GetRealName().Length * 3.84f);
                    DrawingMiniMap.HeroNameColor_2 = (Color)new Vector3(0.7490196f, 0, 0.7490196f);
                    DrawingMiniMap.HeroColorTP_2 = (Color)new Vector3(0.7490196f, 0, 0.7490196f);
                    DrawingMiniMap.Remover(BeAwarePlus.MiniMapPosition);
                }
                if (3 == BeAwarePlus.player.Id)
                {
                    DrawingMiniMap.PositionTP_3.Add(BeAwarePlus.MiniMapPosition);
                    if (MenuManager.Menu.Item("tpscroll_teleport_hero_name").GetValue<bool>())
                    {
                        DrawingMiniMap.NamePosition_3.Add(BeAwarePlus.MiniMapPosition);
                    }
                    DrawingMiniMap.HeroName_3 = (BeAwarePlus.player.Hero.GetRealName());
                    DrawingMiniMap.HeroNamePos_3 = (int)(BeAwarePlus.player.Hero.GetRealName().Length * 3.84f);
                    DrawingMiniMap.HeroNameColor_3 = (Color)new Vector3(0.9529412f, 0.9411765f, 0.04313726f);
                    DrawingMiniMap.HeroColorTP_3 = (Color)new Vector3(0.9529412f, 0.9411765f, 0.04313726f);
                    DrawingMiniMap.Remover(BeAwarePlus.MiniMapPosition);
                }
                if (4 == BeAwarePlus.player.Id)
                {
                    DrawingMiniMap.PositionTP_4.Add(BeAwarePlus.MiniMapPosition);
                    if (MenuManager.Menu.Item("tpscroll_teleport_hero_name").GetValue<bool>())
                    {
                        DrawingMiniMap.NamePosition_4.Add(BeAwarePlus.MiniMapPosition);
                    }
                    DrawingMiniMap.HeroName_4 = (BeAwarePlus.player.Hero.GetRealName());
                    DrawingMiniMap.HeroNamePos_4 = (int)(BeAwarePlus.player.Hero.GetRealName().Length * 3.84f);
                    DrawingMiniMap.HeroNameColor_4 = (Color)new Vector3(1, 0.4196079f, 0);
                    DrawingMiniMap.HeroColorTP_4 = (Color)new Vector3(1, 0.4196079f, 0);
                    DrawingMiniMap.Remover(BeAwarePlus.MiniMapPosition);
                }
                if (5 == BeAwarePlus.player.Id)
                {
                    DrawingMiniMap.PositionTP_5.Add(BeAwarePlus.MiniMapPosition);
                    if (MenuManager.Menu.Item("tpscroll_teleport_hero_name").GetValue<bool>())
                    {
                        DrawingMiniMap.NamePosition_5.Add(BeAwarePlus.MiniMapPosition);
                    }
                    DrawingMiniMap.HeroName_5 = (BeAwarePlus.player.Hero.GetRealName());
                    DrawingMiniMap.HeroNamePos_5 = (int)(BeAwarePlus.player.Hero.GetRealName().Length * 3.84f);
                    DrawingMiniMap.HeroNameColor_5 = (Color)new Vector3(0.9960785f, 0.5254902f, 0.7607844f);
                    DrawingMiniMap.HeroColorTP_5 = (Color)new Vector3(0.9960785f, 0.5254902f, 0.7607844f);
                    DrawingMiniMap.Remover(BeAwarePlus.MiniMapPosition);
                }
                if (6 == BeAwarePlus.player.Id)
                {
                    DrawingMiniMap.PositionTP_6.Add(BeAwarePlus.MiniMapPosition);
                    if (MenuManager.Menu.Item("tpscroll_teleport_hero_name").GetValue<bool>())
                    {
                        DrawingMiniMap.NamePosition_6.Add(BeAwarePlus.MiniMapPosition);
                    }
                    DrawingMiniMap.HeroName_6 = (BeAwarePlus.player.Hero.GetRealName());
                    DrawingMiniMap.HeroNamePos_6 = (int)(BeAwarePlus.player.Hero.GetRealName().Length * 3.84f);
                    DrawingMiniMap.HeroNameColor_6 = (Color)new Vector3(0.6313726f, 0.7058824f, 0.2784314f);
                    DrawingMiniMap.HeroColorTP_6 = (Color)new Vector3(0.6313726f, 0.7058824f, 0.2784314f);
                    DrawingMiniMap.Remover(BeAwarePlus.MiniMapPosition);
                }
                if (7 == BeAwarePlus.player.Id)
                {
                    DrawingMiniMap.PositionTP_7.Add(BeAwarePlus.MiniMapPosition);
                    if (MenuManager.Menu.Item("tpscroll_teleport_hero_name").GetValue<bool>())
                    {
                        DrawingMiniMap.NamePosition_7.Add(BeAwarePlus.MiniMapPosition);
                    }
                    DrawingMiniMap.HeroName_7 = (BeAwarePlus.player.Hero.GetRealName());
                    DrawingMiniMap.HeroNamePos_7 = (int)(BeAwarePlus.player.Hero.GetRealName().Length * 3.84f);
                    DrawingMiniMap.HeroNameColor_7 = (Color)new Vector3(0.3960785f, 0.8509805f, 0.9686275f);
                    DrawingMiniMap.HeroColorTP_7 = (Color)new Vector3(0.3960785f, 0.8509805f, 0.9686275f);
                    DrawingMiniMap.Remover(BeAwarePlus.MiniMapPosition);
                }
                if (8 == BeAwarePlus.player.Id)
                {
                    DrawingMiniMap.PositionTP_8.Add(BeAwarePlus.MiniMapPosition);
                    if (MenuManager.Menu.Item("tpscroll_teleport_hero_name").GetValue<bool>())
                    {
                        DrawingMiniMap.NamePosition_8.Add(BeAwarePlus.MiniMapPosition);
                    }
                    DrawingMiniMap.HeroName_8 = (BeAwarePlus.player.Hero.GetRealName());
                    DrawingMiniMap.HeroNamePos_8 = (int)(BeAwarePlus.player.Hero.GetRealName().Length * 3.84f);
                    DrawingMiniMap.HeroNameColor_8 = (Color)new Vector3(0, 0.5137255f, 0.1294118f);
                    DrawingMiniMap.HeroColorTP_8 = (Color)new Vector3(0, 0.5137255f, 0.1294118f);
                    DrawingMiniMap.Remover(BeAwarePlus.MiniMapPosition);
                }

                if (9 == BeAwarePlus.player.Id)
                {
                    DrawingMiniMap.PositionTP_9.Add(BeAwarePlus.MiniMapPosition);
                    if (MenuManager.Menu.Item("tpscroll_teleport_hero_name").GetValue<bool>())
                    {
                        DrawingMiniMap.NamePosition_9.Add(BeAwarePlus.MiniMapPosition);
                    }
                    DrawingMiniMap.HeroName_9 = (BeAwarePlus.player.Hero.GetRealName());
                    DrawingMiniMap.HeroNamePos_9 = (int)(BeAwarePlus.player.Hero.GetRealName().Length * 3.84f);
                    DrawingMiniMap.HeroNameColor_9 = (Color)new Vector3(0.6431373f, 0.4117647f, 0);
                    DrawingMiniMap.HeroColorTP_9 = (Color)new Vector3(0.6431373f, 0.4117647f, 0);
                    DrawingMiniMap.Remover(BeAwarePlus.MiniMapPosition);
                }
            }
            catch
            {
                return;
            }
        }

        public static void TPTeleportStart()
        {
            try
            {
                if (0 == BeAwarePlus.player.Id)
                {
                    DrawingMiniMap.PositionTP_0.Add(BeAwarePlus.MiniMapPosition);
                    DrawingMiniMap.HeroColorTP_0 = (Color)new Vector3(0.2f, 0.4588236f, 1);
                    DrawingMiniMap.Remover(BeAwarePlus.MiniMapPosition);
                }
                if (1 == BeAwarePlus.player.Id)
                {
                    DrawingMiniMap.PositionTP_1.Add(BeAwarePlus.MiniMapPosition);
                    DrawingMiniMap.HeroColorTP_1 = (Color)new Vector3(0.4f, 1, 0.7490196f);
                    DrawingMiniMap.Remover(BeAwarePlus.MiniMapPosition);
                }
                if (2 == BeAwarePlus.player.Id)
                {
                    DrawingMiniMap.PositionTP_2.Add(BeAwarePlus.MiniMapPosition);
                    DrawingMiniMap.HeroColorTP_2 = (Color)new Vector3(0.7490196f, 0, 0.7490196f);
                    DrawingMiniMap.Remover(BeAwarePlus.MiniMapPosition);
                }
                if (3 == BeAwarePlus.player.Id)
                {
                    DrawingMiniMap.PositionTP_3.Add(BeAwarePlus.MiniMapPosition);
                    DrawingMiniMap.HeroColorTP_3 = (Color)new Vector3(0.9529412f, 0.9411765f, 0.04313726f);
                    DrawingMiniMap.Remover(BeAwarePlus.MiniMapPosition);
                }
                if (4 == BeAwarePlus.player.Id)
                {
                    DrawingMiniMap.PositionTP_4.Add(BeAwarePlus.MiniMapPosition);
                    DrawingMiniMap.HeroColorTP_4 = (Color)new Vector3(1, 0.4196079f, 0);
                    DrawingMiniMap.Remover(BeAwarePlus.MiniMapPosition);
                }
                if (5 == BeAwarePlus.player.Id)
                {
                    DrawingMiniMap.PositionTP_5.Add(BeAwarePlus.MiniMapPosition);
                    DrawingMiniMap.HeroColorTP_5 = (Color)new Vector3(0.9960785f, 0.5254902f, 0.7607844f);
                    DrawingMiniMap.Remover(BeAwarePlus.MiniMapPosition);
                }
                if (6 == BeAwarePlus.player.Id)
                {
                    DrawingMiniMap.PositionTP_6.Add(BeAwarePlus.MiniMapPosition);
                    DrawingMiniMap.HeroColorTP_6 = (Color)new Vector3(0.6313726f, 0.7058824f, 0.2784314f);
                    DrawingMiniMap.Remover(BeAwarePlus.MiniMapPosition);
                }
                if (7 == BeAwarePlus.player.Id)
                {
                    DrawingMiniMap.PositionTP_7.Add(BeAwarePlus.MiniMapPosition);
                    DrawingMiniMap.HeroColorTP_7 = (Color)new Vector3(0.3960785f, 0.8509805f, 0.9686275f);
                    DrawingMiniMap.Remover(BeAwarePlus.MiniMapPosition);
                }
                if (8 == BeAwarePlus.player.Id)
                {
                    DrawingMiniMap.PositionTP_8.Add(BeAwarePlus.MiniMapPosition);                    
                    DrawingMiniMap.HeroColorTP_8 = (Color)new Vector3(0, 0.5137255f, 0.1294118f);
                    DrawingMiniMap.Remover(BeAwarePlus.MiniMapPosition);
                }
                if (9 == BeAwarePlus.player.Id)
                {
                    DrawingMiniMap.PositionTP_9.Add(BeAwarePlus.MiniMapPosition);
                    DrawingMiniMap.HeroColorTP_9 = (Color)new Vector3(0.6431373f, 0.4117647f, 0);
                    DrawingMiniMap.Remover(BeAwarePlus.MiniMapPosition);
                }
            }
            catch
            {
                return;
            }

        }
        public static void BTTeleportStartEnemy()
        {
            DrawingMiniMap.Position_BT_Enemy.Add(BeAwarePlus.MiniMapPosition);
            DrawingMiniMap.Remover2(BeAwarePlus.MiniMapPosition);                                                 
        }
        public static void BTTeleportStartAlly()
        {
            DrawingMiniMap.Position_BT_Ally.Add(BeAwarePlus.MiniMapPosition);
            DrawingMiniMap.Remover2(BeAwarePlus.MiniMapPosition);
        }
        public static void Item()
        {
            DrawingMiniMap.Position_Item.Add(BeAwarePlus.MiniMapPosition);
            DrawingMiniMap.Remover2(BeAwarePlus.MiniMapPosition);           
        }
    }
}
