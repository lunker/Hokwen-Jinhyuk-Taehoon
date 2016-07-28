using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.BitConverter;


namespace IRCC
{
    public struct Header
    {
        public short comm;
        public short code;
        public int size;
        public int reserved;
    }
    public struct Packet
    {
        public Header header;
        public byte[] body;
    }
    public static class IRCCHelper
    {
        public static byte[] packetToByte(Packet p)
        {
            /*
            byte[] msg = new byte[size];
            
            msg[0-1] = BitConverter.GetBytes(p.header.comm);
            msg[2-4] = 

            Array.Copy(GetBytes(p.header.comm), 0, msg, 0, 2);
            Array.Copy(GetBytes(p.header.code), 0, msg, 2, 2);
            Array.Copy(GetBytes(p.header.size), 0, msg, 4, 4);
            Array.Copy(GetBytes(p.header.reserved), 0, msg, 8, 4);
            Array.Copy(p.body, 0, msg, 12, p.body.size);
           
            //////////////////// 
            comm type (2byte)
            msg[0] = p.header.comm & 11110000b;
            msg[1] = p.header.comm & 00001111b;

            code (2byte)
            msg[2] = p.header.code & 11110000b;
            msg[3] = p.header.code & 00001111b;

            size (4byte)
            msg[4] = p.header.size
            msg[5] = p.header.size
            msg[6] = p.header.size
            msg[7] = p.header.size

            reserved (4byte)
            msg[] = p.header.reserved
            msg[] = p.header.reserved
            msg[] = p.header.reserved
            msg[] = p.header.reserved
            */

            byte[] bComm = GetBytes(p.header.comm);
            byte[] bCode = GetBytes(p.header.code);
            byte[] bSize = GetBytes(p.header.size);
            byte[] bRsvd = GetBytes(p.header.reserved);

            return bComm.Concat(bCode).Concat(bSize).Concat(bRsvd).Concat(p.body).ToArray();
        }

        /*
        public static HeaderToByte //send this first
        public static BodyToByte   //process this while sending and send when finished
        public static ByteToHeader //get this first
        public static ByteToPacket //get buffer size from header and receive (give header as argument)
        */
         

        public static Packet byteToPacket(byte[] b)
        {
            Packet p = new Packet();

            p.header.comm = ToInt16(b, 0);
            p.header.code = ToInt16(b, 2);
            p.header.size = ToInt32(b, 4);
            p.header.reserved = ToInt32(b, 8);
            Array.Copy(b, 12, p.body, 0, p.header.size);
            
            return p;
        }

        /*
        private static byte[] SubArray(this byte[] data, int index, int length)
        {
            byte[] result = new byte[length];
            Array.Copy(data, index, result, 0, length);
            return result;
        }
        */
    }
}
