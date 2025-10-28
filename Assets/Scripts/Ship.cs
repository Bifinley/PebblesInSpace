using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Ship : MonoBehaviour
{
    [SerializeField] public float currentShipHealth = 100f;
    [SerializeField] public float maxShipHealth = 100f;

    [SerializeField] private Slider healthBarSlider;
    [SerializeField] private TMP_Text healthAmountText;

    private void Start()
    {
        currentShipHealth = maxShipHealth;
    }

    private void Update()
    {
        Die();
        UpdateHealthUI();

        if (currentShipHealth > maxShipHealth)
        {
            currentShipHealth = maxShipHealth;
        }
    }
    public void ShipHeal(float heal)
    {
        healthBarSlider.value += heal;
        currentShipHealth += heal;
        //Debug.Log($"Ship recovered {heal} health!");
    }
    public void TakeShipDamage(float damage)
    {
        healthBarSlider.value -= damage;
        currentShipHealth -= damage;

        if (currentShipHealth <= 0)
        {
            Debug.Log("You died.");
            currentShipHealth = 0;
            Die();
        }
        //Debug.Log($"Ship took {damage} damage!");
    }

    private void UpdateHealthUI()
    {
        healthAmountText.text = $"{currentShipHealth}/{maxShipHealth}";
    }

    public void Die()
    {
        GameManager.Instance.GameOver();
    }
}
