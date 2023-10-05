using MassTransit;

namespace Consumer;

public class TestConsumer : IConsumer<TestCommand>
{
    private readonly ITypedClient _client;

    public TestConsumer(ITypedClient client)
    {
        _client = client;
    }

    public async Task Consume(ConsumeContext<TestCommand> context)
    {
        await _client.ApiCall();
    }
}

public class TestCommand
{
}

public interface ITypedClient
{
    Task ApiCall();
}

public class TypedClient : ITypedClient
{
    private readonly HttpClient _client;

    public TypedClient(HttpClient client)
    {
        _client = client;
    }

    public async Task ApiCall()
    {
        await _client.GetStringAsync("http://localhost:1080/test");
    }
}