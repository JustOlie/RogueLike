using UnityEngine;

public class Consumable : MonoBehaviour
{
    // Enum voor de verschillende soorten consumables
    public enum ItemType
    {
        HealthPotion,
        Fireball,
        ScrollOfConfusion
    }

    // Private variabele om het type consumable op te slaan
    [SerializeField] private ItemType type;

    // Public getter voor het type consumable
    public ItemType Type => type;

    // Start is called before the first frame update
    void Start()
    {
        // Voeg dit item toe aan de lijst met items in de GameManager
        GameManager.Instance.AddItem(this);
    }
}
