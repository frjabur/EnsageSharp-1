using Ensage;
using Ensage.Common.Extensions;
using Ensage.Common.Objects;
using Ensage.Common.Objects.UtilityObjects;
using System;
using System.Linq;

namespace BeAwarePlus
{
    internal static class HeroChecker
    {
        internal static bool Ancient_Apparition_IsHere;
        internal static bool Nyx_Assassin_IsHere;
        internal static bool Bounty_Hunter_IsHere;
        internal static bool Morphling_IsHere;
        internal static bool Clinkz_IsHere;
        internal static bool Furion_IsHere;
        internal static bool Wisp_IsHere;
        internal static bool Nevermore_IsHere;
        private static bool _loaded = false;
        private static readonly Sleeper herochecker = new Sleeper();

        internal static void Init()
        {
            Game.OnUpdate += GameOnOnUpdate;
        }
        private static void GameOnOnUpdate(EventArgs args)
        {
            if (_loaded && Heroes.All.Count >= 10)
                return;
            if (herochecker.Sleeping)
                return;
            if (Heroes.All.Count >= 10)
                _loaded = true;
            herochecker.Sleep(5000);

            if (BeAwarePlus.me == null || !BeAwarePlus.me.IsValid)
                BeAwarePlus.me = ObjectManager.LocalHero;
            var enemyTeam = Heroes.GetByTeam(BeAwarePlus.me.GetEnemyTeam());

            if (!Nyx_Assassin_IsHere)
                Nyx_Assassin_IsHere =
                enemyTeam.Any(x => x.ClassId == ClassId.CDOTA_Unit_Hero_Nyx_Assassin);

            if (!Bounty_Hunter_IsHere)
                Bounty_Hunter_IsHere =
                enemyTeam.Any(x => x.ClassId == ClassId.CDOTA_Unit_Hero_BountyHunter);

            if (!Ancient_Apparition_IsHere)
                Ancient_Apparition_IsHere =
                enemyTeam.Any(x => x.ClassId == ClassId.CDOTA_Unit_Hero_AncientApparition);

            if (!Morphling_IsHere)
                Morphling_IsHere =
                enemyTeam.Any(x => x.ClassId == ClassId.CDOTA_Unit_Hero_Morphling);

            if (!Clinkz_IsHere)
                Clinkz_IsHere =
                enemyTeam.Any(x => x.ClassId == ClassId.CDOTA_Unit_Hero_Clinkz);

            if (!Furion_IsHere)
                Furion_IsHere =
                enemyTeam.Any(x => x.ClassId == ClassId.CDOTA_Unit_Hero_Furion);

            if (!Wisp_IsHere)
                Wisp_IsHere =
                enemyTeam.Any(x => x.ClassId == ClassId.CDOTA_Unit_Hero_Wisp);

            if (!Nevermore_IsHere)
                Nevermore_IsHere =
                enemyTeam.Any(x => x.ClassId == ClassId.CDOTA_Unit_Hero_Nevermore);
        }        
    }   
}
