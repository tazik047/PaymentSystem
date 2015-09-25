function accountIsBlocked(row, index) {
    console.log(row);
    if (row.IsBlocked)
        return { class: 'alert-danger', title: 'Карта заблокирована' }
    return {};
}

function sortDate(a, b) {
    var mas = a.split('.');
    if (mas[2].length == 4) {
        a = new Date(parseInt(mas[2]), parseInt(mas[1]) - 1, parseInt(mas[0]));
        mas = b.split('.');
        b = new Date(parseInt(mas[2]), parseInt(mas[1]) - 1, parseInt(mas[0]));
    } else {
        var mas1 = mas[2].substring(5).split(':');
        a = new Date(parseInt(mas[2]), parseInt(mas[1]) - 1, parseInt(mas[0]), parseInt(mas1[0]), parseInt(mas1[1]));
        mas = b.split('.');
        var mas1 = mas[2].substring(5).split(':');
        b = new Date(parseInt(mas[2]), parseInt(mas[1]) - 1, parseInt(mas[0]), parseInt(mas1[0]), parseInt(mas1[1]));
    }
    if (a > b)
        return 1;
    else if (a < b)
        return -1;
    return 0;
}

function createBootstrapTable(url) {
    $('#bootstrap-table').bootstrapTable({
        onClickRow: function (row) {
            if (row.Id != '')
                location.href = url + "/" + row.Id;
        },
        striped: true,
    });
}

function changeOperation(text, url) {
    swal({
        title: "Вы уверены?",
        text: "Вы точно хотите " + text + " операцию?",
        type: "warning",
        showCancelButton: true,
        confirmButtonColor: "#DD6B55",
        confirmButtonText: "Да",
        cancelButtonText: "Отмена",
        closeOnConfirm: false
    },
        function () {
            $.get(url, function (str) {
                swal({
                    title: str,
                    type: "success",
                    showCancelButton: false,
                    confirmButtonText: "Oк",
                    closeOnConfirm: false,
                },
                    function (conf) {
                        location.reload();
                    });
            }).fail(function (e) {
                console.log(e);
                swal("Ошибка", "Не удалось изменить статус операции.", "error")
            });
        });
}

$(document).ready(function () {
    $.extend($.fn.bootstrapTable.defaults, $.fn.bootstrapTable.locales['ru-RU']);

    $('.go-top').click(function () {
        $.scrollTo(0, 1000);
    });
    createBootstrapTable($('#bootstrap-table').attr('click-href'));
    $('#blockUser,#blockAccount').click(function (e) {
        var bt = $(this);
        swal({
            title: "Вы уверены?",
            text: "Вы точно хотите "+bt.text() + "?",
            type: "warning",
            showCancelButton: true,
            confirmButtonColor: "#DD6B55",
            confirmButtonText: "Да",
            cancelButtonText: "Отмена",
            closeOnConfirm: false
        },
        function () {
            $.get(bt.attr('href'), function (str) {
                swal({
                    title: str,
                    type: "success",
                    showCancelButton: false,
                    confirmButtonText: "Oк",
                    closeOnConfirm: false,
                },
                    function (conf) {
                        location.reload();
                    });
            }).fail(function () {
                swal("Ошибка", "Не удалось" + bt.html(), "warning")
            });
        });
    });
});