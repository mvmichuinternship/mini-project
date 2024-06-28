const checkLoggedIn=()=>{
    var data = localStorage.getItem("token")
    var role = localStorage.getItem("role")
    // var mainDiv = document.getElementById("main-div")
    if(data){
        if(role=="Admin"){
            window.location.href="./adminDashboard.html"
        }
        if(role=="Customer"){
            window.location.href="./viewMenu.html"
        }
    }
  }


  const tokenValidation=()=>{
    var token = localStorage.getItem("token");
    // var role = localStorage.getItem("role");
    if(token==="undefined" || !token){
        window.location.href="./login.html"
    }
  }
  const checkAdmin=()=>{
    var role = localStorage.getItem("role");
    var token = localStorage.getItem("token");
    var id= document.getElementById("admin-dash-menu")
    if(role!="Admin" || token==undefined || !token){
      showToast("Unauthorised entry not permitted",1000,'#de0a26')
      id.classList.add("hidden")
      setTimeout(()=>window.location.href="./home.html", 1000);
    }
  }

  const logout=()=>{
    localStorage.clear()
    window.location.reload()
  }

  function showToast(message, duration = 1000, color) {
    console.log("Toasted")
    Toastify({
      text: message,
      duration: duration,
      gravity: 'top', 
      position: 'center', 
      close: true ,
      backgroundColor: color
    }).showToast();
  }