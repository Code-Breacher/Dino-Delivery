using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TRex_Controller : MonoBehaviour
{
    // Start is called before the first frame update
    public float moveSpeed = 1.0f;
    public float DisToAttack = 5;
    public float health = 2;

    private float watch;
    private float watchDif = 3f;

    private GameObject Player;
    private Transform target;
    private Rigidbody2D rb;

    private Vector2 movement;
    private Vector2 distance;
    private Vector2 movementDirection;

    private Player_Controller pc;
    void Start()
    {
        Player = GameObject.FindWithTag("Player");
        target = Player.transform;
        rb = this.GetComponent<Rigidbody2D>();
        pc = Player.GetComponent<Player_Controller>();
        watch = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = target.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        distance = direction;
        if (((Mathf.Abs(distance.x) + Mathf.Abs(distance.y)) < DisToAttack) && pc.getBush())
        {
            rb.rotation = angle - 90;
        }
        direction.Normalize();
        movement = direction;
        watch -= Time.deltaTime;
    }

    void MoveCharacter(Vector2 direction)
    {
        //rb.MovePosition((Vector2)transform.position + (direction * moveSpeed * Time.deltaTime));
        rb.velocity = new Vector2(direction.x * moveSpeed * Time.deltaTime, direction.y * moveSpeed * Time.deltaTime);
    }

    void FixedUpdate()
    {
        //Debug.Log(Mathf.Abs(distance.x) + Mathf.Abs(distance.y));
        if (((Mathf.Abs(distance.x) + Mathf.Abs(distance.y)) < DisToAttack) && pc.getBush())
        {
                MoveCharacter(movement);
        }
        else
        {
            if (watch < 0)
            {
                watch = watchDif;
                CalculateDirection();
            }
            //MoveCharacter(new Vector2(0, 0));
        }
    }

    void CalculateDirection()
    {
        movementDirection = new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f));
        float angle = Mathf.Atan2(movementDirection.y, movementDirection.x) * Mathf.Rad2Deg;
        rb.rotation = angle - 90;
        movementDirection.Normalize();
        MoveCharacter(movementDirection);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Projectile")
        {
            health--;
            if (health <= 0)
            {
                Game_Singelton.instance.ChangeDinos(2);
                Vector3 position = new Vector3(Random.Range(14f, 268f), Random.Range(-29f, 229f), 0);
                Vector3 direction = target.position - position;
                while ((Mathf.Abs(direction.x) + Mathf.Abs(direction.y)) < DisToAttack)
                {
                    position = new Vector3(Random.Range(14f, 268f), Random.Range(-29f, 229f), 0);
                    direction = target.position - position;
                }


                rb.position = new Vector2(position.x, position.y);
            }
        }
    }
}
