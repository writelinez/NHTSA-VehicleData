using System;
using System.Collections.Generic;
using System.Text;

namespace NHTSACLI.Core
{
    public static class ParameterConverter
    {
        public static object Convert(string strParam, Type type)
        {
            object value = strParam;
            if (type.Equals(typeof(string[])))
            {
                value = strParam.Split(',');
            }
            else if (type.Equals(typeof(int)))
            {
                int iValue = 0;
                int.TryParse(strParam, out iValue);
                value = iValue;
            }
            else if (type.Equals(typeof(DateTime)))
            {
                DateTime dt = DateTime.MinValue;
                DateTime.TryParse(strParam, out dt);
                value = dt;
            }
            return value;
        }
    }
}
