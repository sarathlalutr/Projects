$(document).ready(function () {
});

function EditDescriptionEntry() {
    if ($("txtDescription").val() == "") {
        showOverAlert('Please enter Description', 'error', 'bottom');
        $("#txtDescription").focus();
        return false;
    }

}