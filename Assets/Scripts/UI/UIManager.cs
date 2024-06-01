using System.Diagnostics;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private static UIManager instance;

    [Header("Documents")]
    public GameObject healthBarObject;
    public GameObject messagesObject;
    public GameObject inventoryObject; // Voeg een GameObject voor de inventaris toe

    private HealthBar healthBar;
    private Messages messages;
    private InventoryUI inventoryUI; // Voeg een InventoryUI toe

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        if (healthBarObject != null)
        {
            healthBar = healthBarObject.GetComponent<HealthBar>();
        }
        if (messagesObject != null)
        {
            messages = messagesObject.GetComponent<Messages>();
        }
        if (inventoryObject != null)
        {
            inventoryUI = inventoryObject.GetComponent<InventoryUI>(); // Verkrijg de InventoryUI-component
        }
    }

    public static UIManager Instance
    {
        get
        {
            if (instance == null)
            {
                Debug.LogError("UIManager is not initialized!");
            }
            return instance;
        }
    }

    public void UpdateHealth(int current, int max)
    {
        if (healthBar != null)
        {
            healthBar.SetValues(current, max);
        }
    }

    public void AddMessage(string message, Color color)
    {
        if (messages != null)
        {
            messages.AddMessage(message, color);
        }
    }

    // Voeg een methode toe om toegang te krijgen tot het InventoryUI-component
    public InventoryUI GetInventoryUI()
    {
        return inventoryUI;
    }
}
