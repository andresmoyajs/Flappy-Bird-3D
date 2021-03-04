using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExitScript : MonoBehaviour
{
	[SerializeField] Button Confirmar;

    private void Update()
    {
        Confirmar.onClick.AddListener(() => {
            Debug.Log("Saliste");
            Application.Quit();

        });
    }
}
