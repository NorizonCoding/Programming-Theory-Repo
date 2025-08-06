using UnityEngine;
using UnityEngine.InputSystem;

namespace Vehicles
{
    [RequireComponent (typeof(Rigidbody))]
    // INHERITANCE
    public abstract class Vehicle : MonoBehaviour
    {
        protected float curSpeed;
        
        // ENCAPSULATION
        public float Speed { get { return curSpeed; } }

        [SerializeField] protected bool engineEnabled;

        // ENCAPSULATION
        public bool EngineStatus { get { return engineEnabled; } }

        protected Vector3 prevPosition;

        protected Vector3 spawnPosition;

        protected Quaternion spawnRotation;

        [SerializeField] protected VehicleData vehicleData;

        protected new Rigidbody rigidbody;

        protected virtual void Awake()
        {
            spawnPosition = transform.position;
            spawnRotation = transform.rotation;

            rigidbody = GetComponent<Rigidbody>();
            rigidbody.mass = vehicleData.mass;

            rigidbody.maxLinearVelocity = vehicleData.maxSpeed;


            prevPosition = transform.position;
        }

        // ABSTRACTION
        private void CalculateSpeed()
        {
            curSpeed = (transform.position - prevPosition).magnitude/Time.fixedDeltaTime;
            prevPosition = transform.position;
        }

        // POLYMORPHISM
        protected virtual void FixedUpdate()
        {
            CalculateSpeed();
        }

        // ABSTRACTION
        virtual protected void ToggleEngine()
        {
            engineEnabled = !engineEnabled;
        }

        // POLYMORPHISM // ABSTRACTION
        protected abstract void Move();

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Killzone"))
            {
                rigidbody.angularVelocity = Vector3.zero;
                rigidbody.linearVelocity = Vector3.zero;
                transform.SetPositionAndRotation(spawnPosition, spawnRotation);
            }
        }
    }
}
