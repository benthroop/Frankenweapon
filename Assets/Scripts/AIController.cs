using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour 
{
	public UnityEngine.AI.NavMeshAgent agent { get; private set; }             // the navmesh agent required for the path finding
	public Transform target;                                    // target to aim for
	public Transform head;

	private void Start()
	{
		// get the components on the object we need ( should not be null due to require component so no need to check )
		agent = GetComponentInChildren<UnityEngine.AI.NavMeshAgent>();
		agent.updateRotation = false;
		agent.updatePosition = true;
	}


	private void Update()
	{
		if (target != null)
		{
			agent.SetDestination(target.position);
		}

		head.LookAt(target);
	}


	public void SetTarget(Transform target)
	{
		this.target = target;
	}
}
