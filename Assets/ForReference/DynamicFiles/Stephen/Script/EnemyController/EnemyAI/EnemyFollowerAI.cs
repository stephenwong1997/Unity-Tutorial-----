using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EnemyFollowerAI : EnemyAI
{
    public float noticeTime = 0.5f;

    public GameObject nextFollowTarget;
    public GameObject encounterTarget;
    [Header("-1 means not required")]
    public PathPoint[] path;
    protected int pathIndex = 0;


    private float alertness;
    public bool alert;


    public GameObject alertSign;
    private Image circle;
    private Image ExclamationMark;

    protected float timerForReachPath;
    protected float rotationAcceleration;
    private Vector3 rotationStep;
    private bool finishRotation;
    private bool startWaiting;
    private bool finishWaiting;


    // debug
    private float timer;

    // Start is called before the first frame update
    protected override void Awake()
    {
        base.Awake();
        timerForReachPath = 0;
        alert = false;
        rotationStep = Vector3.zero;
        finishRotation = false;
        finishWaiting = false;
        if (alertSign) {
            circle = alertSign.transform.Find("Circle").GetComponent<Image>();
            ExclamationMark = alertSign.transform.Find("ExclamationMark").GetComponent<Image>();
        }
  
        if (!nextFollowTarget && path.Length >= 1)
        {
            nextFollowTarget = path[pathIndex].pathPoint;
        }

        //CheckPathWayCorrectness();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        if (alertness > 0) { alertness -= 1 / noticeTime * Time.deltaTime * 0.15f; }
        if (alert) { Alert(); }
        else if(path.Length!=0){

            FollowPath();
        }
        UpdateAlertSign();
        if(navMeshAgent.isActiveAndEnabled)navMeshAgent.SetDestination(nextFollowTarget.transform.position); 
        MovementAnimation();

       


    }

    private void UpdateAlertSign()
    {
        alertSign.transform.LookAt(Camera.main.transform);
        circle.fillAmount = Mathf.Clamp(alertness, 0f, 1f);
    }


    protected virtual void MovementAnimation()
    {
        //if (!alert)//walk
        //{
        //    anim.SetFloat("walkSpeed", ExtensionMethods.Remap(navMeshAgent.velocity.magnitude, 0, navMeshAgent.speed, 0, 1));
        //}
        //else {

        //    anim.SetFloat("walkSpeed", ExtensionMethods.Remap(navMeshAgent.velocity.magnitude, 0, navMeshAgent.speed, 0, -1));
        //}
        anim.SetFloat("walkSpeed", ExtensionMethods.Remap(navMeshAgent.velocity.magnitude, 0, navMeshAgent.speed, 0, 1));
        if (alert)
        { anim.SetFloat("walkSpeed", ExtensionMethods.Remap(navMeshAgent.velocity.magnitude, 0, navMeshAgent.speed, 0, -1)); }
    }

    protected virtual void Alert()
    {
        nextFollowTarget = encounterTarget;
    }

    protected virtual void FollowPath()
    {
        //print("current follow target : " + path[pathIndex].pathPoint.name);
     
        if (navMeshAgent.stoppingDistance >= Vector3.Distance(transform.position, nextFollowTarget.transform.position))
        {

            RotateToPathPointRotation();
            if (finishRotation)
            {
                finishRotation = false;
                startWaiting = true;
                rotationStep = Vector3.zero;
            }
            WaitUntilPointWaitingTime();
            if (finishWaiting)
            {
                finishWaiting = false;
                CallEventFunction();
                ChangeToNextTarget();
            }

        }
    }

    private void WaitUntilPointWaitingTime()
    {
        if (startWaiting)
        {
            timer += Time.deltaTime;
            if(timer >= path[pathIndex].waitingTime)
            {
                startWaiting = false;
                timer = 0;
                finishWaiting = true;

            }
        }

    }


    private void RotateToPathPointRotation()
    {
        if (path[pathIndex].rotation < 0) { finishRotation = true; }
        if (path[pathIndex].rotation >= 0 && rotationStep == Vector3.zero)
        {
            rotationStep = (new Vector3(0, path[pathIndex].rotation, 0) - this.transform.eulerAngles) / Mathf.Clamp(path[pathIndex].rotationSecond,0.3f,Mathf.Infinity); // calculate angle difference and divide it into parts based on rotation speed
        }
        float angle = 0;
        if (finishRotation == false)// this.transform.eulerAngles != new Vector3(0, path[pathIndex].rotation, 0) && path[pathIndex].waitingTime >= 0
        {
            if(rotationStep.y> 0 )
            anim.SetBool("turningRight", true);
            if(rotationStep.y<0)
            anim.SetBool("turningLeft", true);
            this.transform.Rotate(rotationStep * Time.deltaTime); // rotate one rotationStep per second;
            angle = Mathf.Abs(Mathf.DeltaAngle(this.transform.localEulerAngles.y, path[pathIndex].rotation)); // calculate the difference of two angle
        }
        if (path[pathIndex].rotation >= 0 && angle < 100f*Time.deltaTime) // finish rotation
        {
            finishRotation = true;

            anim.SetBool("turningLeft", false);
            anim.SetBool("turningRight", false);
        }

    }

    private void CallEventFunction()
    {
        if (path[pathIndex].eventToCall )
        {
            path[pathIndex].eventToCall.GetComponent<MonoBehaviour>().Invoke(path[pathIndex].functionName,0);
            print(path[pathIndex].functionName);
            //path[pathIndex].eventToCall.GetComponent<PathController>().ClearPathWay();
            //CheckPathWayCorrectness();
        }
    }

    private void ChangeToNextTarget()
    {
        if (path.Length > 0)
        {
            if (path[pathIndex].eventToCall) //Invoke有delay, 確保invoking function is not pending 才 next target
            {
                if (!path[pathIndex].eventToCall.GetComponent<MonoBehaviour>().IsInvoking(path[pathIndex].functionName))
                {
                    pathIndex = (pathIndex + 1) % path.Length;
                    nextFollowTarget = path[pathIndex].pathPoint;
                }
            }
            else {
                pathIndex = (pathIndex + 1) % path.Length;
                nextFollowTarget = path[pathIndex].pathPoint;
            }
               
        }
    }

    private void CheckPathWayCorrectness()
    {
        if (path.Length > 0 && path[pathIndex].rotation >= 0 && path[pathIndex].rotationSecond < 0)
        {
            Debug.LogError("If you have rotation, you need to give it a rotation speed, error object : " + path[pathIndex].pathPoint.name);
        }
    }

    public void newTarget(GameObject target)
    {
        nextFollowTarget = target;
    }

    public void EncounterObject(GameObject target)
    {
        alertness += 1/noticeTime * Time.deltaTime;
     
        if (alertness >= 1)
        {
            alert = true;
            encounterTarget = target;
        }
  
    }

    public void SetPathIndex(int index)
    {
        pathIndex = index;

    }

    public bool getAlert()
    {
        return alert;
    }

}
