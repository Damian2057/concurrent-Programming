using System.Collections.Concurrent;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Data
{
    internal class BallListLogger : BallApiListLogger
    {
        private readonly string logPath;
        private Task? LoggerTask;
        private readonly ConcurrentQueue<JObject> BallConcurrentQueue = new();
        private readonly Mutex queueMutex = new();
        private readonly JArray DataArray;
        private readonly Mutex fileMutex = new();

        public BallListLogger()
        {
            string PathToSave = Path.GetTempPath();
            logPath = PathToSave + "ballsLogs.json";

            //If file doesnt exists create new one.
            if (File.Exists(logPath))
            {
                try
                {
                    string input = File.ReadAllText(logPath);
                    DataArray = JArray.Parse(input);
                    return;
                }
                catch (JsonReaderException)
                {
                    DataArray = new();
                }
            }

            DataArray = new();
            File.Create(logPath);
        }

        ~BallListLogger()
        {
            fileMutex.WaitOne();
            fileMutex.ReleaseMutex();
        }

        public void AddLogToSave(BallInterface ball)
        {
            queueMutex.WaitOne();
            try
            {
                JObject itemToAdd = JObject.FromObject(ball);
                itemToAdd["Time"] = DateTime.Now.ToString("HH:mm:ss");
                BallConcurrentQueue.Enqueue(itemToAdd);

                if (LoggerTask == null || LoggerTask.IsCompleted)
                {
                    LoggerTask = Task.Factory.StartNew(this.LogToFile);
                }
            }
            finally
            {
                queueMutex.ReleaseMutex();
            }
        }

        private async Task LogToFile()
        {
            //Append logs until queue empty
            while (BallConcurrentQueue.TryDequeue(out JObject ball))
            {
                DataArray.Add(ball);
            }

            // Convert data to string and save it
            string output = JsonConvert.SerializeObject(DataArray);

            fileMutex.WaitOne();
            try
            {
                File.WriteAllText(logPath, output);
            }
            finally
            {
                fileMutex.ReleaseMutex();
            }
        }
    }
}
