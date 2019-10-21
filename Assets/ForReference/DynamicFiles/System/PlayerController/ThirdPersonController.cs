using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script requires you to have setup your animator with 3 parameters, "InputMagnitude", "InputX", "InputZ"
//With a blend tree to control the inputmagnitude and allow blending between animations.

//[RequireComponent(typeof(CharacterController))]


//when player jumps, it can attack, so it slow it down for 0.5f second
//SlideAttack() not working

public class ThirdPersonController : MonoBehaviour
{

    public static float InputX;
    public static float InputZ;
    public static bool characterIsJump;
    public static bool characterIsRun;
    private Vector3 desiredMoveDirection;
    public float desiredRotationSpeed = 0.2f;
    private Animator anim;
    public float Speed = 3;
    private float allowPlayerRotation;
    private Camera cam;
    public bool canWalk = true;
    public bool canRun = true;
    public bool canAttack = true;
    public bool canRoll = true;
    public bool canJump = true;
    public bool canCrouch = true;
    private bool isGrounded;
    public bool crouched;
    private bool attacking;
    private bool rolling;
    private bool changingAttackMode;
    private bool died;
    public int attackMode = 0;
    public float jumpForce = 13000;
    private float verticalVel;
    private Vector3 moveVector;
    private Vector3 verticalVelocity;
    private Rigidbody characterRigidbody;
    private float getNumberOfOne;
    public delegate void DelegateMethod();
    public DelegateMethod methodToCall;
    public GameObject weaponBack;
    public GameObject weaponHanded_WeaponManager;
    public Weapon weapon;
    public Weapon currentWeapon;
    private WeaponManager weaponManager;
    private CharacterStats updateCharStats;
    private float defaultPlayerHeight;
    private Vector3 defaultPlayerCenter;
    private CapsuleCollider capsuleCollider;
    private CameraLock cameraLock;
    // Use this for initialization
    void Start()
    {
        //Cursor.lockState =CursorLockMode.Locked;
        anim = this.GetComponent<Animator>();
        cam = Camera.main;
        characterRigidbody = this.GetComponent<Rigidbody>();
        //controller = this.GetComponent<CharacterController>();
     
            updateCharStats = FindObjectOfType<CharacterStats>();
        weapon = FindObjectOfType<CharacterStats>().GetWeapon();
        currentWeapon = Weapon.Armed;
        if (weaponBack.transform.Find(weapon.ToString())) { weaponBack.transform.Find(weapon.ToString()).gameObject.SetActive(true); } else if (weapon == Weapon.Armed) {
            weaponHanded_WeaponManager.transform.Find(weapon.ToString()).gameObject.SetActive(true);
        }
        if (weaponHanded_WeaponManager.GetComponent<WeaponManager>()) { weaponManager = weaponHanded_WeaponManager.GetComponent<WeaponManager>(); } else { Debug.LogWarning("WeaponHanded is missing WeaponManager!"); }

        Speed = updateCharStats.getSpeed();
        cameraLock = FindObjectOfType<CameraLock>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        defaultPlayerHeight = capsuleCollider.height;
        defaultPlayerCenter = capsuleCollider.center;

    }

    // Update is called once per frame

    void Update()
    {

        updateCharStats.SetWeapon(weapon);
        //if (dialogueManager.checkIfConversationing())
        //{
        //    Cursor.lockState = CursorLockMode.None;
        //}
        //else
        //{
        //    Cursor.lockState = CursorLockMode.Locked;
        //}
        if (!died && cameraLock.GetCameraLock()<=0)
        {
            isGrounded = fixedIsGround();
            if (canJump)
            {
                fixedJump();
            }
            //SlideAttack();//cant use, dont know why
            if(canAttack)  Attack();
            if (!attacking)
            {
                InputMagnitude();
                if(canCrouch)Crouch();
                //fixedRun();
            }
            if (canRoll)  Roll();
            detectAttackMode();

        }
        else {
            InputMagnitude();
        }
    }
    void onClickChanageWeapon()
    {
        if (Input.GetKeyDown(KeyCode.P)){
            ChangeStatWeapon(Weapon.Dagger);
        }
    }
    public void ChangeStatWeapon(Weapon changeWeapon)
    {
        ClearCurrentWeapon(weapon);
        changeAttackMode(0);
        weapon = changeWeapon;
        updateCharStats.SetWeapon(changeWeapon);
        if(attackMode==0)weaponBack.transform.Find(weapon.ToString()).gameObject.SetActive(true);
        else if(attackMode==1) weaponHanded_WeaponManager.transform.Find(weapon.ToString()).gameObject.SetActive(true);
    }
    void ClearCurrentWeapon(Weapon currentWeapon)
    {
        weaponHanded_WeaponManager.transform.Find(weapon.ToString()).gameObject.SetActive(false);
        weaponBack.transform.Find(weapon.ToString()).gameObject.SetActive(false);
    }


