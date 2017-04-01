// credits: beminee, Magmaring(bruninjaman), spyware293 and Jumpering
using System;
using System.Linq;
using System.Timers;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ensage;
using Ensage.Common;
using Ensage.Common.Extensions;
using Ensage.Common.Menu;
using Ensage.Common.Objects;
using Ensage.Common.Objects.UtilityObjects;
using SharpDX;
using SharpDX.Direct3D9;


namespace BeAwarePlus
{
    class BeAwarePlus
    {
        private static Vector2 minimap_pos2d;
        private static List<Vector2> pos = new List<Vector2>();
        private static Font font;
        private static bool Roshan_Dead;
        private static int Roshan_Respawn_Min_Time;
        private static int Roshan_Respawn_Max_Time;
        private static bool Ancient_Apparition_IsHere;
        private static bool Nyx_Assassin_IsHere;
        private static bool Bounty_Hunter_IsHere;
        private static bool Morphling_IsHere;
        private static bool Clinkz_IsHere;
        private static bool Furion_IsHere;
        private static bool Wisp_IsHere;
        private static bool _loaded = false;
        private static readonly Sleeper HeroChecker = new Sleeper();
        private static Timer A_Timer;
        private static Hero me;
        private static SideMessage informationmessage;
        private static readonly Menu Menu = new Menu("BeAwarePlus", "BeAwarePlus", true, "beawareplus", true);
        private static bool UseDefSound => Menu.Item("enable_default_sound").GetValue<bool>();
        private static void Main(string[] args)
        {
            Events.OnLoad += EventsOnOnLoad;
        }
        private static void EventsOnOnLoad(object sender, EventArgs eventArgs)
        {           
            var settingsMenu = new Menu("Settings", "Settings");
            Menu.AddSubMenu(settingsMenu);

            //Spells Menu
            var spellsMenu = new Menu("Spells", "Spells");
            settingsMenu.AddSubMenu(spellsMenu);

            var invokerMenu = new Menu("Invoker", "Invoker", false, "npc_dota_hero_invoker", true);
            spellsMenu.AddSubMenu(invokerMenu);
            var sunstrikeMenu = new Menu("Sun Strike", "Sun Strike", false, "invoker_sun_strike", true);
            sunstrikeMenu.AddItem(new MenuItem("invoker_sun_strike", "Enable").SetValue(true));
            sunstrikeMenu.AddItem(new MenuItem("invoker_sun_strike_msg", "Enable Information Message").SetValue(true));
            sunstrikeMenu.AddItem(new MenuItem("invoker_sun_strike_sound", "Enable Sound").SetValue(true));
            sunstrikeMenu.AddItem(new MenuItem("invoker_sun_strike_minimap", "Enable Min Map").SetValue(true));
            invokerMenu.AddSubMenu(sunstrikeMenu);
            var ghostwalkMenu = new Menu("Ghost Walk", "Ghost Walk", false, "invoker_ghost_walk", true);
            ghostwalkMenu.AddItem(new MenuItem("invoker_ghost_walk", "Enable").SetValue(true));
            ghostwalkMenu.AddItem(new MenuItem("invoker_ghost_walk_msg", "Enable Information Message").SetValue(true));
            ghostwalkMenu.AddItem(new MenuItem("invoker_ghost_walk_sound", "Enable Sound").SetValue(true));
            invokerMenu.AddSubMenu(ghostwalkMenu);

            var kunkkaMenu = new Menu("Kunkka", "Kunkka", false, "npc_dota_hero_kunkka", true);
            spellsMenu.AddSubMenu(kunkkaMenu);
            var torrentMenu = new Menu("Torrent", "Torrent", false, "kunkka_torrent", true);
            torrentMenu.AddItem(new MenuItem("kunkka_torrent", "Enable").SetValue(true));
            torrentMenu.AddItem(new MenuItem("kunkka_torrent_msg", "Enable Information Message").SetValue(true));
            torrentMenu.AddItem(new MenuItem("kunkka_torrent_sound", "Enable Sound").SetValue(true));
            torrentMenu.AddItem(new MenuItem("kunkka_torrent_minimap", "Enable Min Map").SetValue(true));
            kunkkaMenu.AddSubMenu(torrentMenu);

            var monkeykingMenu = new Menu("Monkey King", "Monkey King", false, "npc_dota_hero_monkey_king", true);
            spellsMenu.AddSubMenu(monkeykingMenu);
            var primalspringMenu = new Menu("Primal Spring", "Primal Spring", false, "monkey_king_primal_spring", true);
            primalspringMenu.AddItem(new MenuItem("monkey_king_primal_spring", "Enable").SetValue(true));
            primalspringMenu.AddItem(new MenuItem("monkey_king_primal_spring_msg", "Enable Information Message").SetValue(true));
            primalspringMenu.AddItem(new MenuItem("monkey_king_primal_spring_sound", "Enable Sound").SetValue(true));
            primalspringMenu.AddItem(new MenuItem("monkey_king_primal_spring_minimap", "Enable Min Map").SetValue(true));
            monkeykingMenu.AddSubMenu(primalspringMenu);
            
            var spiritbreakerMenu = new Menu("Spirit Breaker", "Spirit Breaker", false, "npc_dota_hero_spirit_breaker", true);
            spellsMenu.AddSubMenu(spiritbreakerMenu);
            var chargeofdarknessMenu = new Menu("Charge Of Darkness", "Charge Of Darkness", false, "spirit_breaker_charge_of_darkness", true);
            chargeofdarknessMenu.AddItem(new MenuItem("spirit_breaker_charge_of_darkness", "Enable").SetValue(true));
            chargeofdarknessMenu.AddItem(new MenuItem("spirit_breaker_charge_of_darkness_msg", "Enable Information Message").SetValue(true));
            chargeofdarknessMenu.AddItem(new MenuItem("spirit_breaker_charge_of_darkness_sound", "Enable Sound").SetValue(true));
            chargeofdarknessMenu.AddItem(new MenuItem("spirit_breaker_charge_of_darkness_minimap_start", "Enable Min Map Start").SetValue(true));
            chargeofdarknessMenu.AddItem(new MenuItem("spirit_breaker_charge_of_darkness_minimap_end", "Enable Min Map End").SetValue(true));
            spiritbreakerMenu.AddSubMenu(chargeofdarknessMenu);

            var shadowfiendMenu = new Menu("Shadow Fiend", "Shadow Fiend", false, "npc_dota_hero_nevermore", true);
            spellsMenu.AddSubMenu(shadowfiendMenu);
            var darklordMenu = new Menu("Dark Lord", "Dark Lord", false, "nevermore_dark_lord", true);
            darklordMenu.AddItem(new MenuItem("nevermore_dark_lord", "Enable").SetValue(true));
            darklordMenu.AddItem(new MenuItem("nevermore_dark_lord_msg", "Enable Information Message").SetValue(true));
            darklordMenu.AddItem(new MenuItem("nevermore_dark_lord_sound", "Enable Sound").SetValue(true));
            shadowfiendMenu.AddSubMenu(darklordMenu);

            var sniperMenu = new Menu("Sniper", "Sniper", false, "npc_dota_hero_sniper", true);
            spellsMenu.AddSubMenu(sniperMenu);
            var assassinateMenu = new Menu("Assassinate", "Assassinate", false, "sniper_assassinate", true);
            assassinateMenu.AddItem(new MenuItem("sniper_assassinate", "Enable").SetValue(true));
            assassinateMenu.AddItem(new MenuItem("sniper_assassinate_msg", "Enable Information Message").SetValue(true));
            assassinateMenu.AddItem(new MenuItem("sniper_assassinate_sound", "Enable Sound").SetValue(true));
            assassinateMenu.AddItem(new MenuItem("sniper_assassinate_minimap", "Enable Min Map").SetValue(true));
            sniperMenu.AddSubMenu(assassinateMenu);

            var bountyhunterMenu = new Menu("Bounty Hunter", "Bounty Hunter", false, "npc_dota_hero_bounty_hunter", true);
            spellsMenu.AddSubMenu(bountyhunterMenu);
            var shadowwalkMenu = new Menu("Shadow Walk", "Shadow Walk", false, "bounty_hunter_wind_walk", true);
            shadowwalkMenu.AddItem(new MenuItem("bounty_hunter_wind_walk", "Enable").SetValue(true));
            shadowwalkMenu.AddItem(new MenuItem("bounty_hunter_wind_walk_msg", "Enable Information Message").SetValue(true));
            shadowwalkMenu.AddItem(new MenuItem("bounty_hunter_wind_walk_sound", "Enable Sound").SetValue(true));
            shadowwalkMenu.AddItem(new MenuItem("bounty_hunter_wind_walk_minimap", "Enable Min Map").SetValue(true));
            bountyhunterMenu.AddSubMenu(shadowwalkMenu);
            var trackMenu = new Menu("Track", "Track", false, "bounty_hunter_track", true);
            trackMenu.AddItem(new MenuItem("bounty_hunter_track", "Enable").SetValue(true));
            trackMenu.AddItem(new MenuItem("bounty_hunter_track_msg", "Enable Information Message").SetValue(true));
            trackMenu.AddItem(new MenuItem("bounty_hunter_track_sound", "Enable Sound").SetValue(true));
            bountyhunterMenu.AddSubMenu(trackMenu);

            var bloodseekerMenu = new Menu("Bloodseeker", "Bloodseeker", false, "npc_dota_hero_bloodseeker", true);
            spellsMenu.AddSubMenu(bloodseekerMenu);
            var thirstMenu = new Menu("Thirst", "Thirst", false, "bloodseeker_thirst", true);
            thirstMenu.AddItem(new MenuItem("bloodseeker_thirst", "Enable").SetValue(true));
            thirstMenu.AddItem(new MenuItem("bloodseeker_thirst_msg", "Enable Information Message").SetValue(true));
            thirstMenu.AddItem(new MenuItem("bloodseeker_thirst_sound", "Enable Sound").SetValue(true));
            thirstMenu.AddItem(new MenuItem("bloodseeker_thirst_minimap", "Enable Min Map").SetValue(true));
            bloodseekerMenu.AddSubMenu(thirstMenu);

            var ancientapparitionMenu = new Menu("Ancient Apparition", "Ancient Apparition", false, "npc_dota_hero_ancient_apparition", true);
            spellsMenu.AddSubMenu(ancientapparitionMenu);
            var iceblastMenu = new Menu("Ice Blast", "Ice Blast", false, "ancient_apparition_ice_blast", true);
            iceblastMenu.AddItem(new MenuItem("ancient_apparition_ice_blast", "Enable").SetValue(true));
            iceblastMenu.AddItem(new MenuItem("ancient_apparition_ice_blast_msg", "Enable Information Message").SetValue(true));
            iceblastMenu.AddItem(new MenuItem("ancient_apparition_ice_blast_sound", "Enable Sound").SetValue(true));
            iceblastMenu.AddItem(new MenuItem("ancient_apparition_ice_blast_minimap", "Enable Min Map").SetValue(true));
            ancientapparitionMenu.AddSubMenu(iceblastMenu);

            var miranaMenu = new Menu("Mirana", "Mirana", false, "npc_dota_hero_mirana", true);
            spellsMenu.AddSubMenu(miranaMenu);
            var moonlightshadowMenu = new Menu("Moonlight Shadow", "Moonlight Shadow", false, "mirana_invis", true);
            moonlightshadowMenu.AddItem(new MenuItem("mirana_invis", "Enable").SetValue(true));
            moonlightshadowMenu.AddItem(new MenuItem("mirana_invis_msg", "Enable Information Message").SetValue(true));
            moonlightshadowMenu.AddItem(new MenuItem("mirana_invis_sound", "Enable Sound").SetValue(true));
            moonlightshadowMenu.AddItem(new MenuItem("mirana_invis_minimap_mirana", "Enable Min Map Mirana").SetValue(true));
            moonlightshadowMenu.AddItem(new MenuItem("mirana_invis_minimap_all", "Enable Min Map All").SetValue(true));
            miranaMenu.AddSubMenu(moonlightshadowMenu);

            var sandkingMenu = new Menu("Sand King", "Sand King", false, "npc_dota_hero_sand_king", true);
            spellsMenu.AddSubMenu(sandkingMenu);
            var epicenterMenu = new Menu("Epicenter", "Epicenter", false, "sandking_epicenter", true);
            epicenterMenu.AddItem(new MenuItem("sandking_epicenter", "Enable").SetValue(true));
            epicenterMenu.AddItem(new MenuItem("sandking_epicenter_msg", "Enable Information Message").SetValue(true));
            epicenterMenu.AddItem(new MenuItem("sandking_epicenter_sound", "Enable Sound").SetValue(true));
            epicenterMenu.AddItem(new MenuItem("sandking_epicenter_minimap", "Enable Min Map").SetValue(true));
            sandkingMenu.AddSubMenu(epicenterMenu);

            var furionMenu = new Menu("Nature's Prophet", "Nature's Prophet", false, "npc_dota_hero_furion", true);
            spellsMenu.AddSubMenu(furionMenu);
            var teleportationMenu = new Menu("Teleportation", "Teleportation", false, "furion_teleportation", true);
            teleportationMenu.AddItem(new MenuItem("furion_teleportation", "Enable").SetValue(true));
            teleportationMenu.AddItem(new MenuItem("furion_teleportation_msg", "Enable Information Message").SetValue(true));
            teleportationMenu.AddItem(new MenuItem("furion_teleportation_sound", "Enable Sound").SetValue(true));
            teleportationMenu.AddItem(new MenuItem("furion_teleportation_minimap_start", "Enable Min Map Start").SetValue(true));
            teleportationMenu.AddItem(new MenuItem("furion_teleportation_minimap_end", "Enable Min Map End").SetValue(true));
            furionMenu.AddSubMenu(teleportationMenu);
            var wrathofnatureMenu = new Menu("Wrath Of Nature", "Wrath Of Nature", false, "furion_wrath_of_nature", true);
            wrathofnatureMenu.AddItem(new MenuItem("furion_wrath_of_nature", "Enable").SetValue(true));
            wrathofnatureMenu.AddItem(new MenuItem("furion_wrath_of_nature_msg", "Enable Information Message").SetValue(true));
            wrathofnatureMenu.AddItem(new MenuItem("furion_wrath_of_nature_sound", "Enable Sound").SetValue(true));
            wrathofnatureMenu.AddItem(new MenuItem("furion_wrath_of_nature_minimap", "Enable Min Map").SetValue(true));
            furionMenu.AddSubMenu(wrathofnatureMenu);

            var alchemistMenu = new Menu("Alchemist", "Alchemist", false, "npc_dota_hero_alchemist", true);
            spellsMenu.AddSubMenu(alchemistMenu);
            var unstableconcoctionMenu = new Menu("Unstable Concoction", "Unstable Concoction", false, "alchemist_unstable_concoction", true);
            unstableconcoctionMenu.AddItem(new MenuItem("alchemist_unstable_concoction", "Enable").SetValue(true));
            unstableconcoctionMenu.AddItem(new MenuItem("alchemist_unstable_concoction_msg", "Enable Information Message").SetValue(true));
            unstableconcoctionMenu.AddItem(new MenuItem("alchemist_unstable_concoction_sound", "Enable Sound").SetValue(true));
            unstableconcoctionMenu.AddItem(new MenuItem("alchemist_unstable_concoction_minimap", "Enable Min Map").SetValue(true));
            alchemistMenu.AddSubMenu(unstableconcoctionMenu);

            var clinkzMenu = new Menu("Clinkz", "Clinkz", false, "npc_dota_hero_clinkz", true);
            spellsMenu.AddSubMenu(clinkzMenu);
            var skeletonwalkMenu = new Menu("Skeleton Walk", "Skeleton Walk", false, "clinkz_wind_walk", true);
            skeletonwalkMenu.AddItem(new MenuItem("clinkz_wind_walk", "Enable").SetValue(true));
            skeletonwalkMenu.AddItem(new MenuItem("clinkz_wind_walk_msg", "Enable Information Message").SetValue(true));
            skeletonwalkMenu.AddItem(new MenuItem("clinkz_wind_walk_sound", "Enable Sound").SetValue(true));
            skeletonwalkMenu.AddItem(new MenuItem("clinkz_wind_walk_minimap", "Enable Min Map").SetValue(true));
            clinkzMenu.AddSubMenu(skeletonwalkMenu);

            var nyxassassinMenu = new Menu("Nyx Assassin", "Nyx Assassin", false, "npc_dota_hero_nyx_assassin", true);
            spellsMenu.AddSubMenu(nyxassassinMenu);
            var vendettaMenu = new Menu("Vendetta", "Vendetta", false, "nyx_assassin_vendetta", true);
            vendettaMenu.AddItem(new MenuItem("nyx_assassin_vendetta", "Enable").SetValue(true));
            vendettaMenu.AddItem(new MenuItem("nyx_assassin_vendetta_msg", "Enable Information Message").SetValue(true));
            vendettaMenu.AddItem(new MenuItem("nyx_assassin_vendetta_sound", "Enable Sound").SetValue(true));
            vendettaMenu.AddItem(new MenuItem("nyx_assassin_vendetta_minimap", "Enable Min Map").SetValue(true));
            nyxassassinMenu.AddSubMenu(vendettaMenu);

            var wispMenu = new Menu("Wisp", "Wisp", false, "npc_dota_hero_wisp", true);
            spellsMenu.AddSubMenu(wispMenu);
            var wisprelocateMenu = new Menu("Relocate", "Relocate", false, "wisp_relocate", true);
            wisprelocateMenu.AddItem(new MenuItem("wisp_relocate", "Enable").SetValue(true));
            wisprelocateMenu.AddItem(new MenuItem("wisp_relocate_msg", "Enable Information Message").SetValue(true));
            wisprelocateMenu.AddItem(new MenuItem("wisp_relocate_sound", "Enable Sound").SetValue(true));
            wisprelocateMenu.AddItem(new MenuItem("wisp_relocate_minimap_start", "Enable Min Map Start").SetValue(true));
            wisprelocateMenu.AddItem(new MenuItem("wisp_relocate_minimap_end", "Enable Min Map End").SetValue(true));
            wispMenu.AddSubMenu(wisprelocateMenu);

            var morphlingMenu = new Menu("Morphling", "Morphling", false, "npc_dota_hero_morphling", true);
            spellsMenu.AddSubMenu(morphlingMenu);
            var replicateMenu = new Menu("Replicate", "Replicate", false, "morphling_replicate", true);
            replicateMenu.AddItem(new MenuItem("morphling_replicate", "Enable").SetValue(true));
            replicateMenu.AddItem(new MenuItem("morphling_replicate_msg", "Enable Information Message").SetValue(true));
            replicateMenu.AddItem(new MenuItem("morphling_replicate_sound", "Enable Sound").SetValue(true));
            replicateMenu.AddItem(new MenuItem("morphling_replicate_minimap", "Enable Min Map").SetValue(true));
            morphlingMenu.AddSubMenu(replicateMenu);

            var trollwarlordMenu = new Menu("Troll Warlord", "Troll Warlord", false, "npc_dota_hero_troll_warlord", true);
            spellsMenu.AddSubMenu(trollwarlordMenu);
            var battletranceMenu = new Menu("Battle Trance", "Battle Trance", false, "troll_warlord_battle_trance", true);
            battletranceMenu.AddItem(new MenuItem("troll_warlord_battle_trance", "Enable").SetValue(true));
            battletranceMenu.AddItem(new MenuItem("troll_warlord_battle_trance_msg", "Enable Information Message").SetValue(true));
            battletranceMenu.AddItem(new MenuItem("troll_warlord_battle_trance_sound", "Enable Sound").SetValue(true));
            battletranceMenu.AddItem(new MenuItem("troll_warlord_battle_trance_minimap", "Enable Min Map").SetValue(true));
            trollwarlordMenu.AddSubMenu(battletranceMenu);

            var ursaMenu = new Menu("Ursa", "Ursa", false, "npc_dota_hero_ursa", true);
            spellsMenu.AddSubMenu(ursaMenu);
            var enrageMenu = new Menu("Enrage", "Enrage", false, "ursa_enrage", true);
            enrageMenu.AddItem(new MenuItem("ursa_enrage", "Enable").SetValue(true));
            enrageMenu.AddItem(new MenuItem("ursa_enrage_msg", "Enable Information Message").SetValue(true));
            enrageMenu.AddItem(new MenuItem("ursa_enrage_sound", "Enable Sound").SetValue(true));
            enrageMenu.AddItem(new MenuItem("ursa_enrage_minimap", "Enable Min Map").SetValue(true));
            ursaMenu.AddSubMenu(enrageMenu);

            //Items Menu
            var itemsMenu = new Menu("Items", "Items");
            settingsMenu.AddSubMenu(itemsMenu);

            var smokeofdeceitMenu = new Menu("Smoke Of Deceit", "Smoke Of Deceit", false, "item_smoke_of_deceit", true);
            smokeofdeceitMenu.AddItem(new MenuItem("smoke_of_deceit", "Enable").SetValue(true));
            smokeofdeceitMenu.AddItem(new MenuItem("smoke_of_deceit_msg", "Enable Information Message").SetValue(true));
            smokeofdeceitMenu.AddItem(new MenuItem("smoke_of_deceit_sound", "Enable Sound").SetValue(true));
            smokeofdeceitMenu.AddItem(new MenuItem("smoke_of_deceit_minimap", "Enable Min Map").SetValue(true));
            itemsMenu.AddSubMenu(smokeofdeceitMenu);

            var shadowbladeMenu = new Menu("Shadow Blade", "Shadow Blade", false, "item_invis_sword", true);
            shadowbladeMenu.AddItem(new MenuItem("invis_sword", "Enable").SetValue(true));
            shadowbladeMenu.AddItem(new MenuItem("invis_sword_msg", "Enable Information Message").SetValue(true));
            shadowbladeMenu.AddItem(new MenuItem("invis_sword_sound", "Enable Sound").SetValue(true));
            shadowbladeMenu.AddItem(new MenuItem("invis_sword_minimap", "Enable Min Map").SetValue(true));
            itemsMenu.AddSubMenu(shadowbladeMenu);

            var shadowamuletMenu = new Menu("Shadow Amulet", "Shadow Amulet", false, "item_shadow_amulet", true);
            shadowamuletMenu.AddItem(new MenuItem("shadow_amulet", "Enable").SetValue(true));
            shadowamuletMenu.AddItem(new MenuItem("shadow_amulet_msg", "Enable Information Message").SetValue(true));
            shadowamuletMenu.AddItem(new MenuItem("shadow_amulet_sound", "Enable Sound").SetValue(true));
            shadowamuletMenu.AddItem(new MenuItem("shadow_amulet_minimap", "Enable Min Map").SetValue(true));
            itemsMenu.AddSubMenu(shadowamuletMenu);

            var glimmercapeMenu = new Menu("Glimmer Cape", "Glimmer Cape", false, "item_glimmer_cape", true);
            glimmercapeMenu.AddItem(new MenuItem("glimmer_cape", "Enable").SetValue(true));
            glimmercapeMenu.AddItem(new MenuItem("glimmer_cape_msg", "Enable Information Message").SetValue(true));
            glimmercapeMenu.AddItem(new MenuItem("glimmer_cape_sound", "Enable Sound").SetValue(true));
            glimmercapeMenu.AddItem(new MenuItem("glimmer_cape_minimap", "Enable Min Map").SetValue(true));
            itemsMenu.AddSubMenu(glimmercapeMenu);

            var silveredgeMenu = new Menu("Silver Edge", "Silver Edge", false, "item_silver_edge", true);
            silveredgeMenu.AddItem(new MenuItem("silver_edge", "Enable").SetValue(true));
            silveredgeMenu.AddItem(new MenuItem("silver_edge_msg", "Enable Information Message").SetValue(true));
            silveredgeMenu.AddItem(new MenuItem("silver_edge_sound", "Enable Sound").SetValue(true));
            silveredgeMenu.AddItem(new MenuItem("silver_edge_minimap", "Enable Min Map").SetValue(true));
            itemsMenu.AddSubMenu(silveredgeMenu);

            var gemMenu = new Menu("Gem of True Sight", "Gem of True Sight", false, "item_gem", true);
            gemMenu.AddItem(new MenuItem("gem", "Enable").SetValue(true));
            gemMenu.AddItem(new MenuItem("gem_msg", "Enable Information Message").SetValue(true));
            gemMenu.AddItem(new MenuItem("gem_sound", "Enable Sound").SetValue(true));
            itemsMenu.AddSubMenu(gemMenu);

            var rapierMenu = new Menu("Divine Rapier", "Divine Rapier", false, "item_rapier", true);
            rapierMenu.AddItem(new MenuItem("rapier", "Enable").SetValue(true));
            rapierMenu.AddItem(new MenuItem("rapier_msg", "Enable Information Message").SetValue(true));
            rapierMenu.AddItem(new MenuItem("rapier_sound", "Enable Sound").SetValue(true));
            itemsMenu.AddSubMenu(rapierMenu);

            //Runes Menu
            var runesMenu = new Menu("Runes", "Runes");
            settingsMenu.AddSubMenu(runesMenu);

            var hasteMenu = new Menu("Haste", "Haste", false, "rune_haste", true);
            hasteMenu.AddItem(new MenuItem("rune_haste", "Enable").SetValue(true));
            hasteMenu.AddItem(new MenuItem("rune_haste_msg", "Enable Information Message").SetValue(true));
            hasteMenu.AddItem(new MenuItem("rune_haste_sound", "Enable Sound").SetValue(true));
            runesMenu.AddSubMenu(hasteMenu);

            var regenMenu = new Menu("Regen", "Regen", false, "rune_regen", true);
            regenMenu.AddItem(new MenuItem("rune_regen", "Enable").SetValue(true));
            regenMenu.AddItem(new MenuItem("rune_regen_msg", "Enable Information Message").SetValue(true));
            regenMenu.AddItem(new MenuItem("rune_regen_sound", "Enable Sound").SetValue(true));
            runesMenu.AddSubMenu(regenMenu);

            var arcaneMenu = new Menu("Arcane", "Arcane", false, "rune_arcane", true);
            arcaneMenu.AddItem(new MenuItem("rune_arcane", "Enable").SetValue(true));
            arcaneMenu.AddItem(new MenuItem("rune_arcane_msg", "Enable Information Message").SetValue(true));
            arcaneMenu.AddItem(new MenuItem("rune_arcane_sound", "Enable Sound").SetValue(true));
            runesMenu.AddSubMenu(arcaneMenu);

            var doubledamageMenu = new Menu("Doubledamage", "Doubledamage", false, "rune_doubledamage", true);
            doubledamageMenu.AddItem(new MenuItem("rune_doubledamage", "Enable").SetValue(true));
            doubledamageMenu.AddItem(new MenuItem("rune_doubledamage_msg", "Enable Information Message").SetValue(true));
            doubledamageMenu.AddItem(new MenuItem("rune_doubledamage_sound", "Enable Sound").SetValue(true));
            runesMenu.AddSubMenu(doubledamageMenu);

            var invisMenu = new Menu("Invis", "Invis", false, "rune_invis", true);
            invisMenu.AddItem(new MenuItem("rune_invis", "Enable").SetValue(true));
            invisMenu.AddItem(new MenuItem("rune_invis_msg", "Enable Information Message").SetValue(true));
            invisMenu.AddItem(new MenuItem("rune_invis_sound", "Enable Sound").SetValue(true));
            runesMenu.AddSubMenu(invisMenu);

            //Additional Menu
            var additionalMenu = new Menu("Additional", "Additional");
            settingsMenu.AddSubMenu(additionalMenu);

            var radarMenu = new Menu("Radar", "Radar", false, "radar_scan", true);            
            radarMenu.AddItem(new MenuItem("radar_scan", "Enable").SetValue(true));
            radarMenu.AddItem(new MenuItem("radar_scan_msg", "Enable Information Message").SetValue(true));
            radarMenu.AddItem(new MenuItem("radar_scan_sound", "Enable Sound").SetValue(true));
            radarMenu.AddItem(new MenuItem("radar_scan_minimap", "Enable Min Map").SetValue(true));
            additionalMenu.AddSubMenu(radarMenu);

            var checkrunMenu = new Menu("Check Rune", "Check Rune", false, "rune_bounty", true);
            checkrunMenu.AddItem(new MenuItem("check_rune", "Enable").SetValue(true));
            checkrunMenu.AddItem(new MenuItem("check_rune_sec", "Time Per Sec Rune").SetValue(new Slider(10, 0, 30)));
            checkrunMenu.AddItem(new MenuItem("check_rune_msg", "Enable Information Message").SetValue(true));
            checkrunMenu.AddItem(new MenuItem("check_rune_sound", "Enable Sound").SetValue(true));
            additionalMenu.AddSubMenu(checkrunMenu);

            var handofmidasMenu = new Menu("Hand Of Midas", "Hand Of Midas", false, "item_hand_of_midas", true);
            handofmidasMenu.AddItem(new MenuItem("use_midas", "Enable").SetValue(true));
            handofmidasMenu.AddItem(new MenuItem("use_midas_sec", "Time Per Sec Midas").SetValue(new Slider(5, 0, 10)));
            handofmidasMenu.AddItem(new MenuItem("use_midas_msg", "Enable Information Message").SetValue(true));
            handofmidasMenu.AddItem(new MenuItem("use_midas_sound", "Enable Sound").SetValue(true));
            additionalMenu.AddSubMenu(handofmidasMenu);

            var roshanMenu = new Menu("Roshan", "Roshan", false, "roshan_halloween_levels", true);
            roshanMenu.AddItem(new MenuItem("roshan", "Enable").SetValue(true));
            roshanMenu.AddItem(new MenuItem("roshan_msg", "Enable Information Message").SetValue(true));
            roshanMenu.AddItem(new MenuItem("roshan_sound", "Enable Sound").SetValue(true));
            additionalMenu.AddSubMenu(roshanMenu);
            
            Menu.AddItem(new MenuItem("enable_msg", "Enable Information Message").SetValue(true));
            Menu.AddItem(new MenuItem("enable_sound", "Enable Sound").SetValue(true));
            Menu.AddItem(new MenuItem("enable_default_sound", "Enable Default Sound").SetValue(false)).SetTooltip("All Sounds Becomes Default");
            Menu.AddItem(new MenuItem("enable_minimap", "Enable Mini Map").SetValue(true));
            Menu.AddToMainMenu();

            font = new Font(
                Drawing.Direct3DDevice9,
                new FontDescription
                {
                    FaceName = "Arial",
                    Height = 60,
                    OutputPrecision = FontPrecision.Character,
                    Quality = FontQuality.ClearTypeNatural,
                    CharacterSet = FontCharacterSet.Ansi,
                    MipLevels = 100,
                    PitchAndFamily = FontPitchAndFamily.Modern,
                    Weight = FontWeight.Heavy,
                    Width = 30
                });

            Roshan_Dead = false;
            Roshan_Respawn_Min_Time = 480;
            Roshan_Respawn_Max_Time = 660;
            A_Timer = new Timer(1000);
            A_Timer.Elapsed += OnTimedEvent;
            A_Timer.AutoReset = true;
            A_Timer.Enabled = true;

            PrintSuccess(">>>>>> BeAwarePlus Loaded!");

            var sList = new StringList()
            {
                SList = LangName,
                SelectedIndex = 0
            };
            var language = new MenuItem("lang", "Language").SetValue(sList);
            Menu.AddItem(language);

            me = ObjectManager.LocalHero;
            Entity.OnParticleEffectAdded += OnParticleEvent;
            Unit.OnModifierAdded += HeroOnOnModifierAdded;
            Game.OnUpdate += GameOnOnUpdate;
            Events.OnLoad -= EventsOnOnLoad;
            Game.OnFireEvent += Game_OnGameEvent;

            Drawing.OnEndScene += draw;
            Drawing.OnPreReset += Drawing_OnPreReset;
            Drawing.OnPostReset += Drawing_OnPostReset;
        }
        private static void draw(EventArgs args)
        {
            if (Menu.Item("enable_minimap").GetValue<bool>())
            {
                foreach (Vector2 position in pos.ToList())
                    font.DrawText(null, "*", (int)position.X + -13, (int)position.Y + -22, Color.Red);
            }                            
        }
        private static async void remover(Vector2 val)
        {
            await Task.Delay(5000);
            pos.RemoveAt(0);
            if (pos.Contains(val))
            {
                pos.Remove(val);
            }
        }
        static void Drawing_OnPostReset(EventArgs args)
        {
            font.OnResetDevice();
        }
        static void Drawing_OnPreReset(EventArgs args)
        {
            font.OnLostDevice();
        }
        private static void GameOnOnUpdate(EventArgs args)
        {
            if (_loaded && Heroes.All.Count >= 10)
                return;
            if (HeroChecker.Sleeping)
                return;
            if (Heroes.All.Count >= 10)
                _loaded = true;
            HeroChecker.Sleep(5000);
            if (me == null || !me.IsValid)
                me = ObjectManager.LocalHero;
            var enemyTeam = Heroes.GetByTeam(me.GetEnemyTeam());

            if (!Nyx_Assassin_IsHere)
                Nyx_Assassin_IsHere =
                enemyTeam.Any(x => x.ClassID == ClassID.CDOTA_Unit_Hero_Nyx_Assassin);

            if (!Bounty_Hunter_IsHere)
                Bounty_Hunter_IsHere =
                enemyTeam.Any(x => x.ClassID == ClassID.CDOTA_Unit_Hero_BountyHunter);

            if (!Ancient_Apparition_IsHere)
                Ancient_Apparition_IsHere =
                enemyTeam.Any(x => x.ClassID == ClassID.CDOTA_Unit_Hero_AncientApparition);

            if (!Morphling_IsHere)
                Morphling_IsHere =
                enemyTeam.Any(x => x.ClassID == ClassID.CDOTA_Unit_Hero_Morphling);

            if (!Clinkz_IsHere)
                Clinkz_IsHere =
                enemyTeam.Any(x => x.ClassID == ClassID.CDOTA_Unit_Hero_Clinkz);

            if (!Furion_IsHere)
                Furion_IsHere =
                enemyTeam.Any(x => x.ClassID == ClassID.CDOTA_Unit_Hero_Furion);

            if (!Wisp_IsHere)
                Wisp_IsHere =
                enemyTeam.Any(x => x.ClassID == ClassID.CDOTA_Unit_Hero_Wisp);

        }        
        public static int GetLangId
        {
            get { return Menu.Item("lang").GetValue<StringList>().SelectedIndex; }
            set { throw new NotImplementedException(); }
        }
        private static readonly string[] Addition =
        {
            "en",
            "ru"
        };
        private static readonly string[] LangName =
        {
            "EN",
            "RU"
        };       
        private static void PlaySound(string path)
        {
            if (!Menu.Item("enable_sound").GetValue<bool>()) return;

            var player =
            new System.Media.SoundPlayer();
            var fullpath = Environment.CurrentDirectory;

            fullpath = fullpath.Remove(fullpath.Length - 10);

            if (UseDefSound)
            {
                fullpath += @"\dota\materials\ensage_ui\sounds\default.wav";
            }
            else
            {
                fullpath += @"\dota\materials\ensage_ui\sounds\" + path;
            }
            player.SoundLocation = fullpath;
            player.Load();
            player.Play();           
        }
        private static void HeroOnOnModifierAdded(Unit sender, ModifierChangedEventArgs args)
        {

            //Invoker Sun Strike    
            if (args.Modifier.Name.Contains("modifier_invoker_sun_strike") && args.Modifier.Owner.Team != me.Team && Menu.Item("invoker_sun_strike").GetValue<bool>())
            {
                if (Menu.Item("invoker_sun_strike_msg").GetValue<bool>())
                {
                    MessageEnemyCreator("invoker", "invoker_sun_strike");
                }
                if (Menu.Item("invoker_sun_strike_sound").GetValue<bool>())
                {
                    PlaySound("invoker_sun_strike_" + Addition[GetLangId] + ".wav");
                }
                if (Menu.Item("invoker_sun_strike_minimap").GetValue<bool>())
                {
                    minimap_pos2d = HUDInfo.WorldToMinimap(args.Modifier.Owner.Position);
                    pos.Add(minimap_pos2d);
                    remover(minimap_pos2d);
                }                                               
            }

            //Kunkka Torrent
            if (args.Modifier.Name.Contains("modifier_kunkka_torrent_thinker") && args.Modifier.Owner.Team != me.Team && Menu.Item("kunkka_torrent").GetValue<bool>())
            {
                if (Menu.Item("kunkka_torrent_msg").GetValue<bool>())
                {
                    MessageEnemyCreator("kunkka", "kunkka_torrent");
                }
                if (Menu.Item("kunkka_torrent_sound").GetValue<bool>())
                {
                    PlaySound("default_" + Addition[GetLangId] + ".wav");
                }
                if (Menu.Item("kunkka_torrent_minimap").GetValue<bool>())
                {
                    minimap_pos2d = HUDInfo.WorldToMinimap(args.Modifier.Owner.Position);
                    pos.Add(minimap_pos2d);
                    remover(minimap_pos2d);
                }                                                
            }

            //Monkey King Primal Spring
            if (args.Modifier.Name.Contains("modifier_monkey_king_spring_thinker") && args.Modifier.Owner.Team != me.Team && Menu.Item("monkey_king_primal_spring").GetValue<bool>())
            {
                if (Menu.Item("monkey_king_primal_spring_msg").GetValue<bool>())
                {
                    MessageEnemyCreator("monkey_king", "monkey_king_primal_spring");
                }
                if (Menu.Item("monkey_king_primal_spring_sound").GetValue<bool>())
                {
                    PlaySound("default_" + Addition[GetLangId] + ".wav");
                }
                if (Menu.Item("monkey_king_primal_spring_minimap").GetValue<bool>())
                {
                    minimap_pos2d = HUDInfo.WorldToMinimap(args.Modifier.Owner.Position);
                    pos.Add(minimap_pos2d);
                    remover(minimap_pos2d);
                }         
            }

            //Radar
            if (args.Modifier.Name.Contains("modifier_radar_thinker") && args.Modifier.Owner.Team != me.Team && Menu.Item("radar_scan").GetValue<bool>())
            {
                if (Menu.Item("radar_scan_msg").GetValue<bool>())
                {
                    MessageEnemyCreator("radar", "radar_scan");
                }
                if (Menu.Item("radar_scan_sound").GetValue<bool>())
                {
                    PlaySound("radar_scan_" + Addition[GetLangId] + ".wav");
                }
                if (Menu.Item("radar_scan_minimap").GetValue<bool>())
                {
                    minimap_pos2d = HUDInfo.WorldToMinimap(args.Modifier.Owner.Position);
                    pos.Add(minimap_pos2d);
                    remover(minimap_pos2d);
                }          
            }

            if (!(sender is Hero))
                return;
            if (sender.IsIllusion)
                return;
            string index;
            if (sender.Team == me.Team)
            {

                //Spirit Breaker Charge of Darkness
                if (args.Modifier.Name.Contains("modifier_spirit_breaker_charge_of_darkness_vision") && Menu.Item("spirit_breaker_charge_of_darkness").GetValue<bool>())
                {
                    if (Menu.Item("spirit_breaker_charge_of_darkness_msg").GetValue<bool>())
                    {
                        MessageAllyCreator(sender.Name.Substring(14), "spirit_breaker_charge_of_darkness");
                    }
                    if (Menu.Item("spirit_breaker_charge_of_darkness_sound").GetValue<bool>())
                    {
                        PlaySound("spirit_breaker_charge_of_darkness_" + Addition[GetLangId] + ".wav");
                    }
                    if (Menu.Item("spirit_breaker_charge_of_darkness_minimap_end").GetValue<bool>())
                    {
                        minimap_pos2d = HUDInfo.WorldToMinimap(args.Modifier.Owner.Position);
                        pos.Add(minimap_pos2d);
                        remover(minimap_pos2d);
                    }       
                }

                //Shadow Fiend Dark Lord
                if (args.Modifier.Name.Contains("modifier_nevermore_presence") && Utils.SleepCheck("nevermore_dark_lord") && Menu.Item("nevermore_dark_lord").GetValue<bool>())
                {
                    if (Menu.Item("nevermore_dark_lord_msg").GetValue<bool>())
                    {
                        MessageAllyCreator(sender.Name.Substring(14), "nevermore_dark_lord");
                    }
                    if (Menu.Item("nevermore_dark_lord_sound").GetValue<bool>())
                    {
                        PlaySound("default_" + Addition[GetLangId] + ".wav");
                    }                  
                    Utils.Sleep(5000, "nevermore_dark_lord");
                }

                //Sniper Assassinate
                if (args.Modifier.Name.Contains("modifier_sniper_assassinate") && Menu.Item("sniper_assassinate").GetValue<bool>())
                {
                    if (Menu.Item("sniper_assassinate_msg").GetValue<bool>())
                    {
                        MessageAllyCreator(sender.Name.Substring(14), "sniper_assassinate");
                    }
                    if (Menu.Item("sniper_assassinate_sound").GetValue<bool>())
                    {
                        PlaySound("sniper_assassinate_" + Addition[GetLangId] + ".wav");
                    }
                    if (Menu.Item("sniper_assassinate_minimap").GetValue<bool>())
                    {
                        minimap_pos2d = HUDInfo.WorldToMinimap(args.Modifier.Owner.Position);
                        pos.Add(minimap_pos2d);
                        remover(minimap_pos2d);
                    }              
                }

                //Bounty Hunter Track
                if (args.Modifier.Name.Contains("modifier_bounty_hunter_track") && Bounty_Hunter_IsHere && Menu.Item("bounty_hunter_track").GetValue<bool>())
                {
                    if (Menu.Item("bounty_hunter_track_msg").GetValue<bool>())
                    {
                        MessageAllyCreator(sender.Name.Substring(14), "bounty_hunter_track");
                    }
                    if (Menu.Item("bounty_hunter_track_sound").GetValue<bool>())
                    {
                        PlaySound("bounty_hunter_track_" + Addition[GetLangId] + ".wav");
                    }                                      
                }

                //Invoker Ghost Walk
                if (args.Modifier.Name.Contains("modifier_invoker_ghost_walk_enemy") && Utils.SleepCheck("invoker_ghost_walk") && Menu.Item("invoker_ghost_walk").GetValue<bool>())
                {
                    if (Menu.Item("invoker_ghost_walk_msg").GetValue<bool>())
                    {
                        MessageAllyCreator(sender.Name.Substring(14), "invoker_ghost_walk");
                    }
                    if (Menu.Item("invoker_ghost_walk_sound").GetValue<bool>())
                    {
                        PlaySound("invoker_ghost_walk_" + Addition[GetLangId] + ".wav");
                    }                   
                    Utils.Sleep(3000, "invoker_ghost_walk");
                }

                //Bloodseeker Thirst
                if (args.Modifier.Name.Contains("modifier_bloodseeker_thirst_vision") && Utils.SleepCheck("bloodseeker_thirst") && Menu.Item("bloodseeker_thirst").GetValue<bool>())
                {
                    if (Menu.Item("bloodseeker_thirst_msg").GetValue<bool>())
                    {
                        MessageAllyCreator(sender.Name.Substring(14), "bloodseeker_thirst");
                    }
                    if (Menu.Item("bloodseeker_thirst_sound").GetValue<bool>())
                    {
                        PlaySound("bloodseeker_thirst_" + Addition[GetLangId] + ".wav");
                    }
                    if (Menu.Item("bloodseeker_thirst_minimap").GetValue<bool>())
                    {
                        minimap_pos2d = HUDInfo.WorldToMinimap(args.Modifier.Owner.Position);
                        pos.Add(minimap_pos2d);
                        remover(minimap_pos2d);
                    }                                       
                    Utils.Sleep(10000, "bloodseeker_thirst");
                }
            }
            else
            {

                //Rune Haste
                if (args.Modifier.Name.Contains("modifier_rune_haste") && Utils.SleepCheck("rune_haste") && Menu.Item("rune_haste").GetValue<bool>())
                {
                    if (Menu.Item("rune_haste_msg").GetValue<bool>())
                    {
                        index = sender.Name.Remove(0, 14);
                        MessageRuneCreator(index, "rune_haste");
                    }
                    if (Menu.Item("rune_haste_sound").GetValue<bool>())
                    {
                        PlaySound("rune_haste_" + Addition[GetLangId] + ".wav");
                    }                                        
                    Utils.Sleep(10000, "rune_haste");
                }

                //Rune Regen
                if (args.Modifier.Name.Contains("modifier_rune_regen") && Utils.SleepCheck("rune_regen") && Menu.Item("rune_regen").GetValue<bool>())
                {
                    if (Menu.Item("rune_regen_msg").GetValue<bool>())
                    {
                        index = sender.Name.Remove(0, 14);
                        MessageRuneCreator(index, "rune_regen");
                    }
                    if (Menu.Item("rune_regen_sound").GetValue<bool>())
                    {
                        PlaySound("rune_regen_" + Addition[GetLangId] + ".wav");
                    }                                        
                    Utils.Sleep(10000, "rune_regen");
                }

                //Rune Arcane
                if (args.Modifier.Name.Contains("modifier_rune_arcane") && Utils.SleepCheck("rune_arcane") && Menu.Item("rune_arcane").GetValue<bool>())
                {
                    if (Menu.Item("rune_arcane_msg").GetValue<bool>())
                    {
                        index = sender.Name.Remove(0, 14);
                        MessageRuneCreator(index, "rune_arcane");
                    }
                    if (Menu.Item("rune_arcane_sound").GetValue<bool>())
                    {
                        PlaySound("default_" + Addition[GetLangId] + ".wav");
                    }                                        
                    Utils.Sleep(10000, "rune_arcane");
                }

                //Rune Doubledamage
                if (args.Modifier.Name.Contains("modifier_rune_doubledamage") && Utils.SleepCheck("rune_doubledamage") && Menu.Item("rune_doubledamage").GetValue<bool>())
                {
                    if (Menu.Item("rune_doubledamage_msg").GetValue<bool>())
                    {
                        index = sender.Name.Remove(0, 14);
                        MessageRuneCreator(index, "rune_doubledamage");
                    }
                    if (Menu.Item("rune_doubledamage_sound").GetValue<bool>())
                    {
                        PlaySound("rune_doubledamage_" + Addition[GetLangId] + ".wav");
                    }                                        
                    Utils.Sleep(10000, "rune_doubledamage");
                }

                //Rune Invis
                if (args.Modifier.Name.Contains("modifier_rune_invis") && Utils.SleepCheck("rune_invis") && Menu.Item("rune_invis").GetValue<bool>())
                {
                    if (Menu.Item("rune_invis_msg").GetValue<bool>())
                    {
                        index = sender.Name.Remove(0, 14);
                        MessageRuneCreator(index, "rune_invis");
                    }
                    if (Menu.Item("rune_invis_sound").GetValue<bool>())
                    {
                        PlaySound("rune_invis_" + Addition[GetLangId] + ".wav");
                    }                    
                    Utils.Sleep(3000, "rune_invis");
                }

                //Shadow Blade
                if (args.Modifier.Name.Contains("modifier_item_invisibility_edge_windwalk") && Utils.SleepCheck("invis_sword") && Menu.Item("invis_sword").GetValue<bool>())
                {
                    if (Menu.Item("invis_sword_msg").GetValue<bool>())
                    {
                        index = sender.Name.Remove(0, 14);
                        MessageItemCreator(index, "invis_sword");
                    }
                    if (Menu.Item("invis_sword_sound").GetValue<bool>())
                    {
                        PlaySound("invis_sword_" + Addition[GetLangId] + ".wav");
                    }
                    if (Menu.Item("invis_sword_minimap").GetValue<bool>())
                    {
                        minimap_pos2d = HUDInfo.WorldToMinimap(args.Modifier.Owner.Position);
                        pos.Add(minimap_pos2d);
                        remover(minimap_pos2d);
                    }                                       
                    Utils.Sleep(3000, "invis_sword");
                }

                //Shadow Amulet
                if (args.Modifier.Name.Contains("modifier_item_shadow_amulet_fade") && Utils.SleepCheck("shadow_amulet") && Menu.Item("shadow_amulet").GetValue<bool>())
                {
                    if (Menu.Item("shadow_amulet_msg").GetValue<bool>())
                    {
                        index = sender.Name.Remove(0, 14);
                        MessageItemCreator(index, "shadow_amulet");
                    }
                    if (Menu.Item("shadow_amulet_sound").GetValue<bool>())
                    {
                        PlaySound("shadow_amulet_" + Addition[GetLangId] + ".wav");
                    }
                    if (Menu.Item("shadow_amulet_minimap").GetValue<bool>())
                    {
                        minimap_pos2d = HUDInfo.WorldToMinimap(args.Modifier.Owner.Position);
                        pos.Add(minimap_pos2d);
                        remover(minimap_pos2d);
                    }                                   
                    Utils.Sleep(3000, "shadow_amulet");
                }

                //Glimmer Cape
                if (args.Modifier.Name.Contains("modifier_item_glimmer_cape_fade") && Utils.SleepCheck("glimmer_cape") && Menu.Item("glimmer_cape").GetValue<bool>())
                {
                    if (Menu.Item("glimmer_cape_msg").GetValue<bool>())
                    {
                        index = sender.Name.Remove(0, 14);
                        MessageItemCreator(index, "glimmer_cape");
                    }
                    if (Menu.Item("glimmer_cape_sound").GetValue<bool>())
                    {
                        PlaySound("glimmer_cape_" + Addition[GetLangId] + ".wav");
                    }
                    if (Menu.Item("glimmer_cape_minimap").GetValue<bool>())
                    {
                        minimap_pos2d = HUDInfo.WorldToMinimap(args.Modifier.Owner.Position);
                        pos.Add(minimap_pos2d);
                        remover(minimap_pos2d);
                    }                                                           
                    Utils.Sleep(3000, "glimmer_cape");
                }

                //Silver Edge
                if (args.Modifier.Name.Contains("modifier_item_silver_edge_windwalk") && Utils.SleepCheck("silver_edge") && Menu.Item("silver_edge").GetValue<bool>())
                {
                    if (Menu.Item("silver_edge_msg").GetValue<bool>())
                    {
                        index = sender.Name.Remove(0, 14);
                        MessageItemCreator(index, "silver_edge");
                    }
                    if (Menu.Item("silver_edge_sound").GetValue<bool>())
                    {
                        PlaySound("silver_edge_" + Addition[GetLangId] + ".wav");
                    }
                    if (Menu.Item("silver_edge_minimap").GetValue<bool>())
                    {
                        minimap_pos2d = HUDInfo.WorldToMinimap(args.Modifier.Owner.Position);
                        pos.Add(minimap_pos2d);
                        remover(minimap_pos2d);
                    }                                                           
                    Utils.Sleep(3000, "silver_edge");
                }

                //Gem of True Sight
                if (args.Modifier.Name.Contains("modifier_item_gem_of_true_sight") && Utils.SleepCheck("gem") && Menu.Item("gem").GetValue<bool>())
                {
                    if (Menu.Item("gem_msg").GetValue<bool>())
                    {
                        index = sender.Name.Remove(0, 14);
                        MessageItemCreator(index, "gem");
                    }
                    if (Menu.Item("gem_sound").GetValue<bool>())
                    {
                        PlaySound("default_" + Addition[GetLangId] + ".wav");
                    }                                       
                    Utils.Sleep(15000, "gem");
                }

                //Divine Rapier
                if (args.Modifier.Name.Contains("modifier_item_divine_rapier") && Utils.SleepCheck("rapier") && Menu.Item("rapier").GetValue<bool>())
                {
                    if (Menu.Item("rapier_msg").GetValue<bool>())
                    {
                        index = sender.Name.Remove(0, 14);
                        MessageItemCreator(index, "rapier");
                    }
                    if (Menu.Item("rapier_sound").GetValue<bool>())
                    {
                        PlaySound("default_" + Addition[GetLangId] + ".wav");
                    }                                     
                    Utils.Sleep(15000, "rapier");
                }

            }
        }
        public static void OnParticleEvent(Entity hero, ParticleEffectAddedEventArgs args)
        {

            //Smoke of Deceit  
            if (args.Name.Contains("smoke_of_deceit") && Menu.Item("smoke_of_deceit").GetValue<bool>())
            {
                DelayAction.Add(200, () =>
                {
                    var anyAllyWithSmokeEffect =
                    Heroes.GetByTeam(me.Team).Any(x => x.HasModifier("modifier_smoke_of_deceit"));
                    if (!anyAllyWithSmokeEffect)
                    {
                        if (Menu.Item("smoke_of_deceit_msg").GetValue<bool>())
                        {
                            MessageItemCreator("default2", "smoke_of_deceit");
                        }
                        if (Menu.Item("smoke_of_deceit_sound").GetValue<bool>())
                        {
                            PlaySound("item_smoke_of_deceit_" + Addition[GetLangId] + ".wav");
                        }
                        if (Menu.Item("smoke_of_deceit_minimap").GetValue<bool>())
                        {
                            minimap_pos2d = HUDInfo.WorldToMinimap(args.ParticleEffect.GetControlPoint(0));
                            pos.Add(minimap_pos2d);
                            remover(minimap_pos2d);
                        }

                    }
                });                 
            }

            //Ancient Apparition Ice Blast
            if (args.Name.Contains("ancient_apparition_ice_blast_final") && Ancient_Apparition_IsHere && Menu.Item("ancient_apparition_ice_blast").GetValue<bool>())
            {
                if (Menu.Item("ancient_apparition_ice_blast_msg").GetValue<bool>())
                {
                    MessageEnemyCreator("ancient_apparition", "ancient_apparition_ice_blast");
                }
                if (Menu.Item("ancient_apparition_ice_blast_sound").GetValue<bool>())
                {
                    PlaySound("ancient_apparition_ice_blast_" + Addition[GetLangId] + ".wav");
                }               
                DelayAction.Add(200, () =>
                {
                    if (Menu.Item("ancient_apparition_ice_blast_minimap").GetValue<bool>())
                    {
                        minimap_pos2d = HUDInfo.WorldToMinimap(args.ParticleEffect.GetControlPoint(0));
                        pos.Add(minimap_pos2d);
                        remover(minimap_pos2d);
                    }                   
                });
            }

            //Mirana Moonlight Shadow
            if (args.Name.Contains("mirana_moonlight_cast") && args.ParticleEffect.Owner.Team != me.Team && Menu.Item("mirana_invis").GetValue<bool>())                       
                {
                if (Menu.Item("mirana_invis_msg").GetValue<bool>())
                {
                    MessageEnemyCreator("mirana", "mirana_invis");
                }
                if (Menu.Item("mirana_invis_sound").GetValue<bool>())
                {
                    PlaySound("moonlight_shadow_" + Addition[GetLangId] + ".wav");
                }            
                DelayAction.Add(200, () =>
                {
                    if (Menu.Item("mirana_invis_minimap_mirana").GetValue<bool>())
                    {
                        minimap_pos2d = HUDInfo.WorldToMinimap(args.ParticleEffect.GetControlPoint(0));
                        pos.Add(minimap_pos2d);
                        remover(minimap_pos2d);
                    }                   
                });
            }

            //Mirana Moonlight Shadow All Mini Map
            if (args.Name.Contains("mirana_moonlight_recipient") && args.ParticleEffect.Owner.Team != me.Team && Menu.Item("mirana_invis").GetValue<bool>())
            {
                DelayAction.Add(200, () =>
                {
                    if (Menu.Item("mirana_invis_minimap_all").GetValue<bool>())
                    {
                        minimap_pos2d = HUDInfo.WorldToMinimap(args.ParticleEffect.GetControlPoint(0));
                        pos.Add(minimap_pos2d);
                        remover(minimap_pos2d);
                    }
                });
            }

            //Sandking Epicenter
            if (args.Name.Contains("sandking_epicenter") && args.ParticleEffect.Owner.Team != me.Team && Menu.Item("sandking_epicenter").GetValue<bool>())
            {
                if (Menu.Item("sandking_epicenter_msg").GetValue<bool>())
                {
                    MessageEnemyCreator("sand_king", "sandking_epicenter");
                }
                if (Menu.Item("sandking_epicenter_sound").GetValue<bool>())
                {
                    PlaySound("default_" + Addition[GetLangId] + ".wav");
                }                                
                DelayAction.Add(200, () =>
                {
                    if (Menu.Item("sandking_epicenter_minimap").GetValue<bool>())
                    {
                        minimap_pos2d = HUDInfo.WorldToMinimap(args.ParticleEffect.GetControlPoint(0));
                        pos.Add(minimap_pos2d);
                        remover(minimap_pos2d);
                    }
                });
            }

            //Nature's Prophet Teleportation Start
            if (args.Name.Contains("furion_teleport") && args.ParticleEffect.Owner.Team != me.Team && Menu.Item("furion_teleportation").GetValue<bool>())
            {
                if (Menu.Item("furion_teleportation_msg").GetValue<bool>())
                {
                    MessageEnemyCreator("furion", "furion_teleportation");
                }
                if (Menu.Item("furion_teleportation_sound").GetValue<bool>())
                {
                    PlaySound("furion_teleportation_" + Addition[GetLangId] + ".wav");
                }         
                DelayAction.Add(200, () =>
                {
                    if (Menu.Item("furion_teleportation_minimap_start").GetValue<bool>())
                    {
                        minimap_pos2d = HUDInfo.WorldToMinimap(args.ParticleEffect.GetControlPoint(0));
                        pos.Add(minimap_pos2d);
                        remover(minimap_pos2d);
                    }           
                });
            }

            //Nature's Prophet Teleportation End
            if (args.Name.Contains("furion_teleport_end") && Furion_IsHere && Menu.Item("furion_teleportation").GetValue<bool>())
            {
                DelayAction.Add(200, () =>
                {
                    if (Menu.Item("furion_teleportation_minimap_end").GetValue<bool>())                
                    {
                        minimap_pos2d = HUDInfo.WorldToMinimap(args.ParticleEffect.GetControlPoint(1));
                        pos.Add(minimap_pos2d);
                        remover(minimap_pos2d);
                    }     
                });
            }

            //Nature's Prophet Wrath of Nature
            if (args.Name.Contains("furion_wrath_of_nature") && args.ParticleEffect.Owner.Team != me.Team && Menu.Item("furion_wrath_of_nature").GetValue<bool>())
            {
                if (Menu.Item("furion_wrath_of_nature_msg").GetValue<bool>())
                {
                    MessageEnemyCreator("furion", "furion_wrath_of_nature");
                }
                if (Menu.Item("furion_wrath_of_nature_sound").GetValue<bool>())
                {
                    PlaySound("furion_wrath_of_nature_" + Addition[GetLangId] + ".wav");
                }
                DelayAction.Add(200, () =>
                {
                    if (Menu.Item("furion_wrath_of_nature_minimap").GetValue<bool>())
                    {
                        minimap_pos2d = HUDInfo.WorldToMinimap(args.ParticleEffect.GetControlPoint(0));
                        pos.Add(minimap_pos2d);
                        remover(minimap_pos2d);
                    }     
                });
            }

            //Alchemist Unstable Concoction
            if (args.Name.Contains("alchemist_unstableconc") && args.ParticleEffect.Owner.Team != me.Team && Menu.Item("alchemist_unstable_concoction").GetValue<bool>())
            {
                if (Menu.Item("alchemist_unstable_concoction_msg").GetValue<bool>())
                {
                    MessageEnemyCreator("alchemist", "alchemist_unstable_concoction");
                }
                if (Menu.Item("alchemist_unstable_concoction_sound").GetValue<bool>())
                {
                    PlaySound("unstable_concoction_" + Addition[GetLangId] + ".wav");
                }              
                DelayAction.Add(200, () =>
                {
                    if (Menu.Item("alchemist_unstable_concoction_minimap").GetValue<bool>())
                    {
                        minimap_pos2d = HUDInfo.WorldToMinimap(args.ParticleEffect.GetControlPoint(0));
                        pos.Add(minimap_pos2d);
                        remover(minimap_pos2d);
                    }    
                });
            }

            //Bounty Hunter Shadow Walk
            if (args.Name.Contains("bounty_hunter_windwalk") && Bounty_Hunter_IsHere && Menu.Item("bounty_hunter_wind_walk").GetValue<bool>())
            {
                if (Menu.Item("bounty_hunter_wind_walk_msg").GetValue<bool>())
                {
                    MessageEnemyCreator("bounty_hunter", "bounty_hunter_wind_walk");
                }
                if (Menu.Item("bounty_hunter_wind_walk_sound").GetValue<bool>())
                {
                    PlaySound("bounty_hunter_wind_walk_" + Addition[GetLangId] + ".wav");
                }                           
                DelayAction.Add(200, () =>
                {
                    if (Menu.Item("bounty_hunter_wind_walk_minimap").GetValue<bool>())
                    {
                        minimap_pos2d = HUDInfo.WorldToMinimap(args.ParticleEffect.GetControlPoint(0));
                        pos.Add(minimap_pos2d);
                        remover(minimap_pos2d);
                    }                    
                });
            }

            //Clinkz Skeleton Walk
            if (args.Name.Contains("clinkz_windwalk") && Clinkz_IsHere && Menu.Item("clinkz_wind_walk").GetValue<bool>())
            {
                if (Menu.Item("clinkz_wind_walk_msg").GetValue<bool>())
                {
                    MessageEnemyCreator("clinkz", "clinkz_wind_walk");
                }
                if (Menu.Item("clinkz_wind_walk_sound").GetValue<bool>())
                {
                    PlaySound("clinkz_wind_walk_" + Addition[GetLangId] + ".wav");
                }          
                DelayAction.Add(200, () =>
                {
                    if (Menu.Item("clinkz_wind_walk_minimap").GetValue<bool>())
                    {
                        minimap_pos2d = HUDInfo.WorldToMinimap(args.ParticleEffect.GetControlPoint(0));
                        pos.Add(minimap_pos2d);
                        remover(minimap_pos2d);
                    }                 
                });
            }

            //Nyx Assassin Vendetta
            if (args.Name.Contains("nyx_assassin_vendetta_start") && Nyx_Assassin_IsHere && Menu.Item("nyx_assassin_vendetta").GetValue<bool>())
            {
                if (Menu.Item("nyx_assassin_vendetta_msg").GetValue<bool>())
                {
                    MessageEnemyCreator("nyx_assassin", "nyx_assassin_vendetta");
                }
                if (Menu.Item("nyx_assassin_vendetta_sound").GetValue<bool>())
                {
                    PlaySound("nyx_assassin_vendetta_" + Addition[GetLangId] + ".wav");
                }                               
                DelayAction.Add(200, () =>
                {
                    if (Menu.Item("nyx_assassin_vendetta_minimap").GetValue<bool>())
                    {
                        minimap_pos2d = HUDInfo.WorldToMinimap(args.ParticleEffect.GetControlPoint(0));
                        pos.Add(minimap_pos2d);
                        remover(minimap_pos2d);
                    }           
                });
            }

            //Wisp Relocate Start
            if (args.Name.Contains("wisp_relocate_channel") && args.ParticleEffect.Owner.Team != me.Team && Menu.Item("wisp_relocate").GetValue<bool>())
            {
                if (Menu.Item("wisp_relocate_msg").GetValue<bool>())
                {
                    MessageEnemyCreator("wisp", "wisp_relocate");
                }
                if (Menu.Item("wisp_relocate_sound").GetValue<bool>())
                {
                    PlaySound("wisp_relocate_" + Addition[GetLangId] + ".wav");
                }
                DelayAction.Add(200, () =>
                {
                    if (Menu.Item("wisp_relocate_minimap_start").GetValue<bool>())
                    {
                        minimap_pos2d = HUDInfo.WorldToMinimap(args.ParticleEffect.GetControlPoint(0));
                        pos.Add(minimap_pos2d);
                        remover(minimap_pos2d);
                    }       
                });
            }

            //Wisp Relocate End
            if (args.Name.Contains("wisp_relocate_marker_endpoint") && Wisp_IsHere && Menu.Item("wisp_relocate").GetValue<bool>())
            {                
                DelayAction.Add(200, () =>
                {
                    if (Menu.Item("wisp_relocate_minimap_end").GetValue<bool>())
                    {
                        minimap_pos2d = HUDInfo.WorldToMinimap(args.ParticleEffect.GetControlPoint(0));
                        pos.Add(minimap_pos2d);
                        remover(minimap_pos2d);
                    }                    
                });
            }

            //Morphling Replicate
            if (args.Name.Contains("morphling_replicate") && Morphling_IsHere && Menu.Item("morphling_replicate").GetValue<bool>())
            {
                if (Menu.Item("morphling_replicate_msg").GetValue<bool>())
                {
                    MessageEnemyCreator("morphling", "morphling_replicate");
                }
                if (Menu.Item("morphling_replicate_sound").GetValue<bool>())
                {
                    PlaySound("morphling_replicate_" + Addition[GetLangId] + ".wav");
                }                                
                DelayAction.Add(200, () =>
                {
                    if (Menu.Item("morphling_replicate_minimap").GetValue<bool>())
                    {
                        minimap_pos2d = HUDInfo.WorldToMinimap(args.ParticleEffect.GetControlPoint(0));
                        pos.Add(minimap_pos2d);
                        remover(minimap_pos2d);
                    }                    
                });
            }

            //Troll Warlord Battle Trance
            if (args.Name.Contains("troll_warlord_battletrance_cast") && args.ParticleEffect.Owner.Team != me.Team && Menu.Item("troll_warlord_battle_trance").GetValue<bool>())
            {
                if (Menu.Item("troll_warlord_battle_trance_msg").GetValue<bool>())
                {
                    MessageEnemyCreator("troll_warlord", "troll_warlord_battle_trance");
                }
                if (Menu.Item("troll_warlord_battle_trance_sound").GetValue<bool>())
                {
                    PlaySound("troll_warlord_battle_trance_" + Addition[GetLangId] + ".wav");
                }                                
                DelayAction.Add(200, () =>
                {
                    if (Menu.Item("troll_warlord_battle_trance_minimap").GetValue<bool>())
                    {
                        minimap_pos2d = HUDInfo.WorldToMinimap(args.ParticleEffect.GetControlPoint(0));
                        pos.Add(minimap_pos2d);
                        remover(minimap_pos2d);
                    }                    
                });
            }

            //Ursa Enrage
            if (args.Name.Contains("ursa_enrage_buff") && args.ParticleEffect.Owner.Team != me.Team && Menu.Item("ursa_enrage").GetValue<bool>())
            {
                if (Menu.Item("ursa_enrage_msg").GetValue<bool>())
                {
                    MessageEnemyCreator("ursa", "ursa_enrage");
                }
                if (Menu.Item("ursa_enrage_sound").GetValue<bool>())
                {
                    PlaySound("ursa_enrage_" + Addition[GetLangId] + ".wav");
                }                               
                DelayAction.Add(200, () =>
                {
                    if (Menu.Item("ursa_enrage_minimap").GetValue<bool>())
                    {
                        minimap_pos2d = HUDInfo.WorldToMinimap(args.ParticleEffect.GetControlPoint(0));
                        pos.Add(minimap_pos2d);
                        remover(minimap_pos2d);
                    }                    
                });
            }

            //Spirit Breaker Charge Start
            if (args.Name.Contains("spirit_breaker_charge_start") && args.ParticleEffect.Owner.Team != me.Team && Menu.Item("spirit_breaker_charge_of_darkness").GetValue<bool>())
            {                               
                DelayAction.Add(200, () =>
                {
                    if (Menu.Item("spirit_breaker_charge_of_darkness_minimap_start").GetValue<bool>())
                    {
                        minimap_pos2d = HUDInfo.WorldToMinimap(args.ParticleEffect.GetControlPoint(0));
                        pos.Add(minimap_pos2d);
                        remover(minimap_pos2d);
                    }                    
                });
            }
        }
        static void Game_OnGameEvent(FireEventEventArgs args)
        {
            if (args.GameEvent.Name == "dota_roshan_kill")
            {
                Roshan_Dead = true;
                Roshan_Respawn_Min_Time = 480;
                Roshan_Respawn_Max_Time = 660;
            }
        }
        private static void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            if (!Game.IsInGame) return;
            var me = ObjectManager.LocalPlayer;
            if (me == null || me.Hero == null) return;

