using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemoryPool
{
    private class PoolItem
    {
        public bool isActive;
        public GameObject gameObject;
    }


    private int increaseCount = 5; // 동적으로 늘어날 메모리 풀 개수
    private int maxCount; //  현재 등록된 오브젝트 개수
    private int activeCount; // 활성화되어있는 오브젝트 개수.


    private GameObject poolObject; // 관리할 프리팹

    private List<PoolItem> poolItemList; // 실질적인 메모리풀..

    public MemoryPool(GameObject poolObject)
    {
        maxCount = 0;
        activeCount = 0;
        this.poolObject = poolObject;
        poolItemList = new List<PoolItem>();

        InstiateObjects();
    }

    public void InstiateObjects()
    {
        // 5개 만들고 active false 해서 늘려놓는 개념이구나. 일단 만들어두고 안보이게.
        maxCount += increaseCount;

        for ( int i = 0; i < increaseCount; i++)
        {
            PoolItem poolItem = new PoolItem();
            poolItem.isActive = false;
            poolItem.gameObject = GameObject.Instantiate(poolObject);
            /*poolItem.gameObject = GameObject.Instantiate(poolObject, Vector2.zero, Quaternion.identity);*/
            poolItem.gameObject.SetActive(false);

            poolItemList.Add(poolItem);
        }

    }

    public void DestroyObjects()
    {
        // 씬이 전환되거나 그렇게 정말 다 사라져야할때.
        if (poolItemList == null) return;
                
        foreach (var poolItem in poolItemList)
        {
            GameObject.Destroy(poolItem.gameObject);
        }

        poolItemList.Clear();
    }

    public GameObject ActivatePoolItem(Vector2 startPosition)
    {
        // 리스트에 있는 것중 비활성화된 아이템 하나를 꺼내서 active하고 리턴.
        if (poolItemList == null) return null;


        if(maxCount == activeCount) // 개수가 부족하면 더 늘리고..
        {
            InstiateObjects();
        }

        foreach(var item in poolItemList)
        {
            if (!item.isActive)
            {
                activeCount++;
                item.isActive = true;
                item.gameObject.GetComponent<BulletScript>().StartPositionSetup(startPosition);
                item.gameObject.SetActive(true);                
                return item.gameObject;
            }
        }

        return null;
    }

    public void DeactivatePoolItem(GameObject removeObject)
    {
        if (poolItemList == null) return;

        foreach(var item in poolItemList)
        {
            if(item.gameObject == removeObject)
            {
                activeCount--;
                item.isActive = false;
                item.gameObject.SetActive(false);
            }
        }

    }

    public void DeactivatePoolItems()
    {
        foreach(var item in poolItemList)
        {
            if(item.gameObject != null && item.isActive)
            {
                item.isActive = false;
                item.gameObject.SetActive(false);
            }            
        }
        activeCount = 0;
    }

}
