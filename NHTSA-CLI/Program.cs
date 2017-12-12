using NHTSAVehicleData;
using System;
using System.Reflection;
using System.Linq;
using NHTSAVehicleData.Core.Attributes;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using NHTSACLI.Core;

namespace NHTSACLI
{
    class Program
    {
        static bool canExit = false;

        static void Main(string[] args)
        {
            NHTSAClient client = new NHTSAClient();

            Art.PrintApplicationTitle();

            Art.PrintTableRow(80, "Commands", "Parameters", "Example");
            foreach (var commandInfos in GetCommandInfos())
            {
                Art.PrintTableRow(80, commandInfos);
            }

            while (!canExit)
            {
                Console.WriteLine("Enter Command.");
                string input = Console.ReadLine();

                Console.WriteLine("Executing Command...");
                ExecuteCommand(input, client);
                Console.WriteLine("Complete.");
            }
        }

        static List<string[]> GetCommandInfos()
        {
            List<string[]> returnValue = new List<string[]>();

            Type clientType = typeof(NHTSAClient);

            IEnumerable<FunctionInfoAttribute> functionAttributes = clientType
                .GetMethods()
                .Where(t => t.GetCustomAttribute<FunctionInfoAttribute>() != null)
                .Select(t => t.GetCustomAttribute<FunctionInfoAttribute>());

            foreach (var functionInfo in functionAttributes)
            {
                string[] p = new string[3];
                p[0] = functionInfo.Name;
                p[1] = functionInfo.Parameters;
                p[2] = functionInfo.Usage;
                returnValue.Add(p);
            }


            return returnValue;
        }

        static void ExecuteCommand(string input, NHTSAClient client)
        {
            string[] inputArray = input.Split(' ');
            string command = string.Empty;
            List<KeyValuePair<string, string>> commandParameters = new List<KeyValuePair<string, string>>();

            //parse command from input
            command = inputArray.ElementAtOrDefault(0);
            if (string.IsNullOrEmpty(command))
            {
                Art.PrintError("Invalid or missing command");
                return;
            }

            //parse command parameters
            if (inputArray.Length > 1)
            {
                foreach (var item in inputArray.Skip(1))
                {
                    string[] paramsArray = item.Split('=');
                    commandParameters.Add(new KeyValuePair<string, string>(paramsArray.ElementAtOrDefault(0), paramsArray.ElementAtOrDefault(1)));
                }
            }

            Type clientType = typeof(NHTSAClient);

            MethodInfo mi = clientType.GetMethods()
                .FirstOrDefault(t => t.GetCustomAttribute<FunctionInfoAttribute>() != null && t.GetCustomAttribute<FunctionInfoAttribute>().Name.Equals(command));
            if (mi == null)
            {
                Art.PrintError($"{command} is an invalid command");
            }

            ParameterInfo[] pInfos = mi.GetParameters();

            //Only supports string parameters at current. Dont think i will need anything else here.
            object[] funcParameters = new object[pInfos.Length];
            int fIndex = 0;
            foreach (var item in pInfos)
            {
                 var cParam = commandParameters.Where(t => t.Key.Equals(item.Name, StringComparison.CurrentCultureIgnoreCase));
                if (cParam.Count() == 1)
                {
                    funcParameters[fIndex] = ParameterConverter.Convert(cParam.Single().Value, item.ParameterType);
                    fIndex++;
                }
            }

            var result = mi.Invoke(client, funcParameters);
            if (result != null)
            {
                string jsonResult = JsonConvert.SerializeObject(result, Formatting.Indented);

                Console.BackgroundColor = ConsoleColor.White;
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine(jsonResult);
                Console.ResetColor();
            }
        }
    }
}
