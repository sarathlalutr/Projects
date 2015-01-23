$(document).ready(function () {
});

function DescriptionEntry() {
    if ($("#ContentPlaceHolder1_txtDescription").val() == "") {
        showAlert('Please enter Description', 'error', 'bottom');
        $("#ContentPlaceHolder1_txtDescription").focus();
        return false;
    }

}