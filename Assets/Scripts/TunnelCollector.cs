using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TunnelCollector : MonoBehaviour
{

    [SerializeField] private ObjectPool pool;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Tunnel")){
            pool.ReturnToPool(other.gameObject);
        }

       
    }
}
