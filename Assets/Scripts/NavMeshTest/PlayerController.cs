using System;
using UnityEngine;
using UnityEngine.AI;

namespace NavMeshTest
{
    public class PlayerController : MonoBehaviour
    {
        private NavMeshAgent _navMeshAgent;
        private Camera _camera;
        
        private void Awake()
        {
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _camera = Camera.main;
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Debug.LogWarning("1");
                RaycastHit hit;
                Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
                Physics.Raycast(ray, out hit,
                    Mathf.Infinity);
                _navMeshAgent.destination = hit.point;
            }
        }
    }
}