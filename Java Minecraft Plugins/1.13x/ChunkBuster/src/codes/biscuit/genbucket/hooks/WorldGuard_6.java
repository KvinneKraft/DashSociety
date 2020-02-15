// 
// Decompiled by Procyon v0.5.36
// 

package codes.biscuit.genbucket.hooks;

import com.sk89q.worldguard.LocalPlayer;
import com.sk89q.worldguard.protection.ApplicableRegionSet;
import com.sk89q.worldguard.bukkit.RegionQuery;
import com.sk89q.worldguard.bukkit.RegionContainer;
import com.sk89q.worldguard.protection.association.RegionAssociable;
import com.sk89q.worldguard.protection.flags.DefaultFlag;
import com.sk89q.worldguard.protection.flags.StateFlag;
import org.bukkit.Bukkit;
import com.sk89q.worldguard.bukkit.WorldGuardPlugin;
import org.bukkit.entity.Player;
import org.bukkit.Location;

public class WorldGuard_6 implements WorldGuardHook
{
    public boolean canBreakBlock(final Location block, final Player p) {
        final WorldGuardPlugin worldGuardPlugin = (WorldGuardPlugin)Bukkit.getServer().getPluginManager().getPlugin("WorldGuard");
        final RegionContainer container = worldGuardPlugin.getRegionContainer();
        final RegionQuery query = container.createQuery();
        final ApplicableRegionSet set = query.getApplicableRegions(block);
        final LocalPlayer localPlayer = worldGuardPlugin.wrapPlayer(p);
        return set.queryState((RegionAssociable)localPlayer, new StateFlag[] { DefaultFlag.BLOCK_BREAK }) != StateFlag.State.DENY;
    }
}
