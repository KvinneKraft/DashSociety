// 
// Decompiled by Procyon v0.5.36
// 

package codes.biscuit.genbucket.hooks;

import codes.biscuit.genbucket.utils.Bucket;
import org.bukkit.WorldBorder;
import org.bukkit.Material;
import org.bukkit.Chunk;
import org.bukkit.Location;
import org.bukkit.block.Block;
import org.bukkit.entity.Player;
import org.bukkit.plugin.PluginManager;
import java.util.HashSet;
import java.util.EnumMap;
import org.bukkit.OfflinePlayer;
import java.util.Set;
import net.milkbowl.vault.economy.Economy;
import java.util.Map;
import codes.biscuit.genbucket.GenBucket;

public class HookUtils
{
    private GenBucket main;
    private Map<Hooks, Object> enabledHooks;
    private Economy economy;
    private Set<OfflinePlayer> bypassPlayers;
    
    public HookUtils(final GenBucket main) {
        this.enabledHooks = new EnumMap<Hooks, Object>(Hooks.class);
        this.bypassPlayers = new HashSet<OfflinePlayer>();
        this.main = main;
        this.economy = (Economy)main.getServer().getServicesManager().getRegistration((Class)Economy.class).getProvider();
        final PluginManager pm = main.getServer().getPluginManager();
        if (main.getConfigValues().isFactionsHookEnabled() && pm.getPlugin("MassiveCore") != null && pm.getPlugin("Factions") != null && pm.getPlugin("Factions").getDescription().getDepend().contains("MassiveCore")) {
            main.getLogger().info("Hooked into MassiveCore factions");
            this.enabledHooks.put(Hooks.MASSIVECOREFACTIONS, new MassiveCoreHook());
        }
        else if (main.getConfigValues().isFactionsHookEnabled() && pm.getPlugin("Factions") != null) {
            main.getLogger().info("Hooked into FactionsUUID/SavageFactions");
            this.enabledHooks.put(Hooks.FACTIONSUUID, new FactionsUUIDHook());
        }
        if (main.getConfigValues().isWorldGuardHookEnabled() && pm.getPlugin("WorldGuard") != null) {
            final String pluginVersion = main.getServer().getPluginManager().getPlugin("WorldGuard").getDescription().getVersion();
            if (pluginVersion.startsWith("7") && pm.getPlugin("WorldEdit") != null) {
                main.getLogger().info("Hooked into WorldGuard 7");
                this.enabledHooks.put(Hooks.WORLDGUARD, new WorldGuard_7());
            }
            else if (pluginVersion.startsWith("6")) {
                main.getLogger().info("Hooked into WorldGuard 6");
                this.enabledHooks.put(Hooks.WORLDGUARD, new WorldGuard_6());
            }
        }
        if (main.getConfigValues().isWorldBorderHookEnabled() && pm.getPlugin("WorldBorder") != null) {
            main.getLogger().info("Hooked into WorldBorder");
            this.enabledHooks.put(Hooks.WORLDBORDER, new WorldBorderHook());
        }
        if (main.getConfigValues().isCoreProtectHookEnabled() && pm.getPlugin("CoreProtect") != null) {
            main.getLogger().info("Hooked into CoreProtect");
            this.enabledHooks.put(Hooks.COREPROTECT, new CoreProtectHook());
        }
        if (main.serverIsBeforeFlattening()) {
            main.getLogger().info("Hooked into Minecraft 1.8-1.12");
            this.enabledHooks.put(Hooks.MINECRAFTONEEIGHT, new Minecraft_1_8());
        }
        if (main.serverIsAfterOffhand()) {
            main.getLogger().info("Hooked into Minecraft 1.9+");
            this.enabledHooks.put(Hooks.MINECRAFTONENINE, new Minecraft_1_9());
        }
    }
    
    public void clearOffhand(final Player p) {
        if (this.enabledHooks.containsKey(Hooks.MINECRAFTONENINE)) {
            final MinecraftAbstraction minecraftHook = this.enabledHooks.get(Hooks.MINECRAFTONENINE);
            minecraftHook.clearOffhand(p);
        }
    }
    
    public void setData(final Block block, final byte data) {
        if (this.enabledHooks.containsKey(Hooks.MINECRAFTONEEIGHT)) {
            final MinecraftAbstraction minecraftHook = this.enabledHooks.get(Hooks.MINECRAFTONEEIGHT);
            minecraftHook.setBlockData(block, data);
        }
    }
    
