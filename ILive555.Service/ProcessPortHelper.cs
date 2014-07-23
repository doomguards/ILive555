using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace ILive555.Service
{
    public class ProcessPortUtil
    {

        #region Demo
            ////uint port = 1025;
            ////uint processorId = ProcessPortHelper.GetProcessIdByPort(TcpOrUdp.TcpType, port);
            ////Console.WriteLine("Port {0} is using by processor {1}",port,processorId);

            ////uint processorId1 = 1072;

            ////uint[] TcpPorts = new uint[100];
            ////uint count = ProcessPortHelper.GetAllPortByProcessId(TcpOrUdp.TcpType, processorId1, TcpPorts, (uint)TcpPorts.Length);
            ////Console.WriteLine("Processor {0} is using TCP port: ", processorId1);
            ////for (uint i = 0; i < count; ++i)
            ////{
            ////    Console.WriteLine(TcpPorts[i]);
            ////}

            ////uint[] UdpPorts = new uint[100];
            ////uint count1 = ProcessPortHelper.GetAllPortByProcessId(TcpOrUdp.UdpType, processorId1, UdpPorts, (uint)UdpPorts.Length);
            ////Console.WriteLine("Processor {0} is using UDP port: ", processorId1);
            ////for (uint i = 0; i < count1; ++i)
            ////{
            ////    Console.WriteLine(UdpPorts[i]);
            ////}
            ////Console.ReadKey();

        #endregion

        public static List<int> GetTcpPortsBy(uint pid)
        {
            var list = new List<int>();

            uint[] TcpPorts = new uint[100];
            uint count = ProcessPortHelper.GetAllPortByProcessId(TcpOrUdp.TcpType, pid, TcpPorts, (uint)TcpPorts.Length);
            Console.WriteLine("Processor {0} is using TCP port: ", pid);
            for (uint i = 0; i < count; ++i)
            {
                list.Add((int)TcpPorts[i]);
                Console.WriteLine(TcpPorts[i]);
            }
            return list;

        }
        /// <summary>
        /// 需要跟ProcessorPort.dll一起使用
        /// </summary>
        public enum TcpOrUdp
        {
            TcpType,
            UdpType
        };
        public class ProcessPortHelper
        {
            [DllImport("ProcessorPort.dll", CallingConvention = CallingConvention.StdCall)]
            public extern static uint GetProcessIdByPort(TcpOrUdp type, uint dwPort);

            [DllImport("ProcessorPort.dll", CallingConvention = CallingConvention.StdCall)]
            public extern static uint GetAllPortByProcessId(TcpOrUdp type, uint dwProcessId, uint[] dwAllPort, uint dwMaxLen);
        }

    }

}
