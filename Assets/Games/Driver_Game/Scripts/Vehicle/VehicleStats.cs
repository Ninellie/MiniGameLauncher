using UnityEngine;

namespace DriverGame
{
    /// <summary>
    /// Data class containing the main stats of the vehicle.
    /// </summary>
    [System.Serializable]
    public class VehicleStats
    {
        [Tooltip("The maximum speed forwards")]
        public float maxSpeed = 50;

        [Tooltip("How fast the vehicle accelerates.")]
        public float acceleration = 20;

        [Tooltip("How quickly the vehicle can turn left and right."), Range(0,3)]
        public float steeringPower = 1.5f;

        [Tooltip("How much side friction is applied"), Range(0,1)]
        public float grip = 1;
    }
}
