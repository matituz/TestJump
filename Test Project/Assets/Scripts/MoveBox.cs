using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBox : MonoBehaviour
{
    bool transformPlayer = false;
    GameObject Player;
    void Start()
    {
        
    }

    // Update is called once per frame
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name == "Player")
        {
            Player = collision.gameObject;
            Player.transform.parent = transform;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        Player.transform.parent = null;
    }
}
