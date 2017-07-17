  var frames;
  var elements;
  var contentFrame;
  frames = document.getElementsByTagName("iframe");

  for (i=0; i<frames.length; i++){
    if (frames[i].name=='I1'){
      contentFrame=frames[i];
    }
  }



  var dropdown = contentFrame.contentWindow.document.getElementsByName("sigla_conselho")[0];


  var opts = dropdown.options;

  for (j=0; j < opts.length; j++){
  	if (opts[j].value == "CRM"){

  		dropdown.selectedIndex = j;
  		break;
  	}
  }
