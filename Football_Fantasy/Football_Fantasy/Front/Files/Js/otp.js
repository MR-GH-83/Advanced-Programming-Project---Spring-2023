let Code;
let Email;
if(CheckFirst())
{
    GetEmail();
}
else
{
    window.location.replace("./sign_up.html");
}
function CheckFirst()
{
    let search = window.location.search;
    if(search.search("auth=1")!=-1)
    {
        return true;
    }
    return false;
}
function VlaueOfSearch(value)
{
    let index_first = value.search("value=");
    index_first= index_first+6;
    let index_end = value.length;
    value = value.substring(index_first,index_end);
    return value;
}
function synchronousRequest(url) 
{
    const xhr = new XMLHttpRequest();
    xhr.open('GET', url, false);
    xhr.send(null);
    if (xhr.status === 200) {
       return xhr.responseText;
    } else {
       throw new Error('Request failed: ' + xhr.statusText);
    }
}
function GetEmail()
{
    let search = window.location.search;
    let token = VlaueOfSearch(search);
    let url = "http://localhost:3001/readtokenotp?token="+token;
    let response = synchronousRequest(url);
    let data = JSON.parse(response);
    Email = data.massage;
    console.log(Email);
    if(data.status=='OK')
    {
        let otp_url = "http://localhost:3001/verfiyation/set?email="+Email;
        let otp_response = synchronousRequest(otp_url);
        otp_response= JSON.parse(otp_response);
        Code = otp_response.code;
        console.log(Code);
        document.getElementById("email").innerHTML=data.massage;
        document.getElementById("load").style.display="none";
        document.getElementById("verfiybox").style.display="block";
    }
    else
    {
        window.location.replace("./sign_up.html");
    }
}
function RequestToSendEmail(email)
{
    let url = "http://localhost:3001/sendcode?email="+email;
    fetch(url)
    .then(response=>response.json())
    .then(data=>{
        if(data.status=="Fail")
        {
            document.getElementById("textsub").innerHTML="Unfortunately, we could not send a message to your email";
        }
        else
        {
            document.getElementById("textsub").innerHTML="Verification code sent into your";
            document.getElementById("email").innerHTML=email;        
        }
        
    })
    .catch(error=>console.log(error));
}
function CheckCode()
{
    let enter_code = document.getElementById("code").value;
    if(enter_code==Code)
    {
        RequestToServerForSave()

        let url = "http://localhost:3001/gettoken?email="+Email;
        let response = synchronousRequest(url)
        response = JSON.parse(response);

        window.location.replace("../../index.html?auth=1&value="+response.massage);

    }
    else
    {
        document.getElementById("email").innerHTML="";
        document.getElementById("textsub").innerHTML="The code you entered is incorrect";
    }

}
function RequestToServerForSave()
{
    let url="http://localhost:3001/verfiyation/saveDataToUser";
    synchronousRequest(url);
}

