using Football_Fantasy.DataAccess;
using Football_Fantasy.SubClasses;
using ServiceStack;

namespace Football_Fantasy.Business;

public class BusinessCreateTeam
{
    public static PlayerListClass ConvertTeamIdOfPlayerListToName(PlayerListClass PlayerList)
    {
        List<RealTeam> TeamList = DataCreateTeam.GetAllRealTeam();
        foreach (var player in PlayerList.elements)
        {
            foreach (var team in TeamList)
            {
                if (team.id == player.team)
                {
                    player.team = team.name;
                    break;
                }
            }
        }

        return PlayerList;
    }
    public static PlayerListClass ConvertElementTypeToName(PlayerListClass PlayerList)
    {
        foreach (var player in PlayerList.elements)
        {
            switch (player.element_type)
            {
                case "1" : player.element_type = "GK";
                    break;
                case "2" : player.element_type = "DEF";
                    break;
                case "3" : player.element_type = "MID";
                    break;
                case "4" : player.element_type = "FWD";
                    break;
            }
        }
        return PlayerList;
    }
    public static PlayerListClass ConvertPhotoToLink(PlayerListClass PlayerList)
    {
        foreach (var player in PlayerList.elements)
        {
            player.photo = "p" + player.code + ".png";
        }

        return PlayerList;
    }
    public static PlayerListClass ConvertNowCostToMillion(PlayerListClass PlayerList)
    {
        foreach (var player in PlayerList.elements)
        {
            player.now_cost = (Convert.ToDouble(player.now_cost)/10).ToString();
        }

        return PlayerList;
    }
    public static bool RequestToApiForTeam()
    {
        string url = "https://fantasy.premierleague.com/api/bootstrap-static/";
        TeamFromApi TeamList = new TeamFromApi();
        TeamList = url.GetJsonFromUrl().FromJson<TeamFromApi>();
        DataCreateTeam.SaveTeamList(TeamList);
        return true;
    }
    public static bool RquestToApiAndSaveData()
    {
        string url = "https://fantasy.premierleague.com/api/bootstrap-static/";
        PlayerListClass PlayerList = new PlayerListClass();
        PlayerList = url.GetJsonFromUrl().FromJson<PlayerListClass>();
        PlayerList = ConvertTeamIdOfPlayerListToName(PlayerList);
        PlayerList = ConvertElementTypeToName(PlayerList);
        PlayerList = ConvertPhotoToLink(PlayerList);
        PlayerList = ConvertNowCostToMillion(PlayerList);
        DataCreateTeam.SavePlayerList(PlayerList);
        return true;
    }
    public static int MaxSizePagation(int LengthOfList)
    {
        if (LengthOfList == 0)
        {
            return 1;
        }

        if (LengthOfList < 50)
        {
            return 1;
        }
    if (LengthOfList % 50 == 0)
        {
            return LengthOfList / 50;
        }
        else
        {
            return LengthOfList / 50 + 1;
        }
        
    }
    public static List<Player> PagationFormWithAListOfPlayers(List<Player> list,string page)
    {
        int IntPage = Convert.ToInt32(page);
        int FirstPlayer = (IntPage - 1) * 50;
        int LastPlayer = FirstPlayer + 49;
        List<Player> result = new List<Player>();
        for (int i = FirstPlayer; i <= LastPlayer; i++)
        {
            if(i>=list.Count)
                break;
            result.Add(list[i]);
        }
        return result;
    }
    public static List<Player> PlayersByType(List<Player> list, string type)
    {
        if (type == "ALL" || type == "" || type == null)
            return list;
        List<Player> result = new List<Player>();
        foreach (var player in list)
        {
            if (player.element_type == type)
            {
                result.Add(player);
            }
        }

        return result;
    }
    public static List<Player> SortByIndexOfSearching(List<Player> list,List<int> indexs)
    {
        List<Player> result = new List<Player>();
        for(int flag=0;flag<11;flag++)
        {
            for(int i=0;i<indexs.Count;i++)
            {
                if (flag == indexs[i])
                { 
                    result.Add(list[i]);
                }
            }
            
        }
        return result;
    }
    public static List<Player> PlayersBySearchInName(List<Player> list,string str)
    {
        if (str == "" || str==null)
            return list;
        List<Player> playerlist = new List<Player>();
        List<int> index = new List<int>();
        foreach (var player in list)
        {
            if (player.web_name.ToLower().IndexOf(str.ToLower()) != -1)
            {
                playerlist.Add(player);
                index.Add(player.web_name.ToLower().IndexOf(str.ToLower()));
            }
        }

        return SortByIndexOfSearching(playerlist, index);
    }
    public static List<Player> SortPlayerByTotalPoint(List<Player> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            for (int j = 0; j < list.Count-1; j++)
            {
                if (Convert.ToInt32(list[j].total_points) < Convert.ToInt32(list[j+1].total_points))
                {
                    Player tem = list[j];
                    list[j] = list[j + 1];
                    list[j + 1] = tem;
                }
            }
        }

