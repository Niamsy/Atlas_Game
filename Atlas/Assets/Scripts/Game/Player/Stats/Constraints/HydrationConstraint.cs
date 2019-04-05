public class HydrationConstraint : Constraint<float>
{
    public HydrationConstraint()
    {
        max = 100;
        current = max;
    }

    public void Update(double deltaTime)
    {
        consume((float)5);
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
