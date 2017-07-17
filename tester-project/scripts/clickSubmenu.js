var frames=document.getElementsByTagName('iframe');
var subMenu;


for(i=0;i<frames.length;i++){
   if (frames[i].name == 'submenu'){
     subMenu = frames[i];
   }
}

//subMenu.contentWindow.document.getElementById('417').getElementsByTagName('a')[0].click();
var subMenuItems = subMenu.contentWindow.document.getElementsByTagName('h1');


for (j= 0; j< subMenuItems.length; j++){
    console.log(subMenuItems[j].text);
    if (subMenuItems[j].innerHTML.search("Endereços</A>") != -1){
      subMenuItems[j].click();
   }
}


//console.log(subMenu.contentWindow.document.body.getElementsByTagName('h1')[6].innerHTML.search("Endereços</a>"))
