using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMonster1_AI : EnemyFollowerAI
{
    public string resetLevelObjectName;
    private CameraLock cameraLock;
    // Start is called before the first frame update
    void Start()
    {
        cameraLock = FindObjectOfType<CameraLock>();

    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

    }

    protected override void Alert()
    {
        nextFollowTarget = encounterTarget;
        cameraLock.LockCameraLock();
        if (navMeshAgent.stoppingDistance >= Vector3.Distance(transform.position, nextFollowTarget.transform.position))
        {
            resetScene();

        }
    }
    private void resetScene()
    {
        if(resetLevelObjectName!="")
        GameObject.Find(resetLevelObjectName).GetComponent<ResetLevel>().resetLevel() ;
    }
}

