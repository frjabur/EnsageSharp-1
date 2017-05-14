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

        //TP/////////////////////////////////
        internal static ColorBGRA HeroColorTP_0;
        internal static ColorBGRA HeroColorTP_1;
        internal static ColorBGRA HeroColorTP_2;
        internal static ColorBGRA HeroColorTP_3;
        internal static ColorBGRA HeroColorTP_4;
        internal static ColorBGRA HeroColorTP_5;
        internal static ColorBGRA HeroColorTP_6;
        internal static ColorBGRA HeroColorTP_7;
        internal static ColorBGRA HeroColorTP_8;
        internal static ColorBGRA HeroColorTP_9;

        //Spells//////////////////////////////
        internal static ColorBGRA HeroColorSpells_0;
        internal static ColorBGRA HeroColorSpells_1;
        internal static ColorBGRA HeroColorSpells_2;
        internal static ColorBGRA HeroColorSpells_3;
        internal static ColorBGRA HeroColorSpells_4;
        internal static ColorBGRA HeroColorSpells_5;
        internal static ColorBGRA HeroColorSpells_6;
        internal static ColorBGRA HeroColorSpells_7;
        internal static ColorBGRA HeroColorSpells_8;
        internal static ColorBGRA HeroColorSpells_9;

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
        internal static ColorBGRA HeroNameColorFurion;



        //TP/////////////////////////////////
        internal static List<Vector2> PositionTP_0 = new List<Vector2>();
        internal static List<Vector2> PositionTP_1 = new List<Vector2>();
        internal static List<Vector2> PositionTP_2 = new List<Vector2>();
        internal static List<Vector2> PositionTP_3 = new List<Vector2>();
        internal static List<Vector2> PositionTP_4 = new List<Vector2>();
        internal static List<Vector2> PositionTP_5 = new List<Vector2>();
        internal static List<Vector2> PositionTP_6 = new List<Vector2>();
        internal static List<Vector2> PositionTP_7 = new List<Vector2>();
        internal static List<Vector2> PositionTP_8 = new List<Vector2>();
        internal static List<Vector2> PositionTP_9 = new List<Vector2>();

        //Spells//////////////////////////////
        internal static List<Vector2> PositionSpells_0 = new List<Vector2>();
        internal static List<Vector2> PositionSpells_1 = new List<Vector2>();
        internal static List<Vector2> PositionSpells_2 = new List<Vector2>();
        internal static List<Vector2> PositionSpells_3 = new List<Vector2>();
        internal static List<Vector2> PositionSpells_4 = new List<Vector2>();
        internal static List<Vector2> PositionSpells_5 = new List<Vector2>();
        internal static List<Vector2> PositionSpells_6 = new List<Vector2>();
        internal static List<Vector2> PositionSpells_7 = new List<Vector2>();
        internal static List<Vector2> PositionSpells_8 = new List<Vector2>();
        internal static List<Vector2> PositionSpells_9 = new List<Vector2>();

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
        internal static List<Vector2> NamePositionFurion = new List<Vector2>();

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
        internal static string HeroNameFurion;

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
        internal static int HeroNamePosFurion;

        private static int x;
        private static int y;

        internal static int Recalibrate_X => MenuManager.Menu.Item("recalibrate_x").GetValue<Slider>().Value;
        internal static int Recalibrate_Y => MenuManager.Menu.Item("recalibrate_y").GetValue<Slider>().Value;
        internal static int MiniMapSizeIcon => MenuManager.Menu.Item("mini_map_size_icon").GetValue<Slider>().Value;
        internal static int MiniMapSizeName => MenuManager.Menu.Item("mini_map_size_name").GetValue<Slider>().Value;
        internal static int Timer => MenuManager.Menu.Item("timer").GetValue<Slider>().Value;

        public static void Init()
        {
            Drawing.OnEndScene += DrawPosition;
            Drawing.OnEndScene += DrawHeroNamePosition;
            Drawing.OnEndScene += DrawHeroPositionTP;
            Drawing.OnEndScene += DrawHeroPositionSpells;
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
        //(*Spells)
        internal static void DrawHeroPositionSpells(EventArgs args)
        {
            //0               
            if (MenuManager.Menu.Item("enable_minimap").GetValue<bool>())
            {
                foreach (Vector2 HeroPositionSpells_0 in PositionSpells_0.ToList())
                    DrawShadowText(Font, (DrawMinimapIcon[DrawMinimapIndex]),
                        (int)HeroPositionSpells_0.X - 12 - MiniMapSizeIcon / 6 - x,
                        (int)HeroPositionSpells_0.Y - 23 - MiniMapSizeIcon / 3 - y, HeroColorSpells_0);
            }
            //1
            if (MenuManager.Menu.Item("enable_minimap").GetValue<bool>())
            {
                foreach (Vector2 HeroPositionSpells_1 in PositionSpells_1.ToList())
                    DrawShadowText(Font, (DrawMinimapIcon[DrawMinimapIndex]),
                        (int)HeroPositionSpells_1.X - 12 - MiniMapSizeIcon / 6 - x,
                        (int)HeroPositionSpells_1.Y - 23 - MiniMapSizeIcon / 3 - y, HeroColorSpells_1);
            }
            //2
            if (MenuManager.Menu.Item("enable_minimap").GetValue<bool>())
            {
                foreach (Vector2 HeroPositionSpells_2 in PositionSpells_2.ToList())
                    DrawShadowText(Font, (DrawMinimapIcon[DrawMinimapIndex]),
                        (int)HeroPositionSpells_2.X - 12 - MiniMapSizeIcon / 6 - x,
                        (int)HeroPositionSpells_2.Y - 23 - MiniMapSizeIcon / 3 - y, HeroColorSpells_2);
            }
            //3
            if (MenuManager.Menu.Item("enable_minimap").GetValue<bool>())
            {
                foreach (Vector2 HeroPositionSpells_3 in PositionSpells_3.ToList())
                    DrawShadowText(Font, (DrawMinimapIcon[DrawMinimapIndex]),
                        (int)HeroPositionSpells_3.X - 12 - MiniMapSizeIcon / 6 - x,
                        (int)HeroPositionSpells_3.Y - 23 - MiniMapSizeIcon / 3 - y, HeroColorSpells_3);
            }
            //4
            if (MenuManager.Menu.Item("enable_minimap").GetValue<bool>())
            {
                foreach (Vector2 HeroPositionSpells_4 in PositionSpells_4.ToList())
                    DrawShadowText(Font, (DrawMinimapIcon[DrawMinimapIndex]),
                        (int)HeroPositionSpells_4.X - 12 - MiniMapSizeIcon / 6 - x,
                        (int)HeroPositionSpells_4.Y - 23 - MiniMapSizeIcon / 3 - y, HeroColorSpells_4);
            }
            //5
            if (MenuManager.Menu.Item("enable_minimap").GetValue<bool>())
            {
                foreach (Vector2 HeroPositionSpells_5 in PositionSpells_5.ToList())
                    DrawShadowText(Font, (DrawMinimapIcon[DrawMinimapIndex]),
                        (int)HeroPositionSpells_5.X - 12 - MiniMapSizeIcon / 6 - x,
                        (int)HeroPositionSpells_5.Y - 23 - MiniMapSizeIcon / 3 - y, HeroColorSpells_5);
            }
            //6
            if (MenuManager.Menu.Item("enable_minimap").GetValue<bool>())
            {
                foreach (Vector2 HeroPositionSpells_6 in PositionSpells_6.ToList())
                    DrawShadowText(Font, (DrawMinimapIcon[DrawMinimapIndex]),
                        (int)HeroPositionSpells_6.X - 12 - MiniMapSizeIcon / 6 - x,
                        (int)HeroPositionSpells_6.Y - 23 - MiniMapSizeIcon / 3 - y, HeroColorSpells_6);
            }
            //7
            if (MenuManager.Menu.Item("enable_minimap").GetValue<bool>())
            {
                foreach (Vector2 HeroPositionSpells_7 in PositionSpells_7.ToList())
                    DrawShadowText(Font, (DrawMinimapIcon[DrawMinimapIndex]),
                        (int)HeroPositionSpells_7.X - 12 - MiniMapSizeIcon / 6 - x,
                        (int)HeroPositionSpells_7.Y - 23 - MiniMapSizeIcon / 3 - y, HeroColorSpells_7);
            }
            //8
            if (MenuManager.Menu.Item("enable_minimap").GetValue<bool>())
            {
                foreach (Vector2 HeroPositionSpells_8 in PositionSpells_8.ToList())
                    DrawShadowText(Font, (DrawMinimapIcon[DrawMinimapIndex]),
                        (int)HeroPositionSpells_8.X - 12 - MiniMapSizeIcon / 6 - x,
                        (int)HeroPositionSpells_8.Y - 23 - MiniMapSizeIcon / 3 - y, HeroColorSpells_8);
            }
            //9
            if (MenuManager.Menu.Item("enable_minimap").GetValue<bool>())
            {
                foreach (Vector2 HeroPositionSpells_9 in PositionSpells_9.ToList())
                    DrawShadowText(Font, (DrawMinimapIcon[DrawMinimapIndex]),
                        (int)HeroPositionSpells_9.X - 12 - MiniMapSizeIcon / 6 - x,
                        (int)HeroPositionSpells_9.Y - 23 - MiniMapSizeIcon / 3 - y, HeroColorSpells_9);
            }
        }

        //(*TP)
        internal static void DrawHeroPositionTP(EventArgs args)
        {           
            //0               
            if (MenuManager.Menu.Item("enable_minimap").GetValue<bool>())
            {                     
                    foreach (Vector2 HeroPositionTP_0 in PositionTP_0.ToList())
                    DrawShadowText(Font, (DrawMinimapIcon[DrawMinimapIndex]), 
                        (int)HeroPositionTP_0.X - 12 - MiniMapSizeIcon / 6 - x , 
                        (int)HeroPositionTP_0.Y - 23 - MiniMapSizeIcon / 3 - y, HeroColorTP_0);                               
            }
            //1
            if (MenuManager.Menu.Item("enable_minimap").GetValue<bool>())
            {
                foreach (Vector2 HeroPositionTP_1 in PositionTP_1.ToList())
                    DrawShadowText(Font, (DrawMinimapIcon[DrawMinimapIndex]), 
                        (int)HeroPositionTP_1.X - 12 - MiniMapSizeIcon / 6 - x, 
                        (int)HeroPositionTP_1.Y - 23 - MiniMapSizeIcon / 3 - y, HeroColorTP_1);
            }
            //2
            if (MenuManager.Menu.Item("enable_minimap").GetValue<bool>())
            {
                foreach (Vector2 HeroPositionTP_2 in PositionTP_2.ToList())
                    DrawShadowText(Font, (DrawMinimapIcon[DrawMinimapIndex]), 
                        (int)HeroPositionTP_2.X - 12 - MiniMapSizeIcon / 6 - x, 
                        (int)HeroPositionTP_2.Y - 23 - MiniMapSizeIcon / 3 - y, HeroColorTP_2);
            }
            //3
            if (MenuManager.Menu.Item("enable_minimap").GetValue<bool>())
            {
                foreach (Vector2 HeroPositionTP_3 in PositionTP_3.ToList())
                    DrawShadowText(Font, (DrawMinimapIcon[DrawMinimapIndex]), 
                        (int)HeroPositionTP_3.X - 12 - MiniMapSizeIcon / 6 - x, 
                        (int)HeroPositionTP_3.Y - 23 - MiniMapSizeIcon / 3 - y, HeroColorTP_3);
            }
            //4
            if (MenuManager.Menu.Item("enable_minimap").GetValue<bool>())
            {
                foreach (Vector2 HeroPositionTP_4 in PositionTP_4.ToList())
                    DrawShadowText(Font, (DrawMinimapIcon[DrawMinimapIndex]), 
                        (int)HeroPositionTP_4.X - 12 - MiniMapSizeIcon / 6 - x, 
                        (int)HeroPositionTP_4.Y - 23 - MiniMapSizeIcon / 3 - y, HeroColorTP_4);
            }
            //5
            if (MenuManager.Menu.Item("enable_minimap").GetValue<bool>())
            {
                foreach (Vector2 HeroPositionTP_5 in PositionTP_5.ToList())
                    DrawShadowText(Font, (DrawMinimapIcon[DrawMinimapIndex]), 
                        (int)HeroPositionTP_5.X - 12 - MiniMapSizeIcon / 6 - x, 
                        (int)HeroPositionTP_5.Y - 23 - MiniMapSizeIcon / 3 - y, HeroColorTP_5);
            }
            //6
            if (MenuManager.Menu.Item("enable_minimap").GetValue<bool>())
            {
                foreach (Vector2 HeroPositionTP_6 in PositionTP_6.ToList())
                    DrawShadowText(Font, (DrawMinimapIcon[DrawMinimapIndex]), 
                        (int)HeroPositionTP_6.X - 12 - MiniMapSizeIcon / 6 - x, 
                        (int)HeroPositionTP_6.Y - 23 - MiniMapSizeIcon / 3 - y, HeroColorTP_6);
            }
            //7
            if (MenuManager.Menu.Item("enable_minimap").GetValue<bool>())
            {
                foreach (Vector2 HeroPositionTP_7 in PositionTP_7.ToList())
                    DrawShadowText(Font, (DrawMinimapIcon[DrawMinimapIndex]), 
                        (int)HeroPositionTP_7.X - 12 - MiniMapSizeIcon / 6 - x, 
                        (int)HeroPositionTP_7.Y - 23 - MiniMapSizeIcon / 3 - y, HeroColorTP_7);
            }
            //8
            if (MenuManager.Menu.Item("enable_minimap").GetValue<bool>())
            {
                foreach (Vector2 HeroPositionTP_8 in PositionTP_8.ToList())
                    DrawShadowText(Font, (DrawMinimapIcon[DrawMinimapIndex]), 
                        (int)HeroPositionTP_8.X - 12 - MiniMapSizeIcon / 6 - x, 
                        (int)HeroPositionTP_8.Y - 23 - MiniMapSizeIcon / 3 - y, HeroColorTP_8);
            }
            //9
            if (MenuManager.Menu.Item("enable_minimap").GetValue<bool>())
            {
                foreach (Vector2 HeroPositionTP_9 in PositionTP_9.ToList())
                    DrawShadowText(Font, (DrawMinimapIcon[DrawMinimapIndex]), 
                        (int)HeroPositionTP_9.X - 12 - MiniMapSizeIcon / 6 - x, 
                        (int)HeroPositionTP_9.Y - 23 - MiniMapSizeIcon / 3 - y, HeroColorTP_9);
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
        }
        internal static void DrawPosition(EventArgs args)
        {
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

            //Furion
            if (MenuManager.Menu.Item("enable_minimap").GetValue<bool>())
            {
                foreach (Vector2 HeroNamePositionFurion in NamePositionFurion.ToList())
                    DrawShadowText(HeroNameFont, HeroNameFurion,
                        (int)HeroNamePositionFurion.X - 9 - HeroNamePosFurion + Recalibrate_X,
                        (int)HeroNamePositionFurion.Y + Recalibrate_Y, HeroNameColorFurion);
            }
        }
        internal static void Remover(Vector2 val)
        {
            DelayAction.Add(Timer * 1000, () =>
            {                
                //Position
                if (PositionTP_0.Contains(val)) { PositionTP_0.Remove(val); }
                if (PositionTP_1.Contains(val)) { PositionTP_1.Remove(val); }
                if (PositionTP_2.Contains(val)) { PositionTP_2.Remove(val); }
                if (PositionTP_3.Contains(val)) { PositionTP_3.Remove(val); }
                if (PositionTP_4.Contains(val)) { PositionTP_4.Remove(val); }
                if (PositionTP_5.Contains(val)) { PositionTP_5.Remove(val); }
                if (PositionTP_6.Contains(val)) { PositionTP_6.Remove(val); }
                if (PositionTP_7.Contains(val)) { PositionTP_7.Remove(val); }
                if (PositionTP_8.Contains(val)) { PositionTP_8.Remove(val); }
                if (PositionTP_9.Contains(val)) { PositionTP_9.Remove(val); }

                //PositionTP
                if (PositionSpells_0.Contains(val)) { PositionSpells_0.Remove(val); }
                if (PositionSpells_1.Contains(val)) { PositionSpells_1.Remove(val); }
                if (PositionSpells_2.Contains(val)) { PositionSpells_2.Remove(val); }
                if (PositionSpells_3.Contains(val)) { PositionSpells_3.Remove(val); }
                if (PositionSpells_4.Contains(val)) { PositionSpells_4.Remove(val); }
                if (PositionSpells_5.Contains(val)) { PositionSpells_5.Remove(val); }
                if (PositionSpells_6.Contains(val)) { PositionSpells_6.Remove(val); }
                if (PositionSpells_7.Contains(val)) { PositionSpells_7.Remove(val); }
                if (PositionSpells_8.Contains(val)) { PositionSpells_8.Remove(val); }
                if (PositionSpells_9.Contains(val)) { PositionSpells_9.Remove(val); }

                //NamePosition
                if (NamePosition_0.Contains(val)) { NamePosition_0.Remove(val); }
                if (NamePosition_1.Contains(val)) { NamePosition_1.Remove(val); }
                if (NamePosition_2.Contains(val)) { NamePosition_2.Remove(val); }
                if (NamePosition_3.Contains(val)) { NamePosition_3.Remove(val); }
                if (NamePosition_4.Contains(val)) { NamePosition_4.Remove(val); }
                if (NamePosition_5.Contains(val)) { NamePosition_5.Remove(val); }
                if (NamePosition_6.Contains(val)) { NamePosition_6.Remove(val); }
                if (NamePosition_7.Contains(val)) { NamePosition_7.Remove(val); }
                if (NamePosition_8.Contains(val)) { NamePosition_8.Remove(val); }
                if (NamePosition_9.Contains(val)) { NamePosition_9.Remove(val); }
            });
        }
        internal static void Remover2(Vector2 val)
        {
            DelayAction.Add(Timer * 1000, () =>
            {
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

                //Furion
                if (NamePositionFurion.Contains(val))
                {
                    NamePositionFurion.Remove(val);
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
