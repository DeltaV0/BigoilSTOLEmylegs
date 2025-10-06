using UnityEngine;

public class selector : MonoBehaviour
{
    public bool stopped;
    public GameObject gun;
    public GameObject[] gunUI;

    //public int[] Inventory;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(stopped){
            gun.SetActive(true);
            gunUI[0].SetActive(true);
            gunUI[1].SetActive(false);
        } else {
            gunUI[1].SetActive(true);
            gunUI[0].SetActive(false);
            gun.SetActive(false);
        }
        if(Input.mouseScrollDelta.y != 0){
            stopped = !stopped;
        }
    }
}
