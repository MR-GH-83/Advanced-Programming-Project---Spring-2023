

let ElementIdOfPrevPlayer = "";
let PlayerSelected = [];
let NameOfPlayerSelected = [];
let IsAnyPostTypeSelected="Post-ALL";
let Search="";
let SortBy="";
let Price="";
let Team="";
let Page=1;
let SizePage=0;
let Email = getCookie("email");
console.log(Email);
// let Emial = prompt("Please enter your email for create team:");
RequestToServerForPlayer(Search,IsAnyPostTypeSelected.replace("Post-",""),SortBy,Price,Team,Page);
RequestToServerForTeam();
// CreateTeam();
// console.log(document.getElementById("tracklist_0").getAttribute("value"));





function IsThisNamePlayerChose(PlayerName)
{
    for(let i=0;i<NameOfPlayerSelected.length;i++)
    {
        if(NameOfPlayerSelected[i]==PlayerName)
        {
            return true;
        }

    }
    return false;
}


function AddPlayer(PlayerName)
{
    // if(IsThisNamePlayerChose(PlayerName))
    // {
    //    alert("This player has been selected, please select another one");
    // }
    // else
    // {
    //     let playerskin = document.getElementById(ElementIdOfPrevPlayer);
    //     playerskin.style.backgroundImage="url('https://s29.picofile.com/file/8464413926/Player_Jersey.png')";
    //     let num = GetPlayerNumber(ElementIdOfPrevPlayer,ElementIdOfPrevPlayer.length);
    //     let subtitle = "Sub_" + num;
    //     document.getElementById(subtitle).innerHTML=PlayerName;
    //     document.getElementById(subtitle).style.fontSize="12px";
    //     NameOfPlayerSelected.push(PlayerName);
    // }
    if (IsThisNamePlayerChose(PlayerName)) {
      alert("This player has been selected, please select another one");
    } else {
      let playerskin = document.getElementById(ElementIdOfPrevPlayer);
      playerskin.style.backgroundImage =
        "url('https://s29.picofile.com/file/8464413926/Player_Jersey.png')";
      let num = GetPlayerNumber(
        ElementIdOfPrevPlayer,
        ElementIdOfPrevPlayer.length
      );
      let subtitle = "Sub_" + num;
      document.getElementById(subtitle).innerHTML = PlayerName;
      document.getElementById(subtitle).style.fontSize = "12px";
      NameOfPlayerSelected.push(PlayerName);
      let request = {
        player: PlayerName,
        user_email: Email,
      };
      fetch("http://localhost:3001/addplayer", {
        method: "POST",
        headers: {
          Accept: "application/json",
          "Content-Type": "application/json",
        },
        body: JSON.stringify(request),
      })
        .then((response) => response.json())
        .then((data) => {
          console.log(data);
          if (data.status == "OK") {
            Price = data.price;
            document.getElementById("remaining").innerHTML = Price;
            document.getElementById("LengthOfPlayer").innerHTML =
              CountOfPlayer;
          }
        })
        .catch((error) => console.log(error));

    }
}















