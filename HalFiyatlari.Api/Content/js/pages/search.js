(function ($) {
    "use strict";

    /* Search - DateTimePicker */
    let dtP_Opts = {
        format: 'L',
        locale: 'tr'
    };

    let iStartDate = $('#iStartDate').datetimepicker(dtP_Opts);
    let iFinishDate = $('#iFinishDate').datetimepicker(dtP_Opts);
})(jQuery);