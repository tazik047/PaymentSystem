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
        })

    });
});