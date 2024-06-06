using UnityEngine;
using UnityEngine.UIElements;

public class HealthBar : MonoBehaviour
{
    private VisualElement root;
    private VisualElement healthBar;
    private Label healthLabel;
    private Label currentLevel;
    private Label XpBar;

    void Start()
    {
        var uiDocument = GetComponent<UIDocument>();
        root = uiDocument.rootVisualElement;

        healthBar = root.Q<VisualElement>("HealthBar");
        healthLabel = root.Q<Label>("HealthLabel");
        currentLevel = root.Q<Label>("CurrentLevel");
        XpBar = root.Q<Label>("XpBar");
    }

    public void SetValues(int currentHitPoints, int maxHitPoints)
    {
        float percent = (float)currentHitPoints / maxHitPoints * 100;
        healthBar.style.width = new Length(percent, LengthUnit.Percent);
        healthLabel.text = $"{currentHitPoints}/{maxHitPoints} HP";
    }

    public void SetLevel(int level)
    {
        currentLevel.text = $"Level: {level}";
    }

    public void SetXP(int xp)
    {
        XpBar.text = $"XP: {xp}";
    }
}
