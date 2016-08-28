using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BallistaScript : MonoBehaviour {
    public float force = 100.0f;
    public float speed = 180.0f;
    public float turnSpeed = 360.0f;
    public float maxMotorTorque = 100.0f;
    public float maxBrakeTorque = 100.0f;

    public GameObject mainCamera;
    public GameObject boltPrefab;

    private List<WheelCollider> wheelColliders = new List<WheelCollider>();

    public Vector2 mouseSensitivity = new Vector2(1, 1);
    private Vector2 cameraRotation;

    public Vector2 input;
    public bool fireInput = false;
    public float boltVelocity = 50.0f;

    private Rigidbody body;

    public void Start() {
        Cursor.lockState = CursorLockMode.Locked;

        foreach (Transform child in transform.Find("Wheel Colliders")) {
            wheelColliders.Add(child.gameObject.GetComponent<WheelCollider>());
        }

        Vector3 rotation = mainCamera.transform.localRotation.eulerAngles;
        cameraRotation.x = rotation.x;
        cameraRotation.y = rotation.y;

        body = GetComponent<Rigidbody>();
    }

    public void Update() {
        input.x = Input.GetAxis("Horizontal");
        input.y = Input.GetAxis("Vertical");

        cameraRotation.x += mouseSensitivity.x * Input.GetAxis("Mouse X");
        cameraRotation.y += mouseSensitivity.y * Input.GetAxis("Mouse Y");

        fireInput = Input.GetMouseButtonDown(0);
    }

    public void FixedUpdate() {
        float motorInput = Mathf.Max(input.y, 0.0f);
        float brakeInput = Mathf.Max(-input.y, 0.0f);

        foreach (WheelCollider collider in wheelColliders) {
            float side = Mathf.Sign(collider.transform.localPosition.x);
            collider.steerAngle = side * Mathf.Rad2Deg * Mathf.Abs(input.x) * -Mathf.Atan2(collider.transform.localPosition.z, side * collider.transform.localPosition.x);
            collider.motorTorque = (-input.x * side + motorInput) * maxMotorTorque;
            collider.brakeTorque = brakeInput * maxBrakeTorque;
        }

        if (fireInput) {
            GameObject bolt = (GameObject) Instantiate(boltPrefab, transform.position + 1.0f * Vector3.up, mainCamera.transform.rotation);
            Rigidbody boltBody = bolt.GetComponent<Rigidbody>();
            boltBody.velocity = body.velocity + mainCamera.transform.forward * boltVelocity;
        }
    }

    public void LateUpdate() {
        Vector3 rotation = transform.rotation.eulerAngles;

        Quaternion xQuaternion = Quaternion.AngleAxis(rotation.y + cameraRotation.x, Vector3.up);
        Quaternion yQuaternion = Quaternion.AngleAxis(cameraRotation.y, Vector3.left);

        mainCamera.transform.localRotation = xQuaternion * yQuaternion;

        mainCamera.transform.position = transform.position;
        mainCamera.transform.Translate(new Vector3(0, 5, -10));
    }

    private static float ClampAngle(float angle, float min, float max) {
        if (angle < -360.0f) {
            angle += 360.0f;
        }

        if (angle > 360.0f) {
            angle -= 360.0f;
        }

        return Mathf.Clamp(angle, min, max);
    }
}
