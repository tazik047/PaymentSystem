function createBootstrapTable(url) {
    $('#bootstrap-table').bootstrapTable({
        onClickRow: function (row) {
            location.href = url + "/" + row.Id;
        }
    });
    $(document).ready(function () {
        createBootstrapTable($('#bootstrap-table').attr('click-href'));
    });
}