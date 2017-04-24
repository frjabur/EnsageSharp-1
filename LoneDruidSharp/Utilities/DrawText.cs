using Ensage;
using Ensage.Common;
using Ensage.Common.Menu;
using SharpDX;
using System;

namespace LoneDruidSharpRewrite.Utilities
{
    public class DrawText
    {
        private string text;

        private Vector2 textSize;

        private readonly Sleeper sleeper;

        public DrawText()
        {
            sleeper = new Sleeper();
        }

        public Color Color { get; set; }

        public FontFlags FontFlags { get; set; }

        public Vector2 Position { get; set; }

        public Vector2 Size { get; private set; }

        

        public string Text
        {
            get
            {
                return text;
            }

            set
            {
                text = value;
                if (sleeper.Sleeping)
                {
                    return;
                }

                Size = Drawing.MeasureText(text, "Arial", textSize, FontFlags);
                sleeper.Sleep(2000);
            }
        }

        public Vector2 TextSize
        {
            get
            {
                return textSize;
            }

            set
            {
                textSize = value;
                Size = Drawing.MeasureText(text, "Arial", textSize, FontFlags);
                sleeper.Sleep(2000);
            }
        }

        public void Draw()
        {
            Drawing.DrawText(text, Position, textSize, Color, FontFlags);
        }

        public void DrawTextAutoIronTalonText(bool on)
        {
            var startPos = new Vector2(Convert.ToSingle(Drawing.Width) - 200, Convert.ToSingle(Drawing.Height * 0.50));
            text = "IronTalon" + " [" + Utils.KeyToText(Variable.MenuManager.AutoTalonMenu.GetValue<KeyBind>().Key) + "] " + (on ? "ON" : "OFF");
            Position = startPos;
            textSize = new Vector2(20);
            Color = on ? Color.Yellow : Color.Red;
            FontFlags = FontFlags.AntiAlias | FontFlags.DropShadow | FontFlags.Additive | FontFlags.Custom | FontFlags.StrikeOut;
            Draw();
        }

        public void DrawTextAutoMidasText(bool on)
        {
            var startPos = new Vector2(Convert.ToSingle(Drawing.Width) - 200, Convert.ToSingle(Drawing.Height * 0.54));
            text = "Midas" + " [" + Utils.KeyToText(Variable.MenuManager.AutoMidasMenu.GetValue<KeyBind>().Key) + "] " + (on ? "ON" : "OFF");
            Position = startPos;
            textSize = new Vector2(20);
            Color = on ? Color.Yellow : Color.Red;
            FontFlags = FontFlags.AntiAlias | FontFlags.DropShadow | FontFlags.Additive | FontFlags.Custom | FontFlags.StrikeOut;
            Draw();
        }

        public void DrawTextOnlyBearLastHitText(bool on)
        {
            var startPos = new Vector2(Convert.ToSingle(Drawing.Width) - 200, Convert.ToSingle(Drawing.Height * 0.58));
            text = "Bear Last Hit" + " [" + Utils.KeyToText(Variable.MenuManager.OnlyBearLastHitMenu.GetValue<KeyBind>().Key) + "] " + (on? "ON" : "OFF");
            Position = startPos;
            textSize = new Vector2(20);
            Color = on? Color.Yellow : Color.Red;
            FontFlags = FontFlags.AntiAlias | FontFlags.DropShadow | FontFlags.Additive | FontFlags.Custom | FontFlags.StrikeOut;
            Draw();
        }

        public void DrawTextCombinedLastHitText(bool on)
        {
            var startPos = new Vector2(Convert.ToSingle(Drawing.Width) - 200, Convert.ToSingle(Drawing.Height * 0.66));
            text = "Combined Last Hit" + " [" + Utils.KeyToText(Variable.MenuManager.CombinedLastHitMenu.GetValue<KeyBind>().Key) + "] " + (on ? "ON" : "OFF");
            Position = startPos;
            textSize = new Vector2(20);
            Color = on ? Color.Yellow : Color.Red;
            FontFlags = FontFlags.AntiAlias | FontFlags.DropShadow | FontFlags.Additive | FontFlags.Custom | FontFlags.StrikeOut;
            Draw();
        }

        public void DrawTextBearChaseText(bool on)
        {
            var startPos = new Vector2(Convert.ToSingle(Drawing.Width) - 200, Convert.ToSingle(Drawing.Height * 0.62));
            text = "Bear Chasing!" + " [" + Utils.KeyToText(Variable.MenuManager.BearChaseMenu.GetValue<KeyBind>().Key) + "] ";
            Position = startPos;
            textSize = new Vector2(20);
            Color = on? Color.HotPink : Color.Red;
            FontFlags = FontFlags.AntiAlias | FontFlags.DropShadow | FontFlags.Additive | FontFlags.Custom | FontFlags.StrikeOut;
            Draw();
        }


    }
}
