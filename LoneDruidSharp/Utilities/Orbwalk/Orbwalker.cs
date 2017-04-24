using Ensage;
using Ensage.Common;
using Ensage.Common.Extensions;
using SharpDX;
using System;

namespace LoneDruidSharpRewrite.Features.Orbwalk
{
    public class Orbwalker : AttackAnimationTracker
    {
        #region Fields

        /// <summary>
        ///     The attacker.
        /// </summary>
        private readonly Attacker attacker;

        /// <summary>
        ///     The attack sleeper.
        /// </summary>
        private readonly Sleeper attackSleeper;

        /// <summary>
        ///     The attack sleeper 2.
        /// </summary>
        private readonly Sleeper attackSleeper2;

        /// <summary>
        ///     The move sleeper.
        /// </summary>
        private readonly Sleeper moveSleeper;

        /// <summary>
        ///     The hero.
        /// </summary>
        private bool hero;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="AttackAnimationTracker" /> class.
        /// </summary>
        /// <param name="unit">
        ///     The unit.
        /// </param>
        public Orbwalker(Unit unit)
            : base(unit)
        {
            hero = unit.Equals(ObjectManager.LocalHero);
            attackSleeper = new Sleeper();
            moveSleeper = new Sleeper();
            attackSleeper2 = new Sleeper();
            attacker = new Attacker(unit);
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     The attack.
        /// </summary>
        /// <param name="target">
        ///     The target.
        /// </param>
        /// <param name="useModifier">
        ///     The use modifier.
        /// </param>
        public void Attack(Unit target, bool useModifier)
        {
            attacker.Attack(target, useModifier);
        }

        /// <summary>
        ///     The orbwalk.
        /// </summary>
        /// <param name="target">
        ///     The target.
        /// </param>
        /// <param name="bonusWindupMs">
        ///     The bonus windup ms.
        /// </param>
        /// <param name="bonusRange">
        ///     The bonus range.
        /// </param>
        /// <param name="attackmodifiers">
        ///     The attackmodifiers.
        /// </param>
        /// <param name="followTarget">
        ///     The follow target.
        /// </param>
        public void OrbwalkOn(
            Unit target,
            float bonusWindupMs = 0,
            float bonusRange = 0,
            bool attackmodifiers = true,
            bool followTarget = false)
        {
            OrbwalkOn(target, Game.MousePosition, bonusWindupMs, bonusRange, attackmodifiers, followTarget);
        }

        /// <summary>
        ///     The orbwalk.
        /// </summary>
        /// <param name="target">
        ///     The target.
        /// </param>
        /// <param name="movePosition">
        ///     The move Position.
        /// </param>
        /// <param name="bonusWindupMs">
        ///     The bonus windup ms.
        /// </param>
        /// <param name="bonusRange">
        ///     The bonus range.
        /// </param>
        /// <param name="attackmodifiers">
        ///     The attackmodifiers.
        /// </param>
        /// <param name="followTarget">
        ///     The follow target.
        /// </param>
        public void OrbwalkOn(
            Unit target,
            Vector3 movePosition,
            float bonusWindupMs = 0,
            float bonusRange = 0,
            bool attackmodifiers = true,
            bool followTarget = false)
        {
            if (Unit == null || !Unit.IsValid)
            {
                return;
            }

            var targetHull = 0f;
            if (target != null)
            {
                targetHull = target.HullRadius;
            }

            float distance = 0;
            if (target != null)
            {
                var pos = Prediction.InFront(
                    Unit,
                    (float)((Game.Ping / 1000) + (Unit.GetTurnTime(target.Position) * Unit.MovementSpeed)));
                distance = pos.Distance2D(target) - Unit.Distance2D(target);
            }

            var isValid = target != null && target.IsValid && target.IsAlive && target.IsVisible && !target.IsInvul()
                          && !target.HasModifiers(
                              new[] { "modifier_ghost_state", "modifier_item_ethereal_blade_slow" },
                              false)
                          && target.Distance2D(Unit)
                          <= Unit.GetAttackRange() + Unit.HullRadius + 50 + targetHull + bonusRange
                          + Math.Max(distance, 0);
            if (isValid || (target != null && Unit.IsAttacking() && Unit.GetTurnTime(target.Position) < 0.1))
            {
                var canAttack = CanAttack(target, bonusWindupMs) && !target.IsAttackImmune() && !target.IsInvul()
                                && Unit.CanAttack();
                if (canAttack && !attackSleeper.Sleeping && (!hero || Utils.SleepCheck("Orbwalk.Attack")))
                {
                    attacker.Attack(target, attackmodifiers);
                    attackSleeper.Sleep(
                        (float)
                        ((UnitDatabase.GetAttackPoint(Unit) * 1000) + (Unit.GetTurnTime(target) * 1000)
                         + Game.Ping + 100));
                    moveSleeper.Sleep(
                        (float)
                        ((UnitDatabase.GetAttackPoint(Unit) * 1000) + (Unit.GetTurnTime(target) * 1000) + 50));
                    if (!hero)
                    {
                        return;
                    }

                    Utils.Sleep(
                        (UnitDatabase.GetAttackPoint(Unit) * 1000) + (Unit.GetTurnTime(target) * 1000)
                        + Game.Ping + 100,
                        "Orbwalk.Attack");
                    Utils.Sleep(
                        (UnitDatabase.GetAttackPoint(Unit) * 1000) + (Unit.GetTurnTime(target) * 1000) + 50,
                        "Orbwalk.Move");
                    return;
                }

                if (canAttack && !attackSleeper2.Sleeping)
                {
                    attacker.Attack(target, attackmodifiers);
                    attackSleeper2.Sleep(100);
                    return;
                }
            }

            var canCancel = (CanCancelAttack() && !CanAttack(target, bonusWindupMs))
                            || (!isValid && !Unit.IsAttacking() && CanCancelAttack());
            if (!canCancel || moveSleeper.Sleeping || attackSleeper.Sleeping
                || (hero && (!Utils.SleepCheck("Orbwalk.Move") || !Utils.SleepCheck("Orbwalk.Attack"))))
            {
                return;
            }

            if (followTarget)
            {
                Unit.Follow(target);
            }
            else
            {
                Unit.Move(movePosition);
            }

            moveSleeper.Sleep(100);
        }

        #endregion
    }
}
