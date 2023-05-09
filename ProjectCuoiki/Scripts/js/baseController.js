var common = {
    init: function () {
        common.registerEvent();
    },
    registerEvent: function () {
        $("#txtKeyword").autocomplete({
            minLength: 0,
            source: function (request, response) {
                $.ajax({
                    url: "Product/ListName",
                    dataType: "json",
                    data: {
                        q: $("#txtKeyword").val()
                    },
                    success: function (res) {
                        response($.map(res, function (item) {
                            return {label: item.name, value: item.name}
                        }));
                    },
                    error: function (xhr, status, error) {
                        alert("error")
                    }
                });
            },
            focus: function (event, ui) {
                $("#txtKeyword").val(ui.item.label);
                return false;
            },
            select: function (event, ui) {
                $("#txtKeyword").val(ui.item.label);
                return false;
            }
        })
            
    }
}

common.init();