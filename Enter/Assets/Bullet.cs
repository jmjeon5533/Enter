using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : PlayerMove
{
    Vector3 direction;

    
    public void Shoot(Vector3 direction)
    {
        this.direction = direction;
        Destroy(gameObject, 2f);
    }
    
    void Start()
    {
        transform.eulerAngles = direction;
    }

    
    void Update()
    {
        transform.Translate((direction.normalized * Time.deltaTime) * 10);
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Wall"))
        {
            var playerScript = GameObject.Find("Player").
                GetComponent<PlayerMove>();

            playerScript.BulletWallParticle(gameObject.transform.position);
            Destroy(gameObject);
        }
    }
}
