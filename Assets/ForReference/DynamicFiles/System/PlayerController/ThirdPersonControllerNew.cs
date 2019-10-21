using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonControllerNew : MonoBehaviour
{
    //input field
    public static float InputX;
    public static float InputZ;
    public float RotationSpeed = 0.2f;
    public float jumpSpeed = 10f;

    //constant force
    private Vector3 desiredMoveDirection;
    protected float verticalSpeed;
    protected float forwardSpeed;
    protected float desiredForwardSpeed;
    public float gravity = 20f;

    //constant
    const float stickingGravityProportion = 0.3f;
    private float defaultPlayerHeight;
    private Vector3 defaultPlayerCenter;

    //calculateion
    private Vector3 movement;
    private float remapShiftSpeed = 1f;
    public bool canWalk = true;
    public bool canRun = true;
    public bool canAttack = true;
    public bool canRoll = true;
    public bool canJump = true;
    public bool canCrouch = true;
    public bool canChanageWeapon = true;
    public bool canRotateCharacter = true;
    private bool isGrounded;
    private float knockVer;
    private float knockHoz;


    //states
    private int attackMode = 0;
    private bool readyToJump;
    private bool crouched;
    private bool attacking = false;
    private bool changingAttackMode = false;
    private bool gettingHit = false;
    private bool knockedDown;

    protected AnimatorStateInfo m_PreviousCurrentStateInfo;    // Information about the base layer of the animator from last frame.
    protected AnimatorStateInfo m_PreviousNextStateInfo;
    protected bool m_PreviousIsAnimatorTransitioning;

    protected AnimatorStateInfo m_CurrentStateInfo;    // Information about the base layer of the animator cached.
    protected AnimatorStateInfo m_NextStateInfo;
    protected bool m_IsAnimatorTransitioning;

    // Parameters
    readonly int fallingAnimationHash = Animator.StringToHash("Unarmed - Fall");
    readonly int Combo1AnimationHash = Animator.StringToHash("Unarmed-Attack-L1");
    readonly int Combo2AnimationHash = Animator.StringToHash("Unarmed-Attack-R1");
    readonly int Combo3AnimationHash = Animator.StringToHash("Unarmed-Attack-L3");



    //basic Objects
    private CharacterStats charStats;
    private Camera cam;
    private Animator anim;
    private CameraLock cameraLock;
    private int m_cameraLock;
    private CharacterController characterController;
    private WeaponManager weaponManager;
    public GameObject weaponBack;
    public GameObject weaponHanded_WeaponManager;
    public delegate void DelegateMethod();
    public DelegateMethod methodToCall;

    public GameObject test;



    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        charStats = FindObjectOfType<CharacterStats>();
        cameraLock = FindObjectOfType<CameraLock>();
        anim = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
        weaponManager = transform.GetComponentInChildren<WeaponManager>();
        m_cameraLock = cameraLock.GetCameraLock();
        if (charStats.GetHavingWeapon().ToString() != Weapon.Armed.ToString())
        {
            weaponBack.transform.Find(charStats.GetHavingWeapon().ToString()).gameObject.SetActive(true);
        }
        

        defaultPlayerHeight = characterController.height;
        defaultPlayerCenter = characterController.center;

    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (m_cameraLock != cameraLock.GetCameraLock())
        {
            UpdateCameraLockState();
            m_cameraLock = cameraLock.GetCameraLock();
        }
        CacheAnimatorState();
        GetInput();
        if (canRun)
        {
            Run(); // edit the remapShiftSpeed value;
        }
        if(canCrouch)
        {
            Crouch();
        }
        detectAttackMode();



        if (canAttack) Attack();


        RotateCharacterToCameraPosition();


        isGrounded = characterController.isGrounded;


        if (Input.GetKeyDown(KeyCode.L))
        { FlyKnockBack(test,10,10, 10); }
 
        
    }

    private void OnAnimatorMove()
    {
        CalculateForwardMovement();
        CalculateVerticalMovement();

         movement = transform.forward * forwardSpeed * Time.deltaTime;
        if (attacking || changingAttackMode || gettingHit ||knockedDown)
        { movement = anim.deltaPosition; }
        if(knockVer!= 0 || knockHoz != 0)
        {
            movement = -transform.forward * knockHoz * Time.deltaTime;
            verticalSpeed = knockVer;
            knockVer = knockHoz = 0;

        }

        movement += Vector3.up * verticalSpeed * Time.deltaTime;

  

        characterController.Move(movement);

    }

    void UpdateCameraLockState()
    {
        if(cameraLock.GetCameraLock() >0 )
        {
            canWalk = false;
            canRun = false;
            canAttack = false;
            canRoll = false;
            canJump = false;
            canCrouch = false;
            canChanageWeapon = false;
            canRotateCharacter = false;
        }
        if (cameraLock.GetCameraLock() <= 0)
        {
            canWalk = true;
            canRun = true;
            canAttack = true;
            canRoll = true;
            canJump = true;
            canCrouch = true;
            canChanageWeapon = true;
            canRotateCharacter = true;
        }
    }

    void CacheAnimatorState()
    {
        m_PreviousCurrentStateInfo = m_CurrentStateInfo;
        m_PreviousNextStateInfo = m_NextStateInfo;
        m_PreviousIsAnimatorTransitioning = m_IsAnimatorTransitioning;

        m_CurrentStateInfo = anim.GetCurrentAnimatorStateInfo(0);
        m_NextStateInfo = anim.GetNextAnimatorStateInfo(0);
        m_IsAnimatorTransitioning = anim.IsInTransition(0);
    }


    private void Run()
    {
            if (Input.GetKey(KeyCode.LeftShift) && !crouched && attackMode == 0)
                remapShiftSpeed = Mathf.Lerp(remapShiftSpeed, 1f, 0.25f);
            else remapShiftSpeed = Mathf.Lerp(remapShiftSpeed, 0.5f, 0.25f);

            if (attackMode == 1)
            {
                remapShiftSpeed = 1f;

            }

    }

    void Crouch()
    {

        if (Input.GetKey(KeyCode.LeftControl))
        {

            characterController.height = defaultPlayerHeight * 3 / 4;
            characterController.center = defaultPlayerCenter * 6.5f / 10;

            //capsuleCollider.center = new Vector3 (0,defaultPlayerHeight / (2 * 2),0);
            anim.SetBool("crouch", true);
            crouched = true;
        }
        else
        {
            characterController.height = defaultPlayerHeight;
            characterController.center = defaultPlayerCenter;
            anim.SetBool("crouch", false);
            crouched = false;
        }
    }

    public void detectAttackMode()
    {
        if (isGrounded && canChanageWeapon) {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                changeAttackMode(0);

            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                changeAttackMode(1);

            }
        }

    }

    public void changeAttackMode(int attackMode)
    {
        //changingAttackMode = true;
        anim.SetInteger("attackMode", attackMode);
        this.attackMode = attackMode;
        void MethodToPass()
        {
            if (attackMode == 0 )
            {
                weaponBack.transform.Find(charStats.GetHavingWeapon().ToString()).gameObject.SetActive(true);
                weaponHanded_WeaponManager.transform.Find(charStats.GetHavingWeapon().ToString()).gameObject.SetActive(false);
            }
            if (attackMode == 1)
            {
                weaponBack.transform.Find(charStats.GetHavingWeapon().ToString()).gameObject.SetActive(false);
                weaponHanded_WeaponManager.transform.Find(charStats.GetHavingWeapon().ToString()).gameObject.SetActive(true);
            }


        }
        methodToCall = MethodToPass;

        if (attackMode == 0)
        {
            StartCoroutine(wait(0.8f, methodToCall));
        }
        if (attackMode == 1)
        {
            StartCoroutine(wait(0.05f, methodToCall));
       }
     
    }

    public void StateChangeChangeWeapon()
    {
        changingAttackMode = !changingAttackMode;
    }

    public void GetHitBegin()
    {
        gettingHit = true;
    }

    public void GetHitEnd()
    {
        gettingHit = false;
    }

    public void StoodUp()
    {
        canWalk = true;
        canRun = true;
        canAttack = true;
        canRoll = true;
        canJump = true;
        canCrouch = true;
        canChanageWeapon = true;
        canRotateCharacter = true;
        knockedDown = false;
    }

    public void FlyKnockBack(GameObject incomingObject, float knockVer, float knockHoz ,int damage)
    {
        transform.LookAt(new Vector3(incomingObject.transform.position.x,transform.position.y, incomingObject.transform.position.z));
    
        anim.SetBool("knockFly", true);
        anim.Play("Hit-To-Back");
        this.knockVer = knockVer;
        this.knockHoz = knockHoz;
        knockedDown = true;
        canWalk = false;
        canRun = false;
        canAttack = false;
        canRoll = false;
        canJump = false;
        canCrouch = false;
        canChanageWeapon = false;
        canRotateCharacter = false;

    }

    void Attack()
    {
        anim.ResetTrigger("attackTrigger");
        if (attackMode == 0 && !playingAttackAnimation())
        {
            anim.SetBool("attack", false);

        }
        else if (attackMode == 1 && !playingAttackAnimation())
        {
            anim.SetBool("attack", false);

        }
 
        if (Input.GetMouseButtonDown(0)  && !crouched )
        {
            anim.SetTrigger("attackTrigger");
        }


    }

    private bool playingAttackAnimation()
    {
        bool returner = m_CurrentStateInfo.shortNameHash == Combo1AnimationHash; //m_NextStateInfo.shortNameHash == Combo1AnimationHash ||
        returner |= m_CurrentStateInfo.shortNameHash == Combo2AnimationHash;
        returner |= m_CurrentStateInfo.shortNameHash == Combo3AnimationHash;
        return returner;
    }



    public void Enable()
    {
        weaponManager.EnableWeaponDetection(charStats.GetWeapon());
    } // animation Event
    public void Disable()
    {
        weaponManager.DisableWeaponDetection(charStats.GetWeapon());
    }

    public void PauseMovement()
    {

        attacking = true;
    }
    public void StopPausingMovement()
    {
        attacking = false;
    }



    void GetInput()
    {
        InputX = Input.GetAxis("Horizontal");
        InputZ = Input.GetAxis("Vertical");
    }

    void RotateCharacterToCameraPosition()
    {
        Vector2 moveInput = new Vector2(InputX, InputZ);
        if (moveInput.sqrMagnitude > 1f)
         moveInput.Normalize();

        if (moveInput.magnitude > 0f)
        {
            var forward = cam.transform.forward;
            var right = cam.transform.right;

            forward.y = 0f;
            right.y = 0f;

            desiredMoveDirection = forward * InputZ + right * InputX;

            if(canRotateCharacter)
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(desiredMoveDirection), RotationSpeed);
        }
    }

    void CalculateForwardMovement()
    {
        
        Vector2 moveInput = new Vector2(InputX, InputZ);
        print(moveInput);
        if (moveInput.sqrMagnitude > 1f)
            moveInput.Normalize();

        desiredForwardSpeed = moveInput.magnitude * charStats.getSpeed()* remapShiftSpeed;
        float acceleration = moveInput.sqrMagnitude >0f ? 20 : 25;

        forwardSpeed = Mathf.MoveTowards(forwardSpeed, desiredForwardSpeed, acceleration * Time.deltaTime);
        if (!canWalk)
        {
            moveInput = Vector2.zero;
            forwardSpeed = 0f;
        }
    
        anim.SetFloat("getNumberOfOne", ExtensionMethods.Remap(moveInput.magnitude, 0, 1, 0, remapShiftSpeed));
        if(crouched)
        anim.SetFloat("crouchWalk", moveInput.magnitude);
 
    }


    void CalculateVerticalMovement()
    {
        if (isGrounded)
        {
            anim.SetBool("knockFly", false);
            anim.SetBool("isJumping", false);
            anim.SetBool("isGrounded", true);


            verticalSpeed = -gravity * stickingGravityProportion;
            if (canJump &&  !m_IsAnimatorTransitioning)//m_CurrentStateInfo.shortNameHash != fallingAnimationHash
            {
                Jump();
            }
        }
        else {
            anim.SetBool("isGrounded", false);
            verticalSpeed -= gravity * Time.deltaTime; }

    }


    void Jump()
    {

        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            verticalSpeed = jumpSpeed;
            anim.SetBool("isJumping", true);
            anim.SetBool("isGrounded", false);
        }


    }

    public void GetHit()
    {
        if (attackMode == 0)
        {
            switch (Random.Range(1, 6))
            {
                case 1:
                    anim.Play("Unarmed-GetHit-B1");
                    break;
                case 2:
                    anim.Play("Unarmed-GetHit-F1");
                    break;
                case 3:
                    anim.Play("Unarmed-GetHit-F2");
                    break;
                case 4:
                    anim.Play("Unarmed-GetHit-L1");
                    break;
                case 5:
                    anim.Play("Unarmed-GetHit-R1");
                    break;
            }
        }

        if (attackMode == 1)
        {
            switch (Random.Range(1, 6))
            {
                case 1:
                    anim.Play("2Hand-Sword-GetHit-B1");
                    break;
                case 2:
                    anim.Play("2Hand-Sword-GetHit-F1");
                    break;
                case 3:
                    anim.Play("2Hand-Sword-GetHit-F1");
                    break;
                case 4:
                    anim.Play("2Hand-Sword-GetHit-L1");
                    break;
                case 5:
                    anim.Play("2Hand-Sword-GetHit-R1");
                    break;
            }
        }

    }

    IEnumerator wait(float waitTime, DelegateMethod method)
    {
        yield return new WaitForSecondsRealtime(waitTime);
        method();
    }

}
