using Ensage;
using Ensage.Common;
using System;

namespace LoneDruidSharpRewrite
{
    class Bootstrap
    {
        private readonly LoneDruidSharp lonedruidsharp;

        public Bootstrap()
        {
            lonedruidsharp = new LoneDruidSharp();
        }

        public void SubscribeEvents()
        {
            Events.OnLoad += Events_Onload;
            Events.OnUpdate += Events_OnUpdate;
            Events.OnClose += Events_OnClose;
            Game.OnUpdate += Game_OnUpdate;
            //Game.OnWndProc += Game_OnWndProc;
            Drawing.OnDraw += Drawing_OnDraw;
            Player.OnExecuteOrder += Player_OnExecuteOrder;
        }

        private void Drawing_OnDraw(EventArgs args)
        {
            lonedruidsharp.OnDraw();
        }

        private void Events_Onload(object sender, EventArgs e)
        {
            lonedruidsharp.OnLoad();
        }

        private void Events_OnClose(object sender, EventArgs e)
        {
            lonedruidsharp.OnClose();
        }

        private void Game_OnUpdate(EventArgs args)
        {
            lonedruidsharp.OnUpdate_IronTalon();
            lonedruidsharp.OnUpdate_AutoMidas();
            lonedruidsharp.OnUpdate_LastHit();
            lonedruidsharp.OnUpdate_bearChase();
        }

        private void Events_OnUpdate(EventArgs args)
        {
            lonedruidsharp.Events_OnUpdate();
        }

        private void Game_OnWndProc(WndEventArgs args)
        {
            lonedruidsharp.OnWndProc(args);
        }

        private void Player_OnExecuteOrder(Player sender, ExecuteOrderEventArgs args)
        {
            if (sender.Equals(ObjectManager.LocalPlayer))
            {
                lonedruidsharp.Player_OnExecuteOrder(args);
            }
        }


    }
}
