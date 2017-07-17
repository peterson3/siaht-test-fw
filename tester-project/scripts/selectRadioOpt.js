var frames;
var elements;
var contentFrame;
frames = document.getElementsByTagName("iframe");

for (i=0; i<frames.length; i++){
  if (frames[i].name=='I2'){
    contentFrame=frames[i];
  }
}



var radioOpt = contentFrame.contentWindow.document.getElementsByName("ind_disponibilidade_es")[0].checked=true;
