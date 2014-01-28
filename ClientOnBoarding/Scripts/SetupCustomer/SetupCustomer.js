// The module pattern
var fxSC = (function () {

    var oTable;

    var bindTabClick = function () {
        $(".tabbable").find('[data-toggle="tab"]').on('click.tab.data-api', function (e) {

            e.preventDefault()

            var actionLink = e.target.attributes["data-link"];
            var customerID = e.target.attributes["data-id"];

            if (actionLink.value != "") {
                //Main data Call
                var request = $.ajax({
                    type: "GET",
                    contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                    url: actionLink.value,
                    dataType: "html",
                    data: "CustomerID=" + customerID.value
                });

                request.done(function (data) {

                    var customerDetail = $('#customerDetail');
                    customerDetail.html(data);

                    $fx.setUnobstrusiveControls("#customerDetail");

                    if (actionLink.value.indexOf("/SetupCustomer/MPIndex") !== -1) {
                        bindMaintenancePolicyControls();
                        bindMaintenanceTable();
                    }
                    else if (actionLink.value.indexOf("/SetupCustomer/AVPIndex") !== -1) {
                        bindAntiVirusPolicyControls();
                        bindAVPolicyTable();
                    }
                    else if (actionLink.value.indexOf("/SetupCustomer/BPIndex") !== -1) {
                        bindBackUpPolicyControls();
                        bindBackupPolicyTable();
                    }
                    else if (actionLink.value.indexOf("/SetupCustomer/ToolInformation") !== -1) {
                        bindSetupCustomerTabsSubmitHandler('#customerDetail');
                        bindCheckBoxClick();
                    }
                    else if (actionLink.value.indexOf("/SetupCustomer/AccessPolicy") !== -1) {
                        bindSetupCustomerTabsSubmitHandler('#customerDetail');
                        bindTimePicker();
                        bindYesNo();
                        bindAccordian();
                    }
                    else if (actionLink.value.indexOf("/SetupCustomer/PatchingPolicy") !== -1) {
                        bindSetupCustomerTabsSubmitHandler('#customerDetail');
                        bindTimePicker();
                        bindAccordian();
                    }
                    else {
                        bindSetupCustomerTabsSubmitHandler('#customerDetail');
                        bindTimePicker();
                    }

                });

                request.fail(function (jqXHR, textStatus) {
                    handleAjaxError(jqXHR, textStatus);
                });
            }
        });
    };

    var bindTimePicker = function () {
        $('.jquery_ptTimeSelect').ptTimeSelect({
            containerClass: "timeCntr",
            containerWidth: "265px",
            setButtonLabel: "Select",
            showminutes: "false",
            minutesLabel: "min",
            hoursLabel: "Hrs",
            zIndex: 9999,
            onBeforeShow: undefined,
            onClose: undefined,

        });
    };

    var bindAccordian = function () {
        $('.panel-heading').on('click', function (e) {
            if ($(this).parents('.panel').children('.panel-collapse').hasClass('in')) {
                e.stopPropagation();
            }
        });
    };

    var bindYesNo = function () {
        $('select[id=RemoteControlPermissionWR_ID]').on("change", function (e) {
            var selValue = $(this).val();
            debugger;

            if (selValue == 2) {
                $(".workstation").attr('disabled', true);
                $(".workstation").val(" ");
            }
            else {
                $(".workstation").attr('disabled', false);
            }
            e.preventDefault();
        })

        $('select[id=RemoteControlPermissionSR_ID]').on("change", function (e) {
            var selValue = $(this).val();
            debugger;

            if (selValue == 2) {
                $(".servers").attr('disabled', true);
                $(".servers").val(" ");
            }
            else {
                $(".servers").attr('disabled', false);
            }
            e.preventDefault();
        })
        $('select[id=RemoteControlPermissionND]').on("change", function (e) {
            var selValue = $(this).val();
            debugger;

            if (selValue == 2) {
                $(".network").attr('disabled', true);
                $(".network").val(" ");
            }
            else {
                $(".network").attr('disabled', false);
            }
            e.preventDefault();
        })

    };

    var bindCheckBoxClick = function () {
        $('#toolInfoChkbox1').change(function () {
            debugger;
            if ($("#toolInfoChkbox1").is(':checked')) {
                $('#RMMToolPassword').get(0).type = 'text';
            }
            else {
                $('#RMMToolPassword').get(0).type = 'Password';
            }
        });
        $('#toolInfoChkbox2').change(function () {
            debugger;
            if ($("#toolInfoChkbox2").is(':checked')) {
                $('#PSAToolPassword').get(0).type = 'text';
            }
            else {
                $('#PSAToolPassword').get(0).type = 'Password';
            }
        });
        $('#backUpChkbox').change(function () {
            debugger;
            if ($("#backUpChkbox").is(':checked')) {
                $('#Password').get(0).type = 'text';
            }
            else {
                $('#Password').get(0).type = 'Password';
            }
        });
    }

    var bindCheckBoxBackup = function () {
        $('#backUpChkbox').change(function () {
            debugger;
            if ($("#backUpChkbox").is(':checked')) {
                $('#Password').get(0).type = 'text';
            }
            else {
                $('#Password').get(0).type = 'Password';
            }
        });
    }

    var bindSetupCustomerTabsSubmitHandler = function (formTab) {
        //$('#customerDetail').find('form').submit(function (e) {
        $(formTab).find('form').submit(function (e) {
            var $form = $(this);
            // We check if jQuery.validator exists on the form
            if (!$form.valid || $form.valid()) {
                $.post($form.attr('action'), $form.serializeArray())
                    .done(function (json) {
                        json = json || {};
                        if (json.success) {
                            switch (json.Tab) {
                                // VIPs are awesome. Give them 50% off.
                                case 'CI': // Customer Information Tab                                
                                    var TITab = $(".tabbable").find('[data-link="/SetupCustomer/ToolInformation"]');
                                    TITab.click();
                                    break;
                                case 'TI': // Tool Information Tab
                                    debugger;
                                    var APTab = $(".tabbable").find('[data-link="/SetupCustomer/AccessPolicy"]');
                                    APTab.click();

                                    break;
                                case 'AP': // Access Policy Tab
                                    var mpTab = $(".tabbable").find('[data-link="/SetupCustomer/MaintenancePolicy"]');
                                    mpTab.click();
                                    break;
                                case 'MP': // Maintenance Policy Tab
                                    $(".modal-popup").dialog("close");
                                    //reload the grid again.  
                                    oTable.fnReloadAjax();
                                    break;
                                case 'PP': // Patching Policy Tab
                                    var avpTab = $(".tabbable").find('[data-link="/SetupCustomer/AntiVirusPolicy"]');
                                    avpTab.click();
                                    break;
                                case 'VP': // Anti-Virus Policy Tab
                                    $(".modal-popup").dialog("close");
                                    //reload the grid again.  
                                    oTable.fnReloadAjax();
                                    break;
                                case 'BP': // Backup Policy Tab
                                    $(".modal-popup").dialog("close");
                                    //reload the grid again.  
                                    oTable.fnReloadAjax();
                                    break;
                            }


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
        });
    }

    //back-up popup handle//
    var backupPolicyModalPopUp = function (link) {
        var linkUrl = link.attr('href');
        var bpCustomerID = 0;

        var setupCustomerLnk = link.attr('data-id');
        if (setupCustomerLnk == "undefined")
            bpCustomerID = 0;
        else
            bpCustomerID = setupCustomerLnk;

        var modalTitle = "Add Back-Up Policy";
        if (bpCustomerID > 0)
            modalTitle = "Update Back-Up Policy"

        if (typeof customerName != "undefined")
            modalTitle = modalTitle + ' - ' + customerName;

        var request = $.ajax({
            type: "GET",
            contentType: "application/x-www-form-urlencoded; charset=UTF-8",
            url: linkUrl,
            dataType: "html",
            data: { "BackUpPolicyID": bpCustomerID }
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
                    height: 650,
                    width: link.data('dialog-width') || 637,
                    close: function (event, ui) {
                        $(this).dialog('destroy').remove();

                    }
                }).find('form') // Attach logic on forms
                        .submit(bindSetupCustomerTabsSubmitHandler('#dvBPPolicy'))
                        .end();

            $fx.setUnobstrusiveControls("#dvBPPolicy");
            bindCheckBoxBackup();
            bindTimePicker();
            contactDialog.dialog('open');
        });

        request.fail(function (jqXHR, textStatus) {
            handleAjaxError(jqXHR, textStatus);
        });
    };

    var bindBackUpPolicyControls = function () {
        $('#add_backuppolicy').on("click", function (e) {
            debugger;
            var link = $(this);
            backupPolicyModalPopUp(link);


            // Prevent the normal behavior since we opened the dialog
            e.preventDefault();
        });
    }

    var bindBPEditControl = function () {
        $('[data-link-edit="Edit"]').on("click", function (e) {
            var link = $(this);
            backupPolicyModalPopUp(link);

            // Prevent the normal behavior since we opened the dialog
            e.preventDefault();
        });
        $('[data-link-edit="Delete"]').on("click", deletePolicy);
    };

    var rebindBPPolicyGrid = function () {
        var backupPolicyLink = $('#backupPolicy');

        var request = $.ajax({
            type: "GET",
            contentType: "application/x-www-form-urlencoded; charset=UTF-8",
            url: backupPolicyLink.attr('data-link'),
            dataType: "html"
        });

        request.done(function (data) {
            $('#ContactsGrid').html(data);

            bindBackUpPolicyControls();
        });

        request.fail(function (jqXHR, textStatus) {
            handleAjaxError(jqXHR, textStatus);
        });
    };

    var bindBackupPolicyTable = function () {
        oTable = $('#BPTable').dataTable({
            "bServerSide": true,
            "sAjaxSource": "/SetupCustomer/BackUpPolicies",
            "bProcessing": true,
            "sPaginationType": "full_numbers",
            "bPaginate": true,
            "bJQueryUI": true,
            "bProcessing": false,
            "bAutoWidth": false,
            "oLanguage": {
                "sSearch": "Search Policy Name: "
            },
            "aoColumns": [
                { "sName": "PolicyName", sWidth: '10%', "sClass": "wrapWord changFont" },
                { "sName": "ProductName", sWidth: '10%', "sClass": "wrapWord changFont" },
                { "sName": "VolumeLocation", sWidth: '10%', "sClass": "wrapWord changFont" },
                { "sName": "FolderLocation", sWidth: '10%', "sClass": "wrapWord changFont" },
                { "sName": "BackUpSchedule", sWidth: '10%', "sClass": "wrapWord changFont" },
                { "sName": "BackUpSetDetails", sWidth: '10%', "sClass": "wrapWord changFont" },
                { "sName": "DifferentialEveryDay", sWidth: '10%', "sClass": "wrapWord changFont" },
                { "sName": "PreviousBackupSaved", sWidth: '10%', "sClass": "wrapWord changFont" },
                { "sName": "FullBackUpEveryDay", sWidth: '10%', "sClass": "wrapWord changFont" },
                { "sName": "BackUpPolicyID", "sWidth": '7%', fnRender: editBPLink, "bSortable": false, "bSearchable": false, "sClass": "wrapWord changFont" }
            ],
            "fnDrawCallback": function (oSettings) {
                bindBPEditControl();
            }
            ,
            "fnInitComplete": function () {
                $('#BPTable_wrapper').addClass('wrapperDiv');
            }
        });

        $('.dataTables_filter input')
        .unbind('keypress keyup')
        .bind('keypress keyup', function (e) {
            if ($(this).val().length < 3 && e.keyCode != 13) return;
            oTable.fnFilter($(this).val());
        });
    };

    var editBPLink = function (oObj) {
        var id = oObj.aData[9];
        var editDelete = "<img class='icon' src='/Images/edit.png' data-id='" + id + "' title='Edit Backup Policy Information' data-link-edit='Edit' href='/SetupCustomer/BackUpPolicy/'>";
        editDelete = editDelete + "<img class='icon' src='/Images/delete.png' data-id='" + id + "'  title='Delete Backup Policy' data-link-edit='Delete' data-policyType='Backup' href='/SetupCustomer/DeletePolicy/'>";
        return editDelete
    }
    //End Pop up handle

    //antivirus popup handle
    var bindAntiVirusPolicyControls = function () {
        $('#add_antiviruspolicy').on("click", function (e) {
            debugger;
            var link = $(this);
            antiVirusPolicyModalPopUp(link);

            // Prevent the normal behavior since we opened the dialog
            e.preventDefault();
        });

    }

    var bindAVPEditControl = function () {
        $('[data-link-edit="Edit"]').on("click", function (e) {
            var link = $(this);
            antiVirusPolicyModalPopUp(link)

            // Prevent the normal behavior since we opened the dialog
            e.preventDefault();
        });
        $('[data-link-edit="Delete"]').on("click", deletePolicy);

    };

    var antiVirusPolicyModalPopUp = function (link) {
        var linkUrl = link.attr('href');
        var avCustomerID = 0;

        var setupCustomerLnk = link.attr('data-id');
        if (setupCustomerLnk == "undefined")
            bpCustomerID = 0;
        else
            avCustomerID = setupCustomerLnk;

        var modalTitle = "Add Anti-virus Policy";
        if (avCustomerID > 0)
            modalTitle = "Update Anti-virus Policy"

        if (typeof customerName != "undefined")
            modalTitle = modalTitle + ' - ' + customerName;

        var request = $.ajax({
            type: "GET",
            contentType: "application/x-www-form-urlencoded; charset=UTF-8",
            url: linkUrl,
            dataType: "html",

            data: { "AntiVirusID": avCustomerID }
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
                    //height: 'auto',
                    height: 525,
                    width: link.data('dialog-width') || 637,
                    close: function (event, ui) {
                        $(this).dialog('destroy').remove();
                    }
                }).find('form') // Attach logic on forms
                        .submit(bindSetupCustomerTabsSubmitHandler('#dvAVPolicy'))
                        .end();

            $fx.setUnobstrusiveControls("#dvAVPolicy");
            bindTimePicker();
            contactDialog.dialog('open');
        });

        request.fail(function (jqXHR, textStatus) {
            handleAjaxError(jqXHR, textStatus);
        });
    };

    var rebindAVPolicyGrid = function () {
        var atntiVirusPolicyLink = $('#antivirusPolicy');
        var request = $.ajax({
            type: "GET",
            contentType: "application/x-www-form-urlencoded; charset=UTF-8",
            url: atntiVirusPolicyLink.attr('data-link'),
            dataType: "html"
        });

        request.done(function (data) {
            $('#ContactsGrid').html(data);

            bindAntiVirusPolicyControls();
        });

        request.fail(function (jqXHR, textStatus) {
            handleAjaxError(jqXHR, textStatus);
        });
    };

    var bindAVPolicyTable = function () {
        oTable = $('#AVPTable').dataTable({
            "bServerSide": true,
            "sAjaxSource": "/SetupCustomer/AntiVirusPolicies",
            "bProcessing": true,
            "sPaginationType": "full_numbers",
            "bPaginate": true,
            "bJQueryUI": true,
            "bProcessing": false,
            "bAutoWidth": false,
            "oLanguage": {
                "sSearch": "Search Policy Name: "
            },
            "aoColumns": [
                { "sName": "PolicyName", sWidth: '9%', "sClass": "wrapWord changFont" },
                { "sName": "ProductName", sWidth: '9%', "sClass": "wrapWord changFont" },
                { "sName": "PatchingTime", sWidth: '9%', "sClass": "wrapWord changFont" },
                { "sName": "WeekOfDaysName", sWidth: '9%', "sClass": "wrapWord changFont" },
                { "sName": "MonthOfDaysName", sWidth: '9%', "sClass": "wrapWord changFont" },
                { "sName": "ExcludedFilesExtension", sWidth: '9%', "sClass": "wrapWord changFont" },
                { "sName": "ExcludedFileTypes", sWidth: '9%', "sClass": "wrapWord changFont" },
                { "sName": "ExcludedFilePaths", sWidth: '9%', "sClass": "wrapWord changFont" },
                { "sName": "AntiVirusID", "sWidth": '7%', fnRender: editAVLink, "bSortable": false, "bSearchable": false, "sClass": "wrapWord changFont" }
            ],
            "fnDrawCallback": function (oSettings) {
                bindAVPEditControl();
            },
            "fnInitComplete": function () {
                $('#AVPTable_wrapper').addClass('wrapperDiv');
            }
        });

        $('.dataTables_filter input')
        .unbind('keypress keyup')
        .bind('keypress keyup', function (e) {
            if ($(this).val().length < 3 && e.keyCode != 13) return;
            oTable.fnFilter($(this).val());
        });
    };

    var editAVLink = function (oObj) {
        var id = oObj.aData[8];
        var editDelete = "<img class='icon' src='/Images/edit.png' data-id='" + id + "' title='Edit Anti-Virus Policy Information' data-link-edit='Edit' href='/SetupCustomer/AntiVirusPolicy/'>";
        editDelete = editDelete + "<img class='icon' src='/Images/delete.png' data-id='" + id + "'  title='Delete Anti-Virus Policy' data-link-edit='Delete' data-policyType='AntiVirus'  href='/SetupCustomer/DeletePolicy/'>";
        return editDelete
    }

    //maintenance popup handle
    var bindMaintenancePolicyControls = function () {
        $('#add_maintenancepolicy').on("click", function (e) {
            debugger;
            var link = $(this);
            maintenancePolicyModalPopUp(link);

            // Prevent the normal behavior since we opened the dialog
            e.preventDefault();
        });

    }

    var bindMPEditControl = function () {
        $('[data-link-edit="Edit"]').on("click", function (e) {
            debugger;
            var link = $(this);
            maintenancePolicyModalPopUp(link)

            // Prevent the normal behavior since we opened the dialog
            e.preventDefault();
        });
        $('[data-link-edit="Delete"]').on("click", deletePolicy);

    };

    var maintenancePolicyModalPopUp = function (link) {
        var linkUrl = link.attr('href');
        var mpCustomerID = 0;

        var setupCustomerLnk = link.attr('data-id');
        if (setupCustomerLnk == "undefined")
            mpCustomerID = 0;
        else
            mpCustomerID = setupCustomerLnk;

        var modalTitle = "Add Manintenance Policy";
        if (mpCustomerID > 0)
            modalTitle = "Update Manintenance Policy";

        if (typeof customerName != "undefined")
            modalTitle = modalTitle + ' - ' + customerName;

        var request = $.ajax({
            type: "GET",
            contentType: "application/x-www-form-urlencoded; charset=UTF-8",
            url: linkUrl,
            dataType: "html",
            data: { "MaintenancePolicyID": mpCustomerID }
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
                    height: 'auto',
                    width: link.data('dialog-width') || 637,
                    close: function (event, ui) {
                        $(this).dialog('destroy').remove();
                    }
                }).find('form') // Attach logic on forms
                        .submit(bindSetupCustomerTabsSubmitHandler('#dvMPPolicy'))
                        .end();

            $('#ScheduledStartDate').datepicker({
                format: "mm/dd/yyyy"
            });


            weekdayStartdate();
            daydate();
            disableWeekDay();

            $fx.setUnobstrusiveControls("#dvMPPolicy");
            bindTimePicker();
            contactDialog.dialog('open');
        });

        request.fail(function (jqXHR, textStatus) {
            handleAjaxError(jqXHR, textStatus);
        });
    };

    var rebindMPPolicyGrid = function () {
        var maintenancePolicyLink = $('#maintinancePolicy');

        var request = $.ajax({
            type: "GET",
            contentType: "application/x-www-form-urlencoded; charset=UTF-8",
            url: maintenancePolicyLink.attr('data-link'),
            dataType: "html"
        });

        request.done(function (data) {
            $('#ContactsGrid').html(data);

            bindMaintenancePolicyControls();
        });

        request.fail(function (jqXHR, textStatus) {
            handleAjaxError(jqXHR, textStatus);
        });
    };

    var bindMaintenanceTable = function () {
        oTable = $('#MPTable').dataTable({
            "bServerSide": true,
            "sAjaxSource": "/SetupCustomer/MaintenancePolicies",
            "bProcessing": true,
            "sPaginationType": "full_numbers",
            "bPaginate": true,
            "bJQueryUI": true,
            "bProcessing": false,
            "bAutoWidth": false,
            "oLanguage": {
                "sSearch": "Search Policy Name: "
            },
            "aoColumns": [
                { "sName": "ActivityName", sWidth: '14.2%', "sClass": "wrapWord changFont" },
                { "sName": "ScheduleTypeName", sWidth: '14.2%', "sClass": "wrapWord changFont" },
                { "sName": "WeekOfDaysName", sWidth: '14.2%', "sClass": "wrapWord changFont" },
               // { "sName": "MonthOfDaysName", sWidth: '14.2%', "sClass": "wrapWord changFont" },
                { "sName": "ScheduleDetail", sWidth: '14.2%', "sClass": "wrapWord changFont" },
                { "sName": "ScheduledStartDate", sWidth: '14.2%', "sClass": "wrapWord changFont" },
                { "sName": "MaintenancePolicyID", "sWidth": '7%', fnRender: editMPLink, "bSortable": false, "bSearchable": false, "sClass": "wrapWord changFont" }
            ],
            "fnDrawCallback": function (oSettings) {
                bindMPEditControl();
            },
            "fnInitComplete": function () {
                $('#MPTable_wrapper').addClass('wrapperDiv');
            }
        });

        $('.dataTables_filter input')
        .unbind('keypress keyup')
        .bind('keypress keyup', function (e) {
            if ($(this).val().length < 3 && e.keyCode != 13) return;
            oTable.fnFilter($(this).val());
        });
    };
    debugger;
    var editMPLink = function make_delete_link(oObj) {
        var id = oObj.aData[5];
        var editDelete = "<img class='icon' src='/Images/edit.png' data-id='" + id + "' title='Edit Maintenance Policy Information' data-link-edit='Edit' href='/SetupCustomer/MaintenancePolicy/'>";
        editDelete = editDelete + "<img class='icon' src='/Images/delete.png' data-id='" + id + "' title='Delete Maintenance Policy' data-policyType='Maintenance' data-link-edit='Delete' href='/SetupCustomer/DeletePolicy/'>";
        return editDelete
    };

    var deletePolicy = function (e) {
        var link = $(this);
        bootbox.confirm("Do you really want to delete policy information?", function (result) {
            if (result == true) {
                var request = $.ajax({
                    type: "GET",
                    contentType: "application/json",
                    url: link.attr('href'),
                    dataType: "json",
                    data: { "PolicyID": link.attr('data-id'), "PolicyType": link.attr('data-policyType') }
                });
                request.done(function (json) {
                    debugger;
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
                    debugger;
                    $fx.handleAjaxError(jqXHR, textStatus);
                });
            }

        })
        e.preventDefault();
    };

    var weekdayStartdate = function () {
        $('#ScheduledStartDate').on("change", function (e) {
            var d = $('#ScheduledStartDate').val();
            var da = new Date(Date.parse(d));
            days = da.getDay() + 1;
            var s = $('#ScheduleType_ID').val();
            debugger;
            if (s != 1) {
                if (s != 2) {
                    $('#WeekOfDays_ID').val(days);
                }
            }
        });
    };
    var daydate = function () {
        $('#WeekOfDays_ID').on("change", function () {
            debugger;
            var d = new Date();
            var present = d.getDay();
            var d = $('#ScheduledStartDate').val();
            var count = $('#WeekOfDays_ID').val();

            if (count == 1) { var seed = 'sunday'; }
            else if (count == 2) { var seed = 'monday'; }
            else if (count == 3) { var seed = 'tuesday'; }
            else if (count == 4) { var seed = 'wednesday'; }
            else if (count == 5) { var seed = 'thursday'; }
            else if (count == 6) { var seed = 'friday'; }
            else if (count == 7) { var seed = 'saturday'; }

            var days = ['sunday', 'monday', 'tuesday', 'wednesday', 'thursday', 'friday', 'saturday'];

            function next(day) {

                var today = new Date();
                var today_day = today.getDay();

                day = day.toLowerCase();

                for (var i = 7; i--;) {
                    if (day === days[i]) {
                        day = (i <= today_day) ? (i + 7) : i;
                        break;
                    }
                }

                var daysUntilNext = day - today_day;

                return new Date().setDate(today.getDate() + daysUntilNext);
            }

            var p = new Date(next(seed));
            var future = p.getDay();
            if (present != future) {
                var date = new Date(next(seed));
            }
            else { var date = new Date(); }

            var futDate = date.getMonth() + 1 + "/" + date.getDate() + "/" + date.getFullYear();
            var d = $('#ScheduleType_ID').val();
            if (d != 2) {
                $('#ScheduledStartDate').val(futDate);
            }

        });
    };
    var disableWeekDay = function () {
      
        var d = $('#ScheduleType_ID').val();
       

        if (d == 1) {
            $("#WeekOfDays_ID").attr('disabled', true);
        }
               

        $('#ScheduleType_ID').change(function () {
            debugger;
            if ($('#ScheduleType_ID').val() == 1) {
                $("#WeekOfDays_ID").attr('disabled', true);

            }
            else { $("#WeekOfDays_ID").attr('disabled', false); }

            if ($('#ScheduleType_ID').val() == 3) {
                $("#WeekOfMonth").attr('disabled', false);
            }
            else { $("#WeekOfMonth").attr('disabled', true); } 



            $('#ScheduledStartDate').attr('disabled', false);

        });

    };
    // public API
    return {
        bindTabClick: bindTabClick,
        bindBackUpPolicyControls: bindBackUpPolicyControls,
        bindAntiVirusPolicyControls: bindAntiVirusPolicyControls,
        bindMaintenancePolicyControls: bindMaintenancePolicyControls,
        bindSetupCustomerTabsSubmitHandler: bindSetupCustomerTabsSubmitHandler
    };
})();

$(document).ready(function () {
    fxSC.bindTabClick();
    fxSC.bindSetupCustomerTabsSubmitHandler('#customerDetail');

    window.$fxSC = fxSC;
});

