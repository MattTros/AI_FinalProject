using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{

    Vector3 upVector = new Vector3(0.0f, 4.0f, 0.0f);
    Vector3 rightVector = new Vector3(4.0f, 0.0f, 0.0f);

    Vector3 mousePos;
    public Transform target; //Assign to the object you want to rotate
    Vector3 objectPos;
    float angle;

    public GameObject projectile;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        mousePos = Input.mousePosition;
        mousePos.z = 5.23f; //The distance between the camera and object
        objectPos = Camera.main.WorldToScreenPoint(target.position);
        mousePos.x = mousePos.x - objectPos.x;
        mousePos.y = mousePos.y - objectPos.y;
        angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));

        if (Input.GetKey(KeyCode.W))
        {
            transform.position += (transform.rotation * upVector) * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.position -= (transform.rotation * upVector) * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.position -= (transform.rotation * rightVector) * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.position += (transform.rotation * rightVector) * Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject tmpBullet = Instantiate(projectile, transform.position, transform.rotation);
            tmpBullet.GetComponent<Rigidbody2D>().AddForce((transform.rotation * upVector) * 100.0f);
            Destroy(tmpBullet, 3.0f);
        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy" || collision.tag == "EnemyProjectile")
        {
            SceneManager.LoadScene(2);
        }
    }
}
