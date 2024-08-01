using UnityEngine;

namespace DriverGame.Game
{
    public class Rotate : MonoBehaviour
    {
        [SerializeField, Tooltip("Rotation speed in degrees per second")]
        Vector3 speed = Vector3.zero;

        void Update()
        {
            transform.Rotate(speed * Time.deltaTime, Space.World);
        }
    }
}