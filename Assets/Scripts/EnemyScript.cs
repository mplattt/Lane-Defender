using System.Collections;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class EnemyScript : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private int health;
    [SerializeField] private int enemyScore;
    public GameObject player;
    private Rigidbody2D rb;
    [SerializeField] private Sprite walk1;
    [SerializeField] private Sprite walk2;
    [SerializeField] private Sprite hit;
    [SerializeField] private Sprite dead;
    private bool _hit;
    private bool _dead;
    private BoxCollider2D box;
    [SerializeField] private GameObject explosion;

    public AudioClip enemyDamage;
    public AudioClip enemyDead;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(Walk1());
        _hit = false;
        _dead = false;
        box = gameObject.GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!_hit && !_dead)
        {
            rb.linearVelocity = new Vector2(-1 * speed, 0);
        }
        else
        {
            rb.linearVelocity = new Vector2(0, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            LoseHealth();
            Instantiate(explosion, new Vector2(collision.transform.position.x, collision.transform.position.y),
            Quaternion.identity);
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.tag == "Back Border")
        {
            player.GetComponent<PlayerController>().LoseLives();
            Destroy(gameObject);
        }
    }

    private void LoseHealth()
    {
        StopAllCoroutines();
        health--;
        if (health == 0)
        {
            AudioSource.PlayClipAtPoint(enemyDead, transform.position);
            player.GetComponent<PlayerController>().score = 
                player.GetComponent<PlayerController>().score + enemyScore;
            player.GetComponent<PlayerController>().UpdateScore();
            StartCoroutine(Dead());
        }
        else
        {
            AudioSource.PlayClipAtPoint(enemyDamage, transform.position);
            StartCoroutine(Hit());
        }
    }

    private IEnumerator Walk1()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = walk1;
        yield return new WaitForSeconds(.5f);
        StartCoroutine(Walk2());
    }

    private IEnumerator Walk2()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = walk2;
        yield return new WaitForSeconds(.5f);
        if (!_hit && !_dead)
        StartCoroutine(Walk1());
    }

    private IEnumerator Hit()
    {
        _hit = true;
        gameObject.GetComponent<SpriteRenderer>().sprite = hit;
        yield return new WaitForSeconds(.5f);
        _hit = false;
        StartCoroutine(Walk1());
    }

    private IEnumerator Dead()
    {
        _dead = true;
        box.enabled = false;
        gameObject.GetComponent<SpriteRenderer>().sprite = dead;
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
