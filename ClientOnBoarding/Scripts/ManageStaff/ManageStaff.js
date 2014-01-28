var fxStaff = (function () {
    var oTable;

    var submitButton;

    var bindOTable = function () {
        var roleType = $('#add_staff');

        oTable = $("#StaffDataTable").dataTable({
            "bServerSide": true,
            "sAjaxSource": "/ManageStaff/GetStaffUsers",
            "sPaginationType": "full_numbers",
            "bPaginate": true,
            "bJQueryUI": true,
            "bProcessing": false,
            "bAutoWidth": false,
            "oLanguage": {
                "sSearch": "Search Staff Name: "
            },
            "aoColumns": [
                { "sName": "CustomerName", "sClass": "wrapWord changFont" },
                { "sName": "EmailAddress", "sClass": "wrapWord changFont" },
                { "sName": "CustomerID", "sWidth": '100', fnRender: editLink, "bSortable": false, "bSearchable": false, "sClass": "wrapWord changFont" }
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
        var id = oObj.aData[2];
        var editDelete = "<img class='icon' src='/Images/edit.png' data-id='" + id + "' title='Edit User Information' data-link-edit='Edit' href='/ManageStaff/Staff/'>";
        editDelete = editDelete + "<img class='icon' src='/Images/delete.png'data-id='" + id + "' title='Delete Contact'data-link-edit='Delete' href='/ManageStaff/DeleteStaff/'>";
        return editDelete
    };

    var bindEditUser = function () {
        $('[data-link-edit="Edit"]').on("click", function (e) {

            var link = $(this);

            UserModalPopUp(link)
            // Prevent the normal behavior since we opened the dialog
            e.preventDefault();
        });
        $('[data-link-edit="Delete"]').on("click", function (e) {
            e.preventDefault();
            var link = $(this);

            bootbox.confirm("Do you really want to delete staff information?", function (result) {
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

        var roleType = $('#add_staff');

        if (CustomerID > 0)
            modalTitle = "Update Staff User";
        else if (CustomerID == 0)
            modalTitle = "Add Staff User";

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
                    //height: 275,
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

    var bindManageStaff = function () {
        $('#add_staff').on("click", function (e) {
            debugger;
            var link = $(this);
            UserModalPopUp(link);

            // Prevent the normal behavior since we opened the dialog
            e.preventDefault();
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
        bindManageStaff: bindManageStaff,
        bindOTable: bindOTable
    };
})();

$(document).ready(function () {

    //ToDo: need to acctach event handler

    //1. Attach Event handler to "Add Staff" button    
    fxStaff.bindManageStaff();

    //2. Attach logic to set datatable programming on User grid
    fxStaff.bindOTable();
});
