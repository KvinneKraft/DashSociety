// 
// Decompiled by Procyon v0.5.36
// 

package codes.biscuit.genbucket.hooks;

import org.bukkit.Material;
import org.bukkit.Location;
import org.bukkit.Bukkit;
import net.coreprotect.CoreProtect;
import net.coreprotect.CoreProtectAPI;

class CoreProtectHook
{
    private CoreProtectAPI api;
    
    CoreProtectHook() {
        this.api = ((CoreProtect)Bukkit.getServer().getPluginManager().getPlugin("CoreProtect")).getAPI();
    }
    
    void logRemoval(final String p, final Location loc, final Material mat, final byte damage) {
        this.api.logRemoval(p + " (GenBucket)", loc, mat, damage);
    }
    
    void logPlacement(final String p, final Location loc, final Material mat, final byte damage) {
        this.api.logPlacement(p + " (GenBucket)", loc, mat, damage);
    }
}
