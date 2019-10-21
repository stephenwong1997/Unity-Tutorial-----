using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathController : MonoBehaviour
{
    public GameObject target;
    public PathPoint[] pathToSet;
    private PathPoint[] empty;
    // Start is called before the first frame update
    void Start()
    {
        empty = new PathPoint[0];
    }

    void SetPathWay()
    {
        if (pathToSet.Length > 0)
        target.GetComponent<EnemyFollowerAI>().newTarget(pathToSet[0].pathPoint);
        target.GetComponent<EnemyFollowerAI>().SetPathIndex(0);
        target.GetComponent<EnemyFollowerAI>().path = pathToSet;

    }
    public void ClearPathWay()
    {
        target.GetComponent<EnemyFollowerAI>().path = empty;
        target.GetComponent<EnemyFollowerAI>().SetPathIndex(0);
    }
}
