using System;
using System.Runtime.InteropServices;
using System.IO;

namespace COMDLLExample
{
    [Guid("12345678-1234-1234-1234-1234567890AB")]
    [ComVisible(true)]
    public class MyCOMClass
    {
        private readonly string logFilePath = "COMLog.txt"; // Define the log file path

        public MyCOMClass()
        {
            // Create or append to the log file
            File.AppendAllText(logFilePath, "[" + DateTime.Now + "] COM Server Started\n");
        }

        private void LogFunctionCall(string functionName, string arguments)
        {
            // Log the function call with arguments to the log file
            File.AppendAllText(logFilePath, "[" + DateTime.Now + "] " + functionName + " called with arguments: " + arguments + "\n");
        }

        public int Add(int a, int b)
        {
            string arguments = "a=" + a + ", b=" + b;
            LogFunctionCall("Add", arguments);
            return a + b;
        }

        public long DoCommand(string args)
        {
            LogFunctionCall("DoCommand", args);
            // Your implementation here
            return 0;
        }

        public long GetValue(string args)
        {
            LogFunctionCall("GetValue", args);
            // Your implementation here
            return 9;
        }

        public long SetValue(string args, string moreargs)
        {
            LogFunctionCall("SetValue", "args=" + args + ", moreargs=" + moreargs);
            // Your implementation here
            return 9;
        }

        public string GetErrorDescription()
        {
            LogFunctionCall("GetErrorDescription", "");
            return "Mock Error Description";
        }

        public long GetLastError(string args)
        {
            LogFunctionCall("GetLastError", args);
            // Your implementation here
            return 0;
        }
    }
}
