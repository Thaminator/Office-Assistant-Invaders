using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Enemy Stats")]
    [SerializeField] float health = 100f;
    [SerializeField] int scoreValue = 183;

    [Header("Shooting")]
    [SerializeField] float shotCounter;
    [SerializeField] float minTimeBetweenShots;
    [SerializeField] float maxTimeBetweenShots;
    [SerializeField] GameObject enemyLaserPrefab;
    [SerializeField] float enemyProjectileSpeed = 5f;

    [Header("Effects")]
    [SerializeField] GameObject deathVFX;
    [SerializeField] float durationOfExplosion = 1f;
    [SerializeField] AudioClip deathSound;
    [SerializeField] [Range(0,1)] float deathSoundVolume = 0.5f;
    [SerializeField] AudioClip shootSound;
    [SerializeField] [Range(0, 1)] float shootSoundVolume = 0.1f;

    [Header("Power Ups")]
    [SerializeField] GameObject lifePrefab;
    [SerializeField] AudioClip lifeSound;
    [SerializeField] [Range(0, 1)] float lifeSoundVolume = 0.1f;
    [SerializeField] float spawnProbability = 0.01f;

    [SerializeField] GameObject powerUpPrefab;
    [SerializeField] float powerUpSpawnProbability = 0.2f;


    void Start()
    {
        shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
    }

    void Update()
    {
        CountDownAndShoot();

    }

    private void CountDownAndShoot()
    {
        shotCounter -= Time.deltaTime;
        if (shotCounter <= 0f)
        { 
            Fire();
            shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
        }

    }

    private void Fire()
    {
        GameObject enemyLaser = Instantiate(
               enemyLaserPrefab,
               transform.position,
               Quaternion.identity) as GameObject;
        enemyLaser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -enemyProjectileSpeed);
        AudioSource.PlayClipAtPoint(shootSound, Camera.main.transform.position, shootSoundVolume);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
        if (!damageDealer) { return; }
        ProcessHit(damageDealer);
    }

    private void ProcessHit(DamageDealer damageDealer)
    {
        health -= damageDealer.GetDamage();
        damageDealer.Hit();
        if (health <= 0)
        {
            Die();
            if (Random.value > 1-spawnProbability)
            {
                ReleaseLife();
            }
            if (Random.value > 1 - powerUpSpawnProbability)
            {
                ReleasePowerUp();
            }
        }
    }


     private void Die()
    {
        FindObjectOfType<GameSession>().AddToScore(scoreValue);
        Destroy(gameObject);
        GameObject explosion = Instantiate(deathVFX, transform.position, transform.rotation);
        Destroy(explosion, durationOfExplosion);
        AudioSource.PlayClipAtPoint(deathSound, Camera.main.transform.position, deathSoundVolume);
    }

    private void ReleaseLife()
    {
        GameObject enemyLaser = Instantiate(
               lifePrefab,
               transform.position,
               Quaternion.identity) as GameObject;
        AudioSource.PlayClipAtPoint(lifeSound, Camera.main.transform.position, lifeSoundVolume);


    }

    private void ReleasePowerUp()
    {
        GameObject powerUp = Instantiate(
               powerUpPrefab,
               transform.position,
               Quaternion.identity) as GameObject;
       


    }



}
