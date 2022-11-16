// See https://aka.ms/new-console-template for more information
using Microsoft.Azure.ServiceBus;
using System.Text;

Console.WriteLine("Hello, World!");
QueueClient queueClient = new QueueClient("","persons");
MessageHandlerOptions handler = new MessageHandlerOptions(exceptionHandler)
{
    AutoComplete = true,
    MaxAutoRenewDuration = TimeSpan.FromSeconds(5),
    MaxConcurrentCalls = 1
};
queueClient.RegisterMessageHandler(handlePersons, handler) ;

Task exceptionHandler(ExceptionReceivedEventArgs arg)
{
    throw new NotImplementedException();
};
Console.ReadKey();
Task handlePersons(Message arg1, CancellationToken arg2)
{
    var json = Encoding.UTF8.GetString(arg1.Body);
    return Task.CompletedTask;
}