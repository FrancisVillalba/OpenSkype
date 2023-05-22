function make_payment() {
    if ($("#txtTotalInvoiceAmount").val() == "") {
        toastr.error('Debes cargar una factura para realizar el pago.')

        return;
    }

    const v_amountPaid = $("#txtMontoLicitado").val();
    const v_change = $("#txtCambio").val();

    var settings = {
        "url": "/Payments/Insert_data_payments",
        "method": "POST",
        "timeout": 0,
        "headers": {
            "Content-Type": "application/json"
        },
        "data": JSON.stringify({
            //"ArticlesId": document.getElementById("ddlArticles").value,//$("#ddlArticles").val(),
            //"ArticlesAmount": $("#txtAmount").val(),
            //"ArticlesPrice": $("#txtPrice").val(),
            //"InvoicesHeaderId": $("#impIdInvoices").val(),
            //"CurrencyTypeId": $("#ddlCurrencyType").val() 
            "InvoiceHeaderId" : $("#impIdInvoices").val(),
            "PaymentTypeId": $("#ddlPaymentType").val(),
            "AmountPaid": v_amountPaid.replace(/[^\d,]/g, ""),
            "Change": v_change.replace(/[^\d,]/g, ""),
            "VoucherNumber": $("#txtNumeroVoucher").val()

        }),
    };

    $.ajax(settings).done(function (response) {
        if (response.status) {
            toastr.success("Pago registrado con exito!")

            /****************************LOAD DETAIL TABLE INVOICES************************************* */
            load_table_detail_invoices();

        } else {
            toastr.error(response.message)
        }

    });


}