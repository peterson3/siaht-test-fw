var frames;
var frameMenu;

frames= document.getElementsByTagName("iframe");

for (i=0; i<frames.length; i++){
	//console.log(frames[i].name);
	if (frames[i].name == "menu"){
	frameMenu = frames[i];
	}
}

//frameMenu.document.forms[0].elements[0].value;

var menuItems = frameMenu.contentWindow.document.getElementsByTagName("a");


for (j=0; j<menuItems.length; j++){
	console.log(menuItems[j].innerText);
	if (menuItems[j].innerText == "Autorização de Procedimento"){

		menuItems[j].click();
	}
}
