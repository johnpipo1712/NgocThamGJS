// LOGIN

function login() {
    if (!inProgressLogin) {
        inProgressLogin = true;
        $("#loadingDivLogin").show();
        $("#error-login").hide();
        $.post($("#login-button").data("action"), $('#form-login').serialize(),
            function (data) {
                if (data.RoleOrFailed == 0) {
                    $("#error-login").html(data.ErrorString);
                    $("#loadingDivLogin").hide();
                    inProgressLogin = false;
                    $("#error-login").show();

                } else {
                    $("#loadingDivLogin").hide();
                    window.location.href = $("#login-button").data("success-action");
                }
            });
    }
    return false; //stop submit form.
}

inProgressLogin = false;
$( "#login-button" ).click(function() {
    login();
});

$("#form-login").submit(function (event) {
    login();
    event.preventDefault();
});

$('#LoginModal').on('shown.bs.modal', function () {
    $("#form-login").find("input[type=text], input[type=password], textarea").val("");
})


// REGISTER
function registerAccount()
{
    if (!inProgressRegister) {
        inProgressRegister = true;
        $("#loadingDivRegister").show();
        $("#error-register").hide();
        $.post($("#register-button").data("action"), $('#form-register').serialize(),
            function (data) {
                if (data.HasError) {
                    $("#error-register").html(data.ErrorString);
                    $("#loadingDivRegister").hide();
                    inProgressRegister = false;
                    $("#error-register").show();

                } else {
                    $("#loadingDivRegister").hide();
                    $("#RegisterModal").modal('hide');
                    $("#registerSuccesfully").modal('show');
                    window.location.href = $("#register-button").data("success-action");
                }
            });
    }
    return false; //stop submit form.
}

inProgressRegister = false;
$( "#register-button" ).click(function() {
    registerAccount();
});

$("#form-register").submit(function (event) {
    registerAccount();
    event.preventDefault();
});

$('#RegisterModal').on('shown.bs.modal', function () {
    $("#form-register").find("input[type=text], input[type=password], textarea").val("");
    $("#form-register").find("select[name=sex]").val(0);
})

// CONTACT
function sendContact() {
    if (!inProgressContact) {
        inProgressContact = true;
        $("#loadingDivContact").show();
        $("#error-contact").hide();
        $.post($("#contact-button").data("action"), $('#form-contact').serialize(),
            function (data) {
                if (data.HasError) {
                    $("#error-contact").html(data.ErrorString);
                    $("#loadingDivContact").hide();
                    $("#error-contact").show();

                } else {
                    $("#loadingDivContact").hide();
                    $("#BookingModal").modal('hide');
                    $("#contactSuccesfully").modal('show');
                }
                inProgressContact = false;
            });
    }
    return false; //stop submit form.
}

inProgressContact = false;
$("#contact-button").click(function () {
    sendContact();
});
$("#form-contact").submit(function (event) {
    sendContact();
    event.preventDefault();
}); 
$('#BookingModal').on('shown.bs.modal', function () {
    $("#form-contact").find("input[type=text], input[type=Email], textarea").val("");
})

// LOGOUT
inProgressLogout = false;
$("#logout-button").click(function () {
    if (!inProgressLogout) {
        inProgressLogout = true;
        $.post($(this).data("action"),
            function (data) {
                if (!data.HasError) {
                    window.location.href = $("#logout-button").data("success-action");
                }
                inProgressLogout = false;
            });
    }
});

// ADD CART with ROLEs
$(".add-cart").click(function () {
    $("#LoginModal").modal('show');
    //$("#loginModelShow").show();
});
