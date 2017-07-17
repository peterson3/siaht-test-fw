var frames;
var elements;
var contentFrame;
frames = document.getElementsByTagName("iframe");

for (i=0; i<frames.length; i++){
  if (frames[i].name=='I1'){
    contentFrame=frames[i];
  }
}



var element = contentFrame.contentWindow.document.getElementsByName("num_crm")[0];



element.value="9999999";
