using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    public Vector3 forceVector;
    public float rotationSpeed;
    public float rotation;
    public float timer;

    public bool thrust;
    public float rotationBool;

    public bool thrustPressed;
    public bool rotatePressed;

    // Use this for initialization 
    void Start()
    {
        timer = 0.0f;
        thrust = false;
        rotationBool = 0.0f;
        thrustPressed = false;
        rotatePressed = false;
    }

    /* forced changes to rigid body physics parameters should be done through the FixedUpdate() 
    method, not the Update() method*/
    void FixedUpdate()
    {
        timer += Time.deltaTime;

        if (timer < 1.0f)
        {
            // Vector3 default initializes all components to 0.0f     
            forceVector.x = 0.0f;
            rotationSpeed = 0.0f;
            thrust = false;

            rotation = -90.0f;
            rotationBool = 0.0f;
        }
        else
        {
            // Vector3 default initializes all components to 0.0f     
            forceVector.x = 1000000000.0f;
            rotationSpeed = 1.0f;

            if (Input.GetKeyDown(KeyCode.W))
            {
                thrustPressed = true;
            }

            if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
            {
                rotatePressed = true;
            }


            // force thruster     
            if (Input.GetAxisRaw("Vertical") > 0 && thrustPressed == true)
            {
                thrust = true;
            }
            if (Input.GetAxisRaw("Horizontal") > 0 && rotatePressed == true)
            {
                Debug.Log("Rotation " + rotation);

                rotationBool = 1.0f;

            }
            else if (Input.GetAxisRaw("Horizontal") < 0 && rotatePressed == true)
            {
                Debug.Log("Rotation " + rotation);

                rotationBool = -1.0f;
            }
            else
            {
                rotationBool = 0.0f;
            }


            if(thrust)
            {
                GetComponent<Rigidbody>().AddRelativeForce(forceVector);
                thrust = false;
            }

            if(rotationBool > 0.1f || rotationBool < 0.1f)
            {
                rotation += rotationSpeed * rotationBool;
                Quaternion rot = Quaternion.Euler(new Vector3(0.0f, rotation, 0.0f));
                if (rotation > 0.01 || rotation < 0.01)
                {
                    GetComponent<Rigidbody>().MoveRotation(rot);
                }
                rotationBool = 0.0f;


                Debug.Log("Rotation " + rotation);

                //gameObject.transform.Rotate(0, 2.0f, 0.0f);
            }

        }

    }


    public GameObject bullet; // the GameObject to spawn

    // Update is called once per frame 
    void Update ()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            Debug.Log("Fire! " + rotation);

            // We don't want to spawn a bullet inside our ship, so some
            // simple trigonometry is done here to spawn the bullet at the
            // tip of where the ship is pointed
            Vector3 spawnPos = gameObject.transform.position;
            spawnPos.x += 1.5f * Mathf.Cos(rotation * Mathf.PI / 180);
            spawnPos.z -= 1.5f * Mathf.Sin(rotation * Mathf.PI / 180);

            //instatiate the bullet
            GameObject obj = Instantiate(bullet, spawnPos, Quaternion.identity) as GameObject;

            // get the Bullet Script Component of the new Bullet instance
            BulletScript b = obj.GetComponent<BulletScript>();

            // set the direction the Bullet will travel in
            Quaternion rot = Quaternion.Euler(new Vector3(0, rotation, 0));
            b.heading = rot;
        }
    }
}


