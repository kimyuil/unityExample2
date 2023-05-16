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


    private int increaseCount = 5; // �������� �þ �޸� Ǯ ����
    private int maxCount; //  ���� ��ϵ� ������Ʈ ����
    private int activeCount; // Ȱ��ȭ�Ǿ��ִ� ������Ʈ ����.


    private GameObject poolObject; // ������ ������

    private List<PoolItem> poolItemList; // �������� �޸�Ǯ..

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
        // 5�� ����� active false �ؼ� �÷����� �����̱���. �ϴ� �����ΰ� �Ⱥ��̰�.
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
        // ���� ��ȯ�ǰų� �׷��� ���� �� ��������Ҷ�.
        if (poolItemList == null) return;
                
        foreach (var poolItem in poolItemList)
        {
            GameObject.Destroy(poolItem.gameObject);
        }

        poolItemList.Clear();
    }

    public GameObject ActivatePoolItem(Vector2 startPosition)
    {
        // ����Ʈ�� �ִ� ���� ��Ȱ��ȭ�� ������ �ϳ��� ������ active�ϰ� ����.
        if (poolItemList == null) return null;


        if(maxCount == activeCount) // ������ �����ϸ� �� �ø���..
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
