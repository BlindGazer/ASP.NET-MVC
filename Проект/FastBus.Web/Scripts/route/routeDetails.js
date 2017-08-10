function registerUserInit(regiserUrl, appBarUrl) {
    InitAjaxFormValidator("#regisetUserForm");
    InitDatepicker();
    $("#regisetUserForm").submit(function (e) {
        e.preventDefault();
        if ($(this).valid()) {
            $.post(regiserUrl, $(this).serialize(),
                function (data) {
                    if (data) {
                        if (data.IsSuccessful) {
                            window.isAuth = true;
                            updateAppBar();
                        } else {
                            showNotify(data.Message, "alert");
                        }
                    }
                });
        }
    });
    function updateAppBar() {
        $.post(appBarUrl,
            function (data) {
                if (data) {
                    $(".app-bar.navy").html(data);
                    $("#scheduleDetailsForm").slideToggle();
                    $("#registerForm").remove();
                    showNotify("Вы успешно зарегистрированы", "success");
                }
            });
    }
};
function detailsInit(reserveUrl, id) {
    $("#buyTicket").click(function () {
        if (window.isAuth) {

        } else {
            $("#scheduleDetailsForm").slideToggle();
            $("#registerForm").slideToggle();
        }
    });

    $("#reserveTicket").click(function () {
        if (window.isAuth) {
            $.post(reserveUrl, { id: id }, function (data) {
                $("#paymentDetails").html(data);
                showBlock(2);
            });
        } else {
            $("#scheduleDetailsForm").slideToggle();
            $("#registerForm").slideToggle();
        }
    });
}
