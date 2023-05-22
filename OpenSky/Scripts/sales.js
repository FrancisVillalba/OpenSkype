$('#btnAdd').click(function () {
    $(location).prop('href', '/Sales/Sales_creation')
});

function Change_price() {
    //
    var cod = document.getElementById("ddlArticles").value;
    // 
    var settings = {
        "url": "/Articles/Get_articles_by_id",
        "method": "POST",
        "timeout": 0,
        "headers": {
            "Content-Type": "application/json"
        },
        "data": JSON.stringify({
            "Id": cod
        }),
    };

    $.ajax(settings).done(function (response) {
        document.getElementById("txtPrice").value = response.Price;
    });
};

$('#btnSave').click(function () {
    if (load_control()) {

        /****************************INSERT HEADER INVOICES************************************* */

        var settings = {
            "url": "/Sales/Insert_data_invoices_header",
            "method": "POST",
            "timeout": 0,
            "headers": {
                "Content-Type": "application/json"
            },
            "data": JSON.stringify({
                "CustomerId": document.getElementById("ddlCustomer").value,//$("#ddlCustomer").val(),
                "InvoicesDate": $("#txtCurrentDate").val(),
                "SalesConditionsId": $("#ddlSalesConditions").val(),
                "InvoicesHeaderId": $("#impIdInvoices").val(),
                "InvoicesCreated": $("#impInvoicesCreated").val(),
                "CurrencyTypeId": $("#ddlCurrencyType").val()
            }),
        };

        $.ajax(settings).done(function (response) {
            if (response.status) {
                $("#impIdInvoices").val(response.InvoicesHeaderId);
                $("#impInvoicesCreated").val("Created");

                /****************************INSERT DETAILS INVOICES************************************* */
                var settings = {
                    "url": "/Sales/Insert_data_invoices_detatail",
                    "method": "POST",
                    "timeout": 0,
                    "headers": {
                        "Content-Type": "application/json"
                    },
                    "data": JSON.stringify({
                        "ArticlesId": document.getElementById("ddlArticles").value,//$("#ddlArticles").val(),
                        "ArticlesAmount": $("#txtAmount").val(),
                        "ArticlesPrice": $("#txtPrice").val(),
                        "InvoicesHeaderId": $("#impIdInvoices").val(),
                        "CurrencyTypeId": $("#ddlCurrencyType").val()

                    }),
                };

                $.ajax(settings).done(function (response) {
                    if (response.status) {
                        $("#txtTotalInvoiceAmount").val(response.TotalInvoiceAmount);

                        /****************************LOAD DETAIL TABLE INVOICES************************************* */
                        load_table_detail_invoices();

                    } else {
                        toastr.error(response.message)
                    }

                });

            } else {
                toastr.error(response.message)
            }

        });
    }
});

function load_table_detail_invoices() {
    var settings = {
        "url": "/Sales/Get_detail_invoices_id",
        "method": "POST",
        "timeout": 0,
        "headers": {
            "Content-Type": "application/json"
        },
        "data": JSON.stringify({
            "InvoicesHeaderId": $("#impIdInvoices").val(),
            "TotalInvoiceAmount": $("#txtTotalInvoiceAmount").val(),
            "CurrencyTypeId": $("#ddlCurrencyType").val()
        }),
    };

    $.ajax(settings).done(function (response) {
        $('#bodyTabla').empty();
        var fila = "";
        $.each(response, function (ind, elem) {
            fila = "<tr><td>" + elem.Item + "</td><td>" + elem.Article + "</td><td>" + elem.AmountArticles + "</td><td>" + elem.Impuesto + "</td><td>" + elem.PriceUnit + "</td><td>" + elem.TotalPrice + "</td><td><input type='button' onclick='eliminarFila(" + elem.Item + "); ' value='Eliminar' /></td></tr>";
            $('#bodyTabla').append(fila)
        });
        clear_item();
    });
}

function eliminarFila(item) {
    var user_confirm = confirm("¿Está seguro que desea eliminar el registro?")
    if (user_confirm) {
        var settings = {
            "url": "/Sales/Delete_detail_invoices",
            "method": "POST",
            "timeout": 0,
            "headers": {
                "Content-Type": "application/json"
            },
            "data": JSON.stringify({
                "ItemDetail": item,
                "InvoicesHeaderId": $("#impIdInvoices").val()
            }),
        };

        $.ajax(settings).done(function (response) {
            if (response.status) {
                load_table_detail_invoices();
                toastr.success('Eliminado correctamente!')
            }
        });
    }
}

function clear_item() {
    $("#ddlArticles").val("").trigger('change');
    $("#txtAmount").val("");
    $("#txtPrice").val("");
};

