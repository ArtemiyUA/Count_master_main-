using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Coin : MonoBehaviour
{
    public static int coin ;
    [SerializeField] TMP_Text coinText;


    private void Start()
    {
        //ResetScore();
        coin = PlayerPrefs.GetInt("Coin");
    }

    private void Update()
    {
        coinText.text = coin.ToString();
    }
   public void SetScore()
    {    
        PlayerPrefs.SetInt("Coin", coin);
    }

    public void ResetScore()
    {
        PlayerPrefs.SetInt("Coin", 0);
    }

}
