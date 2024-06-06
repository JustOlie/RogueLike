using UnityEngine;

public class UIManager : MonoBehaviour
{
    private static UIManager instance;
   

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        floorInfo = FindObjectOfType<FloorInfo>();
    }

    public static UIManager Get { get => instance; }

    [Header("Documents")]
    public GameObject HealthBar;
    public GameObject Messages;
    [SerializeField] private GameObject InventoryUI;
    public FloorInfo floorInfo;

    public InventoryUI Inventory { get => InventoryUI.GetComponent<InventoryUI>(); }

    public void UpdateHealth(int current, int max)
    {
        HealthBar.GetComponent<HealthBar>().SetValues(current, max);
    }

    public void UpdateLevel(int level)
    {
        HealthBar.GetComponent<HealthBar>().SetLevel(level);
    }

    public void UpdateXP(int xp)
    {
        HealthBar.GetComponent<HealthBar>().SetXP(xp);
    }

    public void AddMessage(string message, Color color)
    {
        Messages.GetComponent<Messages>().AddMessage(message, color);
    }

    public void SetFloor(int floorNumber)
    {
        floorInfo.UpdateFloorText(floorNumber);
        // Voeg hier eventuele andere UI-updates toe die je nodig hebt voor een nieuwe verdieping
    }

    public void SetEnemiesLeft(int enemiesLeft)
    {
        floorInfo.UpdateEnemiesLeftText(enemiesLeft);
        // Voeg hier eventuele andere UI-updates toe voor het aantal vijanden dat overblijft
    }
}
