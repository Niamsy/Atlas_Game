using UnityEngine;

namespace Player.Scripts
{
    public class Spawner : MonoBehaviour
    {
        public Vector3 pos;

        public void Spawn()
        {
            Debug.Log("Spawn");
            gameObject.transform.position = pos;
        }
    }
}
