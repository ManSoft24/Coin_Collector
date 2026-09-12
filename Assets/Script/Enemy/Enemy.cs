using System;
using System.Collections;
using UnityEngine;
using UnityRandom = UnityEngine.Random;






public class Enemy : MonoBehaviour, IDamageable
{

    public static event Action<Enemy> OnEnemyMati;
    
    [Header("Pengaturan State Machine")]
    [SerializeField] private float jarakDeteksi = 6f; // masuk CHASE
    [SerializeField] private float jarakSerang = 1.2f; // masuk ATTACK
    [SerializeField] private float jedaSerang = 1f; // detik antar serang
    [SerializeField] private float radiusPatrol = 3f;
    [SerializeField] public int damageSaatTabrakan = 20;
    [SerializeField] public int hp = 100;



    private Vector2 titikAwal; // pusat area keliling
    private Vector2 tujuanPatrol; // titik yang sedang dituju

    // state sekarang -- mulai dari IDLE
    private StateZombie state = StateZombie.IDLE;
    private float waktuSerangTerakhir;

    


    public float ms = 2f;


    protected Transform player;

    private Rigidbody2D rb;

    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }

        titikAwal = transform.position;
        PilihTujuanPatrolBaru();
    }

    void PilihTujuanPatrolBaru()
    {
        Vector2 acak = UnityRandom.insideUnitCircle * radiusPatrol;
        tujuanPatrol = titikAwal + acak;
    }


    void Update()
    {

        // LANGKAH A: tentukan state (aturan pindah)
        PeriksaTransisi();
        // LANGKAH B: jalankan perilaku sesuai state sekarang
        switch (state)
        {
            case StateZombie.IDLE: PerilakuIdle(); break;
            case StateZombie.PATROL: PerilakuPatrol(); break;
            case StateZombie.CHASE: PerilakuChase(); break;
            case StateZombie.ATTACK: PerilakuAttack(); break;
        }

    }
    

    public void Kejar()
    {
        if(player == null) return;

        transform.position = Vector2.MoveTowards(
            transform.position,
            player.position,
            ms * Time.deltaTime
        );
    }

    public void KenaDamage(int damage)
    {
        hp -= damage;
        Debug.Log("Enemy kena damage! " + damage + " ,HP sekarang: " + hp);

        if (hp <= 0)
        {
            Mati();
        }
    }

    protected virtual void Mati()
    {
        Debug.Log("Enemy mati!");
        OnEnemyMati?.Invoke(this);
        Destroy(gameObject);
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            IDamageable playerScript = other.GetComponent<IDamageable>();
            if (playerScript != null)
            {
                playerScript.KenaDamage(damageSaatTabrakan);
            }
        }
    }

    public virtual void Serang()
    {
        Debug.Log("Enemy Menyerang!");
    }


    float JarakKePlayer()
    {
        if (player == null) return Mathf.Infinity;
        return Vector2.Distance(transform.position, player.position);
    }


    void PerilakuIdle()
    {
        Debug.Log("Enemy Idle");
    }

    void PerilakuPatrol()
    {
        rb.MovePosition(Vector2.MoveTowards(
            transform.position, tujuanPatrol, ms * Time.deltaTime));

        if (Vector2.Distance(transform.position, tujuanPatrol) < 0.1f)
            PilihTujuanPatrolBaru();
    }

    void PerilakuChase() 
    {
        Kejar();
    }

    void PerilakuAttack() 
    {
        // menyerang berkala, tidak tiap frame
        if (Time.time >= waktuSerangTerakhir + jedaSerang)
        {
            Serang(); // method dari OOP
            waktuSerangTerakhir = Time.time;
        }
    }

    void PeriksaTransisi()
    {
        float jarak = JarakKePlayer(); // sudah ada dari materi OOP!
        if (jarak <= jarakSerang)
            state = StateZombie.ATTACK; // sangat dekat -> serang
        else if (jarak <= jarakDeteksi)
            state = StateZombie.CHASE; // terlihat -> kejar
        else
            state = StateZombie.PATROL; // jauh -> keliling
    }

    


}
