using TMPro;
using UnityEngine;

public class PointSystem : MonoBehaviour
{
    // going generic for now

    [SerializeField] public float points = 0; // remember to make this private

    [SerializeField] private TMP_Text points_Text;

    private void Start()
    {

    }
    private void Update()
    {
        points_Text.text = $"Points: {points:N0}";
    }

    public void AddPoint(float pointAmount)
    {
        points += (int)pointAmount;
    }
}
