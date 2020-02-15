// 
// Decompiled by Procyon v0.5.36
// 

package codes.biscuit.genbucket.commands;

import java.util.Map;
import codes.biscuit.genbucket.utils.Bucket;
import org.bukkit.OfflinePlayer;
import org.bukkit.entity.Player;
import org.bukkit.inventory.ItemStack;
import net.md_5.bungee.api.ChatColor;
import org.bukkit.Bukkit;
import java.util.Iterator;
import java.util.Collection;
import java.util.ArrayList;
import java.util.Arrays;
import java.util.List;
import org.bukkit.command.Command;
import org.bukkit.command.CommandSender;
import codes.biscuit.genbucket.GenBucket;
import org.bukkit.command.TabExecutor;

public class GenBucketAdminCommand implements TabExecutor
{
    private GenBucket main;
    
    public GenBucketAdminCommand(final GenBucket main) {
        this.main = main;
    }
    
    public List<String> onTabComplete(final CommandSender sender, final Command cmd, final String alias, final String[] args) {
        if (args.length == 1) {
            final List<String> arguments = new ArrayList<String>(Arrays.asList("give", "reload", "bypass"));
            for (final String arg : Arrays.asList("give", "reload", "bypass")) {
                if (!arg.startsWith(args[0].toLowerCase())) {
                    arguments.remove(arg);
                }
            }
            return arguments;
        }
        if (args.length == 3 && args[0].equalsIgnoreCase("give")) {
            final List<String> arguments = new ArrayList<String>(this.main.getBucketManager().getBuckets().keySet());
            for (final String arg : this.main.getBucketManager().getBuckets().keySet()) {
                if (!arg.startsWith(args[2].toLowerCase())) {
                    arguments.remove(arg);
                }
            }
            return arguments;
        }
        return null;
    }
    
    public boolean onCommand(final CommandSender sender, final Command cmd, final String label, final String[] args) {
        if (args.length > 0) {
            final String lowerCase = args[0].toLowerCase();
            switch (lowerCase) {
                case "give": {
                    if (sender.hasPermission("genbucket.give")) {
                        if (args.length > 1) {
                            final Player p = Bukkit.getPlayerExact(args[1]);
                            if (p != null) {
                                if (args.length > 2) {
                                    final Bucket bucket = this.main.getBucketManager().getBucket(args[2]);
                                    if (bucket != null) {
                                        int giveAmount = 1;
                                        if (args.length > 3) {
                                            try {
                                                giveAmount = Integer.parseInt(args[3]);
                                            }
                                            catch (NumberFormatException ex) {
                                                sender.sendMessage(ChatColor.RED + "This isn't a valid give amount!");
                                                return false;
                                            }
                                        }
                                        final ItemStack item = bucket.getItem();
                                        item.setAmount(giveAmount);
                                        if (!this.main.getConfigValues().giveShouldDropItem()) {
                                            if (giveAmount >= 65) {
                                                sender.sendMessage(ChatColor.RED + "You can only give 64 at a time!");
                                                return true;
                                            }
                                            if (p.getInventory().firstEmpty() == -1) {
                                                sender.sendMessage(ChatColor.RED + "This player doesn't have any empty slots in their inventory!");
                                                return true;
                                            }
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
                                        final double price = bucket.getBuyPrice() * giveAmount;
                                        if (!this.main.getConfigValues().getGiveMessage(p, giveAmount, bucket).equals("")) {
                                            sender.sendMessage(this.main.getConfigValues().getGiveMessage(p, giveAmount, bucket));
                                        }
                                        if (!this.main.getConfigValues().getReceiveMessage(giveAmount, price, bucket).equals("")) {
                                            p.sendMessage(this.main.getConfigValues().getReceiveMessage(giveAmount, price, bucket));
                                        }
                                    }
                                    else {
                                        sender.sendMessage(ChatColor.RED + "This bucket does not exist!");
                                    }
                                }
                                else {
                                    sender.sendMessage(ChatColor.RED + "Please specify a bucket!");
                                }
                            }
                            else {
                                sender.sendMessage(ChatColor.RED + "This player is not online!");
                            }
                            break;
                        }
                        sender.sendMessage(ChatColor.RED + "Please specify a player!");
                        break;
                    }
                    else {
                        if (!this.main.getConfigValues().getNoPermissionCommandMessage().equals("")) {
                            sender.sendMessage(this.main.getConfigValues().getNoPermissionCommandMessage());
                            break;
                        }
                        break;
                    }
                    break;
                }
                case "reload": {
                    if (sender.hasPermission("genbucket.reload")) {
                        this.main.reloadConfig();
                        this.main.getConfigValues().loadBuckets();
                        this.main.getUtils().reloadRecipes();
                        sender.sendMessage(ChatColor.GREEN + "Successfully reloaded the config. Most values have been instantly updated.");
                        break;
                    }
                    if (!this.main.getConfigValues().getNoPermissionCommandMessage().equals("")) {
                        sender.sendMessage(this.main.getConfigValues().getNoPermissionCommandMessage());
                        break;
                    }
                    break;
                }
                case "bypass": {
                    if (sender instanceof Player) {
                        final Player p = (Player)sender;
                        if (sender.hasPermission("genbucket.bypass")) {
                            if (this.main.getHookUtils().getBypassPlayers().contains(p)) {
                                sender.sendMessage(ChatColor.RED + "You can no longer place genbuckets anywhere infinitely.");
                                this.main.getHookUtils().getBypassPlayers().remove(p);
                            }
                            else {
                                sender.sendMessage(ChatColor.GREEN + "You can now place genbuckets anywhere infinitely.");
                                this.main.getHookUtils().getBypassPlayers().add((OfflinePlayer)p);
                            }
                        }
                        else if (!this.main.getConfigValues().getNoPermissionCommandMessage().equals("")) {
                            sender.sendMessage(this.main.getConfigValues().getNoPermissionCommandMessage());
                        }
                        break;
                    }
                    sender.sendMessage(ChatColor.RED + "You can only use this command in-game!");
                    break;
                }
                default: {
                    sender.sendMessage(ChatColor.RED + "Invalid argument!");
                    break;
                }
            }
        }
        else {
            sender.sendMessage(ChatColor.GRAY.toString() + ChatColor.STRIKETHROUGH + "--------------" + ChatColor.GRAY + "[" + ChatColor.LIGHT_PURPLE + ChatColor.BOLD + " GenBucket " + ChatColor.GRAY + "]" + ChatColor.GRAY.toString() + ChatColor.STRIKETHROUGH + "--------------");
            sender.sendMessage(ChatColor.LIGHT_PURPLE + "\u25cf /gba give <player> <bucket> [amount] " + ChatColor.GRAY + "- Give a player (a) genbucket(s)");
            sender.sendMessage(ChatColor.LIGHT_PURPLE + "\u25cf /gba reload " + ChatColor.GRAY + "- Reload the config");
            sender.sendMessage(ChatColor.LIGHT_PURPLE + "\u25cf /gba bypass " + ChatColor.GRAY + "- Buy and place genbuckets anywhere infinitely and for free!");
            sender.sendMessage(ChatColor.GRAY.toString() + ChatColor.ITALIC + "v" + this.main.getDescription().getVersion() + " by Biscut");
            sender.sendMessage(ChatColor.GRAY.toString() + ChatColor.STRIKETHROUGH + "------------------------------------------------");
        }
        return false;
    }
}
