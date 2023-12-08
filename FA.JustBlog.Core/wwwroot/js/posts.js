var dataTable

$(document).ready(function () {
    function getQueryParam(name) {
        const urlSearchParams = new URLSearchParams(window.location.search);
        return urlSearchParams.get(name);
    }
    var sortBy = getQueryParam('sortBy');
    console.log(sortBy);
    loadDataTable(sortBy);
});


function loadDataTable(sortBy) {
    if (sortBy === 'Interesting')
    {
        dataTable = $('#tblPost').DataTable({
            ajax: {
                url: '/admin/posts/getposts?sortBy=' + sortBy },
            columns: [
                {
                    data: null,
                    render: function (data, type, row, meta) {
                        return meta.row + 1;
                    },
                    "width": "5%"
                },
                { data: 'title', "width": "12%" },
                { data: 'createdDate', "width": "11%" },
                { data: 'viewCount', "width": "15%" },
                { data: 'name', "width": "4%" },
                { data: 'rate', "width": "15%" },

                {
                    data: 'id',
                    "render": function (data) {
                        return `<div class="w-75 btn-group" role="group">
                            <a class="btn btn-outline-secondary mx-2" href="/admin/posts/edit?id=${data}">
                                <i class="bi bi-pencil-square"></i> Edit
                            </a>
                            <a class="btn btn-outline-danger mx-2" href="/admin/posts/delete?id=${data}">
                                <i class="bi bi-trash3"></i> Delete
                            </a>
                        </div>`
                    },
                    "width": "15%"
                }
            ]
        });
    }
    else
    {
        dataTable = $('#tblPost').DataTable({
            ajax: { url: '/admin/posts/getposts?sortBy=' + sortBy },
            columns: [
                {
                    data: null,
                    render: function (data, type, row, meta) {
                        return meta.row + 1;
                    },
                    "width": "5%"
                },
                { data: 'title', "width": "20%" },
                { data: 'createdDate', "width": "15%", type: Date },
                { data: 'viewCount', "width": "10%" },
                { data: 'categories.name', "width": "15%" },
                { data: 'isPublised', 
                    "render": function (data) {
                        if (data){
                            return `<button class="btn btn-success">
                            <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-check-circle-fill" viewBox="0 0 16 16">
                            <path d="M16 8A8 8 0 1 1 0 8a8 8 0 0 1 16 0m-3.97-3.03a.75.75 0 0 0-1.08.022L7.477 9.417 5.384 7.323a.75.75 0 0 0-1.06 1.06L6.97 11.03a.75.75 0 0 0 1.079-.02l3.992-4.99a.75.75 0 0 0-.01-1.05z"/>
                            </svg> Is Authorize</button>`
                        }else {
                            return `<button class="btn btn-danger">
                            <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-x-circle" viewBox="0 0 16 16">
                              <path d="M8 15A7 7 0 1 1 8 1a7 7 0 0 1 0 14m0 1A8 8 0 1 0 8 0a8 8 0 0 0 0 16"/>
                              <path d="M4.646 4.646a.5.5 0 0 1 .708 0L8 7.293l2.646-2.647a.5.5 0 0 1 .708.708L8.707 8l2.647 2.646a.5.5 0 0 1-.708.708L8 8.707l-2.646 2.647a.5.5 0 0 1-.708-.708L7.293 8 4.646 5.354a.5.5 0 0 1 0-.708"/>
                            </svg>  Unauthorize</button>`
                        }
                    }
                , "width": "15%" },

                {
                    data: 'id',
                    "render": function (data) {
                        return `<div class="w-75 btn-group" role="group">
                            <a class="btn btn-outline-secondary mx-2" href="/admin/posts/edit?id=${data}">
                                <i class="bi bi-pencil-square"></i> Edit
                            </a>
                            <a class="btn btn-outline-danger mx-2" href="/admin/posts/delete?id=${data}">
                                <i class="bi bi-trash3"></i> Delete
                            </a>
                        </div>`
                    },
                    "width": "15%"
                }
            ]
        });
    } 
}
function Delete(url) {
    Swal.fire({
        title: "Are you sure?",
        text: "You won't be able to revert this!",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "Yes, delete it!"
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: url,
                type: 'DELETE',
                success: function (data) {
                    dataTable.ajax.reload();
                    /*toastr.success(data.message);*/
                }
            });
        }
    });
}