    public boolean canPlaceHere(final Player p, final Location loc) {
        if (!p.hasPermission("genbucket.use")) {
            this.cannotPlaceNoPermission(p);
            return false;
        }
        if (loc.getBlockY() > this.main.getConfigValues().getMaxY()) {
            this.cannotPlaceYLevel(p);
            return false;
        }
        if (this.enabledHooks.containsKey(Hooks.MASSIVECOREFACTIONS)) {
            final MassiveCoreHook massiveCoreHook = this.enabledHooks.get(Hooks.MASSIVECOREFACTIONS);
            if (this.main.getConfigValues().needsFaction() && massiveCoreHook.hasNoFaction(p)) {
                this.cannotPlaceNoFaction(p);
                return false;
            }
            if (!massiveCoreHook.isWilderness(loc) && massiveCoreHook.locationIsNotFaction(loc, p)) {
                this.onlyClaim(p);
                return false;
            }
            if (this.main.getConfigValues().cantPlaceWilderness() && massiveCoreHook.isWilderness(loc)) {
                this.onlyClaim(p);
                return false;
            }
        }
        else if (this.enabledHooks.containsKey(Hooks.FACTIONSUUID)) {
            final FactionsUUIDHook factionsUUIDHook = this.enabledHooks.get(Hooks.FACTIONSUUID);
            if (this.main.getConfigValues().needsFaction() && !factionsUUIDHook.hasFaction(p)) {
                this.cannotPlaceNoFaction(p);
                return false;
            }
            if (factionsUUIDHook.isNotWilderness(loc) && !factionsUUIDHook.locationIsFactionClaim(loc, p)) {
                this.onlyClaim(p);
                return false;
            }
            if (this.main.getConfigValues().cantPlaceWilderness() && !factionsUUIDHook.isNotWilderness(loc)) {
                this.onlyClaim(p);
                return false;
            }
        }
        return true;
    }
    
    public boolean canGenChunk(final Player p, final Chunk chunk) {
        final Location middle = chunk.getBlock(7, 63, 7).getLocation();
        if (this.enabledHooks.containsKey(Hooks.MASSIVECOREFACTIONS)) {
            final MassiveCoreHook massiveCoreHook = this.enabledHooks.get(Hooks.MASSIVECOREFACTIONS);
            return (!this.main.getConfigValues().needsFaction() || !massiveCoreHook.hasNoFaction(p)) && (massiveCoreHook.isWilderness(middle) || !massiveCoreHook.locationIsNotFaction(middle, p)) && (!this.main.getConfigValues().cantPlaceWilderness() || !massiveCoreHook.isWilderness(middle));
        }
        if (this.enabledHooks.containsKey(Hooks.FACTIONSUUID)) {
            final FactionsUUIDHook factionsUUIDHook = this.enabledHooks.get(Hooks.FACTIONSUUID);
            return (!this.main.getConfigValues().needsFaction() || factionsUUIDHook.hasFaction(p)) && (!factionsUUIDHook.isNotWilderness(middle) || factionsUUIDHook.locationIsFactionClaim(middle, p)) && (!this.main.getConfigValues().cantPlaceWilderness() || factionsUUIDHook.isNotWilderness(middle));
        }
        return true;
    }
    
    public boolean canGenBlock(final Player p, final Location block, final boolean horizontal) {
        if (this.enabledHooks.containsKey(Hooks.WORLDGUARD)) {
            final WorldGuardHook worldGuardHook = this.enabledHooks.get(Hooks.WORLDGUARD);
            if (!worldGuardHook.canBreakBlock(block, p)) {
                return false;
            }
        }
        if (this.main.getConfigValues().getSpongeCheckRadius() > 0.0) {
            for (double radius = this.main.getConfigValues().getSpongeCheckRadius(), x = block.getX() - radius; x < block.getX() + radius; ++x) {
                for (double y = block.getY() - radius; y < block.getY() + radius; ++y) {
                    for (double z = block.getZ() - radius; z < block.getZ() + radius; ++z) {
                        final Block b = new Location(block.getWorld(), x, y, z).getBlock();
                        if (!b.getLocation().equals((Object)block) && b.getType() == Material.SPONGE) {
                            return false;
                        }
                    }
                }
            }
        }
        if (horizontal) {
            if (this.enabledHooks.containsKey(Hooks.WORLDBORDER)) {
                final WorldBorderHook worldBorderHook = this.enabledHooks.get(Hooks.WORLDBORDER);
                if (!worldBorderHook.isInsideBorder(block)) {
                    return false;
                }
            }
            final WorldBorder border = block.getWorld().getWorldBorder();
            final double x2 = block.getBlockX();
            final double z2 = block.getBlockZ();
            final double size = border.getSize() / 2.0;
            final Location center = border.getCenter();
            return x2 < center.clone().add(size, 0.0, 0.0).getX() && z2 < center.clone().add(0.0, 0.0, size).getZ() && x2 > center.clone().subtract(size, 0.0, 0.0).getX() && z2 > center.clone().subtract(0.0, 0.0, size).getZ();
        }
        return true;
    }
    
