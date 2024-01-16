using UnityEngine;
using UnityEngine.AI;

public class CharacterFollowState : CharacterBaseState
{
    public const string FOLLOW_STATE_NAME = "follow_state";

    private NavMeshAgent _navMeshAgent;
    private Character _leader;

    public CharacterFollowState(NavMeshAgent navMeshAgent, Character leader) :
        base(FOLLOW_STATE_NAME)
    {
        _navMeshAgent = navMeshAgent;
        _leader = leader;
    }

    public override void EnterState(CharacterStateManager stateManager)
    {
        _navMeshAgent.SetDestination(_leader.transform.position);
    }

    public override void UpdateState(CharacterStateManager stateManager)
    {
        if (HasReachedDestination(_navMeshAgent) && _leader.CurrentStateName == CharacterIdleState.IDLE_STATE_NAME)
        {
            stateManager.ChangeState(new CharacterIdleState());
        }
        else
        {
            _navMeshAgent.SetDestination(_leader.transform.position);
        }
    }

    public override void ExitState(CharacterStateManager stateManager)
    {
        // don't stop character if next state if related with movement
        if (stateManager.NextState.Name != CharacterFollowState.FOLLOW_STATE_NAME &&
            stateManager.NextState.Name != CharacterLeadState.LEAD_STATE_NAME)
        {
            _navMeshAgent.velocity = Vector3.zero;
            _navMeshAgent.ResetPath();
        }
    }
}
