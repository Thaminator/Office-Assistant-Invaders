using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPowerUp : MonoBehaviour
{
    [Header("Health Stats")]
    [SerializeField] float health = 1f;
    [SerializeField] int scoreValue = 183;
    [SerializeField] int lifeValue = 1;
    [SerializeField] float delayInSeconds = 1f;


    [Header("Effects")]
    [SerializeField] GameObject deathVFX;
    [SerializeField] AudioClip deathSound;
    [SerializeField] float durationOfExplosion = 1f;
    [SerializeField] [Range(0,1)] float deathSoundVolume = 0.5f;




    void Start()
    {
    }

    void Update()
    {

    }

    



    private void OnTriggerEnter2D(Collider2D other)
    {
        HealthGiver healthGiver = other.gameObject.GetComponent<HealthGiver>();
        if (!healthGiver) { return; }
        ProcessHeal(healthGiver);
    }



    private void ProcessHeal(HealthGiver healthGiver)
    {
        healthGiver.Hit();
            StartCoroutine(Die());
    }



    IEnumerator Die()
    {
        yield return new WaitForSeconds(delayInSeconds);
        FindObjectOfType<GameSession>().AddToScore(scoreValue);
        FindObjectOfType<Player>().GainHealth();
        Destroy(gameObject);
        GameObject explosion = Instantiate(deathVFX, transform.position, transform.rotation);
        Destroy(explosion, durationOfExplosion);
        AudioSource.PlayClipAtPoint(deathSound, Camera.main.transform.position, deathSoundVolume);
    }






}
