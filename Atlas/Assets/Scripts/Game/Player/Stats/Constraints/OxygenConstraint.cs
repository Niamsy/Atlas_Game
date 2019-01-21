﻿public class OxygenConstraint : Constraint<float>
{

    public OxygenConstraint()
    {
        max = 100;
        current = max;
    }

    public void Update(double deltaTime)
    {
        //Operate each frame on the value of the Current Constraint
    }

    public void give(float qte)
    {
        current += qte;
    }

    public bool consume(float qteToConsume)
    {
        current -= qteToConsume;
        return isEmpty();
    }
    public bool isEmpty()
    {
        if (current <= 0)
        {
            return true;
        }
        return false;
    }
}