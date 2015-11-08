using System.Diagnostics;
using System;
using System.ComponentModel;
using System.Data.OleDb;

namespace Carrot.Model
{
    /// <summary>
    /// �Ƿ�Ϊ2:GUID 1:����ID 0:��ͨ�ֶ� | ������Ӧ�ı���������� | ����
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    public class ColumnAttribute : Attribute
    {
        private int _IsPrimaryKey = 0;
        private OleDbType _ColumnType;
        private int _WordNumber;

        /// <summary>
        ///  ��������
        /// </summary>
        public int WordNumber
        {
            get { return _WordNumber; }
            set { _WordNumber = value; }
        }

        /// <summary>
        /// �Ƿ�Ϊ2:GUID|1:����ID|0:��ͨ�ֶ�|-1:�����ֶ�
        /// </summary>
        public int IsPrimaryKey
        {
            get
            {
                return _IsPrimaryKey;
            }
            set
            {
                this._IsPrimaryKey = value;
            }
        }

        /// <summary>
        /// ������Ӧ�ı����������
        /// </summary>
        public OleDbType ColumnType
        {
            get
            {
                return _ColumnType;
            }
            set
            {
                _ColumnType = value;
            }
        }




        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="IsPrimaryKey">�Ƿ�Ϊ2:GUID|1:����ID|0:��ͨ�ֶ�|-1:�����ֶ�</param>
        /// <param name="strSQLType">������Ӧ�ı����������</param>
        /// <param name="WordNumber">����</param>
        public ColumnAttribute(int IsPrimaryKey, OleDbType strSQLType, int WordNumber)
        {
            this._ColumnType = strSQLType;
            this._IsPrimaryKey = IsPrimaryKey;
            this._WordNumber = WordNumber;
        }
    }
}
