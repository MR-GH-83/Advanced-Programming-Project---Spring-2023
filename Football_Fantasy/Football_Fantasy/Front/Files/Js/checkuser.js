function IsThisUserWasLogin()
{
    let result;
    let token;
    let search = window.location.search;
    let cookie = GetCookie("football_fantasy");
    if(search.search("auth=1")!=-1)
    {
        token=VlaueOfSearch(search);
    }
    else
    {
        token=cookie;
    }
    let url = "http://localhost:3001/readtoken?token="+token;
    let response = synchronousRequest(url);
    response = JSON.parse(response);

    if(response.status=="OK")
    {
        result={
            status:"OK",
            massage:response.massage,
            token : token
        };
        return result;
    }
    else
    {
        result = {
            status:"Fail"
        };
        return result;
    }    
}
function GetEmail()
{
    if(IsThisUserWasLogin().status=="OK")
    {
        return IsThisUserWasLogin().massage;
    }
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
function MyRedirect()
{
    let url= window.location.href;
    if(url.search("sign_in.html")!=-1)
    {
        window.location.replace("../../index.html");
    }
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
function VlaueOfSearch(value)
{
    let index_first = value.search("value=");
    index_first= index_first+6;
    let index_end = value.length;
    value = value.substring(index_first,index_end);
    return value;
}