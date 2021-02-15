using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float health = 400;

    [Header("Projectile")]
    [SerializeField] GameObject pewPewPrefab;
    [SerializeField] float pewSpeed = 10f;
    [SerializeField] float projectileFiringPeriod = 0.1f;
    [SerializeField] AudioClip deathSfx;

    float minX;
    float maxX;
    float minY;
    float maxY;
    Coroutine firingCoroutine;

    float spriteX;
    float spriteY;

    //cached reference
    Sprite mySprite;
    // Start is called before the first frame update
    void Start()
    {
        SetUpMoveBoundaries();
        mySprite = GetComponent<SpriteRenderer>().sprite;
        spriteX = mySprite.bounds.extents.x;
        spriteY = mySprite.bounds.extents.y;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Fire();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        DamageDealer damageDealer = collision.gameObject.GetComponent<DamageDealer>();
        if (!damageDealer) { return; }    
        processHits(damageDealer);
    }

    private void processHits(DamageDealer damageDealer)
    {
        health -= damageDealer.getDamage();
        if (health <= 0)
        {
            Destroy(gameObject);
            GameObject explosion = Instantiate(damageDealer.getExplodeFx(), transform.position, transform.rotation) as GameObject;
            Destroy(explosion, 1f);
            AudioSource.PlayClipAtPoint(deathSfx, Camera.main.transform.position);
            FindObjectOfType<LevelLoader>().GameOver();
        }
        FindObjectOfType<HealthDisplay>().ShowHealth();
    }

    private void Fire()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            firingCoroutine = StartCoroutine(FireContinuously());
        }
        if (Input.GetButtonUp("Fire1"))
        {
            //stops all coroutines
            //StopAllCoroutines();
            StopCoroutine(firingCoroutine);
        }
    }

    IEnumerator FireContinuously()
    {
        while (true)
        {
            GameObject laserPew = Instantiate(
                pewPewPrefab,
                transform.position,
                Quaternion.Euler(0, 0, 45)) as GameObject;
            laserPew.GetComponent<Rigidbody2D>().velocity = new Vector2(0, pewSpeed);
            yield return new WaitForSeconds(projectileFiringPeriod);
        }
    }

    private void SetUpMoveBoundaries()
    {
        Camera gameCamera = Camera.main;
        minX = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x;
        maxX = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x;
        minY = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y;
        maxY = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y;
    }

    private void Move()
    {
        var deltaX = Input.GetAxis("Horizontal");
        var deltaY = Input.GetAxis("Vertical");
        var newXPos = Mathf.Clamp(transform.position.x + deltaX * Time.deltaTime * moveSpeed, minX + spriteX, maxX - spriteX);
        var newYPos = Mathf.Clamp(transform.position.y + deltaY * Time.deltaTime * moveSpeed, minY + spriteY, maxY - spriteY);

        transform.position = new Vector2(newXPos, newYPos);
    }

    public float GetHealth() { return health; }

}
