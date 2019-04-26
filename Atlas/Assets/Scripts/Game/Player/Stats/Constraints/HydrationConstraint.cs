public class HydrationConstraint : Constraint<float>
{
    public HydrationConstraint()
    {
        Max = 100;
        Current = Max;
    }

    public void Update(double deltaTime)
    {
        consume((float)0);
        //Operate each frame on the value of the Current Constraint
    }

    public void give(float qte)
    {
        Current += qte;
    }

    public bool consume(float qteToConsume)
    {
        if (Current > 0)
        {
            Current -= qteToConsume;
        }
        return isEmpty();
    }
    public bool isEmpty()
    {
        if (Current <= 0)
        {
            return true;
        }
        return false;
    }
}