function CreateTeam()
{
    fetch("http://localhost:3001/createteam?user_email"+Email)
    .then(response=>response.json())
    .then(data=>{
        if(data.status=="Team")
        {
            ShowPreviousTeam(data.team);
        }
    })
    .catch(error=>console.log(error));
}
function GetEmailByRequestToServer()
{
    let token = getCookie("football_fantasy");
    const valueToken = {
      Token: token,
    };
    fetch("http://localhost:3001/authentication/read-token", {
      method: "POST",
      headers: {
        Accept: "application/json",
        "Content-Type": "application/json",
      },
      body: JSON.stringify(valueToken),
    })
      .then((response) => response.json())
      .then((data) => {
        console.log(data.massage);
        return data.massage;
      })
      .catch((error) => console.log(error));
}
function signOut()
{
    setCookie("football_fantasy","",1);
    window.location.replace("http://localhost:3001/");
}
function getCookie(cname) 
{
    let name = cname + "=";
    let ca = document.cookie.split(";");
    for (let i = 0; i < ca.length; i++) {
      let c = ca[i];
      while (c.charAt(0) == " ") {
        c = c.substring(1);
      }
      if (c.indexOf(name) == 0) {
        return c.substring(name.length, c.length);
      }
    }
    return "";
}
function setCookie(cname, cvalue, exdays) 
{
    const d = new Date();
    d.setTime(d.getTime() + exdays * 24 * 60 * 60 * 1000);
    let expires = "expires=" + d.toUTCString();
    document.cookie = cname + "=" + cvalue + ";" + expires + ";path=/";
}
function Reset()
{
    
    SortByPost("Post-ALL",IsAnyPostTypeSelected);
    PlayerSelector(ElementIdOfPrevPlayer);
    IsAnyPostTypeSelected="Post-ALL";
    Search="";
    SortBy="";
    Price="";
    Team="";
    Page=1;
    RequestToServerForPlayer(Search,IsAnyPostTypeSelected.replace("Post-",""),SortBy,Price,Team,Page);
}
function SortByPost(elementId,Prev) 
{
    if(Prev!=elementId)
    {
      if (document.getElementById(elementId).style.backgroundColor =="rgb(70, 69, 140)") 
      {
          document.getElementById(elementId).style.backgroundColor = "#c3c2fd";
      } 
      else 
      {
          document.getElementById(elementId).style.backgroundColor = "#46458C";
      }
      
        document.getElementById(Prev).style.backgroundColor="#c3c2fd";

      IsAnyPostTypeSelected=elementId; 
  }   
}

function RequestToServerForPlayer(search,type,sort,price,team,page)
{
    
    let api="http://localhost:3001/getplayers?search="+search+"&type="+type+"&sort="+sort+"&price="+price+"&team="+team+"&page="+page;
    console.log(api);
    fetch(api)
    .then(resposne=>resposne.json())
    .then(data=>{
      ShowPagation(data.sizeOfPagation,Page);
      ShowPlayerList(data.playerList);
    })
    .catch(error=>console.log(error));
}
function ShowPagation(Size,Page)
{
    document.getElementById("PresentPage").innerHTML=Page;
    SizePage=Size;
    document.getElementById("SizePagation").innerHTML=SizePage;    
}
function IsThisPlayerChosed(PlayerId)
{
    for(let i=0;i<PlayerSelected.length;i++)
    {
        if(PlayerSelected[i]==PlayerId)
        {
            return true;
        }
    }
    return false;
}
document.addEventListener("click", (e) => {
    let elementId = e.target.id;
    let className = e.target.className;
    if (elementId.search("Player_") == 0) {
      Page=1;
      console.log(elementId);
      console.log(ElementIdOfPrevPlayer);
      console.log(PlayerSelected);
      if(IsThisPlayerChosed(ElementIdOfPrevPlayer))
      {
          console.log(ElementIdOfPrevPlayer);
          ElementIdOfPrevPlayer="";
      }
      PlayerSelector(elementId);
      let type = "Post-"+getPostPlayer(elementId);
      if(ElementIdOfPrevPlayer=="")
          type="Post-ALL";
      SortByPost(type,IsAnyPostTypeSelected);
      RequestToServerForPlayer(Search,IsAnyPostTypeSelected.replace("Post-",""),SortBy,Price,Team,Page);


    }
    if (elementId.search("Post-") == 0) {
      Page=1;
      SortByPost(elementId,IsAnyPostTypeSelected);
      RequestToServerForPlayer(Search,IsAnyPostTypeSelected.replace("Post-",""),SortBy,Price,Team,Page);
    }
    if (elementId.search("P-") == 0) {
      
      Page=1;
      let backelement = elementId.replace("P-", "Post-");
      SortByPost(backelement,IsAnyPostTypeSelected);
      RequestToServerForPlayer(Search,IsAnyPostTypeSelected.replace("Post-",""),SortBy,Price,Team,Page);
    }
    if(elementId.search("Price")==0)
    {
        Page=1;
        console.log(elementId);
        Price=document.getElementById("Price").value;
        RequestToServerForPlayer(Search,IsAnyPostTypeSelected.replace("Post-",""),SortBy,Price,Team,Page);
    }
    if(elementId.search("Selector-Search")==0)
    {
        Page=1;
        let search=document.getElementById("Selector-Search");
        search.addEventListener("keyup",(e)=>{
          Search=document.getElementById("Selector-Search").value;
          RequestToServerForPlayer(Search,IsAnyPostTypeSelected.replace("Post-",""),SortBy,Price,Team,Page);
        })
    }
    if(elementId.search("Sort")==0)
    {
        if(document.getElementById("Sort").value!=SortBy)
        {
            Page=1;
            SortBy=document.getElementById("Sort").value;
            RequestToServerForPlayer(Search,IsAnyPostTypeSelected.replace("Post-",""),SortBy,Price,Team,Page);
        }
    }
    if(elementId.search("Team")==0)
    {
        if(document.getElementById("Team").value!=Team)
        {
            Page=1;
            Team=document.getElementById("Team").value;
            RequestToServerForPlayer(Search,IsAnyPostTypeSelected.replace("Post-",""),SortBy,Price,Team,Page);
        }
    }
    if(elementId.search("tracklist")==0 || elementId.search("logoplayer")==0  || elementId.search("nameplayer")==0  || elementId.search("teamplayer")==0  || elementId.search("priceplayer")==0  || elementId.search("postplayer")==0  || elementId.search("totalpoint")==0 )
    {
        let player = document.getElementById(elementId);
        let name = player.getAttribute("value");
        if(ElementIdOfPrevPlayer!="")
        {
            PlayerSelected.push(ElementIdOfPrevPlayer);
            AddPlayer(name);
        }
    }
    // console.log(elementId);
  });

