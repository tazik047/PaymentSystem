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
});