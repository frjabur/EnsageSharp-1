using Ensage;
using Ensage.Common;
using Ensage.Common.Extensions;
using System;

namespace LoneDruidSharpRewrite.Features.Orbwalk
{
    public class AttackAnimationTracker
    {
        private NetworkActivity lastUnitActivity;

        private float nextUnitAttackEnd;

        private float nextUnitAttackRelease;

        protected AttackAnimationTracker(Unit unit)
        {
            Unit = unit;
            Events.OnUpdate += Track;
        }

        public Unit Unit { get; set; }


        public bool CanAttack(Entity target = null, float bonusWindupMs = 0)
        {
            if (Unit == null || !Unit.IsValid)
            {
                return false;
            }

            var turnTime = 0d;
            if (target != null)
            {
                turnTime = Unit.GetTurnTime(target)
                           + (Math.Max(Unit.Distance2D(target) - Unit.GetAttackRange() - 100, 0)
                              / Unit.MovementSpeed);
            }

            return nextUnitAttackEnd - Game.Ping - (turnTime * 1000) - 75 + bonusWindupMs < Utils.TickCount;
        }

        public bool CanCancelAttack(float delay = 0f)
        {
            if (Unit == null || !Unit.IsValid)
            {
                return true;
            }

            var time = Utils.TickCount;
            var cancelTime = nextUnitAttackRelease - Game.Ping - delay + 50;
            return time >= cancelTime;
        }

        private void Track(EventArgs args)
        {
            if (Unit == null || !Unit.IsValid)
            {
                Events.OnUpdate -= Track;
                return;
            }

            if (!Game.IsInGame || Game.IsPaused)
            {
                return;
            }

            if (Unit.NetworkActivity == lastUnitActivity)
            {
                return;
            }

            lastUnitActivity = Unit.NetworkActivity;
            if (!Unit.IsAttacking())
            {
                if (CanCancelAttack())
                {
                    return;
                }

                lastUnitActivity = 0;
                nextUnitAttackEnd = 0;
                nextUnitAttackRelease = 0;
                return;
            }

            nextUnitAttackEnd = (float)(Utils.TickCount + (UnitDatabase.GetAttackRate(Unit) * 1000));
            nextUnitAttackRelease = (float)(Utils.TickCount + (UnitDatabase.GetAttackPoint(Unit) * 1000));

        }
    }
}
