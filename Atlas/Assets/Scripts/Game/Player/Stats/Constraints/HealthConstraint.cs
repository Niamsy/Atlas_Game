public class HealthConstraint : Constraint<float>
{
    public HealthConstraint()
    {
        max = 200;
        current = max;
    }

    public void Update(double deltaTime)
    {
        consume((float)0.01);
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
