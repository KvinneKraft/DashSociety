// 
// Decompiled by Procyon v0.5.36
// 

package codes.biscuit.genbucket.utils;

import java.util.Iterator;
import java.util.Collection;
import org.bukkit.inventory.ItemStack;
import java.util.HashMap;
import java.util.Map;

public class BucketManager
{
    private Map<String, Bucket> buckets;
    
    public BucketManager() {
        this.buckets = new HashMap<String, Bucket>();
    }
    
    public Map<String, Bucket> getBuckets() {
        return this.buckets;
    }
    
    Bucket createBucket(final String id) {
        final Bucket newBucket = new Bucket(id);
        this.buckets.put(id, newBucket);
        return newBucket;
    }
    
    public Bucket getBucket(final String id) {
        return this.buckets.get(id);
    }
    
    public Bucket matchBucket(final ItemStack item) {
        for (final Map.Entry<String, Bucket> entry : this.buckets.entrySet()) {
            final Bucket bucket = this.getBucket(entry.getKey());
            final ItemStack otherItem = bucket.getItem();
            if (otherItem.getType() == item.getType() && ((item.hasItemMeta() && otherItem.getItemMeta().hasDisplayName()) || (!item.hasItemMeta() && !otherItem.getItemMeta().hasDisplayName())) && otherItem.getItemMeta().getDisplayName().equals(item.getItemMeta().getDisplayName()) && ((!otherItem.getItemMeta().hasLore() && !item.getItemMeta().hasLore()) || otherItem.getItemMeta().getLore().containsAll(item.getItemMeta().getLore()))) {
                return bucket;
            }
        }
        return null;
    }
    
    public Bucket fromSlot(final int slot) {
        for (final Map.Entry<String, Bucket> entry : this.buckets.entrySet()) {
            final Bucket bucket = this.getBucket(entry.getKey());
            if (bucket.getSlot() == slot) {
                return bucket;
            }
        }
        return null;
    }
    
    public Bucket fromShopName(final String name) {
        for (final Map.Entry<String, Bucket> entry : this.buckets.entrySet()) {
            final Bucket bucket = this.getBucket(entry.getKey());
            if (bucket.getGuiItem().getItemMeta().getDisplayName().equals(name)) {
                return bucket;
            }
        }
        return null;
    }
}