    private void cannotPlaceNoFaction(final Player p) {
        if (!this.main.getConfigValues().getNoFactionMessage().equals("")) {
            p.sendMessage(this.main.getConfigValues().getNoFactionMessage());
        }
    }
    
    private void onlyClaim(final Player p) {
        if (!this.main.getConfigValues().getOnlyClaimMessage().equals("")) {
            p.sendMessage(this.main.getConfigValues().getOnlyClaimMessage());
        }
    }
    
    private void cannotPlaceNoPermission(final Player p) {
        if (!this.main.getConfigValues().getNoPermissionPlaceMessage().equals("")) {
            p.sendMessage(this.main.getConfigValues().getNoPermissionPlaceMessage());
        }
    }
    
    private void cannotPlaceYLevel(final Player p) {
        if (!this.main.getConfigValues().getCannotPlaceYLevelMessage().equals("")) {
            p.sendMessage(this.main.getConfigValues().getCannotPlaceYLevelMessage());
        }
    }
    
    public boolean takeBucketPlaceCost(final Player p, final Bucket bucket) {
        if (this.bypassPlayers.contains(p)) {
            return true;
        }
        if (this.hasMoney(p, bucket.getPlacePrice())) {
            this.removeMoney(p, bucket.getPlacePrice());
            return true;
        }
        if (!this.main.getConfigValues().notEnoughMoneyPlaceMessage(bucket.getPlacePrice()).equals("")) {
            p.sendMessage(this.main.getConfigValues().notEnoughMoneyPlaceMessage(bucket.getPlacePrice()));
        }
        return false;
    }
    
    public boolean takeShopMoney(final Player p, final double amount) {
        if (this.bypassPlayers.contains(p)) {
            return true;
        }
        if (this.hasMoney(p, amount)) {
            this.removeMoney(p, amount);
            return true;
        }
        if (!this.main.getConfigValues().notEnoughMoneyBuyMessage(amount).equals("")) {
            p.sendMessage(this.main.getConfigValues().notEnoughMoneyBuyMessage(amount));
        }
        return false;
    }
    
    public boolean isFriendlyPlayer(final Player p, final Player p2) {
        if (this.enabledHooks.containsKey(Hooks.MASSIVECOREFACTIONS)) {
            final MassiveCoreHook massiveCoreHook = this.enabledHooks.get(Hooks.MASSIVECOREFACTIONS);
            return massiveCoreHook.isFriendlyPlayer(p, p2);
        }
        if (this.enabledHooks.containsKey(Hooks.FACTIONSUUID)) {
            final FactionsUUIDHook factionsUUIDHook = this.enabledHooks.get(Hooks.FACTIONSUUID);
            return factionsUUIDHook.isFriendlyPlayer(p, p2);
        }
        return false;
    }
    
    private boolean hasMoney(final Player p, final double money) {
        return this.economy.has((OfflinePlayer)p, money);
    }
    
    private void removeMoney(final Player p, final double money) {
        this.economy.withdrawPlayer((OfflinePlayer)p, money);
    }
    
    public void logRemoval(final Player p, final Location loc, final Material mat, final byte damage) {
        if (this.enabledHooks.containsKey(Hooks.COREPROTECT)) {
            final CoreProtectHook coreProtectHook = this.enabledHooks.get(Hooks.COREPROTECT);
            coreProtectHook.logRemoval(p.getName(), loc, mat, damage);
        }
    }
    
    public void logPlacement(final Player p, final Location loc, final Material mat, final byte damage) {
        if (this.enabledHooks.containsKey(Hooks.COREPROTECT)) {
            final CoreProtectHook coreProtectHook = this.enabledHooks.get(Hooks.COREPROTECT);
            coreProtectHook.logPlacement(p.getName(), loc, mat, damage);
        }
    }
    
    public Set<OfflinePlayer> getBypassPlayers() {
        return this.bypassPlayers;
    }
    
    enum Hooks
    {
        FACTIONSUUID, 
        MASSIVECOREFACTIONS, 
        COREPROTECT, 
        WORLDGUARD, 
        WORLDBORDER, 
        MINECRAFTONEEIGHT, 
        MINECRAFTONENINE;
    }
}
