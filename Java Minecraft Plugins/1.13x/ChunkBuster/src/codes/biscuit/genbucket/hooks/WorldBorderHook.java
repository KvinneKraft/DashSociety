// 
// Decompiled by Procyon v0.5.36
// 

package codes.biscuit.genbucket.hooks;

import com.wimbli.WorldBorder.BorderData;
import com.wimbli.WorldBorder.Config;
import org.bukkit.Location;

class WorldBorderHook
{
    boolean isInsideBorder(final Location loc) {
        final BorderData border = Config.Border(loc.getWorld().getName());
        return border == null || border.insideBorder(loc);
    }
}
