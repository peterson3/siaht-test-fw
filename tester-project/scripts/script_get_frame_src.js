function getFrameSrc(){

var frameSrc;
var frames = document.getElementsByTagName('iframe');
for (i=0; i < frames.length; i++){
	if (frames[i].name == 'pesquisa'){
		frameSrc = frames[i].src
	}
}


return frameSrc;
}

