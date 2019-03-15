using UnityEngine;

namespace Plants
{
    [CreateAssetMenu(menuName = "Plant System/Resource")]
    public class Resources : ScriptableObject, IResource<Resources>
    {
        public Resources Create()
        {
            return CreateInstance<Resources>();
        }
    }
}
