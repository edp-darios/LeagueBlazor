using LeagueBlazor.Classes;

namespace LeagueBlazor.Classes
{
    public class TableRow
    {
        public string teamName { get; set; }
        public int teamRank { get; set; }
        public int teamWins { get; set; }
        public int teamDraws { get; set; }
        public int teamLosses { get; set; }
        public int teamGoals { get; set; }
        public int teamGoalsReceived { get; set; }
        public int teamGoalDifference { get; set; }
        public int teamPoints { get; set; }

        public TableRow(string teamname)
        {
            teamName = teamname;
            teamGoals = 0;
            teamGoalsReceived = 0;
            teamDraws = 0;
            teamWins = 0;
            teamLosses = 0;
            teamGoalDifference = 0;
            teamPoints = 0;
        }
    }
}
