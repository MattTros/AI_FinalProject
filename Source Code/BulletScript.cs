using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Enemy" && gameObject.tag != "Enemy")
        {
            Destroy(gameObject);
        }
        if(collision.tag == "Wall")
        {
            Destroy(gameObject);
        }
        if (collision.tag == "FriendlyProjectile" && gameObject.tag != "FriendlyProjectile")
        {
            Destroy(gameObject);
        }
    }
}
