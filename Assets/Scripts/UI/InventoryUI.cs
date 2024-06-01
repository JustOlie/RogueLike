using System.Collections.Generic;
using System.Reflection.Emit.Label;
using UnityEngine.UIElements;

public class InventoryUI : MonoBehaviour
{
    public Label[] labels = new Label[8];
    public VisualElement root;
    private int selected = 0;
    private int numItems = 0;

    public int Selected => selected;

    private void Clear()
    {
        foreach (var label in labels)
        {
            label.text = "";
        }
    }

    private void Start()
    {
        for (int i = 0; i < labels.Length; i++)
        {
            labels[i] = root.Q<Label>("Item" + (i + 1));
        }

        Clear();
        root.style.display = DisplayStyle.None;
    }

    private void UpdateSelected()
    {
        for (int i = 0; i < labels.Length; i++)
        {
            if (i == selected)
            {
                labels[i].style.backgroundColor = new Color(0.0f, 1.0f, 0.0f); // Groene achtergrond
            }
            else
            {
                labels[i].style.backgroundColor = Color.clear; // Transparante achtergrond
            }
        }
    }

    public void SelectNextItem()
    {
        selected = Mathf.Min(selected + 1, numItems - 1);
        UpdateSelected();
    }

    public void SelectPreviousItem()
    {
        selected = Mathf.Max(selected - 1, 0);
        UpdateSelected();
    }

    public void Show(List<Consumable> list)
    {
        selected = 0;
        numItems = list.Count;
        Clear();

        for (int i = 0; i < Mathf.Min(numItems, labels.Length); i++)
        {
            labels[i].text = list[i].name;
        }

        UpdateSelected();
        root.style.display = DisplayStyle.Flex;
    }

    public void Hide()
    {
        root.style.display = DisplayStyle.None;
    }
}
