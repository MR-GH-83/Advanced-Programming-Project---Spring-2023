using Football_Fantasy.SubClasses;
using Microsoft.EntityFrameworkCore;


namespace Football_Fantasy.DataAccess;

public class DataCreateTeam
{
   public static void EmptyThePlayerTable(Database db)
   {
      db.Database.ExecuteSqlRaw($"DELETE FROM 'Players';");
      db.Database.ExecuteSqlRaw($"UPDATE SQLITE_SEQUENCE SET seq = 0 WHERE name = 'Players';");
      db.SaveChanges();
   }
   public static void EmptyTheTableViaName(string table_name)
   {
      using (var db = new Database())
      {
         db.Database.ExecuteSqlRaw($"DELETE FROM '{table_name}';");
         db.Database.ExecuteSqlRaw($"UPDATE SQLITE_SEQUENCE SET seq = 0 WHERE name = '{table_name}';");
         db.SaveChanges();
      }
   }
   public static void EmptyTheRealTeamTable(Database db)
   {
      db.Database.ExecuteSqlRaw($"DELETE FROM 'RealTeams';");
      db.Database.ExecuteSqlRaw($"UPDATE SQLITE_SEQUENCE SET seq = 0 WHERE name = 'RealTeams';");
      db.SaveChanges();
   }
   public static void SavePlayerList(PlayerListClass playerlist)
   {
      
      using (var db = new Database())
      {
         EmptyThePlayerTable(db);
         foreach(var player in playerlist.elements)
         {
            Player temp = new Player();
            temp.code = player.code;
            temp.element_type=player.element_type;
            temp.first_name = player.first_name;
            temp.id = player.id;
            temp.now_cost = player.now_cost;
            temp.photo = player.photo;
            temp.second_name = player.second_name;
            temp.team = player.team;
            temp.total_points = player.total_points;
            temp.web_name = player.web_name;
            db.Players.Add(temp);
         }
         db.SaveChanges();
      }
   }
   public static void SaveTeamList(TeamFromApi Team)
   {
      using (var db = new Database())
      {
         EmptyTheRealTeamTable(db);
         foreach (var team in Team.teams)
         {
            RealTeam temp = new RealTeam();
            temp.id = team.id;
            temp.name = team.name;
            db.RealTeams.Add(temp);
         }

         db.SaveChanges();
      }
   }
   public static List<Player> GetAllPlayer()
   {
      List<Player> playerlist = new List<Player>();
      using (var db = new Database())
      {
         foreach (var player in db.Players)
         {
            playerlist.Add(player);
         }
      }
      
      return playerlist;
   }
   public static List<RealTeam> GetAllRealTeam()
   {
      List<RealTeam> teams = new List<RealTeam>();
      using (var db = new Database())
      {
         foreach (var team in db.RealTeams)
         {
            teams.Add(team);
         }
      }

      return teams;
   }
   public static double GetPriceTeam(string user_email)
   {
      double result=0;
      using (var db = new Database())
      {
         foreach (var user_team in db.Teams)
         {
            if (user_team.user_email == user_email)
            {
               result = Math.Round(Convert.ToDouble(user_team.price),2);
               break;
            }
         }
      }

      return result;
   }
   public static int GetCountOfTeam(string user_email)
   {
      int Count = 0;
      using (var db = new Database())
      {
         foreach (var user_team in db.Teams)
         {
            if (user_team.user_email == user_email)
            {
               foreach (var players in db.PlayersTeam)
               {
                  if (players.user_email == user_email)
                  {
                     Count++;
                  }
                  
               }
            }
         }

         return Count;
      }

      return 0;
   }
   public static bool DoWeCanAddThisPlayer(string web_name, string user_email)
   {
      using (var db = new Database())
      {
         foreach (var user_team in db.Teams)
         {
            if (user_team.user_email == user_email)
            {
               foreach (var player in db.Players)
               {
                  if (player.web_name == web_name)
                  {
                     return true;
                  }
               }
            }
         }
      }

      return false;
   }
   public static bool IsThisPlayerExistInTeam(string web_name, string user_email)
   {
      using (var db = new Database())
      {
         foreach (var user_team in db.Teams)
         {
            if (user_team.user_email == user_email)
            {
               foreach (var players in db.PlayersTeam)
               {
                  if (players.user_email == user_email && players.web_name == web_name)
                  {
                     return true;
                  }
               }
            }
         }

         return false;
      }

      return false;
   }
   public static void AddPlayerToTeamUser(string web_name,string player_position, string user_email)
   {
      double price = GetPriceTeam(user_email);
      using (var db = new Database())
      {
         foreach (var user_team in db.Teams)
         {
            if (user_team.user_email == user_email)
            {
               Player player = GetPlayerByName(web_name);
               PlayerOfTeam player_saver = new PlayerOfTeam();
               player_saver.web_name = web_name;
               player_saver.player_position = player_position;
               player_saver.user_email = user_email;
               price = Math.Round((price - Convert.ToDouble(player.now_cost)), 2);
               user_team.price = price.ToString();
               db.PlayersTeam.Add(player_saver);
            }
         }

         db.SaveChanges();
      }
   }
   public static List<PlayerOfTeam> PlayerListOfTeam(string user_email)
   {
      List<PlayerOfTeam> Null = new List<PlayerOfTeam>();
      using (var db = new Database())
      {
         foreach (var user_team in db.Teams)
         {
            if (user_team.user_email == user_email)
            {
               foreach (var players in db.PlayersTeam)
               {
                  if(players.user_email==user_email)
                  {
                     Null.Add(players);
                  }
               }
            }
         }
      }

      return Null;
   }
   public static Player GetPlayerByName(string web_name)
   {
      Player result = new Player();
      using (var db = new Database())
      {
         foreach (var player in db.Players)
         {
            if (player.web_name == web_name)
            {
               result = player;
               break;
            }
         }
      }

      return result;
   }
   public static bool IsThisEmailExistInUsers(string user_email)
   {
      using (var db = new Database())
      {
         foreach (var user in db.Users)
         {
            if (user.email == user_email)
            {
               return true;
            }
         }         
      }
      return false;
   }
   public static User GetUserByEmail(string user_email)
   {
      User Null = new User();
      using (var db = new Database())
      {
         foreach (var users in db.Users)
         {
            if (users.email == user_email)
            {
               return users;
            }
         }
      }

      return Null;
   }
   public static bool ThisUserAlreadyHasATeam(string user_email)
   {
      using (var db = new Database())
      {
         foreach (var user_team in db.Teams)
         {
            if (user_team.user_email == user_email)
            {
               return true;
            }
         }
      }

      return false;
   }
   public static Team GetUserTeamByEmail(string user_email)
   {
      Team Null = new Team();
      using (var db = new Database())
      {
         foreach (var user_team in db.Teams)
         {
            if (user_team.user_email == user_email)
            {
               double temp = Convert.ToDouble(user_team.price);
               temp = Math.Round(temp, 2);
               user_team.price = temp.ToString();
               return user_team;
            }
         }

         db.SaveChanges();
      }

      return Null;
   }
   public static bool ThisPlayerExistInTeam(string web_name, string user_email)
   {
      using (var db = new Database())
      {
         foreach (var user_team in db.Teams)
         {
            if (user_team.user_email == user_email)
            {
               foreach (var player_team in db.PlayersTeam)
               {
                  if (player_team.web_name == web_name)
                  {
                     return true;
                  }
               }
            }
         }
      }

      return false;
   }
   public static void RemovePlayer(string web_name, string user_email)
   {
      using (var db = new Database())
      {
         foreach (var user_team in db.Teams)
         {
            if (user_team.user_email == user_email)
            {
               foreach (var playerOfTeam in db.PlayersTeam)
               {
                  if (playerOfTeam.user_email == user_email && playerOfTeam.web_name == web_name)
                  {
                     Player player = GetPlayerByName(playerOfTeam.web_name);
                     db.PlayersTeam.Remove(playerOfTeam);
                     user_team.price = (Convert.ToDouble(user_team.price) + Convert.ToDouble(player.now_cost)).ToString();
                  }
                  
               }
            }
         }

         db.SaveChanges();
      }
   }
   public static void AddTeamToDatabase(Team team,string user_email)
   {
      using (var db = new Database())
      {
         
         db.Teams.Add(team);
         db.SaveChanges();
      }
   }
   public static void RemovePlayersOfATeam(string user_email)
   {
      using (var db = new Database())
      {
         foreach (var user_team in db.Teams)
         {
            if (user_team.user_email == user_email)
            {
               foreach (var playerOfTeam in db.PlayersTeam)
               {
                  if (playerOfTeam.user_email == user_email)
                  {
                     db.PlayersTeam.Remove(playerOfTeam);
                  }
               }
            }
         }
         db.SaveChanges();
      }
   }
   public static string GetPlayerPositionByWebName(string user_email, string web_name)
   {
      using (var db = new Database())
      {
         foreach (var player_team in db.PlayersTeam)
         {
            if (player_team.user_email == user_email)
            {
               if (player_team.web_name == web_name)
               {
                  return player_team.player_position;
               }
            }
         }
      }

      return "";
   }
   public static void Swap(string player_one, string player_two, string user_email)
   {
      string position_one = GetPlayerPositionByWebName(user_email,player_one);
      string position_two = GetPlayerPositionByWebName(user_email,player_two);
      using (var db = new Database())
      {
         foreach (var player_team in db.PlayersTeam)
         {
            if (player_team.user_email == user_email)
            {
               if (player_team.web_name == player_one)
               {
                  player_team.player_position = position_two;
               }

               if (player_team.web_name == player_two)
               {
                  player_team.player_position = position_one;
               }
            }
         }

         db.SaveChanges();
      }
      
   }
   public static void SaveTeamIsNotDone(string user_email)
   {
      using (var db = new Database())
      {
         foreach (var user_team in db.Teams)
         {
            if (user_team.user_email == user_email)
            {
               user_team.done = false;
            }
         }

         db.SaveChanges();
      }
   }

   public static void SaveScore(string user_email, int score)
   {
      using (var db = new Database())
      {
         foreach (var team in db.Teams)
         {
            if (team.user_email == user_email)
            {
               team.score = score.ToString();
            }
         }

         db.SaveChanges();
      }
      
   }
   
   public static void SaveTeamIsDone(string user_email)
   {
      using (var db = new Database())
      {
         foreach (var user_team in db.Teams)
         {
            if (user_team.user_email == user_email)
            {
               user_team.done = true;
            }
         }

         db.SaveChanges();
      }
      
   }

   public static bool TeamIsDone(string user_email)
   {
      using (var db = new Database())
      {
         foreach (var teams in db.Teams)
         {
            if (teams.user_email == user_email)
            {
               if (teams.done)
                  return true;
            }
         }
      }

      return false;

   }
   
}