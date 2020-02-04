package com.dash;

import java.util.ArrayList;
import java.util.List;
import org.bukkit.entity.Player;
import org.bukkit.event.EventHandler;
import org.bukkit.event.Listener;
import org.bukkit.event.player.PlayerFishEvent;
import org.bukkit.inventory.ItemStack;

public class EventsHandler implements Listener
{
    public List<ItemStack> reward_materials = new ArrayList<>();
    public List<Double> reward_chances = new ArrayList<>();        
    
    String fishy_permission = FishRewardz.config.getString("fishy-permission");
    String reward_message = Moony.transStr(FishRewardz.config.getString("reward-message"));
    
    @EventHandler
    public void onFishy(PlayerFishEvent e)
    {
        if(e.getCaught() == null)
            return;
        
        // Implement Random Drops
        
        int reward_id = 0;
        
        Player p = e.getPlayer();
        p.sendMessage(reward_message.replace("%reward%", reward_materials.get(reward_id).getType().toString()));
        
        return;
    };
};