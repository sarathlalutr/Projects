$(document).ready(function () {
});

function DesignationEntry() {
    if ($("#ContentPlaceHolder1_txtDesignationName").val() == "") {
        showAlert('Please enter Designation', 'error', 'bottom');
        $("#ContentPlaceHolder1_txtDesignationName").focus();
        return false;
    }

}