using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.ServiceModel.Channels;

namespace Carrot.BLL
{
    public class CryptEncodingBindingElement : MessageEncodingBindingElement
    {
        private XmlDictionaryReaderQuotas readerQuotas;
        private MessageEncodingBindingElement innerMessageEncodingBindingElement;
        string key;
        string iv;
        public MessageEncodingBindingElement InnerMessageEncodingBindingElement
        {
            get
            {
                return innerMessageEncodingBindingElement;
            }
        }

        public string Key
        {
            get
            {
                return key;
            }
        }
        public string IV
        {
            get
            {
                return iv;
            }
        }

        public CryptEncodingBindingElement(MessageEncodingBindingElement innerMessageEncodingBindingElement, string key, string iv)
        {
            this.readerQuotas = new XmlDictionaryReaderQuotas();
            this.key = key;
            this.iv = iv;
            this.innerMessageEncodingBindingElement = innerMessageEncodingBindingElement;
        }

        public override IChannelFactory<TChannel> BuildChannelFactory<TChannel>(BindingContext context)
        {
            context.BindingParameters.Add(this);
            return context.BuildInnerChannelFactory<TChannel>();
        }
        public override IChannelListener<TChannel> BuildChannelListener<TChannel>(BindingContext context)
        {
            context.BindingParameters.Add(this);
            return context.BuildInnerChannelListener<TChannel>();
        }
        public override bool CanBuildChannelFactory<TChannel>(BindingContext context)
        {
            context.BindingParameters.Add(this);
            return context.CanBuildInnerChannelFactory<TChannel>();
        }
        public override bool CanBuildChannelListener<TChannel>(BindingContext context)
        {
            context.BindingParameters.Add(this);
            return context.CanBuildInnerChannelListener<TChannel>();
        }
        public override MessageEncoderFactory CreateMessageEncoderFactory()
        {
            return new CryptEncoderFactory(innerMessageEncodingBindingElement, key, iv);
        }
        public override T GetProperty<T>(BindingContext context)
        {
            if (typeof(T) == typeof(XmlDictionaryReaderQuotas))
            {
                return this.readerQuotas as T;
            }
            return base.GetProperty<T>(context);

        }
        public override MessageVersion MessageVersion
        {
            get
            {
                return innerMessageEncodingBindingElement.MessageVersion;
            }
            set
            {
                innerMessageEncodingBindingElement.MessageVersion = value;
            }
        }

        public override BindingElement Clone()
        {
            return new CryptEncodingBindingElement(innerMessageEncodingBindingElement, key, iv);
        }
    }
}
