using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    // Start is called before the first frame update
    Vector3 direction;
    float speed;
    Movement2D movement2D;

    [SerializeField]
    Vector2 minLimit;

    [SerializeField]
    Vector2 maxLimit;

    MemoryPool memoryPool;

    public void Setup(Vector3 direction, float speed, MemoryPool memoryPool)
    {
        this.memoryPool = memoryPool;
        this.direction = direction;
        this.speed = speed;
        movement2D = GetComponent<Movement2D>();
    }

    public void StartPositionSetup(Vector2 position)
    {
        transform.position = position;        
    }

    // Update is called once per frame
    void Update()
    {
        movement2D.move(direction * Time.deltaTime * speed) ;

        if( transform.position.x < minLimit.x || transform.position.x > maxLimit.x)
        {
            memoryPool.DeactivatePoolItem(gameObject);
            transform.position = Vector2.zero;
        }

        if (transform.position.y < minLimit.y || transform.position.y > maxLimit.y)
        {
            memoryPool.DeactivatePoolItem(gameObject);
            transform.position = Vector2.zero;
        }
    }
}
