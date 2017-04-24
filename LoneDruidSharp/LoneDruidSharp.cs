using Ensage;
using Ensage.Common;
using Ensage.Common.Extensions;
using Ensage.Common.Menu;
using LoneDruidSharpRewrite.Abilities;
using LoneDruidSharpRewrite.Utilities;
using System.Collections.Generic;
using System.Linq;

namespace LoneDruidSharpRewrite
{
    public class LoneDruidSharp
    {

        private AutoIronTalon autoIronTalon;

        private AutoMidas autoMidas;

        //private SummonSpiritBear summonSpiritBear;

        private static Dictionary<float, Features.Orbwalk.Orbwalker> orbwalkerDictionary = new Dictionary<float, Features.Orbwalk.Orbwalker>();

        private Lasthit lasthit;

        //private Rabid rabid;

        private Move move;

        private bool pause;

        private TargetFind targetFind;

        private DrawText drawText;

        public LoneDruidSharp()
        {
            autoIronTalon = new AutoIronTalon();
            autoMidas = new AutoMidas();
            lasthit = new Lasthit();
            drawText = new DrawText();
        }

        private static Hero Me
        {
            get
            {
                return Variable.Hero;
            }
        }

        private static Unit Bear
        {
            get
            {
                return Variable.Bear;
            }
        }

        private Hero Target
        {
            get
            {
                return targetFind.Target;
            }
        }

        private bool inBearLastHitMode
        {
            get
            {
                return Variable.MenuManager.OnlyBearLastHitModeOn;
            }
        }

        private bool inCombinedLastHitMode
        {
            get
            {
                return Variable.MenuManager.CombineLastHitModeOn;
            }
        }

        public void OnClose()
        {
            pause = true;
        }

        public void OnDraw()
        {
            if(Variable.Hero == null || !Variable.Hero.IsValid || !Variable.Hero.IsAlive)
            {
                return;
            }
            //draw target
            
            //draw default menu
            drawText.DrawTextOnlyBearLastHitText(Variable.OnlyBearLastHitActive);
            //drawText.DrawTextCombinedLastHitText(Variable.CombinedLastHitActive);
            drawText.DrawTextAutoIronTalonText(Variable.AutoTalonActive);
            drawText.DrawTextAutoMidasText(Variable.AutoMidasModeOn);
            drawText.DrawTextBearChaseText(Variable.BearChaseModeOn);
            if (Variable.BearChaseModeOn)
            {
                targetFind.DrawTarget();
            }
            


        }       

        public void OnLoad()
        {
            Variable.Hero = ObjectManager.LocalHero;
            pause = Variable.Hero.ClassId != ClassId.CDOTA_Unit_Hero_LoneDruid;
            if (pause)
            {
                return;
            }
            Variable.Hero = ObjectManager.LocalHero;
            Variable.MenuManager = new MenuManager(Me.Name);
            Variable.EnemyTeam = Me.GetEnemyTeam();
            Variable.MenuManager.Menu.AddToMainMenu();
            Variable.SummonSpiritBear = new SummonSpiritBear(Me.Spellbook.Spell1);
            Variable.Rabid = new Rabid(Me.Spellbook.Spell2); 
            targetFind = new TargetFind();
            move = new Move(Me);
            autoMidas = new AutoMidas();
            lasthit = new Lasthit();
            autoIronTalon = new AutoIronTalon();
            //lasthit = new Lasthit();


            Game.PrintMessage(
                "LoneDruidSharp" + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version + " loaded");
        }

        #region auto iron talon
        public void OnUpdate_IronTalon()
        {
            if (!pause)
            {
                pause = Game.IsPaused;
            }
            Variable.Bear = ObjectManager.GetEntities<Unit>().Where(unit => unit.ClassId.Equals(ClassId.CDOTA_Unit_SpiritBear)).FirstOrDefault();

            if (pause || Variable.Hero == null || !Variable.Hero.IsValid || !Variable.Hero.IsAlive)
            {
                pause = Game.IsPaused;
                return;
            }
            var CanIronTalon = Variable.MenuManager.AutoTalonActive && !Variable.BearChaseModeOn;

            if (CanIronTalon)
            {
                autoIronTalon.Execute();
            }        
        }
        #endregion

        #region automidas, defaulted
        public void OnUpdate_AutoMidas()
        {
            if (pause || Variable.Hero == null || !Variable.Hero.IsValid || !Variable.Hero.IsAlive)
            {
                return;
            }
            if (pause || Variable.Bear == null || !Variable.Bear.IsValid || !Variable.Bear.IsAlive)
            {
                return;
            }
            Variable.Bear = ObjectManager.GetEntities<Unit>().Where(unit => unit.ClassId.Equals(ClassId.CDOTA_Unit_SpiritBear)).FirstOrDefault();
            if (!Variable.AutoMidasModeOn) return;
            if (Variable.BearChaseModeOn) return;
            autoMidas.Execute();
            
        }
        #endregion

