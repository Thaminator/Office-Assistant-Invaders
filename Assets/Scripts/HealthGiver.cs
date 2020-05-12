using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthGiver : MonoBehaviour
{

    [SerializeField] int heal = 1;

    public int GetHeal()
    {
        return heal;
    }

    public void Hit()
    {
        Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }



}
