using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Ensage.Common;
using Ensage.Common.Extensions;
using Ensage.Common.Threading;
using SharpDX;

namespace VisibleByEnemyPlus
{
    using System.Collections.Generic;
    using Ensage;
    using Ensage.Common.Menu;
    using log4net;
    using PlaySharp.Toolkit.Logging;

    internal class Program
    {
        #region Static Fields

        private static readonly ILog Log = AssemblyLogs.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private static Dictionary<Unit, ParticleEffect> _effects = new Dictionary<Unit, ParticleEffect>();

        private static readonly Menu Menu = new Menu("VisibleByEnemyPlus", "visibleByEnemyplus", true);

        private static int red => Menu.Item("red").GetValue<Slider>().Value;

        private static int green => Menu.Item("green").GetValue<Slider>().Value;

        private static int blue => Menu.Item("blue").GetValue<Slider>().Value;

        private static int alpha => Menu.Item("alpha").GetValue<Slider>().Value;

        private static int GetEffectId => Menu.Item("type").GetValue<StringList>().SelectedIndex;

        private static readonly string[] Effects =
        {
        "particles/items_fx/aura_shivas.vpcf",
        "materials/ensage_ui/particles/visiblebyenemy.vpcf",
        "materials/ensage_ui/particles/vbe.vpcf",
        "materials/ensage_ui/particles/visiblebyenemy_omniknight.vpcf",
        "materials/ensage_ui/particles/visiblebyenemy_assault.vpcf",
        "materials/ensage_ui/particles/visiblebyenemy_arrow.vpcf",
	    "materials/ensage_ui/particles/visiblebyenemy_mark.vpcf",
	    "materials/ensage_ui/particles/visiblebyenemy_glyph.vpcf",
	    "materials/ensage_ui/particles/visiblebyenemy_coin.vpcf",
	    "materials/ensage_ui/particles/visiblebyenemy_lightning.vpcf",
	    "materials/ensage_ui/particles/visiblebyenemy_energy_orb.vpcf",
	    "materials/ensage_ui/particles/visiblebyenemy_pentagon.vpcf",
	    "materials/ensage_ui/particles/visiblebyenemy_axis.vpcf"
        };
        private static readonly string[] EffectsName =
        {
        "Default",
        "Default MOD",
        "VBE",
        "Omniknight",
        "Assault",
	    "Arrow",
        "Mark",
	    "Glyph",
	    "Coin",
	    "Lightning",
	    "Energy Orb",
	    "Pentagon",
	    "Axis"
        };

        #endregion

        #region Public Methods and Operators

        private static void Main()
        {
            var sList = new StringList()
            {
                SList   = EffectsName, SelectedIndex = 0
            };
            //var effectType = new MenuItem("type", "EffectType").SetValue(new Slider(0,0,9));
            var effectType = new MenuItem("type", "EffectType").SetValue(sList);
            effectType.ValueChanged += Item_ValueChanged;
            Menu.AddItem(effectType);

            var item = new MenuItem("red", "Red").SetValue(new Slider(255, 0, 255)).SetFontColor(Color.Red);
            item.ValueChanged += ChangeColor;
            Menu.AddItem(item);

            item = new MenuItem("green", "Green").SetValue(new Slider(255, 0, 255)).SetFontColor(Color.Green);
            item.ValueChanged += ChangeColor;
            Menu.AddItem(item);

            item = new MenuItem("blue", "Blue").SetValue(new Slider(255, 0, 255)).SetFontColor(Color.Blue);
            item.ValueChanged += ChangeColor;
            Menu.AddItem(item);

            item = new MenuItem("alpha", "Alpha").SetValue(new Slider(255, 0, 255));
            item.ValueChanged += ChangeTrans;
            Menu.AddItem(item);

            item = new MenuItem("heroes", "Check allied heroes").SetValue(true);
            item.ValueChanged += Item_ValueChanged;
            Menu.AddItem(item);

            item = new MenuItem("wards", "Check wards").SetValue(true);
            item.ValueChanged += Item_ValueChanged;
            Menu.AddItem(item);

            item = new MenuItem("mines", "Check techies mines").SetValue(true);
            item.ValueChanged += Item_ValueChanged;
            Menu.AddItem(item);

            item = new MenuItem("units", "Check controlled units and Neutral creeps").SetValue(true);
            item.ValueChanged += Item_ValueChanged;
            Menu.AddItem(item);

            item = new MenuItem("buildings", "Check buildings").SetValue(true);
            item.ValueChanged += Item_ValueChanged;
            Menu.AddItem(item);

            Menu.AddToMainMenu();

            LoopEntities();
            Entity.OnInt32PropertyChange += Entity_OnInt32PropertyChange;
        }

