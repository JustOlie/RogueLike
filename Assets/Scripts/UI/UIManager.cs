using UnityEngine;

public class UIManager : MonoBehaviour
{
    private static UIManager instance;

    [Header("Documents")]
    public GameObject healthBarObject;
    public GameObject messagesObject;

    private HealthBar healthBar;
    private Messages messages;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        if (healthBarObject != null)
        {
            healthBar = healthBarObject.GetComponent<HealthBar>();
        }
        if (messagesObject != null)
        {
            messages = messagesObject.GetComponent<Messages>();
        }
    }

    public static UIManager Instance
    {
        get
        {
            if (instance == null)
            {
                Debug.LogError("UIManager is not initialized!");
            }
            return instance;
        }
    }

    public void UpdateHealth(int current, int max)
    {
        if (healthBar != null)
        {
            healthBar.SetValues(current, max);
        }
    }

    public void AddMessage(string message, Color color)
    {
        if (messages != null)
        {
            messages.AddMessage(message, color);
        }
    }
}
