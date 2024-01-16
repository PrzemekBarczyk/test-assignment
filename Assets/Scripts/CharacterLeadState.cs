using UnityEngine;
using UnityEngine.AI;

public class CharacterLeadState : CharacterBaseState
{
    public const string LEAD_STATE_NAME = "lead_state";

    private NavMeshAgent _navMeshAgent;
    private Vector3 _destination;

    public CharacterLeadState(NavMeshAgent navMeshAgent, Vector3 destination) :
        base(LEAD_STATE_NAME)
    {
        _navMeshAgent = navMeshAgent;
        _destination = destination;
    }

    public override void EnterState(CharacterStateManager stateManager)
    {
        _navMeshAgent.SetDestination(_destination);
    }

    public override void UpdateState(CharacterStateManager stateManager)
    {
        if (HasReachedDestination(_navMeshAgent))
            stateManager.ChangeState(new CharacterIdleState());
    }

    public override void ExitState(CharacterStateManager stateManager)
    {
        // don't stop character if next state if related with movement
        if (stateManager.NextState.Name != CharacterLeadState.LEAD_STATE_NAME &&
            stateManager.NextState.Name != CharacterFollowState.FOLLOW_STATE_NAME)
        {
            _navMeshAgent.velocity = Vector3.zero;
            _navMeshAgent.ResetPath();
        }
    }
}
