using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel.Channels;
using System.IO;

namespace Carrot.BLL
{
    public class CryptEncoder : MessageEncoder
    {
        CryptEncoderFactory factory;
        MessageEncoder innserEncoder;
        string key;
        string iv;
        public CryptEncoder(CryptEncoderFactory encoderFactory,string key,string iv)
        {
            factory = encoderFactory;
            this.key = key;
            this.iv = iv;
            innserEncoder = factory.InnerMessageEncodingBindingElement.CreateMessageEncoderFactory().Encoder;
        }
        public override string ContentType
        {
            get { return innserEncoder.ContentType; }
        }
        public override string MediaType
        {
            get { return innserEncoder.MediaType; }
        }
        public override MessageVersion MessageVersion
        {
            get { return innserEncoder.MessageVersion; }
        }
        public override bool IsContentTypeSupported(string contentType)
        {
            return innserEncoder.IsContentTypeSupported(contentType);
        }
        public override T GetProperty<T>()
        {
            return innserEncoder.GetProperty<T>();
        }
        public override Message ReadMessage(ArraySegment<byte> buffer, BufferManager bufferManager, string contentType)
        {
            ArraySegment<byte> bytes = new DESCryption(key,iv).Decrypt(buffer);
            int totalLength = bytes.Count;
            byte[] totalBytes = bufferManager.TakeBuffer(totalLength);
            Array.Copy(bytes.Array, 0, totalBytes, 0, bytes.Count);
            ArraySegment<byte> byteArray = new ArraySegment<byte>(totalBytes, 0, bytes.Count);
            bufferManager.ReturnBuffer(byteArray.Array); 
            Message msg = innserEncoder.ReadMessage(byteArray, bufferManager, contentType);
            return msg;

        }
        public override Message ReadMessage(System.IO.Stream stream, int maxSizeOfHeaders, string contentType)
        {
            //读取消息的时候，二进制流为加密的，需要解压
            Stream ms = new DESCryption(key,iv).Decrypt(stream); 
            Message msg = innserEncoder.ReadMessage(ms, maxSizeOfHeaders, contentType);
            return msg;
        }
        public override ArraySegment<byte> WriteMessage(Message message, int maxMessageSize, BufferManager bufferManager, int messageOffset)
        { 
            ArraySegment<byte> bytes = innserEncoder.WriteMessage(message, maxMessageSize, bufferManager);
            ArraySegment<byte> buffer = new DESCryption(key,iv).Encrypt(bytes);
            int totalLength = buffer.Count + messageOffset;
            byte[] totalBytes = bufferManager.TakeBuffer(totalLength);
            Array.Copy(buffer.Array, 0, totalBytes, messageOffset, buffer.Count);
            ArraySegment<byte> byteArray = new ArraySegment<byte>(totalBytes, messageOffset, buffer.Count);
            Console.WriteLine(",原来字节流大小:"+bytes.Count+",压缩后字节流大小:"+byteArray.Count);
            return byteArray;
        }
        public override void WriteMessage(Message message, System.IO.Stream stream)
        {
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            innserEncoder.WriteMessage(message, ms);
            stream = new DESCryption(key,iv).Encrypt(ms);
        }
    }
}