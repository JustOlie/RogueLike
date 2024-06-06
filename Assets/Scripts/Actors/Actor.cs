using System.Collections.Generic;
using UnityEngine;

public class Actor : MonoBehaviour
{
    private AdamMilVisibility algorithm;

    [Header("FieldOfView")]
    public List<Vector3Int> FieldOfView = new List<Vector3Int>();
    public int FieldOfViewRange = 8;

    [Header("Powers")]
    [SerializeField] private int maxHitPoints;
    [SerializeField] private int hitPoints;
    [SerializeField] private int defense;
    [SerializeField] private int power;

    [SerializeField] private int level = 1;
    [SerializeField] private int xp = 0;
    [SerializeField] private int xpToNextLevel = 100;

    public int MaxHitPoints { get => maxHitPoints; }
    public int HitPoints { get => hitPoints; }
    public int Defense { get => defense; }
    public int Power { get => power; }
    public int Level { get => level; }
    public int XP { get => xp; }
    public int XPToNextLevel { get => xpToNextLevel; }

    private void Start()
    {
        algorithm = new AdamMilVisibility();
        UpdateFieldOfView();

        if (GetComponent<Player>())
        {
            UIManager.Get.UpdateHealth(HitPoints, MaxHitPoints);
            UIManager.Get.UpdateLevel(Level);
            UIManager.Get.UpdateXP(XP);
        }
    }

    public void Move(Vector3 direction)
    {
        if (MapManager.Get.IsWalkable(transform.position + direction))
        {
            transform.position += direction;
        }
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

    public void DoDamage(int hp, Actor attacker)
    {
        hitPoints -= hp;

        if (hitPoints < 0) hitPoints = 0;

        if (GetComponent<Player>())
        {
            UIManager.Get.UpdateHealth(hitPoints, MaxHitPoints);
        }

        if (hitPoints == 0)
        {
            Die(attacker);
        }
    }

    public void Heal(int hp)
    {
        int actualHealing = Mathf.Min(maxHitPoints - hitPoints, hp);
        hitPoints += actualHealing;

        if (GetComponent<Player>())
        {
            UIManager.Get.UpdateHealth(hitPoints, MaxHitPoints);
            UIManager.Get.AddMessage($"You were healed for {actualHealing} hit points!", Color.green);
        }
    }

    public void AddXP(int amount)
    {
        xp += amount;

        while (xp >= xpToNextLevel)
        {
            xp -= xpToNextLevel;
            LevelUp();
        }

        if (GetComponent<Player>())
        {
            UIManager.Get.UpdateXP(XP);
        }
    }

    private void LevelUp()
    {
        level++;
        xpToNextLevel = Mathf.RoundToInt(xpToNextLevel * 1.5f);

        // Verhoog stats bij level up
        maxHitPoints += 10;
        hitPoints = maxHitPoints;
        power += 2;
        defense += 1;

        if (GetComponent<Player>())
        {
            UIManager.Get.UpdateLevel(Level);
            UIManager.Get.UpdateHealth(HitPoints, MaxHitPoints);
            UIManager.Get.AddMessage("You leveled up!", Color.yellow);
        }
    }

    private void Die(Actor attacker)
    {
        if (GetComponent<Player>())
        {
            UIManager.Get.AddMessage("You died!", Color.red);
        }
        else
        {
            UIManager.Get.AddMessage($"{name} is dead!", Color.green);
            if (attacker.GetComponent<Player>())
            {
                attacker.AddXP(xp);
            }
        }
        GameManager.Get.CreateGameObject("Dead", transform.position).name = $"Remains of {name}";
        GameManager.Get.RemoveEnemy(this);
        Destroy(gameObject);
    }
}
