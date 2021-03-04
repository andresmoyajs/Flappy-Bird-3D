using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TunnelController : MonoBehaviour
{
    [SerializeField] private float speed;
    // Start is called before the first frame update
    private void Start()
    {
        GameManager.Instance.OnPlayerDeath.AddListener(OnPlayerDeath);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.left * (speed * 3  * Time.deltaTime);
        // Destroy(gameObject, 15);
    }

    private void OnPlayerDeath()
    {
        speed = 0f;
    }
}
