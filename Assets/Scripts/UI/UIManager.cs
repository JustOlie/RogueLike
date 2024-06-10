using UnityEngine;

public class UIManager : MonoBehaviour
{
    private static UIManager instance;
    private FloorInfo floorInfo;
    private SaveGame saveGame;  // Referentie naar de SaveGame-component

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
        saveGame = FindObjectOfType<SaveGame>();  // Initialiseer de SaveGame-component
    }

    public static UIManager Get { get => instance; }

    [Header("Documents")]
    public GameObject HealthBar;
    public GameObject Messages;
    [SerializeField] private GameObject InventoryUI;
    public GameObject SaveGame;

    public InventoryUI Inventory { get => InventoryUI.GetComponent<InventoryUI>(); }

    public void UpdateHealth(int current, int max)
    {
        HealthBar.GetComponent<HealthBar>().SetValues(current, max);
    }

    public void UpdateLevel(int level)
    {
        HealthBar.GetComponent<HealthBar>().SetLevel(level);
        SetLevel(level);
    }

    public void UpdateXP(int xp)
    {
        HealthBar.GetComponent<HealthBar>().SetXP(xp);
        SetXP(xp);
    }

    public void AddMessage(string message, Color color)
    {
        Messages.GetComponent<Messages>().AddMessage(message, color);
    }

    public void SetFloor(int floorNumber)
    {
        floorInfo.UpdateFloorText(floorNumber);
    }

    public void SetEnemiesLeft(int enemiesLeft)
    {
        floorInfo.UpdateEnemiesLeftText(enemiesLeft);
    }

    public void SetLevel(int level)
    {
        saveGame.Save(saveGame.MaxHitPoints, saveGame.HitPoints, saveGame.Defense, saveGame.Power, level, saveGame.XP, saveGame.XpToNextLevel, saveGame.CurrentFloor);
    }

    public void SetXP(int xp)
    {
        saveGame.Save(saveGame.MaxHitPoints, saveGame.HitPoints, saveGame.Defense, saveGame.Power, saveGame.Level, xp, saveGame.XpToNextLevel, saveGame.CurrentFloor);
    }
}
