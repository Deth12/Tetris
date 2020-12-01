using Tetris.StateMachine;
using UnityEngine;
using Tetris.UI;
using TMPro;

public class UIController : MonoBehaviour
{
    [Header("Pause")]
    [SerializeField] private UI_Screen _pauseScreen = default;
    [SerializeField] private UI_Button _pauseButton = default;
    [SerializeField] private UI_Button _resumeButton = default;

    [SerializeField] private TMP_Text _levelCounter = default;
    [SerializeField] private TMP_Text _linesCounter = default;
    [SerializeField] private TMP_Text _scoreCounter = default;

    private GameController _gameController;
    
    public void Initialize(GameController gameController)
    {
        _gameController = gameController;
        
        _gameController.OnGamePause += ShowPauseScreen;
        _gameController.OnGameUnpause += HidePauseScreen;

        SetupScreens();
    }

    private void SetupScreens()
    {
        _pauseScreen.SetupScreen();
    }

    private void ShowPauseScreen()
    {
        _pauseScreen.Show();
    }

    private void HidePauseScreen()
    {
        _pauseScreen.Hide();
    }

    public void UpdateLevelCounter(int value)
    {
        _levelCounter.text = value.ToString();
    }

    public void UpdateLinesCounter(int value)
    {
        _linesCounter.text = value.ToString();
    }

    public void UpdateScoreCounter(int value)
    {
        _scoreCounter.text = value.ToString().PadLeft(9, '0');
    }
}
