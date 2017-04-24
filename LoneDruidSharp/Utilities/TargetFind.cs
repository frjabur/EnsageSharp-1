using Ensage;
using Ensage.Common;
using Ensage.Common.Extensions;
using Ensage.Common.Objects;
using SharpDX;
using System.Linq;

namespace LoneDruidSharpRewrite.Utilities
{
    public class TargetFind
    {
        private readonly DotaTexture heroIcon;

        private readonly Sleeper sleeper;

        private Vector2 iconSize;

        public bool locked;

        public TargetFind()
        {
            sleeper = new Sleeper();
            heroIcon = Drawing.GetTexture("materials/ensage_ui/miniheroes/lone_druid");
            iconSize = new Vector2(HUDInfo.GetHpBarSizeY() * 2);
        }

        public Hero Target { get; private set; }

        public void DrawTarget()
        {
            if(Target == null || !Target.IsVisible || !Target.IsAlive)
            {
                return;
            }

            Vector2 screenPosition;
            if (
                !Drawing.WorldToScreen(
                    Target.Position + new Vector3(0, 0, Target.HealthBarOffset / 3),
                    out screenPosition))
            {
                return;
            }

            screenPosition += new Vector2(-iconSize.X, 0);
            Drawing.DrawRect(screenPosition, iconSize, heroIcon);
            if (locked)
            {
                
                Drawing.DrawText(
                    "LOCKED",
                    screenPosition + new Vector2(iconSize.X + 2, 1),
                    new Vector2((float)(iconSize.X * 0.85)),
                    new Color(255, 150, 110),
                    FontFlags.AntiAlias | FontFlags.Additive);
            }

        }
        public void Find()
        {
            if (sleeper.Sleeping)
            {
                return;
            }

            if(locked && Target != null && Target.IsAlive)
            {
                return;
            }
            Target =
                Heroes.GetByTeam(Variable.EnemyTeam)
                    .Where(
                        x =>
                        x.IsValid && x.IsAlive && !x.IsIllusion && x.IsVisible
                        && x.Distance2D(Game.MousePosition) < 2000)
                    .MinOrDefault(x => x.Distance2D(Game.MousePosition));
            sleeper.Sleep(100);
        }

        public void LockTarget()
        {
            locked = true;
        }

        public void UnlockTarget()
        {
            locked = false;
        }
    }
}
