$(document).ready(function () {
});

function EditDesignationEntry() {
    if ($("#txtDesignationName").val() == "") {
        showOverAlert('Please enter Designation', 'error', 'bottom');
        $("#txtDesignationName").focus();
        return false;
    }

}