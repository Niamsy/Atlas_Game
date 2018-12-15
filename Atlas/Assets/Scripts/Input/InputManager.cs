using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InputManagement
{
    public class InputManager : MonoBehaviour
    {
        public InputAxis[] _Axises;
        public InputKey[] _Keys;

        private void Awake()
        {
            cInput.Init();
            ////cInput.Clear();

            foreach (InputKey key in _Keys)
            {
                key.Set();
            }

            foreach (InputAxis axis in _Axises)
            {
                axis.Set();
            }
        }

    }
}