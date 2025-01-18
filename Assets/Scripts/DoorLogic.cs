using UnityEngine;


public class DoorLogic : MonoBehaviour
{
    public GameObject key;
    public bool keyTaken = false;
    public Transform keyKeeper;

    private void Start()
    {
        keyTaken=false;
    }

    private void Update()
    {
        if (key == null && !keyTaken) KeyTaken();
    }

    public void KeyTaken()
    {
        keyTaken=true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (keyTaken && collision.transform.tag=="Player")
        {
            Destroy(gameObject);
        }
    }
}

