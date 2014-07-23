using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Configuration;
using System.IO;
using System.Threading;
namespace ILive555.Service
{

    public class LiveHelper
    {
        private Process Live555Process { get; set; }
        public static readonly string FilmDir = ConfigurationManager.AppSettings["FilmDir"];
        public string Live555Exe
        {
            get
            {
                return Path.Combine(FilmDir, "live555MediaServer.exe");
            }
        }
        /// <summary>
        /// 启动Live555如果服务没启动
        /// </summary>
        public void StartMediaServer()
        {
            //如果需要更新，则结束主程序
            Process[] proc = Process.GetProcessesByName("live555MediaServer");

            if (proc.Count() <= 0)
            {

                ThreadPool.QueueUserWorkItem((s) =>
                {
                    Process cmd = new Process();
                    cmd.StartInfo.FileName = Live555Exe;
                    cmd.StartInfo.UseShellExecute = false;
                    cmd.StartInfo.RedirectStandardInput = true;
                    cmd.StartInfo.RedirectStandardOutput = true;
                    cmd.StartInfo.CreateNoWindow = true;
                    cmd.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                    cmd.Start();
                    Live555Process = cmd;

                    var output = cmd.StandardOutput.ReadToEnd();

                    Console.WriteLine(output);
                });


            }
            else
            {
                Live555Process = proc[0];

            }
            Thread.Sleep(100);
            var list=ProcessPortUtil.GetTcpPortsBy((uint)Live555Process.Id);
            foreach (var it in list)
            {
                Console.WriteLine(it);
            }
        }
    }
}
