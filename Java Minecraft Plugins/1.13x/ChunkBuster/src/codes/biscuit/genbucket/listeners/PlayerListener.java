// 
// Decompiled by Procyon v0.5.36
// 

package codes.biscuit.genbucket.listeners;

import org.bukkit.event.block.BlockBreakEvent;
import org.bukkit.Location;
import org.bukkit.event.block.BlockDamageEvent;
import org.bukkit.event.player.PlayerJoinEvent;
import java.util.Map;
import org.bukkit.event.inventory.ClickType;
import org.bukkit.event.inventory.InventoryClickEvent;
import org.bukkit.plugin.Plugin;
import codes.biscuit.genbucket.timers.GenningTimer;
import org.bukkit.inventory.ItemStack;
import org.bukkit.block.BlockFace;
import org.bukkit.block.Block;
import org.bukkit.event.player.PlayerDropItemEvent;
import java.util.Iterator;
import org.bukkit.entity.Entity;
import org.bukkit.entity.Player;
import org.bukkit.Material;
import org.bukkit.event.block.Action;
import org.bukkit.event.player.PlayerInteractEvent;
import org.bukkit.event.EventPriority;
import org.bukkit.event.EventHandler;
import codes.biscuit.genbucket.utils.Bucket;
import org.bukkit.event.block.BlockPlaceEvent;
import codes.biscuit.genbucket.GenBucket;
import org.bukkit.event.Listener;

public class PlayerListener implements Listener
{
    private GenBucket main;
    
    public PlayerListener(final GenBucket main) {
        this.main = main;
    }
    
    @EventHandler(priority = EventPriority.HIGHEST)
    public void onBlockPlace(final BlockPlaceEvent e) {
        if (e.isCancelled() && !this.main.getHookUtils().getBypassPlayers().contains(e.getPlayer())) {
            return;
        }
        final Bucket matchedBucket = this.main.getBucketManager().matchBucket(e.getItemInHand());
        if (matchedBucket != null) {
            e.setCancelled(true);
            this.startGenBucket(matchedBucket, e.getPlayer(), e.getBlock(), e.getBlockAgainst().getFace(e.getBlock()), e.getItemInHand());
        }
    }
    
    @EventHandler(priority = EventPriority.HIGHEST)
    public void onBucketPlace(final PlayerInteractEvent e) {
        if (e.isCancelled() && !this.main.getHookUtils().getBypassPlayers().contains(e.getPlayer())) {
            return;
        }
        if (e.getAction().equals((Object)Action.RIGHT_CLICK_BLOCK) && e.getItem() != null && (e.getItem().getType().equals((Object)Material.LAVA_BUCKET) || e.getItem().getType().equals((Object)Material.WATER_BUCKET))) {
            final Bucket bucket = this.main.getBucketManager().matchBucket(e.getItem());
            if (bucket != null) {
                e.setCancelled(true);
                this.startGenBucket(bucket, e.getPlayer(), e.getClickedBlock().getRelative(e.getBlockFace()), e.getBlockFace(), e.getItem());
            }
        }
    }
    
    private boolean noPlayersNearby(final Player p) {
        final double radius = this.main.getConfigValues().getPlayerCheckRadius();
        if (radius != 0.0) {
            for (final Entity entity : p.getNearbyEntities(radius, radius, radius)) {
                if (entity instanceof Player && !this.main.getHookUtils().isFriendlyPlayer(p, (Player)entity)) {
                    if (!this.main.getConfigValues().getNearbyPlayerMessage().equals("")) {
                        p.sendMessage(this.main.getConfigValues().getNearbyPlayerMessage());
                    }
                    return false;
                }
            }
        }
        return true;
    }
    
    @EventHandler(priority = EventPriority.HIGHEST, ignoreCancelled = true)
    public void onItemDrop(final PlayerDropItemEvent e) {
        if (this.main.getConfigValues().bucketsDisappearDrop()) {
            final Bucket bucket = this.main.getBucketManager().matchBucket(e.getItemDrop().getItemStack());
            if (bucket != null && bucket.isInfinite()) {
                e.getItemDrop().remove();
            }
        }
    }
    
