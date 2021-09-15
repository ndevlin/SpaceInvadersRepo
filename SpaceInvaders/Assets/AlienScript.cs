using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienScript : MonoBehaviour
{

    public Vector3 forceVector;
    public int pointValue;
    public int timer;

    // Start is called before the first frame update
    void Start()
    {
        pointValue = 10;
        timer = 300;
        forceVector.x = 10.0f;

    }

    // Update is called once per frame
    void Update()
    {
        timer++;

        Vector3 currVec = ((float)Math.Sin((float)(timer) / 200.0)) * forceVector;

        GetComponent<Rigidbody>().AddRelativeForce(currVec);
    }


    void fixedUpdate()
    {
    }


    public GameObject deathExplosion;

    public AudioClip deathKnell;

    public void Die()
    {
        Debug.Log("Dying");

        AudioSource.PlayClipAtPoint(deathKnell, gameObject.transform.position);

        Instantiate(deathExplosion, gameObject.transform.position, Quaternion.AngleAxis(-90, Vector3.right));

        GameObject obj = GameObject.Find("GlobalObject");
        Global g = obj.GetComponent<Global>();
        g.score += pointValue;


        // Destroy removes the gameObject from the scene and marks it for garbage collection
        Destroy(gameObject);
    }
}
