using Ensage;
using Ensage.Common.Menu;

namespace DotaMapPlus
{
    static class Weather
    {        
        public static void Init()
        {
            var weatherhackMenu = new Menu("Weather Hack", "Weather Hack");
            DotaMapPlus.Menu.AddSubMenu(weatherhackMenu);

            MenuItem item;
            item = new MenuItem("weather", "Selected").SetValue(new StringList(new[] {
                WeatherString.Default.ToString(),
                WeatherString.Snow.ToString(),
                WeatherString.Rain.ToString(),
                WeatherString.Moonbeam.ToString(),
                WeatherString.Pestilence.ToString(),
                WeatherString.Harvest.ToString(),
                WeatherString.Sirocco.ToString(),
                WeatherString.Spring.ToString(),
                WeatherString.Ash.ToString(),
                WeatherString.Aurora.ToString()
            }, 0));
            item.ValueChanged += Weather_ValueChanged;
            weatherhackMenu.AddItem(item);

            var t = DotaMapPlus.Menu.Item("weather").GetValue<StringList>().SelectedIndex;
            if (!Game.IsInGame) return;
            var var = Game.GetConsoleVar("cl_weather");
            var.RemoveFlags(ConVarFlags.Cheat);
            var.SetValue(t);
        }
        private enum WeatherString
        {
            Default = 0,
            Snow = 1,
            Rain = 2,
            Moonbeam = 3,
            Pestilence = 4,
            Harvest = 5,
            Sirocco = 6,
            Spring = 7,
            Ash = 8,
            Aurora = 9
        }
        private static void Weather_ValueChanged(object sender, OnValueChangeEventArgs e)
        {
            var t = e.GetNewValue<StringList>().SelectedIndex;           
            if (!Game.IsInGame) return;
            var var = Game.GetConsoleVar("cl_weather");
            var.RemoveFlags(ConVarFlags.Cheat);
            var.SetValue(t);
        }
    }
}