        return list;
    }
    public static List<Player> SortPlayerByPrice(List<Player> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            for (int j = 0; j < list.Count-1; j++)
            {
                if (Convert.ToDouble(list[j].now_cost) < Convert.ToDouble(list[j+1].now_cost))
                {
                    Player tem = list[j];
                    list[j] = list[j + 1];
                    list[j + 1] = tem;
                }
            }
        }

        return list;
    }
    public static List<Player> PlayerBySort(List<Player> list, string sort)
    {
        if (sort == null || sort == "")
        {
            return list;
        }

        if (sort == "totalpoint")
        {
            return SortPlayerByTotalPoint(list);
        }

        if (sort == "price")
        {
            return SortPlayerByPrice(list);
        }
        
        return list;
    }
    public static List<Player> PlayerByPrice(List<Player> list, string price)
    {
        if (price == "" || price == null)
        {
            return list;
        }

        List<Player> result = new List<Player>();
        double UpCost = Convert.ToDouble(price);
        foreach (var player in list)
        {
            if (Convert.ToDouble(player.now_cost) > UpCost)
            {
                result.Add(player);
            }
        }

        return result;
    }
    public static List<Player> PlayerByTeam(List<Player> list, string team)
    {
        if (team == "" || team == null)
        {
            return list;
        }

        List<Player> result = new List<Player>();
        foreach (var player in list)
        {
            if (player.team == team)
            {
                result.Add(player);
            }
        }

        return result;
    }
    public static Player GetPlayerByOwnList(List<Player> list,string web_name)
    {
        Player Null = new Player();
        foreach (var player in list)
        {
            if (player.web_name == web_name)
            {
                return player;
            }
        }

        return Null;
    }
    public static List<Player> SortByAlphabetical(List<Player> list)
    {
        List<string> Names = new List<string>();
        List<Player> result = new List<Player>();
        foreach (var player in list)
        {
            Names.Add(player.web_name);
        }
        Names.Sort();
        for (int i = 0; i < Names.Count; i++)
        {
            result.Add(GetPlayerByOwnList(list,Names[i]));
        }
        
        return result;
    }
    public static List<Player> GetPlayerList(string search,string type,string sort,string price,string team)
    {
        List<Player> Players = DataCreateTeam.GetAllPlayer();
        Players = SortByAlphabetical(Players);
        Players = PlayerByTeam(Players, team);
        Players = PlayersByType(Players, type);
        Players = PlayerByPrice(Players, price);
        Players = PlayersBySearchInName(Players, search);
        Players = PlayerBySort(Players, sort);
        return Players;
    }
    public static List<RealTeam> GetTeamList()
    {
        return DataCreateTeam.GetAllRealTeam();
    }
    public static bool DoWeHaveMoney(double player_price,double price)
    {
        if (player_price > price)
        {
            return false;
        }

        return true;
    }
    public static string GetPrice(string user_email)
    {
        return DataCreateTeam.GetPriceTeam(user_email).ToString();
    }
    public static int CountOfTeam(List<string> Teams,string Team)
    {
        int count = 0;
        foreach (var team_name in Teams)
        {
            if (team_name == Team)
            {
                count++;
            }
        }

        return count;
    }
    public static bool WeHaveEnothOfPlayerTeam(string web_name, string user_email)
    {
        List<PlayerOfTeam> player_of_teams = new List<PlayerOfTeam>(DataCreateTeam.PlayerListOfTeam(user_email));
        List<string> teams = new List<string>();
        foreach (var playerOfTeam in player_of_teams)
        {
            teams.Add(GetPlayerByName(playerOfTeam.web_name).team);
        }
        Player player = GetPlayerByName(web_name);
        if (CountOfTeam(teams, player.team)==3)
        {
            return false;
        }

        return true;
    }
    public static bool AddPlayer(string web_name,string player_position,string user_email)
    {
        if (DataCreateTeam.ThisUserAlreadyHasATeam(user_email) && DataCreateTeam.IsThisPlayerExistInTeam(web_name,user_email)==false && WeHaveEnothOfPlayerTeam(web_name,user_email))
        {
            if (DataCreateTeam.DoWeCanAddThisPlayer(web_name, user_email) && DataCreateTeam.GetCountOfTeam(user_email)<=15)
            {
                Player player = DataCreateTeam.GetPlayerByName(web_name);
                double price = DataCreateTeam.GetPriceTeam(user_email);
                double player_price = Convert.ToDouble(player.now_cost);
                if (DoWeHaveMoney(player_price, price))
                {
                    DataCreateTeam.AddPlayerToTeamUser(web_name,player_position, user_email);
                    return true;
                }
            }
        }
        
        return false;
    }
    public static List<PlayerOfTeam> GetPlayerOfTeams(string user_email)
    {
        return DataCreateTeam.PlayerListOfTeam(user_email);
    }
    public static bool DeleteTable(string tabel_name)
    {
        DataCreateTeam.EmptyTheTableViaName(tabel_name);
        return true;
    }
    public static bool CreateTeam(string user_email)
    {
        if (DataCreateTeam.IsThisEmailExistInUsers(user_email))
        {
            if (DataCreateTeam.ThisUserAlreadyHasATeam(user_email)==false)
            {
                Team user_team = new Team();
                User user = DataCreateTeam.GetUserByEmail(user_email);
                user_team.price = (105).ToString();
                user_team.user_email = user.email;
                user_team.user_id = (user.id).ToString();
                user_team.score = (0).ToString();
                DataCreateTeam.AddTeamToDatabase(user_team,user_email);
                return true;
            }
            
        }

        return false;
    }
    public static bool ThisUserAlreadyHasTeam(string user_email)
    {
        //DataCreateTeam.RemovePlayersOfATeam(user_email);
        return DataCreateTeam.ThisUserAlreadyHasATeam(user_email);
    }
    public static Team ReturnUserTeam(string user_email)
    {
        return DataCreateTeam.GetUserTeamByEmail(user_email);
    }
    public static bool RemovePlayer(string web_name, string user_email)
    {
        if (DataCreateTeam.ThisUserAlreadyHasATeam(user_email))
        {
            if (DataCreateTeam.ThisPlayerExistInTeam(web_name, user_email))
            {
                DataCreateTeam.RemovePlayer(web_name,user_email);
                return true;
            }
        }

        return false;
    }
    public static Player GetPlayerByName(string web_name)
    {
        return DataCreateTeam.GetPlayerByName(web_name);
    }
    public static string GetPhoto(string web_name)
    {
        Player player = GetPlayerByName(web_name);
        string url ="https://resources.premierleague.com/premierleague/photos/players/250x250/"+ player.photo;
        try
        {
            url.GetJsonFromUrl();
        }
        catch (Exception e)
        {
            url = "https://s29.picofile.com/file/8464413926/Player_Jersey.png";
        }
        return url;
    }
    public static int CountOfTeam(string user_email)
    {
        return DataCreateTeam.GetCountOfTeam(user_email);
    }
    public static bool DoWeHaveMany(string web_name,string user_email)
    {
        Player player = GetPlayerByName(web_name);
        double present_price = DataCreateTeam.GetPriceTeam(user_email);
        double player_price = Convert.ToDouble(player.now_cost);

        if (player_price > present_price)
        {
            return false;
        }

        return true;
    }
    public static string ErrorOfAddPlayer(string web_name, string user_email, string player_position)
    {
        if (DataCreateTeam.ThisUserAlreadyHasATeam(user_email) == false)
        {
            return "Please Login First";
        }

        if (DataCreateTeam.IsThisPlayerExistInTeam(web_name, user_email))
        {
            return "You Already have this player in your team";
        }

        if (DoWeHaveMany(web_name, user_email) == false)
        {
            return "You dont have money for this player please choose another one.";
        }

        if (WeHaveEnothOfPlayerTeam(web_name, user_email) == false)
        {
            return "You took three players from this team. Please choose from another team.";
        }
        return "";
    }
    public static void TeamIsDone(string user_email)
    {
        DataCreateTeam.SaveTeamIsDone(user_email);
    }
    public static bool IsThisPlayerExsistInTeam(string user_email, string web_name)
    {
        List<PlayerOfTeam> playerOfTeams = DataCreateTeam.PlayerListOfTeam(user_email);
        foreach (var player in playerOfTeams)
        {
            if (player.web_name == web_name)
                return true;
        }

        return false;
    }
    public static bool SwapPlayers(string player_one, string player_two, string user_email)
    {
        if (IsThisPlayerExsistInTeam(user_email, player_one) && IsThisPlayerExsistInTeam(user_email, player_two))
        {
            DataCreateTeam.Swap(player_one,player_two,user_email);
            return true;
        }

        return false;
    }
    public static void TeamisNotDone(string user_email)
    {
        DataCreateTeam.SaveTeamIsNotDone(user_email);
    }
    public static Team GetTeam(string user_email)
    {
        return DataCreateTeam.GetUserTeamByEmail(user_email);
    }

    public static int CalculateScore(string user_email)
    {
        if (DataCreateTeam.TeamIsDone(user_email))
        {
            List<PlayerOfTeam> list = GetPlayerOfTeams(user_email);
            int sum_main = MainPlayerPoint(list);
            int sum_sub = SubPlayerPoint(list);
            int score = (sum_main * 2) + sum_sub;
            DataCreateTeam.SaveScore(user_email,score);
            return score;
        }

        return 0;
    }
    
    public static int MainPlayerPoint(List<PlayerOfTeam> list)
    {
        List<int> result = new List<int>();
        foreach (var player in list)
        {
            try
            {
                if (ConvertPlayerPositionToNum(player.player_position) < 12)
                {
                    Player temp = GetPlayerByName(player.web_name);
                    result.Add((Convert.ToInt32(temp.total_points)));
                }
            }
            catch (Exception e)
            {
                if (e.Message == "Player-Position is wrong")
                {
                    return 0;
                }
            }
        }

        return result.Sum();
    }
    public static int SubPlayerPoint(List<PlayerOfTeam> list)
    {
        List<int> result = new List<int>();
        foreach (var player in list)
        {
            try
            {
                if (ConvertPlayerPositionToNum(player.player_position) > 11 )
                {
                    Player temp = GetPlayerByName(player.web_name);
                    result.Add((Convert.ToInt32(temp.total_points)));
                }
            }
            catch (Exception e)
            {
                if (e.Message == "Player-Position is wrong")
                {
                    return 0;
                }
            }
        }

        return result.Sum();
    }

    public static int ConvertPlayerPositionToNum(string player_position)
    {
        if (player_position == null || player_position == "" || player_position.IndexOf("Player_") == -1)
        {
            throw new Exception("Player-Position is wrong");
        }

        int result = Convert.ToInt32(player_position.Substring(7, 2));
        return result;
    }
}