using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChasingEnemy : MonoBehaviour {

    GameObject target;
    float speed;
    float maxAngularSpeed;

    Vector3 orientation = Vector3.up;
    // Use this for initialization
    void Start () {
        target = GameObject.FindGameObjectWithTag("Player");
        speed = 3.0f;
        maxAngularSpeed = 90.0f;
	}
	
	// Update is called once per frame
	void Update () {
        Chase();
	}

    void Chase()
    {
        Vector3 dir = target.transform.position - gameObject.transform.position;

        float angleBetween = Mathf.Acos(Vector3.Dot(dir.normalized, orientation.normalized)) * Mathf.Rad2Deg;
        float angularDisplacement = maxAngularSpeed * Time.deltaTime;

        if (angleBetween < angularDisplacement)
        {
            dir = Vector3.Normalize(dir);
        }
        else
        {
            dir.x = dir.x * Mathf.Cos(angularDisplacement * Mathf.Deg2Rad) - dir.y * Mathf.Sin(angularDisplacement * Mathf.Deg2Rad);
            dir.y = dir.x * Mathf.Sin(angularDisplacement * Mathf.Deg2Rad) + dir.y * Mathf.Cos(angularDisplacement * Mathf.Deg2Rad);

            Vector3 rightNormal = gameObject.transform.right;
            float dotDirRight = Vector3.Dot(dir, rightNormal);

            if (dotDirRight > 0)
            {
                dir = Quaternion.Euler(0, 0, -angularDisplacement) * orientation;
            }
            else if (dotDirRight < 0)
            {
                dir = Quaternion.Euler(0, 0, angularDisplacement) * orientation;
            }
            else
            {
                dir = Vector3.Normalize(dir);
            }
        }

        // Update orientation 
        orientation = dir;
        UpdateOrientation();

        Vector3 pos = transform.position;
        Vector3 velocity = orientation * speed;
        pos += velocity * Time.deltaTime;

        transform.position = pos;
    }

    void UpdateOrientation()
    {
        Vector3 angle = new Vector3(0, 0, -Mathf.Atan2(orientation.x, orientation.y) * Mathf.Rad2Deg);
        transform.eulerAngles = angle;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "FriendlyProjectile")
        {
            GameManager gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
            gm.SetScore(gm.GetScore() + 100);
            Destroy(gameObject);
        }
    }
}
