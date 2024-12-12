using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CoinController : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Coin collected!");
            other.GetComponent<PlayerController>().UpdateScore();
            GameObject.FindAnyObjectByType<GameManager>().OnCoinCollect();
            Destroy(gameObject);
        }
    }
}
