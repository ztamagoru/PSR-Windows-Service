using System;
using System.ComponentModel;
using System.Timers;
using System.IO;
using System.ServiceProcess;

namespace IncrementService
{
    public class IncrementService : ServiceBase
    {
        private Timer timer;
        private int counter;
        private string path = System.Reflection.Assembly.GetExecutingAssembly()
               .Location + @"\..\..\..\..\PSR-Windows-Service\Resources\counter.txt";

        public IncrementService()
        {
            this.ServiceName = "IncrementService";
            this.CanStop = true;
            this.CanPauseAndContinue = true;
            this.AutoLog = true;
        }

        protected override void OnStart(string[] args)
        {
            counter = 0;

            if (!File.Exists(path))
            {
                File.Create(path).Close();
            }

            timer = new Timer();

            timer.Interval = 1000;
            timer.Elapsed += new ElapsedEventHandler(OnTimer);
            timer.Start();
        }

        protected override void OnStop()
        {
            timer.Stop();
        }

        private void OnTimer(object sender, ElapsedEventArgs e)
        {
            counter++;

            using (StreamWriter sw = File.AppendText(path))
            {
                sw.WriteLine(counter);
            }
        }
    }
}
