using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class Boss1AI : MonoBehaviour
{   
    private enum Status{
        idle,
        Follow,
        Stage1FoundPlayer,
        Stage1Claw,
        Stage1Attack,
        Stun
    }

    public enum BodyPart
    {
        Tail
    }
    private Animator anim;
    private GameObject player;
    private NavMeshAgent agent;
    private Rigidbody rb;
    private BoxCollider boxColl;    //used for tree detection 

    private BoxCollider lhitbox;   //take the boxcollider component in the child Object on the left hand of the boss

    //TODO: check the hitbox/attack detection logic 
    // [SerializeField] ThirdPersonController playerControl;

    //TODO:Find a way to reference the tailParticle system without using drag and drop
    [SerializeField] private ParticleSystem tailPs;
    private BoxCollider tailHitbox;
    private int numberHit = 0;
    private int maxNumberHit = 3;

    //Tail particle system
    [SerializeField] private float minPSTime =0.5f;
    [SerializeField] private float maxPSTime =0.8f;
    [SerializeField] private float TailIncreaseRate=0.001f;
    [SerializeField] private float TimeBeforeEnd= 10.0f;
    ParticleSystem.MainModule tailMain;
    ParticleSystem.ShapeModule tailShape;
    ParticleSystem.EmissionModule tailEmission;
    ParticleSystem.MinMaxCurve emissionRate;
    [SerializeField] private float MaxEmissionRate = 20.0f;
    [SerializeField] private Vector3 tailShapeVector = new Vector3(1.0f, 0.25f,0.25f);


    
    [SerializeField] private float walkSpeed = 10.0f;
    [SerializeField] private float runSpeed = 20.0f;
    [SerializeField] private float Stage2Modifier = 1.5f;

    [SerializeField] private float AttackDelay = 7.0f;
    [SerializeField] private float AttackTime = 5.0f;
    [SerializeField] private int stunTime = 4;
    private float timer = 0.0f;
    Status bossState;
    public bool StartAttack = false;



    // Start is called before the first frame update

    void Awake()
    {   
       //get and check if the boss Object has the required component attached
       if(GetComponent<Animator>()) anim = GetComponent<Animator>(); else Debug.LogError("Boss1 AI needs a aniamtor");
       //if(GameObject.FindWithTag("Player")) target = GameObject.FindWithTag("Player"); else Debug.LogError("The player needs to have tag 'Player' for the boss to target");
       if(GetComponent<NavMeshAgent>()) agent = GetComponent<NavMeshAgent>(); else Debug.Log("The boss needs a navmeshagent component dude ");
       if (GetComponent<Rigidbody>()) rb = GetComponent<Rigidbody>(); else Debug.Log("The boss needs a rigidbody component");
       if (GetComponent<BoxCollider>()) boxColl = GetComponent<BoxCollider>(); else Debug.Log("The boss needs a boxCollider for tree detection ");

       foreach(BoxCollider bs in GetComponentsInChildren<BoxCollider>())
        {
            if(bs.gameObject.name== "BossLHhitbox")
            {
                lhitbox = bs;
                break;
            }
        }

        if (lhitbox == null) Debug.Log("There is on left hand hitbox for the boss");
        tailEmission = tailPs.emission;
        tailMain = tailPs.main;
        tailShape = tailPs.shape;

        emissionRate = new ParticleSystem.MinMaxCurve(10.0f);
        
       //ParticleSystem[] ps = GetComponentsInChildren<ParticleSystem>();
       //foreach (ParticleSystem tpS in ps)
       //{
       //    if(tpS.gameObject.name == "TailElectricity")
       //    {
       //        tailPs = tpS;
       //    }
       //    break;
       //}
       

        
        if (tailPs == null) Debug.Log("Cant find TailElectricity");
        player = GameObject.FindWithTag("Player");
        //if (player.GetComponent<ThirdPersonController>()) playerControl = player.GetComponent<ThirdPersonController>(); else Debug.Log("Cant find the 3rd person controller ");
        //the default status of the boss is follow , assuming the boss found the player as the boss fight begins 
        bossState = Status.Follow;
    }

    // Update is called once per frame
    void Update()
    {
        IncreaseTailSize();
       // IncreaseTailSize();
        //basic state machine for the boss1 AI
        switch (bossState)
        {
            case Status.idle:
                idle();
                break;

            case Status.Follow:
                FollowPlayer();
                break;

            case Status.Stage1FoundPlayer:
                Stage1FoundPlayer();
                break;

            case Status.Stage1Attack:
                Stage1Attack();
                break;

            case Status.Stun:
                BossStunned();
                break;

        }
    }

    private void idle()
    {
        if (agent.enabled == true) agent.enabled = false;

    }
    private void FollowPlayer()
    {
        if (agent.enabled == false) agent.enabled = true;
        
            Vector3 forward = transform.TransformDirection(Vector3.forward);
            Vector3 toOther = player.transform.position - transform.position;

            if(Vector3.Dot(forward, toOther) >= 0)
            {
                anim.SetBool("FacingPlayer", true);
            }

        
       
        timer -= Time.deltaTime;

        //TODO: add raycast so that player is the first object on the path of the attack 
        if(timer <= 0)
        {
            bossState = Status.Stage1FoundPlayer;
            return;
        }

        //hard code distance  = 4.8 , if close enough will use claw attck the player
        //if((transform.position - player.transform.position).sqrMagnitude <= 2.2)
        //{
        //    anim.SetTrigger("Stage1Claw");
        //    bossState = Status.idle;

        //}

        agent.speed = walkSpeed;
        agent.SetDestination(player.transform.position);
    }

    public void Stage1ClawFinished()
    {
        bossState = Status.Follow;
        anim.ResetTrigger("Stage1Claw");
    }

    private void Stage1FoundPlayer()
    {
        //Debug.Log("Enter stage 1 Found player roaring");
        if (StartAttack) return;
        if (!StartAttack)
        {
            agent.speed = 0;
            agent.enabled = false;

            gameObject.transform.LookAt(player.transform);
            StartAttack = true;
            anim.SetTrigger("startAttack1");    //change status to runnning towards player when the roaring animation exits
        } 

    }

    private void Stage1Attack()
    {
        //Debug.Log("Enter stage 1 Running towards player");
        timer += Time.deltaTime;

        if(timer>= AttackTime)
        {
            bossState = Status.Follow;
            agent.enabled = true;
            timer = 0;
            return;
        }

        rb.MovePosition(rb.position + transform.forward * runSpeed * Time.deltaTime);
    }

    private void BossStunned()
    {
        timer += Time.deltaTime;
        if(timer >= stunTime)
        {
            //anim.SetBool("Stunned", false);
            ResetAnimVariable();

            bossState = Status.Follow;
            anim.SetBool("idle", false);
            agent.enabled = true;
            timer = AttackDelay;
        }
    }

    //Will be called by the statemachine scipt when the Boss started the running animation 
    public void Boss1StartAttack()
    {
        bossState = Status.Stage1Attack;
        timer = 0;
    }

    //increase the size of the electricity effect on the tail over time 
    private void IncreaseTailSize()
    {   
        //shape scale x towards 2 
        //emission towards 20
        if (maxPSTime >= TimeBeforeEnd)
        {
            //TODO :Add animation and effect(e.g. AOE in the map) to show the exection, implement these things in the stateBehaviour script of the skill3 animation
            //anim.SetBool("Endgame", true);
            Debug.Log("GG Player needs to target the boss'tail");
            return;
        }

        var main = tailPs.main;
        var shape = tailPs.shape;
        var emission = tailPs.emission;

        tailShapeVector.x = Mathf.Lerp(tailShapeVector.x, 1.25f, TailIncreaseRate);
        tailShape.scale = tailShapeVector;

        maxPSTime = Mathf.Lerp(maxPSTime, TimeBeforeEnd, TailIncreaseRate);
        //tailShapeVector.x = Mathf.Lerp(tailShapeVector.x, 1.45f, TailIncreaseRate);

        emissionRate.constant = Mathf.Lerp(emissionRate.constant, MaxEmissionRate, TailIncreaseRate);
        //main.startLifetime = Random.Range(minPSTime, maxPSTime);
    }

    public void SwitchLHitbox()
    {
      
        lhitbox.enabled = !lhitbox.enabled;
        
    }
    public void HandleTrigger(BodyPart bp)
    {
        switch (bp)
        {
            case BodyPart.Tail:
                numberHit++;
                if (numberHit >= maxNumberHit)
                {
                    timer = stunTime;
                    Debug.Log("Tail 3 times");
                }
                break;
        }
    }

    private void ResetAnimVariable()
    {
        anim.SetBool("FacingPlayer", false);
        anim.ResetTrigger("startAttack1");
        anim.SetBool("Stunned", false);
        anim.ResetTrigger("Stage1Claw");
        //Endgame always equal to false until end so dun need to reset 
    }

    //only attack1 will use trigger to detect collision (hitbox on the head of the boss)
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Tree" && bossState == Status.Stage1Attack)
        {
            bossState = Status.Stun;
            anim.SetBool("Stunned", true);
            timer = 0;
            anim.ResetTrigger("startAttack1");
        }

        
        if(other.gameObject.tag =="Player" &&bossState == Status.Follow)
        {
            anim.SetTrigger("Stage1Claw");
            bossState = Status.idle;
        }
        //if(other.gameObject.tag =="Player" && bossState == Status.Stun)
        //{
        //    numberHit++;
        //    if (numberHit >= maxNumberHit)
        //    {
        //        timer = stunTime;
        //        Debug.Log("Tail 3 times");
        //    }
        //}
    }
    

}
