namespace Plants
{
    public interface IResource<T>
    {
        T Create();
    }
}