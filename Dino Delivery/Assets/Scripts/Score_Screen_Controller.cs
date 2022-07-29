using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Score_Screen_Controller : MonoBehaviour
{
    // Start is called before the first frame update
    private float Money;
    private int pizzas;
    private int raptor;
    private int trex;
    private int tri;

    public Text moneyText;
    public Text pizzaText;
    public Text raptorText;
    public Text trexText;
    public Text triText;

    public AudioClip Death_Clip;

    void Awake()
    {
        Game_Singelton.instance.PlaySound(Death_Clip);
    }

    void Start()
    {
        pizzas = Game_Singelton.instance.GetPizzas();
        Money = Game_Singelton.instance.GetMoney();
        raptor = Game_Singelton.instance.GetRaptor();
        trex = Game_Singelton.instance.GetTrex();
        tri = Game_Singelton.instance.GetTri();
        moneyText.text = "Money Gained: " + Money;
        pizzaText.text = "Pizzas Delivered: " + pizzas;
        raptorText.text = "Raptors Slain:" + raptor;
        trexText.text = "T-Rexs Slain:" + trex;
        triText.text = "Triceratops Slain:" + tri;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeScene()
    {
        Game_Singelton.instance.ResetScore();
        SceneManager.LoadScene("GameScene", LoadSceneMode.Single);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
