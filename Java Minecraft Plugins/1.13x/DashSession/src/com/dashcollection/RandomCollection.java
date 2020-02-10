
// Reference: https://stackoverflow.com/questions/6409652/random-weighted-selection-in-java

// I am not good at maths >.<

package com.dashcollection;

import java.util.NavigableMap;
import java.util.Random;
import java.util.TreeMap;

public class RandomCollection<E> 
{
    private final NavigableMap<Double, E> map = new TreeMap<Double, E>();
    private final Random rand;
    private double total = 0;
    
    public RandomCollection()
    {
        this(new Random());
    };
    
    public RandomCollection(Random rand)
    {
        this.rand = rand;
    };
    
    public RandomCollection<E> add(double weight, E result)
    {
        if(weight <= 0) return this;
        
        total += weight;
        map.put(total, result);
        
        return this;
    };
    
    public E next()
    {
        double value = rand.nextDouble() * total;
        return map.higherEntry(value).getValue();
    };
}
