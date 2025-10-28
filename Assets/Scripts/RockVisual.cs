using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RockVisual : MonoBehaviour
{
    [SerializeField] private Transform rockVisualTransform;
    [SerializeField] private TMP_Text currentRockHealthText;
    [SerializeField] private TMP_Text rewardedPointsText;
    [SerializeField] private Slider currentHealthSlider;
    [SerializeField] private Animator rockVisualAnimator;

    // For hiding when destroyed
    [SerializeField] private SpriteRenderer rockVisualSpriteRenderer;
    [SerializeField] private GameObject healthBarSliderVisual;

    private const string RockRewardedPointsOnDestroyed = "RockRewardedPointsOnDestroyed";

    private Rock rock;

    private float rockVisualLocalScale = 0.1f;
    private void Start()
    {
        rock = GetComponentInParent<Rock>();

        // based off damage determines the size of rock!
        // based off size determines the reward of rock!
        // I am rock.
        rockVisualTransform.localScale *= rock.rockDamage;
        rockVisualTransform.localScale *= rockVisualLocalScale;
        rock.maxRockHealth *= rockVisualTransform.localScale.x;
        
        currentHealthSlider.maxValue = rock.maxRockHealth;

        rock.currentRockHealth = rock.maxRockHealth;

        rock.rewardPoints = rock.maxRockHealth * 0.3f;

        rewardedPointsText.text = $"+{rock.rewardPoints:N0}p";
    }

    public void PlayRewardAnimation()
    {
        rockVisualSpriteRenderer.enabled = false;
        healthBarSliderVisual.SetActive(false);
        rockVisualAnimator.SetTrigger(RockRewardedPointsOnDestroyed);
        
    }

    private void Update()
    {
        currentRockHealthText.text = $"{rock.currentRockHealth:N0}/{rock.maxRockHealth}";
        currentHealthSlider.value = rock.currentRockHealth;
    }
}
