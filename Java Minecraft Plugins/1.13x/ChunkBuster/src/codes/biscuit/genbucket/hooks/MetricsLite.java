// 
// Decompiled by Procyon v0.5.36
// 

package codes.biscuit.genbucket.hooks;

import java.nio.charset.StandardCharsets;
import java.io.OutputStream;
import java.util.zip.GZIPOutputStream;
import java.io.ByteArrayOutputStream;
import java.io.InputStream;
import java.io.Reader;
import java.io.BufferedReader;
import java.io.InputStreamReader;
import java.io.DataOutputStream;
import java.net.URL;
import javax.net.ssl.HttpsURLConnection;
import java.util.logging.Level;
import java.lang.reflect.InvocationTargetException;
import org.bukkit.plugin.RegisteredServiceProvider;
import org.json.simple.JSONArray;
import org.json.simple.JSONObject;
import java.util.TimerTask;
import java.util.Timer;
import java.util.Iterator;
import org.bukkit.plugin.ServicePriority;
import org.bukkit.Bukkit;
import java.io.IOException;
import java.util.UUID;
import org.bukkit.configuration.file.YamlConfiguration;
import java.io.File;
import org.bukkit.plugin.Plugin;

public class MetricsLite
{
    public static final int B_STATS_VERSION = 1;
    private static final String URL = "https://bStats.org/submitData/bukkit";
    private boolean enabled;
    private static boolean logFailedRequests;
    private static boolean logSentData;
    private static boolean logResponseStatusText;
    private static String serverUUID;
    private final Plugin plugin;
    
    public MetricsLite(final Plugin plugin) {
        if (plugin == null) {
            throw new IllegalArgumentException("Plugin cannot be null!");
        }
        this.plugin = plugin;
        final File bStatsFolder = new File(plugin.getDataFolder().getParentFile(), "bStats");
        final File configFile = new File(bStatsFolder, "config.yml");
        final YamlConfiguration config = YamlConfiguration.loadConfiguration(configFile);
        if (!config.isSet("serverUuid")) {
            config.addDefault("enabled", (Object)true);
            config.addDefault("serverUuid", (Object)UUID.randomUUID().toString());
            config.addDefault("logFailedRequests", (Object)false);
            config.addDefault("logSentData", (Object)false);
            config.addDefault("logResponseStatusText", (Object)false);
            config.options().header("bStats collects some data for plugin authors like how many servers are using their plugins.\nTo honor their work, you should not disable it.\nThis has nearly no effect on the server performance!\nCheck out https://bStats.org/ to learn more :)").copyDefaults(true);
            try {
                config.save(configFile);
            }
            catch (IOException ex) {}
        }
        MetricsLite.serverUUID = config.getString("serverUuid");
        MetricsLite.logFailedRequests = config.getBoolean("logFailedRequests", false);
        this.enabled = config.getBoolean("enabled", true);
        if (this.enabled) {
            boolean found = false;
            for (final Class<?> service : Bukkit.getServicesManager().getKnownServices()) {
                try {
                    service.getField("B_STATS_VERSION");
                    found = true;
                }
                catch (NoSuchFieldException ex2) {
                    continue;
                }
                break;
            }
            Bukkit.getServicesManager().register((Class)MetricsLite.class, (Object)this, plugin, ServicePriority.Normal);
            if (!found) {
                this.startSubmitting();
            }
        }
    }
    
    public boolean isEnabled() {
        return this.enabled;
    }
    
    private void startSubmitting() {
        final Timer timer = new Timer(true);
        timer.scheduleAtFixedRate(new TimerTask() {
            @Override
            public void run() {
                if (!MetricsLite.this.plugin.isEnabled()) {
                    timer.cancel();
                    return;
                }
                Bukkit.getScheduler().runTask(MetricsLite.this.plugin, () -> MetricsLite.this.submitData());
            }
        }, 300000L, 1800000L);
    }
    
    public JSONObject getPluginData() {
        final JSONObject data = new JSONObject();
        final String pluginName = this.plugin.getDescription().getName();
        final String pluginVersion = this.plugin.getDescription().getVersion();
        data.put((Object)"pluginName", (Object)pluginName);
        data.put((Object)"pluginVersion", (Object)pluginVersion);
        final JSONArray customCharts = new JSONArray();
        data.put((Object)"customCharts", (Object)customCharts);
        return data;
    }
    
