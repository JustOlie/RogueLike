using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    // Lijst om de items in de inventaris bij te houden
    private List<Consumable> items = new List<Consumable>();

    // Maximale aantal items in de inventaris
    public int MaxItems;

    // Functie om een item toe te voegen aan de inventaris
    public bool AddItem(Consumable item)
    {
        if (items.Count < MaxItems)
        {
            items.Add(item);
            return true; // Item succesvol toegevoegd
        }
        else
        {
            return false; // Inventaris is vol
        }
    }

    // Functie om een item te verwijderen uit de inventaris
    public void DropItem(Consumable item)
    {
        if (items.Contains(item))
        {
            items.Remove(item);
        }
    }
}