            //Check Rune
            if (((Math.Round(Game.GameTime) + Menu.Item("check_rune_sec").GetValue<Slider>().Value) % 120) == 0 && Utils.SleepCheck("check_rune") && Menu.Item("check_rune").GetValue<bool>())
            {
                if (Menu.Item("check_rune_msg").GetValue<bool>())
                {
                    MessageCheckRuneCreator(null);
                }
                if (Menu.Item("check_rune_sound").GetValue<bool>())
                {
                    PlaySound("check_rune_" + Addition[GetLangId] + ".wav");
                }                                
                Utils.Sleep(5000, "check_rune");
            }

            //Hand of Midas
            var Midas = me.Hero.FindItem("item_hand_of_midas");
            if (Midas != null && Math.Round(Midas.Cooldown) == Menu.Item("midas_sec").GetValue<Slider>().Value && Utils.SleepCheck("use_midas") && Menu.Item("use_midas").GetValue<bool>())
            {
                if (Menu.Item("use_midas_msg").GetValue<bool>())
                {
                    MessageUseMidasCreator(null);
                }
                if (Menu.Item("use_midas_sound").GetValue<bool>())
                {
                    PlaySound("use_midas_" + Addition[GetLangId] + ".wav");
                }                                
                Utils.Sleep(5000, "use_midas");
            }

