$(document).ready(function () {
});

function allswipeReport() {

    if($("#ContentPlaceHolder1_drpEmployeeId").val() == "-select-") {
        showAlert('Please select Employee No', 'error', 'bottom');
        $("#ContentPlaceHolder1_drpEmployeeId").focus();
        return false;
    }
    else if ($("#ContentPlaceHolder1_txtFromDate").val() == "") {
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