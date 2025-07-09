using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CubeController : MonoBehaviour
{
    public Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        print("cube has been started");
    }

    // Update is called once per frame
    void Update()
    {
        //print("cube is updating");
        if (Input.GetKey(KeyCode.W))
        {
            //print("cube should be going forward");

            //float health = 100;
            //health += 10;
            //health = health + 10;
            // (0,0,0) + (0,0,1) = (0,0,1)
            transform.position += Vector3.forward * Time.deltaTime;
            //transform.position = transform.position +  Vector3.forward;
        }

    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    Debug.LogError(collision.gameObject);
    //}
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Deadzone")
        {
            Debug.LogError("Player is dead");
            SceneManager.LoadScene(0);
        }
    }

}
