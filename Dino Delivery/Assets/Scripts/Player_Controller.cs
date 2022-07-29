using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player_Controller : MonoBehaviour
{
    // Start is called before the first frame update
    private SpriteRenderer sr;
    public Sprite Spear_PizzaSprite;
    public Sprite SpearSprite;
    public Sprite PizzaSprite;
    public Sprite NietherSprite;
    public GameObject Compass;
    private bool noBush = false;
    private bool pizza = false;
    private float invincible = 0f;
    private bool Ratbag = false;
    private bool Rebekka = false;
    private bool Jimbo = false;
    private bool Elizitha = false;
    private bool Fred = false;
    private bool Dave = false;
    private int pizzasDelivered = 0;
    private float MoneyGained = 0;
    private Vector3 targetPosition;
    private RectTransform Compassrt;

    private float tips = 1;

    private bool UpgradeBox = true;
    private bool UpgradeScreenUp = false;
    public GameObject UpgradeScreen; 

    public Text nameText;
    public Text scoreText;
    public Text spearText;
    public Text tipsText;
    private int spears;
    public GameObject shootPosition;
    private float watch;
    private float watchDif = 3f;

    public AudioClip Ratbag_Clip;
    public AudioClip Rebekka_Clip;
    public AudioClip Jimbo_Clip;
    public AudioClip Elizitha_Clip;
    public AudioClip Fred_Clip;
    public AudioClip Dave_Clip;
    public AudioClip Grunt_Clip;
    public AudioClip Bell_Clip;
    public AudioClip Death_Clip;
    public AudioClip Pizza_Clip;
    public AudioClip Distract_Clip;

    public GameObject RatBag_Cave;
    public GameObject Rebekka_Cave;
    public GameObject Jimbo_Cave;
    public GameObject Elizitha_Cave;
    public GameObject Fred_Cave;
    public GameObject Dave_Cave;
    public GameObject Cheese_Box;



    //----movement
    public GameObject projectilePrefab;
    Rigidbody2D rb;
    float horizontal;
    float vertical;
    public float speed = 10;
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        watch = 0;
        spears = 3;
        spearText.text = "Spears: " + spears;
        UpgradeScreen.SetActive(false);
        Game_Singelton.instance.ChangeScore(700, 0, 0);
        scoreText.text = "$: " + Game_Singelton.instance.GetScore();
        sr = gameObject.GetComponent<SpriteRenderer>();
        Compassrt = Compass.GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        TurnCompass();
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        watch -= Time.deltaTime;
        invincible -= Time.deltaTime;
        if(watch < 0)
        {
            noBush = true;
        }

        if ((Input.GetKeyDown(KeyCode.C) || Input.GetButtonDown("Fire1")) && !UpgradeScreenUp) //Down
        {
            if (spears > 0)
            {
                spears--;
                spearText.text = "Spears: " + spears;
                Launch();
            }
        }
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (UpgradeBox)
            {
                if (!UpgradeScreenUp)
                {
                    UpgradeScreen.SetActive(true);
                    UpgradeScreenUp = true;
                }
                else
                {
                    UpgradeScreen.SetActive(false);
                    UpgradeScreenUp = false;
                }
            }
        }
        if (pizza && spears > 0)
        {
            sr.sprite = Spear_PizzaSprite;
        }
        else
        {
            if (pizza || spears > 0)
            {
                if (pizza)
                {
                    sr.sprite = PizzaSprite;
                }
                else
                {
                    sr.sprite = SpearSprite;
                }
            }
            else
            {
                sr.sprite = NietherSprite;
            }
        }
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontal * speed * Time.deltaTime, vertical * speed * Time.deltaTime);

        var direction = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
        var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle + -90, Vector3.forward);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Bush_Controller controller = other.gameObject.GetComponent<Bush_Controller>();

        if (controller != null)
        {
            noBush = false;
            watch = watchDif;
        }

        if (other.gameObject.name == "UpgradeHitBox")
        {
            UpgradeBox = true;
        }

    }

    void OnTriggerStay2D(Collider2D other)
    {
        Bush_Controller controller = other.gameObject.GetComponent<Bush_Controller>();

        if (controller != null)
        {
            noBush = false;
            watch = watchDif;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.name == "UpgradeHitBox")
        {
            UpgradeBox = false;
            UpgradeScreen.SetActive(false);
            UpgradeScreenUp = false;
        }
    }

        void OnCollisionEnter2D(Collision2D other)
    {
        //Bush_Controller controller = other.gameObject.GetComponent<Bush_Controller>();
        /*if (other.gameObject.tag == "Bush")
        {
            Physics2D.IgnoreCollision(other.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }*/
        if(other.gameObject.name == "Ratbag" && Ratbag && pizza)
        {
            nameText.text = "Get another Order";
            Game_Singelton.instance.ChangeScore(100 * tips, 100 * tips, 1);
            scoreText.text = "$: " + Game_Singelton.instance.GetScore();
            Game_Singelton.instance.PlaySound(Ratbag_Clip);
            pizzasDelivered += 1;
            MoneyGained += 100 * tips;
            ResetAll();
        }
        if (other.gameObject.name == "Rebekka" && Rebekka && pizza)
        {
            Game_Singelton.instance.ChangeScore(100 * tips, 100 * tips, 1);
            scoreText.text = "$: " + Game_Singelton.instance.GetScore();
            nameText.text = "Get another Order";
            Game_Singelton.instance.PlaySound(Rebekka_Clip);
            pizzasDelivered += 1;
            MoneyGained += 100 * tips;
            ResetAll();
        }
        if (other.gameObject.name == "Jimbo" && Jimbo && pizza)
        {
            Game_Singelton.instance.ChangeScore(100 * tips, 100 * tips, 1);
            scoreText.text = "$: " + Game_Singelton.instance.GetScore();
            nameText.text = "Get another Order";
            Game_Singelton.instance.PlaySound(Jimbo_Clip);
            pizzasDelivered += 1;
            ResetAll();
        }
        if (other.gameObject.name == "Elizitha" && Elizitha && pizza)
        {
            Game_Singelton.instance.ChangeScore(100 * tips, 100 * tips, 1);
            scoreText.text = "$: " + Game_Singelton.instance.GetScore();
            nameText.text = "Get another Order";
            Game_Singelton.instance.PlaySound(Elizitha_Clip);
            pizzasDelivered += 1;
            ResetAll();
        }
        if (other.gameObject.name == "Fred" && Fred && pizza)
        {
            Game_Singelton.instance.ChangeScore(100 * tips, 100 * tips, 1);
            scoreText.text = "$: " + Game_Singelton.instance.GetScore();
            nameText.text = "Get another Order";
            Game_Singelton.instance.PlaySound(Fred_Clip);
            pizzasDelivered += 1;
            ResetAll();
        }
        if (other.gameObject.name == "Dave" && Dave && pizza)
        {
            Game_Singelton.instance.ChangeScore(100 * tips, 100 * tips, 1);
            scoreText.text = "$: " + Game_Singelton.instance.GetScore();
            nameText.text = "Get another Order";
            Game_Singelton.instance.PlaySound(Dave_Clip);
            pizzasDelivered += 1;
            ResetAll();
        }
        if (other.gameObject.name == "CheeseBox" && !pizza)
        {
            pizza = true;
            Game_Singelton.instance.PlaySound(Bell_Clip);
            int randomName = Random.Range(1, 7);
            if (randomName == 1)
            {
                nameText.text = "Deliver to: Ratbag the Curious";
                Ratbag = true;
            }
            if (randomName == 2)
            {
                nameText.text = "Deliver to: Rebekka";
                Rebekka = true;
            }
            if (randomName == 3)
            {
                nameText.text = "Deliver to: Jimbo";
                Jimbo = true;
            }
            if (randomName == 4)
            {
                nameText.text = "Deliver to: Elizitha";
                Elizitha = true;
            }
            if (randomName == 5)
            {
                nameText.text = "Deliver to: Fred";
                Fred = true;
            }
            if (randomName == 6)
            {
                nameText.text = "Deliver to: Dave";
                Dave = true;
            }
            //Assign Random Name
        }
        if(other.gameObject.tag == "Enemy")
        {
            if (pizza)
            {
                invincible = 3f;
                ResetAll();
                int clipRan = Random.Range(1, 3);
                if(clipRan == 1)
                {
                    Game_Singelton.instance.PlaySound(Pizza_Clip);
                }
                else
                {
                    Game_Singelton.instance.PlaySound(Distract_Clip);
                }
            }
            else
            {
                if(invincible < 0)
                {
                    //rb.position = new Vector2(-22.5f, -6.9f);
                    //Game_Singelton.instance.PlaySound(Death_Clip);
                    SceneManager.LoadScene("ScoreScene", LoadSceneMode.Single);
                    //gameObject.SetActive(false);
                }
            }
        }
    }

    void OnCollisionStay2D(Collision2D other) //Enter
    {
        if (other.gameObject.tag == "Enemy")
        {
            if (pizza)
            {
                invincible = 3f;
                ResetAll();
            }
            else
            {
                if (invincible < 0)
                {
                    //rb.position = new Vector2(-22.5f, -6.9f);
                    //Game_Singelton.instance.PlaySound(Death_Clip);
                    SceneManager.LoadScene("ScoreScene", LoadSceneMode.Single);

                    //gameObject.SetActive(false);
                }
            }
        }
    }

        public bool getBush()
    {
        return noBush;
    }

    void Launch()
    {
        GameObject projectileObject = Instantiate(projectilePrefab, shootPosition.transform.position, Quaternion.identity); //+ Vector2.right * 0.5f

        Spear_Projectile projectile = projectileObject.GetComponent<Spear_Projectile>();
        Vector2 lookDirection = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
        var angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;
        projectile.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        lookDirection.Normalize();
        projectile.Launch(lookDirection, 2000);
        Game_Singelton.instance.PlaySound(Grunt_Clip);
    }

    void ResetAll()
    {
        Ratbag = false;
        Rebekka = false;
        Jimbo = false;
        Elizitha = false;
        Fred = false;
        Dave = false;
        pizza = false;
        nameText.text = "Get another Order";
    }

    public void MoreSpears() 
    {
        if (Game_Singelton.instance.GetScore() >= 100)
        {
            spears += 3;
            spearText.text = "Spears: " + spears;
            Game_Singelton.instance.ChangeScore(-100, 0, 0);
            scoreText.text = "$: " + Game_Singelton.instance.GetScore();
        }
    }

    public void ExtraTips()
    {
        if (Game_Singelton.instance.GetScore() >= 100)
        {
            tips += .25f;
            Game_Singelton.instance.ChangeScore(-150, 0, 0);
            scoreText.text = "$: " + Game_Singelton.instance.GetScore();
            tipsText.text = "$ Multiplier: " + tips;
        }
    }

    public void TurnCompass()
    {
        if (Ratbag)
        {
            targetPosition = RatBag_Cave.transform.position;
        }
        else if (Rebekka)
        {
            targetPosition = Rebekka_Cave.transform.position;
        }
        else if (Jimbo)
        {
            targetPosition = Jimbo_Cave.transform.position;
        }
        else if (Elizitha)
        {
            targetPosition = Elizitha_Cave.transform.position;
        }
        else if (Fred)
        {
            targetPosition = Fred_Cave.transform.position;
        }
        else if (Dave)
        {
            targetPosition = Dave_Cave.transform.position;
        }
        else
        {
            targetPosition = Cheese_Box.transform.position;
        }

        Vector3 direction = targetPosition - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Compassrt.rotation = Quaternion.Euler(0, 0, angle - 90);
    }
}
