using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    float speed = 3f;

    float angle = 0;

    [SerializeField]
    GameObject bullet;
    MemoryPool memoryPool;

    Movement2D movement2D;
       

    void Start()
    {
        movement2D = GetComponent<Movement2D>();
        memoryPool = new MemoryPool(bullet);
    }

    private void OnApplicationQuit()
    {
        memoryPool.DestroyObjects();
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        movement2D.move(new Vector3(x, y, 0) * Time.deltaTime * speed);

        if (Input.GetKey(KeyCode.Space))
        {
            //var bul = Instantiate(bullet, transform.position, Quaternion.identity);
            var bul = memoryPool.ActivatePoolItem(transform.position);

            float bulletX = Mathf.Cos(angle * Mathf.PI / 180);
            float bulletY = Mathf.Sin(angle * Mathf.PI / 180);
            angle += 10;

            bul.GetComponent<BulletScript>().Setup(new Vector3(bulletX, bulletY), 10, memoryPool);
            
        }

        if (Input.GetKey(KeyCode.Escape))
        {
            memoryPool.DeactivatePoolItems ();
        }
    }
}
