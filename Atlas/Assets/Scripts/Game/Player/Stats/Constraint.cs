public class Constraint<T>
{
    protected T Max;
    protected  T Current;

    public T GetMax()
    {
        return Max;
    }

    public T GetCurrent()
    {
        return Current;
    }

    public void SetCurrent(T newCurrent)
    {
        Current = newCurrent;
    }

    public void SetMax(T newMax)
    {
        Max = newMax;
    }

}
