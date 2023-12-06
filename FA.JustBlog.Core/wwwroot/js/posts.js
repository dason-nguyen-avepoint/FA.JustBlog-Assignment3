var dataTable

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        ajax: { url: '/admin/posts/getposts' },
        columns: [
            {
                data: null,
                render: function (data, type, row, meta) {
                    return meta.row + 1;
                },
                "width": "5%"
            },
            { data: 'title', "width": "20%" },
            { data: 'createdDate', "width": "15%" },
            { data: 'viewCount', "width": "15%" },
            { data: 'categoryId', "width": "15%" },
            { data: 'isPublised', "width": "15%" },
            {
                data: 'id',
                "render": function (data) {
                    return `<div class="w-75 btn-group" role="group">
                            <a class="btn btn-outline-secondary mx-2" href="/admin/posts/edit?id=${data}">
                                <i class="bi bi-pencil-square"></i> Edit
                            </a>
                            <a class="btn btn-outline-danger mx-2" onClick=Delete("/admin/posts/delete/${data}")>
                                <i class="bi bi-trash3"></i> Delete
                            </a>
                        </div>`
                },
                "width": "15%"
            }
        ]
    });
}