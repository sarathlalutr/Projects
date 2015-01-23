$(document).ready(function () {
});

function DepartmentEntry() {
    if ($("#ContentPlaceHolder1_txtDepartmentName").val() == "") {
        showAlert('Please enter Department Name', 'error', 'bottom');
        $("#ContentPlaceHolder1_txtDepartmentName").focus();
        return false;
    }

}