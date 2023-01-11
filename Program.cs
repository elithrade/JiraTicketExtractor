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
                return;
            }

            if (startCommitHash == null || endCommitHash == null)
            {
                Console.Error.WriteLine("The hashes for start and end commit must be provided.");
                return;
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
                CreateNoWindow = true
            };

            Process? process = Process.Start(startInfo);
            if (process == null)
            {
                Console.Error.WriteLine("Unable to start git process.");
                return;
            }

            // Read the output
            string output = process.StandardOutput.ReadToEnd();
            Dictionary<string, string> ticketNumbers = OutputParser.ParseOutput(jiraBaseUrl, output);

            foreach (var kv in ticketNumbers)
            {
                Console.WriteLine(kv.Value);
            }

            // Wait for the process to exit
            process.WaitForExit();
        }
    }
}
