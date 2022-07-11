using UnityEngine;

public class PauseManager : MonoBehaviour
{
    [Header("UI pause panel")]
    [SerializeField] private GameObject pausePanel;

    [Header("Entities Container")]
    [SerializeField] AllEntitiesContainer allEntitiesContainer;

    private bool isGamePaused;
    private InputSystem playersInput;
    private UFO ufo;
    private ShipController playersShipController;

    private void Start()
    {
        GetComponents();
        isGamePaused = false;
        Pause(false);
    }

    private void Update()
    {
        if (playersInput.Pause)
        {
            isGamePaused = !isGamePaused;
            Pause(isGamePaused);
        }
    }

    private void GetComponents()
    {
        playersShipController = allEntitiesContainer.PlayersShipControllers;
        playersInput = playersShipController.gameObject.GetComponent<InputSystem>();
        ufo = allEntitiesContainer.UFO;
    }

    public void Pause(bool value)
    {
        OnOrOffPausePanel(value);
        StopOrContinueGame(value);
    }

    private void OnOrOffPausePanel(bool value)
    {
        pausePanel.SetActive(value);
    }

    private void StopOrContinueGame(bool value)
    {
        var timescale = value ? 0 : 1;
        Time.timeScale = timescale;
        playersShipController.enabled = !value;
        ufo.enabled = !value;
    }
}
