using ServiceStack;
using Football_Fantasy.SubClasses;
using Football_Fantasy.Business;
using SQLitePCL;

namespace Football_Fantasy.Presentation;

public class PresentationCreateTeam
{
    public static object ResponseOfRequestPlayerListApi()
    {
        bool status = BusinessCreateTeam.RquestToApiAndSaveData();
        if (status) 
        { 
            return new 
            {
                status="OK", 
                massage="Players Saved."
            };
        } 
        return new 
        {
            status = "Fail", 
            massage = "There is an error in the backside."
        };
        
    }
    public static object ResponseOfRequestTeamList()
    {
        bool status = BusinessCreateTeam.RequestToApiForTeam();
        if (status)
        {
            return new 
            {
                status="OK", 
                massage="Team Saved."
            };
        }
        return new 
        {
            status = "Fail", 
            massage = "There is an error in the backside."
        };
    }
    public static object GetPlayers(string search,string type,string sort,string price,string team,string page)
    {
        List<Player> PlayerList = BusinessCreateTeam.GetPlayerList(search,type,sort,price,team);
        return new
        {
            PlayerList = BusinessCreateTeam.PagationFormWithAListOfPlayers(PlayerList, page),
            SizeOfPagation = BusinessCreateTeam.MaxSizePagation(PlayerList.Count)
        };
    }
    public static object GetTeamList()
    {
        return new
        {
            TeamList = BusinessCreateTeam.GetTeamList()
        };
    }
    public static object AddPlayer(PlayerAdder input)
    {
        string user_email = input.user_email;
        if (user_email == null)
        {
            return new
            {
                status = "Fail",
                price="",
                massage = "Please login."
            };
        }

        if (BusinessCreateTeam.AddPlayer(input.player,input.player_position, user_email))
        {
            return new
            {
                status = "OK",
                price = BusinessCreateTeam.GetPrice(user_email),
                count = BusinessCreateTeam.CountOfTeam(user_email),
                massage = "Saved.",
                name = input.player
            };
        }
        
        return new
        {
            status = "Fail",
            price="",
            massage = BusinessCreateTeam.ErrorOfAddPlayer(input.player,input.user_email,input.player_position)
        };
        
    }
    public static object GetPlayersOfTeam(string user)
    {
        return new
        {
            Tema = BusinessCreateTeam.GetPlayerOfTeams(user)
        };

    }
    public static object ResponseOfCreateTeam(string user_email)
    {
        if (BusinessCreateTeam.CreateTeam(user_email))
        {
            return new
            {
                status = "OK",
                massage = "Team Create with this email.",
                price = "105"
            };
        }

        if (BusinessCreateTeam.ThisUserAlreadyHasTeam(user_email))
        {
            BusinessCreateTeam.CalculateScore(user_email);
            return new
            {
                status="Team",
                massage="You already has a team.",
                countplayer=BusinessCreateTeam.CountOfTeam(user_email),
                playersteam=BusinessCreateTeam.GetPlayerOfTeams(user_email),
                team = BusinessCreateTeam.ReturnUserTeam(user_email)
            };
        }
        return new
        {
            status = "Fail",
            massage = "This email doesn't exist."
        };
    }
    public static object ResponseOfRemovePlayer(string web_name, string user_email)
    {
        if (BusinessCreateTeam.RemovePlayer(web_name, user_email))
        {
            return new
            {
                status = "OK",
                massage = "Player removed.",
                price = BusinessCreateTeam.GetPrice(user_email)
            };
        }

        return new
        {
            status="Fail",
            massage="Player does not exist or this user email."
        };
    }
    public static object GetPlayerByName(string web_name)
    {
        return new
        {
            player=BusinessCreateTeam.GetPlayerByName(web_name)
        };
    }
    public static object GetPrice(string user_email)
    {
        return new
        {
            status = "OK",
            massage = BusinessCreateTeam.GetPrice(user_email)
        };
    }
    public static object GetPhotoUrl(string web_name)
    {
        return new
        {
            url = BusinessCreateTeam.GetPhoto(web_name)
        };
    }
    public static object DeleteTable(string table_name)
    {
        return new
        {
            massage = BusinessCreateTeam.DeleteTable(table_name)
        };
    }
    public static object CreateTeamIsDone(string user_email)
    {
        BusinessCreateTeam.TeamIsDone(user_email);
        return new
        {
            status = "OK",
            massage = "done : true"
        };
    }
    public static object SwapPlayer(string player_one, string player_two,string user_email)
    {
        if (BusinessCreateTeam.SwapPlayers(player_one, player_two, user_email))
        {
            return new
            {
                status = "OK",
                massage = "Swap Seccessfuly"
            };
        }

        return new
        {
            status = "Fail",
            massage = "Wich  one player does not exist."
        };



    }
    public static object TeamIsNotDone(string user_email)
    {
        BusinessCreateTeam.TeamisNotDone(user_email);
        BusinessCreateTeam.CalculateScore(user_email);
        return new
        {
            status = "OK",
            massage = "False save to teams done"
        };

    }
    public static object GetTeamByEmail(string user_email)
    {
        return new
        {
            team = BusinessCreateTeam.GetTeam(user_email)
        };
    }
    public static object ReadToken(string token)
    {
        string value = SubClasses.Token.ReadToken("email", token);
        if (BusinessLogin.IsThisEmailExist(value))
        {
            return new
            {
                status="OK",
                massage = value
            };
        }

        return new
        {
            status = "Fail",
            massage = "Please Login"
        };

    }
    public static object CreateToken(string email)
    {
        
        string ReToken= SubClasses.Token.GenerateToken("email", email); 
        return new
        {
            email = email, 
            massage = ReToken
        };

    }

    public static object Score(string user_email)
    {
        return new
        {
            status = "OK",
            massage = "score save",
            score = BusinessCreateTeam.CalculateScore(user_email)
        };
    }
}
public class PlayerAdder
{
    public string player { get; set; }
    public string player_position { get; set; }
    public string user_email { get; set; } //type : "email"
}

