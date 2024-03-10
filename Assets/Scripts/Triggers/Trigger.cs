using UnityEngine;

public abstract class Trigger: MonoBehaviour
{
    public string ActivatingColliderTag = "Player";
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(ActivatingColliderTag))
        {
            Process();
        }
    }

    protected abstract void Process();
}