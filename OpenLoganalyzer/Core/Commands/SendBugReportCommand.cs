using Octokit;
using OpenLoganalyzer.Core.Interfaces.Command;
using OpenLoganalyzer.Core.Receiever;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenLoganalyzer.Core.Commands
{
    public class SendBugReportCommand : ICommand
    {
        private readonly BugReportRecevier bugReportRecevier;

        private readonly string subject;

        private readonly string description;

        public SendBugReportCommand(string token, string subject, string description)
        {
            bugReportRecevier = new BugReportRecevier(token);
            this.subject = subject;
            this.description = description;
        }

        public async Task<bool> AsyncExecute()
        {
            Issue issue = await bugReportRecevier.CreateAsync(subject, description);
            return false;
        }

        public bool Execute()
        {
            return false;
        }
    }
}
