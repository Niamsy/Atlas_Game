using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Constraint<T>
{
    protected T max;
    protected  T current;

    public T getMax()
    {
        return max;
    }

    public T getCurrent()
    {
        return current;
    }

    public void setCurrent(T newCurrent)
    {
        current = newCurrent;
    }

    public void setMax(T newMax)
    {
        max = newMax;
    }

}
