
// Author: Dashie
// Version: 1.0


package com.dashrobot;


import org.bukkit.configuration.file.FileConfiguration;


//---//
//---// Main Data Refresh Class:
//---//

public class Refresh
{
    public void Action()
    {
        Captcha.plugin.reloadConfig();
        Captcha.config = Captcha.plugin.getConfig();
        
        FileConfiguration config = Captcha.config;
        Events events = Captcha.events;
        
        events.timeout_kick_message = config.getString("dash-captcha.messages.timeout-kick-message");
        events.maximum_succeed_message = config.getString("dash-captcha.messages.too-many-tries-kick-message");
        
        events.inventory_slots = config.getInt("dash-captcha.properties.inventory-slots");
        events.verification_timeout = config.getInt("dash-captcha.properties.verification-timeout");
        events.maximum_attempts = config.getInt("dash-captcha.properties.maximum-attempts");
    };
};
