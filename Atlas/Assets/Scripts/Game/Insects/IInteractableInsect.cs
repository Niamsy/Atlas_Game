using Game.ResourcesManagement.Consumer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Insects
{
    public interface IInteractableInsect
    {
        void insectInteract(InsectAction action, InsectConsumer consumer);
    }
}
