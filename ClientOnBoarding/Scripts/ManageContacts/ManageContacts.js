// The module pattern
var fxMContact = (function () {

    var oTable;
    var maskPhoneNo = function () {
        $("#FirstPhoneNo").mask("999-999-9999", { placeholder: "x" });
        $("#SecondPhoneNo").mask("999-999-9999", { placeholder: "x" });
      //  $("#FirstPhoneNo").mask("", { placeholder: "Ext. No" });
      //  $("#ExtNosecond").mask("", { placeholder: "Ext. No" });
    }
    var bindOTable = function () {
        oTable = $('#myDataTable').dataTable({
            "bServerSide": true,
            "sAjaxSource": "/ManageContacts/GetContacts",
            "bProcessing": true,
            "sPaginationType": "full_numbers",
            "bPaginate": true,
            "bJQueryUI": true,                       
            "bAutoWidth": false,
            "oLanguage": {
                "sSearch": "Search Contact Name: "
            },
            "aoColumns": [
                { "sName": "ContactName", sWidth: '14.2%', "sClass": "wrapWord changFont" },
                { "sName": "Name", sWidth: '10.2%', "sClass": "wrapWord changFont" },
                { "sName": "Email", sWidth: '14.2%', "sClass": "wrapWord changFont" },
                { "sName": "FirstPhoneNo ", sWidth: '14.2%', "sClass": "wrapWord changFont" },
                { "sName": "SecondPhoneNo", sWidth: '14.2%', "sClass": "wrapWord changFont" },
                { "sName": "SMS", sWidth: '14.2%', "sClass": "wrapWord changFont" },
                { "sName": "ContactID", "sWidth": '7%', fnRender: editLink, "bSortable": false, "bSearchable": false, "sClass": "wrapWord changFont" }
            ],
            "fnDrawCallback": function (oSettings) {
                bindEditContact();
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
        var id = oObj.aData[6];
        var editDelete = "<img class='icon' src='/Images/edit.png' data-id='" + id + "' title='Edit Contact Information' data-link-edit='Edit' href='/ManageContacts/Contact/'>";
        editDelete = editDelete + "<img class='icon' src='/Images/delete.png' title='Delete Contact' data-link-delete='Delete'  href='/ManageContacts/DeleteContact/' data-id='" + id + "' />";
        return editDelete
    }

    var bindManageContacts = function () {
        $('#add_contact').on("click", function (e) {
            var link = $(this);
            contactModalPopUp(link, 0);

            // Prevent the normal behavior since we opened the dialog
            e.preventDefault();
        });

        //bindEditContact();
    };

    var bindEditContact = function () {
        $('[data-link-edit="Edit"]').on("click", function (e) {
            var link = $(this);
            contactModalPopUp(link)

            // Prevent the normal behavior since we opened the dialog
            e.preventDefault();
        });
        $('[data-link-delete="Delete"]').on("click", function (e) {

            var link = $(this);
            var contactLnk = link.attr('data-id');

            var linkUrl = link.attr('href');
            bootbox.confirm("Do you really want to delete contact information?", function (result) {
                if (result == true) {
                    var request = $.ajax({
                        type: "GET",
                        contentType: "application/json",
                        url: linkUrl,
                        dataType: "json",
                        data: { "ContactID": contactLnk }
                    });

                    request.done(function (data) {
                        if (data.success) {
                            bootbox.alert(data.errDesc);
                            oTable.fnReloadAjax();
                        }
                        else if (data.errors)
                            bootbox.alert(data.errDesc);

                    })
                }
            })

            e.preventDefault();
        });
    };

    var contactModalPopUp = function (link) {
        var linkUrl = link.attr('href');

        var contactID = 0;
        var contactLnk = link.attr('data-id');
        if (contactLnk == "undefined")
            contactID = 0;
        else
            contactID = contactLnk;

        var modalTitle = "Add Contact";
        if (contactID > 0)
            modalTitle = "Update Contact"

        if (typeof customerName != "undefined")
            modalTitle = modalTitle + ' - ' + customerName;

        var request = $.ajax({
            type: "GET",
            contentType: "application/x-www-form-urlencoded; charset=UTF-8",
            url: linkUrl,
            dataType: "html",
            data: { "ContactID": contactID }
        });

        request.done(function (data) {
            var contactDialog = $('<div class="dilogPading modal-popup">' + data + '</div>')
                .appendTo(document.body)
                .filter('div') // Filter for the div tag only, script tags could surface
                .dialog({ // Create the jQuery UI dialog
                    title: link.data('dialog-title') || modalTitle,
                    modal: true,
                    resizable: link.data('dialog-resize') || false,
                    draggable: false,
                    height:368,
                    width: link.data('dialog-width') || 643,
                    close: function (event, ui) {
                        $(this).dialog('destroy').remove();
                    }
                }).find('form') // Attach logic on forms
                        .submit(contactFormSubmitHandler)
                
                        .end();

            $fx.setUnobstrusiveControls("#dvContact");
            
            contactDialog.dialog('open');
            debugger;
            maskPhoneNo();
           
        });

        request.fail(function (jqXHR, textStatus) {
            handleAjaxError(jqXHR, textStatus);
        });
    };
   
        //bindEditContact();
   
    var contactFormSubmitHandler = function (e) {
        var $form = $(this);
        // We check if jQuery.validator exists on the form
        if (!$form.valid || $form.valid()) {
            $.post($form.attr('action'), $form.serializeArray())
                .done(function (json) {
                    json = json || {};
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

    var rebindContactGrid = function () {
        var manageContactLink = $('#manageContacts');

        var request = $.ajax({
            type: "GET",
            contentType: "application/x-www-form-urlencoded; charset=UTF-8",
            url: manageContactLink.attr('href'),
            dataType: "html"
        });

        request.done(function (data) {
            $('.midFrame').html(data);

            bindManageContacts();

            // Example call to reload from original file
            oTable.fnReloadAjax();
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
        bindManageContacts: bindManageContacts,
        bindEditContact: bindEditContact,
        bindOTable: bindOTable
    };

})();

$(document).ready(function () {
    fxMContact.bindManageContacts();
    fxMContact.bindOTable();
});