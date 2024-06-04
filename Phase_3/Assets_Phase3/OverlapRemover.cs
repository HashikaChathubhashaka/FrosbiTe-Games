using System;
using UnityEngine;

public class OverlapRemover : MonoBehaviour
{
    [SerializeField] private LayerMask collectableMask;
    [SerializeField] private LayerMask obstacleMask;
    private Collider[] hitColliders;
    [SerializeField] private string collectibleTag;
    [SerializeField] private string obstacleTag;
    
    private void Start()
    {
        hitColliders = Physics.OverlapSphere(transform.position, 0.5f, collectableMask);
    }

    private void OnTriggerEnter(Collider other)
    {
        //Collider[] hitColliders = Physics.OverlapSphere(transform.position, 0.5f, collectableMask);

        if (other.gameObject.CompareTag(collectibleTag))
        {
            other.gameObject.SetActive(false);
            Debug.Log("DisabledItem in OnCollisionEnter");
        }
    }

    private void OnCollisionEnter(Collision other) 
    {
        //Collider[] hitColliders = Physics.OverlapSphere(transform.position, 0.5f, collectableMask);
        if (other.gameObject.layer == LayerMask.NameToLayer("Obstacles"))
        {
            if (other.gameObject.CompareTag(collectibleTag))
            {
                other.gameObject.SetActive(false);
                Debug.Log("DisabledItem in OnTriggerEnter");

            }
        }
    }
    
    
}