function GetPlayerNumber(str,len)
{
    let result = "";
    for(let i = len;i>=0;i--)
    {
        if(str[i]=='_')
        {
            result = str.slice(i+1,len);
            break;
        }
    }
    return result;
}
function RequestToServerForTeam()
{
    fetch("http://localhost:3001/getteamlist")
    .then(response=>response.json())
    .then(data=>ShowTeamInSelectTag(data.teamList))
    .catch(error=>console.log(error));
}
function IdOfOption(Num)
{
    return "Team_"+Num
}
function ChangeOptionToShow(Tag,TeamList,NumberTeam)
{
    Tag.setAttribute("id",IdOfOption(NumberTeam));
    Tag.setAttribute("value",TeamList[NumberTeam].name);
    Tag.innerHTML=TeamList[NumberTeam].name;
    return Tag;
}
function ShowTeamInSelectTag(TeamList)
{
    /*
          <select id="Team">
            <option id="Team_0" value="">Team</option>
            
          </select>
    */
    document.getElementById("Team").remove();
    var father = document.getElementById("TTeam");
    var select= document.createElement("select");
    var option = document.createElement("option");
    select.setAttribute("id","Team");
    option.setAttribute("id","Team_0");
    option.setAttribute("value","");
    option.innerHTML="Team";
    select.appendChild(option);
    father.appendChild(select);
    let numcopies=20;
    let orginalDiv = document.getElementById("Team_0");
    for(let i=1;i<=numcopies;i++)
    {
        let CloneDiv = orginalDiv.cloneNode(true);
        document.getElementById("Team").appendChild(ChangeOptionToShow(CloneDiv,TeamList,i));
    }
    
    
}
function GetListPlayer(elements,teams)
{
    let PlayerList=[];
    for(let i in elements)
    {
        let temp=
        {
            code: elements[i].code,
            element_type:ConvertElementTypeToName(elements[i].element_type),
            first_name: elements[i].first_name,
            id:elements[i].id,
            now_cost:elements[i].now_cost/10,
            photo:"p"+elements[i].code+".png",
            second_name:elements[i].second_name,
            team:ConvertTeamIDToName(teams,elements[i].team),
            total_points:elements[i].total_points,
            web_name:elements[i].web_name,
            primary_key:i
        };
        PlayerList.push(temp);
    }
    return PlayerList;
}
function NewDivId(i)
{
    return "tracklist_"+i;
}
function NewLogoId(i)
{
    return "P_"+i;
}
function InformationOfNewDiv(PlayerList,DivTag,PlayerNum)
{   
    let DivIdNum = PlayerNum+1;
    let DivId = NewDivId(DivIdNum);
    DivTag.setAttribute("value",PlayerList[PlayerNum].web_name);
    DivTag.setAttribute("id",DivId);
    DivTag.style.display="block";
    var logoplayer = DivTag.querySelector('#P_0');
    var nameplayer = DivTag.querySelector('.nameplayer');
    var teamplayer = DivTag.querySelector('.teamplayer');
    var priceplayer = DivTag.querySelector('.priceplayer');
    var postplayer = DivTag.querySelector('.postplayer');
    var totalpoint = DivTag.querySelector('.totalpoint'); 
    logoplayer.setAttribute("value",PlayerList[PlayerNum].web_name);
    nameplayer.setAttribute("value",PlayerList[PlayerNum].web_name);
    teamplayer.setAttribute("value",PlayerList[PlayerNum].web_name);
    priceplayer.setAttribute("value",PlayerList[PlayerNum].web_name);
    postplayer.setAttribute("value",PlayerList[PlayerNum].web_name);
    totalpoint.setAttribute("value",PlayerList[PlayerNum].web_name);

    logoplayer.setAttribute("id","P_"+DivIdNum);
    nameplayer.setAttribute("id","nameplayer_"+DivIdNum);
    teamplayer.setAttribute("id","teamplayer_"+DivIdNum);
    priceplayer.setAttribute("id","priceplyer_"+DivIdNum);
    postplayer.setAttribute("id","postplayer_"+DivIdNum);
    totalpoint.setAttribute("id","totalpoint_"+DivIdNum);
    logoplayer.setAttribute('id',NewLogoId(DivIdNum));
 
    let UrlPhoto="url('https://resources.premierleague.com/premierleague/photos/players/250x250/"+PlayerList[PlayerNum].photo+"')";
    logoplayer.style.backgroundImage=UrlPhoto;  
    nameplayer.innerHTML=PlayerList[PlayerNum].web_name;
    teamplayer.innerHTML=PlayerList[PlayerNum].team;
    priceplayer.innerHTML="Є"+PlayerList[PlayerNum].now_cost;
    postplayer.innerHTML=PlayerList[PlayerNum].element_type;
    totalpoint.innerHTML=PlayerList[PlayerNum].total_points;
    return DivTag;
}
function ShowPlayerList(PlayerList)
{
    //<div class="ListOfPlayer" id="ListOfPlayer">
    document.getElementById("ListOfPlayer").remove();
    var ListDiv= document.createElement("div");
    ListDiv.classList.add("ListOfPlayer");
    ListDiv.setAttribute("id","ListOfPlayer");    
    ListDiv.appendChild(CreateTrackList());
    document.getElementById("downbox").appendChild(ListDiv);
    var orginalDiv=document.getElementById("tracklist_0");
    var numCopies=50;
    for(var i=0;i<numCopies;i++)
    {
        var CloneDiv=orginalDiv.cloneNode(true);
        document.getElementById("ListOfPlayer").appendChild(InformationOfNewDiv(PlayerList,CloneDiv,i));
    }
}
function stringToInt(str) {
    var num = parseInt(str);
    if (num == str) return num;
    return NaN;
  }
