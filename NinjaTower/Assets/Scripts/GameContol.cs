using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameContol : MonoBehaviour
{
    public static GameContol instance;
    public GameObject gameOverText;
    public Text scoreText;
    public bool gameOver = false;
    public float scrollSpeed = -2f;
    public int score = 0;
    public bool isDead = false;

    
    
    public GameObject[] hearts;
    public int full_health;
    private int currnetHealth;

    public GameObject[] enemies;
    public bool isAttacking;
    private Animator nim;
    
    // Start is called before the first frame update
    void Awake()
    {
       
        nim = GameObject.Find("Player").GetComponent<Animator>();
        if (instance == null) // it means there no game control found
        {
            instance = this;
        }
        else if (instance != null)
        {
            Destroy(gameObject);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (gameOver == true && Input.GetButtonDown("Jump"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            GameObject.Find("Main Camera").GetComponent<AudioSource>().Play();
        }
        if (gameOver == true && Input.GetButtonDown("Cancel"))
        {
            SceneManager.LoadScene("Menu");
        }
        
    }
    public void BirdScore()
    {
        if (gameOver)
        {
            return;
        }
        GameObject.Find("Canvas").GetComponent<AudioSource>().Play();
        score++;
        scoreText.text = "Score: " + score.ToString();
        if (score % 5 == 0)
        {
            GameObject.Find("Sky").GetComponent<AudioSource>().Play();
        }
    }
    
    public void BirdDie()
    {
        gameOverText.SetActive(true);
        GameObject.Find("Player").GetComponent<AudioSource>().Play();
        GameObject.Find("Main Camera").GetComponent<AudioSource>().Stop();
        gameOver = true;
    }
    
    public void hit()
    {
        
        if (!gameOver)
        {
            if (GameObject.Find("Heart1") != null)
            {
                GameObject.Find("Heart1").SetActive(false);

            }
            else if (GameObject.Find("Heart2") != null)
            { GameObject.Find("Heart2").SetActive(false); }

            else if (GameObject.Find("Heart3") != null)
            { GameObject.Find("Heart3").SetActive(false); }
            else
            {
                nim.SetTrigger("Die");
                nim.SetTrigger("Dief");
                BirdDie();
            }
        }
        
    }
   public void enemy_die(string name)
    {
       
        GameObject.Find(name).SetActive(false);
        
        score = score + 10;
    }

}
