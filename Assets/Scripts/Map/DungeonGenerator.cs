using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;

public class DungeonGenerator : MonoBehaviour
{
    private int width, height;
    private int maxRoomSize, minRoomSize;
    private int maxRooms;
    private int maxEnemies;
    List<Vector4> rooms = new List<Vector4>(); // We gebruiken een List<Vector4> om de kamercoördinaten en -afmetingen op te slaan

    public void SetSize(int width, int height)
    {
        this.width = width;
        this.height = height;
    }

    public void SetRoomSize(int min, int max)
    {
        minRoomSize = min;
        maxRoomSize = max;
    }

    public void SetMaxRooms(int max)
    {
        maxRooms = max;
    }

    public void SetMaxEnemies(int max)
    {
        maxEnemies = max;
    }

    public void Generate()
    {
        rooms.Clear();

        for (int roomNum = 0; roomNum < maxRooms; roomNum++)
        {
            int roomWidth = UnityEngine.Random.Range(minRoomSize, maxRoomSize);
            int roomHeight = UnityEngine.Random.Range(minRoomSize, maxRoomSize);

            int roomX = UnityEngine.Random.Range(0, width - roomWidth - 1);
            int roomY = UnityEngine.Random.Range(0, height - roomHeight - 1);

            // Voeg de kamercoördinaten en -afmetingen toe aan de lijst
            rooms.Add(new Vector4(roomX, roomY, roomWidth, roomHeight));

            // Rest van je code...
        }

        // Nadat alle kamers zijn gegenereerd, plaatsen we vijanden in elke kamer
        foreach (Vector4 room in rooms)
        {
            PlaceEnemies(room, maxEnemies);
        }
    }

    private void PlaceEnemies(Vector4 room, int maxEnemies)
    {
        int num = UnityEngine.Random.Range(0, maxEnemies + 1);

        for (int counter = 0; counter < num; counter++)
        {
            int x = UnityEngine.Random.Range((int)room.x + 1, (int)room.x + (int)room.z - 1);
            int y = UnityEngine.Random.Range((int)room.y + 1, (int)room.y + (int)room.w - 1);

            if (UnityEngine.Random.value < 0.5f)
            {
                GameManager.Get.CreateActor("oneeye", new Vector3(x, y, 0));
            }
            else
            {
                GameManager.Get.CreateActor("vampire", new Vector3(x, y, 0));
            }
        }
    }
}
