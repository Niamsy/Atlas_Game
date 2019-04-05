public class SleepConstraint : Constraint<float>
{
    public SleepConstraint()
    {
        max = 100;
        current = max;
    }

    public void Update(double deltaTime)
    {
        consume((float)0.00);
        //Operate each frame on the value of the Current Constraint
    }

    public void give(float qte)
    {
        current += qte;
    }

    public bool consume(float qteToConsume)
    {
        if (current > 0)
        {
            current -= qteToConsume;
        }
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
