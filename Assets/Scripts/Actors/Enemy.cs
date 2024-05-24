using UnityEngine;

[RequireComponent(typeof(Actor), typeof(AStar))]
public class Enemy : MonoBehaviour
{
    public Actor Target { get; set; }
    public bool IsFighting { get; private set; } = false;
    private AStar algorithm;

    void Start()
    {
        algorithm = GetComponent<AStar>();
        GameManager.Get.AddEnemy(GetComponent<Actor>());
    }

    void Update()
    {
        RunAI();
    }

    public void MoveAlongPath(Vector3Int targetPosition)
    {
        Vector3Int gridPosition = MapManager.Get.FloorMap.WorldToCell(transform.position);
        Vector2 direction = algorithm.Compute((Vector2Int)gridPosition, (Vector2Int)targetPosition);
        Action.Move(GetComponent<Actor>(), direction);
    }

    public void RunAI()
    {
        if (Target == null)
        {
            if (GameManager.Get != null && GameManager.Get.Player != null)
            {
                Target = GameManager.Get.Player;
            }
            else
            {
                return;
            }
        }

        Vector3Int targetGridPosition = MapManager.Get.FloorMap.WorldToCell(Target.transform.position);
        Actor actor = GetComponent<Actor>();

        actor.UpdateFieldOfView(); // Update het gezichtsveld van de vijand

        float distance = Vector3.Distance(transform.position, Target.transform.position);

        if (IsFighting || actor.FieldOfView.Contains(targetGridPosition))
        {
            if (distance < 1.5f) // Als de afstand kleiner is dan 1.5
            {
                Action.Hit(actor, Target); // Voer de Hit functie uit
                IsFighting = true;
            }
            else
            {
                MoveAlongPath(targetGridPosition); // Voer MoveAlongPath uit zoals voorheen
            }
        }
    }
}
