$(document).ready(function () {
});

function detailsEntry() {
    if ($("#ContentPlaceHolder1_txtEmployeeId").val() == "") {
        showAlert('Please enter Employee ID', 'error', 'bottom');
        $("#ContentPlaceHolder1_txtEmployeeId").focus();
        return false;
    }
    else if ($("#ContentPlaceHolder1_txtEmployeeName").val() == "") {
        showAlert('Please enter Employee Name', 'error', 'bottom');
        $("#ContentPlaceHolder1_txtEmployeeName").focus();
        return false;
    }
    else if ($("#ContentPlaceHolder1_drpDesignation").val() == "-select-") {
        showAlert('Please select Designation', 'error', 'bottom');
        $("#ContentPlaceHolder1_drpDesignation").focus();
        return false;
    }
    else if ($("#ContentPlaceHolder1_drpDescription").val() == "-select-") {
        showAlert('Please select Description', 'error', 'bottom');
        $("#ContentPlaceHolder1_drpDescription").focus();
        return false;
    }
    else if ($("#ContentPlaceHolder1_drpBranch").val() == "-select-") {
        showAlert('Please select Branch', 'error', 'bottom');
        $("#ContentPlaceHolder1_drpBranch").focus();
        return false;
    }
    else if ($("#ContentPlaceHolder1_drpDepartment").val() == "-select-") {
            showAlert('Please select Department', 'error', 'bottom');
            $("#ContentPlaceHolder1_drpDepartment").focus();
            return false;
    }
    else if ($("#ContentPlaceHolder1_txtSwipeInTime").val() == "") {
            showAlert('Please select Swipe In Time', 'error', 'bottom');
            $("#ContentPlaceHolder1_txtSwipeInTime").focus();
            return false;
    }
    else if ($("#ContentPlaceHolder1_drpSwipeInLocation").val() == "-select-") {
            showAlert('Please select Swipe In Location', 'error', 'bottom');
            $("#ContentPlaceHolder1_drpSwipeInLocation").focus();
            return false;
    }
    else if ($("#ContentPlaceHolder1_txtSwipeOutTime").val() == "") {
            showAlert('Please select Swipe Out Time', 'error', 'bottom');
            $("#ContentPlaceHolder1_txtSwipeOutTime").focus();
            return false;
    }
    else if ($("#ContentPlaceHolder1_drpSwipeOutLocation").val() == "-select-") {
        showAlert('Please select Swipe Out Location', 'error', 'bottom');
        $("#ContentPlaceHolder1_drpSwipeOutLocation").focus();
        return false;
    }
    else if ($("#ContentPlaceHolder1_txtAprovedBy").val() == "") {
        showAlert('Please enter approver', 'error', 'bottom');
        $("#ContentPlaceHolder1_txtAprovedBy").focus();
        return false;
    }
    else if ($("#ContentPlaceHolder1_txtApprovedDate").val() == "") {
        showAlert('Please enter approved Date', 'error', 'bottom');
        $("#ContentPlaceHolder1_txtApprovedDate").focus();
        return false;
    }
}