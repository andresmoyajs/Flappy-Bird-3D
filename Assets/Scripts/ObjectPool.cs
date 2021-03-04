using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] private GameObject prefab;

    private Queue<GameObject> pool;
    // Start is called before the first frame update


    void Awake()
    {
        pool = new Queue<GameObject>();
        GrowPool();
    }

    public GameObject GetFromPool()
    {

        if (pool.Count <= 1)
        {
            GrowPool();
        }
        var nextObj = pool.Dequeue();
        nextObj.SetActive(true);
        return nextObj;
    }

    public void ReturnToPool(GameObject obj)
    {

        obj.SetActive(false);
        pool.Enqueue(obj);
     
    }

    private void GrowPool()
    {
        for (int i = 0; i<10; i++)
        {
            var newObj = Instantiate(prefab);
            newObj.SetActive(false);
            pool.Enqueue(newObj);
        }
    }




}
