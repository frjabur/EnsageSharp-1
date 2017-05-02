using Ensage;
using Ensage.Common.Menu;
using SharpDX;

namespace BeAwarePlus
{
    public static class Helper
    {
        internal static string Color;

        public static void Hero()
        {
            try
            {
                var HeroCheckColor_0 = ObjectManager.GetPlayerById(0);
                var HeroName_0 = (HeroCheckColor_0.Hero.Name);
                if (HeroName_0 == Color)
                {
                    DrawingMiniMap.Position_0.Add(BeAwarePlus.MiniMapPosition);
                    DrawingMiniMap.Remover(BeAwarePlus.MiniMapPosition);
                    DrawingMiniMap.HeroColor_0 = (Color)new Vector3(0.2f, 0.4588236f, 1);
                }
                var HeroCheckColor_1 = ObjectManager.GetPlayerById(1);
                var HeroName_1 = (HeroCheckColor_1.Hero.Name);
                if (HeroName_1 == Color)
                {
                    DrawingMiniMap.Position_1.Add(BeAwarePlus.MiniMapPosition);
                    DrawingMiniMap.Remover(BeAwarePlus.MiniMapPosition);
                    DrawingMiniMap.HeroColor_1 = (Color)new Vector3(0.4f, 1, 0.7490196f);
                }
                var HeroCheckColor_2 = ObjectManager.GetPlayerById(2);
                var HeroName_2 = (HeroCheckColor_2.Hero.Name);
                if (HeroName_2 == Color)
                {
                    DrawingMiniMap.Position_2.Add(BeAwarePlus.MiniMapPosition);
                    DrawingMiniMap.Remover(BeAwarePlus.MiniMapPosition);
                    DrawingMiniMap.HeroColor_2 = (Color)new Vector3(0.7490196f, 0, 0.7490196f);
                }
                var HeroCheckColor_3 = ObjectManager.GetPlayerById(3);
                var HeroName_3 = (HeroCheckColor_3.Hero.Name);
                if (HeroName_3 == Color)
                {
                    DrawingMiniMap.Position_3.Add(BeAwarePlus.MiniMapPosition);
                    DrawingMiniMap.Remover(BeAwarePlus.MiniMapPosition);
                    DrawingMiniMap.HeroColor_3 = (Color)new Vector3(0.9529412f, 0.9411765f, 0.04313726f);
                }
                var HeroCheckColor_4 = ObjectManager.GetPlayerById(4);
                var HeroName_4 = (HeroCheckColor_4.Hero.Name);
                if (HeroName_4 == Color)
                {
                    DrawingMiniMap.Position_4.Add(BeAwarePlus.MiniMapPosition);
                    DrawingMiniMap.Remover(BeAwarePlus.MiniMapPosition);
                    DrawingMiniMap.HeroColor_4 = (Color)new Vector3(1, 0.4196079f, 0);
                }
                var HeroCheckColor_5 = ObjectManager.GetPlayerById(5);
                var HeroName_5 = (HeroCheckColor_5.Hero.Name);
                if (HeroName_5 == Color)
                {
                    DrawingMiniMap.Position_5.Add(BeAwarePlus.MiniMapPosition);
                    DrawingMiniMap.Remover(BeAwarePlus.MiniMapPosition);
                    DrawingMiniMap.HeroColor_5 = (Color)new Vector3(0.9960785f, 0.5254902f, 0.7607844f);
                }
                var HeroCheckColor_6 = ObjectManager.GetPlayerById(6);
                var HeroName_6 = (HeroCheckColor_6.Hero.Name);
                if (HeroName_6 == Color)
                {
                    DrawingMiniMap.Position_6.Add(BeAwarePlus.MiniMapPosition);
                    DrawingMiniMap.Remover(BeAwarePlus.MiniMapPosition);
                    DrawingMiniMap.HeroColor_6 = (Color)new Vector3(0.6313726f, 0.7058824f, 0.2784314f);
                }
                var HeroCheckColor_7 = ObjectManager.GetPlayerById(7);
                var HeroName_7 = (HeroCheckColor_7.Hero.Name);
                if (HeroName_7 == Color)
                {
                    DrawingMiniMap.Position_7.Add(BeAwarePlus.MiniMapPosition);
                    DrawingMiniMap.Remover(BeAwarePlus.MiniMapPosition);
                    DrawingMiniMap.HeroColor_7 = (Color)new Vector3(0.3960785f, 0.8509805f, 0.9686275f);
                }
                var HeroCheckColor_8 = ObjectManager.GetPlayerById(8);
                var HeroName_8 = (HeroCheckColor_8.Hero.Name);
                if (HeroName_8 == Color)
                {
                    DrawingMiniMap.Position_8.Add(BeAwarePlus.MiniMapPosition);
                    DrawingMiniMap.Remover(BeAwarePlus.MiniMapPosition);
                    DrawingMiniMap.HeroColor_8 = (Color)new Vector3(0, 0.5137255f, 0.1294118f);
                }
                var HeroCheckColor_9 = ObjectManager.GetPlayerById(9);
                var HeroName_9 = (HeroCheckColor_9.Hero.Name);
                if (HeroName_9 == Color)
                {
                    DrawingMiniMap.Position_9.Add(BeAwarePlus.MiniMapPosition);
                    DrawingMiniMap.Remover(BeAwarePlus.MiniMapPosition);
                    DrawingMiniMap.HeroColor_9 = (Color)new Vector3(0.6431373f, 0.4117647f, 0);
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

                if (0 == BeAwarePlus.HeroID)
                {
                    DrawingMiniMap.Position_0.Add(BeAwarePlus.MiniMapPosition);
                    if (MenuManager.Menu.Item("tpscroll_teleport_hero_name").GetValue<bool>())
                    {
                        DrawingMiniMap.NamePosition_0.Add(BeAwarePlus.MiniMapPosition);
                    }
                    DrawingMiniMap.HeroName_0 = (BeAwarePlus.RealName);
                    DrawingMiniMap.HeroNamePos_0 = (int)(BeAwarePlus.HeroNameLength * 3.84f);
                    DrawingMiniMap.HeroNameColor_0 = (Color)new Vector3(0.2f, 0.4588236f, 1);
                    DrawingMiniMap.HeroColor_0 = (Color)new Vector3(0.2f, 0.4588236f, 1);
                    DrawingMiniMap.Remover(BeAwarePlus.MiniMapPosition);                    
                }
                if (1 == BeAwarePlus.HeroID)
                {
                    DrawingMiniMap.Position_1.Add(BeAwarePlus.MiniMapPosition);
                    if (MenuManager.Menu.Item("tpscroll_teleport_hero_name").GetValue<bool>())
                    {
                        DrawingMiniMap.NamePosition_1.Add(BeAwarePlus.MiniMapPosition);
                    }
                    DrawingMiniMap.HeroName_1 = (BeAwarePlus.RealName);
                    DrawingMiniMap.HeroNamePos_1 = (int)(BeAwarePlus.HeroNameLength * 3.84f);
                    DrawingMiniMap.HeroNameColor_1 = (Color)new Vector3(0.4f, 1, 0.7490196f);
                    DrawingMiniMap.HeroColor_1 = (Color)new Vector3(0.4f, 1, 0.7490196f);
                    DrawingMiniMap.Remover(BeAwarePlus.MiniMapPosition);
                }
                if (2 == BeAwarePlus.HeroID)
                {
                    DrawingMiniMap.Position_2.Add(BeAwarePlus.MiniMapPosition);
                    if (MenuManager.Menu.Item("tpscroll_teleport_hero_name").GetValue<bool>())
                    {
                        DrawingMiniMap.NamePosition_2.Add(BeAwarePlus.MiniMapPosition);
                    }
                    DrawingMiniMap.HeroName_2 = (BeAwarePlus.RealName);
                    DrawingMiniMap.HeroNamePos_2 = (int)(BeAwarePlus.HeroNameLength * 3.84f);
                    DrawingMiniMap.HeroNameColor_2 = (Color)new Vector3(0.7490196f, 0, 0.7490196f);
                    DrawingMiniMap.HeroColor_2 = (Color)new Vector3(0.7490196f, 0, 0.7490196f);
                    DrawingMiniMap.Remover(BeAwarePlus.MiniMapPosition);
                }
                if (3 == BeAwarePlus.HeroID)
                {
                    DrawingMiniMap.Position_3.Add(BeAwarePlus.MiniMapPosition);
                    if (MenuManager.Menu.Item("tpscroll_teleport_hero_name").GetValue<bool>())
                    {
                        DrawingMiniMap.NamePosition_3.Add(BeAwarePlus.MiniMapPosition);
                    }
                    DrawingMiniMap.HeroName_3 = (BeAwarePlus.RealName);
                    DrawingMiniMap.HeroNamePos_3 = (int)(BeAwarePlus.HeroNameLength * 3.84f);
                    DrawingMiniMap.HeroNameColor_3 = (Color)new Vector3(0.9529412f, 0.9411765f, 0.04313726f);
                    DrawingMiniMap.HeroColor_3 = (Color)new Vector3(0.9529412f, 0.9411765f, 0.04313726f);
                    DrawingMiniMap.Remover(BeAwarePlus.MiniMapPosition);
                }
                if (4 == BeAwarePlus.HeroID)
                {
                    DrawingMiniMap.Position_4.Add(BeAwarePlus.MiniMapPosition);
                    if (MenuManager.Menu.Item("tpscroll_teleport_hero_name").GetValue<bool>())
                    {
                        DrawingMiniMap.NamePosition_4.Add(BeAwarePlus.MiniMapPosition);
                    }
                    DrawingMiniMap.HeroName_4 = (BeAwarePlus.RealName);
                    DrawingMiniMap.HeroNamePos_4 = (int)(BeAwarePlus.HeroNameLength * 3.84f);
                    DrawingMiniMap.HeroNameColor_4 = (Color)new Vector3(1, 0.4196079f, 0);
                    DrawingMiniMap.HeroColor_4 = (Color)new Vector3(1, 0.4196079f, 0);
                    DrawingMiniMap.Remover(BeAwarePlus.MiniMapPosition);
                }
                if (5 == BeAwarePlus.HeroID)
                {
                    DrawingMiniMap.Position_5.Add(BeAwarePlus.MiniMapPosition);
                    if (MenuManager.Menu.Item("tpscroll_teleport_hero_name").GetValue<bool>())
                    {
                        DrawingMiniMap.NamePosition_5.Add(BeAwarePlus.MiniMapPosition);
                    }
                    DrawingMiniMap.HeroName_5 = (BeAwarePlus.RealName);
                    DrawingMiniMap.HeroNamePos_5 = (int)(BeAwarePlus.HeroNameLength * 3.84f);
                    DrawingMiniMap.HeroNameColor_5 = (Color)new Vector3(0.9960785f, 0.5254902f, 0.7607844f);
                    DrawingMiniMap.HeroColor_5 = (Color)new Vector3(0.9960785f, 0.5254902f, 0.7607844f);
                    DrawingMiniMap.Remover(BeAwarePlus.MiniMapPosition);
                }
                if (6 == BeAwarePlus.HeroID)
                {
                    DrawingMiniMap.Position_6.Add(BeAwarePlus.MiniMapPosition);
                    if (MenuManager.Menu.Item("tpscroll_teleport_hero_name").GetValue<bool>())
                    {
                        DrawingMiniMap.NamePosition_6.Add(BeAwarePlus.MiniMapPosition);
                    }
                    DrawingMiniMap.HeroName_6 = (BeAwarePlus.RealName);
                    DrawingMiniMap.HeroNamePos_6 = (int)(BeAwarePlus.HeroNameLength * 3.84f);
                    DrawingMiniMap.HeroNameColor_6 = (Color)new Vector3(0.6313726f, 0.7058824f, 0.2784314f);
                    DrawingMiniMap.HeroColor_6 = (Color)new Vector3(0.6313726f, 0.7058824f, 0.2784314f);
                    DrawingMiniMap.Remover(BeAwarePlus.MiniMapPosition);
                }
                if (7 == BeAwarePlus.HeroID)
                {
                    DrawingMiniMap.Position_7.Add(BeAwarePlus.MiniMapPosition);
                    if (MenuManager.Menu.Item("tpscroll_teleport_hero_name").GetValue<bool>())
                    {
                        DrawingMiniMap.NamePosition_7.Add(BeAwarePlus.MiniMapPosition);
                    }
                    DrawingMiniMap.HeroName_7 = (BeAwarePlus.RealName);
                    DrawingMiniMap.HeroNamePos_7 = (int)(BeAwarePlus.HeroNameLength * 3.84f);
                    DrawingMiniMap.HeroNameColor_7 = (Color)new Vector3(0.3960785f, 0.8509805f, 0.9686275f);
                    DrawingMiniMap.HeroColor_7 = (Color)new Vector3(0.3960785f, 0.8509805f, 0.9686275f);
                    DrawingMiniMap.Remover(BeAwarePlus.MiniMapPosition);
                }
                if (8 == BeAwarePlus.HeroID)
                {
                    DrawingMiniMap.Position_8.Add(BeAwarePlus.MiniMapPosition);
                    if (MenuManager.Menu.Item("tpscroll_teleport_hero_name").GetValue<bool>())
                    {
                        DrawingMiniMap.NamePosition_8.Add(BeAwarePlus.MiniMapPosition);
                    }
                    DrawingMiniMap.HeroName_8 = (BeAwarePlus.RealName);
                    DrawingMiniMap.HeroNamePos_8 = (int)(BeAwarePlus.HeroNameLength * 3.84f);
                    DrawingMiniMap.HeroNameColor_8 = (Color)new Vector3(0, 0.5137255f, 0.1294118f);
                    DrawingMiniMap.HeroColor_8 = (Color)new Vector3(0, 0.5137255f, 0.1294118f);
                    DrawingMiniMap.Remover(BeAwarePlus.MiniMapPosition);
                }

                if (9 == BeAwarePlus.HeroID)
                {
                    DrawingMiniMap.Position_9.Add(BeAwarePlus.MiniMapPosition);
                    if (MenuManager.Menu.Item("tpscroll_teleport_hero_name").GetValue<bool>())
                    {
                        DrawingMiniMap.NamePosition_9.Add(BeAwarePlus.MiniMapPosition);
                    }
                    DrawingMiniMap.HeroName_9 = (BeAwarePlus.RealName);
                    DrawingMiniMap.HeroNamePos_9 = (int)(BeAwarePlus.HeroNameLength * 3.84f);
                    DrawingMiniMap.HeroNameColor_9 = (Color)new Vector3(0.6431373f, 0.4117647f, 0);
                    DrawingMiniMap.HeroColor_9 = (Color)new Vector3(0.6431373f, 0.4117647f, 0);
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
                if (0 == BeAwarePlus.HeroID)
                {
                    DrawingMiniMap.Position_0.Add(BeAwarePlus.MiniMapPosition);
                    DrawingMiniMap.HeroColor_0 = (Color)new Vector3(0.2f, 0.4588236f, 1);
                    DrawingMiniMap.Remover(BeAwarePlus.MiniMapPosition);
                }
                if (1 == BeAwarePlus.HeroID)
                {
                    DrawingMiniMap.Position_1.Add(BeAwarePlus.MiniMapPosition);
                    DrawingMiniMap.HeroColor_1 = (Color)new Vector3(0.4f, 1, 0.7490196f);
                    DrawingMiniMap.Remover(BeAwarePlus.MiniMapPosition);
                }
                if (2 == BeAwarePlus.HeroID)
                {
                    DrawingMiniMap.Position_2.Add(BeAwarePlus.MiniMapPosition);
                    DrawingMiniMap.HeroColor_2 = (Color)new Vector3(0.7490196f, 0, 0.7490196f);
                    DrawingMiniMap.Remover(BeAwarePlus.MiniMapPosition);
                }
                if (3 == BeAwarePlus.HeroID)
                {
                    DrawingMiniMap.Position_3.Add(BeAwarePlus.MiniMapPosition);
                    DrawingMiniMap.HeroColor_3 = (Color)new Vector3(0.9529412f, 0.9411765f, 0.04313726f);
                    DrawingMiniMap.Remover(BeAwarePlus.MiniMapPosition);
                }
                if (4 == BeAwarePlus.HeroID)
                {
                    DrawingMiniMap.Position_4.Add(BeAwarePlus.MiniMapPosition);
                    DrawingMiniMap.HeroColor_4 = (Color)new Vector3(1, 0.4196079f, 0);
                    DrawingMiniMap.Remover(BeAwarePlus.MiniMapPosition);
                }
                if (5 == BeAwarePlus.HeroID)
                {
                    DrawingMiniMap.Position_5.Add(BeAwarePlus.MiniMapPosition);
                    DrawingMiniMap.HeroColor_5 = (Color)new Vector3(0.9960785f, 0.5254902f, 0.7607844f);
                    DrawingMiniMap.Remover(BeAwarePlus.MiniMapPosition);
                }
                if (6 == BeAwarePlus.HeroID)
                {
                    DrawingMiniMap.Position_6.Add(BeAwarePlus.MiniMapPosition);
                    DrawingMiniMap.HeroColor_6 = (Color)new Vector3(0.6313726f, 0.7058824f, 0.2784314f);
                    DrawingMiniMap.Remover(BeAwarePlus.MiniMapPosition);
                }
                if (7 == BeAwarePlus.HeroID)
                {
                    DrawingMiniMap.Position_7.Add(BeAwarePlus.MiniMapPosition);
                    DrawingMiniMap.HeroColor_7 = (Color)new Vector3(0.3960785f, 0.8509805f, 0.9686275f);
                    DrawingMiniMap.Remover(BeAwarePlus.MiniMapPosition);
                }
                if (8 == BeAwarePlus.HeroID)
                {
                    DrawingMiniMap.Position_8.Add(BeAwarePlus.MiniMapPosition);                    
                    DrawingMiniMap.HeroColor_8 = (Color)new Vector3(0, 0.5137255f, 0.1294118f);
                    DrawingMiniMap.Remover(BeAwarePlus.MiniMapPosition);
                }
                if (9 == BeAwarePlus.HeroID)
                {
                    DrawingMiniMap.Position_9.Add(BeAwarePlus.MiniMapPosition);
                    DrawingMiniMap.HeroColor_9 = (Color)new Vector3(0.6431373f, 0.4117647f, 0);
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
            DrawingMiniMap.Remover(BeAwarePlus.MiniMapPosition);
                                                   
        }
        public static void BTTeleportStartAlly()
        {
            DrawingMiniMap.Position_BT_Ally.Add(BeAwarePlus.MiniMapPosition);
            DrawingMiniMap.Remover(BeAwarePlus.MiniMapPosition);

        }
        public static void Item()
        {
            DrawingMiniMap.Position_Item.Add(BeAwarePlus.MiniMapPosition);
            DrawingMiniMap.Remover(BeAwarePlus.MiniMapPosition);           
        }
    }
}
