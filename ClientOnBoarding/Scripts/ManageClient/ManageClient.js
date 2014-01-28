// The module pattern
var fxCmgmt = (function () {

    var oTable;

    var bindOTable = function () {
        oTable = $('#clientDataTable').dataTable({
            "bServerSide": true,
            "sAjaxSource": "/ManageClient/GetClients",
            "bProcessing": true,
            "sPaginationType": "full_numbers",
            "bPaginate": true,
            "bJQueryUI": true,
            "bProcessing": false,            
            "bAutoWidth": false,
            "oLanguage": {
                "sSearch": "Search Business Name: "
            },
            "aoColumns": [
                { "sName": "BusinessName", sWidth: '22.5%', "sClass": "wrapWord changFont" },
                { "sName": "ServiceTypeName", sWidth: '22.5%', "sClass": "wrapWord changFont" },
                { "sName": "TimeZoneName", sWidth: '22.5%', "sClass": "wrapWord changFont" },
                { "sName": "StatusName", sWidth: '22.5%', "sClass": "wrapWord changFont" },
                { "sName": "ClientID", "sWidth": '7%', fnRender: editLink, "bSortable": false, "bSearchable": false, "sClass": "wrapWord changFont" }
            ],
            "fnDrawCallback": function (oSettings) {
                bindEditContact();
                deleteclient();
            }
        });

        $('.dataTables_filter input')
        .unbind('keypress keyup')
        .bind('keypress keyup', function (e) {
            if ($(this).val().length < 3 && e.keyCode != 13) return;
            oTable.fnFilter($(this).val());
        });
    };

    var editLink = function make_delete_link(oObj) {
        var id = oObj.aData[4];
        var editDelete = "<img class='icon' src='/Images/edit.png' data-id='" + id + "' title='Edit Client Information' data-link-edit='Edit' href='/ManageClient/Client/'>";
        editDelete = editDelete + "<img class='icon' src='/Images/delete.png' title='Delete Client' data-link-delete='Delete'  href='/ManageClient/deleteClient/'  data-id='" + id + "' />";
        return editDelete
    }

    var deleteclient = function () {
        $('[ data-link-delete="Delete"]').on("click", function (e) {
            var link = $(this);
            var clientid = link.attr("data-id");

            bootbox.confirm("Do you really want to delete client information?", function (result) {

                if (result == true) {
                    var action = $.ajax({
                        type: "GET",
                        contentType: "application/json",
                        url: link.attr('href'),
                        dataType: "json",
                        data: { "clientid": clientid }
                    });
                    action.done(function (data) {
                        debugger;
                        if (data.success) {
                            bootbox.alert(data.resultmsg);
                            oTable.fnReloadAjax();
                        }
                        else if (!data.success)
                            bootbox.alert(data.resultmsg);

                    })

                }
            })
            e.preventDefault();
        });
    }

    var bindManageClient = function () {
        $('#add_client').on("click", function (e) {
            debugger;
            var link = $(this);
            clientModalPopUp(link);

            // Prevent the normal behavior since we opened the dialog
            e.preventDefault();
        });
    };

    var bindEditContact = function () {
        $('[data-link-edit="Edit"]').on("click", function (e) {
            var link = $(this);

            clientModalPopUp(link)
            // Prevent the normal behavior since we opened the dialog
            e.preventDefault();
        });
    }

    var clientModalPopUp = function (link) {

        var linkUrl = link.attr('href');
        var clientID = 0;

        var clientLnk = link.attr('data-id');
        if (clientLnk == "undefined")
            clientID = 0;
        else
            clientID = clientLnk;

        var modalTitle = "Add Client";
        if (clientID > 0)
            modalTitle = "Update Client"

        if (typeof customerName != "undefined")
            modalTitle = modalTitle + ' - ' + customerName;

        var request = $.ajax({
            type: "GET",
            contentType: "application/x-www-form-urlencoded; charset=UTF-8",
            url: linkUrl,
            dataType: "html",
            data: { "clientID": clientID }
        });

        request.done(function (data) {
            debugger;
            var contactDialog = $('<div class="dilogPading modal-popup">' + data + '</div>')
                .appendTo(document.body)
                .filter('div') // Filter for the div tag only, script tags could surface
                .dialog({ // Create the jQuery UI dialog
                    title: link.data('dialog-title') || modalTitle,
                    modal: true,
                    resizable: link.data('dialog-resize') || false,
                    draggable: false,                  
                    //height: 303,
                    height: 'auto',
                    width: link.data('dialog-width') || 642,
                    close: function (event, ui) {
                        $(this).dialog('destroy').remove();
                    }
                }).find('form') // Attach logic on forms
                        .submit(contactFormSubmitHandler)
                        .end();

            $fx.setUnobstrusiveControls("#dvClient");
            contactDialog.dialog('open');
        });

        request.fail(function (jqXHR, textStatus) {
            handleAjaxError(jqXHR, textStatus);
        });
    };

    var contactFormSubmitHandler = function (e) {
        var $form = $(this);
        // We check if jQuery.validator exists on the form
        if (!$form.valid || $form.valid()) {
            $.post($form.attr('action'), $form.serializeArray())
                .done(function (json) {
                    json = json || {};
                    debugger;
                    if (json.success) {
                        $(".modal-popup").dialog("close");
                        //reload the grid again.  
                        oTable.fnReloadAjax();
                    } else if (json.errors) {
                        $fx.displayErrors($form, json.errors);
                    }
                })
                .error(function () {
                    var keyValList = new Object();
                    keyValList.Key = '';
                    keyValList.Value = 'An unknown error happened.';
                    var errors = new Object();
                    errors[0] = keyValList;
                    $fx.displayErrors($form, errors);
                });
        }

        // Prevent the normal behavior since we opened the dialog
        e.preventDefault();
    };

    var rebindClientGrid = function () {
        var manageClientLink = $('#manageClient');

        var request = $.ajax({
            type: "GET",
            contentType: "application/x-www-form-urlencoded; charset=UTF-8",
            url: manageClientLink.attr('href'),
            dataType: "html"
        });

        request.done(function (data) {
            $('.midFrame').html(data);

            bindManageClient();
        });

        request.fail(function (jqXHR, textStatus) {
            handleAjaxError(jqXHR, textStatus);
        });
    };

    var handleAjaxError = function (jqXHR, textStatus) {
        if (jqXHR.status == 401) {
            // perform a redirect to the login page since we're no longer authorized
            window.location = $('#loginURL').val();
        }
        else {
            alert("Error: " + jqXHR);
            alert("Request failed: " + textStatus);
        }
    };

    // public API
    return {
        bindManageClient: bindManageClient,
        bindOTable: bindOTable
    };
})();

$(document).ready(function () {    
    fxCmgmt.bindManageClient();
    fxCmgmt.bindOTable();
});