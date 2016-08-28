using UnityEngine;
using System.Collections;

public class BoltScript : MonoBehaviour {
    private Rigidbody body;
    private Collider boltCollider;

    public void Start() {
        body = GetComponent<Rigidbody>();
        boltCollider = GetComponentInChildren<Collider>();
    }

    public void OnCollisionEnter(Collision collision) {
        if (body != null) {
            BoltTargetScript targetScript = collision.collider.GetComponentInParent<BoltTargetScript>();

            if (targetScript != null) {
                transform.parent = targetScript.transform;
                Destroy(body);
                body = null;
                Destroy(boltCollider);
                boltCollider = null;
                Destroy(this);
            }
        }
    }
}
