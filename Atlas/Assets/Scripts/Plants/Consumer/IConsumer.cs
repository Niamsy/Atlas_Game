using Variables;

namespace Plants
{
    public interface IConsumer
    {
        Resources Type { get; }

        int Rate { get; }

        int Quantity { get; }

        Stock Stock { get; }

        int StarvationTimeLimit { get; }

        bool Starved { get; }

        float Range { get; }

        void Consume();

        int Load(IProducer producer, int quantity);

    }
}
