using TMPro;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI countOfHealthText;
    [SerializeField] private AllEntitiesContainer container;

    private void Start()
    {
        var playerHealth = container.PlayersShipControllers.gameObject.GetComponent<PlayerHealth>();
        playerHealth.OnHitEvent += UpdateHealthText;
        UpdateHealthText(playerHealth.CurrentHealth);
    }

    private void UpdateHealthText(int currentHealth)
    {
        countOfHealthText.text = currentHealth.ToString();
    }
}
