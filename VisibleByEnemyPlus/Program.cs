using System;
using System.Collections.Generic;
using System.Linq;
using Ensage;
using Ensage.Common;
using Ensage.Common.Extensions;
using Ensage.Common.Menu;
using SharpDX;
using SharpDX.Direct3D9;
using Ensage.Common.Extensions.SharpDX;

namespace VisibleByEnemyPlus
{
    internal class Program
    {
        #region Static Fields

        private static Dictionary<Unit, ParticleEffect> _effects = new Dictionary<Unit, ParticleEffect>();

        private static ParticleEffect effect;

        private static Font Font;
        
        private static List<Vector2> Position = new List<Vector2>();

        private static bool _loaded;

        private static readonly Menu Menu = new Menu("VisibleByEnemyPlus", "visibleByEnemyplus", true, "visiblebyenemyplus", true).SetFontColor(Color.Aqua);

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
        "materials/ensage_ui/particles/visiblebyenemy_axis.vpcf",
        "materials/ensage_ui/particles/visiblebyenemy_beam_jagged.vpcf",
        "materials/ensage_ui/particles/visiblebyenemy_beam_rainbow.vpcf",
        "materials/ensage_ui/particles/visiblebyenemy_walnut_statue.vpcf",
        "materials/ensage_ui/particles/visiblebyenemy_thin_thick.vpcf",
        "materials/ensage_ui/particles/visiblebyenemy_ring_wave.vpcf",
        "materials/ensage_ui/particles/visiblebyenemy_visible.vpcf"

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
        "Axis",
        "Beam Jagged",
        "Beam Rainbow",
        "Walnut Statue",
        "Thin Thick",
        "Ring Wave",
        "Visible"
        };

        #endregion

        #region Public Methods and Operators

        private static void Main()

        {
            Events.OnLoad += Events_OnLoad;
        }

