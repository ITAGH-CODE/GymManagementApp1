$(document).ready(function () {
    $('#list-clients').DataTable(
        {
            "lengthChange": false,
            "info": false,
            "processing": false,
            "searching": false,
            "ajax": {
                "url": "/Client/GetClients",
                "type": "GET",
                "datatype": "json"
            },
            "columns": [
                { "data": "FirstName" },
                { "data": "LastName" },
                { "data": "ICN" },
                { "data": "Phone" },
                {
                    "data": "IsPaymentOk", "render": function (data) {
                        if (data) {
                            return '<div class="icon-status"><i class="fa fa-check-square fa-lg text-success"></i></div>';
                        }
                        else
                            return '<div class="icon-status icon-status-highlight"><i class="fa fa-thumbs-down fa-lg text-danger"></i></div>';
                    }
                },
                {
                    "data": "IdClient", "render": function (data) {
                        return '<div class="actions-buttons"><a onclick="openInPopUpEdit(' + data + ')" class="btn btn-outline-info"><i class="fa fa-pencil"></i></a>&nbsp;<a onclick="showPopUpDelete(' + data + ')" class="btn btn-outline-info text-danger trigger-btn"><i class="fa fa-trash"></i></a></div>'
                    }
                }
            ]

        });
});