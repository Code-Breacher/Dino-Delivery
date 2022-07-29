using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_Singelton : MonoBehaviour
{
    // Start is called before the first frame update
    public static Game_Singelton instance { get; private set; }
    AudioSource audioSource;
    private float score = 0;
    private int pizzasDelivered = 0;
    private float moneyGained = 0;
    private int raptor = 0;
    private int trex = 0;
    private int tri = 0;

    void Awake()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PlaySound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }

    public float GetScore()
    {
        return score;
    }

    public void ChangeScore(float Stmp, float Mtmp, int Ptmp)
    {
        score += Stmp;
        pizzasDelivered += Ptmp;
        moneyGained += Mtmp;
    }
    
    public int GetPizzas()
    {
        return pizzasDelivered;
    }

    public float GetMoney()
    {
        return moneyGained;
    }

    public void ChangeDinos(int tmp)
    {
        if (tmp == 1)
        {
            raptor += 1;
        }
        if (tmp == 2)
        {
            trex += 1;
        }
        if (tmp == 3)
        {
            tri += 1;
        }
    }

    public int GetRaptor()
    {
        return raptor;
    }

    public int GetTrex()
    {
        return trex;
    }

    public int GetTri()
    {
        return tri;
    }

    public void ResetScore()
    {
        score = 0;
        moneyGained = 0;
        pizzasDelivered = 0;
        raptor = 0;
        trex = 0;
        tri = 0;
    }
}
