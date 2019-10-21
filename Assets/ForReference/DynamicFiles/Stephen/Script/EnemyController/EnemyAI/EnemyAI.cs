using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyAI : MonoBehaviour, ICombat
{
    protected EnemyStats stats;
    protected NavMeshAgent navMeshAgent;
    protected Animator anim;
    public string damagedAnimationName;
    [SerializeField] protected Material DissolveMaterial;
    [SerializeField] protected GameObject particalOfDeath;

    //Blood Bar
    public GameObject bloodBar;
    public Vector3 bloodBarLocation;
    private Slider bloodSlider;
    private GameObject CreatedBloodBar;
    private int knockThreshold;

    // Start is called before the first frame update


    protected virtual void Awake()
    {
        if (GetComponent<EnemyStats>()) stats = GetComponent<EnemyStats>(); else Debug.LogError("Enemy AI Controller need a EnemyStats class");
        if (GetComponent<NavMeshAgent>()) navMeshAgent = GetComponent<NavMeshAgent>(); else Debug.LogError("Enemy AI Controller need a NavMeshAgent class");
        if (GetComponent<Animator>()) anim = GetComponent<Animator>(); else Debug.LogError("EnemyAI Controller need an Animtor");
        CreateBloodBar(); 
    }

    protected virtual void Update()
    {
        BloodBarSystem();
    }

    private void CreateBloodBar()
    {
        if (stats == null)
        {
            Debug.LogWarning("There is no EnemyStats attach to " + this.transform.gameObject.name);
        }

        if (bloodBar)
        {
            CreatedBloodBar = Instantiate(bloodBar);
            CreatedBloodBar.transform.SetParent(transform);
            CreatedBloodBar.transform.localPosition = bloodBarLocation;
            bloodSlider = CreatedBloodBar.transform.Find("Canvas").Find("Slider").GetComponent<Slider>();

        }

    }
    void BloodBarSystem()
    {
        CreatedBloodBar.transform.localPosition = bloodBarLocation;
        UpdateBloodBar();

    }
    void UpdateBloodBar()
    {
        if (CreatedBloodBar && Camera.main)
        {
            bloodSlider.gameObject.transform.parent.LookAt(Camera.main.transform);
            bloodSlider.value = stats.getEnemyHealthPercentage();
        }
    }


    public void TakeDamage(int damage)
    {
        knockThreshold += damage;
        stats.TakeDamage(damage);
        if (knockThreshold >= stats.KnockThreshold())
        {
            if (this.damagedAnimationName != "")
            {
    
                PlayDamagedAnimation(this.damagedAnimationName);
            }
            knockThreshold = 0;
        }

    }

    public void PlayDamagedAnimation(string animationName)
    {
        anim.Play(animationName);
        //anim.SetTrigger("getHit");
        //anim.ResetTrigger("getHit");
    }



}
