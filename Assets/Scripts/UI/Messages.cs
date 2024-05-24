using UnityEngine;
using UnityEngine.UIElements;

public class Messages : MonoBehaviour
{
    private Label[] labels = new Label[5];
    private VisualElement root;

    void Start()
    {
        // Toekenning van de variabelen
        root = GetComponent<UIDocument>().rootVisualElement;
        for (int i = 0; i < labels.Length; i++)
        {
            labels[i] = root.Q<Label>("label" + (i + 1));
        }

        // Clear functie uitvoeren
        Clear();

        // Voeg een bericht toe
        AddMessage("Welcome to the dungeon, Adventurer!", Color.green);
    }

    public void Clear()
    {
        for (int i = 0; i < labels.Length; i++)
        {
            labels[i].text = "";
        }
    }

    public void MoveUp()
    {
        for (int i = labels.Length - 1; i > 0; i--)
        {
            labels[i].text = labels[i - 1].text;
            labels[i].style.color = labels[i - 1].style.color;
        }
        labels[0].text = "";
    }

    public void AddMessage(string content, Color color)
    {
        // Voer de MoveUp functie uit
        MoveUp();

        // Stel Labels[0] in met de voorziene tekst en kleur
        labels[0].text = content;
        labels[0].style.color = new StyleColor(color);
    }
}
