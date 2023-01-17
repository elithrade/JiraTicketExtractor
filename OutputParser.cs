namespace ConsoleApp
{
    public class OutputParser
    {
        public static Dictionary<string, string> ParseOutput(string jiraBaseUrl, string output)
        {
            string[] outputLines = output.Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

            var jiraLinks = new List<string>();
            var ticketNumbers = new Dictionary<string, string>();

            foreach (var line in outputLines)
            {
                string[] parts = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                if (parts[0] != "+")
                {
                    // Skip identical commits
                    continue;
                }
                var ticketNumber = parts[2];
                if (ticketNumber.Contains('-') && !ticketNumbers.ContainsKey(ticketNumber))
                {
                    // Valid ticket
                    ticketNumbers.Add(ticketNumber, $"{jiraBaseUrl}/browse/{ticketNumber}");
                }
            }

            return ticketNumbers;
        }
    }
}