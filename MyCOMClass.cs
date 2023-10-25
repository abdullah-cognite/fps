using System;
using System.Runtime.InteropServices;
using System.IO;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;

namespace COMDLLExample
{
    [Guid("12345678-1234-1234-1234-1234567890AB")]
    [ComVisible(true)]
    public class MyCOMClass
    {
        private readonly string logFilePath = "COMLog.txt"; // Define the log file path
        private readonly string csvFilePath = "commands.csv"; // Define the CSV file path
        private List<CommandData> commands;

        public MyCOMClass()
        {
            // Create or append to the log file
            File.AppendAllText(logFilePath, "[" + DateTime.Now + "] COM Server Started\n");

            // Load commands from the CSV file
            LoadCommands();
        }

        private void LogFunctionCall(string functionName, string arguments)
        {
            // Log the function call with arguments to the log file
            File.AppendAllText(logFilePath, "[" + DateTime.Now + "] " + functionName + " called with arguments: " + arguments + "\n");
        }

        private void LoadCommands()
        {
            try
            {
                commands = new List<CommandData>();
                foreach (var line in File.ReadLines(csvFilePath))
                {
                    var parts = line.Split(',');
                    if (parts.Length == 3)
                    {
                        var command = parts[0].Trim();
                        int delay;

                        // Directly assign string, no parsing needed
                        string output = parts[2].Trim(); 

                        if (int.TryParse(parts[1].Trim(), out delay))
                        {
                            var commandData = new CommandData
                            {
                                Command = command,
                                Delay = delay,
                                Output = output // Direct assignment
                            };

                            commands.Add(commandData);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                commands = new List<CommandData>();
                Console.WriteLine("Error loading commands from CSV file: " + ex.Message);
            }
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
            var command = commands.Find(c => c.Command == args);

            if (command != null)
            {
                LogFunctionCall("Found command in CSV", args);
                System.Threading.Thread.Sleep(command.Delay);

                long outputAsLong;
                if (long.TryParse(command.Output, out outputAsLong))
                {
                    return outputAsLong;
                }
                else
                {
                    return -1; // Or another error value
                }
            }

            return -1; // Return an error value if the command is not found
        }

        public object GetValue(string args)
        {
            LogFunctionCall("GetValue", args);
            var command = commands.Find(c => c.Command == args);

            if (command != null)
            {
                LogFunctionCall("Found GetValue in CSV", args);
                System.Threading.Thread.Sleep(command.Delay);

                // Since Output is now a string, we can directly return it
                return command.Output;
            }

            return "0"; // Return an error value as a string, since Output is a string now
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

    public class CommandData
    {
        public string Command { get; set; }
        public int Delay { get; set; }
        public string Output { get; set; } // Modified from long to string
    }
}
