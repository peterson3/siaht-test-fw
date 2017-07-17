var frames;
var framePrincipal;

frames= document.getElementsByTagName("iframe");

for (i=0; i<frames.length; i++){
	//console.log(frames[i].name);
	if (frames[i].name == "principal"){
	framePrincipal = frames[i];
	}
}

//framePrincipal.document.forms[0].elements[0].value;

var tipoAtendimento = framePrincipal.contentWindow.document.getElementById("cod_tratamento");
var opts = tipoAtendimento.options;

for (j=0; j < opts.length; j++){
	if (opts[j].text == "CONSULTA ELETIVA"){

		tipoAtendimento.selectedIndex = j;
		break;
	}
}
