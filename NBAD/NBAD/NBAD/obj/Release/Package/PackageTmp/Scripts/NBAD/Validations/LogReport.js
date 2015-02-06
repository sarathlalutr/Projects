$(document).ready(function () {
});

function LogReport() {

    if ($("#ContentPlaceHolder1_txtFromDate").val() == "") {
        showAlert('Please enter From Date', 'error', 'bottom');
        $("#ContentPlaceHolder1_txtFromDate").focus();
        return false;
    }
    else if ($("#ContentPlaceHolder1_txtToDate").val() == "") {
        showAlert('Please enter To Date', 'error', 'bottom');
        $("#ContentPlaceHolder1_txtToDate").focus();
        return false;
    }
    showDashPop();
    return false;
}