        private static void ChangeColor(object sender, OnValueChangeEventArgs e)
        {
            foreach (var effect in _effects.Values.Where(x => x != null))
            {
                effect.SetControlPoint(1, new Vector3(red, green, blue));
            }
        }

        private static void ChangeTrans(object sender, OnValueChangeEventArgs e)
        {
            foreach (var effect in _effects.Values.Where(x => x != null))
            {
                effect.SetControlPoint(2, new Vector3(alpha));
                effect.Restart();
            }
        }

        #endregion

        #region Methods
        private static bool IsWard(Entity sender)
        {
            return (sender.ClassID == ClassID.CDOTA_NPC_Observer_Ward ||
                    sender.ClassID == ClassID.CDOTA_NPC_Observer_Ward_TrueSight);
        }

        private static bool IsMine(Entity sender)
        {
            return sender.ClassID == ClassID.CDOTA_NPC_TechiesMines;
        }

        private static bool IsUnit(Unit sender)
        {
            return !(sender is Hero) && !(sender is Building) && 
                ((sender.ClassID != ClassID.CDOTA_BaseNPC_Creep_Lane && sender.ClassID != ClassID.CDOTA_BaseNPC_Creep_Siege) || sender.IsControllable)
                            && sender.ClassID != ClassID.CDOTA_NPC_TechiesMines
                            && sender.ClassID != ClassID.CDOTA_NPC_Observer_Ward
                            && sender.ClassID != ClassID.CDOTA_NPC_Observer_Ward_TrueSight;
        }

        private static void Entity_OnInt32PropertyChange(Entity sender, Int32PropertyChangeEventArgs args)
        {
            var unit = sender as Unit;
            if (unit == null)
                return;
             
            if (args.PropertyName != "m_iTaggedAsVisibleByTeam")
                return;
            DelayAction.Add(50, () =>
            {
                try
                {
                    var player = ObjectManager.LocalPlayer;
                    var hero = ObjectManager.LocalHero;
                    if (hero == null)
                        return;

                    if (player == null || player.Team == Team.Observer ||
                        sender.Team == ObjectManager.LocalHero.GetEnemyTeam())
                        return;

                    /*Log.Debug("------------------------------------");
                    Log.Debug($"sender: {sender.Name}. hero: {sender.ClassID}");
                    Log.Debug($"team: {sender.Team}. EnemyTeam: {ObjectManager.LocalHero.GetEnemyTeam()}");
                    Log.Debug("------------------------------------");*/
                    var visible = args.NewValue == 0x1E;
                    // heroes
                    if (sender is Hero && Menu.Item("heroes").GetValue<bool>())
                    {
                        HandleEffect(unit, visible);
                    }

                    // wards
                    else if (IsWard(sender) && Menu.Item("wards").GetValue<bool>())
                    {
                        HandleEffect(unit, visible);
                    }

                    // mines
                    else if (IsMine(sender) && Menu.Item("mines").GetValue<bool>())
                    {
                        HandleEffect(unit, visible);
                    }

                    // units
                    else if (Menu.Item("units").GetValue<bool>() && IsUnit(unit))
                    {
                        HandleEffect(unit, visible);
                    }

                    // buildings
                    else if (sender is Building && Menu.Item("buildings").GetValue<bool>())
                    {
                        HandleEffect(unit, visible);
                    }
                }
                catch (Exception)
                {
                    // ignored
                }
            });
        }

