using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UdpPractice
{
    class ProgramEntry
    {
        static void Main(string[] args)
        {
            //Just to redistribute and pick between TCP and UDP
            ProgramTCP.MainTCP(args);
            //ProgramUDP.MainUDP(args);
        }

    }
}
