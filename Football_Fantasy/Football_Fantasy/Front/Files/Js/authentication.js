let form = document.getElementsByTagName("form");
function submitForm(event){

    //Preventing page refresh
    event.preventDefault();
}
form[0].addEventListener('submit', submitForm); 

class Validation 
{
    static ValidateEmail(email) 
    {
        const re = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
        return re.test(String(email).toLowerCase());
    }
    static ValidatePassword(password) 
    {
        if (password.length < 5) 
        {
            return false;
        }
        return true;
    }
    static ValidateUsername(username) 
    {
        const re = /^[a-zA-Z0-9_]{5,15}$/;
        return re.test(String(username));
    }
    static ValidateName(name) 
    {
        if (name.length < 3) {
        return false;
        }
        const re = /^[a-zA-Z]+([ ]?[a-zA-Z]+)*$/;
        return re.test(String(name));
    }
}
function ErroValidationSignIn(value)
{
    if(!Validation.ValidateEmail(value.email))
    {
        let response = document.getElementById("response_email");
        response.innerHTML="Your email is incorect";
        response.style.backgroundColor="rgba(255, 0, 0, 0.315)";
    }
    if(!Validation.ValidatePassword(value.password))
    {
        let response = document.getElementById("response_password");
        response.innerHTML="Your password is weak";    
        response.style.backgroundColor="rgba(255, 0, 0, 0.315)";
    }
}
function CheckData(values)
{
    if(Validation.ValidateEmail(values.email)&&Validation.ValidatePassword(values.password))
    {
        RequestToServer(values);
    }
    else
    {
        ErroValidationSignIn(values);
    }
}
function SignIn()
{
    let json_value = {
        email:document.getElementById("signin-email").value,
        password:document.getElementById("signin-password").value
    };
    CheckData(json_value);
}
function RequestToServer(values)
{
    let url = "http://localhost:3001/authentication/signin-request";
    fetch(url,{
        method: "POST",
        headers: {
            Accept: "application/json",
            "Content-Type": "application/json",
        },
        body: JSON.stringify(values),
    })
    .then(response=>response.json())
    .then(data=>{
            if(data.status=="OK")
            {
                LoginSec();
                GetToken(values.email);
                
            }
            else
            {
                LoginFail(data.massage);
            }
    })
    .catch(error=>console.log(error));
}
function setCookie(cname, cvalue, exdays) {
    const d = new Date();
    d.setTime(d.getTime() + (exdays * 24 * 60 * 60 * 1000));
    let expires = "expires="+d.toUTCString();
    document.cookie = cname + "=" + cvalue + ";" + expires + ";path=/";
}
function GetToken(email)
{
    let url = "http://localhost:3001/gettoken?email="+email;
    fetch(url)
    .then(response=>response.json())
    .then(data=>{ 
            SaveToken(data.massage);
            LoginRedirection(data.massage);
            window.localStorage.setItem("football_fantasy",data.massage);
    })
    .catch(error=>console.log(error));
    
}
function Redirect(url)
{
    window.location.replace(url);
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
function SaveToken(token)
{
    setCookie("football_fantasy",token,10);
}
function LoginSec()
{
    let email_response = document.getElementById("response_email");
    let password_response = document.getElementById("response_password");
    email_response.innerHTML="";
    password_response.innerHTML="";
    setTimeout(function(){
        email_response.style.backgroundColor="#1fb127a6";
        email_response.innerHTML="Login was successful";
    },200);
}
function LoginFail(str)
{
    let email_response = document.getElementById("response_email");
    let password_response = document.getElementById("response_password");
    email_response.innerHTML="";
    password_response.innerHTML="";
    setTimeout(function(){
        email_response.style.backgroundColor="rgba(255, 0, 0, 0.315)";
        email_response.innerHTML=str;
    },200);
}
function LoginRedirection(token)
{
    window.location.replace("../../index.html?auth=1&value="+token);
}
function SignUp()
{
    let json_Value = 
    {
        name:document.getElementById("signup_name_input").value,
        email:document.getElementById("signup_email_input").value,
        username:document.getElementById("signup_username_input").value,
        password:document.getElementById("signup_password_input").value
    };
    CheckDataSingUp(json_Value);
}
function CheckDataSingUp(values)
{
    if(Validation.ValidateEmail(values.email) && Validation.ValidateName(values.name) && Validation.ValidatePassword(values.password) &&Validation.ValidateUsername(values.username))
    {
        //RequestToServerSignUp(values);
        let url = "http://localhost:3001/authentication/signup-request";
        let response =  synchronousRequestPost(url,values);
        let data = JSON.parse(response);
        
        if(data.status=="OK")
        {
            SignUpSec(data);
            window.location.replace("./otp.html?auth=1&value="+data.token);
        }
        else
        {
            SignUpFail(data);
        }
    }
    else
    {
        ErroValidationSignUp(values);
    }
}
function RequestToServerSignUp(values)
{
    let url = "http://localhost:3001/authentication/signup-request";
    fetch(url,{
        method: "POST",
        headers: {
            Accept: "application/json",
            "Content-Type": "application/json",
        },
        body: JSON.stringify(values),
    })
    .then(response=>response.json())
    .then(data=>{
        if(data.status=="OK")
        {
            window.location.replace("./otp.html?auth=1&value="+data.token);
        }
        else
        {
            SignUpFail(data);
        }
    })
    .catch(error=>console.log(error));
}
function ErroValidationSignUp(values)
{
    document.getElementById("response_signup").innerHTML="";
    setTimeout(function(){
        if(!Validation.ValidateName(values.name))
        {
            let response = document.getElementById("response_signup");
            response.innerHTML="Your name is not correct";
            response.style.backgroundColor="rgba(255, 0, 0, 0.315)";
            return "";
        }
        if(!Validation.ValidateEmail(values.email))
        {
            let response = document.getElementById("response_signup");
            response.innerHTML="Your email is not correct";
            response.style.backgroundColor="rgba(255, 0, 0, 0.315)";
            return "";
        }
        if(!Validation.ValidateUsername(values.username))
        {
            let response = document.getElementById("response_signup");
            response.innerHTML="Your username is not correct";
            response.style.backgroundColor="rgba(255, 0, 0, 0.315)";
            return "";
        }
        if(!Validation.ValidatePassword(values.password))
        {
            let response = document.getElementById("response_signup");
            response.innerHTML="Your password is not correct";
            response.style.backgroundColor="rgba(255, 0, 0, 0.315)";
            return "";
        }
    },200);
    
}
function SignUpSec(data)
{
    let response = document.getElementById("response_signup");
    response.innerHTML="";
    setTimeout(function(){
        response.innerHTML="Please verification your account";
        response.style.backgroundColor="#1fb127a6";
    },1000);
}
function SignUpFail(data)
{

    let response = document.getElementById("response_signup");
    response.innerHTML="";
    setTimeout(function(){
        response.innerHTML=data.massage;
        response.style.backgroundColor="rgba(255, 0, 0, 0.315)";
    },200);

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
function synchronousRequestPost(url,params) 
{
    const xhr = new XMLHttpRequest();
    xhr.open("POST", url, false);
    xhr.setRequestHeader('Content-Type','application/json');
    xhr.send(JSON.stringify(params));
    if (xhr.status === 200) {
       return xhr.responseText;
    } else {
       throw new Error('Request failed: ' + xhr.statusText);
    }
}