using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserPowerup : MonoBehaviour
{

    [SerializeField] float laserScaleFactor = 0.2f;
    [SerializeField] float delayInSeconds = 10f;

    public void activateLaserPowerup()
    {
        makeLaserBig();

        StartCoroutine(makeLaserSmall());
    }



    public void makeLaserBig()
    {
        gameObject.transform.localScale += new Vector3(laserScaleFactor, laserScaleFactor, 0); 

    }

    public IEnumerator makeLaserSmall()
    {
        yield return new WaitForSeconds(delayInSeconds);
        gameObject.transform.localScale -= new Vector3(1, 1, 0); ;

    }

    public void Hit()
    {
        Destroy(gameObject);
    }

}
