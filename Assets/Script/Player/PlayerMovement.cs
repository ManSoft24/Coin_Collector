using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class PlayerMovement : MonoBehaviour
{
    public float kecepatan = 5f; 
    public int nilaiKoin = 1;
    public int hpPlayer = 100;
  
    private Vector2 arahGerak;   // nilai dari action "Move" 

    private bool moving;

    public Animator anim;
  
    // Dipanggil OTOMATIS oleh komponen Player Input 
    // saat action "Move" pada asset InputSystem_Actions aktif. 
    // Nama method WAJIB: On + nama action  ->  OnMove 
    void OnMove(InputValue value) 
    { 
        // TODO: ambil nilai Vector2 dari input, simpan ke arahGerak 
        arahGerak = value.Get<Vector2>() ;  
    } 
  
    void Update() 
    { 
        // TODO: gerakkan objek memakai arahGerak. 
        // Ingat kalikan kecepatan DAN Time.deltaTime! 
        Vector3 arah = new Vector3(arahGerak.x, arahGerak.y, 0); 
        transform.position += arah * kecepatan * Time.deltaTime ; 

        Animate();

    }

    private void Animate()
    {
        if(arahGerak.magnitude > 0.1f || arahGerak.magnitude < -0.1f)
        {
            moving = true;
        }
        else
        {
            moving = false;
        }
        if (moving)
        {
            anim.SetFloat("X",arahGerak.x);
            anim.SetFloat("Y",arahGerak.y);
        }

        anim.SetBool("Moving",moving);
    }

    // Dipanggil otomatis saat Player menyentuh objek ber-Trigger 
    void OnTriggerEnter2D(Collider2D other)
    {
        // TODO: cek apakah yang disentuh punya tag "Coin" 
        if ( other.CompareTag("Coins") )
        {
            // TODO: hancurkan koin yang tersentuh 
            FindFirstObjectByType<ScoreManager>().TambahScore(nilaiKoin);

            Destroy(other.gameObject)
            ;
        }
    }

}
