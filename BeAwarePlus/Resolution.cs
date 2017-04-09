using Ensage;
using Ensage.Common;
using SharpDX;
using System;

namespace BeAwarePlus
{
    class Resolution
    {
        private static Vector2 ScreenSize;
        public static void Init()
        {
            var percent = HUDInfo.RatioPercentage();
            ScreenSize = new Vector2(Drawing.Width, Drawing.Height);
            var Resolution = (ScreenSize.X + "x" + ScreenSize.Y);

            //1920x1080 (16:9)
            if (Resolution == 1920 + "x" + 1080)
            {
                MessageCreator.msgX = 256;
                MessageCreator.msgY = 128;
                MessageCreator.sizeheroX = 97;
                MessageCreator.sizeheroYspell = 55;
                MessageCreator.sizeitemX = 113;
                MessageCreator.herospellY = 62;
                MessageCreator.heroallyX = 152;
                MessageCreator.heroX = 9;
                MessageCreator.spellX = 193;
                MessageCreator.itemX = 170;
            }

            //1768x992 (16:9)
            else if (Resolution == 1768 + "x" + 992)
            {
                MessageCreator.msgX = 248;
                MessageCreator.msgY = 120;
                MessageCreator.sizeheroX = 97;
                MessageCreator.sizeheroYspell = 55;
                MessageCreator.sizeitemX = 113;
                MessageCreator.herospellY = 56;
                MessageCreator.heroallyX = 145;
                MessageCreator.heroX = 9;
                MessageCreator.spellX = 186;
                MessageCreator.itemX = 165;
            }

            //1600x900 (16:9)
            else if (Resolution == 1600 + "x" + 900)
            {
                MessageCreator.msgX = 235;
                MessageCreator.msgY = 110;
                MessageCreator.sizeheroX = 88;
                MessageCreator.sizeheroYspell = 50;
                MessageCreator.sizeitemX = 100;
                MessageCreator.herospellY = 52;
                MessageCreator.heroallyX = 140;
                MessageCreator.heroX = 8;
                MessageCreator.spellX = 177;
                MessageCreator.itemX = 157;
            }

            //1360x768 (16:9)
            else if (Resolution == 1360 + "x" + 768) 
            {
                MessageCreator.msgX = 195;
                MessageCreator.msgY = 95;
                MessageCreator.sizeheroX = 68;
                MessageCreator.sizeheroYspell = 42;
                MessageCreator.sizeitemX = 85;
                MessageCreator.herospellY = 46;
                MessageCreator.heroallyX = 121;
                MessageCreator.heroX = 7;
                MessageCreator.spellX = 146;
                MessageCreator.itemX = 130;
            }
            //1366x768 (16:9)
            else if (Resolution == 1366 + "x" + 768)
            {
                MessageCreator.msgX = 195;
                MessageCreator.msgY = 95;
                MessageCreator.sizeheroX = 68;
                MessageCreator.sizeheroYspell = 42;
                MessageCreator.sizeitemX = 85;
                MessageCreator.herospellY = 46;
                MessageCreator.heroallyX = 121;
                MessageCreator.heroX = 7;
                MessageCreator.spellX = 146;
                MessageCreator.itemX = 130;
            }

            //1280x720 (16:9)
            else if (Resolution == 1280 + "x" + 720)
            {
                MessageCreator.msgX = 190;
                MessageCreator.msgY = 90;
                MessageCreator.sizeheroX = 64;
                MessageCreator.sizeheroYspell = 39;
                MessageCreator.sizeitemX = 80;
                MessageCreator.herospellY = 44;
                MessageCreator.heroallyX = 121;
                MessageCreator.heroX = 6;
                MessageCreator.spellX = 146;
                MessageCreator.itemX = 130;
            }

            //1680x1050 (16:10)
            else if (Resolution == 1680 + "x" +1050)
            {
                MessageCreator.msgX = 256;
                MessageCreator.msgY = 128;
                MessageCreator.sizeheroX = 97;
                MessageCreator.sizeheroYspell = 55;
                MessageCreator.sizeitemX = 113;
                MessageCreator.herospellY = 62;
                MessageCreator.heroallyX = 152;
                MessageCreator.heroX = 9;
                MessageCreator.spellX = 193;
                MessageCreator.itemX = 170;
            }

            //1600x1024 (16:10)
            else if (Resolution == 1600 + "x" + 1024)
            {
                MessageCreator.msgX = 256;
                MessageCreator.msgY = 128;
                MessageCreator.sizeheroX = 92;
                MessageCreator.sizeheroYspell = 55;
                MessageCreator.sizeitemX = 110;
                MessageCreator.herospellY = 62;
                MessageCreator.heroallyX = 152;
                MessageCreator.heroX = 9;
                MessageCreator.spellX = 190;
                MessageCreator.itemX = 170;
            }

            //1440x960 (16:10)
            else if (Resolution == 1440 + "x" + 960)
            {
                MessageCreator.msgX = 220;
                MessageCreator.msgY = 110;
                MessageCreator.sizeheroX = 73;
                MessageCreator.sizeheroYspell = 48;
                MessageCreator.sizeitemX = 92;
                MessageCreator.herospellY = 53;
                MessageCreator.heroallyX = 141;
                MessageCreator.heroX = 8;
                MessageCreator.spellX = 166;
                MessageCreator.itemX = 150;
            }

            //1440x900 (16:10)
            else if (Resolution == 1440 + "x" + 900)
            {
                MessageCreator.msgX = 220;
                MessageCreator.msgY = 110;
                MessageCreator.sizeheroX = 73;
                MessageCreator.sizeheroYspell = 48;
                MessageCreator.sizeitemX = 92;
                MessageCreator.herospellY = 53;
                MessageCreator.heroallyX = 141;
                MessageCreator.heroX = 8;
                MessageCreator.spellX = 166;
                MessageCreator.itemX = 150;
            }

            //1280x800 (16:10)
            else if (Resolution == 1280 + "x" + 800)
            {
                MessageCreator.msgX = 195;
                MessageCreator.msgY = 110;
                MessageCreator.sizeheroX = 70;
                MessageCreator.sizeheroYspell = 48;
                MessageCreator.sizeitemX = 88;
                MessageCreator.herospellY = 53;
                MessageCreator.heroallyX = 120;
                MessageCreator.heroX = 7;
                MessageCreator.spellX = 142;
                MessageCreator.itemX = 130;
            }

            //1280x768 (16:10)
            else if (Resolution == 1280 + "x" + 768)
            {
                MessageCreator.msgX = 195;
                MessageCreator.msgY = 110;
                MessageCreator.sizeheroX = 70;
                MessageCreator.sizeheroYspell = 48;
                MessageCreator.sizeitemX = 88;
                MessageCreator.herospellY = 53;
                MessageCreator.heroallyX = 120;
                MessageCreator.heroX = 7;
                MessageCreator.spellX = 142;
                MessageCreator.itemX = 130;
            }

            //1280x1024 (4:3)
            else if (Resolution == 1280 + "x" + 1024)
            {
                MessageCreator.msgX = 228;
                MessageCreator.msgY = 110;
                MessageCreator.sizeheroX = 75;
                MessageCreator.sizeheroYspell = 48;
                MessageCreator.sizeitemX = 92;
                MessageCreator.herospellY = 53;
                MessageCreator.heroallyX = 146;
                MessageCreator.heroX = 8;
                MessageCreator.spellX = 174;
                MessageCreator.itemX = 159;
            }

            //1280x960 (4:3)
            else if (Resolution == 1280 + "x" + 960)
            {
                MessageCreator.msgX = 228;
                MessageCreator.msgY = 110;
                MessageCreator.sizeheroX = 75;
                MessageCreator.sizeheroYspell = 48;
                MessageCreator.sizeitemX = 92;
                MessageCreator.herospellY = 53;
                MessageCreator.heroallyX = 146;
                MessageCreator.heroX = 8;
                MessageCreator.spellX = 174;
                MessageCreator.itemX = 159;
            }
            else
            {
                Console.WriteLine(@"Your screen resolution is not supported and drawings might have wrong size (" + Resolution + ")");
                MessageCreator.msgX = 256;
                MessageCreator.msgY = 128;
                MessageCreator.sizeheroX = 97;
                MessageCreator.sizeheroYspell = 55;
                MessageCreator.sizeitemX = 113;
                MessageCreator.herospellY = 62;
                MessageCreator.heroallyX = 152;
                MessageCreator.heroX = 9;
                MessageCreator.spellX = 193;
                MessageCreator.itemX = 170;
            }
        }
    }
}
