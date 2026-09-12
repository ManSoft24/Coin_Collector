using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int totalKoin;

    public int koinTerkumpul;

    public GameObject WinCanvas;

    [SerializeField] private int totalNyawa = 100;
    private int jumlahZombieMati = 0;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        totalKoin = GameObject.FindGameObjectsWithTag("Coins").Length;

    }

    void OnEnable()
    {
        Enemy.OnEnemyMati += SaatEnemyMati;
    }

    void OnDisable()
    {
        Enemy.OnEnemyMati -= SaatEnemyMati;
    }

    void SaatEnemyMati(Enemy enemy)
    {
        jumlahZombieMati++;
        Debug.Log("GameManager dengar event. Zombie mati: " + jumlahZombieMati + " (" + enemy.name + ")");
    }


    void Update()
    {
        koinTerkumpul = FindFirstObjectByType<ScoreManager>().score;
         
        if (koinTerkumpul == totalKoin)
        {
            Menang();        
        }

    }

    void Menang()
    {

        WinCanvas.SetActive(true);
        Debug.Log("Kamu Menang!");
        Time.timeScale = 0f;
    }
}
