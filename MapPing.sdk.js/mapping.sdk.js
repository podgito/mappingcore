window.mping = (function () {
    function MPing(els) { //Prototype constructor
  
    }
  
    var mping = {
      send: function (key, value) {
        var data = JSON.stringify({
          "value": value
        });
  
        var xhr = new XMLHttpRequest();
        // xhr.withCredentials = true; //Is this needed
  
        xhr.addEventListener("readystatechange", function () {
          if (this.readyState === 4) {
            console.log(this.responseText);
          }
        });
  
        xhr.open("POST", "https://funcmappingdev.azurewebsites.net/api/message");
        xhr.setRequestHeader("content-type", "application/json");
  
        xhr.send(data);
      }
    };
  
    return mping;
  }());