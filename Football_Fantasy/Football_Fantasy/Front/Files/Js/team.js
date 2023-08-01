let ElementIdOfPrevPlayer = "";
let PlayerSelected = [];
let NameOfPlayerSelected = [];
let IsAnyPostTypeSelected = "Post-ALL";
let Search = "";
let SortBy = "";
let Price = "";
let Team = "";
let Page = 1;
let Remaining = "105";
let Email = GetEmail();
let Token = "";
let SizePage = 0;
let CountOfPlayer = PlayerSelected.length;
let SwapStatus = false;
let PathPhoto = "../Img/Players/";
let PathDefaultJersey = "../Img/Photage/Jersey-default.svg";
let PathSelectedJersey = "../Img/Photage/Jersey-selected.svg";
let PathPlayerJersey = "../Img/Photage/Player-Jersey.png";
let PathLogoPlayer = "../Img/Photage/Player.svg";
let SwapPlayer = {
  web_name: "",
  position: "",
  post: "",
};
let SwapPrev = "";
if(IsThisUserWasLogin().status=="Fail")
{
    window.location.replace("./sign_in.html");
}
else
{
    let search = window.location.search;
    let token = VlaueOfSearch(search);
    Token = token;
    document.getElementById("home").querySelector("a").setAttribute("href","../../index.html?auth=1&value="+token);
    document.getElementById("load").style.display="none";
    document.getElementById("bod").style.display="block";
}
RequestToServerForPlayer(
  Search,
  IsAnyPostTypeSelected.replace("Post-", ""),
  SortBy,
  Price,
  Team,
  Page
);
RequestToServerForTeam();
CreateTeam();
//GetRemainingFromServer(Remaining);
function TeamDone(){
  let count = document.getElementById("LengthOfPlayer").innerHTML;
  let but = document.getElementById("TeamIsDone");
  but.style.filter = "brightness(100%)";
  but.style.color = "#000000";
  but.style.cursor = "pointer";
  if (stringToInt(count) < 15) {
    but.style.backgroundColor = "#5af7dd";
    but.style.filter="brightness(60%)";
    fetch("http://localhost:3001/teamisnotdone?user_email=" + Email)
      .then((response) => response.json())
      .then((data) => console.log(data))
      .catch((error) => console.log(error));
  }
}
function FinishJob() {
  let count = document.getElementById("LengthOfPlayer").innerHTML;
  if (count == "15") {
    fetch("http://localhost:3001/end?user_email=" + Email)
      .then((response) => response.json())
      .then((data) => {})
      .catch((error) => console.log(error));
    window.location.replace("index.html");
  }
}
function GetRemainingFromServer(Remaining) {
  fetch("http://localhost:3001/getprice?user_email=" + Email)
    .then((response) => response.json())
    .then((data) => {
      Remaining = data.massage;
      document.getElementById("remaining").innerHTML = Remaining;
      //console.log(data);
    })
    .catch((error) => console.log(erro));
}
function GetPhoto(link, count, playerskin) {
  let url = link.slice(5, count - 2);
  console.log(url);
  fetch(url)
    .then((data) => console.log(data))
    .catch((error) => {
      throw new Error("Not working");
    });
}
function IsThisNamePlayerChose(PlayerName) {
  for (let i = 0; i < NameOfPlayerSelected.length; i++) {
    if (NameOfPlayerSelected[i] == PlayerName) {
      return true;
    }
  }
  return false;
}
function AddPlayer(PlayerName, playernumber) {
  if (IsThisNamePlayerChose(PlayerName)) {
    alert("This player has been selected, please select another one");
  } else {
    let request = {
      player: PlayerName,
      player_position: ElementIdOfPrevPlayer,
      user_email: Email,
    };
    //console.log(request);
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
          let playerskin = document.getElementById(ElementIdOfPrevPlayer);
          PlayerSelected.push(ElementIdOfPrevPlayer);
          document
            .getElementById(ElementIdOfPrevPlayer)
            .querySelector(".remove").style.display = "block";
          let UrlPhoto =PathPhoto+data.name+".png";
          loadPlayer(UrlPhoto,playerskin);
          let num = GetPlayerNumber(
            ElementIdOfPrevPlayer,
            ElementIdOfPrevPlayer.length
          );
          let subtitle = "Sub_" + num;
          document.getElementById(subtitle).innerHTML = PlayerName;
          document.getElementById(subtitle).style.fontSize = "12px";
          NameOfPlayerSelected.push(PlayerName);
          Remaining = document.getElementById("remaining").innerHTML;
          Remaining = data.price;
          document.getElementById("remaining").innerHTML = Remaining;
          CountOfPlayer = PlayerSelected.length;
          document.getElementById("LengthOfPlayer").innerHTML = CountOfPlayer;
          console.log(data);
          console.log(PlayerName);
          ElementIdOfPrevPlayer = "";
          if (data.count == 15) {
            TeamDone();
            CountOfPlayer = 15;
          }

          if (stringToInt(num) >= 12) {
            document.getElementById(
              "swapicon_" + stringToInt(num)
            ).style.display = "block";
          }
        }
        if (data.status == "Fail") {
          alert(data.massage);
        }
      })
      .catch((error) => console.log(error));
  }
}
function ShowRemaining(remaining) {
  Remaining = document.getElementById("remaining").innerHTML;
  document.getElementById("remaining").innerHTML = remaining;
  Remaining = remaining;
}
function CreateTeam() {
  let url = "http://localhost:3001/createteam?user_email=" + Email;
  fetch(url)
    .then((response) => response.json())
    .then((data) => {
      ShowRemaining(data.team.price);
      Remaining = data.team.price;
      CountOfPlayer = data.countplayer;

      document.getElementById("LengthOfPlayer").innerHTML = data.countplayer;
      if ((data.massage = "You already has a team.")) {
        ShowTeamFromDatabase(data.playersteam);
      }
    })
    .catch((error) => console.log(error));
}
function ShowTeamFromDatabase(PlayerList) {
  /*
              <div class="player" id="Player_04">
                <div class="subtitle" id="Sub_04">DEF</div>
                <div class="remove" id="Remove_04">
                  <i class="fa fa-remove" id="RemoveIcon_04" style="color: red"></i>
                </div>
              </div>
              let playerskin = document.getElementById(ElementIdOfPrevPlayer);
                PlayerSelected.push(ElementIdOfPrevPlayer);
                document.getElementById(ElementIdOfPrevPlayer).querySelector(".remove").style.display = "block";
                playerskin.style.backgroundImage = "url('" + data.photo + "')";
                console.log(playerskin.style.backgroundImage);
                let num = GetPlayerNumber(ElementIdOfPrevPlayer,ElementIdOfPrevPlayer.length);
                let subtitle = "Sub_" + num;
                document.getElementById(subtitle).innerHTML = PlayerName;
                document.getElementById(subtitle).style.fontSize = "12px";
                NameOfPlayerSelected.push(PlayerName);
                Remaining = data.price;
                document.getElementById("remaining").innerHTML = Remaining;
                CountOfPlayer = PlayerSelected.length;
                document.getElementById("LengthOfPlayer").innerHTML =CountOfPlayer;

                ElementIdOfPrevPlayer = "";



          */
  for (let i = 0; i < PlayerList.length; i++) {
    let UrlPhoto = PathPhoto +PlayerList[i].web_name+".png";
    PlayerSelected.push(PlayerList[i].player_position);
    NameOfPlayerSelected.push(PlayerList[i].web_name);
    let player = document.getElementById(PlayerList[i].player_position);
    let subtitle = player.querySelector(".subtitle");
    let remove = player.querySelector(".remove");
    loadPlayer(UrlPhoto,player);
    remove.style.display = "block";
    subtitle.innerHTML = PlayerList[i].web_name;
    subtitle.style.fontSize = "12px";
    //GetUrlPhoto(PlayerList[i].web_name,url);
    let number = GetPlayerNumber(
      PlayerList[i].player_position,
      PlayerList[i].player_position.length
    );
    number = stringToInt(number);
    if (number >= 12) {
      document.getElementById("swapicon_" + number).style.display = "block";
    }
    if (document.getElementById("LengthOfPlayer").innerHTML == "15") {
      TeamDone();
    }
  }
}
function GetUrlPhoto(web_name, url) {
  fetch("http://localhost:3001/photourl?web_name=" + web_name)
    .then((response) => response.json())
    .then((data) => (url = data.url))
    .catch((error) => console.log(error));
}
function signOut() {
  setCookie("football_fantasy", "", 1);
  window.location.replace("http://localhost:3001/");
}
function getCookie(cname) {
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
function setCookie(cname, cvalue, exdays) {
  const d = new Date();
  d.setTime(d.getTime() + exdays * 24 * 60 * 60 * 1000);
  let expires = "expires=" + d.toUTCString();
  document.cookie = cname + "=" + cvalue + ";" + expires + ";path=/";
}
function Reset() {
  SortByPost("Post-ALL", IsAnyPostTypeSelected);
  PlayerSelector(ElementIdOfPrevPlayer);
  IsAnyPostTypeSelected = "Post-ALL";
  Search = "";
  SortBy = "";
  Price = "";
  Team = "";
  Page = 1;
  RequestToServerForPlayer(
    Search,
    IsAnyPostTypeSelected.replace("Post-", ""),
    SortBy,
    Price,
    Team,
    Page
  );
}
function SortByPost(elementId, Prev) {
    console.log(document.getElementById(elementId).style.backgroundColor);
  if (Prev != elementId) {
    if (
      document.getElementById(elementId).style.backgroundColor ==
      "rgb(3 ,10 ,96 )"
      //rgb(70, 69, 140) 90 247 220
    ) {
      document.getElementById(elementId).style.backgroundColor = "#ffffff";
    } else {
      document.getElementById(elementId).style.backgroundColor = "#030a60";

    }
    document.getElementById(elementId).querySelector("span").style.color="#ffffff";
    document.getElementById(Prev).querySelector("span").style.color="#000000";
    document.getElementById(Prev).style.backgroundColor = "#ffffff";

    IsAnyPostTypeSelected = elementId;
  }
}
function RequestToServerForPlayer(search, type, sort, price, team, page) {
  let api =
    "http://localhost:3001/getplayers?search=" +
    search +
    "&type=" +
    type +
    "&sort=" +
    sort +
    "&price=" +
    price +
    "&team=" +
    team +
    "&page=" +
    page;
  //console.log(api);
  fetch(api)
    .then((resposne) => resposne.json())
    .then((data) => {
      ShowPagation(data.sizeOfPagation, Page);
      ShowPlayerList(data.playerList);
    })
    .catch((error) => console.log(error));
}
function ShowPagation(Size, Page) {
  document.getElementById("PresentPage").innerHTML = Page;
  SizePage = Size;
  document.getElementById("SizePagation").innerHTML = SizePage;
}
function IsThisPlayerChosed(PlayerId) {
  for (let i = 0; i < PlayerSelected.length; i++) {
    if (PlayerSelected[i] == PlayerId) {
      return true;
    }
  }
  return false;
}
document.addEventListener("keyup", (e) => {
  //console.log(e);
  let key = e.key;
  if (SwapStatus == true && key == "Escape") {
    for (let i = 1; i <= 11; i++) {
      let num = GetForNumber(i);
      if (GetPostByNumber(i) == SwapPlayer.post) {
        document.getElementById("Player_" + num).style.animation = "none";
      }
    }
    SwapStatus = false;
  }
});
document.addEventListener("click", (e) => {
  let elementId = e.target.id;
  let className = e.target.className;
  //console.log(elementId);
  if (
    elementId.search("Player_") != 0 &&
    SwapStatus == true &&
    SwapPrev != elementId
  ) {
    alert("Please choose a player for replace.");
  }
  if (elementId.search("Player_") == 0) {
    //console.log(elementId);
    //console.log(ElementIdOfPrevPlayer);
    //console.log(PlayerSelected);
    if (SwapStatus == true) {
      let post_player = getPostPlayer(elementId);
      if (post_player == SwapPlayer.post) {
        let num = GetPlayerNumber(elementId, elementId.length);
        let name = document.getElementById("Sub_" + num).innerHTML;
        let SwapTemp = {
          web_name: name,
          position: elementId,
          post: post_player,
        };
        if(ElementIdOfPrevPlayer.indexOf(elementId)!=-1)
        {
            alert("Wrong.");
        }
        else
        {
            let player = document.getElementById(elementId);
            player.querySelector(".subtitle").innerHTML = SwapPlayer.web_name;
            let UrlPhoto_1 =PathPhoto+SwapPlayer.web_name+".png";
            loadPlayer(UrlPhoto_1,player);
            let player_replace = document.getElementById(SwapPlayer.position);
            player_replace.querySelector(".subtitle").innerHTML = SwapTemp.web_name;
            let UrlPhoto_2 = PathPhoto+SwapTemp.web_name+".png";
            loadPlayer(UrlPhoto_2,player_replace);
            for (let i = 1; i <= 11; i++) {
              if (GetPostByNumber(i) == post_player) {
                let player = document.getElementById("Player_" + GetForNumber(i));
                player.style.animation = "none";
              }
            }
            fetch(
              "http://localhost:3001/swap?player_one=" +
                SwapPlayer.web_name +
                "&player_two=" +
                SwapTemp.web_name +
                "&user_email=" +
                Email
            )
              .then((response) => response.json())
              .then((data) => {
                console.log(data);
              })
              .catch((error) => console.log(error));
            SwapStatus = false;
        }
      } else {
        alert("Please choose a player with the right post.");
      }
    }
    if (SwapStatus == false) {
      Page = 1;
      if (IsThisPlayerChosed(ElementIdOfPrevPlayer)) {
        ElementIdOfPrevPlayer = "";
      }
      if (IsThisPlayerChosed(elementId) == false) {
        PlayerSelector(elementId);
        let type = "Post-" + getPostPlayer(elementId);
        if (ElementIdOfPrevPlayer == "") type = "Post-ALL";
        SortByPost(type, IsAnyPostTypeSelected);
        RequestToServerForPlayer(
          Search,
          IsAnyPostTypeSelected.replace("Post-", ""),
          SortBy,
          Price,
          Team,
          Page
        );
      }
    }
  }
  if (elementId.search("Post-") == 0 && SwapStatus == false) {
    Page = 1;
    SortByPost(elementId, IsAnyPostTypeSelected);
    RequestToServerForPlayer(
      Search,
      IsAnyPostTypeSelected.replace("Post-", ""),
      SortBy,
      Price,
      Team,
      Page
    );
    if (elementId == "Post-ALL" && ElementIdOfPrevPlayer != "") {
      PlayerSelector(ElementIdOfPrevPlayer);
      ElementIdOfPrevPlayer = "";
    }
    let post = elementId.replace("Post-", "");
    if (post != getPostPlayer(ElementIdOfPrevPlayer)) {
      PlayerSelector(ElementIdOfPrevPlayer);
      ElementIdOfPrevPlayer = "";
    }
  }
  if (elementId.search("P-") == 0 && SwapStatus == false) {
    Page = 1;
    let backelement = elementId.replace("P-", "Post-");
    SortByPost(backelement, IsAnyPostTypeSelected);
    RequestToServerForPlayer(
      Search,
      IsAnyPostTypeSelected.replace("Post-", ""),
      SortBy,
      Price,
      Team,
      Page
    );
    if (elementId == "P-ALL" && ElementIdOfPrevPlayer != "") {
      PlayerSelector(ElementIdOfPrevPlayer);
      ElementIdOfPrevPlayer = "";
    }
    let post = elementId.replace("P-", "");
    if (post != getPostPlayer(ElementIdOfPrevPlayer)) {
      PlayerSelector(ElementIdOfPrevPlayer);
      ElementIdOfPrevPlayer = "";
    }
  }
  if (elementId.search("Price") == 0 && SwapStatus == false) {
    Page = 1;
    //console.log(elementId);
    Price = document.getElementById("Price").value;
    RequestToServerForPlayer(
      Search,
      IsAnyPostTypeSelected.replace("Post-", ""),
      SortBy,
      Price,
      Team,
      Page
    );
  }
  if (elementId.search("Selector-Search") == 0 && SwapStatus == false) {
    Page = 1;
    let search = document.getElementById("Selector-Search");
    search.addEventListener("keyup", (e) => {
      Search = document.getElementById("Selector-Search").value;
      RequestToServerForPlayer(
        Search,
        IsAnyPostTypeSelected.replace("Post-", ""),
        SortBy,
        Price,
        Team,
        Page
      );
    });
  }
  if (elementId.search("Sort") == 0 && SwapStatus == false) {
    if (document.getElementById("Sort").value != SortBy) {
      Page = 1;
      SortBy = document.getElementById("Sort").value;
      RequestToServerForPlayer(
        Search,
        IsAnyPostTypeSelected.replace("Post-", ""),
        SortBy,
        Price,
        Team,
        Page
      );
    }
  }
  if (elementId.search("Team") == 0 && SwapStatus == false) {
    if (document.getElementById("Team").value != Team) {
      Page = 1;
      Team = document.getElementById("Team").value;
      RequestToServerForPlayer(
        Search,
        IsAnyPostTypeSelected.replace("Post-", ""),
        SortBy,
        Price,
        Team,
        Page
      );
    }
  }
  if (
    elementId.search("tracklist") == 0 ||
    elementId.search("logoplayer") == 0 ||
    elementId.search("nameplayer") == 0 ||
    elementId.search("teamplayer") == 0 ||
    elementId.search("priceplayer") == 0 ||
    elementId.search("postplayer") == 0 ||
    (elementId.search("totalpoint") == 0 && SwapStatus == false)
  ) {
    let number = GetPlayerNumber(elementId, elementId.length);
    let player = document.getElementById(elementId);
    let name = player.getAttribute("value");
    if (ElementIdOfPrevPlayer != "") {
      AddPlayer(name, number);
    }
  }
  // console.log(elementId);
  if (
    elementId.search("RemoveIcon") == 0 ||
    (elementId.search("Remove") == 0 && SwapStatus == false)
  ) {
    let playerid = "Player_" + GetPlayerNumber(elementId, elementId.length);

    let subtitle = "Sub_" + GetPlayerNumber(elementId, elementId.length);
    //console.log(subtitle);
    RemovePlayerFromServer(
      document.getElementById(subtitle).innerHTML,
      Email,
      playerid
    );
  }
  if (SwapStatus == true) {
  }
  if (elementId.search("booticon") == 0 || elementId.search("swapicon_") == 0) {
    console.log(elementId);
    // if (SwapStatus == true && SwapPrev == elementId) {
    //   let post = getPostPlayer(elementId);
    //   for (let i = 1; i <= 11; i++) {
    //     if (GetPostByNumber(i) == post) {
    //       document.getElementById(
    //         "Player_" + GetForNumber(i)
    //       ).style.animation = "none";
    //     }
    //   }
    //   SwapPrev = "";
    // }
    alert("If you want to go back press 'Escape'. ");
    if (SwapStatus == false) {
      let number = GetPlayerNumber(elementId, elementId.length);
      SwapStatus = true;
      let main_player = "Player_" + number;
      let post = getPostPlayer(main_player);
      for (let i = 1; i <= 11; i++) {
        let num = GetForNumber(i);
        let player = document.getElementById("Player_" + num);
        if (GetPostByNumber(i) == post) {
          player.style.animation = "shake 0.8s";
          player.style.animationIterationCount = "infinite";
        }
      }
      let web_name = document.getElementById("Sub_" + number).innerHTML;
      SwapPlayer.web_name = web_name;
      SwapPlayer.position = "Player_" + number;
      SwapPlayer.post = getPostPlayer(SwapPlayer.position);
      SwapPrev = elementId;
    }
    // if (SwapStatus == true && SwapPrev == "") {
    //   SwapStatus = false;
    // }
  }
});
function GetPostByNumber(numplayer) {
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
function GetForNumber(i) {
  if (i < 10) {
    return "0" + i;
  }
  return i;
}
function GetPlayerNumber(str, len) {
  let result = "";
  for (let i = len; i >= 0; i--) {
    if (str[i] == "_") {
      result = str.slice(i + 1, len);
      break;
    }
  }
  return result;
}
function RequestToServerForTeam() {
  fetch("http://localhost:3001/getteamlist")
    .then((response) => response.json())
    .then((data) => ShowTeamInSelectTag(data.teamList))
    .catch((error) => console.log(error));
}
function IdOfOption(Num) {
  return "Team_" + Num;
}
function ChangeOptionToShow(Tag, TeamList, NumberTeam) {
  Tag.setAttribute("id", IdOfOption(NumberTeam));
  Tag.setAttribute("value", TeamList[NumberTeam].name);
  Tag.innerHTML = TeamList[NumberTeam].name;
  return Tag;
}
function ShowTeamInSelectTag(TeamList) {
  /*
                    <select id="Team">
                      <option id="Team_0" value="">Team</option>

                    </select>
              */
  document.getElementById("Team").remove();
  var father = document.getElementById("TTeam");
  var select = document.createElement("select");
  var option = document.createElement("option");
  select.setAttribute("id", "Team");
  option.setAttribute("id", "Team_0");
  option.setAttribute("value", "");
  option.innerHTML = "Team";
  select.appendChild(option);
  father.appendChild(select);
  let numcopies = 20;
  let orginalDiv = document.getElementById("Team_0");
  for (let i = 1; i <= numcopies; i++) {
    let CloneDiv = orginalDiv.cloneNode(true);
    document
      .getElementById("Team")
      .appendChild(ChangeOptionToShow(CloneDiv, TeamList, i));
  }
}
function GetListPlayer(elements, teams) {
  let PlayerList = [];
  for (let i in elements) {
    let temp = {
      code: elements[i].code,
      element_type: ConvertElementTypeToName(elements[i].element_type),
      first_name: elements[i].first_name,
      id: elements[i].id,
      now_cost: elements[i].now_cost / 10,
      photo: "p" + elements[i].code + ".png",
      second_name: elements[i].second_name,
      team: ConvertTeamIDToName(teams, elements[i].team),
      total_points: elements[i].total_points,
      web_name: elements[i].web_name,
      primary_key: i,
    };
    PlayerList.push(temp);
  }
  return PlayerList;
}
function NewDivId(i) {
  return "tracklist_" + i;
}
function NewLogoId(i) {
  return "P_" + i;
}
function InformationOfNewDiv(PlayerList, DivTag, PlayerNum) {
  let DivIdNum = PlayerNum + 1;
  let DivId = NewDivId(DivIdNum);
  DivTag.setAttribute("value", PlayerList[PlayerNum].web_name);
  DivTag.setAttribute("id", DivId);
  DivTag.style.display = "block";
  var logoplayer = DivTag.querySelector("#P_0");
  var nameplayer = DivTag.querySelector(".nameplayer");
  var teamplayer = DivTag.querySelector(".teamplayer");
  var priceplayer = DivTag.querySelector(".priceplayer");
  var postplayer = DivTag.querySelector(".postplayer");
  var totalpoint = DivTag.querySelector(".totalpoint");
  logoplayer.setAttribute("value", PlayerList[PlayerNum].web_name);
  nameplayer.setAttribute("value", PlayerList[PlayerNum].web_name);
  teamplayer.setAttribute("value", PlayerList[PlayerNum].web_name);
  priceplayer.setAttribute("value", PlayerList[PlayerNum].web_name);
  postplayer.setAttribute("value", PlayerList[PlayerNum].web_name);
  totalpoint.setAttribute("value", PlayerList[PlayerNum].web_name);

  logoplayer.setAttribute("id", "P_" + DivIdNum);
  nameplayer.setAttribute("id", "nameplayer_" + DivIdNum);
  teamplayer.setAttribute("id", "teamplayer_" + DivIdNum);
  priceplayer.setAttribute("id", "priceplyer_" + DivIdNum);
  postplayer.setAttribute("id", "postplayer_" + DivIdNum);
  totalpoint.setAttribute("id", "totalpoint_" + DivIdNum);
  logoplayer.setAttribute("id", NewLogoId(DivIdNum));
  let UrlPhoto = PathPhoto +PlayerList[PlayerNum].web_name+".png";
  loadImage(UrlPhoto,logoplayer);

  nameplayer.innerHTML = PlayerList[PlayerNum].web_name;
  teamplayer.innerHTML = PlayerList[PlayerNum].team;
  priceplayer.innerHTML = "Є" + PlayerList[PlayerNum].now_cost;
  postplayer.innerHTML = PlayerList[PlayerNum].element_type;
  totalpoint.innerHTML = PlayerList[PlayerNum].total_points;
  return DivTag;
}
function ShowPlayerList(PlayerList) {
  //<div class="ListOfPlayer" id="ListOfPlayer">
  document.getElementById("ListOfPlayer").remove();
  var ListDiv = document.createElement("div");
  ListDiv.classList.add("ListOfPlayer");
  ListDiv.setAttribute("id", "ListOfPlayer");
  ListDiv.appendChild(CreateTrackList());
  document.getElementById("downbox").appendChild(ListDiv);
  var orginalDiv = document.getElementById("tracklist_0");
  var numCopies = 50;
  for (var i = 0; i < numCopies; i++) {
    var CloneDiv = orginalDiv.cloneNode(true);
    document
      .getElementById("ListOfPlayer")
      .appendChild(InformationOfNewDiv(PlayerList, CloneDiv, i));
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

  if (elementId == ElementIdOfPrevPlayer) {
    if (
      document.getElementById(elementId).style.backgroundImage == "url('"+PathDefaultJersey+"')"
    ) {
      document.getElementById(elementId).style.backgroundImage =    "url('"+PathSelectedJersey+"')";
      return "";
    } else {
      document.getElementById(elementId).style.backgroundImage =   "url('"+PathDefaultJersey+"')";
    }
    ElementIdOfPrevPlayer = "";
    return "";
  } else {
    if (ElementIdOfPrevPlayer != "") {
      document.getElementById(ElementIdOfPrevPlayer).style.backgroundImage =
      "url('"+PathDefaultJersey+"')";
    }
    document.getElementById(elementId).style.backgroundImage =   "url('"+PathSelectedJersey+"')";
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
function CreateTrackList() {
  /*
                <div class="tracklist" id="tracklist_0">
                        <div class="logoplayer" id="P_0"></div>
                        <div class="nameplayer"></div>
                        <div class="teamplayer"></div>
                        <div class="priceplayer"></div>
                        <div class="postplayer"></div>
                        <div class="totalpoint"></div>
              */
  var tracklist = document.createElement("div");
  tracklist.classList.add("tracklist");
  tracklist.setAttribute("id", "tracklist_0");
  tracklist.setAttribute("value", "0");

  var logo = document.createElement("div");
  logo.classList.add("logoplayer");
  logo.setAttribute("id", "P_0");
  logo.setAttribute("value", "0");

  var name = document.createElement("div");
  name.classList.add("nameplayer");
  name.setAttribute("value", "0");

  var teamplayer = document.createElement("div");
  teamplayer.classList.add("teamplayer");
  teamplayer.setAttribute("value", "0");

  var price = document.createElement("div");
  price.classList.add("priceplayer");
  price.setAttribute("value", "0");

  var postplayer = document.createElement("div");
  postplayer.classList.add("postplayer");
  postplayer.setAttribute("value", "0");

  var totalpoint = document.createElement("div");
  totalpoint.classList.add("totalpoint");
  totalpoint.setAttribute("value", "0");

  tracklist.appendChild(logo);
  tracklist.appendChild(name);
  tracklist.appendChild(teamplayer);
  tracklist.appendChild(price);
  tracklist.appendChild(postplayer);
  tracklist.appendChild(totalpoint);

  return tracklist;
}
function PrevPage() {
  let page = document.getElementById("PresentPage").innerHTML;
  if (page != "1") {
    document.getElementById("PresentPage").innerHTML = (
      stringToInt(page) - 1
    ).toString();
    Page--;
    RequestToServerForPlayer(
      Search,
      IsAnyPostTypeSelected.replace("Post-", ""),
      SortBy,
      Price,
      Team,
      Page
    );
  }
}
function NextPage() {
  let page = document.getElementById("PresentPage").innerHTML;
  if (page != SizePage) {
    document.getElementById("PresentPage").innerHTML = (
      stringToInt(page) + 1
    ).toString();
    Page++;
    RequestToServerForPlayer(
      Search,
      IsAnyPostTypeSelected.replace("Post-", ""),
      SortBy,
      Price,
      Team,
      Page
    );
  }
}
function RemovePlayerFromServer(web_name, email, playerid) {
  //console.log(web_name);
  //console.log(email);
  fetch(
    "http://localhost:3001/removeplayer?web_name=" +
      web_name +
      "&user_email=" +
      email
  )
    .then((response) => response.json())
    .then((data) => {
      //console.log(data);
      if (data.status == "OK") {
        RemovePlayer(playerid, web_name);
        Remaining = data.price;
        document.getElementById("remaining").innerHTML = data.price;
      }
    })
    .catch((error) => console.log(error));
}
function RemovePlayer(playerid, web_name) {
  let player = document.getElementById(playerid);
  let num = GetPlayerNumber(playerid, playerid.length);
  let sub = "Sub_" + num;
  let remove = "Remove_" + num;
  //console.log(remove);
  if (IsThisPlayerChosed(playerid)) {
    player.style.backgroundImage ="url('"+PathDefaultJersey+"')" ;
    document.getElementById(sub).innerHTML = getPostPlayer(playerid);
    document.getElementById(sub).style.fontSize = "1.1vw";
    document.getElementById(remove).style.display = "none";
    for (let i = 0; i < PlayerSelected.length; i++) {
      if (PlayerSelected[i] == playerid) {
        PlayerSelected.splice(i, 1);
      }
      if (NameOfPlayerSelected[i] == web_name) {
        NameOfPlayerSelected.splice(i, 1);
      }
    }
    Remaining = document.getElementById("remaining").innerHTML;
    document.getElementById("remaining").innerHTML = Remaining;
    CountOfPlayer = PlayerSelected.length;
    document.getElementById("LengthOfPlayer").innerHTML = CountOfPlayer;
    if (stringToInt(num) >= 12) {
      document.getElementById("swapicon_" + num).style.display = "none";
    }
    TeamDone();
  }
}
function loadImage(imageUrl,Tag)
{
    let img = document.createElement('img');
    img.src = imageUrl;
    img.onload = function(){
        Tag.style.backgroundImage="url('"+imageUrl+"')";
    }
    img.onerror = function()
    {
        Tag.style.backgroundImage="url('"+PathLogoPlayer+"')";
    }
}
function loadPlayer(imageUrl,Tag)
{
    let img = document.createElement('img');
    img.src = imageUrl;
    img.onload = function(){
        Tag.style.backgroundImage="url('"+imageUrl+"')";
    }
    img.onerror = function()
    {
        Tag.style.backgroundImage="url('"+PathPlayerJersey+"')";
    }
}
function FinishJob() {
  let count = document.getElementById("LengthOfPlayer").innerHTML;
  if (count == "15") {
      fetch("http://localhost:3001/end?user_email="+Email)
      .then(response=>response.json())
      .then(data=>{})
      .catch(error=>console.log(error));
      window.location.replace("../../index.html?auth=1&value="+Token);
  }
}