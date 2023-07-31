document.addEventListener('click',(e)=>{
    let element_id = e.target.id;
    if(element_id=="findoutmore")
    {
        window.location.replace("./Files/Html/new.html");
    }
    if(element_id=="csignup")
    {
        window.location.replace("./Files/Html/sign_up.html");
    }
})
IsThisUserWasLogin();
function IsThisUserWasLogin()
{
    let cookie = GetCookie("football_fantasy");
    let localStorag = window.localStorage.getItem("football_fanatsy");
    let auth = window.location.search;
    value = VlaueOfSearch(auth);     
    if(cookie!="")
    {
        value=cookie;
    }   
    if(localStorag!=null)
    {
        console.log(localStorag);
        value=localStorag;
    }
    let request = RequestForToken(value);
    request.then(response=>{
            console.log(response);
            if(response.status=="OK")
            {
                MyRedirect(response.massage,value);
            }
            else
            {
                NotLogin();
            }
    });
    
}
async function RequestForToken(cookie)
{
    let url = "http://localhost:3001/readtoken?token="+cookie;
    let myPromis = new Promise(function(resolve){
        let xhttp = new XMLHttpRequest();
        xhttp.open("GET",url,false);
        xhttp.send();
        let response = xhttp.response;
        let data = JSON.parse(response);
        resolve(data);
    })
    let result = await myPromis;
    return result;
}
function GetCookie(cname) 
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
function SetCookie(cname, cvalue, exdays) 
{
    const d = new Date();
    d.setTime(d.getTime() + exdays * 24 * 60 * 60 * 1000);
    let expires = "expires=" + d.toUTCString();
    document.cookie = cname + "=" + cvalue + ";" + expires + ";path=/";
}
function MyRedirect(email,token)
{
    console.log(email);
    let buttom = document.getElementById("Playe_Game");
    buttom.addEventListener('click',e=>{
        window.location.replace("Files/Html/team.html?auth=1&value="+token);
    })
    buttom.querySelector("a").setAttribute("href","Files/Html/team.html?auth=1&value="+token);
    let info = document.getElementsByClassName("info");
    let loading = document.getElementsByClassName("loading");
    for(let i=0;i<3;i++)
    {
        info[i].style.display="block";
        loading[i].style.display="none";
    }
    document.getElementById("gaming").style.display="block";
    let gaming = document.getElementById("gaming");
    gaming.querySelector("a").setAttribute("href","Files/Html/team.html?auth=1&value="+token);
    let card = document.getElementById("card_1");
    card.style.backgroundImage="url('./Files/Img/Card.png')";
    card.querySelector(".info .title span").innerHTML = "Hi "+email;
    let getTeamUrl = "http://localhost:3001/teamget?user_email="+email;
    let response = synchronousRequest(getTeamUrl);
    response = JSON.parse(response);
    console.log(response);
    if(response.team.done==false)
    {
        card.querySelector(".info .text span").innerHTML = "You haven't completed your team yet";
        card.querySelector(".info .text").style.top = "30px";
        card.querySelector(".info .buttom").style.display = "none";
    }
    else
    {
        card.querySelector(".info .text span").innerHTML = "Your score is : "+response.team.score;
        card.querySelector(".info .text").style.top = "30px";
        card.querySelector(".info .buttom").style.display = "none";
    }
}
function NotLogin()
{
    let info = document.getElementsByClassName("info");
    let loading = document.getElementsByClassName("loading");
    for(let i=0;i<3;i++)
    {
        info[i].style.display="block";
        loading[i].style.display="none";
    }
    document.getElementById("sign_in").style.display="block";
    document.getElementById("sign_up").style.display="block";
    let card = document.getElementById("card_1");
    card.style.backgroundImage="url('./Files/Img/Card.png')";
    document.getElementById("Playe_Game").style.display="none";

}
function VlaueOfSearch(value)
{
    let index_first = value.search("value=");
    index_first= index_first+6;
    let index_end = value.length;
    value = value.substring(index_first,index_end);
    return value;
}
function synchronousRequest(url) {
    const xhr = new XMLHttpRequest();
    xhr.open('GET', url, false);
    xhr.send(null);
    if (xhr.status === 200) {
       return xhr.responseText;
    } else {
       throw new Error('Request failed: ' + xhr.statusText);
    }
}