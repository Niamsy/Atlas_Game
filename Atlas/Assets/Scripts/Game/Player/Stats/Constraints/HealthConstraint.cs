public class HealthConstraint : Constraint<float>
{
    public HealthConstraint()
    {
        Max = 200;
        Current = Max;
    }

    public void Update(double deltaTime)
    {
        Consume((float)0.00);
    }

    public void Give(float qte)
    {
        Current += qte;
    }

    public bool Consume(float qteToConsume)
    {
        if (Current > 0)
        {
            Current -= qteToConsume;
        }
        return IsEmpty();
    }
    public bool IsEmpty()
    {
        if (Current <= 0)
        {
            return true;
        }
        return false;
    }
}
