using System;
using System.IO;
using System.Collections.Generic;
using LeagueBlazor.Classes;

namespace LeagueBlazor
{
    public class Table
    {
        public List<TableRow> Rows { get; set; }

        private Table(List<TableRow> rows)
        {
            Rows = rows;
        }

        public static Table CreateTable(List<Match> matches)
        {
            Dictionary<string, TableRow> tableRows = new Dictionary<string, TableRow>();
            foreach (Match match in matches)
            {
                string homeTeam = match.homeTeam;
                string awayTeam = match.awayTeam;
                if (!tableRows.ContainsKey(homeTeam))
                {
                    tableRows.Add(homeTeam, new TableRow(homeTeam));
                }
                if (!tableRows.ContainsKey(awayTeam))
                {
                    tableRows.Add(awayTeam, new TableRow(awayTeam));
                }
                //teamGoals
                tableRows[homeTeam].teamGoals += match.homeGoals;
                tableRows[awayTeam].teamGoals += match.awayGoals;
                //teamGoalsReceived
                tableRows[homeTeam].teamGoalsReceived += match.awayGoals;
                tableRows[awayTeam].teamGoalsReceived += match.homeGoals;
                //teamDraws
                tableRows[homeTeam].teamDraws += matchResults(match.homeGoals, match.awayGoals, 1);
                tableRows[awayTeam].teamDraws += matchResults(match.awayGoals, match.homeGoals, 1);
                //teamWins
                tableRows[homeTeam].teamWins += matchResults(match.homeGoals, match.awayGoals, 2);
                tableRows[awayTeam].teamWins += matchResults(match.awayGoals, match.homeGoals, 2);
                //teamLosses
                tableRows[homeTeam].teamLosses += matchResults(match.awayGoals, match.homeGoals, 2);
                tableRows[awayTeam].teamLosses += matchResults(match.homeGoals, match.awayGoals, 2);
                //teamGoalDifference
                tableRows[homeTeam].teamGoalDifference += match.homeGoals - match.awayGoals;
                tableRows[awayTeam].teamGoalDifference += match.awayGoals - match.homeGoals;
                //teamPoints
                tableRows[homeTeam].teamPoints += matchResults(match.homeGoals, match.awayGoals, 3);
                tableRows[awayTeam].teamPoints += matchResults(match.awayGoals, match.homeGoals, 3);
            }
            return new Table(new List<TableRow>(tableRows.Values));
        }

        private static int matchResults(int firstTeamGoals, int secondTeamGoals, int caseSwitch)
        {
            switch (caseSwitch)
            {
                // 1 gibt die draws aus
                case 1:
                    if (firstTeamGoals == secondTeamGoals)
                        return 1;
                    else
                        return 0;
                // 2 gibt die wins und losses aus       
                case 2:
                    if (firstTeamGoals > secondTeamGoals)
                        return 1;
                    else
                        return 0;
                // 3 gibt die Punktzahl für den gewinn aus                  
                case 3:
                    if (firstTeamGoals > secondTeamGoals)
                        return 3;
                    if (firstTeamGoals == secondTeamGoals)
                        return 1;
                    else
                        return 0;
                default:
                    return 0;
            }
        }

        public void Sort()
        {
            Rows.Sort((x, y) =>
            {
                if (x.teamPoints > y.teamPoints)
                    return -1;
                else if (x.teamPoints < y.teamPoints)
                    return 1;
                else if (x.teamGoalDifference > y.teamGoalDifference)
                    return -1;
                else if (x.teamGoalDifference < y.teamGoalDifference)
                    return 1;
                else
                    return x.teamName.CompareTo(y.teamName);
            });
        }
        public List<TableRow> getRows()
        {
            Sort();
            return Rows;
        }

    }
}
