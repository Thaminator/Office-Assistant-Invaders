using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spinner : MonoBehaviour
{

    [SerializeField] float speedofSpin = 50f;
    void Update()
    {
        transform.Rotate(0, 0, speedofSpin*Time.deltaTime);
    }
}
