using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExecutionUtil
{
    public class SchemaValues
    {
        private string dataType;
        private string dataTypeLength;

        public string DataType
        {
            get { return dataType; }
            set { dataType = value; }
        }

        public string DataTypeLength
        {
            get { return dataTypeLength; }
            set { dataTypeLength = value; }
        }
    }
}
