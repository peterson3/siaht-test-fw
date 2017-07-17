var b=document.getElementsByTagName('iframe');

for(i=0;i<b.length;i++){
    if ('I1'==b[i].name){
        b[i].contentWindow.location.reload();
    }

}

var b=document.getElementsByTagName('iframe');

for(i=0;i<b.length;i++){
    if ('principal'==b[i].name){
        b[i].onload();
    }

}


//set visible all divs

var b=document.getElementsByTagName('div');

for(i=0;i<b.length;i++){
    b[i].style.visibility = "visible";
}
