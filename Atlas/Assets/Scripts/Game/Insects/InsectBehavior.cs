using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Insects
{
    public class InsectBehavior : MonoBehaviour
    {
        [SerializeField]
        public InsectSystem insect;

        [SerializeField]
        public int          radius;
        protected bool      isActing;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}