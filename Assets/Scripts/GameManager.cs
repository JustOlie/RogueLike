using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Singleton instance
    private static GameManager instance;

    // Lijst om de consumables bij te houden
    private List<Consumable> consumables = new List<Consumable>();

    // Singleton pattern
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GameManager>();
                if (instance == null)
                {
                    GameObject gameManagerObject = new GameObject("GameManager");
                    instance = gameManagerObject.AddComponent<GameManager>();
                }
            }
            return instance;
        }
    }

    // Awake is called when the script instance is being loaded
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    // Functie om een item toe te voegen aan de lijst
    public void AddItem(Consumable item)
    {
        consumables.Add(item);
    }

    // Functie om een item te verwijderen uit de lijst
    public void RemoveItem(Consumable item)
    {
        consumables.Remove(item);
    }

    // Functie om het item te verkrijgen op een specifieke locatie
    public Consumable GetItemAtLocation(Vector3 location)
    {
        // Itereer door de lijst om het item op de locatie te vinden
        foreach (var item in consumables)
        {
            if (item.transform.position == location)
            {
                return item;
            }
        }
        return null; // Als er geen item op die locatie is gevonden
    }

    // Nieuwe methode om nabije vijanden op te halen
    public List<Actor> GetNearbyEnemies(Vector3 location)
    {
        List<Actor> nearbyEnemies = new List<Actor>();

        foreach (var enemy in Enemies)
        {
            if (Vector3.Distance(enemy.transform.position, location) < 5f)
            {
                nearbyEnemies.Add(enemy);
            }
        }

        return nearbyEnemies;
    }

    // Vervolg van de GameManager-klasse zoals gegeven in het andere script

    public Actor Player;
    public List<Actor> Enemies = new List<Actor>();

    public GameObject CreateGameObject(string name, Vector2 position)
    {
        GameObject actor = Instantiate(Resources.Load<GameObject>($"Prefabs/{name}"), new Vector3(position.x + 0.5f, position.y + 0.5f, 0), Quaternion.identity);
        actor.name = name;
        return actor;
    }

    public void AddEnemy(Actor enemy)
    {
        Enemies.Add(enemy);
    }

    public void RemoveEnemy(Actor enemy)
    {
        if (Enemies.Contains(enemy))
        {
            Enemies.Remove(enemy);
        }
    }

    public void StartEnemyTurn()
    {
        foreach (var enemy in Enemies)
        {
            enemy.GetComponent<Enemy>().RunAI();
        }
    }

    public Actor GetActorAtLocation(Vector3 location)
    {
        if (Player.transform.position == location)
        {
            return Player;
        }
        else
        {
            foreach (Actor enemy in Enemies)
            {
                if (enemy.transform.position == location)
                {
                    return enemy;
                }
            }
        }
        return null;
    }
}
