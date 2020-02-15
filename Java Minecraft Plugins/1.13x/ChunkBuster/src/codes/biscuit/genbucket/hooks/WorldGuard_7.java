// 
// Decompiled by Procyon v0.5.36
// 

package codes.biscuit.genbucket.hooks;

import com.sk89q.worldguard.LocalPlayer;
import com.sk89q.worldguard.protection.ApplicableRegionSet;
import com.sk89q.worldguard.protection.regions.RegionQuery;
import com.sk89q.worldguard.protection.regions.RegionContainer;
import com.sk89q.worldguard.protection.association.RegionAssociable;
import com.sk89q.worldguard.protection.flags.Flags;
import com.sk89q.worldguard.protection.flags.StateFlag;
import com.sk89q.worldguard.bukkit.WorldGuardPlugin;
import com.sk89q.worldedit.bukkit.BukkitAdapter;
import com.sk89q.worldguard.WorldGuard;
import org.bukkit.entity.Player;
import org.bukkit.Location;

public class WorldGuard_7 implements WorldGuardHook
{
    public boolean canBreakBlock(final Location block, final Player p) {
        final WorldGuard worldGuard = WorldGuard.getInstance();
        final RegionContainer container = worldGuard.getPlatform().getRegionContainer();
        final RegionQuery regionQuery = container.createQuery();
        final ApplicableRegionSet set = regionQuery.getApplicableRegions(BukkitAdapter.adapt(block));
        final LocalPlayer localPlayer = WorldGuardPlugin.inst().wrapPlayer(p);
        return set.queryState((RegionAssociable)localPlayer, new StateFlag[] { Flags.BLOCK_BREAK }) != StateFlag.State.DENY;
    }
}
