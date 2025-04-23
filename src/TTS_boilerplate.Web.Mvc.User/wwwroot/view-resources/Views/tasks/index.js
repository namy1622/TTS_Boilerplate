(function ($) {
    $(function () {
        var _$taskStateConbobox = $('#TaskStateCombobox');

        _$taskStateConbobox.change(function () {
            location.href = '/Tasks?state=' + _$taskStateConbobox.val();
        });
    });
}
)(jQuery);