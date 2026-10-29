using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI; 
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    public TextMeshProUGUI healthText;
    public Slider healthSlider;

    public GameManager gameManager;

    void Start()
    {
        currentHealth = maxHealth;

        healthSlider.maxValue = maxHealth;
        healthSlider.value = currentHealth;
    }

    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("Enemy"))
        {
            Enemy enemy = other.GetComponent<Enemy>();
            TakeDamage(enemy.damageSaatTabrakan);
        }
    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;

        if (currentHealth < 0)
        {
            currentHealth = 0;
        }

        healthSlider.value = currentHealth;

        if (currentHealth == 0)
        {
            Debug.Log("Player has died!");

            gameManager.Kalah();
        }
    }

    void Update()
    {
        healthText.text = currentHealth.ToString() + '/' + maxHealth.ToString();
        
    }
}