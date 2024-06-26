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
    if(!token){
        window.location.href="./login.html"
    }
  }

  const logout=()=>{
    localStorage.clear()
    window.location.reload()
  }