function getPostPlayer(elementId) {
    if (elementId.search("Player_") != 0) {
      return "";
    }
    elementId = elementId.replace("Player_", "");
    let numplayer = stringToInt(elementId);
    if (numplayer == 1 || numplayer == 12) {
      return "GK";
    }
    if ((numplayer <= 5 && numplayer >= 2) || numplayer == 13) {
      return "DEF";
    }
    if ((numplayer <= 9 && numplayer >= 6) || numplayer == 14) {
      return "MID";
    }
    if ((numplayer <= 11 && numplayer >= 10) || numplayer == 15) {
      return "FWD";
    }
  }
  function PlayerSelector(elementId) {

    let urlDefault =
      'url("https://gaming.uefa.com/en/uclfantasy/static-assets/ucl2020/images/Jersey-default.svg")';
    let urlSelect =
      'url("https://gaming.uefa.com/en/uclfantasy/static-assets/ucl2020/images/Jersey-selected.svg")';
    if (elementId == ElementIdOfPrevPlayer) {
      if (document.getElementById(elementId).style.backgroundImage ==urlDefault) 
      {
        document.getElementById(elementId).style.backgroundImage = urlSelect;
        return "";
      } 
      else{
        document.getElementById(elementId).style.backgroundImage = urlDefault;
      }
      ElementIdOfPrevPlayer="";
      return "";
    } else {
      if (ElementIdOfPrevPlayer != "") {
        document.getElementById(
          ElementIdOfPrevPlayer
        ).style.backgroundImage = urlDefault;
      }
      document.getElementById(elementId).style.backgroundImage = urlSelect;
      ElementIdOfPrevPlayer = elementId;
    }
  }
  function PriceRemover(elementId) {
    let PriceValue = document.getElementById("Price").value;
    if (PriceValue == 0) {
      document.getElementById("Price_Remove").style.display = "none";
    } else {
      document.getElementById("Price_Remove").style.display = "block";
    }
  }
