using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spear_Projectile : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody2D rigidbody2d;
    float lifespan = 1.0f;
    //public AudioClip LightningClip;

    void Awake()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        lifespan -= Time.deltaTime;
        if (lifespan < 0)
        {
            Destroy(gameObject);
        }
    }

    public void Launch(Vector2 direction, float force)
    {
        //SoundPlay.instance.PlaySound(LightningClip);
        rigidbody2d.AddForce(direction * force);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        //we also add a debug log to know what the projectile touch
        //Debug.Log("Projectile Collision with " + other.gameObject);
        Destroy(gameObject);
    }
}
