using Octokit;
using OpenLoganalyzer.Core.Interfaces.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenLoganalyzer.Core.Receiever
{
    public class BugReportRecevier
    {
        private readonly GitHubClient github;

        public BugReportRecevier(string token)
        {
            github = new GitHubClient(new ProductHeaderValue("XanatosX_OpenLoganalyzer"));
            if (string.IsNullOrEmpty(token))
            {
                return;
            }
            var tokenAuth = new Credentials(token);
            github.Credentials = tokenAuth;
        }

        public async Task<Issue> CreateAsync(string subject, string description)
        {
            Issue returnIssue = null;
            if (string.IsNullOrEmpty(subject) || string.IsNullOrEmpty(description))
            {
                return returnIssue;
            }

            NewIssue createIssue = new NewIssue(subject);
            createIssue.Body = description;
            try
            {
                returnIssue = await github.Issue.Create("XanatosX", "OpenLoganalyzer", createIssue);

            }
            catch (Exception ex )
            {
                Console.WriteLine(ex.Message);
                returnIssue = null;
            }

            return returnIssue;
        }

        public async Task<Issue> GetIssueAsync(int id)
        {
            Issue returnIssue = null;

            returnIssue = await github.Issue.Get("XanatosX", "OpenLoganalyzer", id);

            return returnIssue;
        }
    }
}