        private static void Events_OnLoad(object sender, EventArgs e)
        {
            if (!_loaded)
            {
                _loaded = true;

                PrintSuccess(">>>>>> VisibleByEnemyPlus Loaded!");

                var sList = new StringList(EffectsName){SelectedIndex = 0};

                var effectType = new MenuItem("type", "Effect Type").SetValue(sList);
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

                item = new MenuItem("heroes", "Check Allied Heroes").SetValue(true);
                item.ValueChanged += Item_ValueChanged;
                Menu.AddItem(item);

                item = new MenuItem("wards", "Check Wards").SetValue(true);
                item.ValueChanged += Item_ValueChanged;
                Menu.AddItem(item);

                item = new MenuItem("mines", "Check Techies Mines").SetValue(true);
                item.ValueChanged += Item_ValueChanged;
                Menu.AddItem(item);
                
                item = new MenuItem("shrines", "Check Shrines").SetValue(true);
                item.ValueChanged += Item_ValueChanged;
                Menu.AddItem(item);

                item = new MenuItem("draw_minimap_shrines", "Draw Mini Map Shrines").SetValue(true);
                item.ValueChanged += Item_ValueChanged;
                Menu.AddItem(item);

                item = new MenuItem("neutrals", "Check Neutral Creeps").SetValue(true);
                item.ValueChanged += Item_ValueChanged;
                Menu.AddItem(item);

                item = new MenuItem("units", "Check Controlled Units").SetValue(true);
                item.ValueChanged += Item_ValueChanged;
                Menu.AddItem(item);

                item = new MenuItem("buildings", "Check Buildings").SetValue(false);
                item.ValueChanged += Item_ValueChanged;
                Menu.AddItem(item);

                Menu.AddToMainMenu();

                Entity.OnInt32PropertyChange += Entity_OnInt32PropertyChange;

                if (Drawing.RenderMode == RenderMode.Dx9)
                {
                    Font = new Font(
                        Drawing.Direct3DDevice9,
                        new FontDescription
                        {
                            FaceName = "Arial",
                            Height = 18,
                            OutputPrecision = FontPrecision.Default,
                            Quality = FontQuality.Default,
                            CharacterSet = FontCharacterSet.Default,
                            MipLevels = 3,
                            PitchAndFamily = FontPitchAndFamily.Default,
                            Weight = FontWeight.Black,
                        });
                }
                Drawing.OnEndScene += DrawHeroPosition;
                Drawing.OnPostReset += DrawingOnPostReset;
                Drawing.OnPreReset += DrawingOnPreReset;
            }
            LoopEntities();
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
        private static void PrintSuccess(string text, params object[] arguments)
        {
            PrintEncolored(text, ConsoleColor.Green, arguments);
        }
        private static void PrintEncolored(string text, ConsoleColor color, params object[] arguments)
        {
            var clr = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.WriteLine(text, arguments);
            Console.ForegroundColor = clr;
        }
        #endregion

        #region Methods
        private static bool IsWard(Entity sender)
        {
            return (sender.ClassId == ClassId.CDOTA_NPC_Observer_Ward ||
                sender.ClassId == ClassId.CDOTA_NPC_Observer_Ward_TrueSight);
        }

        private static bool IsMine(Entity sender)
        {
            return sender.ClassId == ClassId.CDOTA_NPC_TechiesMines;
        }

        private static bool IsShrine(Entity sender)
        {
            return sender.ClassId == ClassId.CDOTA_BaseNPC_Healer;
        }

        private static bool IsNeutral(Unit sender)
        {
            return sender.ClassId == ClassId.CDOTA_BaseNPC_Creep_Neutral;
        }

        private static bool IsUnit(Unit sender)
        {
            return !(sender is Hero) && !(sender is Building) &&
                ((sender.ClassId != ClassId.CDOTA_BaseNPC_Creep_Lane
                && sender.ClassId != ClassId.CDOTA_BaseNPC_Creep_Siege) || sender.IsControllable)
                && sender.ClassId != ClassId.CDOTA_NPC_TechiesMines
                && sender.ClassId != ClassId.CDOTA_NPC_Observer_Ward
                && sender.ClassId != ClassId.CDOTA_NPC_Observer_Ward_TrueSight
                && sender.ClassId != ClassId.CDOTA_BaseNPC_Building
                && sender.ClassId != ClassId.CDOTA_BaseNPC_Healer
                && sender.ClassId != ClassId.CDOTA_BaseNPC_Creep_Neutral;

        }
        private static void Remover(Vector2 val)
        {
            DelayAction.Add(50, () =>
            {
                if (Position.Contains(val))
                {
                    Position.Remove(val);
                }
                else
                {
                    Position.Remove(val);
                }
            });
        }
        static void DrawingOnPostReset(EventArgs args)
        {
            Font.OnResetDevice();
        }
        static void DrawingOnPreReset(EventArgs args)
        {
            Font.OnLostDevice();
        }
        internal static void DrawHeroPosition(EventArgs args)
        {
            foreach (Vector2 position in Position.ToList())
                DrawShadowText(Font, "V", (int)position.X + -8, (int)position.Y - 8, Color.Aqua);
        }
        private static void DrawShadowText(Font f, string stext, int x, int y, Color color)
        {
            f.DrawText(null, stext, x + 2, y + 2, Color.Black);
            f.DrawText(null, stext, x, y, color);
        }
        private static void Entity_OnInt32PropertyChange(Entity sender, Int32PropertyChangeEventArgs args)
        {
            var unit = sender as Unit;
            if (unit == null)
            {
                return;
            }

            if (args.PropertyName != "m_iTaggedAsVisibleByTeam")
            {
                return;
            }

            var newValue = args.NewValue;
            var oldValue = args.OldValue;
            if (newValue == oldValue)
            {
                return;
            }
            DelayAction.Add(50, () =>
            {
                try
                {
                    var player = ObjectManager.LocalPlayer;
                    var hero = ObjectManager.LocalHero;
                    if (hero == null)
                    {
                        return;
                    }

                    if (player == null || player.Team == Team.Observer ||
                        sender.Team == ObjectManager.LocalHero.GetEnemyTeam())
                    {
                        return;
                    }

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

                    // shrines
                    else if (IsShrine(sender) && Menu.Item("shrines").GetValue<bool>())
                    {
                        HandleEffect(unit, visible);
                        Shrine(unit, visible);
                    }

                    // neutrals
                    else if (IsNeutral(unit) && Menu.Item("neutrals").GetValue<bool>())
                    {
                        HandleEffect(unit, visible);
                    }

                    // units
                    else if (IsUnit(unit) && Menu.Item("units").GetValue<bool>())
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
            if (player == null || player.Team == Team.Observer)
            {
                return;
            }
            var units = ObjectManager.GetEntities<Unit>().Where(x => x.Team == player.Team).ToList();
            if (Menu.Item("heroes").GetValue<bool>())
            {
                foreach (var hero in units.Where(x => x is Hero).ToList())
                {
                    HandleEffect(hero, hero.IsVisibleToEnemies);
                }
            }
            if (Menu.Item("wards").GetValue<bool>())
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

            if (Menu.Item("shrines").GetValue<bool>())
            {
                foreach (var shrine in units.Where(IsShrine).ToList())
                {
                     Shrine(shrine, shrine.IsVisibleToEnemies);
                }
            }
            if (Menu.Item("neutrals").GetValue<bool>())
            {
                foreach (var neutral in units.Where(IsNeutral).ToList())
                {
                    HandleEffect(neutral, neutral.IsVisibleToEnemies);
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
        
        private static void Shrine(Entity sender, bool visible)
        {
            if (!sender.IsValid)
            {
                return;
            }           
            var MapPosition = sender.Position;
            var MiniMapPosition = HUDInfo.WorldToMinimap(sender.Position);
            if (visible && sender.IsAlive && Menu.Item("draw_minimap_shrines").GetValue<bool>())
            {                
                if (MapPosition == new Vector3(-4224, 1279.969f, 384))
                {
                    Position.Add(MiniMapPosition);
                }
                else if (MapPosition == new Vector3(639.9688f, -2560, 384))
                {
                    Position.Add(MiniMapPosition);
                }
                else if (MapPosition == new Vector3(4191.969f, -1600, 385.1875f))
                {
                    Position.Add(MiniMapPosition);
                }
                else if (MapPosition == new Vector3(-128.0313f, 2528, 385.1875f))
                {
                    Position.Add(MiniMapPosition);
                }
                else
                {
                    return;
                }
            }
            else
            {                
                Remover(MiniMapPosition);
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
            {
                return;
            }
            bool hero = false, wards = false,  mines = false, shrines = false, units = false, neutrals = false, buildings = false;
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
                case "shrines":
                    shrines = true;
                    break;         
                case "neutrals":
                    neutrals = true;
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
                var index = e.GetNewValue<StringList>().SelectedIndex;
                foreach (var source in ObjectManager.GetEntities<Hero>().Where(x=>x.Team==ObjectManager.LocalHero.Team))
                {
                    HandleEffect(source, false);
                    if (source.IsVisible)
                        HandleEffect(source, true, index);
                }
            }
            var newDict = new Dictionary<Unit, ParticleEffect>();
            foreach (var effect in _effects)
            {
                if (hero && effect.Key is Hero)
                {
                    effect.Value.Dispose();
                }
                else if (wards && IsWard(effect.Key))
                {
                    effect.Value.Dispose();
                }
                else if (mines && IsMine(effect.Key))
                {
                    effect.Value.Dispose();
                }
                else if (shrines && IsShrine(effect.Key))
                {
                    effect.Value.Dispose();
                }
                else if (neutrals && IsNeutral(effect.Key))
                {
                    effect.Value.Dispose();
                }
                else if (units && IsUnit(effect.Key))
                {
                    effect.Value.Dispose();
                }                 
                else if (buildings && effect.Key is Building)
                {
                    effect.Value.Dispose();
                }
                else
                {
                    newDict.Add(effect.Key, effect.Value);
                }
            }
            _effects = newDict;            
        }
        #endregion
    }
}
