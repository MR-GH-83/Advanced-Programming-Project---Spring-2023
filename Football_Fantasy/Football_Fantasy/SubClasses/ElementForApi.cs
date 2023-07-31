using Newtonsoft.Json;

namespace Football_Fantasy.SubClasses;

public class PlayerListClass
{
    public List<PlayerClassInPlayerList> elements { get; set; }
}

public class PlayerClassInPlayerList
{
    public string code { get; set; }
    public string element_type {get;set;}
    public string first_name { get; set; }
    public string id { get; set; }
    public string now_cost { get; set; }
    public string photo { get; set; }
    public string second_name { get; set; }
    public string team { get; set; }
    public string total_points { get; set; }
    public string web_name { get; set; }
}

public class TeamFromApi
{
    public List<TeamApi> teams { get; set; }
}

public class TeamApi
{
    public string id { get; set; }
    public string name { get; set; }
}