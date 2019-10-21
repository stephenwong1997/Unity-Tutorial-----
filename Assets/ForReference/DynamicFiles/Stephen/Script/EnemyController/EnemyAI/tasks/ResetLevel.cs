using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetLevel : MonoBehaviour
{
    public GameObject levelPrefab;
    public Transform PlayerSpawnPosition;
    // Start is called before the first frame update

    public void resetLevel()
    {
        Destroy(GameObject.Find(levelPrefab.name));
        FindObjectOfType<CameraLock>().UnlockCameraLock();
        GameObject.FindGameObjectWithTag("Player").transform.position = PlayerSpawnPosition.position;
       GameObject newLevelPrefab =  Instantiate(levelPrefab);
        newLevelPrefab.name = levelPrefab.name;
    }
}
