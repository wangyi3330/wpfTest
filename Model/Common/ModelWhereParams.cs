using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using System.Data.OleDb;

namespace Carrot.Model
{
    public class ModelWhereParams
    {
        public ModelWhereParams(object model)
        {
            Model = model;
            WhereParameter = null;
            Select = "";
            Order = "";
        }
        public ModelWhereParams(object model, string select, string order)
        {
            Model = model;
            WhereParameter = null;
            Select = select;
            Order = order;
        }
        public ModelWhereParams(object model, WhereParameter whereParameter)
        {
            List<Carrot.Model.WhereParameter> wp = new List<Carrot.Model.WhereParameter>();
            wp.Add(whereParameter);
            Model = model;
            WhereParameter = wp;
            Select = "";
            Order = "";
        }
        public ModelWhereParams(object model, List<WhereParameter> whereParameter)
        {
            Model = model;
            WhereParameter = whereParameter;
            Select = "";
            Order = "";
        }
        public ModelWhereParams(object model, List<WhereParameter> whereParameter, string select, string order)
        {
            Model = model;
            WhereParameter = whereParameter;
            Select = select;
            Order = order;
        }
        private object model;
        private List<WhereParameter> whereParameter;
        private string select;
        private string order;

        public string Select
        {
            get { return select; }
            set { select = value; }
        }
        public string Order
        {
            get { return order; }
            set { order = value; }
        }
        public object Model
        {
            get { return model; }
            set { model = value; }
        }
        public List<WhereParameter> WhereParameter
        {
            get { return whereParameter; }
            set { whereParameter = value; }
        }


    }
}
