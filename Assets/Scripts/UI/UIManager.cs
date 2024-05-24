using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(UIDocument))]
public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [Header("Documents")]
    public GameObject healthBar;
    public GameObject messages;

    private HealthBar healthBarScript;
    private Messages messagesScript;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    void Start()
    {
        if (healthBar != null)
        {
            healthBarScript = healthBar.GetComponent<HealthBar>();
        }

        if (messages != null)
        {
            messagesScript = messages.GetComponent<Messages>();
        }
    }

    public void UpdateHealth(int current, int max)
    {
        if (healthBarScript != null)
        {
            healthBarScript.UpdateHealth(current, max);
        }
    }

    public void AddMessage(string message, Color color)
    {
        if (messagesScript != null)
        {
            messagesScript.AddMessage(message, color);
        }
    }
}