    public void Enable()
    {
        //print("Enable Weapon Detection");
        weaponManager.EnableWeaponDetection(currentWeapon);
    } // animation Event
    public void Disable()
    {
        //print("Disable Weapon Detection");
        weaponManager.DisableWeaponDetection(currentWeapon);
    }
 


    void Crouch()
    {

        if (Input.GetKey(KeyCode.LeftControl))
        {
            capsuleCollider.height = defaultPlayerHeight*3/ 4;
            capsuleCollider.center = defaultPlayerCenter*6.5f/ 10;

            //capsuleCollider.center = new Vector3 (0,defaultPlayerHeight / (2 * 2),0);
            anim.SetBool("crouch", true);
            crouched = true;
        }
        else
        {
            capsuleCollider.height = defaultPlayerHeight;
            capsuleCollider.center = defaultPlayerCenter;
            anim.SetBool("crouch", false);
            crouched = false;
        }
    }
    public void detectAttackMode()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            changeAttackMode(0);
            chanageWeapon(false);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) && weapon != Weapon.Armed)
        {
            changeAttackMode(1);
            crouched = false;
            chanageWeapon(true);
        }

    }
    public void changeAttackMode(int attackMode)
    {
        changingAttackMode = true;
        anim.SetInteger("attackMode", attackMode);
        this.attackMode = attackMode;
        void MethodToPass()
        {
            changingAttackMode = false;
        }
        methodToCall = MethodToPass;

        StartCoroutine(wait(1f, methodToCall));
    }
    public void chanageWeapon(bool onHand)
    {
        if (onHand)
        {
            void MethodToPass()
            {
                //print("equip weapon");
                currentWeapon = weapon;
                weaponBack.transform.Find(weapon.ToString()).gameObject.SetActive(!onHand);
                weaponHanded_WeaponManager.transform.Find(weapon.ToString()).gameObject.SetActive(onHand);
            }
            methodToCall = MethodToPass;

            StartCoroutine(wait(0.3f, methodToCall));
        }
        else
        {
            void MethodToPass()
            {
                //print("Disarm");
                currentWeapon = Weapon.Armed;
                if(weaponBack.transform.Find(weapon.ToString()))weaponBack.transform.Find(weapon.ToString()).gameObject.SetActive(!onHand);
                weaponHanded_WeaponManager.transform.Find(weapon.ToString()).gameObject.SetActive(onHand);
            }
            methodToCall = MethodToPass;

            StartCoroutine(wait(0.6f, methodToCall));
        }

    }
    public void GetHit()
    {
        if ( attackMode == 0)
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

    void checkDeath()
    {
        if(updateCharStats.getPlayerHealthPercentage() <=0)
        if (attackMode == 0)
        {
            died = true;
            anim.Play("Unarmed-Death1");

        }
        if (attackMode == 1)
        {
            died = true;
            anim.Play("2Hand-Sword-Death1");
        }

    }


    void Roll()
    {
        if (Input.GetKeyDown(KeyCode.C) && !rolling && (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D)))
        {
            anim.SetBool("roll", true);
            rolling = true;
            void MethodToPass()
            {
                rolling = false;
            }
            methodToCall = MethodToPass;

            StartCoroutine(wait(0.83f, methodToCall));
        }
        else
        {
            anim.SetBool("roll", false);
        }
        if (rolling)//accelerate
        {
            //this.transform.Translate(Vector3.forward * Speed * Time.deltaTime);
            this.GetComponent<Rigidbody>().velocity = Vector3.forward * Speed * Time.deltaTime;
        }
    }
    void Attack()
    {
      

        if (Input.GetMouseButtonDown(0) && !attacking && !crouched  && !rolling &&! checkAnimationJumpIsPlaying())
        {
            anim.SetBool("attack", true);
            attacking = true;
            void MethodToPass()
            {
                attacking = false;
                anim.SetBool("attack", false);
            }
            methodToCall = MethodToPass;
            if (attackMode == 0)
            {
                StartCoroutine(wait(0.12f, methodToCall));
            }
            else if (attackMode == 1)
            { StartCoroutine(wait(0.20f, methodToCall)); }

        }
    }
    bool checkAnimationAttackIsPlayeing()
    {
        return anim.GetCurrentAnimatorStateInfo(0).IsName("Unarmed-Attack-L1") || anim.GetCurrentAnimatorStateInfo(0).IsName("Unarmed-Attack-R1") || anim.GetCurrentAnimatorStateInfo(0).IsName("Unarmed-Attack-L3") || anim.GetCurrentAnimatorStateInfo(0).IsName("2Hand-Sword-Attack1") || anim.GetCurrentAnimatorStateInfo(0).IsName("2Hand-Sword-Attack4") || anim.GetCurrentAnimatorStateInfo(0).IsName("2Hand-Sword-Attack5");
    }
    bool checkAnimationJumpIsPlaying()
    {
        return anim.GetCurrentAnimatorStateInfo(0).IsName("2Hand-Sword-Fall") || anim.GetCurrentAnimatorStateInfo(0).IsName("2Hand-Sword-Jump") || anim.GetCurrentAnimatorStateInfo(0).IsName("Jump") || anim.GetCurrentAnimatorStateInfo(0).IsName("Unarmed-Fall");
    }
    void PlayerMoveAndRotation()
    {
        InputX = Input.GetAxis("Horizontal");
        InputZ = Input.GetAxis("Vertical");

        var forward = cam.transform.forward;
        var right = cam.transform.right;

        forward.y = 0f;
        right.y = 0f;

        desiredMoveDirection = forward * InputZ + right * InputX;


        if (cameraLock.GetCameraLock()<=0) { 
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(desiredMoveDirection), desiredRotationSpeed);
    }

        if (Input.GetKey(KeyCode.LeftShift) && canRun)
        {
            if ((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D)))
            {
                getNumberOfOne = Mathf.Lerp(getNumberOfOne, 1, 0.15f);
            }
            else
            {
                getNumberOfOne = Mathf.Lerp(getNumberOfOne, 0, 0.2f);
            }
        }
        else
        {
            if (attackMode == 0)
            {
                getNumberOfOne = Mathf.Lerp(getNumberOfOne,Mathf.Max(Mathf.Abs(InputX), Mathf.Abs(InputZ)) / 2,0.15f);
            }
            else if(attackMode == 1)
            {
                getNumberOfOne = Mathf.Max(Mathf.Abs(InputX), Mathf.Abs(InputZ));
            }
        }
        if (crouched)
        {
            getNumberOfOne = Mathf.Max(Mathf.Abs(InputX), Mathf.Abs(InputZ));
            anim.SetFloat("crouchWalk", getNumberOfOne);
            getNumberOfOne /= 5;
        }
        if (changingAttackMode)
        {
            getNumberOfOne = Mathf.Lerp(getNumberOfOne, 0, 0.8f);
        }


        if (getNumberOfOne > 0 && !crouched)
        {
            anim.SetFloat("getNumberOfOne", getNumberOfOne);
        }

        if (cameraLock.GetCameraLock()>0)
        {
            getNumberOfOne = Mathf.Lerp(getNumberOfOne, 0, 0.2f);
            anim.SetFloat("getNumberOfOne", getNumberOfOne);
        }
        else {
            this.transform.Translate(Vector3.forward * getNumberOfOne * 5 * Speed * Time.deltaTime);
        }

    }

    void InputMagnitude()
    {
        //Calculate Input Vectors
        InputX = Input.GetAxis("Horizontal");
        InputZ = Input.GetAxis("Vertical");
        float anyInputWASD = new Vector2(InputX, InputZ).sqrMagnitude;
        if (anyInputWASD > allowPlayerRotation)
        {
            PlayerMoveAndRotation();
        }

    }
    bool fixedIsGround()
    {
        Ray ray = new Ray(new Vector3(transform.position.x, capsuleCollider.bounds.max.y - capsuleCollider.height*0.99f, transform.position.z), -Vector3.up);
        return Physics.Raycast(ray, 0.3f);
    }
    void fixedJump()
    {
        if (fixedIsGround() && Input.GetKeyDown(KeyCode.Space))
        {

            characterIsJump = true;
            characterRigidbody.velocity = Vector3.up * jumpForce;
            anim.SetBool("isJumping", true);
        }
        else
        {
            characterIsJump = false;
            anim.SetBool("isJumping", false);
            //void MethodToPass()
            //{
            //    characterIsJump = false;
            //}
            //methodToCall = MethodToPass;
            //if (characterIsJump)
            //{
            //    StartCoroutine(wait(0.5f, methodToCall));
            //}
        }

        if (fixedIsGround())
        {
            anim.SetBool("isGrounded", true);

        }
        else
        {
            anim.SetBool("isGrounded", false);
        }
    }
    //void fixedRun()
    //{
    //    if (Input.GetKey(KeyCode.LeftShift))
    //    {
    //        characterIsRun = true;
    //        //Speed = defaultSpeed + 2;
    //    }
    //    else
    //    {
    //        //Speed = defaultSpeed;
    //        characterIsRun = false;
    //    }
    //}


    IEnumerator wait(float waitTime, DelegateMethod method)
    {
        yield return new WaitForSecondsRealtime(waitTime);
        method();
    }
    //void SlideAttack()
    //{
    //    if (Input.GetMouseButtonDown(0) && (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D)))
    //    {
    //        anim.SetBool("slideAttack", true);
    //    }
    //    else {
    //        anim.SetBool("slideAttack", false);
    //    }

    //}
}