        private static void LoopEntities()
        {
            var player = ObjectManager.LocalPlayer;
            if (player == null || player.Team == Team.Observer )
                return;
            var units = ObjectManager.GetEntities<Unit>().Where(x => x.Team == player.Team).ToList();
            if (Menu.Item("heroes").GetValue<bool>())
            {
                foreach (var hero in units.Where(x => x is Hero).ToList())
                {
                    HandleEffect(hero, hero.IsVisibleToEnemies);
                }
            }
            if ( Menu.Item("wards").GetValue<bool>())
            {
                foreach (var ward in units.Where(IsWard).ToList())
                {
                    HandleEffect(ward, ward.IsVisibleToEnemies);
                }
            }
            if (Menu.Item("mines").GetValue<bool>())
            {
                foreach (var mine in units.Where(IsMine).ToList())
                {
                    HandleEffect(mine, mine.IsVisibleToEnemies);
                }
            }
            if (Menu.Item("units").GetValue<bool>())
            {
                foreach (var unit in units.Where(IsUnit).ToList())
                {
                    HandleEffect(unit, unit.IsVisibleToEnemies);
                }
            }
            if (Menu.Item("buildings").GetValue<bool>())
            {
                foreach (var building in units.Where(x => x is Building).ToList())
                {
                    HandleEffect(building, building.IsVisibleToEnemies);
                }
            }
        }

        private static void HandleEffect(Unit unit, bool visible,int index=-1)
        {
            if (!unit.IsValid)
            {
                return;
            }
            if (index == -1)
                index = GetEffectId;
            if (visible && unit.IsAlive)
            {
                ParticleEffect effect;
                if (!_effects.TryGetValue(unit, out effect))
                {
                    
                    effect = unit.AddParticleEffect(Effects[index]);
                    switch (index)
                    {
                        case 0:

                            break;
                        default:
                            effect.SetControlPoint(1, new Vector3(red, green, blue));
                            effect.SetControlPoint(2, new Vector3(alpha));
                            break;
                    }
                    _effects.Add(unit, effect);
                }
            }
            else
            {
                ParticleEffect effect;
                if (_effects.TryGetValue(unit, out effect))
                {
                    effect.Dispose();
                    _effects.Remove(unit);
                }
            }
        }


        // ReSharper disable once InconsistentNaming
        private static void Item_ValueChanged(object sender, OnValueChangeEventArgs e)
        {
            var item = sender as MenuItem;
            if (item == null)
                return;

            bool hero = false, wards = false,  mines = false,units = false,buildings=false;
            switch (item.Name)
            {
                case "heroes":
                    hero = true;
                    break;
                case "wards":
                    wards = true;
                    break;
                case "mines":
                    mines = true;
                    break;
                case "units":
                    units = true;
                    break;
                case "buildings":
                    buildings = true;
                    break;
            }
            // update dictionary
            if (item.Name == "type")
            {
                /*hero = true;
                wards = true;
                mines = true;
                units = true;
                buildings = true;*/
                var index = e.GetNewValue<StringList>().SelectedIndex;
                //Game.PrintMessage(" Effect ==> " + EffectsName[index], MessageType.ChatMessage);
                foreach (var source in ObjectManager.GetEntities<Hero>().Where(x=>x.Team==ObjectManager.LocalHero.Team))
                {
                    HandleEffect(source, false);
                    if (source.IsVisible)
                        HandleEffect(source, true, index);
                }
            }
            var newDict = new Dictionary<Unit,ParticleEffect>();
            foreach (var effect in _effects)
            {
                if( hero && effect.Key is Hero )
                    effect.Value.Dispose();
                else if( wards && IsWard(effect.Key) )
                    effect.Value.Dispose();
                else if (mines && IsMine(effect.Key))
                    effect.Value.Dispose();
                else if (units && IsUnit(effect.Key))
                    effect.Value.Dispose();
                else if (buildings && effect.Key is Building)
                    effect.Value.Dispose();
                else
                    newDict.Add(effect.Key,effect.Value);
            }
            _effects = newDict;
            
        }

        #endregion
    }
}
