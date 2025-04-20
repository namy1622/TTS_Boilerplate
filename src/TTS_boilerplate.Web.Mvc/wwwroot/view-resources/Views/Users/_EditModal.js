(function ($) {
    var _userService = abp.services.app.user,
        l = abp.localization.getSource('TTS_boilerplate'),
        _$modal = $('#UserEditModal'),
        _$form = _$modal.find('form');

    console.log('da vao editmodal.js');
    function save() {
        if (!_$form.valid()) {
            return;
        }

        var user = _$form.serializeFormToObject();
        user.roleNames = [];
        var _$roleCheckboxes = _$form[0].querySelectorAll("input[name='role']:checked");
        if (_$roleCheckboxes) {
            for (var roleIndex = 0; roleIndex < _$roleCheckboxes.length; roleIndex++) {
                var _$roleCheckbox = $(_$roleCheckboxes[roleIndex]);
                user.roleNames.push(_$roleCheckbox.val());
            }
        }

        abp.ui.setBusy(_$form);
        _userService.update(user).done(function () {
            _$modal.modal('hide');
            abp.notify.info(l('SavedSuccessfully'));
            abp.event.trigger('user.edited', user);
        }).always(function () {
            abp.ui.clearBusy(_$form);
        });
    }

    _$form.closest('div.modal-content').find(".save-button").click(function (e) {
        e.preventDefault();
        save();
    });

    _$form.find('input').on('keypress', function (e) {
        if (e.which === 13) {
            e.preventDefault();
            save();
        }
    });

    _$modal.on('shown.bs.modal', function () {
        _$form.find('input[type=text]:first').focus();
    });


    $.validator.addMethod("dateGreaterThan", function (value, element, params) {
        var creationDate = $(params).val(); // Lấy giá trị của CreationDate
        if (!value || !creationDate) {
            return true; // Nếu một trong hai trường rỗng, để quy tắc required xử lý
        }
        var creationDateObj = new Date(creationDate + "T00:00:00");
        var expirationDateObj = new Date(value + "T00:00:00");
        return expirationDateObj > creationDateObj;
    }, "Ngày hết hạn phải lớn hơn ngày tạo!");


    _$form.validate({
        rules: {
            NameProduct: {
                required: true,
                maxlength: 64,
                minlength: 2
            },
            Price: {
                required: true,
                number: true
            },
            DescriptionProduct: {
                required: true,
                maxlength: 256
            },
            CreationDate: {
                required: true,

            },
            ExpirationDate: {
                required: true,

                dateGreaterThan: "#creationDate_1"
            },
            NameCategory: {
                required: true
            },
            //ProductImagePath: {
            //    required: true
            //}
        },
        messages: {
            NameProduct: {
                required: "Bạn phải nhập tên sản phẩm!",
                maxlength: "Tên sản phẩm không quá 64 ký tự!",
                minlegth: "Tên sản phẩm phải lớn hơn {0} ký tự!"
            },
            Price: {
                required: "Bạn phải nhập giá của sản phẩm!",
                number: "Giá sản phẩm phải là số!"
            },
            DescriptionProduct: {
                required: "Hãy nhập mô tả của sản phẩm!",
                maxlength: "Mô tả không quá 256 kí tự!"
            },
            CreationDate: {
                required: "Bạn hãy nhập ngày sản xuất!",

            },
            ExpirationDate: {
                required: "Bạn hãy nhập ngày hết hạn!",

            },
            NameCategory: {
                required: "Hãy chọn Loại sản phẩm!"
            },
            //ProductImagePath: {
            //    required: "Chọn ảnh điiiiiii!"
            //}
        }
    })
})(jQuery);
