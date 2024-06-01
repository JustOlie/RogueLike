using UnityEngine;
using System.Collections.Generic;

public class Actor : MonoBehaviour
{
    [Header("Powers")]
    [SerializeField] private int maxHitPoints;
    [SerializeField] private int hitPoints;
    [SerializeField] private int defense;
    [SerializeField] private int power;

    public int MaxHitPoints { get { return maxHitPoints; } }
    public int HitPoints { get { return hitPoints; } }
    public int Defense { get { return defense; } }
    public int Power { get { return power; } }

    public List<Vector3Int> FieldOfView { get; private set; }

    private void Awake()
    {
        FieldOfView = new List<Vector3Int>(); // Placeholder voor zichtbare cellen
    }

    private void Start()
    {
        if (GetComponent<Player>() != null)
        {
            UIManager.Instance.UpdateHealth(hitPoints, maxHitPoints);
        }
    }

    public void Move(Vector2 direction)
    {
        transform.position += (Vector3)direction;
    }

    public void UpdateFieldOfView()
    {
        var pos = MapManager.Get.FloorMap.WorldToCell(transform.position);

        FieldOfView.Clear();
        algorithm.Compute(pos, FieldOfViewRange, FieldOfView);

        if (GetComponent<Player>())
        {
            MapManager.Get.UpdateFogMap(FieldOfView);
        }
    }

    public void TakeDamage(int damage)
    {
        hitPoints -= damage;
        if (hitPoints < 0)
        {
            hitPoints = 0;
        }

        if (GetComponent<Player>() != null)
        {
            UIManager.Instance.UpdateHealth(hitPoints, maxHitPoints);
        }

        if (hitPoints <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        if (GetComponent<Player>() != null)
        {
            UIManager.Instance.AddMessage("You died!", Color.red);
        }
        else
        {
            string message = $"{gameObject.name} is dead!";
            UIManager.Instance.AddMessage(message, Color.green);
            GameManager.Get.CreateActor("Dead", transform.position);
            if (GetComponent<Enemy>() != null)
            {
                GameManager.Get.RemoveEnemy(this);
            }
        }

        Destroy(gameObject);
    }

    public void DoDamage(int hp)
    {
        hitPoints -= hp;
        if (hitPoints < 0)
        {
            hitPoints = 0;
        }

        if (GetComponent<Player>() != null)
        {
            UIManager.Instance.UpdateHealth(hitPoints, maxHitPoints);
        }

        if (hitPoints == 0)
        {
            Die();
        }
    }
}
