using Ensage;
using Ensage.Common;
using Ensage.Common.Extensions;
using System.Linq;

namespace LoneDruidSharpRewrite.Utilities
{
    public class AutoIronTalon
    {
        private Item ironTalon;

        private Unit ironTalonTarget;

        private Hero me
        {
            get
            {
                return Variable.Hero;
            }
        }

        private Unit bear
        {
            get
            {
                return Variable.Bear;
            }
        }

        public AutoIronTalon()
        {
            //FindItems();
            //getIronTalonUnit();
            //ObjectManager.OnAddEntity += ObjectManager_OnAddEntity;
            //ObjectManager.OnRemoveEntity += ObjectManager_OnRemoveEntity;
        }

        public bool HasIronTalonOn(Unit unit)
        {
            return unit.Inventory.Items.Any(x => x.ClassId == ClassId.CDOTA_Item_Iron_Talon && x.CanBeCasted());        
        }

        public void FindIronTalon(Unit unit)
        {
            if (HasIronTalonOn(unit))
            {
                ironTalon = unit.FindItem("item_iron_talon");
            }
            else
            {
                ironTalon = null;
            }
        }

        public void FindIronTalonTarget(Unit src)
        {
            if (src == null) return;
            if (!HasIronTalonOn(src)) return;
            FindIronTalon(src);
            if (ironTalon == null) return;
            // get the highest current hp unit
            var Target = ObjectManager.GetEntities<Unit>().Where(x =>
                                       !x.IsMagicImmune() && x.Team != Variable.Hero.Team &&
                                       (x.ClassId == ClassId.CDOTA_BaseNPC_Creep_Lane ||
                                        x.ClassId == ClassId.CDOTA_BaseNPC_Creep_Siege ||
                                        x.ClassId == ClassId.CDOTA_BaseNPC_Creep_Neutral) && x.IsSpawned && x.IsAlive
                                        && x.Distance2D(src) <= 350 + 100)
                                       .OrderByDescending(x => 0.4 * x.Health / x.MaximumHealth).FirstOrDefault();
            if(Target == null)
            {
                ironTalonTarget = null;
            }
            else
            {
                ironTalonTarget = Target;
            }

        }

        public void Use(Item irontalon, Unit src, Unit target)
        {
            if (src == null) return;
            if (irontalon == null) return;
            var UseCond = !src.IsChanneling() && src.IsAlive;
            if (!UseCond) return;
            if (target == null) return;           
            if(Utils.SleepCheck("iron talon"))
            {
                irontalon.UseAbility(target);
                Utils.Sleep(1000, "iron talon");
            }
        }

        public void Execute()
        {
            if (HasIronTalonOn(me))
            {
                FindIronTalon(me);
                FindIronTalonTarget(me);
                Use(ironTalon, me, ironTalonTarget);
            }
            if (HasIronTalonOn(bear))
            {
                FindIronTalon(bear);
                FindIronTalonTarget(bear);
                Use(ironTalon, bear, ironTalonTarget);
            }
        }


    }
}
