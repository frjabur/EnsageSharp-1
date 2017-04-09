using Ensage.Common.Menu;
using SharpDX;

namespace BeAwarePlus
{
    internal static class MenuManager
    {
        private static bool _loaded;
        public static readonly Menu Menu = new Menu("BeAwarePlus", "BeAwarePlus", true, "beawareplus", true).SetFontColor(Color.Aqua);     
           
        public static void Init()
        {
            if (_loaded)
                return;
            _loaded = true;
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
            var treedanceMenu = new Menu("Tree Dance", "Tree Dance", false, "monkey_king_tree_dance", true);
            treedanceMenu.AddItem(new MenuItem("monkey_king_tree_dance", "Enable").SetValue(true));
            treedanceMenu.AddItem(new MenuItem("monkey_king_tree_dance_msg", "Enable Information Message").SetValue(false));
            treedanceMenu.AddItem(new MenuItem("monkey_king_tree_dance_sound", "Enable Sound").SetValue(false));
            treedanceMenu.AddItem(new MenuItem("monkey_king_tree_dance_minimap", "Enable Min Map").SetValue(true));
            monkeykingMenu.AddSubMenu(treedanceMenu);

            var spiritbreakerMenu = new Menu("Spirit Breaker", "Spirit Breaker", false, "npc_dota_hero_spirit_breaker", true);
            spellsMenu.AddSubMenu(spiritbreakerMenu);
            var chargeofdarknessMenu = new Menu("Charge of Darkness", "Charge of Darkness", false, "spirit_breaker_charge_of_darkness", true);
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
            moonlightshadowMenu.AddItem(new MenuItem("mirana_invis_minimap_all_heroes", "Enable Min Map All Heroes").SetValue(true));
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
            var wrathofnatureMenu = new Menu("Wrath of Nature", "Wrath of Nature", false, "furion_wrath_of_nature", true);
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

            var townportalscrollMenu = new Menu("Town Portal Scroll", "Town Portal Scroll", false, "item_tpscroll", true);
            townportalscrollMenu.AddItem(new MenuItem("tpscroll_teleport_ally", "Enable Teleport Ally").SetValue(true));
            townportalscrollMenu.AddItem(new MenuItem("tpscroll_teleport_enemy", "Enable Teleport Enemy").SetValue(true));
            townportalscrollMenu.AddItem(new MenuItem("tpscroll_teleport_start", "Enable Teleport Start").SetValue(true));
            townportalscrollMenu.AddItem(new MenuItem("tpscroll_teleport_hero_name", "Enable Hero Name").SetValue(true));
            townportalscrollMenu.AddItem(new MenuItem("tpscroll_teleport_ally_msg", "Enable Ally Information Message").SetValue(false));
            townportalscrollMenu.AddItem(new MenuItem("tpscroll_teleport_enemy_msg", "Enable Enemy Information Message").SetValue(true));
            townportalscrollMenu.AddItem(new MenuItem("tpscroll_teleport_ally_sound", "Enable Ally Sound").SetValue(false));
            townportalscrollMenu.AddItem(new MenuItem("tpscroll_teleport_enemy_sound", "Enable Enemy Sound").SetValue(true));                       
            itemsMenu.AddSubMenu(townportalscrollMenu);

            var bootsoftravelMenu = new Menu("Boots of Travel", "Boots of Travel", false, "item_travel_boots", true);
            bootsoftravelMenu.AddItem(new MenuItem("bt_teleport_all", "Enable Teleport All").SetValue(false));
            bootsoftravelMenu.AddItem(new MenuItem("bt_teleport_enemy", "Enable Teleport Enemy").SetValue(true));
            bootsoftravelMenu.AddItem(new MenuItem("bt_teleport_enemy_msg", "Enable Enemy Information Message").SetValue(true));
            bootsoftravelMenu.AddItem(new MenuItem("bt_teleport_enemy_sound", "Enable Enemy Sound").SetValue(true));           
            bootsoftravelMenu.AddItem(new MenuItem("bt_teleport_red.X", "Red").SetValue(new Slider(255, 0, 255)).SetFontColor(Color.Red));
            bootsoftravelMenu.AddItem(new MenuItem("bt_teleport_green.Y", "Green").SetValue(new Slider(0, 0, 255)).SetFontColor(Color.Green));
            bootsoftravelMenu.AddItem(new MenuItem("bt_teleport_blue.Z", "Blue").SetValue(new Slider(0, 0, 255)).SetFontColor(Color.Blue));
            itemsMenu.AddSubMenu(bootsoftravelMenu);

            var smokeofdeceitMenu = new Menu("Smoke of Deceit", "Smoke of Deceit", false, "item_smoke_of_deceit", true);
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

            var handofmidasMenu = new Menu("Hand of Midas", "Hand of Midas", false, "item_hand_of_midas", true);
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

            //Minimap Setting Menu
            var minimapsettingMenu = new Menu("Minimap Setting", "Minimap Setting");
            minimapsettingMenu.AddItem(new MenuItem("enable_minimap", "Enable Mini Map").SetValue(true));
            minimapsettingMenu.AddItem(new MenuItem("timer", "Timer Remove Mini Map Icon").SetValue(new Slider(5, 1, 9)));
            minimapsettingMenu.AddItem(new MenuItem("mini_map_size_icon", "Mini Map Size Icon").SetValue(new Slider(0, -40, 40))).ValueChanged += DrawingMiniMap.OnValueChanged;
            minimapsettingMenu.AddItem(new MenuItem("mini_map_size_name", "Mini Map Size Name").SetValue(new Slider(0, -40, 40))).ValueChanged += DrawingMiniMap.OnValueChanged;
            minimapsettingMenu.AddItem(new MenuItem("recalibrate_x", "Recalibrate Name X").SetValue(new Slider(0, -30, 30)));
            minimapsettingMenu.AddItem(new MenuItem("recalibrate_y", "Recalibrate Name Y").SetValue(new Slider(0, -30, 30)));
            var xList = new StringList() { SList = DrawingMiniMap.DrawMinimap, SelectedIndex = 0 };
            var DrawMinimapType = new MenuItem("drawminimap_type", "Minimap Icon Type").SetValue(xList);
            minimapsettingMenu.AddItem(DrawMinimapType);
            minimapsettingMenu.AddItem(new MenuItem("text1", "Item Color:").SetFontColor(Color.Red));
            minimapsettingMenu.AddItem(new MenuItem("item_red.X", "Red").SetValue(new Slider(255, 0, 255)).SetFontColor(Color.Red));
            minimapsettingMenu.AddItem(new MenuItem("item_green.Y", "Green").SetValue(new Slider(0, 0, 255)).SetFontColor(Color.Green));
            minimapsettingMenu.AddItem(new MenuItem("item_blue.Z", "Blue").SetValue(new Slider(0, 0, 255)).SetFontColor(Color.Blue));
            settingsMenu.AddSubMenu(minimapsettingMenu);

            //Menu
            Menu.AddItem(new MenuItem("enable_msg", "Enable Information Message").SetValue(true));
            Menu.AddItem(new MenuItem("enable_sound", "Enable Sound").SetValue(true));
            Menu.AddItem(new MenuItem("enable_default_sound", "Enable Default Sound").SetValue(false)).SetTooltip("All Sounds Becomes Default");
            var sList = new StringList() { SList = BeAwarePlus.LangName, SelectedIndex = 0 };
            var language = new MenuItem("lang", "Language").SetValue(sList);
            Menu.AddItem(language);           
            Menu.AddToMainMenu();                                  
        }       
    }
}