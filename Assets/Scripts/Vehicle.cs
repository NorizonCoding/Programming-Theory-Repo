using UnityEngine;
using UnityEngine.InputSystem;

namespace Vehicles
{
    [RequireComponent (typeof(Rigidbody))]
    public abstract class Vehicle : MonoBehaviour
    {
        protected float curSpeed;
        public float Speed { get { return curSpeed; } }

        [SerializeField] protected bool engineEnabled;
        public bool EngineStatus { get { return engineEnabled; } }

        protected Vector3 prevPosition;

        protected Vector3 spawnPosition;
        protected Quaternion spawnRotation;

        [SerializeField] protected VehicleData vehicleData;

        protected new Rigidbody rigidbody;

        // Start is called once before the first frame Update
        protected virtual void Awake()
        {
            spawnPosition = transform.position;
            spawnRotation = transform.rotation;

            rigidbody = GetComponent<Rigidbody>();
            rigidbody.mass = vehicleData.mass;
            rigidbody.maxLinearVelocity = vehicleData.maxSpeed;

            prevPosition = transform.position;
        }

        // Called at the start of every fixed update
        private void CalculateSpeed()
        {
            curSpeed = (transform.position - prevPosition).magnitude/Time.fixedDeltaTime;
            prevPosition = transform.position;
        }

        protected virtual void FixedUpdate()
        {
            CalculateSpeed();
        }

        virtual protected void ToggleEngine()
        {
            engineEnabled = !engineEnabled;
        }

        protected abstract void Move();

        public abstract void EnableInput();
        public abstract void DisableInput();

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Killzone"))
            {
                transform.position = spawnPosition;
                transform.rotation = spawnRotation;
                rigidbody.linearVelocity = Vector3.zero;
                rigidbody.angularVelocity = Vector3.zero;
            }
        }
    }
}
