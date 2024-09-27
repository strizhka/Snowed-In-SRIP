using Input;
using Zenject;

public class GameStateMachine
{
    private InputReaderSwitcher _inputReaderSwitcher;
    private GameState _currentState;

    public GameState CurrentState => _currentState;
    
    [Inject]
    public void Construct(InputReaderSwitcher inputReaderSwitcher)
    {
        _inputReaderSwitcher = inputReaderSwitcher;
    }

    // private void Start()
    // {
    //     ChangeState(GameState.Menu);
    // }

    public void ChangeState(GameState newState)
    {
        switch (newState)
        {
            case GameState.Gameplay:
                GameplayState();
                break;
            case GameState.Menu:
                MenuState();
                break;
        }
    }

    private void GameplayState()
    {
        _inputReaderSwitcher.SetActiveInputHandler(InputHandlerType.Gameplay);
        _currentState = GameState.Gameplay;
    }

    private void MenuState()
    {
        _inputReaderSwitcher.SetActiveInputHandler(InputHandlerType.UI);
    }
}