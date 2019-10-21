using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDyingKnight : MonoBehaviour
{
    public GameObject Knight;
    public GameObject Arrow;
    public GameObject Monster;
    public Transform ArrowPosition;
    private bool shootArrowTimer;
    private bool shoot = false;
    
    private float timer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (shootArrowTimer)
        {
            timer += Time.deltaTime; 
        }
        if ((int)timer == 2 && shoot ==false)
        {
            shoot = true;
            Arrow.transform.position = ArrowPosition.position;
            Arrow.transform.rotation = ArrowPosition.rotation;
            Instantiate(Arrow);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Knight.SetActive(true);
            Monster.SetActive(true);
            shootArrowTimer = true;

        }
    }
}
