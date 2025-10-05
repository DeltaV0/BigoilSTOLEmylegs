using UnityEngine;

public class DoorLocked : MonoBehaviour
{
    public bool Locked;

    public Rigidbody rb;

    public GameObject Lock;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Locked = true;
        rb.isKinematic = true;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Unlock()
    {
        Lock.SetActive(false);
        Locked = false;
        rb.isKinematic = false;
    }
}
