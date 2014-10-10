var str = "";
function getpagesCommandHandler(sender, args) {

    

    if (args._commandName == "GetSubscribers") {
        jQuery.ajax({
            type: "GET",
            url: sender._baseUrl + "Sitefinity/Public/Services/GetFormsSubscribers/GetSubscribers.svc/GetSingleFormSubscribers/?itemId=" + args._dataItem.Id,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            processdata: false,
            success: function (data) {
                if (data != "ERROR") {

                    var myWindow = window.open("", "MsgWindow" + args._dataItem.Id, "toolbar=yes, scrollbars=yes,menubar=yes, resizable=yes, top=500, left=500, width=400, height=400");
                    myWindow.document.write("<html><head></head><body></body></html>");

                    myWindow.document.write("<h2>Emails of the users subscribed for forms notifications are:</h2>" + data);
                }
            },
            error: function (data) { }
        });


    }
}

function OnMasterViewLoadedCustom(sender, args) {

    sender.add_itemCommand(getpagesCommandHandler);
}
