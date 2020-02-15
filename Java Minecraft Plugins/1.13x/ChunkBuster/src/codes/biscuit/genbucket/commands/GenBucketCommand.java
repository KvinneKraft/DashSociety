// 
// Decompiled by Procyon v0.5.36
// 

package codes.biscuit.genbucket.commands;

import codes.biscuit.genbucket.utils.Bucket;
import org.bukkit.inventory.meta.ItemMeta;
import org.bukkit.inventory.ItemStack;
import org.bukkit.inventory.Inventory;
import net.md_5.bungee.api.ChatColor;
import java.util.List;
import org.bukkit.Material;
import org.bukkit.inventory.InventoryHolder;
import org.bukkit.Bukkit;
import org.bukkit.entity.Player;
import org.bukkit.command.Command;
import org.bukkit.command.CommandSender;
import codes.biscuit.genbucket.GenBucket;
import org.bukkit.command.CommandExecutor;

public class GenBucketCommand implements CommandExecutor
{
    private GenBucket main;
    
    public GenBucketCommand(final GenBucket main) {
        this.main = main;
    }
    
    public boolean onCommand(final CommandSender sender, final Command command, final String label, final String[] args) {
        if (sender instanceof Player) {
            if (sender.hasPermission("genbucket.shop")) {
                final Player p = (Player)sender;
                if (this.main.getConfigValues().isGUIEnabled() && !p.getOpenInventory().getTitle().contains(this.main.getConfigValues().getGUITitle())) {
                    final Inventory confirmInv = Bukkit.createInventory((InventoryHolder)null, 9 * this.main.getConfigValues().getGUIRows(), this.main.getConfigValues().getGUITitle());
                    ItemStack exitItem = this.main.getConfigValues().getExitItemStack();
                    if (!exitItem.getType().equals((Object)Material.AIR)) {
                        final ItemMeta cancelItemMeta = exitItem.getItemMeta();
                        cancelItemMeta.setDisplayName(this.main.getConfigValues().getExitName());
                        cancelItemMeta.setLore((List)this.main.getConfigValues().getExitLore());
                        exitItem.setItemMeta(cancelItemMeta);
                        if (this.main.getConfigValues().isExitGlowing()) {
                            exitItem = this.main.getUtils().addGlow(exitItem);
                        }
                    }
                    ItemStack fillItem = this.main.getConfigValues().getFillItemStack();
                    if (!fillItem.getType().equals((Object)Material.AIR)) {
                        final ItemMeta fillItemMeta = fillItem.getItemMeta();
                        fillItemMeta.setDisplayName(this.main.getConfigValues().getFillName());
                        fillItemMeta.setLore((List)this.main.getConfigValues().getFillLore());
                        fillItem.setItemMeta(fillItemMeta);
                        if (this.main.getConfigValues().isFillGlowing()) {
                            fillItem = this.main.getUtils().addGlow(fillItem);
                        }
                    }
                    for (int i = 0; i < 9 * this.main.getConfigValues().getGUIRows(); ++i) {
                        final Bucket bucket = this.main.getBucketManager().fromSlot(i);
                        if (bucket != null) {
                            final ItemStack bucketItem = bucket.getGuiItem();
                            confirmInv.setItem(i, bucketItem);
                        }
                        else if (this.main.getConfigValues().getExitSlots().contains(i)) {
                            confirmInv.setItem(i, exitItem);
                        }
                        else {
                            confirmInv.setItem(i, fillItem);
                        }
                    }
                    p.openInventory(confirmInv);
                }
            }
            else if (!this.main.getConfigValues().getNoPermissionCommandMessage().equals("")) {
                sender.sendMessage(this.main.getConfigValues().getNoPermissionCommandMessage());
            }
        }
        else {
            sender.sendMessage(ChatColor.RED + "You can only use this command in-game!");
        }
        return true;
    }
}
