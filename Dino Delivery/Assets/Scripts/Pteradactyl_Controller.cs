using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pteradactyl_Controller : MonoBehaviour
{
    // Start is called before the first frame update
    private float watch;
    private float watchDif = 10f;
    public float moveSpeed = 100f;

    private Vector2 movementDirection;
    private Rigidbody2D rb;

    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        watch = 0;
    }

    // Update is called once per frame
    void Update()
    {
        watch -= Time.deltaTime;
        if (watch < 0)
        {
            watch = watchDif;
            CalculateDirection();
        }
    }

    void CalculateDirection()
    {
        movementDirection = new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f));
        float angle = Mathf.Atan2(movementDirection.y, movementDirection.x) * Mathf.Rad2Deg;
        rb.rotation = angle - 90;
        //movementDirection.Normalize();
        MoveCharacter(movementDirection);
    }

    void MoveCharacter(Vector2 direction)
    {
        //rb.MovePosition((Vector2)transform.position + (direction * moveSpeed * Time.deltaTime));
        rb.velocity = new Vector2(direction.x * moveSpeed * Time.deltaTime, direction.y * moveSpeed * Time.deltaTime);
    }
}
