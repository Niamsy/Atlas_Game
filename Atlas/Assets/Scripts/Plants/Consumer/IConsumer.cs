using Variables;

namespace Plants
{
    public interface IConsumer
    {
        Resources Type { get; }

        int Rate { get; }

        int Quantity { get; }

        Stock Stocks { get; }

        int StarvationTimeLimit { get; }

        bool Starverd { get; }

        FloatReference Range { get; }

        void Consume();

        int Load(IProducer producer, int quantity);

    }
}
