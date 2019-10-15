using UnityEngine;

namespace Tools
{
    public class DestroyAfter : MonoBehaviour
    {
        public int countDown = 3;
        
        private void Start()
        {
            Invoke(nameof(DestroyThisInstance), countDown);
        }

        private void DestroyThisInstance()
        {
            Destroy(gameObject);
        } 
    }
}
