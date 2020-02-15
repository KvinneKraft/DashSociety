// 
// Decompiled by Procyon v0.5.36
// 

package codes.biscuit.genbucket.hooks;

import org.bukkit.entity.Player;
import org.bukkit.block.Block;

public interface MinecraftAbstraction
{
    void setBlockData(final Block p0, final byte p1);
    
    void clearOffhand(final Player p0);
}
