using UnityEngine.AI;

public abstract class CharacterBaseState
{
    public string Name { get; private set; }

    public CharacterBaseState(string name)
    {
        Name = name;
    }

    public abstract void EnterState(CharacterStateManager stateManager);
    public abstract void UpdateState(CharacterStateManager stateManager);
    public abstract void ExitState(CharacterStateManager stateManager);

    protected bool HasReachedDestination(NavMeshAgent navMeshAgent)
    {
        if (!navMeshAgent.pathPending)
        {
            if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
            {
                if (!navMeshAgent.hasPath || navMeshAgent.velocity.sqrMagnitude == 0f)
                {
                    return true;
                }
            }
        }

        return false;
    }
}
