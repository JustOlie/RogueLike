using UnityEngine;

public class TombStone : MonoBehaviour
{
    // Start is called before the first frame update
    private void Start()
    {
        // Voeg deze tombstone toe aan de GameManager
        GameManager.Get.AddTombStone(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
