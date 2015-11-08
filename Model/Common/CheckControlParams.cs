using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Carrot.Model
{
    public class CheckControlParams
    {
        public enum ValueType
        {
            Int,
            String,
            Datetime,
            Double
        };
        public CheckControlParams(string name, string value, string errormsg, ValueType valueType)
        {
            Name = name;
            Value = value;
            Type = valueType;
            Errormsg = errormsg;
            IsRequired = false;
        }
        public CheckControlParams(string name,string value,string errormsg, ValueType valueType,bool isRequired)
        {
            Name = name;
            Value = value;
            Type = valueType;
            Errormsg = errormsg;
            IsRequired = isRequired;
        }
        private bool isRequired;

        public bool IsRequired
        {
            get { return isRequired; }
            set { isRequired = value; }
        }
        private string errormsg;

        public string Errormsg
        {
            get { return errormsg; }
            set { errormsg = value; }
        }
        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        private string value;

        public string Value
        {
            get { return this.value; }
            set { this.value = value; }
        }
        private ValueType type;

        public ValueType Type
        {
            get { return type; }
            set { type = value; }
        }
    }
}
