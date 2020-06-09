using System.Collections;
using UnityEngine;

namespace Tools
{
    public class DestroyAfter : MonoBehaviour
    {
        public int countDown = 3;
        
        private void Start()
        {
            StartCoroutine(DestroyThisInstance());
        }

        private IEnumerator DestroyThisInstance()
        {
            yield return new WaitForSecondsRealtime(countDown);
            var reach = gameObject.GetComponent<SetupReachPoint>();
            if (reach != null)
            {
                reach.init();
            }
            Destroy(gameObject);
        } 
    }
}
