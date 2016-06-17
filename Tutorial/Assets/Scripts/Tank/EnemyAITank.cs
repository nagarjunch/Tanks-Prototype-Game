using UnityEngine;
using System.Collections;

public class EnemyAITank : MonoBehaviour {

    public Color m_PlayerColor;
    public GameObject m_Target;

    private NavMeshAgent m_navMeshAgent;

	// Use this for initialization
	void Start () {
        m_navMeshAgent = GetComponent<NavMeshAgent>();
        
        MeshRenderer[] renderers = GetComponentsInChildren<MeshRenderer>();
        for (int i = 0; i < renderers.Length; i++)
        {
            renderers[i].material.color = m_PlayerColor;
        }
    }
	
	// Update is called once per frame
	void Update () {
        m_navMeshAgent.SetDestination(m_Target.transform.position);
    }
}
