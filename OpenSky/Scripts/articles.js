$('#btnAdd').click(function () {
    $(location).prop('href', '/Articles/Insert_articles')
});

$("#example1").on("click", "#btnEdit", function () {
    var id = "";

    //$(this).parents("tr").find("td:first").each(function () {
    //    valores += $(this).html() + "\n";
    //});

    $(this).parents("tr").each(function () {
        id = $(this).find('input[type="hidden"]').val();
    }); 

    window.location.replace("/articles/Update_articles?Id=" + id);
});

$('#btnBack').click(function () {
    $(location).prop('href', '/Articles/Articles')
});

$('#btnUpdate').click(function () {  
    if (Load_control()) {
        //conecta con el controlador
        var settings = {
            "url": "/Articles/update_datos_articles",
            "method": "POST",
            "timeout": 0,
            "headers": {
                "Content-Type": "application/json"
            },
            "data": JSON.stringify({
                "Id": $("#lblId").html(),
                "ReferenceInternal": $("#txtCodigo").val(),
                "Name": $("#txtNombre").val(), 
                "Iva": $("#ddlTaxes").val(),
                "Price": $("#txtPrecio").val(),
                "categorie_id": $("#ddlCategorias").val(),
                "MinimumAmount": $("#txtCantidadMinima").val()
            }),
        };

        $.ajax(settings).done(function (response) {
            if (response.status) {
                toastr.success("Se guardo con exito!")
                $(location).prop('href', '/Articles/Articles')
            } else {
                toastr.error(response.message)
            }

        });
    } 
});

$('#btnSave').click(function () {

    if (Load_control()) {
        //conecta con el controlador
        var settings = {
            "url": "/Articles/Insert_data_articles",
            "method": "POST",
            "timeout": 0,
            "headers": {
                "Content-Type": "application/json"
            },
            "data": JSON.stringify({
                "ReferenceInternal": $("#txtCodigo").val(),
                "Name": $("#txtNombre").val(),
                //"InitialAmount": $("#txtCantidadMinima").val(),
                "Iva": $("#ddlTaxes").val(),
                "Price": $("#txtPrecio").val(), 
                "categorie_id": $("#ddlCategorias").val(),
                "MinimumAmount": $("#txtCantidadMinima").val() 
            }),
        };

        $.ajax(settings).done(function (response) {
            if (response.status) {
                toastr.success("Se guardo con exito!")
                $(location).prop('href', '/Articles/Articles')
            } else {
                toastr.error(response.message)
            }

        });
    }

});

function Load_control() {
    var statusLoad = true

    var name = $("#txtNombre").val();
    if (name.length == 0) {
        statusLoad = false
        toastr.error('Ingrese nombre')
        return;
    }

    var codigo = $("#txtCodigo").val();
    if (codigo.length == 0) {
        statusLoad = false
        toastr.error('Ingrese código')
        return;
    }

    var categoria = $("#ddlCategorias").val();
    if (categoria.length == 0) {
        statusLoad = false
        toastr.error('Seleccione categoría')
        return;
    }

    var precio = $("#txtPrecio").val();
    if (precio.length == 0) {
        statusLoad = false
        toastr.error('Ingrese precio')
        return;
    }

    var minima = $("#txtCantidadMinima").val();
    if (minima.length == 0) {
        statusLoad = false
        toastr.error('Ingrese cantidad mínima')
        return;
    }

    var taxes = $("#ddlTaxes").val();
    if (taxes.length == 0) {
        statusLoad = false
        toastr.error('Seleccione impuesto')
        return;
    }

    return statusLoad
}

$("#example1").on("click", ".my-checkBox", function () {
    //var seleccion = []; 
    //row = $(this).closest('tr');
    //seleccion.push({
    //    Id: row.find('td:eq(0)').text(),
    //    nombre: row.find('td:eq(1)').text()
    //});

    var id = "";
    var status = "I";
    //$(this).parents("tr").find("td:first").each(function () {
    //    id += $(this).html() + "\n";
    //});
    $(this).parents("tr").each(function () {
        id = $(this).find('input[type="hidden"]').val();
    }); 

    var curRow = $('#' + $.trim(id));
    if ($(curRow).prop('checked')) {
        status = "A";
    }


    //    //conecta con el controlador
    var settings = {
        "url": "/Articles/Update_status_articles",
        "method": "POST",
        "timeout": 0,
        "headers": {
            "Content-Type": "application/json"
        },
        "data": JSON.stringify({
            "id": id,
            "status": status
        }),
    };

    $.ajax(settings).done(function (response) {
        if (response.status) {
            toastr.success("Registro modificado correctamente.!")
        } else {
            toastr.error(response.message)
        }

    });
});