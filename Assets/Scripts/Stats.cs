using UnityEngine;

public class Stats : MonoBehaviour
{
    [SerializeField] private float currentClickDamage = 10;
    [SerializeField] public float maxClickDamage = 10;

    private void Update()
    {
        currentClickDamage = maxClickDamage;
    }
}
