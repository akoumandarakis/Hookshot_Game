using UnityEngine;
using System.Collections;

public class GroundScript : MonoBehaviour {

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.GetComponent<PlayerScript>())
        {
            other.gameObject.GetComponent<PlayerScript>().isGrounded = true;
        }
    }

    void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.GetComponent<PlayerScript>())
        {
            other.gameObject.GetComponent<PlayerScript>().isGrounded = false;
        }
    }

}
