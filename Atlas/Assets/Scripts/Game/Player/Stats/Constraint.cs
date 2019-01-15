using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Constraint<T>
{
    protected T max;
    protected  T current;

    T getMax()
    {
        return max;
    }

    T getCurrent()
    {
        return current;
    }

    void setCurrent(T newCurrent)
    {
        current = newCurrent;
    }

    void setMax(T newMax)
    {
        max = newMax;
    }

}
