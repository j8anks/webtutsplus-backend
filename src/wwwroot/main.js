var domain = "https://localhost:7188";
var tokenExpires;
var userName;


function tokenIsExpired() {
    let now = new Date();
    tokenExpires = new Date(sessionStorage.getItem("tokenExpires"));
    if (tokenExpires < now) {
        return true
    }
    else{
        return false;
    }    
}

function getToken() {
    let token = sessionStorage.getItem("token");
    return token;
}