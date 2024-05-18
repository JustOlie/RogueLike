using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Actor), typeof(AStar))]
public class Enemy : MonoBehaviour
{
    public Actor Target { get; set; }
    public bool IsFighting { get; private set; } = false;
    private AStar algorithm;
    // Start is called before the first frame update
    void Start()
    {
        algorithm = GetComponent<AStar>();
        GameManager.Get.AddEnemy(GetComponent<Actor>());
    }

    // Update is called once per frame
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
            // Check if GameManager.Get is not null and GameManager.Get.Player is not null
            if (GameManager.Get != null && GameManager.Get.Player != null)
            {
                Target = GameManager.Get.Player;
            }
            else
            {
                // Handle the case where the player is not found or GameManager.Get is null
                return; // exit the method to avoid further execution
            }
        }

        Vector3Int gridPosition = MapManager.Get.FloorMap.WorldToCell(Target.transform.position);

        if (IsFighting || GetComponent<Actor>().FieldOfView.Contains(gridPosition))
        {
            IsFighting = true;
            MoveAlongPath(gridPosition);
        }
    }
}