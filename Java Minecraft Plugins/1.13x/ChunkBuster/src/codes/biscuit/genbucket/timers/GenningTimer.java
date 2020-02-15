// 
// Decompiled by Procyon v0.5.36
// 

package codes.biscuit.genbucket.timers;

import org.bukkit.Material;
import org.bukkit.Chunk;
import org.bukkit.block.BlockFace;
import codes.biscuit.genbucket.GenBucket;
import org.bukkit.block.Block;
import codes.biscuit.genbucket.utils.Bucket;
import org.bukkit.entity.Player;
import org.bukkit.scheduler.BukkitRunnable;

public class GenningTimer extends BukkitRunnable
{
    private Player p;
    private Bucket bucket;
    private Block startingBlock;
    private Block currentBlock;
    private GenBucket main;
    private BlockFace direction;
    private int limit;
    private int blockCounter;
    private int chunkCounter;
    private Chunk previousChunk;
    
    public GenningTimer(final Player p, final Bucket bucket, final Block startingBlock, final BlockFace direction, final GenBucket main, final int limit) {
        this.blockCounter = 0;
        this.chunkCounter = 0;
        this.previousChunk = null;
        this.p = p;
        this.bucket = bucket;
        this.startingBlock = startingBlock;
        this.currentBlock = startingBlock;
        this.direction = direction;
        this.main = main;
        this.limit = limit;
    }
    
    public void run() {
        if (this.blockCounter < this.limit && this.currentBlock.getY() <= this.main.getConfigValues().getMaxY() && this.main.getHookUtils().canGenBlock(this.p, this.currentBlock.getLocation(), this.direction != BlockFace.UP && this.direction != BlockFace.DOWN) && (this.main.getConfigValues().getIgnoredBlockList().contains(this.currentBlock.getType()) || (this.bucket.isPatch() && this.currentBlock.getType() == this.bucket.getBlockItem().getType()) || (this.bucket.isDelete() && !this.main.getConfigValues().getDeleteBlacklist().contains(this.currentBlock.getType())) || (this.bucket.getBlockItem().getType().hasGravity() && this.direction == BlockFace.DOWN && this.main.getConfigValues().addBlockUnderGravity() && this.currentBlock.getType() == this.main.getConfigValues().getGravityBlock().getType()))) {
            if (this.previousChunk == null || !this.previousChunk.equals(this.currentBlock.getChunk())) {
                this.previousChunk = this.currentBlock.getChunk();
                if (!this.main.getHookUtils().canGenChunk(this.p, this.currentBlock.getChunk())) {
                    this.cancel();
                    return;
                }
            }
            this.main.getHookUtils().logRemoval(this.p, this.currentBlock.getLocation(), this.currentBlock.getType(), this.currentBlock.getData());
            this.main.getHookUtils().logPlacement(this.p, this.currentBlock.getLocation(), this.bucket.getBlockItem().getType(), this.bucket.getBlockItem().getData().getData());
            this.currentBlock.setType(this.bucket.getBlockItem().getType());
            if (this.main.serverIsBeforeFlattening()) {
                this.main.getHookUtils().setData(this.currentBlock, this.bucket.getBlockItem().getData().getData());
            }
            if (this.bucket.getBlockItem().getType().hasGravity() && this.direction == BlockFace.DOWN && this.main.getConfigValues().addBlockUnderGravity()) {
                final Block underblock = this.currentBlock.getRelative(BlockFace.DOWN);
                if (underblock.getY() <= this.main.getConfigValues().getMaxY() && this.main.getHookUtils().canGenBlock(this.p, underblock.getLocation(), false) && (this.main.getConfigValues().getIgnoredBlockList().contains(underblock.getType()) || (this.bucket.isPatch() && underblock.getType() == this.bucket.getBlockItem().getType()))) {
                    underblock.setType(this.main.getConfigValues().getGravityBlock().getType());
                    if (this.main.serverIsBeforeFlattening()) {
                        this.main.getHookUtils().setData(underblock, this.main.getConfigValues().getGravityBlock().getData().getData());
                    }
                }
            }
            if (this.bucket.isByChunk() && this.currentBlock.getChunk() != this.currentBlock.getRelative(this.direction).getChunk()) {
                ++this.chunkCounter;
                if (this.chunkCounter >= this.main.getConfigValues().getMaxChunks()) {
                    this.cancel();
                }
            }
            this.currentBlock = this.currentBlock.getRelative(this.direction);
            if (this.main.getConfigValues().cancellingEnabled()) {
                Material wool;
                try {
                    wool = Material.valueOf("WOOL");
                }
                catch (IllegalArgumentException ex) {
                    wool = Material.valueOf("LIME_WOOL");
                }
                this.p.sendBlockChange(this.startingBlock.getLocation(), wool, (byte)5);
            }
        }
        else {
            if (this.main.getConfigValues().cancellingEnabled()) {
                this.main.getUtils().getCurrentGens().remove(this.startingBlock.getLocation());
                this.p.sendBlockChange(this.startingBlock.getLocation(), this.startingBlock.getType(), this.startingBlock.getData());
            }
            this.cancel();
        }
        ++this.blockCounter;
    }
}
