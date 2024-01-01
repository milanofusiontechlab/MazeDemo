using UnityEngine;
using UnityEngine.AI;

public class Sphere : MonoBehaviour
{/*
    public enum SphereType
    {
        Red,
        Blue,
        BOX
    }
    public SphereType type;*/
    private NavMeshAgent agent;
    private float Speed=25f;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
       /* if (type == SphereType.Red)
        {
            Speed = Random.Range(11f, 20f);
        }
        else if (type == SphereType.Blue)
        {
            Speed = Random.Range(5f, 10f);
        }
        else if (type == SphereType.BOX)
        {
            Speed = Random.Range(21f, 40f);
        }*/

        agent.speed = Speed;
        SetRandomDestination();
    }

    void Update()
    {
     
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            SetRandomDestination();
        }
        if (!GameManager.Instance.GamePlay)
        {
            Destroy(this.gameObject);
        }
    }

    void SetRandomDestination()
    {
        Vector3 randomPoint;
        NavMeshHit hit;

        do
        {
            randomPoint = Random.insideUnitSphere * 20f;
            randomPoint.y = 0;

            NavMesh.SamplePosition(randomPoint, out hit, 20f, NavMesh.AllAreas);
        } while (Vector3.Distance(agent.destination, hit.position) < 5f);

        agent.SetDestination(hit.position);
    }

}
