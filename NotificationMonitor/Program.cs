using NotificationMonitor.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotificationMonitor
{
    class Program
    {
        static void Main(string[] args)
        {

            TerminalsRepository terminalstatus = new TerminalsRepository();
            terminalstatus.CheckTerminalsStatus();

            Console.WriteLine("Hola Mundo");
            Console.Read();
        }
    }
}
