$('#btnPrint').click(function () {
    const processNumber = $("#impIdInvoices").val();
     
    window.open('/Pdf/GeneratePdf?processNumber=' + processNumber, '_blank');
});
