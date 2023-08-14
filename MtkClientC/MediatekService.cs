using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MtkClientC
{
    public class MediatekService
    {

        public List<byte[]> HandShake = new List<byte[]>() { new byte[] { 0xA0 }, new byte[] { 0x0A }, new byte[] { 0x50 }, new byte[] { 0x05 } };

    }
}


