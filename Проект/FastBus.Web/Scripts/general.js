$().ready(function () {
    $(".select2").select2();
    //update valid state
    $().ready(function () {
        $(".select2").change(function () {
            $("form").validate().element(".select2");
        });
    });
    $(".datepicker").datepicker({
        otherDays: true,
        format: "dd/mm/yyyy"
    }).keyup(function (e) {
        if (e.keyCode === 8 || e.keyCode === 46) {
            $(this).find("input").val(null);
            $(this).datepicker();
        }
    });

    $(function () {
        $(".panel-collapse").panel();
    });

});