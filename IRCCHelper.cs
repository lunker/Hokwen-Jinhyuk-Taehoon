using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.BitConverter;

namespace Ircc
{
    public struct Header
    {
        public short comm;
        public short code;
        public int size;
        public int reserved;

        public Header(short comm, short code, int size, int reserved)
        {
            this.comm = comm;
            this.code = code;
            this.size = size;
            this.reserved = reserved;
        }
    }
    public struct Packet
    {
        public Header header;
        public byte[] data;

        public Packet(Header header, byte[] data)
        {
            this.header = header;
            this.data = data;
        }
    }
    public static class IrccHelper
    {
        public class FieldIndex
        {
            public const int COMM = 0;
            public const int CODE = 2;
            public const int SIZE = 4;
            public const int RSVD = 8;
            public const int DATA = 12;
        }

        public static byte[] packetToBytes(Packet p)
        {
            /*
            byte[] msg = new byte[2 + 2 + 4 + 4 + p.data.Length];
            Array.Copy(GetBytes(p.header.comm), 0, msg, FieldIndex.COMM, 2);
            Array.Copy(GetBytes(p.header.code), 0, msg, FieldIndex.CODE, 2);
            Array.Copy(GetBytes(p.header.size), 0, msg, FieldIndex.SIZE, 4);
            Array.Copy(GetBytes(p.header.reserved), 0, msg, FieldIndex.RSVD, 4);
            Array.Copy(p.data, 0, msg, FieldIndex.DATA, p.data.size);

            return msg;
            */

            byte[] bComm = GetBytes(p.header.comm);
            byte[] bCode = GetBytes(p.header.code);
            byte[] bSize = GetBytes(p.header.size);
            byte[] bRsvd = GetBytes(p.header.reserved);

            return bComm.Concat(bCode).Concat(bSize).Concat(bRsvd).Concat(p.data).ToArray();
        }

        public static Header bytesToHeader(byte[] b)
        {
            Header h = new Header();

            h.comm = ToInt16(b, FieldIndex.COMM);
            h.code = ToInt16(b, FieldIndex.CODE);
            h.size = ToInt32(b, FieldIndex.SIZE);
            h.reserved = ToInt32(b, FieldIndex.RSVD);

            return h;
        }

        public static Packet bytesToPacket(byte[] b)
        {
            Packet p = new Packet();

            p.header.comm = ToInt16(b, FieldIndex.COMM);
            p.header.code = ToInt16(b, FieldIndex.CODE);
            p.header.size = ToInt32(b, FieldIndex.SIZE);
            p.header.reserved = ToInt32(b, FieldIndex.RSVD);
            Array.Copy(b, FieldIndex.DATA, p.data, 0, p.header.size);
            
            return p;
        }
    }
}
