using UnityEngine;

public class Gun : MonoBehaviour
{
    //public LineRenderer lr;
    [SerializeField] private selector halt;

    public Transform a;

    public GameObject bullet;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && halt.stopped)
        {
            /*RaycastHit hit;
            if (Physics.Raycast(a.position, a.forward, out hit, 25f))
            {
                lr.SetPosition(0, a.position);
                lr.SetPosition(1, hit.point);
                if (hit.transform.tag == "lock") {
                    Debug.Log("aaa");
                    hit.transform.root.GetComponent<DoorLocked>().Unlock();
                 }
             }*/

            GameObject bullet2 = Instantiate(bullet, a.position, a.rotation);
            bullet2.GetComponent<Rigidbody>().linearVelocity = a.forward * 10;

         }
    }
}
