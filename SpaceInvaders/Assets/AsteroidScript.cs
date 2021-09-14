using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidScript : MonoBehaviour
{
    public int pointValue;

    // Start is called before the first frame update
    void Start()
    {
        pointValue = 10;
    }

    // Update is called once per frame
    void Update()
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
