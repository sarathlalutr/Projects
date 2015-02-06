$(document).ready(function () {
});

function LocationEntry() {
    if ($("#ContentPlaceHolder1_txtLocation").val() == "") {
        showAlert('Please enter Location', 'error', 'bottom');
        $("#ContentPlaceHolder1_txtLocation").focus();
        return false;
    }

}