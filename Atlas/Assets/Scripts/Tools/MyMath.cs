using UnityEngine;

namespace Tools
{
    public static class MyMath
    {
        public static int AddWithRetenue(ref int final, int add, int max)
        {
            final += add;
            var val = (final / max);
            final = final % max;
            return (val);
        }
        
        public static int AddWithRetenueFloat(ref float final, float add, float max)
        {
            final += add;
            var val = Mathf.FloorToInt(final / max);
            final = final % max;
            return (val);
        }
    }
}
