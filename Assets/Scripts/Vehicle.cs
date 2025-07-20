using UnityEngine;
using UnityEngine.InputSystem;

namespace Vehicles
{
    [RequireComponent (typeof(Rigidbody))]
    public abstract class Vehicle : MonoBehaviour
    {
        [SerializeField] protected float curSpeed;

        public float Speed { get { return curSpeed; } }

        [SerializeField] protected bool engineEnabled;

        protected Vector3 prevPosition;

        [SerializeField] protected VehicleData vehicleData;

        protected new Rigidbody rigidbody;

        // Start is called once before the first frame Update
        protected virtual void Awake()
        {
            rigidbody = GetComponent<Rigidbody>();
            rigidbody.mass = vehicleData.mass;
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
    }

    public interface IInput // Interface with input functions
    {
        /// <summary>
        /// Does all the math to move the player based on the player's input.
        /// </summary>
        void Move();
    }
}
