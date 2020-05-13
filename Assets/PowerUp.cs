using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [Header("Health Stats")]
    [SerializeField] float health = 1f;
    [SerializeField] int scoreValue = 183;
    [SerializeField] float delayInSeconds = 1f;


    [Header("Effects")]
    [SerializeField] GameObject deathVFX;
    [SerializeField] AudioClip deathSound;
    [SerializeField] float durationOfExplosion = 1f;
    [SerializeField] [Range(0, 1)] float deathSoundVolume = 0.5f;





    private void OnTriggerEnter2D(Collider2D other)
    {
        LaserPowerup laserPowerup = other.gameObject.GetComponent<LaserPowerup>();
        if (!laserPowerup) { return; }
        ProcessPowerup(laserPowerup);

    }



    public void ProcessPowerup(LaserPowerup laserPowerup)
    {
        laserPowerup.Hit();
        StartCoroutine(Die());
    }



    IEnumerator Die()
    {
        yield return new WaitForSeconds(delayInSeconds);
        FindObjectOfType<GameSession>().AddToScore(scoreValue);
        FindObjectOfType<Player>().activateLaserPowerup();
        Destroy(gameObject);
        GameObject explosion = Instantiate(deathVFX, transform.position, transform.rotation);
        Destroy(explosion, durationOfExplosion);
        AudioSource.PlayClipAtPoint(deathSound, Camera.main.transform.position, deathSoundVolume);
    }




}
