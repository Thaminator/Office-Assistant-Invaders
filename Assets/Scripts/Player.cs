﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //config params
    [Header ("Player")]
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float verticalMoveSpeed = 10f;
    [SerializeField] float xPadding = 1f;
    [SerializeField] float yPadding = 1f;
    [SerializeField] int health = 5;
    [SerializeField] AudioClip deathSound;
    [SerializeField] [Range(0, 1)] float deathSoundVolume = 0.5f;
    [SerializeField] AudioClip shootSound;
    [SerializeField] [Range(0, 1)] float shootSoundVolume = 0.1f;

    [Header("Effects")]
    [SerializeField] AudioClip damageSound;
    [SerializeField] [Range(0, 1)] float damageSoundVolume = 0.5f;


    [Header ("Projectile")]
    [SerializeField] GameObject laserPrefab;
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] float projectileFiringPeriod = 0.1f;

    Coroutine firingCoroutine;

    float xMin;
    float xMax;
    float yMin;
    float yMax;

    [Header("PowerUp")]
    [SerializeField] float laserScaleFactor = 0.2f;
    [SerializeField] float delayInSeconds = 5f;

    // Start is called before the first frame update
    void Start()
    {
        SetUpMoveBoundaries();
    }

   

    // Update is called once per frame
    void Update()
    {
        Move();
        Fire();
    }

    private IEnumerator FireContinuously()
    {

        while (true)
        {
 
            GameObject laser = Instantiate(
                           laserPrefab,
                           transform.position,
                           Quaternion.identity) as GameObject;
            laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
            AudioSource.PlayClipAtPoint(shootSound, Camera.main.transform.position, shootSoundVolume);
            yield return new WaitForSeconds(projectileFiringPeriod);
        }
    }



    private void Fire()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            firingCoroutine = StartCoroutine(FireContinuously());
        }

        if(Input.GetButtonUp("Fire1"))
        {
            StopCoroutine(firingCoroutine);
        }


    }

    private void Move()
    {
        var deltaX = Input.GetAxis("Horizontal")*Time.deltaTime*moveSpeed;
        var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * verticalMoveSpeed;

        var newXPos =  Mathf.Clamp(transform.position.x + deltaX, xMin, xMax);
        var newYPos = Mathf.Clamp(transform.position.y + deltaY, yMin, yMax);
        transform.position = new Vector2(newXPos, newYPos);
    }


    private void SetUpMoveBoundaries()
    {
        Camera gameCamera = Camera.main;
        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + xPadding;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - xPadding;
        yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + yPadding;
        yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - yPadding;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
        if (!damageDealer) { return; }
        ProcessHit(damageDealer);
    }
    private void ProcessHit(DamageDealer damageDealer)
    {
        AudioSource.PlayClipAtPoint(damageSound, Camera.main.transform.position, damageSoundVolume);
        health -= damageDealer.GetDamage();
        damageDealer.Hit();
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        laserPrefab.transform.localScale = new Vector3(0.15f, 0.15f, 0.15f);
        projectileSpeed = 10f;
        shootSoundVolume = 0.01f;
        FindObjectOfType<Level>().LoadGameOver();
        Destroy(gameObject);
        AudioSource.PlayClipAtPoint(deathSound, Camera.main.transform.position, deathSoundVolume);

    }

    public int GetHealth()

    {
        return health;
    }

    public void GainHealth()
    {
        health = health + 1;
    }

    public void activateLaserPowerup()
    {
        makeLaserBig();
        StartCoroutine(makeLaserSmall());
    }

    public void makeLaserBig()
    {
        laserPrefab.transform.localScale = new Vector3(laserScaleFactor, laserScaleFactor, 0);
        projectileSpeed = 50f;
        shootSoundVolume = 0.4f;
    }

    public IEnumerator makeLaserSmall()
    {
        yield return new WaitForSeconds(delayInSeconds);
        laserPrefab.transform.localScale = new Vector3(0.15f, 0.15f, 0.15f);
        projectileSpeed = 10f;
        shootSoundVolume = 0.01f;

    }



}
