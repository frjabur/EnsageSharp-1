using Ensage;
using SharpDX;

namespace BeAwarePlus
{
    public static class Helper
    {
        public static void Hero()
        {
            try
            {
                if (ObjectManager.GetPlayerById(0).Hero.Name == BeAwarePlus.HeroColor)
                {
                    DrawingMiniMap.Position_0.Add(BeAwarePlus.MiniMapPosition);
                    DrawingMiniMap.Remover(BeAwarePlus.MiniMapPosition);
                    DrawingMiniMap.HeroColor_0 = (Color)new Vector3(0.2f, 0.4588236f, 1);
                }

                if (ObjectManager.GetPlayerById(1).Hero.Name == BeAwarePlus.HeroColor)
                {
                    DrawingMiniMap.Position_1.Add(BeAwarePlus.MiniMapPosition);
                    DrawingMiniMap.Remover(BeAwarePlus.MiniMapPosition);
                    DrawingMiniMap.HeroColor_1 = (Color)new Vector3(0.4f, 1, 0.7490196f);
                }

                if (ObjectManager.GetPlayerById(2).Hero.Name == BeAwarePlus.HeroColor)
                {
                    DrawingMiniMap.Position_2.Add(BeAwarePlus.MiniMapPosition);
                    DrawingMiniMap.Remover(BeAwarePlus.MiniMapPosition);
                    DrawingMiniMap.HeroColor_2 = (Color)new Vector3(0.7490196f, 0, 0.7490196f);
                }

                if (ObjectManager.GetPlayerById(3).Hero.Name == BeAwarePlus.HeroColor)
                {
                    DrawingMiniMap.Position_3.Add(BeAwarePlus.MiniMapPosition);
                    DrawingMiniMap.Remover(BeAwarePlus.MiniMapPosition);
                    DrawingMiniMap.HeroColor_3 = (Color)new Vector3(0.9529412f, 0.9411765f, 0.04313726f);
                }

                if (ObjectManager.GetPlayerById(4).Hero.Name == BeAwarePlus.HeroColor)
                {
                    DrawingMiniMap.Position_4.Add(BeAwarePlus.MiniMapPosition);
                    DrawingMiniMap.Remover(BeAwarePlus.MiniMapPosition);
                    DrawingMiniMap.HeroColor_4 = (Color)new Vector3(1, 0.4196079f, 0);
                }
            
                if (ObjectManager.GetPlayerById(5).Hero.Name == BeAwarePlus.HeroColor)
                {
                    DrawingMiniMap.Position_5.Add(BeAwarePlus.MiniMapPosition);
                    DrawingMiniMap.Remover(BeAwarePlus.MiniMapPosition);
                    DrawingMiniMap.HeroColor_5 = (Color)new Vector3(0.9960785f, 0.5254902f, 0.7607844f);
                }

                if (ObjectManager.GetPlayerById(6).Hero.Name == BeAwarePlus.HeroColor)
                {
                    DrawingMiniMap.Position_6.Add(BeAwarePlus.MiniMapPosition);
                    DrawingMiniMap.Remover(BeAwarePlus.MiniMapPosition);
                    DrawingMiniMap.HeroColor_6 = (Color)new Vector3(0.6313726f, 0.7058824f, 0.2784314f);
                }

                if (ObjectManager.GetPlayerById(7).Hero.Name == BeAwarePlus.HeroColor)
                {
                    DrawingMiniMap.Position_7.Add(BeAwarePlus.MiniMapPosition);
                    DrawingMiniMap.Remover(BeAwarePlus.MiniMapPosition);
                    DrawingMiniMap.HeroColor_7 = (Color)new Vector3(0.3960785f, 0.8509805f, 0.9686275f);
                }

                if (ObjectManager.GetPlayerById(8).Hero.Name == BeAwarePlus.HeroColor)
                {
                    DrawingMiniMap.Position_8.Add(BeAwarePlus.MiniMapPosition);
                    DrawingMiniMap.Remover(BeAwarePlus.MiniMapPosition);
                    DrawingMiniMap.HeroColor_8 = (Color)new Vector3(0, 0.5137255f, 0.1294118f);
                }

                if (ObjectManager.GetPlayerById(9).Hero.Name == BeAwarePlus.HeroColor)
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
        public static void BTTeleportStartEnemy()
        {
            DrawingMiniMap.Position_BT_Enemy.Add(BeAwarePlus.MiniMapPosition);
            DrawingMiniMap.Remover2(BeAwarePlus.MiniMapPosition);                                                 
        }
        public static void Item()
        {
            DrawingMiniMap.Position_Item.Add(BeAwarePlus.MiniMapPosition);
            DrawingMiniMap.Remover2(BeAwarePlus.MiniMapPosition);           
        }
    }
}