    private void startGenBucket(final Bucket bucket, final Player p, final Block block, BlockFace direction, final ItemStack removeItem) {
        if (this.main.getHookUtils().canPlaceHere(p, block.getLocation()) && this.noPlayersNearby(p) && this.main.getHookUtils().takeBucketPlaceCost(p, bucket)) {
            if (bucket.getDirection() == Bucket.Direction.HORIZONTAL && (direction.equals((Object)BlockFace.UP) || direction.equals((Object)BlockFace.DOWN))) {
                if (!this.main.getConfigValues().getWrongDirectionMessage().equals("")) {
                    p.sendMessage(this.main.getConfigValues().getWrongDirectionMessage());
                }
                return;
            }
            switch (bucket.getDirection()) {
                case UPWARDS: {
                    direction = BlockFace.UP;
                    break;
                }
                case DOWNWARDS: {
                    direction = BlockFace.DOWN;
                    break;
                }
            }
            int limit = this.main.getConfigValues().getVerticalTravel();
            if (direction != BlockFace.UP && direction != BlockFace.DOWN) {
                limit = this.main.getConfigValues().getHorizontalTravel();
            }
            final GenningTimer genningTimer = new GenningTimer(p, bucket, block, direction, this.main, limit);
            genningTimer.runTaskTimer((Plugin)this.main, 0L, (long)this.main.getConfigValues().getBlockSpeedDelay());
            if (this.main.getConfigValues().cancellingEnabled()) {
                this.main.getUtils().getCurrentGens().put(block.getLocation(), genningTimer);
            }
            if (!bucket.isInfinite()) {
                if (!this.main.getHookUtils().getBypassPlayers().contains(p)) {
                    if (removeItem.getAmount() <= 1) {
                        if (this.main.serverIsAfterOffhand() && !p.getItemInHand().equals((Object)removeItem)) {
                            this.main.getHookUtils().clearOffhand(p);
                        }
                        else {
                            p.setItemInHand((ItemStack)null);
                        }
                    }
                    else {
                        removeItem.setAmount(removeItem.getAmount() - 1);
                    }
                }
                if (!this.main.getConfigValues().getPlaceNormalMessage(bucket.getPlacePrice()).equals("") && bucket.getPlacePrice() > 0.0) {
                    p.sendMessage(this.main.getConfigValues().getPlaceNormalMessage(bucket.getPlacePrice()));
                }
            }
            else if (!this.main.getConfigValues().getPlaceInfiniteMessage(bucket.getPlacePrice()).equals("") && bucket.getPlacePrice() > 0.0) {
                p.sendMessage(this.main.getConfigValues().getPlaceInfiniteMessage(bucket.getPlacePrice()));
            }
        }
    }
    
    @EventHandler
    public void onShopClick(final InventoryClickEvent e) {
        if (e.getClickedInventory() != null && e.getView().getTitle() != null && e.getView().getTitle().equals(this.main.getConfigValues().getGUITitle())) {
            e.setCancelled(true);
            final Player p = (Player)e.getWhoClicked();
            if (e.getCurrentItem() != null && e.getCurrentItem().hasItemMeta() && e.getCurrentItem().getItemMeta().hasDisplayName()) {
                final Bucket bucket = this.main.getBucketManager().fromShopName(e.getCurrentItem().getItemMeta().getDisplayName());
                if (bucket != null) {
                    double price = bucket.getBuyPrice();
                    int amount = 1;
                    if (e.getClick().equals((Object)ClickType.SHIFT_LEFT) || e.getClick().equals((Object)ClickType.SHIFT_RIGHT)) {
                        amount = 16;
                        price *= this.main.getConfigValues().getBulkBuyAmount();
                    }
                    if (this.main.getHookUtils().takeShopMoney(p, price)) {
                        final ItemStack item = bucket.getItem();
                        item.setAmount(amount);
                        if (!this.main.getConfigValues().shopShouldDropItem() && p.getInventory().firstEmpty() == -1) {
                            if (!this.main.getConfigValues().getNoSpaceBuyMessage().equals("")) {
                                p.sendMessage(this.main.getConfigValues().getNoSpaceBuyMessage());
                            }
                            return;
                        }
                        final Map excessItems = p.getInventory().addItem(new ItemStack[] { item });
                        for (final Object excessItem : excessItems.values()) {
                            int itemCount;
                            for (itemCount = ((ItemStack)excessItem).getAmount(); itemCount > 64; itemCount -= 64) {
                                ((ItemStack)excessItem).setAmount(64);
                                p.getWorld().dropItemNaturally(p.getLocation(), (ItemStack)excessItem);
                            }
                            if (itemCount > 0) {
                                ((ItemStack)excessItem).setAmount(itemCount);
                                p.getWorld().dropItemNaturally(p.getLocation(), (ItemStack)excessItem);
                            }
                        }
                        if (!this.main.getConfigValues().getBuyConfirmationMessage(amount).equals("")) {
                            p.sendMessage(this.main.getConfigValues().getBuyConfirmationMessage(amount));
                        }
                    }
                }
                else if (this.main.getConfigValues().getExitName().equals(e.getCurrentItem().getItemMeta().getDisplayName())) {
                    p.closeInventory();
                }
            }
        }
    }
    
    @EventHandler
    public void onJoin(final PlayerJoinEvent e) {
        if (this.main.getConfigValues().showUpdateMessage() && e.getPlayer().isOp()) {
            this.main.getUtils().checkUpdates(e.getPlayer());
        }
    }
    
    @EventHandler
    public void onBlockDamage(final BlockDamageEvent e) {
        if (this.main.getConfigValues().cancellingEnabled()) {
            final Location b = e.getBlock().getLocation();
            if (this.main.getUtils().getCurrentGens().containsKey(b)) {
                this.main.getUtils().getCurrentGens().get(b).cancel();
                this.main.getUtils().getCurrentGens().remove(b);
            }
        }
    }
    
    @EventHandler
    public void onBlockBreak(final BlockBreakEvent e) {
        if (this.main.getConfigValues().cancellingEnabled()) {
            final Location b = e.getBlock().getLocation();
            if (this.main.getUtils().getCurrentGens().containsKey(b)) {
                this.main.getUtils().getCurrentGens().get(b).cancel();
                this.main.getUtils().getCurrentGens().remove(b);
            }
        }
    }
}
