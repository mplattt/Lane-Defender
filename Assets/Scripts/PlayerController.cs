using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Processors;

public class PlayerController : MonoBehaviour
{
    private PlayerInput myPlayerInput;
    private InputAction upAction;
    private InputAction downAction;
    private InputAction shootAction;
    [SerializeField] private Transform shootPoint;
    [SerializeField] private GameObject bullet;
    private Rigidbody2D rb;
    private bool top;
    private bool bottom;
    [SerializeField] private int lives;
    private bool dead;
    public int score;
    private int highScore;
    [SerializeField] private TMP_Text scoreCounter;
    [SerializeField] private TMP_Text livesCounter;
    [SerializeField] private TMP_Text gameOverText;
    [SerializeField] private TMP_Text newHighScoreText;
    private bool newHighScore;
    [SerializeField] private GameObject explosion;

    public AudioClip tankShoot;
    public AudioClip lifeLost;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        myPlayerInput = GetComponent<PlayerInput>();
        myPlayerInput.currentActionMap.Enable();
        upAction = myPlayerInput.currentActionMap.FindAction("MoveUp");
        downAction = myPlayerInput.currentActionMap.FindAction("MoveDown");
        shootAction = myPlayerInput.currentActionMap.FindAction("Fire");

        upAction.started += upAction_started;
        downAction.started += downAction_started;
        shootAction.started += shootAction_started;

        rb = gameObject.GetComponent<Rigidbody2D>();

        score = 0;
        UpdateScore();
        livesCounter.text = "Lives: " + lives;
    }

    private void upAction_started(InputAction.CallbackContext context)
    {
        if (!top)
        {
            moveUp();
        }
    }

    private void downAction_started(InputAction.CallbackContext context)
    {
        if (!bottom)
        {
            moveDown();
        }
    }

    private void shootAction_started(InputAction.CallbackContext context)
    {
        Shoot();
    }

    private void moveUp()
    {
        rb.linearVelocityY = 10;
    }

    private void moveDown()
    {
        rb.linearVelocityY = -10;
    }

    private void Shoot()
    {
        if (!dead)
        {
            AudioSource.PlayClipAtPoint(tankShoot, transform.position);
            Instantiate(explosion, new Vector2(shootPoint.transform.position.x, shootPoint.transform.position.y),
            Quaternion.identity);
            Instantiate(bullet, new Vector2(shootPoint.transform.position.x, shootPoint.transform.position.y),
            Quaternion.identity);
        }
        else
        {
            Respawn();
        }
    }

    private void GameOver()
    {
        dead = true;
        if (newHighScore)
        {
            newHighScoreText.text = "New High Score! <br>" + highScore;
        }
        gameOverText.text = "Game Over <br>Space to Restart";
        Time.timeScale = 0;
    }

    private void Respawn()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach(GameObject enemy in enemies)
        {
            Destroy(enemy);
        }
        score = 0;
        UpdateScore();
        dead = false;
        lives = 3;
        livesCounter.text = "Lives: " + lives;
        newHighScore = false;
        top = false;
        bottom = false;
        newHighScoreText.text = "";
        gameOverText.text = "";
        Time.timeScale = 1;
    }

    public void LoseLives()
    {
        AudioSource.PlayClipAtPoint(lifeLost, transform.position);
        lives--;
        livesCounter.text = "Lives: " + lives;
        if (lives == 0)
        {
            GameOver();
        }
    }

    public void UpdateScore()
    {
        if (score > highScore)
        {
            highScore = score;
            newHighScore = true;
        }
        scoreCounter.text = "Score: " + score + "<br>High Score: " + highScore;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Lane")
        {
            rb.linearVelocity = new Vector2(0, 0);
        }

        if (collision.tag == "Top")
        {
            top = true;
        }

        if (collision.tag == "Bottom")
        {
            bottom = true;
        }

        if (collision.gameObject.tag == "Enemy")
        {
            Destroy(collision.gameObject);
            LoseLives();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Top")
        {
            top = false;
        }
        
        if (collision.tag == "Bottom")
        {
            bottom = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
