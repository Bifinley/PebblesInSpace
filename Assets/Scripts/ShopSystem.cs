using System.Runtime.CompilerServices;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class ShopSystem : MonoBehaviour
{
    [SerializeField] private PointSystem pointSystem;
    [SerializeField] private Ship ship;
    [SerializeField] private Stats playerStats;

    [SerializeField] private TMP_Text healBuyButtonText;
    [SerializeField] private TMP_Text clickDamageBuyButtonText;

    // Payment Cost Amounts
    [SerializeField] private int healPaymentAmount = 30;
    [SerializeField] private float healPaymentAmountMult; // these are to store the multiplier for PaymentAmount

    [SerializeField] private int clickDamagePaymentAmount = 30;
    [SerializeField] private float clickDamagePaymentAmountMult; // these are to store the multiplier for PaymentAmount

    

    private void Start()
    {
        healBuyButtonText.text = $"Heal: {healPaymentAmount:N0}p";
        clickDamageBuyButtonText.text = $"Click Damage: {clickDamagePaymentAmount:N0}p";
    }

    public void HealBuyButton()
    {
        if (pointSystem.points >= healPaymentAmount && ship.currentShipHealth < ship.maxShipHealth)
        {
            pointSystem.points -= healPaymentAmount;

            if (ship.currentShipHealth < ship.maxShipHealth)
            {
                int healAmount = 10;
                ship.ShipHeal(healAmount);

                healPaymentAmountMult = healPaymentAmount; 
                healPaymentAmountMult *= 1.2f;
                healPaymentAmount = (int)healPaymentAmountMult;

                UpdateUIPrices();
            }
        }
        else
        {
            if (ship.currentShipHealth >= ship.maxShipHealth)
            {
                Debug.Log("You are full health.");
            }
            if (pointSystem.points < healPaymentAmount)
            {
                Debug.Log("You cannot afford this item.");
            }
        }
    }
    public void ClickDamageBuyButton()
    {
        if (pointSystem.points >= clickDamagePaymentAmount)
        {
            pointSystem.points -= clickDamagePaymentAmount;

            float clickDamageAmount = 1;
            playerStats.maxClickDamage += clickDamageAmount;

            clickDamagePaymentAmountMult = clickDamagePaymentAmount; // PaymentAmount is added to the Mult variable
            clickDamagePaymentAmountMult *= 1.2f; // then that mult variable is multplied by 1.2f
            clickDamagePaymentAmount = (int)clickDamagePaymentAmountMult; // then slapped right back into the original PaymentAmount

            UpdateUIPrices();
        }
        else
        {
            if (pointSystem.points < clickDamagePaymentAmount)
            {
                Debug.Log("You cannot afford this item.");
            }
        }

    }

    private void UpdateUIPrices()
    {
        healBuyButtonText.text = $"Heal: {healPaymentAmount:N0}p";
        clickDamageBuyButtonText.text = $"Click Damage: {clickDamagePaymentAmount:N0}p";
    }
}
