function showBlock(block) {
    var tabs = $(".tabcontrol li").removeClass("active");
    $(tabs.get(block)).addClass("active");
    switch (block) {
    case 0:
        $("#searchBlock").slideDown();
        $("#scheduleDetails").slideUp();
        $("#paymentDetails").slideUp();
        break;
    case 1:
        $("#searchBlock").slideUp();
        $("#scheduleDetails").slideDown();
        break;
    case 2:
        $("#scheduleDetails").slideUp();
        $("#paymentDetails").slideDown();
        $(".backSearch").click(function () {
            showBlock(0);
        });
        break;
    }
}

function InitSchedule(config) {
    var conf = {
        departureUrl: config.departureUrl,
        destinationUrl: config.destinationUrl,
        departurePlaceholder: config.departurePlaceholder,
        destinationPlaceholder: config.destinationPlaceholder,
        detailsUrl: config.detailsUrl
    }

    function processResults(data) {
        return {
            results: $.map(data, function (item) {
                return {
                    text: item,
                    slug: item,
                    id: item
                }
            })
        };
    }
    function bindingDetailsBtn() {
        $(".details").click(function () {
            var id = $(this).attr("schedule-id");
            $.post(conf.detailsUrl, { id: id },
                function (data) {
                    $("#scheduleDetails").html(data);
                    showBlock(1);
                    $(".backSearch").click(function () {
                        showBlock(0);
                    });
                });
        });
    }
    function bindingSelect2() {
        $("#Departure").select2({
            minimumInputLength: 2,
            ajax: {
                url: conf.departureUrl,
                delay: 250,
                type: "POST",
                data: function (params) {
                    return {
                        departure: params.term
                    };
                },
                processResults: processResults
            },
            placeholder: conf.departurePlaceholder,
            cache: true,
            language: "ru"
        });
        $("#Destination").select2({
            minimumInputLength: 2,
            ajax: {
                url: conf.destinationUrl,
                delay: 250,
                type: "POST",
                data: function (params) {
                    return {
                        departure: $("#Departure").val(),
                        destination: params.term
                    };
                },
                processResults: processResults
            },
            placeholder: conf.destinationPlaceholder,
            cache: true,
            language: "ru"
        });
    }

    bindingSelect2();
    return {
        updateCallback: function (data) {
            $("#searchContent").html(data);
            bindingDetailsBtn();
        },
        resetCallback: function () {
            $("select").val(null);
            bindingSelect2();
        }
    }
};
