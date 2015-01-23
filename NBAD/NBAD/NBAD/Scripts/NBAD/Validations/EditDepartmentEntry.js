$(document).ready(function () {
});

function EditDepartmentEntry() {
    if ($("#txtDepartmentName").val() == "") {
        showOverAlert('Please enter Department Name', 'error', 'bottom');
        $("#txtDepartmentName").focus();
        return false;
    }

}