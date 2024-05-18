using UnityEngine;

[RequireComponent(typeof(Actor))]
public class Enemy : MonoBehaviour
{
    private void Start()
    {
        // Add the enemy to GameManager's list of enemies
        GameManager.Get.AddEnemy(GetComponent<Actor>());
    }
}
