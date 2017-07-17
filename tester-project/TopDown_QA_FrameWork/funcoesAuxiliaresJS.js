> document.getElementById("417").getElementsByTagName("a")[0].click(); 
"Não é possível obter a propriedade 'getElementsByTagName' de referência indefinida ou nula" 
>> document.getElementById("417").innerHTML.getElementsByTagName("a")[0].click(); 
"Não é possível obter a propriedade 'innerHTML' de referência indefinida ou nula" 
>> document.getElementById("417").innerHTML 
"Não é possível obter a propriedade 'innerHTML' de referência indefinida ou nula" 
>> document.getElementById("417").style.innerHTML 
"Não é possível obter a propriedade 'style' de referência indefinida ou nula" 


document.getElementsByTagName("iframe").innerHTML;


var frames = document.getElementsByTagName("iframe");

for (i = 0; i < frames.length; i++) { 
    cars[i].name;
}


var frames = document.getElementsByTagName("iframe");
var subMenu;
console.log(frames.length);

for (i = 0; i < frames.length; i++) { 
    //console.log(frames[i].name);
   if (frames[i].name == "submenu"){
	subMenu = frames	[i];
}
}

console.log(subMenu.innerHTML);




var frames = document.getElementsByTagName('iframe');
var subMenu;
console.log(frames.length);

for (i = 0; i < frames.length; i++) { 
    //console.log(frames[i].name);
   if (frames[i].name == 'submenu'){
	subMenu = frames[i];
	console.log('Achou o submenu');
}
}

//console.log(subMenu.contentWindow.document.body.innerHTML);

//console.log(subMenu.contentWindow.document.body.innerHTML.getElementById('417').innerHMTL);

//console.log(subMenu.contentWindow.document.getElementById('417'));

subMenu.contentWindow.document.getElementById('417').getElementsByTagName('a')[0].click();



var frames = document.getElementsByTagName('iframe');
var subMenu;
console.log(frames.length);

for (i = 0; i < frames.length; i++) { 
    //console.log(frames[i].name);
   if (frames[i].name == 'submenu'){
	subMenu = frames[i];
	console.log('Achou o submenu');
}
}

subMenu.contentWindow.document.getElementById('417').getElementsByTagName('a')[0].click();
