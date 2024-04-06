/*============================ Validate user ====================================================================*/

function btnLogin_Click() {
    var username = document.getElementById("txtUsername").value;
    var password = document.getElementById("txtPassword").value;
  
    if ((username != "" && password != "") && !(isNaN(username) && isNaN(password))) {
        var obj = {
            Username: username,
            Password: password
        }

        $.ajax({
            url: "/Login/ValidateUser",
            method: "Post",
            data: { userObj: obj },
            success: function (result) {
                //alert(result);
                if (result == "admin") {
                    window.location.href = document.location.origin + "/Admin";
                }
                else if (result == "user") {
                    alert(result)
                    window.location.href = document.location.origin + "/User";
                }
                else if (result == "NotRegistered") {
                    alert("user not registered");
                }
                else {
                    alert(result);
                }
            },
            error: function (error) {
                alert(error);
            }
        })
    }
    else {
        //debugger;
        //if (isNaN(username) || username == "") {

        //    document.getElementById("rvfUsername").innerHTML = "Username Required";
        //    document.getElementById("rvfUsername").style.visibility = "visible";
        //}
        //else if (isNaN(password) || password == "") {
        //    document.getElementById("rvfPassword").innerHTML = "Password Required";
        //    document.getElementById("rvfPassword").style.visibility = "visible";
        //}
        //else {
        //    document.getElementById("rvfUsername").innerHTML = "Username Required";
        //    document.getElementById("rvfPassword").innerHTML = "Password Required";
        //    document.getElementById("rvfUsername").style.visibility = "visible";
        //    document.getElementById("rvfPassword").style.visibility = "visible";
        //}

        if (username == "" && password == "") {
            document.getElementById("rvfUsername").innerHTML = "Username Required";
            document.getElementById("rvfPassword").innerHTML = "Password Required";
            document.getElementById("rvfUsername").style.visibility = "visible";
            document.getElementById("rvfPassword").style.visibility = "visible";
        }
        else if (username == "") {
            document.getElementById("rvfUsername").innerHTML = "Username Required";
            document.getElementById("rvfUsername").style.visibility = "visible";
        }
        else {
            document.getElementById("rvfPassword").innerHTML = "Password Required";
            document.getElementById("rvfPassword").style.visibility = "visible";
        }
        console.log("test")
            
    }
    
}
/*============================ Register User ====================================================================*/

function Register() {
    var username = document.getElementById("txtUsername").value;
    var password = document.getElementById("txtPassword").value;
    var name = document.getElementById("txtName").value; 
    var email = document.getElementById("txtEmail").value;
    var mobile = document.getElementById("txtEmail").value;
    var postcode = document.getElementById("txtPostCode").value; 
    var address = document.getElementById("txtAddress").value;

    var userObj = {
        Name: name,
        Username: username,
        Password: password,
        Address: address, 
        Modile: mobile, 
        Email: email,
        Postcode: postcode
    }

    $.ajax({
        url: "/Login/RegisterUser",
        method: "Post",
        data: { userObj: userObj },
        success: function (result) {
            if (result == true) {
                alert("Register successfull you will be redirect to login page.");
                window.location.href = document.location.origin + "/Login";
            }
            else { alert("Somthind went wrong"); }
        },
        error: function (error) {
            alert(error);
        }
    })
}