function CreateTrackList()
{
    /*
      <div class="tracklist" id="tracklist_0">
              <div class="logoplayer" id="P_0"></div>
              <div class="nameplayer"></div>
              <div class="teamplayer"></div>
              <div class="priceplayer"></div>
              <div class="postplayer"></div>
              <div class="totalpoint"></div> 
    */
    var tracklist=document.createElement("div");
    tracklist.classList.add("tracklist");
    tracklist.setAttribute("id","tracklist_0");
    tracklist.setAttribute("value","0");

    var logo=document.createElement("div");
    logo.classList.add("logoplayer");
    logo.setAttribute("id","P_0");
    logo.setAttribute("value","0");


    var name=document.createElement("div");
    name.classList.add("nameplayer");
    name.setAttribute("value","0");


    var teamplayer=document.createElement("div");
    teamplayer.classList.add("teamplayer");
    teamplayer.setAttribute("value","0");


    var price=document.createElement("div");
    price.classList.add("priceplayer");
    price.setAttribute("value","0");


    var postplayer=document.createElement("div");
    postplayer.classList.add("postplayer");
    postplayer.setAttribute("value","0");


    var totalpoint=document.createElement("div");
    totalpoint.classList.add("totalpoint");
    totalpoint.setAttribute("value","0");


    tracklist.appendChild(logo);
    tracklist.appendChild(name);
    tracklist.appendChild(teamplayer);
    tracklist.appendChild(price);
    tracklist.appendChild(postplayer);
    tracklist.appendChild(totalpoint);

    return tracklist;
}
function PrevPage()
{
    let page = document.getElementById("PresentPage").innerHTML;
    if(page!="1")
    {
        document.getElementById("PresentPage").innerHTML=(stringToInt(page)-1).toString();
        Page--;
        RequestToServerForPlayer(Search,IsAnyPostTypeSelected.replace("Post-",""),SortBy,Price,Team,Page);
    }
}
function NextPage()
{
    let page = document.getElementById("PresentPage").innerHTML;
    if(page!=SizePage)
    {   
        document.getElementById("PresentPage").innerHTML=(stringToInt(page)+1).toString();
        Page++;
        RequestToServerForPlayer(Search,IsAnyPostTypeSelected.replace("Post-",""),SortBy,Price,Team,Page);
    }
}
