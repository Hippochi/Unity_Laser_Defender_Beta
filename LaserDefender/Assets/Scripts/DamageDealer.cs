using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    [SerializeField] int damage = 100;
    [SerializeField] GameObject explosionFX;

    public int getDamage(){return damage;}
    public GameObject getExplodeFx() { return explosionFX; }

    public void Hit()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Hit();
    }

}
