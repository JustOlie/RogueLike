using UnityEngine;
using UnityEngine.UIElements;

public class Messages : MonoBehaviour
{
    private Label[] labels = new Label[5];
    private VisualElement root;

    void Start()
    {
        root = GetComponent<UIDocument>().rootVisualElement;

        for (int i = 0; i < labels.Length; i++)
        {
            labels[i] = root.Q<Label>("label" + (i + 1));
            if (labels[i] == null)
            {
                Debug.LogError("Label not found: label" + (i + 1));
            }
        }

        Clear();

        AddMessage("Welcome to the dungeon, Adventurer!", Color.green);
    }

    public void Clear()
    {
        for (int i = 0; i < labels.Length; i++)
        {
            if (labels[i] != null)
            {
                labels[i].text = "";
            }
        }
    }

    public void MoveUp()
    {
        for (int i = labels.Length - 1; i > 0; i--)
        {
            if (labels[i] != null && labels[i - 1] != null)
            {
                labels[i].text = labels[i - 1].text;
                labels[i].style.color = labels[i - 1].style.color;
            }
        }
        if (labels[0] != null)
        {
            labels[0].text = "";
        }
    }

    public void AddMessage(string content, Color color)
    {
        MoveUp();
        if (labels[0] != null)
        {
            labels[0].text = content;
            labels[0].style.color = new StyleColor(color);
        }
    }
}
