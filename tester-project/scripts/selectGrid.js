	var frames;
	var framePrincipal;

	frames= document.getElementsByTagName('iframe');

	for (i=0; i<frames.length; i++){
		//console.log(frames[i].name);
		if (frames[i].name == 'I4'){
		framePrincipal = frames[i];
		}
	}

//framePrincipal.document.forms[0].elements[0].value;

var grid = framePrincipal.contentWindow.document.getElementById('grid_tipo_prestador_1');

/*var attributes = grid.attributes;

for (j=0; j< attributes.length; j++){
  if (grid.attributes[j] != null)
    console.log(attributes[j].name)
}*/


var oGrid = grid.object;
/*
var attributes = oGrid.attributes;

for (j=0; j< attributes.length; j++){
  if (oGrid.attributes[j] != null)
    console.log(attributes[j].name)
}*/


console.log(oGrid.rowCount());
console.log(oGrid.columnCount());
oGrid.setCell(0,0,'S');
