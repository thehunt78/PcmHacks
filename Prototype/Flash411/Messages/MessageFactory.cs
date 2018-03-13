﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flash411
{
    /// <summary>
    /// This class is responsible for generating the messages that the app sends to the PCM.
    /// </summary>
    /// <remarks>
    /// The messages generated by this class are byte-for-byte exactly what the PCM 
    /// receives, with the exception of the CRC byte at the end. CRC bytes must be 
    /// added by the currently-selected Device class if the actual device doesn't add 
    /// the CRC byte automatically.
    ///
    /// Some devices will require these messages to be translated according to the specific
    /// device's protocol - that too is the job of the currently-selected Device class.
    /// </remarks>
    class MessageFactory
    {
        /// <summary>
        /// Create a request to read the given block of PCM memory.
        /// </summary>
        public Message CreateReadRequest(byte block)
        {
            byte[] bytes = new byte[] { 0x6C, 0x10, 0xF0, 0x3C, block };
            return new Message(bytes);
        }

        /// <summary>
        /// Create a request to read the PCM's operating system ID.
        /// </summary>
        /// <returns></returns>
        public Message CreateOperatingSystemIdReadRequest()
        {
            return CreateReadRequest(BlockId.OperatingSystemID);
        }

        /// <summary>
        /// Create a request to read the first segment of the PCM's VIN.
        /// </summary>
        public Message CreateVinRequest1()
        {
            return CreateReadRequest(BlockId.Vin1);
        }

        /// <summary>
        /// Create a request to read the second segment of the PCM's VIN.
        /// </summary>
        public Message CreateVinRequest2()
        {
            return CreateReadRequest(BlockId.Vin2);
        }

        /// <summary>
        /// Create a request to read the thid segment of the PCM's VIN.
        /// </summary>
        public Message CreateVinRequest3()
        {
            return CreateReadRequest(BlockId.Vin3);
        }

        /// <summary>
        /// Create a request to retrieve a 'seed' value from the PCM.
        /// </summary>
        public Message CreateSeedRequest()
        {
            byte[] bytes = new byte[] { 0x6C, 0x10, 0xF0, 0x27, 0x01 };
            return new Message(bytes);
        }

        /// <summary>
        /// Create a request to send a 'key' value to the PCM.
        /// </summary>
        public Message CreateUnlockRequest(UInt16 key)
        {
            byte keyHigh = (byte)((key & 0xFF00) >> 8);
            byte keyLow = (byte)(key & 0xFF);
            byte[] bytes = new byte[] { 0x6C, 0x10, 0xF0, 0x27, 0x02, keyHigh, keyLow };
            return new Message(bytes);
        }
    }
}
