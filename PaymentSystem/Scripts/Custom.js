function accountIsBlocked(row, index) {
    console.log(row);
    return {classes: (row.isBlocked ? 'alert alert-danger' : 'false')};
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