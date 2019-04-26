using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineEnemy : MonoBehaviour {

    GameObject target;
    public GameObject projectile;
    Vector3 upVector = new Vector3(0.0f, 4.0f, 0.0f);
    float speed;
    float direction;
    float x;
    float rotationValue;

    // Use this for initialization
    void Start ()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        speed = 3.0f;
        x = 0.0f;
        rotationValue = 2.0f;
        StartCoroutine(ShotTimer());
    }
	
	// Update is called once per frame
	void Update ()
    {
        Vector3 movement = Vector3.zero;

        direction = 1 + Mathf.PerlinNoise(x, 0.5f) * 7.9f;
        x += 0.1f;

        switch ((int)direction)
        {
            case 1: // Up Left
                movement.x = -1;
                movement.y = 1;
                break;
            case 2: // Up
                movement.x = 0;
                movement.y = 1;
                break;
            case 3: // Up Right
                movement.x = 1;
                movement.y = 1;
                break;
            case 4: // Right
                movement.x = 1;
                movement.y = 0;
                break;
            case 5: // Down Right
                movement.x = 1;
                movement.y = -1;
                break;
            case 6: // Down
                movement.x = 0;
                movement.y = -1;
                break;
            case 7: // Down Left
                movement.x = -1;
                movement.y = -1;
                break;
            case 8: // Left
                movement.x = -1;
                movement.y = 0;
                break;
        }

        transform.position += (movement * speed) * Time.deltaTime;

        Vector3 dir = target.transform.position - gameObject.transform.position;
        dir = Vector3.Normalize(dir);

        transform.position += (dir * (speed / 2)) * Time.deltaTime;

        Rotate();
    }

    private void Rotate()
    {
        transform.Rotate(new Vector3(transform.rotation.x, transform.rotation.y, rotationValue));
    }

    private IEnumerator ShotTimer()
    {
        while(true)
        {
            yield return new WaitForSeconds(2.0f);
            Shoot();
        }
    }

    private void Shoot()
    {
        ///Up Shot
        GameObject tmpBullet = Instantiate(projectile, transform.position, transform.rotation);
        tmpBullet.GetComponent<Rigidbody2D>().AddForce((transform.rotation * upVector) * 100.0f);
        Destroy(tmpBullet, 3.0f);
        ///Right Shot
        GameObject tmpBullet2 = Instantiate(projectile, transform.position, transform.rotation);
        tmpBullet2.GetComponent<Rigidbody2D>().AddForce((transform.rotation * Vector3.right * 4) * 100.0f);
        Destroy(tmpBullet2, 3.0f);
        ///Left Shot
        GameObject tmpBullet3 = Instantiate(projectile, transform.position, transform.rotation);
        tmpBullet3.GetComponent<Rigidbody2D>().AddForce((transform.rotation * -Vector3.right * 4) * 100.0f);
        Destroy(tmpBullet3, 3.0f);
        ///Down Shot
        GameObject tmpBullet4 = Instantiate(projectile, transform.position, transform.rotation);
        tmpBullet4.GetComponent<Rigidbody2D>().AddForce((transform.rotation * -upVector) * 100.0f);
        Destroy(tmpBullet4, 3.0f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "FriendlyProjectile")
        {
            GameManager gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
            gm.SetScore(gm.GetScore() + 100);
            Destroy(gameObject);
        }
    }
}
