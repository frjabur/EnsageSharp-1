using Ensage;
using Ensage.Common.Extensions;
using System;

namespace LoneDruidSharpRewrite.Features.Orbwalk
{
    public class Attacker
    {
        #region Fields

        /// <summary>
        ///     The attack.
        /// </summary>
        private readonly Action<Unit> attack;

        /// <summary>
        ///     The use modifiers.
        /// </summary>
        private bool useModifier;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="Attacker" /> class.
        /// </summary>
        /// <param name="unit">
        ///     The unit.
        /// </param>
        public Attacker(Unit unit)
        {
            Unit = unit;
            switch (unit.ClassId)
            {
                case ClassId.CDOTA_Unit_Hero_Clinkz:
                    AttackModifier = unit.Spellbook.Spell2;
                    attack = (target) =>
                    {
                        if (useModifier && Unit.CanCast() && AttackModifier.Level > 0
                            && unit.Mana > AttackModifier.ManaCost)
                        {
                            AttackModifier.UseAbility(target);
                            return;
                        }

                        Unit.Attack(target);
                    };
                    break;
                case ClassId.CDOTA_Unit_Hero_DrowRanger:
                    AttackModifier = unit.Spellbook.Spell1;
                    attack = (target) =>
                    {
                        if (useModifier && Unit.CanCast() && AttackModifier.Level > 0
                            && unit.Mana > AttackModifier.ManaCost)
                        {
                            AttackModifier.UseAbility(target);
                            return;
                        }

                        Unit.Attack(target);
                    };
                    break;
                case ClassId.CDOTA_Unit_Hero_Viper:
                    AttackModifier = unit.Spellbook.SpellQ;
                    attack = (target) =>
                    {
                        if (useModifier && Unit.CanCast() && AttackModifier.Level > 0
                            && unit.Mana > AttackModifier.ManaCost)
                        {
                            AttackModifier.UseAbility(target);
                            return;
                        }

                        Unit.Attack(target);
                    };
                    break;
                case ClassId.CDOTA_Unit_Hero_Huskar:
                    AttackModifier = unit.Spellbook.Spell2;
                    attack = (target) =>
                    {
                        if (useModifier && Unit.CanCast() && AttackModifier.Level > 0
                            && Unit.Health > Unit.MaximumHealth * 0.35)
                        {
                            AttackModifier.UseAbility(target);
                            return;
                        }

                        Unit.Attack(target);
                    };
                    break;
                case ClassId.CDOTA_Unit_Hero_Silencer:
                    AttackModifier = unit.Spellbook.Spell2;
                    attack = (target) =>
                    {
                        if (useModifier && Unit.CanCast() && AttackModifier.Level > 0
                            && Unit.Mana > AttackModifier.ManaCost)
                        {
                            AttackModifier.UseAbility(target);
                            return;
                        }

                        Unit.Attack(target);
                    };
                    break;
                case ClassId.CDOTA_Unit_Hero_Jakiro:
                    AttackModifier = unit.Spellbook.Spell3;
                    attack = (target) =>
                    {
                        if (useModifier && Unit.CanCast() && AttackModifier.Level > 0
                            && AttackModifier.CanBeCasted())
                        {
                            AttackModifier.UseAbility(target);
                            return;
                        }

                        Unit.Attack(target);
                    };
                    break;
                case ClassId.CDOTA_Unit_Hero_Obsidian_Destroyer:
                    AttackModifier = unit.Spellbook.Spell1;
                    attack = (target) =>
                    {
                        if (useModifier && Unit.CanCast() && AttackModifier.Level > 0
                            && Unit.Mana > AttackModifier.ManaCost)
                        {
                            AttackModifier.UseAbility(target);
                            return;
                        }

                        Unit.Attack(target);
                    };
                    break;
                case ClassId.CDOTA_Unit_Hero_Enchantress:
                    AttackModifier = unit.Spellbook.Spell4;
                    attack = (target) =>
                    {
                        if (useModifier && Unit.CanCast() && AttackModifier.Level > 0
                            && Unit.Mana > AttackModifier.ManaCost)
                        {
                            AttackModifier.UseAbility(target);
                            return;
                        }

                        Unit.Attack(target);
                    };
                    break;
                default:
                    attack = (target) =>
                    {
                        if (target == null)
                        {
                            return;
                        }

                        Unit.Attack(target);
                    };
                    break;
            }
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets or sets the attack modifier.
        /// </summary>
        public Ability AttackModifier { get; set; }

        /// <summary>
        ///     Gets or sets the unit.
        /// </summary>
        public Unit Unit { get; set; }

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
        public void Attack(Unit target, bool useModifier = true)
        {
            this.useModifier = useModifier;
            attack.Invoke(target);
        }

        #endregion
    }
}
