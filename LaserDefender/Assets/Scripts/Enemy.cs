using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float health = 100;
    [SerializeField] GameObject laserPrefab;
    [SerializeField] float enemyPewSpeed = 10f;
    [SerializeField] AudioClip deathSfx;
    [SerializeField] AudioClip shootSfx;
    [SerializeField] int points;
    // Start is called before the first frame update

    float shotCounter;
    void Start()
    {
        shotCounter = Random.Range(0.2f, 1.5f);
    }

    // Update is called once per frame
    void Update()
    {
        CountDownAndShoot();
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
        Die(damageDealer);
    }

    private void Die(DamageDealer damageDealer)
    {
        if (health <= 0)
        {
            Destroy(gameObject);
            FindObjectOfType<GameSession>().AddtoScore(points);
            GameObject explosion = Instantiate(damageDealer.getExplodeFx(), transform.position, transform.rotation) as GameObject;
            Destroy(explosion, 1f);
            AudioSource.PlayClipAtPoint(deathSfx, Camera.main.transform.position);
        }
    }

    private void CountDownAndShoot()
    {
        shotCounter -= Time.deltaTime;
        if (shotCounter <= 0f)
        {
            AudioSource.PlayClipAtPoint(shootSfx, Camera.main.transform.position, 0.25f);
            GameObject laserPew = Instantiate(laserPrefab,
                transform.position,
                Quaternion.identity) as GameObject;
            laserPew.GetComponent<Rigidbody2D>().velocity = new Vector2 (0, -1*enemyPewSpeed);
            shotCounter = Random.Range(0.2f, 3f);
        }

    }


}
