using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Harvester : MonoBehaviour
{
    [SerializeField] public Harvest _harvest;
    [SerializeField] public Seed _seed;

    // Harvest Analytics
    private Dictionary<string, int> _harvests = new Dictionary<string, int>();

    // Harvest to sell
    // Assignment 2 - Data structure to hold collected harvests
    private List<CollectedHarvest> collectedHarvests = new List<CollectedHarvest>();

    public static Harvester _instance;
       
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }

        _instance = this;
    }

    // Assignment 2
    public List<CollectedHarvest> GetCollectedHarvest()
    {
        return collectedHarvests;
    }

    // Assignment 2
    public void RemoveHarvest(CollectedHarvest harvest)
    {
        collectedHarvests.Remove(harvest);
    }

    // Assignment 2 - CollectHarvest method to collect the harvest when picked up
    public void CollectHarvest(string name, int amount)
    {
        CollectedHarvest collectedHarvest = new CollectedHarvest(name, DateTime.Now.ToString(), amount);
        collectedHarvests.Add(collectedHarvest);
    }
    

    public void ShowHarvest(string plantName, int harvestAmount, int seedAmount, Vector2 position)
    {
        // initiate a harvest with random amount
        Harvest harvest = Instantiate(_harvest, position + Vector2.up + Vector2.right, Quaternion.identity);
        harvest.SetHarvest(plantName, harvestAmount);
        
        // initiate one seed object
        Seed seed = Instantiate(_seed, position + Vector2.up + Vector2.left, Quaternion.identity);
        seed.SetSeed(plantName, seedAmount);
    }

    //Assignment 3
    public void SortHarvestByAmount()
    {
        // Sort the collected harvest using Quick sort
        QuickSort(0, collectedHarvests.Count - 1);
    }

    private void QuickSort(int low, int high)
    {
        if (low < high)
        {
            int partition = QuickSortPartition(low, high);
            QuickSort(low, partition - 1);
            QuickSort(partition + 1, high);
        }
    }

    private int QuickSortPartition(int low, int high)
    {
        int pivot = collectedHarvests[high]._amount;
        int i = low - 1;
        for (int j = low; j < high; j++)
        {
            if (collectedHarvests[j]._amount <= pivot)
            {
                i++;
                Swap(i, j);
            }
        }
        i++;
        Swap(i, high);
        return i;
    }

    private void Swap(int i, int j)
    {
        CollectedHarvest tmp = collectedHarvests[i];
        collectedHarvests[i] = collectedHarvests[j];
        collectedHarvests[j] = tmp;
    }
}

// For Assignment 2, this holds a collected harvest object
[System.Serializable]
public struct CollectedHarvest
{
    public string _name;
    public string _time;
    public int _amount;
    
    public CollectedHarvest(string name, string time, int amount)
    {
        _name = name;
        _time = time;
        _amount = amount;
    }
}