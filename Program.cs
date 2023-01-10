using System;
using System.Diagnostics;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            // Initialize variables for the switches
            string? startCommitHash = null;
            string? endCommitHash = null;
            string? jiraBaseUrl = null;

            // Loop through the arguments
            for (int i = 0; i < args.Length; i++)
            {
                // Check for the start commit hash switch
                if (args[i] == "--start")
                {
                    // Get the value of the switch
                    startCommitHash = args[i + 1];
                }

                // Check for the end commit hash switch switch
                if (args[i] == "--end")
                {
                    // Get the value of the switch
                    endCommitHash = args[i + 1];
                }
                // Check for the end commit hash switch switch
                if (args[i] == "--jiraUrl")
                {
                    // Get the value of the switch
                    jiraBaseUrl = args[i + 1];
                }
            }

            if (jiraBaseUrl == null)
            {
                Console.Error.WriteLine("Jira base url must be provided.");
            }

            if (startCommitHash == null || endCommitHash == null)
            {
                Console.Error.WriteLine("The hashes for start and end commit must be provided.");
            }

            // Set the command to run and the arguments
            string command = "git";
            string arguments = $"cherry -v {startCommitHash} {endCommitHash}";

            // Start the process
            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = command,
                Arguments = arguments,
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                CreateNoWindow = true
            };

            Process? process = Process.Start(startInfo);
            if (process == null)
            {
                Console.Error.WriteLine("Unable to start git process.");
                return;
            }

            string error = process.StandardError.ReadToEnd();
            if (!string.IsNullOrEmpty(error))
            {
                Console.WriteLine(error);
                return;
            }

            // Read the output
            string output = process.StandardOutput.ReadToEnd();
            string[] outputLines = output.Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

            var jiraLinks = new List<string>();

            foreach (var line in outputLines)
            {
                Console.WriteLine(line);
            }


            // Wait for the process to exit
            process.WaitForExit();
        }
    }
}
