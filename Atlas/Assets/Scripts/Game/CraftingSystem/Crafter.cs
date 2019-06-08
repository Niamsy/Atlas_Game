using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Game.Crafting
{
    public class Crafter : Menu.MenuWidget
    {
        [SerializeField]
        private RecipeBook _Book;

        public RecipeBook RecipeBook { get => _Book; private set { } }

        protected override void InitialiseWidget()
        {
            
        }

        // Start is called before the first frame update
        void Start()
        {
            if (_Book == null)
            {
                Debug.LogWarning("No Recipe book setup up on Crafter");
            }    
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}