using Ensage;
using Ensage.Common.Menu;
using SharpDX;
using System;

namespace WeatherPlus
{
    class WeatherPlus
    {
        private static bool _initialized = false;
        private static readonly Menu Menu = new Menu("WeatherPlus", "WeatherPlus", true, "weatherplus", true).SetFontColor(Color.Aqua);

        private enum Weather {
            Default         = 0,
            Snow            = 1,
            Rain            = 2,
            Moonbeam        = 3,
            Pestilence      = 4,
            Harvest         = 5,
            Sirocco         = 6,
            Spring          = 7,
            Ash             = 8,
            Aurora          = 9
        }

        static void Main(string[] args)
        {

            MenuItem item;

            item = new MenuItem("weather", "Selected").SetValue(new StringList(new[] { 
                        Weather.Default.ToString(), 
                        Weather.Snow.ToString(),
                        Weather.Rain.ToString(),
                        Weather.Moonbeam.ToString(),
                        Weather.Pestilence.ToString(),
                        Weather.Harvest.ToString(),
                        Weather.Sirocco.ToString(),
                        Weather.Spring.ToString(),
                        Weather.Ash.ToString(),
                        Weather.Aurora.ToString()
            }, 0));

            item.ValueChanged += Item_ValueChanged;
            Menu.AddItem(item);

            Menu.AddToMainMenu();
            Game.OnUpdate += Game_OnUpdate;
        }

        private static void Item_ValueChanged(object sender, OnValueChangeEventArgs e)
        {
            var t = e.GetNewValue<StringList>().SelectedIndex;
            if (!Game.IsInGame) return;
            var var = Game.GetConsoleVar("cl_weather");
            var.RemoveFlags(ConVarFlags.Cheat);
            var.SetValue(t);
        }

        private static void Game_OnUpdate(EventArgs args) {
            if (!Game.IsInGame)
            {
                _initialized = false;
                return;
            }
            
            if (_initialized) {
                return;
            }
            
            _initialized = true;
            var t = Menu.Item("weather").GetValue<StringList>().SelectedIndex;
            var var = Game.GetConsoleVar("cl_weather");
            var.RemoveFlags(ConVarFlags.Cheat);
            var.SetValue(t);
        }
    }
}
