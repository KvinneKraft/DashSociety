// 
// Decompiled by Procyon v0.5.36
// 

package codes.biscuit.genbucket.hooks;

import org.bukkit.entity.Player;
import org.bukkit.block.Block;

public class Minecraft_1_8 implements MinecraftAbstraction
{
    public void setBlockData(final Block block, final byte data) {
        block.setData(data);
    }
    
    public void clearOffhand(final Player p) {
    }
}
