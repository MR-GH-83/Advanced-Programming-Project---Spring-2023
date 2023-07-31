using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Football_Fantasy;

public class Database : DbContext
{
    public DbSet<User> Users { get; set; }
    
    public DbSet<PlayerOfTeam> PlayersTeam { get; set; }
    public DbSet<Player> Players { get; set; }
    
    public DbSet<Team> Teams { get; set; }
    public DbSet<RealTeam> RealTeams { get; set; }
    public DbSet<OTP> Otps { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder contextOptionsBuilder)
    {
        contextOptionsBuilder.UseSqlite("Data source=database.db");
    }
}

public class PlayerOfTeam
{
        public string web_name { get; set; }
        public string player_position { get; set; }
        public string user_email { get; set; }
        [Key]
        public int primary_key { get; set; }
}
public class Team
{
    
    public string user_email { get; set; }
    public string user_id { get; set; }
    public string score { get; set; }
    public string price { get; set; } 
    
    public bool done { get; set; }
    
    [Key] public int primary_key { get; set; }
}


public class Player
{
    public string code { get; set; }
    public string element_type { get; set; }
    public string first_name { get; set; }
    public string id { get; set; }
    public string now_cost { get; set; }
    public string photo { get; set; }
    public string second_name { get; set; }
    public string team { get; set; }
    public string total_points { get; set; }
    public string web_name { get; set; }
    [Key]
    public int primary_key { get; set; }
}

public class RealTeam
{
    public string name { get; set; }
    public string id { get; set; }
    [Key]
    public int primary_key { get; set; }
}
public class User
{
    public string name { get; set; }
    public string email { get; set; }
    public string username { get; set; }
    public string password { get; set; }
    [Key]
    public int id { get; set; }
}

public class OTP
{
    public string name { get; set; }
    public string email { get; set; }
    public string username { get; set; }
    public string password { get; set; }
    [Key]
    public int primaryKey { get; set; }
}