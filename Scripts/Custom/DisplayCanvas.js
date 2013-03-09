/// <reference path="../_references.js" />
$(function () {//this is public static void c# basically entry function
 

 
    /*Retrieve the data to draw*/
    
     var drawingCanvas = document.getElementById('canvas');
     var ctx = drawingCanvas.getContext('2d');

    /*Obtain Data from Url*/

    /*Retrieve the data to draw*/
     $.getJSON("/api/ConnectNode", function (data) {
         $.each(data, function (key, value) {
             alert(value);

         });
     });
    

     DrawNode(2, 3, ctx);
     DrawNode(5, 50, ctx);
     ConnectNode(2, 3, 5, 50, ctx);


    //I call only utilize functions that are in the containing anonymous function
    /******Functions START****/
     function DrawNode(x, y, ctx)
     {
      
         ctx.beginPath();
             ctx.arc(x, y, 5, 0, Math.PI * 2, true); //draw small circle represents a node
         ctx.stroke();
    
     }

     function ConnectNode(x1,y1,x2,y2,ctx) {
             ctx.beginPath();
                ctx.lineTo(x1, y1);
                ctx.lineTo(x2, y2);
                ctx.stroke();
            
     }
    /******Functions END****/

});
