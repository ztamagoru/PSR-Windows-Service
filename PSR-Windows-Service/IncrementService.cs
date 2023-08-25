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

        public IncrementService()
        {
            this.ServiceName = "IncrementService";
            this.CanStop = true;
            this.CanPauseAndContinue = true;
            this.AutoLog = true;
        }

        protected override void OnStop()
        {
            timer.Stop();
        }

        private void OnTimer(object sender, ElapsedEventArgs e)
        {
            counter++;

            string path = System.Reflection.Assembly.GetExecutingAssembly()
               .Location + @"\..\..\..\..\PSR-Windows-Service\Resources\counter.txt";

            using (StreamWriter sw = File.AppendText(path))
            {
                sw.WriteLine(counter);
            }
        }
    }
}
