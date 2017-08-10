$.fn.serializeObject = function () {
    var o = {};
    var a = this.serializeArray();
    $.each(a, function () {
        if (o[this.name] !== undefined) {
            if (!o[this.name].push) {
                o[this.name] = [o[this.name]];
            }
            o[this.name].push(this.value || '');
        } else {
            o[this.name] = this.value || '';
        }
    });
    return o;
};

function showNotify(text, type, title, timeout) {
    $.Notify({
        caption: title ? title : "Уведомление",
        content: text,
        type: type ? type : "info",
        timeout: timeout ? timeout : 8000,
        shadow: true
    });
};

function showResponseNotify(response, container) {
    if (response && response.Message) {
        showNotify(response.Message, response.IsSuccessful ? "success" : "alert");
        if (container) {
            $(container).find(".validation-summary-errors").remove();
        }
    }
};

function InitSelect2() {
    $("select.select2").select2({ language: "ru" });
    //update valid state
    //$("select.select2").change(function () {
    //   $("form").validate().element(".select2");
    //});
}

function InitDatepicker() {
    $(".datepicker").datepicker({
        otherDays: true,
        format: "dd.mm.yyyy"
    }).keyup(function (e) {
        if (e.keyCode === 8 || e.keyCode === 46) {
            $(this).find("input").val(null);
            $(this).datepicker();
        }
    });
}

function InitPanel() {
    $(".panel-collapse").panel();
}

function Pagination(configuration) {
    var self = this;
    self.conf = {
        formId: configuration.formId,
        contentSelector: configuration.contentSelector,
        pageSelector: configuration.pageSelector,
        lengthSelector: configuration.lengthSelector,
        orderByDescSelector: configuration.orderByDescSelector,
        callback: configuration.callback,
        deleteCallback: configuration.deleteCallback,
        resetSelector: configuration.resetSelector,
        resetCallback: configuration.resetCallback,
        submitSelector: configuration.submitSelector ? configuration.submitSelector : "input[type=submit], button[type=submit]"
    };
    if (conf.formId == null || conf.formId === "") return null;

    self.initPagingEvent = function () {
        $(self.conf.lengthSelector)
            .change(function () {
                self.search();
            });
        $(configuration.pageSelector)
            .click(function () {
                self.search($(this).attr("page"));
            });
    }

    self.progress = function (start) {
        if (start === true) {
            $(self.conf.formId).find(self.conf.submitSelector).addClass("disabled");
        } else {
            $(self.conf.formId).find(self.conf.submitSelector).removeClass("disabled");
        }
    };

    $(self.conf.formId)
        .submit(function () {
            self.search();
            return false;
        });
    $(self.conf.resetSelector)
        .click(function () {
            $(self.conf.formId)[0].reset();
            if (self.conf.resetCallback)
                self.conf.resetCallback();
            else {
                InitSelect2();
                self.search();
            }
        });
    self.initPagingEvent();

    self.search = function (page) {
        var query = $(self.conf.formId).serializeObject();
        if (!query) {
            showNotify("Некорректные данные");
            return;
        }
        var url = $(self.conf.formId).attr("action");

        query["Paging.Page"] = page;
        query["Paging.Length"] = $(self.conf.lengthSelector).val();

        self.progress(true);
        $.post(url, query).done(function (data) {
            self.conf.callback ? self.conf.callback(data) : $(self.conf.contentSelector).html(data);
            self.initPagingEvent();
            if (self.conf.deleteCallback) {
                self.conf.deleteCallback();
            }
            self.progress(false);
        }).fail(function () {
            self.progress(false);
        });
    }

    return self;
}

function InitAjaxFormValidator(form) {
    $(form).removeData("validator");
    $(form).removeData("unobtrusiveValidation");
    $.validator.unobtrusive.parse(form);
}
