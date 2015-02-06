$(document).ready(function () {
});

function BranchTimingEntry() {
    if ($("#ContentPlaceHolder1_drpBranch").val() == "-select-") {
        showAlert('Please select Branch', 'error', 'bottom');
        $("#ContentPlaceHolder1_drpBranch").focus();
        return false;
    }
    
}