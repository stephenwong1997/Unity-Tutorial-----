using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyVision_AI : MonoBehaviour
{
    [Range(0.0f, 360.0f)]
    public float viewAngle;
    public GameObject FromTarget;

    private bool wasHittingTarget;
    private GameObject lastPositionGameObject;
    private EnemyFollowerAI enemyFollowerAI;
    private float viewDistance;

    private void OnValidate()
    {
        if (FromTarget == null)
        {
            Debug.LogError("This script need a FromTarget to Work");
        }
        if (FromTarget.GetComponent<EnemyFollowerAI>() == null)
        {
            Debug.LogError("This script can only be used with EnemyFollowAI or its child class");
        }
    }


    void Start()
    {
        lastPositionGameObject = new GameObject();
        //lastPositionGameObject.transform.parent = this.transform;
        lastPositionGameObject.name = "LastPositionPoint";
        lastPositionGameObject.transform.position = FromTarget.transform.position;
        if (!FromTarget.GetComponent<EnemyFollowerAI>().nextFollowTarget)
        {
            FromTarget.GetComponent<EnemyFollowerAI>().nextFollowTarget = lastPositionGameObject;
        }
        wasHittingTarget = false;

        enemyFollowerAI = GetComponentInParent<EnemyFollowerAI>();
        viewDistance = GetComponent<SphereCollider>().radius;

    }

    private void OnTriggerStay(Collider other)
    {
        //Vector3 targetDir = other.transform.position - FromTarget.transform.position;
        //float angle = Vector3.Angle(targetDir, FromTarget.transform.forward);

        float playerHeight = 0;
        if (other.CompareTag("Player"))
        {
            Vector3 targetDir = other.transform.position - FromTarget.transform.position;
            float angle = Vector3.Angle(targetDir, FromTarget.transform.forward);
            playerHeight = other.GetComponent<CharacterController>().height * 0.75f;


            Ray ray = new Ray(this.transform.position, new Vector3(other.transform.position.x, other.transform.position.y + playerHeight, other.transform.position.z) - this.transform.position);
            RaycastHit hit;
            Physics.Raycast(ray, out hit, viewDistance*3f);
            if (hit.collider.CompareTag("Player"))
            {
                if (angle <= viewAngle / 2)
                {
                    Debug.DrawRay(ray.origin, ray.direction * viewDistance, Color.red);
                    wasHittingTarget = true;
                    enemyFollowerAI.EncounterObject(other.gameObject);
                }
                else { Debug.DrawRay(ray.origin, ray.direction * viewDistance, Color.green); }

            }


            if (!hit.collider.CompareTag("Player") && enemyFollowerAI.getAlert())
            {
                wasHittingTarget = false;
                lastPositionGameObject.transform.position = other.transform.position;
                enemyFollowerAI.newTarget(lastPositionGameObject);
            }
        }
    }

    private void OnDestroy()
    {
        Destroy(lastPositionGameObject);
    }

    //private void OnTriggerExit(Collider other)
    //{
    //    if (wasHittingTarget)
    //    {
    //        wasHittingTarget = false;

    //        lastPositionGameObject.transform.position = other.transform.position;
    //        enemyFollowerAI.newTarget(lastPositionGameObject);
    //    }
    //}
}
