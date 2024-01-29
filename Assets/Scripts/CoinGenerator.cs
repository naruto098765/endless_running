using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class CoinGenerator : MonoBehaviour
{
    //public TextMeshPro coin_text;
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }


}
