using OpenLoganalyzerLib.Core.Interfaces.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace OpenLoganalyzerLib.Core.Configuration
{
    public class FilterColumn : IFilterColumn
    {
        public string Type => type;
        private string type;

        public List<string> PossibleRegex => possibleRegex;
        private readonly List<string> possibleRegex;

        public FilterColumn(string type)
        {
            possibleRegex = new List<string>();
            this.type = type;
        }

        public void AddNewRegex(string regex)
        {
            if (!possibleRegex.Contains(regex))
            {
                possibleRegex.Add(regex);
            }
        }

        public void AddMultipleRegex(string[] regexList)
        {
            foreach (string regex in regexList)
            {
                AddNewRegex(regex);
            }
        }

        public void AddMultipleRegex(List<string> regexList)
        {
            AddMultipleRegex(regexList.ToArray());
        }

        public bool IsValid(string logLine)
        {
            foreach (string regex in possibleRegex)
            {
                try
                {
                    Regex regexToCheck = new Regex(regex);
                    Match match = regexToCheck.Match(logLine);
                    if (match.Success && match.Groups.Count > 0)
                    {
                        return true;
                    }
                }
                catch (Exception)
                {
                    continue;
                }
            }
            return false;
        }

        public string GetMatchingPart(string logLine)
        {
            foreach (string regex in possibleRegex)
            {
                try
                {
                    Regex regexToCheck = new Regex(regex);
                    Match match = regexToCheck.Match(logLine);
                    if (match.Success)
                    {
                        return match.Groups[match.Groups.Count - 1].ToString();
                    }
                }
                catch (Exception)
                {
                    continue;
                }
            }
            return "";
        }

        public void RenameColumn(string newName)
        {
            type = newName;
        }

        public void Reset()
        {
            possibleRegex.Clear();
        }

        public void RemoveRegex(string regex)
        {
            possibleRegex.Remove(regex);
        }
    }
}
