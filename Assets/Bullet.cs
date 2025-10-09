using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.tag);
        if (collision.gameObject.tag == "Lock")
        {
            Debug.Log("aaaa");
            collision.transform.root.GetComponent<DoorLocked>().Unlock();
            collision.transform.GetComponent<Rigidbody>().linearVelocity = transform.forward * 5f;
         }
        Destroy(gameObject);
    }
}
