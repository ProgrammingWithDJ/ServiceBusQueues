# ServiceBusQueues
Service bus with Azure Functions

This contains two project one is serviceBusqueue which sends the message to the Azure Service Bus queue and then there's we JobProccessor which proccesses the incoming messages in the queue

We have used best practises to store connection string in appsettings and used dependecy injection whenever possible.
Also added the code to use managed Identity.
