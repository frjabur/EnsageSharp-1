using System;

namespace BeAwarePlus
{
    public static class Sound
    {
        private static bool UseDefSound => MenuManager.Menu.Item("enable_default_sound").GetValue<bool>();

        internal static void PlaySound(string path)
        {
            if (!MenuManager.Menu.Item("enable_sound").GetValue<bool>()) return;

            var player =
            new System.Media.SoundPlayer();
            var fullpath = Environment.CurrentDirectory;

            fullpath = fullpath.Remove(fullpath.Length - 10);

            if (UseDefSound)
            {
                fullpath += @"\dota\materials\ensage_ui\sounds\default.wav";
            }
            else
            {
                fullpath += @"\dota\materials\ensage_ui\sounds\" + path;
            }
            player.SoundLocation = fullpath;
            player.Load();
            player.Play();
        }
    }
}
