using UnityEngine;

public class HitBox : MonoBehaviour
{
    public string collidesWithTag;

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Trigger");
        if (collision.gameObject.tag == collidesWithTag)
        {
            PlayerBehaviour player = GetComponent<PlayerBehaviour>();
            if (player != null)
                player.OnHit();
        }        
    }

    void OnTriggerEnter(Collider collision)
    {
        Debug.Log("Trigger");
        if (collision.gameObject.tag == collidesWithTag)
        {
            PlayerBehaviour player = GetComponent<PlayerBehaviour>();
            if (player != null)
                player.OnHit();
        }
    }

    void OnTriggerStay (Collider other)
    {
        Debug.Log ("A collider is inside the DoorObject trigger");
    }
    
    void OnTriggerExit (Collider other)
    {
        Debug.Log ("A collider has exited the DoorObject trigger");
    }
}