    private JSONObject getServerData() {
        final int playerAmount = Bukkit.getOnlinePlayers().size();
        final int onlineMode = Bukkit.getOnlineMode() ? 1 : 0;
        final String bukkitVersion = Bukkit.getVersion();
        final String javaVersion = System.getProperty("java.version");
        final String osName = System.getProperty("os.name");
        final String osArch = System.getProperty("os.arch");
        final String osVersion = System.getProperty("os.version");
        final int coreCount = Runtime.getRuntime().availableProcessors();
        final JSONObject data = new JSONObject();
        data.put((Object)"serverUUID", (Object)MetricsLite.serverUUID);
        data.put((Object)"playerAmount", (Object)playerAmount);
        data.put((Object)"onlineMode", (Object)onlineMode);
        data.put((Object)"bukkitVersion", (Object)bukkitVersion);
        data.put((Object)"javaVersion", (Object)javaVersion);
        data.put((Object)"osName", (Object)osName);
        data.put((Object)"osArch", (Object)osArch);
        data.put((Object)"osVersion", (Object)osVersion);
        data.put((Object)"coreCount", (Object)coreCount);
        return data;
    }
    
    private void submitData() {
        final JSONObject data = this.getServerData();
        final JSONArray pluginData = new JSONArray();
        for (final Class<?> service : Bukkit.getServicesManager().getKnownServices()) {
            try {
                service.getField("B_STATS_VERSION");
                for (final RegisteredServiceProvider<?> provider : Bukkit.getServicesManager().getRegistrations((Class)service)) {
                    try {
                        pluginData.add(provider.getService().getMethod("getPluginData", (Class[])new Class[0]).invoke(provider.getProvider(), new Object[0]));
                    }
                    catch (NullPointerException ex) {}
                    catch (NoSuchMethodException ex2) {}
                    catch (IllegalAccessException ex3) {}
                    catch (InvocationTargetException ex4) {}
                }
            }
            catch (NoSuchFieldException ex5) {}
        }
        data.put((Object)"plugins", (Object)pluginData);
        final JSONObject data2;
        new Thread(() -> {
            try {
                sendData(this.plugin, data2);
            }
            catch (Exception e) {
                if (MetricsLite.logFailedRequests) {
                    this.plugin.getLogger().log(Level.WARNING, "Could not submit plugin stats of " + this.plugin.getName(), e);
                }
            }
        }).start();
    }
    
    private static void sendData(final Plugin plugin, final JSONObject data) throws Exception {
        if (data == null) {
            throw new IllegalArgumentException("Data cannot be null!");
        }
        if (Bukkit.isPrimaryThread()) {
            throw new IllegalAccessException("This method must not be called from the main thread!");
        }
        if (MetricsLite.logSentData) {
            plugin.getLogger().info("Sending data to bStats: " + data.toString());
        }
        final HttpsURLConnection connection = (HttpsURLConnection)new URL("https://bStats.org/submitData/bukkit").openConnection();
        final byte[] compressedData = compress(data.toString());
        connection.setRequestMethod("POST");
        connection.addRequestProperty("Accept", "application/json");
        connection.addRequestProperty("Connection", "close");
        connection.addRequestProperty("Content-Encoding", "gzip");
        connection.addRequestProperty("Content-Length", String.valueOf(compressedData.length));
        connection.setRequestProperty("Content-Type", "application/json");
        connection.setRequestProperty("User-Agent", "MC-Server/1");
        connection.setDoOutput(true);
        final DataOutputStream outputStream = new DataOutputStream(connection.getOutputStream());
        outputStream.write(compressedData);
        outputStream.flush();
        outputStream.close();
        final InputStream inputStream = connection.getInputStream();
        final BufferedReader bufferedReader = new BufferedReader(new InputStreamReader(inputStream));
        final StringBuilder builder = new StringBuilder();
        String line;
        while ((line = bufferedReader.readLine()) != null) {
            builder.append(line);
        }
        bufferedReader.close();
        if (MetricsLite.logResponseStatusText) {
            plugin.getLogger().info("Sent data to bStats and received response: " + builder.toString());
        }
    }
    
    private static byte[] compress(final String str) throws IOException {
        if (str == null) {
            return null;
        }
        final ByteArrayOutputStream outputStream = new ByteArrayOutputStream();
        final GZIPOutputStream gzip = new GZIPOutputStream(outputStream);
        gzip.write(str.getBytes(StandardCharsets.UTF_8));
        gzip.close();
        return outputStream.toByteArray();
    }
}
