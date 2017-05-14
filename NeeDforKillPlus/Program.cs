namespace PartialMapHack
{
    using System;
    using System.Collections.Generic;

    using Ensage;
    using Ensage.Common.Menu;

    internal class Program
    {
        private static bool loaded;

        private static readonly Menu Menu = new Menu("NeeDforKillPlus", "NeeDforKillPlus", true);

        private static void Main()
        {
            Menu.AddItem(new MenuItem("NeeDforKillPlus", "NeeDforKillPlus").SetValue(new KeyBind('0', KeyBindType.Press)));
            Menu.AddToMainMenu();
            Game.OnUpdate += Game_OnUpdate;
        }

        private static void Game_OnUpdate(EventArgs args)
        {


            if ((Game.IsKeyDown(Menu.Item("NeeDforKillPlus").GetValue<KeyBind>().Key))               
                && !Game.IsChatOpen)
            {
                loaded = false;
                return;
            }
            if (loaded)
            {
                return;
            }
            var list = new Dictionary<string, float>
                           {
                               { "fow_client_nofiltering", 0  }
                           };
            foreach (var data in list)
            {
                var var = Game.GetConsoleVar(data.Key);
                var.RemoveFlags(ConVarFlags.Cheat);
                var.SetValue(data.Value);
            }
            loaded = true;
        }      
    }
}