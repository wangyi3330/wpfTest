using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel.Channels;

namespace Carrot.BLL
{
    public class CryptEncoderFactory : MessageEncoderFactory
    {
        private MessageEncodingBindingElement innerMessageEncodingBindingElement;
        CryptEncoder messageEncoder;
        string key;
        string iv; 
        public CryptEncoderFactory(MessageEncodingBindingElement innerMessageEncodingBindingElement, string key,string iv)
        {
            this.innerMessageEncodingBindingElement = innerMessageEncodingBindingElement;
            this.key = key;
            this.iv = iv;
            messageEncoder = new CryptEncoder(this,key, iv);
        }
        public override MessageEncoder CreateSessionEncoder()
        {
            return base.CreateSessionEncoder();
        }
        public override MessageEncoder Encoder
        {
            get { return messageEncoder; }
        }
        public override MessageVersion MessageVersion
        {
            get { return innerMessageEncodingBindingElement.MessageVersion; }
        }
        public MessageEncodingBindingElement InnerMessageEncodingBindingElement
        {
            get
            {
                return innerMessageEncodingBindingElement;
            }
        }
    }
}