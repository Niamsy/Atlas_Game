using UnityEngine;

namespace Player.Scripts
{
    public class Spawner : MonoBehaviour
    {
        public Vector3 pos;

        public void Spawn()
        {
            gameObject.transform.localPosition = pos;
        }
    }
}
