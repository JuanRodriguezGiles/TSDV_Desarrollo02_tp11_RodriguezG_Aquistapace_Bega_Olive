﻿using UnityEngine;

public class Enemy : MonoBehaviour, ItakeDamage
{
    public GameObject[] rewardGameObject;
    private int ratedrop = 20;

    public int score;
    public float speed = 40f;
    public GameObject player;

    [SerializeField] Animator anim;
    
    private float timeToExplode = 1f;
    private float onTime;
    private int damageKamikaze = 20;
    private bool destroyed;
    private LevelGenerator levelGenerator;
    private void Awake()
    {
        levelGenerator = FindObjectOfType<LevelGenerator>();
        player = FindObjectOfType<Player>().gameObject;
    }

    private void Start()
    {
        if (!player)
            player = FindObjectOfType<Player>().gameObject;
    }

    private void Update()
    {
        if (levelGenerator.onGame)
        {
            onTime = Time.deltaTime;
            Vector3 objetive = player.transform.position;
            Vector3 direction = objetive - transform.position;
            transform.Translate(direction.normalized * (speed * Time.deltaTime));
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.tag == "Player" && !destroyed)
        {
            destroyed = true;
            other.transform.GetComponent<Player>().TakeDamage(damageKamikaze);
            TakeDamage(0);
            ratedrop = 0;
        }
    }

    public void TakeDamage(int damage)
    {
        int rand = Random.Range(1, 101);
        if (rand <= ratedrop)
        {
            int randPowerUp = Random.Range(0, rewardGameObject.Length);
            Instantiate(rewardGameObject[randPowerUp], transform.position, Quaternion.identity);
        }
        anim.SetBool("IsDead", true);
        transform.GetComponent<CircleCollider2D>().enabled = false;

        Destroy(this.gameObject, timeToExplode);
        levelGenerator.enemies[1].enemiesCount--;
        levelGenerator.enemyKilledCount++;
        levelGenerator.score += score;
        levelGenerator.totalEnemies ++;
    }
}