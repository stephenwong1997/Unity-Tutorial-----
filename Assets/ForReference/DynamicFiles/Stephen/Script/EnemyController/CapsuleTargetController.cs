using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CapsuleTargetController : MonoBehaviour
{
    EnemyStats enemyStats;
    public GameObject bloodBar;
    //GameObject player;
    private Slider bloodSlider;
    private GameObject CreatedBloodBar;
    public Material DissolveMaterial;
    private float _materialCutOff;
    private bool died;
    public GameObject particalOfDeath;
    // Start is called before the first frame update
    void Start()
    {
        //player = GameObject.FindGameObjectWithTag("Player");
        if (GetComponent<EnemyStats>())
        { enemyStats = GetComponent<EnemyStats>();} else {Debug.LogWarning("There is no EnemyStats attach to "+ this.transform.gameObject.name);}
        if (bloodBar)
        {
            CreatedBloodBar = Instantiate(bloodBar);
            CreatedBloodBar.transform.SetParent(transform);
            CreatedBloodBar.transform.localPosition = new Vector3(0, 1.3f, 0);
            bloodSlider = CreatedBloodBar.transform.Find("Canvas").Find("Slider").GetComponent<Slider>();
        }



    }

    // Update is called once per frame
    void Update()
    {
        DeathDissolve();
        if (CreatedBloodBar)
        {
            bloodSlider.gameObject.transform.parent.LookAt(Camera.main.transform);
            bloodSlider.value = enemyStats.getEnemyHealthPercentage();
        }
    }

    void DeathDissolve()
    {
        if (enemyStats.getEnemyHealthPercentage() < 0)
        {
            GetComponent<Renderer>().material = DissolveMaterial;
            _materialCutOff += Time.deltaTime / 2;
            GetComponent<Renderer>().material.SetFloat("_cutoff", _materialCutOff);
            particalOfDeath.SetActive(true);
            if(CreatedBloodBar)
            {
                Destroy(CreatedBloodBar);
            }
            if (_materialCutOff > 2)
            {
                Destroy(this.gameObject);
            }
        }

    }

}
