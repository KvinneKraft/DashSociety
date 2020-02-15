// 
// Decompiled by Procyon v0.5.36
// 

package codes.biscuit.genbucket.utils;

import java.util.List;
import java.util.Map;
import org.bukkit.inventory.ItemStack;

public class Bucket
{
    private String id;
    private ItemStack item;
    private ItemStack blockItem;
    private Direction direction;
    private boolean byChunk;
    private boolean isPatch;
    private boolean isDelete;
    private ItemStack guiItem;
    private int slot;
    private double buyPrice;
    private boolean infinite;
    private double placePrice;
    private Map<Character, ItemStack> ingredients;
    private List<String> recipeShape;
    private int recipeAmount;
    
    Bucket(final String id) {
        this.id = id;
    }
    
    String getId() {
        return this.id;
    }
    
    public ItemStack getItem() {
        return this.item.clone();
    }
    
    void setItem(final ItemStack item) {
        this.item = item;
    }
    
    public ItemStack getGuiItem() {
        return this.guiItem.clone();
    }
    
    void setGuiItem(final ItemStack guiItem) {
        this.guiItem = guiItem;
    }
    
    public Direction getDirection() {
        return this.direction;
    }
    
    void setDirection(final Direction direction) {
        this.direction = direction;
    }
    
    public boolean isByChunk() {
        return this.byChunk;
    }
    
    void setByChunk(final boolean byChunk) {
        this.byChunk = byChunk;
    }
    
    public boolean isPatch() {
        return this.isPatch;
    }
    
    void setPatch(final boolean patch) {
        this.isPatch = patch;
    }
    
    int getSlot() {
        return this.slot;
    }
    
    void setSlot(final int slot) {
        this.slot = slot;
    }
    
    public double getBuyPrice() {
        return this.buyPrice;
    }
    
    void setBuyPrice(final double buyPrice) {
        this.buyPrice = buyPrice;
    }
    
    public boolean isInfinite() {
        return this.infinite;
    }
    
    void setInfinite(final boolean infinite) {
        this.infinite = infinite;
    }
    
    public double getPlacePrice() {
        return this.placePrice;
    }
    
    void setPlacePrice(final double placePrice) {
        this.placePrice = placePrice;
    }
    
    public ItemStack getBlockItem() {
        return this.blockItem;
    }
    
    void setBlockItem(final ItemStack blockItem) {
        this.blockItem = blockItem;
    }
    
    List<String> getRecipeShape() {
        return this.recipeShape;
    }
    
    void setRecipeShape(final List<String> recipeShape) {
        this.recipeShape = recipeShape;
    }
    
    Map<Character, ItemStack> getIngredients() {
        return this.ingredients;
    }
    
    void setIngredients(final Map<Character, ItemStack> ingredients) {
        this.ingredients = ingredients;
    }
    
    int getRecipeAmount() {
        return this.recipeAmount;
    }
    
    void setRecipeAmount(final int recipeAmount) {
        this.recipeAmount = recipeAmount;
    }
    
    void setDelete(final boolean delete) {
        this.isDelete = delete;
    }
    
    public boolean isDelete() {
        return this.isDelete;
    }
    
    public enum Direction
    {
        ANY, 
        HORIZONTAL, 
        UPWARDS, 
        DOWNWARDS;
    }
}
