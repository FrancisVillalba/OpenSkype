function verify_login(username, password) {
    var settings = {
        "url": "/Home/validate_login",
        "method": "POST",
        "timeout": 0,
        "headers": {
            "Content-Type": "application/json"
        },
        "data": JSON.stringify({
            "username": username,
            "password": password
        }),
    };

    $.ajax(settings).done(function (response) {
        if (response.status == 1)
        {
            $(location).prop('href', '/MainBoard/MainBoard')
        } else if (response.status == 2)
        {
            $(location).prop('href', '/Home/ResetPassword')
        } else if (response.status == 0)
        {
            toastr.error(response.message)
        }
    });
}

function reset_password(username, password) { 

    var settings = {
        "url": "/Home/update_password_reset",
        "method": "POST",
        "timeout": 0,
        "headers": {
            "Content-Type": "application/json"
        },
        "data": JSON.stringify({
            "username": username,
            "new_password": password
        }),
    };

    $.ajax(settings).done(function (response) { 
        if (response.status) {
            $(location).prop('href', '/MainBoard/MainBoard')
        }  
    });
}

$('#btnReset').click(function () { 
    var username = $("#lblUsername").html(); 
    var txtPassNew = $.trim($("#txtPassNew").val());
    var txtPassConfirmPass = $.trim($("#txtPassConfirmPass").val());

    if (txtPassNew.length == 0) {
        toastr.error('Ingrese la nueva contraseña')
        return;
    }

    if (txtPassConfirmPass.length == 0) {
        toastr.error('Ingrese la contraseña confirmada')
        return;
    }

    if (txtPassNew != txtPassConfirmPass) {
        toastr.error('Las contraseñas no coinciden, favor verificar!')
        return;
    } 

    reset_password(username, txtPassNew);
});

