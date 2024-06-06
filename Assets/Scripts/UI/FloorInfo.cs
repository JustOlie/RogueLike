using UnityEngine;
using UnityEngine.UIElements;

public class FloorInfo : MonoBehaviour
{
    private VisualElement root;
    private Label floorLabel;
    private Label enemiesLabel;

    void Start()
    {
        var uiDocument = GetComponent<UIDocument>();
        root = uiDocument.rootVisualElement;

        floorLabel = root.Q<Label>("Floor");
        enemiesLabel = root.Q<Label>("Enemies");
    }

    public void UpdateFloorText(int floorNumber)
    {
        floorLabel.text = $"Floor: {floorNumber}";
    }

    public void UpdateEnemiesLeftText(int enemiesLeft)
    {
        enemiesLabel.text = $"Enemies: {enemiesLeft}";
    }
}
