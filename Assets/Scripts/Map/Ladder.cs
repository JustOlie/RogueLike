using UnityEngine;

public class Ladder : MonoBehaviour
{
    // Bool om aan te geven of de ladder omhoog gaat
    public bool Up;

    // Start methode om de ladder toe te voegen aan de GameManager
    private void Start()
    {
        GameManager.Get.AddLadder(this);
    }
}
