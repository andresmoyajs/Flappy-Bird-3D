using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private float spawnRate;
    [SerializeField] private Vector2 gapRange;
    [SerializeField] private float xPos;
    [SerializeField] private float zPos;


    [SerializeField] private ObjectPool tunnelPool;


    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.OnPlayerDeath.AddListener(OnPlayerDeath);
        StartCoroutine(SpawnAsync());
    }

    private IEnumerator SpawnAsync()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnRate);

            var tunnels = tunnelPool.GetFromPool();
            var gapPosition = Random.Range(gapRange.x, gapRange.y);

            tunnels.transform.position = new Vector3(xPos, gapPosition,zPos);
 

        }
    }

    private void OnPlayerDeath()
    {
        StopAllCoroutines();
    }
  


}


