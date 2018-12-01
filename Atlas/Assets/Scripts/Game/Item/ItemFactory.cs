using UnityEngine;

namespace Game.Item
{
    public static class ItemFactory
    {
        private static ItemAbstract[] _allItemsList;
        
        private static void Create()
        {
            _allItemsList = Resources.LoadAll<ItemAbstract>("Items/");
        }

        public static ItemAbstract GetItemForId(int id)
        {
            if (_allItemsList == null)
                Create();

            foreach (var item in _allItemsList)
                if (item.Id == id)
                    return (item);
            
            Debug.LogError("Object of id:" + id + " not found");
            return (null);
        }
        
    }
}
