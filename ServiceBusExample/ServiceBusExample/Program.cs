// See https://aka.ms/new-console-template for more information
using SbHelper;

Console.WriteLine("Hello, World!");

var person = new
{
    firstName = "Ravi",
    lastName = "Teja"
};
ServiceBusHelper serviceBusHelper = new ServiceBusHelper(); 
await serviceBusHelper.SendMessageSync(person,"persons");