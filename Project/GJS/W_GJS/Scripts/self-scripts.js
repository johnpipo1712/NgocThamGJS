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


// CHANGE PASSWORD

function changePassword() {
    if (!inProgressChangePassword) {
        inProgressChangePassword = true;
        $("#loadingDivChangePassword").show();
        $("#error-change-password").hide();
        $.post($("#change-password-button").data("action"), $('#form-change-password').serialize(),
            function (data) {
                if (data.HasError) {
                    $("#error-change-password").html(data.ErrorString);
                    $("#loadingDivChangePassword").hide();
                    $("#error-change-password").show();

                } else {
                    $("#loadingDivChangePassword").hide();
                    $("#ChangePasswordModal").modal('hide');
                    $("#changePasswordSuccesfully").modal('show');
                    
                }
                inProgressChangePassword = false;
            });
    }
    return false; //stop submit form.
}

inProgressChangePassword = false;
$("#change-password-button").click(function () {
    changePassword();
});

$("#form-change-password").submit(function (event) {
    changePassword();
    event.preventDefault();
});

$('#ChangePasswordModal').on('shown.bs.modal', function () {
    $("#form-change-password").find("input[type=text], input[type=password]").val("");
})


// Update user information

function userInformationUpdate() {
    if (!inProgressUserInformationUpdate) {
        inProgressUserInformationUpdate = true;
        $("#loadingDivUserInformationUpdate").show();
        $("#error-user-information-update").hide();
        $.post($("#information-update-button").data("action"), $('#form-user-information-update').serialize(),
            function (data) {
                if (data.HasError) {
                    $("#error-user-information-update").html(data.ErrorString);
                    $("#loadingDivUserInformationUpdate").hide();
                    $("#error-user-information-update").show();
                } else {
                    $("#loadingDivUserInformationUpdate").hide();
                    $("#userInformationUpdateSuccesfully").modal('show');
                }
                inProgressUserInformationUpdate = false;
            });
    }
    return false; //stop submit form.
}

inProgressUserInformationUpdate = false;
$("#information-update-button").click(function () {
    userInformationUpdate();
});

$("#form-user-information-update").submit(function (event) {
    userInformationUpdate();
    event.preventDefault();
});

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
