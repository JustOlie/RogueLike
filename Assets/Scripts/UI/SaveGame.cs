using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UIElements;

public class SaveGame : MonoBehaviour
{
    private static SaveGame instance;
    private VisualElement root;
    private Label lastLevelLabel;
    private Label lastXpLabel;
    private Label lastFloorLabel;

    private string saveFilePath;

    public int MaxHitPoints { get; private set; }
    public int HitPoints { get; private set; }
    public int Defense { get; private set; }
    public int Power { get; private set; }
    public int Level { get; private set; }
    public int XP { get; private set; }
    public int XpToNextLevel { get; private set; }
    public int CurrentFloor { get; private set; }

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
    }

    private void Start()
    {
        var uiDocument = GetComponent<UIDocument>();
        root = uiDocument.rootVisualElement;

        lastLevelLabel = root.Q<Label>("LastLevel");
        lastXpLabel = root.Q<Label>("LastXp");
        lastFloorLabel = root.Q<Label>("LastFloor");

        saveFilePath = Path.Combine(Application.persistentDataPath, "savegame.json");
        Load();
        UpdateUILabels(); // Verander naar UpdateUILabels() bij het opstarten
    }

    public void Save(int maxHP, int hp, int def, int pow, int lvl, int xp, int xpToNextLvl, int floor)
    {
        MaxHitPoints = maxHP;
        HitPoints = hp;
        Defense = def;
        Power = pow;
        Level = lvl;
        XP = xp;
        XpToNextLevel = xpToNextLvl;
        CurrentFloor = floor;

        var saveData = new SaveData
        {
            MaxHitPoints = MaxHitPoints,
            HitPoints = HitPoints,
            Defense = Defense,
            Power = Power,
            Level = Level,
            XP = XP,
            XpToNextLevel = XpToNextLevel,
            CurrentFloor = CurrentFloor
        };

        string json = JsonUtility.ToJson(saveData);
        File.WriteAllText(saveFilePath, json);
        UpdateUILabels(); // Verander naar UpdateUILabels() bij het opslaan
    }

    public void Load()
    {
        if (File.Exists(saveFilePath))
        {
            string json = File.ReadAllText(saveFilePath);
            var saveData = JsonUtility.FromJson<SaveData>(json);

            MaxHitPoints = saveData.MaxHitPoints;
            HitPoints = saveData.HitPoints;
            Defense = saveData.Defense;
            Power = saveData.Power;
            Level = saveData.Level;
            XP = saveData.XP;
            XpToNextLevel = saveData.XpToNextLevel;
            CurrentFloor = saveData.CurrentFloor;
        }
    }

    public void UpdateUILabels()
    {
        if (lastLevelLabel != null)
        {
            lastLevelLabel.text = $"Level: {Level}";
        }

        if (lastXpLabel != null)
        {
            lastXpLabel.text = $"XP: {XP}";
        }

        if (lastFloorLabel != null)
        {
            lastFloorLabel.text = $"Current Floor: {CurrentFloor}";
        }
    }

    private void OnApplicationQuit()
    {
        Save(MaxHitPoints, HitPoints, Defense, Power, Level, XP, XpToNextLevel, CurrentFloor);
    }

    [System.Serializable]
    private class SaveData
    {
        public int MaxHitPoints;
        public int HitPoints;
        public int Defense;
        public int Power;
        public int Level;
        public int XP;
        public int XpToNextLevel;
        public int CurrentFloor;
    }
}
