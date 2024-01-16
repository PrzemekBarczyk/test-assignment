public class CharacterStateManager
{
    public CharacterBaseState CurrentState { get; private set; }
    public CharacterBaseState NextState { get; private set; }
    public CharacterBaseState PreviousState { get; private set; }

    public void ChangeState(CharacterBaseState newState)
    {
        PreviousState = CurrentState;
        NextState = newState;

        PreviousState?.ExitState(this);

        CurrentState = NextState;
        NextState = null;

        CurrentState?.EnterState(this);
    }
}
