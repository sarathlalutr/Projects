$(document).ready(function () {
    $("#aSearch").click(function () {
        $("#searchDiv").slideToggle("fast");
    });

});

function showOverAlert(message, msgType, location) {
    //alert("from base");
    var n = noty({
        layout: location,
        theme: 'defaultTheme',
        type: msgType, //Alert  Success  Error  Warning  Information  Confirm
        text: message, // can be html or string
        modal: true,

        timeout: 4000, // delay for closing event. Set false for sticky notifications
        force: false, // adds notification to the beginning of queue when set to true
        killer: true, // for close all notifications before show
        maxVisible: 5, // you can set max visible notification for dismissQueue true option,
        closeWith: ['click'] // ['click', 'button', 'hover']
        //   , buttons: [
        //{
        //    addClass: 'btn btn-primary', text: 'Ok', onClick: function ($noty) {

        //        // this = button element
        //        // $noty = $noty element

        //        $noty.close();
        //        //noty({ text: 'You clicked "Ok" button', type: 'success' });
        //    }
        //}
        //    ]
    });
}

function showAlert(message, msgType, location) {
    var msgNewType = msgType;
    if (msgType == "success")
        msgNewType = "information";
    new $.Zebra_Dialog(message, {
        'type': msgNewType, //"error", "warning", "question", "information"
        'title': msgType,
        'position': ['center', 'middle'],
        'buttons': false,
        'modal': true,
    });
}

function showConfirm(message) {
    new $.Zebra_Dialog(message, {
        'type': 'question', //"error", "warning", "question", "information"
        'title': 'Confirm',
        'buttons': [
            { caption: 'Yes', callback: function () { return true; } },
            //{caption: 'No', callback: function() { alert('"No" was clicked')}},
            { caption: 'Cancel', callback: function () { return false; } }
        ],
        'modal': true,
    });
}

function showConfirm(message) {
    var n = noty({
        text: message, //'Are you sure?',
        type: 'confirm',
        dismissQueue: false,
        layout: 'center',
        modal: true,
        theme: 'defaultTheme',
        buttons:
        [
            {
                addClass: 'btn btn-primary',
                text: 'Ok',
                onClick: function ($noty) {
                    $noty.close();
                    return true;
                }
            },
            {
                addClass: 'btn btn-danger',
                text: 'Cancel',
                onClick: function ($noty) {
                    $noty.close();
                    return false;
                }
            }
        ]
    });
}

function showDivPageLoading() {
    var argLen = arguments.length;
    var ky = "";

    if (argLen == 0) {
        ky = "";
    } else if (argLen == 1) {
        ky = arguments[0];
    }

    $("#divPageLoading" + ky).show();
}

function hideDivPageLoading() {
    var argLen = arguments.length;
    var ky = "";

    if (argLen == 0) {
        ky = "";
    } else if (argLen == 1) {
        ky = arguments[0];
    }
    //alert(ky);
    $("#divPageLoading" + ky).fadeOut("fast");
}

function showDashPop() {
    $("#divDashPop").fadeIn();
    return false;
}

function hideDashPop() {
    $("#divDashPop").fadeOut();
    return false;
}

function getTodayDate() {
    var today = new Date();
    var dd = today.getDate();
    var mm = today.getMonth() + 1; //January is 0!
    var yyyy = today.getFullYear();

    if (dd < 10) {
        dd = '0' + dd;
    }

    if (mm < 10) {
        mm = '0' + mm;
    }

    today = dd + '/' + mm + '/' + yyyy;
    return today;
}

function getTodatDatetime() {

    var today = new Date();
    var dd = today.getDate();
    var mm = today.getMonth() + 1; //January is 0!
    var yyyy = today.getFullYear();
    var hh = today.getHours();
    var min = today.getMinutes();

    if (dd < 10) {
        dd = '0' + dd;
    }

    if (mm < 10) {
        mm = '0' + mm;
    }

    if (hh < 10) {
        hh = '0' + hh;
    }

    if (min < 10) {
        min = '0' + min;
    }

    var time = hh + ":" + min;
    var todayDate = dd + '/' + mm + '/' + yyyy + ' ' + time;

    return todayDate;
}

