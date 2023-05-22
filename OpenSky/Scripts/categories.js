
$('#btnBack').click(function () {
    $(location).prop('href', '/Categories/Categories')
});

$('#btnAdd').click(function () {
    $(location).prop('href', '/Categories/insert_categories')
});

$('#btnSave').click(function () {
    var name = $("#txtCategorieName").val();


    //controla que el campo se haya cargado
    if (name.length == 0) {
        toastr.error('Ingrese nombre de la categoría')
        return;
    }

    //conecta con el controlador
    var settings = {
        "url": "/Categories/Insert_articles_categories",
        "method": "POST",
        "timeout": 0,
        "headers": {
            "Content-Type": "application/json"
        },
        "data": JSON.stringify({
            "name": name
        }),
    };

    $.ajax(settings).done(function (response) {
        if (response.status) {
            toastr.success("Se guardo con exito!")
            $(location).prop('href', '/Categories/Categories')
        } else {
            toastr.error(response.message)
        }

    });
});

$('#btnUpdate').click(function () {
    var name = $("#txtUpdtateCategorieName").val();
    var lblID = $("#lblId").html();

    //controla que el campo se haya cargado
    if (name.length == 0) {
        toastr.error('Ingrese nombre de la categoría')
        return;
    }

    //conecta con el controlador
    var settings = {
        "url": "/Categories/Update_datos_articles_categories",
        "method": "POST",
        "timeout": 0,
        "headers": {
            "Content-Type": "application/json"
        },
        "data": JSON.stringify({
            "name": name,
            "id": lblID
        }),
    };

    $.ajax(settings).done(function (response) {
        if (response.status) {
            toastr.success("Se guardo con exito!")
            $(location).prop('href', '/Categories/Categories')
        } else {
            toastr.error(response.message)
        }

    });
});


$("#example1").on("click", "#btnEdit", function () {
    var valores = "";
   
    $(this).parents("tr").each(function () { 
        valores = $(this).find('input[type="hidden"]').val();
    }); 

    window.location.replace("/Categories/Update_categories?ID=" + valores); 
});
$("#example1").on("click", ".my-checkBox", function () {
    //var seleccion = []; 
    //row = $(this).closest('tr');
    //seleccion.push({
    //    Id: row.find('td:eq(0)').text(),
    //    nombre: row.find('td:eq(1)').text()
    //});

    var id = "";
    var status = "I";
    $(this).parents("tr").each(function () {
        id = $(this).find('input[type="hidden"]').val();
    }); 

    var curRow = $('#' + $.trim(id));
    if ($(curRow).prop('checked')) {
        status = "A";
    }


    //    //conecta con el controlador
    var settings = {
        "url": "/Categories/Update_status_articles_categories",
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
$("#example1").on("click", "#btnDelete", function () {
    var id = "";

    $(this).parents("tr").find("td:first").each(function () {
        id += $(this).html() + "\n";
    });

    var settings = {
        "url": "/Categories/Delete_datos_articles_categories",
        "method": "POST",
        "timeout": 0,
        "headers": {
            "Content-Type": "application/json"
        },
        "data": JSON.stringify({
            "id": id
        }),
    };

    $.ajax(settings).done(function (response) {
        if (response.status) {
            toastr.success("Registro eliminado correctamente.!")
            window.location.replace("/Categories/Categorie");
        } else {
            toastr.error(response.message)
        }

    });

})



