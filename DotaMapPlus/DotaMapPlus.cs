using Ensage;
using Ensage.Common;
using Ensage.Common.Menu;
using SharpDX;
using System;

namespace DotaMapPlus
{
    class DotaMapPlus
	{
        internal static readonly Menu Menu = new Menu("DotaMapPlus", "DotaMapPlus", true, "dotamapplus", true).SetFontColor(Color.Aqua);
        private static readonly ConVar ZoomVar = Game.GetConsoleVar("dota_camera_distance");
        private static readonly ConVar renderVar = Game.GetConsoleVar("r_farz");
        private static readonly MenuItem zoomKey = new MenuItem("zoomKey", "Key");

        static void Main()
        {
            Game.OnWndProc += Game_OnWndProc;
            Events.OnLoad += EventsOnOnLoad;
        }
        private static void EventsOnOnLoad(object sender, EventArgs eventArgs)
        {
            var zoomhackMenu = new Menu("Zoom Hack", "Zoom Hack");
            Menu.AddSubMenu(zoomhackMenu);

            var slider = new MenuItem("distance", "Camera Distance").SetValue(new Slider(1550, 1134, 9000));
            slider.ValueChanged += Slider_ValueChanged;
            zoomhackMenu.AddItem(slider);

            var maphackMenu = new Menu("Map Hack", "Map Hack");
            Menu.AddSubMenu(maphackMenu);

            var fogenable = new MenuItem("fog_enable", "Fog Disable").SetValue(true);
            fogenable.ValueChanged += ValueChanged;
            maphackMenu.AddItem(fogenable);
            var nofiltering = new MenuItem("no_filtering", "Filtering Disable").SetValue(true);
            nofiltering.ValueChanged += ValueChanged;
            maphackMenu.AddItem(nofiltering);
            var particle = new MenuItem("particle", "Particle Hack Enable").SetValue(true);
            particle.ValueChanged += ValueChanged;
            maphackMenu.AddItem(particle);
            
            Menu.AddToMainMenu();

            PrintSuccess(">>>>>> DotaMapPlus Loaded!");
            Weather.Init();

            ZoomVar.RemoveFlags(ConVarFlags.Cheat);
            renderVar.RemoveFlags(ConVarFlags.Cheat);
            ZoomVar.SetValue(slider.GetValue<Slider>().Value);
            renderVar.SetValue(2*(slider.GetValue<Slider>().Value));

            DelayAction.Add(3000, () =>
            {
                if (Menu.Item("fog_enable").GetValue<bool>()) { Game.GetConsoleVar("fog_enable").SetValue(0); }
                if (!Menu.Item("fog_enable").GetValue<bool>()) { Game.GetConsoleVar("fog_enable").SetValue(1); }

                if (Menu.Item("no_filtering").GetValue<bool>()) { Game.GetConsoleVar("fow_client_nofiltering").SetValue(1); }
                if (!Menu.Item("no_filtering").GetValue<bool>()) { Game.GetConsoleVar("fow_client_nofiltering").SetValue(0); }

                if (Menu.Item("particle").GetValue<bool>())
                {
                    Game.GetConsoleVar("dota_use_particle_fow").SetValue(0);
                    Game.GetConsoleVar("dota_use_particle_fow_unbloated").SetValue(0);
                }
                if (!Menu.Item("particle").GetValue<bool>())
                {
                    Game.GetConsoleVar("dota_use_particle_fow").SetValue(1);
                    Game.GetConsoleVar("dota_use_particle_fow_unbloated").SetValue(1);
                }
            });
        }
        private static void ValueChanged(object sender, OnValueChangeEventArgs e)
        {
            var item = sender as MenuItem;
            if (item.Name == "fog_enable")
            {
                if (e.GetNewValue<bool>()) { Game.GetConsoleVar("fog_enable").SetValue(0); }
                if (!e.GetNewValue<bool>()) { Game.GetConsoleVar("fog_enable").SetValue(1); }
            }
            if (item.Name == "no_filtering")
            {
                if (e.GetNewValue<bool>()) { Game.GetConsoleVar("fow_client_nofiltering").SetValue(1); }
                if (!e.GetNewValue<bool>()) { Game.GetConsoleVar("fow_client_nofiltering").SetValue(0); }
            }
            if (item.Name == "particle")
            {
                if (e.GetNewValue<bool>())
                {
                    Game.GetConsoleVar("dota_use_particle_fow").SetValue(0);
                    Game.GetConsoleVar("dota_use_particle_fow_unbloated").SetValue(0);
                }
                if (!e.GetNewValue<bool>())
                {
                    Game.GetConsoleVar("dota_use_particle_fow").SetValue(1);
                    Game.GetConsoleVar("dota_use_particle_fow_unbloated").SetValue(1);
                }
            }
        }              
        private static void Slider_ValueChanged(object sender, OnValueChangeEventArgs e)
        {
            ZoomVar.SetValue(e.GetNewValue<Slider>().Value);
            renderVar.SetValue(2*(e.GetNewValue<Slider>().Value));
        }
		private static void Game_OnWndProc(WndEventArgs args)
		{
            if (args.Msg == (ulong) WindowsMessages.MOUSEWHEEL && Game.IsInGame )
            {
                if (Game.IsKeyDown(0x11))                   
                {
                    var delta = (short)((args.WParam >> 16) & 0xFFFF);
                    var zoomValue = ZoomVar.GetInt();
                    if (delta < 0) zoomValue += 50;
                    if (delta > 0) zoomValue -= 50;
                    if (zoomValue < 1134) zoomValue = 1134;
                    if (zoomValue > 9000) zoomValue = 9000;

                    ZoomVar.SetValue(zoomValue);
                    Menu.Item("distance").SetValue(new Slider(zoomValue, 1134, 9000));
                    args.Process = false;
                }
            }
        }
        private static void PrintSuccess(string text, params object[] arguments)
        {
            PrintEncolored(text, ConsoleColor.Green, arguments);
        }
        private static void PrintEncolored(string text, ConsoleColor color, params object[] arguments)
        {
            var clr = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.WriteLine(text, arguments);
            Console.ForegroundColor = clr;
        }
    }
}
