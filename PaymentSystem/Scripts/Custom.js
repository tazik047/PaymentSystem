function accountIsBlocked(row, index) {
    console.log(row);
    if (row.IsBlocked)
        return { class: 'alert-danger', title: 'Карта заблокирована' }
    return {};
}

function createBootstrapTable(url) {
    $('#bootstrap-table').bootstrapTable({
        onClickRow: function (row) {
            if (row.Id != '')
                location.href = url + "/" + row.Id;
        }
    });
}

function changeOperation(text, url) {
    swal({
        title: "Вы уверены?",
        text: "Вы точно хотите "+text + " операцию?",
        type: "warning",
        showCancelButton: true,
        confirmButtonColor: "#DD6B55",
        confirmButtonText: "Да",
        cancelButtonText: "Отмена",
        closeOnConfirm: false
        },
        function() {
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
            }).fail(function () {
                swal("Ошибка", "Не удалось изменить статус операции.", "error")
            });
        });
}

$(document).ready(function () {
    createBootstrapTable($('#bootstrap-table').attr('click-href'));
    $('#blockUser,#blockAccount').click(function (e) {
        var bt = $(this);
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