function load_control() {
    var statusLoad = true

    var customer = $("#ddlCustomer").val();
    if (customer.length == 0) {
        statusLoad = false
        toastr.error('Seleccione un cliente')
        return;
    }

    var date = $("#txtCurrentDate").val();
    if (date.length == 0) {
        statusLoad = false
        toastr.error('Cargue fecha de factura')
        return;
    }

    var articles = $("#ddlArticles").val();
    if (articles.length == 0) {
        statusLoad = false
        toastr.error('Selecciona un artículo')
        return;
    }

    var amount = $("#txtAmount").val();
    if (amount.length == 0) {
        statusLoad = false
        toastr.error('Cargue una cantidad')
        return;
    }

    var price = $("#txtPrice").val();
    if (price.length == 0) {
        statusLoad = false
        toastr.error('Cargue el precio')
        return;
    }

    return statusLoad
};

function filter_table(opcion) {
    //console.log(opcion);
    //alert("Actualizando datos: " + opcion);

    var settings = {
        "url": "/Sales/SalesFilter",
        "method": "POST",
        "timeout": 0,
        "headers": {
            "Content-Type": "application/json"
        },
        "data": JSON.stringify({
            "filter": opcion,
            "data": $("#txtData").val()

        }),
    };

    $.ajax(settings).done(function (response) {
        $("#example1_wrapper").empty();
        $('#bodyTabla').empty();
        var fila = "";
        $.each(response, function (ind, elem) {
            fila = "<tr><td>" + elem.Process_number + "</td><td>" + elem.Invoices_number + "</td><td>" + elem.Timbrado + "</td><td>" + elem.DateInvoices + "</td><td>" + elem.AmountInvoices + "</td><td>" + elem.DateCreated + "</td><td>" + elem.Customer + "</td><td>" + elem.Branch + "</td><td>" + elem.User + "</td><td>" + elem.Status + "</td><td><a style='color: blue' id='btnEdit'><i class='fa fa-edit'></i></a></td></tr>";
            $('#bodyTabla').append(fila)
        });

        $("#txtData").val("");

        $("#example1").DataTable({
            //"columnDefs": [{ "visible": false, "targets": 0 }],
            order: [[0, 'ASC']],
            "responsive": true, "lengthChange": false, "autoWidth": false,
            "buttons": ["copy", "csv", "excel", "pdf", "print", "colvis"]
        }).buttons().container().appendTo('#example1_wrapper .col-md-6:eq(0)');
        $('#example2').DataTable({
            "paging": true,
            "lengthChange": false,
            "searching": false,
            "ordering": true,
            "info": true,
            "autoWidth": false,
            "responsive": true,
        });

        $("input[data-bootstrap-switch]").each(function () {
            $(this).bootstrapSwitch('state', $(this).prop('checked'));
        })
        //if (response.status) {
        //    $("#txtTotalInvoiceAmount").val(response.TotalInvoiceAmount);

        //    /****************************LOAD DETAIL TABLE INVOICES************************************* */
        //    load_table_detail_invoices();

        //} else {
        //    toastr.error(response.message)
        //}

    });
}

function control_type_payment(option) {
    debugger;
    var data = $("#ddlPaymentType").val();

    switch (data.toUpperCase()) {
        case "4-CH":
            $("#txtNumeroVoucher").prop('disabled', false);
            $("#txtCambio").prop('disabled', true);
            const total = $("#txtTotalInvoiceAmount").val();
            $("#txtMontoLicitado").val(total);
            know_change(this);
            break;
        case "1-EF":
            $("#txtNumeroVoucher").prop('disabled', true);
            $("#txtCambio").prop('disabled', true);
            $("#txtMontoLicitado").val("0");
            know_change(this);
            break;
        case "2-TC":
            $("#txtNumeroVoucher").prop('disabled', false);
            $("#txtCambio").prop('disabled', true);
            const total1 = $("#txtTotalInvoiceAmount").val();
            $("#txtMontoLicitado").val(total1);
            know_change(this);
            break;
        case "3-TD":
            $("#txtNumeroVoucher").prop('disabled', false);
            $("#txtCambio").prop('disabled', true);
            const total2 = $("#txtTotalInvoiceAmount").val();
            $("#txtMontoLicitado").val(total2);
            know_change(this);
            break;
        default:
        // code block
    }
} 

function know_change(data) {
    //debugger;
    const total = $("#txtTotalInvoiceAmount").val();

    let montoLicitado = $("#txtMontoLicitado").val();
    if (montoLicitado == "") {
        montoLicitado = "0";
    }
      
    const result = montoLicitado.replace(/[^\d,]/g, "") - total.replace(/[^\d,]/g, ""); 
    const formateador = new Intl.NumberFormat('es-ES');
    const numeroFormateado = formateador.format(result); 
    $("#txtCambio").val(numeroFormateado); 
}

$("#example102").on("click", "#btnEdit", function () {
    var id = "";
    debugger;
    //$(this).parents("tr").find("td:first").each(function () {
    //    valores += $(this).html() + "\n";
    //});

    $(this).parents("tr").each(function () {
        id = $(this).find('input[type="hidden"]').val();
    });

    window.location.replace("/Sales/Sales_update?Id=" + id);
});


