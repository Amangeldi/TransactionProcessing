using Shared;
using System.Security.Cryptography.X509Certificates;

namespace Processing
{
    public static class ProcessExecutor
    {
        // Транзакции сгрупированные по запросам
        public static List<RequestDTO> Requests { get; set; } = new List<RequestDTO>();
        public static async Task Execute(ILogger logger)
        {
            while(true)
            {
                if (Requests.Any())
                {
                    // С каждого запроса выполняем одну транзакцию
                    List<Task> tasks = new List<Task>();
                    foreach(var request in Requests.ToList())
                    {
                        // имитируем обработку транзакции
                        tasks.Add(TransactionProcess());
                        request.TransactionsCount--;
                        // имитируем callback
                        logger.LogCritical($"[Client: {request.Client}] left {request.TransactionsCount} transactions");
                        if(request.TransactionsCount == 0)
                        {
                            // Если все транзакции клиента выполнены, удаляем запрос из очереди
                            Requests.Remove(request);
                        }
                    }
                    // Выполняем транзакции одного цикла параллельно
                    await Task.WhenAll(tasks);
                }
                await Task.Delay(3000);
            }
        }
        public static async Task TransactionProcess()
        {
            await Task.Delay(1000);
        }
    }
}
