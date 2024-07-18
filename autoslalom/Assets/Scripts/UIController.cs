using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class UIController : MonoBehaviour
{
    [SerializeField] private TMP_InputField nickInput;
    [SerializeField] private Button mainMenuStartButton;
    [SerializeField] private Button mainMenuExitButton;
    [SerializeField] private Button lossMenuStartButton;
    [SerializeField] private Button lossMenuLeaveButton;
    [SerializeField] private Button lossMenuSaveResultButton;
    [SerializeField] private Button pauseMenuContinueButton;
    [SerializeField] private Button pauseMenuRestartButton;
    [SerializeField] private Button pauseMenuLeaveButton;
    [SerializeField] private MenuMover lossMenuMoving;
    [SerializeField] private MenuMover pauseMenuMoving;
    [SerializeField] private MenuMover mainMenuMoving;
    [SerializeField] private TMP_Text delayCounter;
    [SerializeField] private TMP_Text scoreText;
    private bool delayInProgress = false;
    private float continueDelay = 3f;
    private void Start()
    {
        EventBus.CameraStabilized += () => HandleUIState(lossMenuMoving);
        EventBus.ResultGotten += ShowScore;

        mainMenuExitButton.onClick.AddListener(Exit);
        mainMenuStartButton.onClick.AddListener(() => HandleUIState(mainMenuMoving, EventBus.GameStarted));

        lossMenuLeaveButton.onClick.AddListener(() => HandleUIState(mainMenuMoving));
        lossMenuLeaveButton.onClick.AddListener(() => HandleUIState(lossMenuMoving, EventBus.GameLeaved));
        lossMenuStartButton.onClick.AddListener(() => HandleUIState(lossMenuMoving, EventBus.GameStarted));
        lossMenuSaveResultButton.onClick.AddListener(() => HandleUIState(EventBus.PlayerGotten));

        pauseMenuRestartButton.onClick.AddListener(() => HandleUIState(pauseMenuMoving, EventBus.GameStarted));
        pauseMenuContinueButton.onClick.AddListener(() => HandleUIState(pauseMenuMoving, EventBus.GameContinued, continueDelay));
        pauseMenuLeaveButton.onClick.AddListener(() => HandleUIState(pauseMenuMoving, EventBus.GameLeaved));
        pauseMenuLeaveButton.onClick.AddListener(() => HandleUIState(mainMenuMoving));

        HandleUIState(mainMenuMoving);
    }
    private void Update()
    {
        if(delayInProgress || pauseMenuMoving.enabled || !Input.GetKeyDown(KeyCode.Escape))
        {
            return;
        }
        if (GameStateManager.Current == GameStates.Running)
        {
            HandleUIState(pauseMenuMoving, EventBus.GamePaused);
        }
        else if (GameStateManager.Current == GameStates.Paused)
        {
            HandleUIState(pauseMenuMoving, EventBus.GameContinued, continueDelay);
        }
    }
    private void ShowScore(int score)
    {
        scoreText.text = score.ToString();
    }
    private void HandleUIState(Action<string> action)
    {
        action?.Invoke(nickInput.text);
    }
    private void HandleUIState(MenuMover movingMenu)
    {
        movingMenu.enabled = true;
    }
    private void HandleUIState(MenuMover movingMenu, Action action)
    {
        movingMenu.enabled = true;
        action?.Invoke();
    }
    private void HandleUIState(MenuMover movingMenu, Action action, float delay)
    {
        delayInProgress = true;
        movingMenu.enabled = true;
        StartCoroutine(Delay(delay, action));
    }
    private IEnumerator Delay(float delay, Action action)
    {
        delayCounter.gameObject.SetActive(true);
        for (float i = 0f; i < delay; i++)
        {
            delayCounter.text = (delay - i).ToString();
            yield return new WaitForSeconds(delay/delay);
        }
        delayCounter.gameObject.SetActive(false);
        action?.Invoke();
        delayInProgress = false;
    }
    private void Exit()
    {
        Application.Quit();
    }
}
