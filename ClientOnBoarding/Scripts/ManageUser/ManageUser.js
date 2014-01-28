var fxUser = (function () {
    var oTable;
    var submitButton;

    var bindOTable = function () {
        var roleType = $('#Add_User');

        oTable = $("#UserDataTable").dataTable({
            "bServerSide": true,
            "sAjaxSource": "/ManageUser/" + roleType.data('roleurl'),
            "bProcessing": true,
            "sPaginationType": "full_numbers",
            "bPaginate": true,
            "bJQueryUI": true,
            "bProcessing": false,            
            "bAutoWidth": false,
            "oLanguage": {
                "sSearch": "Search Customer Name: "
            },
            "aoColumns": [
                { "sName": "CustomerName", sWidth: '16.6%', "sClass": "wrapWord changFont" },
                { "sName": "EmailAddress", sWidth: '16.6%', "sClass": "wrapWord changFont" },
                { "sName": "TimeZoneName", sWidth: '16.6%', "sClass": "wrapWord changFont" },
                { "sName": "CustomerContactName", sWidth: '16.6%', "sClass": "wrapWord changFont" },
                { "sName": "NOCCommunicationName", sWidth: '16.6%', "sClass": "wrapWord changFont" },
                { "sName": "CustomerID", "sWidth": '9%', fnRender: editLink, "bSortable": false, "bSearchable": false, "sClass": "wrapWord changFont" }
            ],
            "fnDrawCallback": function (oSettings) {
                bindEditUser();
            }
        });

        $('.dataTables_filter input')
        .unbind('keypress keyup')
        .bind('keypress keyup', function (e) {
            if ($(this).val().length < 3 && e.keyCode != 13) return;
            oTable.fnFilter($(this).val());
        });
    };

    var editLink = function (oObj) {
        var id = oObj.aData[5];
        var editDelete = "<img class='icon' src='/Images/edit.png' data-id='" + id + "' title='Edit User Information' data-link-edit='Edit' href='/ManageUser/User/'>";
        editDelete = editDelete + "<img class='icon' src='/Images/delete.png'data-id='" + id + "' title='Delete Contact'data-link-edit='Delete' href='/ManageUser/DeleteUser/'>";
        editDelete = editDelete + "<img class='icon' src='/Images/emailid.png' data-id='" + id + "' title='Send User Name and Password mail' data-link-edit='Email' href='/ManageUser/Email/'>";
        return editDelete
    };

    var bindEditUser = function () {
        $('[data-link-edit="Edit"]').on("click", function (e) {

            var link = $(this);

            UserModalPopUp(link)
            // Prevent the normal behavior since we opened the dialog
            e.preventDefault();
        });
        $('[data-link-edit="Email"]').on("click", function (e) {
            debugger;
            var link = $(this);

            var request = $.ajax({
                type: "GET",
                contentType: "application/json",
                url: link.attr('href'),
                dataType: "json",
                data: { "CustomerID": link.attr('data-id') }
            });

            request.done(function (json) {
                debugger;
                json = json || {};
                if (json.success) {
                    bootbox.alert(json.MessageDetail, function () {
                    });
                }
                else if (json.errors) {
                    $fx.displayErrors($form, json.errors);
                }
            });

            request.fail(function (jqXHR, textStatus) {
                debugger;
                $fx.handleAjaxError(jqXHR, textStatus);
            });
            // Prevent the normal behavior since we opened the dialog
            e.preventDefault();
        });
        $('[data-link-edit="Delete"]').on("click", function (e) {
            e.preventDefault();
            var link = $(this);

            bootbox.confirm("Do you really want to delete user information?", function (result) {
                if (result == true) {
                    var request = $.ajax({
                        type: "GET",
                        contentType: "application/json",
                        url: link.attr('href'),
                        dataType: "json",
                        data: { "CustomerID": link.attr('data-id') }
                    });

                    request.done(function (json) {
                        json = json || {};
                        if (json.success) {
                            bootbox.alert(json.resultmsg);
                            oTable.fnReloadAjax();
                        }
                        else {
                            bootbox.alert(json.resultmsg);
                        }
                    });

                    request.fail(function (jqXHR, textStatus) {
                        $fx.handleAjaxError(jqXHR, textStatus);
                    });
                }
            })

        });
    }

    var bindSubmitHandlers = function () {
        $('#Sbmt').on("click", function (e) {
            submitButton = $(this);
        });

        $('#SbmtEmail').on("click", function (e) {
            submitButton = $(this);
        });
    }

    var UserModalPopUp = function (link) {
        debugger;
        var linkUrl = link.attr('href');
        var CustomerID = 0;
        var contactLnk = link.attr('data-id');
        if (contactLnk == "undefined")
            CustomerID = 0;
        else
            CustomerID = contactLnk;

        var roleType = $('#Add_User');

        if (roleType.data('roleurl') == "GetAdminUsers" && CustomerID > 0)
            modalTitle = "Update Admin User";
        else if (roleType.data('roleurl') == "GetAdminUsers" && CustomerID == 0)
            modalTitle = "Add Admin User";
        else if (roleType.data('roleurl') == "GetNormalUsers" && CustomerID > 0)
            modalTitle = "Update Normal User";
        else if (roleType.data('roleurl') == "GetNormalUsers" && CustomerID == 0)
            modalTitle = "Add Normal User";

        var request = $.ajax({
            type: "GET",
            contentType: "application/x-www-form-urlencoded; charset=UTF-8",
            url: linkUrl,
            dataType: "html",
            data: { "CustomerID": CustomerID }
        });

        request.done(function (data) {
            var UserDialog = $('<div class="dilogPading modal-popup">' + data + '</div>')
                .appendTo(document.body)
                .filter('div') // Filter for the div tag only, script tags could surface
                .dialog({ // Create the jQuery UI dialog
                    title: link.data('dialog-title') || modalTitle,
                    modal: true,
                    resizable: link.data('dialog-resize') || false,
                    draggable: false,
                    //height: 370,
                    height: 'auto',
                    width: link.data('dialog-width') || 642,
                    close: function (event, ui) {
                        $(this).dialog('destroy').remove();
                    }
                }).find('form') // Attach logic on forms
                        .submit(UserFormSubmitHandler)
                        .end();

            $fx.setUnobstrusiveControls("#dvUser");
            bindCheckBoxUserPassword();
            bindSubmitHandlers();
            UserDialog.dialog('open');
        });

        request.fail(function (jqXHR, textStatus) {
            $fx.handleAjaxError(jqXHR, textStatus);
        });
    };

    var UserFormSubmitHandler = function (e) {
        debugger;
        var $form = $(this);

        // We check if jQuery.validator exists on the form
        if (!$form.valid || $form.valid()) {
            var data = $form.serializeArray();
            data.push({ name: 'SubmitButtonID', value: submitButton.attr('id') });

            $.post($form.attr('action'), data)
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

        submitButton = null;
        // Prevent the normal behavior since we opened the dialog
        e.preventDefault();
    };

    var bindManageUser = function () {
        $('#Add_User').on("click", function (e) {
            debugger;
            var link = $(this);
            UserModalPopUp(link);

            // Prevent the normal behavior since we opened the dialog
            e.preventDefault();
        });
    };

    var rebindUserGrid = function () {
        var manageUserLink = $('#adminuser');

        var request = $.ajax({
            type: "GET",
            contentType: "application/x-www-form-urlencoded; charset=UTF-8",
            url: manageUserLink.attr('href'),
            dataType: "html"
        });

        request.done(function (data) {
            $('.midFrame').html(data);

            bindManageUser();
        });

        request.fail(function (jqXHR, textStatus) {
            $fx.handleAjaxError(jqXHR, textStatus);
        });
    };

    var bindCheckBoxUserPassword = function () {
        $('#userPasswordChkbox').change(function () {
            debugger;
            if ($("#userPasswordChkbox").is(':checked')) {
                $('#Password').get(0).type = 'text';
            }
            else {
                $('#Password').get(0).type = 'Password';
            }
        });
    }

    // public API
    return {
        bindOTable: bindOTable,
        bindManageUser: bindManageUser,
        UserModalPopUp: UserModalPopUp,
        UserFormSubmitHandler: UserFormSubmitHandler,
        bindEditUser: bindEditUser,
        rebindUserGrid: rebindUserGrid,
    };
})

    ();

$(document).ready(function () {

    //ToDo: need to acctach event handler

    //1. Attach Event handler to "Add User" button    
    fxUser.bindManageUser();

    //2. Attach logic to set datatable programming on User grid
    fxUser.bindOTable();
});
