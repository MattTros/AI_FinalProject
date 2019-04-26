using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileEnemy : MonoBehaviour {

    GameObject target;
    float speed;
    float maxAngularSpeed;

    Vector3 orientation = Vector3.up;

    // Use this for initialization
    void Start ()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        speed = 0.5f;
        maxAngularSpeed = 90.0f;

        Vector3 dir = target.transform.position - gameObject.transform.position;
        orientation = dir;
        UpdateOrientation();

        transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y * -1, transform.localScale.z);
    }
	
	// Update is called once per frame
	void Update ()
    {
        Vector3 pos = transform.position;
        Vector3 velocity = orientation * speed;
        pos += velocity * Time.deltaTime;

        transform.position = pos;

        Destroy(gameObject, 8.0f);
    }

    void UpdateOrientation()
    {
        Vector3 angle = new Vector3(0, 0, -Mathf.Atan2(orientation.x, orientation.y) * Mathf.Rad2Deg);
        transform.eulerAngles = angle;
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
