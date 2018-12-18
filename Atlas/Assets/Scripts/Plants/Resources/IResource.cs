using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Plants
{
    public interface IResource<T>
    {
        T Create();
    }
}