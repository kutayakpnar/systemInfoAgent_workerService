using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace SystemInfoWorker
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Bash script çalıştırılıyor...");

                // Proje dizinindeki bash script'in yolunu alıyoruz
                string scriptPath = Path.Combine(Directory.GetCurrentDirectory(), "Scripts", "hardware_info.sh");

                // Bash script'ini çalıştırıyoruz ve çıktıyı alıyoruz
                string scriptOutput = RunBashScript(scriptPath);

                // Çıktıyı konsola basıyoruz
                _logger.LogInformation("Bash Script Output:\n" + scriptOutput);

                // 10 dakika bekleme (örneğin)
                await Task.Delay(600000, stoppingToken);
            }
        }

        private string RunBashScript(string scriptPath)
        {
            var process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "/bin/bash",
                    Arguments = scriptPath,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                }
            };

            process.Start();
            string result = process.StandardOutput.ReadToEnd();
            process.WaitForExit();

            if (process.ExitCode != 0)
            {
                string error = process.StandardError.ReadToEnd();
                _logger.LogError($"Script çalıştırma hatası: {error}");
            }

            return result;
        }
    }
}