        #region lasthit
        public void OnUpdate_LastHit()
        {
            if (!pause)
            {
                pause = Game.IsPaused;
            }
            if (pause || Variable.Hero == null || !Variable.Hero.IsValid || !Variable.Hero.IsAlive)
            {
                return;
            }
            if (pause || Variable.Bear == null || !Variable.Bear.IsValid || !Variable.Bear.IsAlive)
            {
                pause = Game.IsPaused;
                return;
            }

            var onlyBearLastHitModeOn = Variable.OnlyBearLastHitActive && !Variable.CombinedLastHitActive;
            if (!onlyBearLastHitModeOn) return;
            
            var combinedLastHitModeOn = Variable.CombinedLastHitActive && !Variable.OnlyBearLastHitActive;
            if (onlyBearLastHitModeOn)
            {
                if (Utils.SleepCheck("onlybearlasthit"))
                {
                    lasthit.OnlyBearLastHitExecute();
                    Utils.Sleep(100, "onlybearlasthit");

                };               
            }

            if (combinedLastHitModeOn)
            {
                if (Utils.SleepCheck("combinedlasthit"))
                {
                    lasthit.CombinedLastHitExecute();
                    Utils.Sleep(100, "combinedlasthit");
                }
            }
            




        }
        #endregion

        #region bear chase
        public void OnUpdate_bearChase()
        {
            if (!pause)
            {
                pause = Game.IsPaused;
            }
            
            if (pause || Variable.Bear == null || !Variable.Bear.IsValid || !Variable.Bear.IsAlive)
            {
                pause = Game.IsPaused;
                return;
            }
            //re openup Auto Midas and Auto IronTalon if no enemy is nearby me and Bear
            /*
            var anyEnemyNearMe = ObjectManager.GetEntities<Hero>().Any(x => x.IsAlive && x.Team != Variable.Hero.Team
                                                        && x.Distance2D(Me) <= 1000);
            var anyEnemyNearBear = ObjectManager.GetEntities<Hero>().Any(x => x.IsAlive && x.Team != Variable.Hero.Team
                                                        && x.Distance2D(Bear) <= 1000);

            if(!anyEnemyNearBear && !anyEnemyNearMe)
            {
                if (!Variable.AutoTalonActive)
                {
                    //Variable.MenuManager.AutoTalonMenu.SetValue(new KeyBind(Variable.MenuManager.AutoTalonMenu.GetValue<KeyBind>().Key, KeyBindType.Toggle, true));
                }
                if (!Variable.AutoMidasModeOn)
                {
                    //Variable.MenuManager.AutoMidasMenu.SetValue(new KeyBind(Variable.MenuManager.AutoMidasMenu.GetValue<KeyBind>().Key, KeyBindType.Toggle, true));
                }
            }
            if (Bear == null) return;
            if (!Variable.BearChaseModeOn) return;
            targetFind.Find();
            if (Target == null) return;                              
            */
            //UnitOrbwalk.Orbwalk(Bear, Target);
        }
        #endregion

        public void OnWndProc(WndEventArgs args)
        {
            if (pause || Variable.Hero == null || !Variable.Hero.IsValid || !Variable.Hero.IsAlive)
            {
                return;
            }
            
            if (Target == null || !Target.IsValid)
            {
                return;
            }
            
        }

        public void Events_OnUpdate()
        {
            if (pause || Variable.Hero == null || !Variable.Hero.IsValid || !Variable.Hero.IsAlive)
            {
                return;
            }

            
            if (!Variable.BearChaseModeOn) return;
            if (Bear == null) return;
            targetFind.Find();
            if (Target == null || !Target.IsValid)
            {
                return;
            }
            targetFind.LockTarget();
            if (Target == null || !Target.IsValid) return;
            Features.Orbwalk.Orbwalker orbwalker = new Features.Orbwalk.Orbwalker(Bear);
            if (!orbwalkerDictionary.TryGetValue(Bear.Handle, out orbwalker))
            {
                orbwalker = new Features.Orbwalk.Orbwalker(Bear);
                orbwalkerDictionary.Add(Bear.Handle, orbwalker);
            }
            orbwalker.OrbwalkOn(Target, 0, 0, false, true);
            if (Me.IsRanged)
            {
                Orbwalking.Orbwalk(Target, 0, 0, false, true);
            }
            else
            {
                if (Bear.Distance2D(Me) > 900)
                {
                    if (Utils.SleepCheck("Move"))
                    {
                        Me.Move(Bear.Position);
                        Utils.Sleep(500, "Move");
                    }
                }
                else
                {
                    if (Utils.SleepCheck("Hold"))
                    {
                        Me.Hold();
                        Utils.Sleep(1000, "Hold");
                    }
                }
            }
            if (Utils.SleepCheck("attack")) {
                Bear.Attack(Target);
                Utils.Sleep(300, "attack");
            }
        }

        public void Player_OnExecuteOrder(ExecuteOrderEventArgs args)
        {
            if (pause || Variable.Hero == null || !Variable.Hero.IsValid || !Variable.Hero.IsAlive)
            {
                return;
            }
            lasthit.resetAutoAttackMode();
            if (args.OrderId == OrderId.AttackTarget)
            {
                targetFind.UnlockTarget();
                targetFind.Find();
            }
            else {
                targetFind.UnlockTarget();
            }
            // re lock target
            if (Variable.BearChaseModeOn)
            {
                //should disable Auto Iron Talon and Midas until Noenemy is nearby
                if (Variable.AutoTalonActive)
                {
                    Variable.MenuManager.AutoTalonMenu.SetValue(new KeyBind(Variable.MenuManager.AutoTalonMenu.GetValue<KeyBind>().Key, KeyBindType.Toggle, false));
                }
                if (Variable.AutoMidasModeOn)
                {
                    Variable.MenuManager.AutoMidasMenu.SetValue(new KeyBind(Variable.MenuManager.AutoMidasMenu.GetValue<KeyBind>().Key, KeyBindType.Toggle, false));
                }
                //targetFind.UnlockTarget();
                //targetFind.Find();
                //
            }
        }

    }
}
