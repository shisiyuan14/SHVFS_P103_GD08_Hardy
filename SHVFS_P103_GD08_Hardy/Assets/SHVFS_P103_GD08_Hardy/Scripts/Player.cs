using System.Collections;
using System.Collections.Generic;
using System.Security;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float player_speed = 5, jump_height = 2;
    [SerializeField]

    private void Update()
    {
        

        if (Input.GetKey(KeyCode.W))
        {
            this.transform.Translate(Vector3.forward * player_speed * Time.deltaTime);
            Debug.Log(" w pushed ");
        }
        if (Input.GetKey(KeyCode.S))
        {
            this.transform.Translate(Vector3.forward * - player_speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.A))
        {
            this.transform.Translate(Vector3.right * - player_speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D))
        {
            this.transform.Translate(Vector3.right * player_speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D))
        {
            this.transform.Translate(Vector3.right * player_speed * Time.deltaTime);
        }
        if(Input.GetKey(KeyCode.Space))
        {
            this.transform.Translate(Vector3.down * -player_speed * Time.deltaTime);
        }
    }
    // make the player move forward, back, left, right
    

   
}
