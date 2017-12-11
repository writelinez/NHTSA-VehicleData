using System;
using System.Collections.Generic;
using System.Text;

namespace NHTSAVehicleData.Core.Attributes
{
    public class FunctionInfoAttribute : Attribute
    {
        public string Name { get; set; }

        public string Parameters { get; set; }

        public string Usage { get; set; }

        public FunctionInfoAttribute() { }

        public FunctionInfoAttribute(string name, string parameters, string usage)
        {
            Name = name;
            Parameters = parameters;
            Usage = usage;
        }
    }
}
