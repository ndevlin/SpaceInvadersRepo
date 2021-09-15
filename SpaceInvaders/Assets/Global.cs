using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Global : MonoBehaviour
{
    public Vector3 originInScreenCoords;
    public int score;


    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        originInScreenCoords = Camera.main.WorldToScreenPoint(new Vector3(0, 0, 0));
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("escape"))
        {
            Application.LoadLevel("TitleScreen");
        }
    }
}
