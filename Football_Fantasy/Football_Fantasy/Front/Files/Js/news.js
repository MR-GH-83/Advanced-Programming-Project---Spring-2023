//Test();
MainNews();
document.addEventListener('click',(e)=>{
    let element_id = e.target.id;
    let element_class = e.target.className;
    if(element_id!="home" && element_class!="" && element_class!="container")
    {    
        console.log(element_id);
        console.log(element_class);
        window.location.reload();
    }
})

function MainNews()
{
    let url = "https://www.uefa.com/uefachampionsleague/news/"
    //let url = "https://www.varzesh3.com/";
    let response = synchronousRequest(url);
    const parser = new DOMParser();
    const htmlDocument = parser.parseFromString(response, 'text/html');
    let Tag = htmlDocument.getElementsByClassName("section--content clearfix");
    document.getElementById("add").appendChild(Tag[1]);
    //document.body.appendChild(Tag[1]);
    //document.head.append(htmlDocument.head);
    console.log(document.body);
    console.log(Tag)
    return response;
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
