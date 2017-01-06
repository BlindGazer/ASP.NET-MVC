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

function Pagination(configuration) {
    var self = this;
    self.conf = {
        formId: configuration.formId,
        contentSelector: configuration.contentSelector,
        pageSelector: configuration.pageSelector,
        lengthSelector: configuration.lengthSelector,
        orderBySelector: configuration.orderBySelector,
        orderByDescSelector: configuration.orderByDescSelector,
        callback: configuration.callback,
        resetSelector: configuration.resetSelector,
        submitSelector: configuration.submitSelector ? configuration.submitSelector : "input[type=submit], button[type=submit]"
    };
    if (conf.formId == null || conf.formId === "") return null;

    self.initPagingEvent = function() {
        $(self.conf.lengthSelector)
            .change(function () {
                self.search();
            });
        //$(self.conf.orderBySelector)
        //    .onChanged(function () {
        //        self.search();
        //    });
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
            $(".select2").select2();
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
                self.progress(false);
        }).fail(function() {
            self.progress(false);
        });
    }

    return self;
}

