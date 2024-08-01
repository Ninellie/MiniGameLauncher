using System;
using UnityEngine;

namespace DriverGame
{
    /// <summary>
    /// Data class containing the physics data of the vehicle.
    /// </summary>
    [Serializable]
    public class VehiclePhysics
    {
        [Tooltip("The gravity applied when the vehicle is grounded")]
        [SerializeField]
        private float gravity = 20;

        [Tooltip("The gravity applied when the vehicle is flying")]
        [SerializeField]
        private float fallGravity = 50;

        public float Gravity { get => gravity; set => gravity = value; }
        
        public float FallGravity { get => fallGravity; set => fallGravity = value; }
    }
}
