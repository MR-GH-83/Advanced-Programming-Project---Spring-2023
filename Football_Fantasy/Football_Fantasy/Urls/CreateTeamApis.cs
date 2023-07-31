using Football_Fantasy.Business;
using Football_Fantasy.Presentation;
namespace Football_Fantasy.Urls;

public class CreateTeamApis
{
    public static void GetApi(WebApplication app)
    {
        app.MapGet("/game", async context =>
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "HtmlFiles/Game.html");
            var fileStream = File.OpenRead(filePath);
            await fileStream.CopyToAsync(context.Response.Body);
        });
        
        app.MapGet("/createteam", PresentationCreateTeam.ResponseOfCreateTeam);

        app.MapGet("/gettoken", PresentationCreateTeam.CreateToken);
        
        app.MapGet("/readtoken", PresentationCreateTeam.ReadToken);
        
        app.MapGet("/playerlistsaver", PresentationCreateTeam.ResponseOfRequestPlayerListApi);//Save the Real Player in Database
        
        app.MapGet("/teamlistsaver", PresentationCreateTeam.ResponseOfRequestTeamList);//Save the Real Team in Database

        app.MapGet("/getteamlist", PresentationCreateTeam.GetTeamList);// return the realteams.
        
        app.MapGet("/getplayers", PresentationCreateTeam.GetPlayers);//Get Player by search type sort price team page
        
        app.MapPost("/addplayer", PresentationCreateTeam.AddPlayer);//Add Player in Team 
        
        app.MapGet("/playerofteam", PresentationCreateTeam.GetPlayersOfTeam);//Get Player in Team

        app.MapGet("/teamget", PresentationCreateTeam.GetTeamByEmail);

        app.MapGet("/teamisnotdone", PresentationCreateTeam.TeamIsNotDone);
        
        app.MapGet("/removeplayer", PresentationCreateTeam.ResponseOfRemovePlayer);

        app.MapGet("/getplayerbyname", PresentationCreateTeam.GetPlayerByName);
        
        app.MapGet("/getprice", PresentationCreateTeam.GetPrice);
        
        app.MapGet("/photourl", PresentationCreateTeam.GetPhotoUrl);
        
        app.MapGet("/cleartable", PresentationCreateTeam.DeleteTable);//Cleare the all rows in the table

        app.MapGet("/swap" ,PresentationCreateTeam.SwapPlayer);

        app.MapGet("/end", PresentationCreateTeam.CreateTeamIsDone);

        app.MapGet("/calculatescore", PresentationCreateTeam.Score);

    }
}
/*
    
*/