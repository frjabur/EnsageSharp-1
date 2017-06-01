using Ensage;
using Ensage.Common;
using Ensage.Common.Menu;
using SharpDX;
using SharpDX.Direct3D9;
using System;
using System.Collections.Generic;
using System.Linq;

namespace JungleScanPlus
{
    public static class JungleScanPlus
    {
        private static Font Font;
        private static readonly Menu Menu = new Menu("JungleScanPlus", "JungleScanPlus", true, "junglescanplus", true).SetFontColor(Color.Aqua);
        static void Main()
        {
            Events.OnLoad += EventsOnOnLoad;
        }
        private static void EventsOnOnLoad(object sender, EventArgs eventArgs)
        {
            Menu.AddItem(new MenuItem("enable", "Enable").SetValue(true));
            Menu.AddItem(new MenuItem("enableminimap", "Enable Mini Map").SetValue(true));
            Menu.AddItem(new MenuItem("enableradius", "Enable Radius").SetValue(true));
            Menu.AddToMainMenu();

            Entity.OnParticleEffectAdded += OnParticleEvent;
            Events.OnLoad -= EventsOnOnLoad;

            Drawing.OnPostReset += argst => { Font.OnResetDevice(); };
            Drawing.OnPreReset += argst => { Font.OnLostDevice(); };

            if (Drawing.RenderMode == RenderMode.Dx9)
            {
                Font = new Font(Drawing.Direct3DDevice9,
                    new FontDescription
                    {
                        FaceName = "Arial",
                        Height = 50,
                        OutputPrecision = FontPrecision.Default,
                        Quality = FontQuality.Default,
                        CharacterSet = FontCharacterSet.Default,
                        MipLevels = 3,
                        PitchAndFamily = FontPitchAndFamily.Default,
                        Weight = FontWeight.Black,
                    });                
            }
        }
             
        private static void OnParticleEvent(Entity hero, ParticleEffectAddedEventArgs args)
        {          
            if (hero.Name.Contains("npc_dota_neutral_") && Menu.Item("enable").GetValue<bool>())
            {
                DelayAction.Add(50, () =>            
                {
                    var GetControlPoint = args.ParticleEffect.GetControlPoint(0);
                    List<Vector2> Position = new List<Vector2>();

                    if (!args.ParticleEffect.Owner.IsVisible)
                    {                       
                        Position.Add(HUDInfo.WorldToMinimap(GetControlPoint));
                        DelayAction.Add(1000, () =>
                        {
                            Position.RemoveAt(0);
                        });

                        Drawing.OnEndScene += argst =>
                        {
                            if (Drawing.Direct3DDevice9 == null) return;
                            foreach (var pos in Position.ToList())
                            {
                                if (!pos.IsZero && Menu.Item("enableminimap").GetValue<bool>())
                                {
                                    Font.DrawText(null, "○", (int)pos.X - 15 + 2, (int)pos.Y - 30 + 2, Color.White);
                                    Font.DrawText(null, "○", (int)pos.X - 15, (int)pos.Y - 30, Color.White);
                                }
                            }
                        };
                        if (!GetControlPoint.IsZero && Menu.Item("enableradius").GetValue<bool>())
                        {
                            ParticleEffect range = new ParticleEffect(@"materials\ensage_ui\particles\junglescanplus.vpcf", GetControlPoint);
                            range.SetControlPoint(1, new Vector3(500, 255, 0));
                            range.SetControlPoint(2, new Vector3(255, 0, 0));
                            DelayAction.Add(500, () =>
                            {
                                if (range != null)
                                {
                                    range.Dispose();
                                    range = null;
                                }
                            });
                        }                                           
                    }
                });
            }
        }                  
    }
}