            //Roshan MB Alive
            if (Roshan_Dead && Menu.Item("roshan").GetValue<bool>())
            {
                if (--Roshan_Respawn_Min_Time + 5 == 0 && Utils.SleepCheck("roshan_mb_alive"))
                {
                    if (Menu.Item("roshan_msg").GetValue<bool>())
                    {
                        MessageRoshanMBAliveCreator(null);
                    }
                    if (Menu.Item("roshan_sound").GetValue<bool>())
                    {
                        PlaySound("roshan_mb_alive_" + Addition[GetLangId] + ".wav");
                    }                                        
                    Utils.Sleep(5000, "roshan_mb_alive");
                }

            //Roshan Alive            
                if (--Roshan_Respawn_Max_Time + 5 == 0 && Utils.SleepCheck("roshan_alive"))
                {
                    if (Menu.Item("roshan_msg").GetValue<bool>())
                    {
                        MessageRoshanAliveCreator(null);
                    }
                    if (Menu.Item("roshan_sound").GetValue<bool>())
                    {
                        PlaySound("roshan_alive_" + Addition[GetLangId] + ".wav");
                    }                                       
                    Utils.Sleep(5000, "roshan_alive");
                    Roshan_Dead = false;
                }
            }
        }
        static void MessageAllyCreator(string hero, string spell)
        {
            if (Menu.Item("enable_msg").GetValue<bool>())
            { 
                informationmessage = new SideMessage(hero + spell, new Vector2(256, 128), stayTime: 5000);
                informationmessage.AddElement(new Vector2(0, 0), new Vector2(256, 128), Drawing.GetTexture("ensage_ui/other/msg0_" + Addition[GetLangId]));
                informationmessage.AddElement(new Vector2(152, 62), new Vector2(97, 55), Drawing.GetTexture("ensage_ui/heroes_horizontal/" + hero));
                informationmessage.AddElement(new Vector2(9, 62), new Vector2(55, 55), Drawing.GetTexture("ensage_ui/spellicons/" + spell));
                informationmessage.CreateMessage();
            }
        }
        static void MessageEnemyCreator(string hero, string spell)
        {
            if (Menu.Item("enable_msg").GetValue<bool>())
            {
                informationmessage = new SideMessage(hero + spell, new Vector2(256, 128), stayTime: 5000);
                informationmessage.AddElement(new Vector2(0, 0), new Vector2(256, 128), Drawing.GetTexture("ensage_ui/other/msg1_" + Addition[GetLangId]));
                informationmessage.AddElement(new Vector2(9, 62), new Vector2(97, 55), Drawing.GetTexture("ensage_ui/heroes_horizontal/" + hero));
                informationmessage.AddElement(new Vector2(193, 62), new Vector2(55, 55), Drawing.GetTexture("ensage_ui/spellicons/" + spell));
                informationmessage.CreateMessage();
            }           
        }
        static void MessageRuneCreator(string hero, string rune)
        {
            if (Menu.Item("enable_msg").GetValue<bool>())
            {
                informationmessage = new SideMessage(hero + rune, new Vector2(256, 128), stayTime: 5000);
                informationmessage.AddElement(new Vector2(0, 0), new Vector2(256, 128), Drawing.GetTexture("ensage_ui/other/msg2_" + Addition[GetLangId]));
                informationmessage.AddElement(new Vector2(9, 62), new Vector2(97, 55), Drawing.GetTexture("ensage_ui/heroes_horizontal/" + hero));
                informationmessage.AddElement(new Vector2(193, 62), new Vector2(55, 55), Drawing.GetTexture("ensage_ui/modifier_textures/" + rune));
                informationmessage.CreateMessage();
            }           
        }
        static void MessageItemCreator(string hero, string item)
        {
            if (Menu.Item("enable_msg").GetValue<bool>())
            {
                informationmessage = new SideMessage(hero + item, new Vector2(256, 128), stayTime: 5000);
                informationmessage.AddElement(new Vector2(0, 0), new Vector2(256, 128), Drawing.GetTexture("ensage_ui/other/msg3_" + Addition[GetLangId]));
                informationmessage.AddElement(new Vector2(9, 62), new Vector2(97, 55), Drawing.GetTexture("ensage_ui/heroes_horizontal/" + hero));
                informationmessage.AddElement(new Vector2(170, 62), new Vector2(113, 55), Drawing.GetTexture("ensage_ui/items/" + item));
                informationmessage.CreateMessage();
            }            
        }
        static void MessageCheckRuneCreator(string check_rune)
        {
            if (Menu.Item("enable_msg").GetValue<bool>())
            {
                informationmessage = new SideMessage("check_rune", new Vector2(256, 128), stayTime: 5000);
                informationmessage.AddElement(new Vector2(0, 0), new Vector2(256, 128), Drawing.GetTexture("ensage_ui/other/msg4_" + Addition[GetLangId]));
                informationmessage.CreateMessage();
            }            
        }
        static void MessageUseMidasCreator(string use_midas)
        {
            if (Menu.Item("enable_msg").GetValue<bool>())
            {
                informationmessage = new SideMessage("use_midas", new Vector2(256, 128), stayTime: 5000);
                informationmessage.AddElement(new Vector2(0, 0), new Vector2(256, 128), Drawing.GetTexture("ensage_ui/other/msg5_" + Addition[GetLangId]));
                informationmessage.CreateMessage();
            }            
        }
        static void MessageRoshanAliveCreator(string roshan_alive)
        {
            if (Menu.Item("enable_msg").GetValue<bool>())
            {
                informationmessage = new SideMessage("rosha_nalive", new Vector2(256, 128), stayTime: 5000);
                informationmessage.AddElement(new Vector2(0, 0), new Vector2(256, 128), Drawing.GetTexture("ensage_ui/other/msg6_" + Addition[GetLangId]));
                informationmessage.CreateMessage();
            }           
        }
        static void MessageRoshanMBAliveCreator(string roshan_mb_alive)
        {
            if (Menu.Item("enable_msg").GetValue<bool>())
            {
                informationmessage = new SideMessage("roshan_mb_alive", new Vector2(256, 128), stayTime: 5000);
                informationmessage.AddElement(new Vector2(0, 0), new Vector2(256, 128), Drawing.GetTexture("ensage_ui/other/msg7_" + Addition[GetLangId]));
                informationmessage.CreateMessage();
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
    }
}
