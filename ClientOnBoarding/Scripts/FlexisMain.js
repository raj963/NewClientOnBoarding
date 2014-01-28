// The module pattern
window.$fx = (function () {
    var bHeight;
    var bWidth;
    var getBrowserHW = function () {
        bWidth = window.innerWidth;
        bHeight = window.innerHeight;
    };

    var getValidationSummaryErrors = function ($form) {
        // We verify if we created it beforehand
        var errorSummary = $form.find('.validation-summary-errors, .validation-summary-valid');
        if (!errorSummary.length) {

            //before prepaend, need to delete earlier div
            $('.pmErrorDetail').remove();
            errorSummary = $('<div class="pmErrorDetail"><ul></ul></div>')
                .prependTo($form);
        }

        return errorSummary;
    };

    var displayErrors = function (form, errors) {
        debugger;
        var validator = $(form).validate();

        var keyValList = new Object();
        for (var i = 0; i < errors.length; i++) {
            if (errors[i].Key != '')
                keyValList[errors[i].Key] = errors[i].Value;
        }

        if (!jQuery.isEmptyObject(keyValList))
            validator.showErrors(keyValList);

        var items = $.map(errors, function (error) {
            var result = ''
            if (error.Key == '') { result = result + '<li>' + error.Value + '</li>' }
            return result;
        }).join('');

        if (items.length > 0) {
            var errorSummary = getValidationSummaryErrors(form)
            .removeClass('validation-summary-valid')

            var ul = errorSummary
                .find('ul')
                .empty()
                .append(items);
        }
    };

    var setUnobstrusiveControls = function (parseFramID) {
        //get the relevant form        
        var form = $(parseFramID).find('form');

        if (form.length > 0) {
            //use the normal unobstrusive.parse method
            $.validator.unobtrusive.parse(parseFramID);

            //get the collections of unobstrusive validators, and jquery validators
            //and compare the two
            var unobtrusiveValidation = form.data('unobtrusiveValidation');
            var validator = form.validate();

            $.each(unobtrusiveValidation.options.rules, function (elname, elrules) {
                if (validator.settings.rules[elname] == undefined) {
                    var args = {};
                    $.extend(args, elrules);
                    args.messages = unobtrusiveValidation.options.messages[elname];
                    //edit:use quoted strings for the name selector
                    $("[name='" + elname + "']").rules("add", args);
                } else {
                    $.each(elrules, function (rulename, data) {
                        if (validator.settings.rules[elname][rulename] == undefined) {
                            var args = {};
                            args[rulename] = data;
                            args.messages = unobtrusiveValidation.options.messages[elname][rulename];
                            //edit:use quoted strings for the name selector
                            $("[name='" + elname + "']").rules("add", args);
                        }
                    });
                }
            });
        }
    }

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

    var addDataTableFunction = function () {

    };

    // public API
    return {
        displayErrors: displayErrors,
        setUnobstrusiveControls: setUnobstrusiveControls,
        handleAjaxError: handleAjaxError,
        addDataTableFunction: addDataTableFunction,
        getBrowserHW: getBrowserHW
    };

})();

$.fn.dataTableExt.oApi.fnReloadAjax = function (oSettings, sNewSource, fnCallback, bStandingRedraw) {
    if (sNewSource !== undefined && sNewSource !== null) {
        oSettings.sAjaxSource = sNewSource;
    }

    // Server-side processing should just call fnDraw
    if (oSettings.oFeatures.bServerSide) {
        this.fnDraw();
        return;
    }

    this.oApi._fnProcessingDisplay(oSettings, true);
    var that = this;
    var iStart = oSettings._iDisplayStart;
    var aData = [];

    this.oApi._fnServerParams(oSettings, aData);

    oSettings.fnServerData.call(oSettings.oInstance, oSettings.sAjaxSource, aData, function (json) {
        /* Clear the old information from the table */
        that.oApi._fnClearTable(oSettings);

        /* Got the data - add it to the table */
        var aData = (oSettings.sAjaxDataProp !== "") ?
            that.oApi._fnGetObjectDataFn(oSettings.sAjaxDataProp)(json) : json;

        for (var i = 0 ; i < aData.length ; i++) {
            that.oApi._fnAddData(oSettings, aData[i]);
        }

        oSettings.aiDisplay = oSettings.aiDisplayMaster.slice();

        that.fnDraw();

        if (bStandingRedraw === true) {
            oSettings._iDisplayStart = iStart;
            that.oApi._fnCalculateEnd(oSettings);
            that.fnDraw(false);
        }

        that.oApi._fnProcessingDisplay(oSettings, false);

        /* Callback user function - for event handlers etc */
        if (typeof fnCallback == 'function' && fnCallback !== null) {
            fnCallback(oSettings);
        }
    }, oSettings);
};

// Using the module pattern for a jQuery feature
$(document).ready(function () {
    $("#loading").bind("ajaxSend", function () {
        $(this).show();
    }).bind("ajaxComplete", function () {
        $(this).hide();
    });

    $(window).load(function () {
        $fx.getBrowserHW();
        //var height = $(this).height() - $header.height() + $footer.height();        
        //$('#topMid').height = window.innerHeight;
        //$('#topMid').width = window.innerWidth;
        $('#topMid').css('min-height', window.innerHeight-50);
    });
});

$(document).ajaxComplete(function (event, xhr, settings) {
    if (xhr.status == 401) {
        // perform a redirect to the login page since we're no longer authorized
        window.location = settings.url;
    }
});
