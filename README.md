# JiraTicketExtractor
Extracting jira tickets from git commits and generate a list of jira urls. This program makes use of `git cherry` command, and this [blog post](http://jafrog.com/2012/03/22/git-cherry.html) for explanations on how this command work.

# Usage
`dotnet run --start <start commit hash> --end <end commit hash> --jiraUrl <jira url>`

`start commit hash` is the baseline commit that. `end commit hash` is the current commit.

For example:

`dotnet run --start 12345 --end 23456 --jiraUrl https://myOrg.atlassian.net`

# Publish

To publish and produce an executable in Windows environment:

`dotnet publish -c Release -r win-x64 --self-contained true`

