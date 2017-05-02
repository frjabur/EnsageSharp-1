using Ensage;
using Ensage.Common;
using Ensage.Common.Menu;
using SharpDX;
using SharpDX.Direct3D9;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BeAwarePlus
{
    internal static class DrawingMiniMap
    {
        private static Font Font;
        private static Font HeroNameFont;

        internal static ColorBGRA HeroColor_0;
        internal static ColorBGRA HeroColor_1;
        internal static ColorBGRA HeroColor_2;
        internal static ColorBGRA HeroColor_3;
        internal static ColorBGRA HeroColor_4;
        internal static ColorBGRA HeroColor_5;
        internal static ColorBGRA HeroColor_6;
        internal static ColorBGRA HeroColor_7;
        internal static ColorBGRA HeroColor_8;
        internal static ColorBGRA HeroColor_9;      

        internal static ColorBGRA HeroNameColor_0;
        internal static ColorBGRA HeroNameColor_1;
        internal static ColorBGRA HeroNameColor_2;
        internal static ColorBGRA HeroNameColor_3;
        internal static ColorBGRA HeroNameColor_4;
        internal static ColorBGRA HeroNameColor_5;
        internal static ColorBGRA HeroNameColor_6;
        internal static ColorBGRA HeroNameColor_7;
        internal static ColorBGRA HeroNameColor_8;
        internal static ColorBGRA HeroNameColor_9;



        
        internal static List<Vector2> Position_0 = new List<Vector2>();
        internal static List<Vector2> Position_1 = new List<Vector2>();
        internal static List<Vector2> Position_2 = new List<Vector2>();
        internal static List<Vector2> Position_3 = new List<Vector2>();
        internal static List<Vector2> Position_4 = new List<Vector2>();
        internal static List<Vector2> Position_5 = new List<Vector2>();
        internal static List<Vector2> Position_6 = new List<Vector2>();
        internal static List<Vector2> Position_7 = new List<Vector2>();
        internal static List<Vector2> Position_8 = new List<Vector2>();
        internal static List<Vector2> Position_9 = new List<Vector2>();

        internal static List<Vector2> Position_Item = new List<Vector2>();
        internal static List<Vector2> Position_BT_Enemy = new List<Vector2>();
        internal static List<Vector2> Position_BT_Ally = new List<Vector2>();

        internal static List<Vector2> NamePosition_0 = new List<Vector2>();
        internal static List<Vector2> NamePosition_1 = new List<Vector2>();
        internal static List<Vector2> NamePosition_2 = new List<Vector2>();
        internal static List<Vector2> NamePosition_3 = new List<Vector2>();
        internal static List<Vector2> NamePosition_4 = new List<Vector2>();
        internal static List<Vector2> NamePosition_5 = new List<Vector2>();
        internal static List<Vector2> NamePosition_6 = new List<Vector2>();
        internal static List<Vector2> NamePosition_7 = new List<Vector2>();
        internal static List<Vector2> NamePosition_8 = new List<Vector2>();
        internal static List<Vector2> NamePosition_9 = new List<Vector2>();
        
        internal static string HeroName_0;
        internal static string HeroName_1;
        internal static string HeroName_2;
        internal static string HeroName_3;
        internal static string HeroName_4;
        internal static string HeroName_5;
        internal static string HeroName_6;
        internal static string HeroName_7;
        internal static string HeroName_8;
        internal static string HeroName_9;

        internal static int HeroNamePos_0;       
        internal static int HeroNamePos_1;
        internal static int HeroNamePos_2;
        internal static int HeroNamePos_3;
        internal static int HeroNamePos_4;
        internal static int HeroNamePos_5;
        internal static int HeroNamePos_6;
        internal static int HeroNamePos_7;
        internal static int HeroNamePos_8;
        internal static int HeroNamePos_9;

        private static int x;
        private static int y;

        internal static int Recalibrate_X => MenuManager.Menu.Item("recalibrate_x").GetValue<Slider>().Value;
        internal static int Recalibrate_Y => MenuManager.Menu.Item("recalibrate_y").GetValue<Slider>().Value;
        internal static int MiniMapSizeIcon => MenuManager.Menu.Item("mini_map_size_icon").GetValue<Slider>().Value;
        internal static int MiniMapSizeName => MenuManager.Menu.Item("mini_map_size_name").GetValue<Slider>().Value;
        internal static int Timer => MenuManager.Menu.Item("timer").GetValue<Slider>().Value;

        public static void Init()
        {
            Drawing.OnEndScene += DrawHeroNamePosition;
            Drawing.OnEndScene += DrawHeroPosition;
            Drawing.OnPostReset += DrawingOnPostReset;
            Drawing.OnPreReset += DrawingOnPreReset;
            Drawing.OnPostReset += DrawingNameOnPostReset;
            Drawing.OnPreReset += DrawingNameOnPreReset;

            if (Drawing.RenderMode == RenderMode.Dx9)
            {
                Font = new Font(             
                    Drawing.Direct3DDevice9,            
                    new FontDescription
                    {
                        FaceName = "Arial",
                        Height = 60 + MiniMapSizeIcon,
                        OutputPrecision = FontPrecision.Default,
                        Quality = FontQuality.Default,
                        CharacterSet = FontCharacterSet.Default,
                        MipLevels = 3,
                        PitchAndFamily = FontPitchAndFamily.Default,
                        Weight = FontWeight.Black,
                    });
            }

            if (Drawing.RenderMode == RenderMode.Dx9)
            {
                HeroNameFont = new Font(
                    Drawing.Direct3DDevice9,
                    new FontDescription
                    {
                        FaceName = "Arial",
                        Height = 20 + MiniMapSizeName,
                        OutputPrecision = FontPrecision.Default,
                        Quality = FontQuality.Default,
                        CharacterSet = FontCharacterSet.Default,
                        MipLevels = 3,
                        PitchAndFamily = FontPitchAndFamily.Default,
                        Weight = FontWeight.Black,
                    });
            }
            if (DrawMinimapIcon[DrawMinimapIndex] == "*")
            {
                x = 0;
                y = 0;
            }
            if (DrawMinimapIcon[DrawMinimapIndex] == "x")
            {
                x = 2;
                y = 9;
            }           
            if (DrawMinimapIcon[DrawMinimapIndex] == "o")
            {
                x = 4;
                y = 11;
            }
            if (DrawMinimapIcon[DrawMinimapIndex] == "+")
            {
                x = 4;
                y = 8;
            }
            if (DrawMinimapIcon[DrawMinimapIndex] == "!")
            {
                x = 0;
                y = 5;
            }
            if (DrawMinimapIcon[DrawMinimapIndex] == "#")
            {
                x = 2;
                y = 5;
            }

        }
        public static int DrawMinimapIndex
        {
            get { return MenuManager.Menu.Item("drawminimap_type").GetValue<StringList>().SelectedIndex; }
            set { throw new NotImplementedException(); }
        }
        public static readonly string[] DrawMinimapIcon = { "*", "x", "o", "+", "!", "#" };
        public static readonly string[] DrawMinimap = { "Default", "x", "o", "+", "!", "#" };

        static Dictionary<string, Color> ColorSelectItem = new Dictionary<string, Color>()
        {
            {"Red", Color.Red },
            {"Blue",Color.Blue },
            {"Teal",Color.Teal },
            {"Purple",Color.Purple },
            {"Yellow",Color.Yellow },
            {"Orange",Color.Orange },
            {"Pink",Color.Pink },
            {"Gray",Color.Gray },
            {"Light Blue",Color.LightBlue },
            {"Green",Color.Green },
            {"Brown",Color.Brown }
        };
        static Dictionary<string, Color> ColorSelectBT = new Dictionary<string, Color>()
        {
            {"Red", Color.Red },
            {"Blue",Color.Blue },
            {"Teal",Color.Teal },
            {"Purple",Color.Purple },
            {"Yellow",Color.Yellow },
            {"Orange",Color.Orange },
            {"Pink",Color.Pink },
            {"Gray",Color.Gray },
            {"Light Blue",Color.LightBlue },
            {"Green",Color.Green },
            {"Brown",Color.Brown }

        };
        internal static void OnValueChanged(object sender, OnValueChangeEventArgs onValueChangeEventArgs)
        {
            if (Drawing.RenderMode == RenderMode.Dx9)
            {
                Font = new Font(            
                    Drawing.Direct3DDevice9,            
                    new FontDescription                
                    {
                        FaceName = "Arial",
                        Height = 60 + MiniMapSizeIcon,
                        OutputPrecision = FontPrecision.Default,
                        Quality = FontQuality.Default,
                        CharacterSet = FontCharacterSet.Default,
                        MipLevels = 3,
                        PitchAndFamily = FontPitchAndFamily.Default,
                        Weight = FontWeight.Black,
                    });
            }

            if (Drawing.RenderMode == RenderMode.Dx9)
            {
                HeroNameFont = new Font(                     
                    Drawing.Direct3DDevice9,                                   
                    new FontDescription
                {
                        FaceName = "Arial",
                        Height = 20 + MiniMapSizeName,
                        OutputPrecision = FontPrecision.Default,
                        Quality = FontQuality.Default,
                        CharacterSet = FontCharacterSet.Default,
                        MipLevels = 3,
                        PitchAndFamily = FontPitchAndFamily.Default,
                        Weight = FontWeight.Black,
                    });
            }
        }
        private static void DrawShadowText(Font f, string stext, int x, int y, Color color)
        {
            f.DrawText(null, stext, x + 2, y + 2, Color.Black);
            f.DrawText(null, stext, x, y, color);
        }
        //(*)
        internal static void DrawHeroPosition(EventArgs args)
        {           
            //0               
            if (MenuManager.Menu.Item("enable_minimap").GetValue<bool>())
            {                     
                    foreach (Vector2 HeroPosition_0 in Position_0.ToList())
                    DrawShadowText(Font, (DrawMinimapIcon[DrawMinimapIndex]), 
                        (int)HeroPosition_0.X - 12 - MiniMapSizeIcon / 6 - x , 
                        (int)HeroPosition_0.Y - 23 - MiniMapSizeIcon / 3 - y, HeroColor_0);                               
            }
            //1
            if (MenuManager.Menu.Item("enable_minimap").GetValue<bool>())
            {
                foreach (Vector2 HeroPosition_1 in Position_1.ToList())
                    DrawShadowText(Font, (DrawMinimapIcon[DrawMinimapIndex]), 
                        (int)HeroPosition_1.X - 12 - MiniMapSizeIcon / 6 - x, 
                        (int)HeroPosition_1.Y - 23 - MiniMapSizeIcon / 3 - y, HeroColor_1);
            }
            //2
            if (MenuManager.Menu.Item("enable_minimap").GetValue<bool>())
            {
                foreach (Vector2 HeroPosition_2 in Position_2.ToList())
                    DrawShadowText(Font, (DrawMinimapIcon[DrawMinimapIndex]), 
                        (int)HeroPosition_2.X - 12 - MiniMapSizeIcon / 6 - x, 
                        (int)HeroPosition_2.Y - 23 - MiniMapSizeIcon / 3 - y, HeroColor_2);
            }
            //3
            if (MenuManager.Menu.Item("enable_minimap").GetValue<bool>())
            {
                foreach (Vector2 HeroPosition_3 in Position_3.ToList())
                    DrawShadowText(Font, (DrawMinimapIcon[DrawMinimapIndex]), 
                        (int)HeroPosition_3.X - 12 - MiniMapSizeIcon / 6 - x, 
                        (int)HeroPosition_3.Y - 23 - MiniMapSizeIcon / 3 - y, HeroColor_3);
            }
            //4
            if (MenuManager.Menu.Item("enable_minimap").GetValue<bool>())
            {
                foreach (Vector2 HeroPosition_4 in Position_4.ToList())
                    DrawShadowText(Font, (DrawMinimapIcon[DrawMinimapIndex]), 
                        (int)HeroPosition_4.X - 12 - MiniMapSizeIcon / 6 - x, 
                        (int)HeroPosition_4.Y - 23 - MiniMapSizeIcon / 3 - y, HeroColor_4);
            }
            //5
            if (MenuManager.Menu.Item("enable_minimap").GetValue<bool>())
            {
                foreach (Vector2 HeroPosition_5 in Position_5.ToList())
                    DrawShadowText(Font, (DrawMinimapIcon[DrawMinimapIndex]), 
                        (int)HeroPosition_5.X - 12 - MiniMapSizeIcon / 6 - x, 
                        (int)HeroPosition_5.Y - 23 - MiniMapSizeIcon / 3 - y, HeroColor_5);
            }
            //6
            if (MenuManager.Menu.Item("enable_minimap").GetValue<bool>())
            {
                foreach (Vector2 HeroPosition_6 in Position_6.ToList())
                    DrawShadowText(Font, (DrawMinimapIcon[DrawMinimapIndex]), 
                        (int)HeroPosition_6.X - 12 - MiniMapSizeIcon / 6 - x, 
                        (int)HeroPosition_6.Y - 23 - MiniMapSizeIcon / 3 - y, HeroColor_6);
            }
            //7
            if (MenuManager.Menu.Item("enable_minimap").GetValue<bool>())
            {
                foreach (Vector2 HeroPosition_7 in Position_7.ToList())
                    DrawShadowText(Font, (DrawMinimapIcon[DrawMinimapIndex]), 
                        (int)HeroPosition_7.X - 12 - MiniMapSizeIcon / 6 - x, 
                        (int)HeroPosition_7.Y - 23 - MiniMapSizeIcon / 3 - y, HeroColor_7);
            }
            //8
            if (MenuManager.Menu.Item("enable_minimap").GetValue<bool>())
            {
                foreach (Vector2 HeroPosition_8 in Position_8.ToList())
                    DrawShadowText(Font, (DrawMinimapIcon[DrawMinimapIndex]), 
                        (int)HeroPosition_8.X - 12 - MiniMapSizeIcon / 6 - x, 
                        (int)HeroPosition_8.Y - 23 - MiniMapSizeIcon / 3 - y, HeroColor_8);
            }
            //9
            if (MenuManager.Menu.Item("enable_minimap").GetValue<bool>())
            {
                foreach (Vector2 HeroPosition_9 in Position_9.ToList())
                    DrawShadowText(Font, (DrawMinimapIcon[DrawMinimapIndex]), 
                        (int)HeroPosition_9.X - 12 - MiniMapSizeIcon / 6 - x, 
                        (int)HeroPosition_9.Y - 23 - MiniMapSizeIcon / 3 - y, HeroColor_9);
            }
        }
        //Name
        internal static void DrawHeroNamePosition(EventArgs args)
        {    
            //0        
            if (MenuManager.Menu.Item("enable_minimap").GetValue<bool>())
            {
                foreach (Vector2 HeroNamePosition_0 in NamePosition_0.ToList())
                    DrawShadowText(HeroNameFont, HeroName_0, 
                        (int)HeroNamePosition_0.X - 9 - HeroNamePos_0 + Recalibrate_X, 
                        (int)HeroNamePosition_0.Y + Recalibrate_Y, HeroNameColor_0);
            }
            //1
            if (MenuManager.Menu.Item("enable_minimap").GetValue<bool>())
            {
                foreach (Vector2 HeroNamePosition_1 in NamePosition_1.ToList())
                    DrawShadowText(HeroNameFont, HeroName_1, 
                        (int)HeroNamePosition_1.X - 9 - HeroNamePos_1 + Recalibrate_X, 
                        (int)HeroNamePosition_1.Y + Recalibrate_Y, HeroNameColor_1);
            }
            //2
            if (MenuManager.Menu.Item("enable_minimap").GetValue<bool>())
            {
                foreach (Vector2 HeroNamePosition_2 in NamePosition_2.ToList())
                    DrawShadowText(HeroNameFont, HeroName_2, 
                        (int)HeroNamePosition_2.X - 9 - HeroNamePos_2 + Recalibrate_X, 
                        (int)HeroNamePosition_2.Y + Recalibrate_Y, HeroNameColor_2);
            }
            //3
            if (MenuManager.Menu.Item("enable_minimap").GetValue<bool>())
            {
                foreach (Vector2 HeroNamePosition_3 in NamePosition_3.ToList())
                    DrawShadowText(HeroNameFont, HeroName_3, 
                        (int)HeroNamePosition_3.X - 9 - HeroNamePos_3 + Recalibrate_X, 
                        (int)HeroNamePosition_3.Y + Recalibrate_Y, HeroNameColor_3);
            }
            //4
            if (MenuManager.Menu.Item("enable_minimap").GetValue<bool>())
            {
                foreach (Vector2 HeroNamePosition_4 in NamePosition_4.ToList())
                    DrawShadowText(HeroNameFont, HeroName_4, 
                        (int)HeroNamePosition_4.X - 9 - HeroNamePos_4 + Recalibrate_X, 
                        (int)HeroNamePosition_4.Y + Recalibrate_Y, HeroNameColor_4);
            }
            //5
            if (MenuManager.Menu.Item("enable_minimap").GetValue<bool>())
            {
                foreach (Vector2 HeroNamePosition_5 in NamePosition_5.ToList())
                    DrawShadowText(HeroNameFont, HeroName_5, 
                        (int)HeroNamePosition_5.X - 9 - HeroNamePos_5 + Recalibrate_X, 
                        (int)HeroNamePosition_5.Y + Recalibrate_Y, HeroNameColor_5);
            }
            //6
            if (MenuManager.Menu.Item("enable_minimap").GetValue<bool>())
            {
                foreach (Vector2 HeroNamePosition_6 in NamePosition_6.ToList())
                    DrawShadowText(HeroNameFont, HeroName_6, 
                        (int)HeroNamePosition_6.X - 9 - HeroNamePos_6 + Recalibrate_X, 
                        (int)HeroNamePosition_6.Y + Recalibrate_Y, HeroNameColor_6);
            }
            //7
            if (MenuManager.Menu.Item("enable_minimap").GetValue<bool>())
            {
                foreach (Vector2 HeroNamePosition_7 in NamePosition_7.ToList())
                    DrawShadowText(HeroNameFont, HeroName_7, 
                        (int)HeroNamePosition_7.X - 9 - HeroNamePos_7 + Recalibrate_X, 
                        (int)HeroNamePosition_7.Y + Recalibrate_Y, HeroNameColor_7);
            }
            //8
            if (MenuManager.Menu.Item("enable_minimap").GetValue<bool>())
            {
                foreach (Vector2 HeroNamePosition_8 in NamePosition_8.ToList())
                    DrawShadowText(HeroNameFont, HeroName_8, 
                        (int)HeroNamePosition_8.X - 9 - HeroNamePos_8 + Recalibrate_X, 
                        (int)HeroNamePosition_8.Y + Recalibrate_Y, HeroNameColor_8);
            }
            //9
            if (MenuManager.Menu.Item("enable_minimap").GetValue<bool>())
            {
                foreach (Vector2 HeroNamePosition_9 in NamePosition_9.ToList())
                    DrawShadowText(HeroNameFont, HeroName_9, 
                        (int)HeroNamePosition_9.X - 9 - HeroNamePos_9 + Recalibrate_X, 
                        (int)HeroNamePosition_9.Y + Recalibrate_Y, HeroNameColor_9);
            }

            //Item
            if (MenuManager.Menu.Item("enable_minimap").GetValue<bool>())
            {
                foreach (Vector2 HeroPosition_Item in Position_Item.ToList())
                    DrawShadowText(Font, (DrawMinimapIcon[DrawMinimapIndex]), 
                        (int)HeroPosition_Item.X - 12 - MiniMapSizeIcon / 6 - x, 
                        (int)HeroPosition_Item.Y - 23 - MiniMapSizeIcon / 3 - y, 
                        ColorSelectItem[MenuManager.Menu.Item("coloritem").GetValue<StringList>().SelectedValue]);

            }

            //BT Teleport Enemy
            if (MenuManager.Menu.Item("enable_minimap").GetValue<bool>())
            {
                foreach (Vector2 HeroPosition_BT_Enemy in Position_BT_Enemy.ToList())                  
                DrawShadowText(Font, (DrawMinimapIcon[DrawMinimapIndex]), 
                    (int)HeroPosition_BT_Enemy.X - 12 - MiniMapSizeIcon / 6 - x, 
                    (int)HeroPosition_BT_Enemy.Y - 23 - MiniMapSizeIcon / 3 - y, 
                    ColorSelectBT[MenuManager.Menu.Item("colorbt").GetValue<StringList>().SelectedValue]);
                
            }
            //BT Teleport Ally
            if (MenuManager.Menu.Item("enable_minimap").GetValue<bool>())
            {
                foreach (Vector2 HeroPosition_BT_Ally in Position_BT_Ally.ToList())
                    DrawShadowText(Font, (DrawMinimapIcon[DrawMinimapIndex]),
                        (int)HeroPosition_BT_Ally.X - 12 - MiniMapSizeIcon / 6 - x,
                        (int)HeroPosition_BT_Ally.Y - 23 - MiniMapSizeIcon / 3 - y, Color.White);

            }
        }
        internal static void Remover(Vector2 val)
        {
            DelayAction.Add(Timer * 1000, () =>
            {                

                //Position
                if (Position_0.Contains(val))
                {
                    Position_0.Remove(val);
                }
                if (Position_1.Contains(val))
                {
                    Position_1.Remove(val);
                }
                if (Position_2.Contains(val))
                {
                    Position_2.Remove(val);
                }
                if (Position_3.Contains(val))
                {
                    Position_3.Remove(val);
                }
                if (Position_4.Contains(val))
                {
                    Position_4.Remove(val);
                }
                if (Position_5.Contains(val))
                {
                    Position_5.Remove(val);
                }
                if (Position_6.Contains(val))
                {
                    Position_6.Remove(val);
                }
                if (Position_7.Contains(val))
                {
                    Position_7.Remove(val);
                }
                if (Position_8.Contains(val))
                {
                    Position_8.Remove(val);
                }
                if (Position_9.Contains(val))
                {
                    Position_9.Remove(val);
                }

                //PositionItem
                if (Position_Item.Contains(val))
                {
                    Position_Item.Remove(val);
                }

                //PositionBT Enemy
                if (Position_BT_Enemy.Contains(val))
                {
                    Position_BT_Enemy.Remove(val);
                }

                //PositionBT Ally
                if (Position_BT_Ally.Contains(val))
                {
                    Position_BT_Ally.Remove(val);
                }

                //NamePosition
                if (NamePosition_0.Contains(val))
                {
                    NamePosition_0.Remove(val);
                }
                if (NamePosition_1.Contains(val))
                {
                    NamePosition_1.Remove(val);
                }
                if (NamePosition_2.Contains(val))
                {
                    NamePosition_2.Remove(val);
                }
                if (NamePosition_3.Contains(val))
                {
                    NamePosition_3.Remove(val);
                }
                if (NamePosition_4.Contains(val))
                {
                    NamePosition_4.Remove(val);
                }
                if (NamePosition_5.Contains(val))
                {
                    NamePosition_5.Remove(val);
                }
                if (NamePosition_6.Contains(val))
                {
                    NamePosition_6.Remove(val);
                }
                if (NamePosition_7.Contains(val))
                {
                    NamePosition_7.Remove(val);
                }
                if (NamePosition_8.Contains(val))
                {
                    NamePosition_8.Remove(val);
                }
                if (NamePosition_9.Contains(val))
                {
                    NamePosition_9.Remove(val);
                }
            });
        }        
        static void DrawingOnPostReset(EventArgs args)
        {
            Font.OnResetDevice();
        }
        static void DrawingOnPreReset(EventArgs args)
        {
            Font.OnLostDevice();
        }
        static void DrawingNameOnPostReset(EventArgs args)
        {
            HeroNameFont.OnResetDevice();
        }
        static void DrawingNameOnPreReset(EventArgs args)
        {
            HeroNameFont.OnLostDevice();
        }                             
    }
}
