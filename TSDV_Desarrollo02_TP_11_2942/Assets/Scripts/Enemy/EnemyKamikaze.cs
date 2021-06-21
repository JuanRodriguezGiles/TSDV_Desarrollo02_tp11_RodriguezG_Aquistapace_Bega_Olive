﻿using UnityEngine;

public class EnemyKamikaze : MonoBehaviour, ItakeDamage
{

    // random de powerup en otro script

    private int score = 100;
    public GameObject explosion;
    private float timeToExplode = 0.8f;
    private float speed = 5;
    private float onTime;
    private int damageKamikaze = 20;
    public GameObject player;
    private bool destroyed;

    public void SetEnemy(GameObject player)
    {
        this.player = player;
    }

    private void Start()
    {
        if (player)
            player = FindObjectOfType<Player>().gameObject;
    }

    private void Update()
    {
        onTime = Time.deltaTime;
        Vector3 objetive = player.transform.position;
        Vector3 direction = objetive - transform.position;
        transform.Translate(direction.normalized * (speed * Time.deltaTime));
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.tag == "Player" && !destroyed)
        {
            destroyed = true;
            GetComponent<Player>().TakeDamage(damageKamikaze);
            Destroy(gameObject);
        }
    }

    public void TakeDamage(int damage)
    {
        Destroy(gameObject);
    }
}