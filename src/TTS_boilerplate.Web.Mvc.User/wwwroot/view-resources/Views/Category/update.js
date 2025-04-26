(function ($) {
    console.log('da vao update.js');
    var _$modalUpdate = $('#CategoryUpdateModal'),
        _$form = _$modalUpdate.find('form');
    _categoryService = abp.services.app.category;

    console.log('sau khai bao bien');

    function save() {
        if (!_$form.valid()) {
            return;
        }
        var category = _$form.serializeFormToObject();

        abp.ui.setBusy(_$form);
        _categoryService.updatecategory(category)
            .done(function () {
                _$modalUpdate.modal('hide');
                abp.notify.info('Cập nhật thành công!');
                abp.event.trigger('category.updateModalSaved', category);
            }).always(function () {
                abp.ui.clearBusy(_$form);
            });
    }


    _$form.closest('div.modal-content').find(".save-button").click(function (e) {
        e.preventDefault();
        save();
    });

    _$form.find('input').on('keypress', function (e) {
        if (e.which == 13) {
            e.preventDefault();
            save();
        }
    })


    _$modalUpdate.on('shown.bs.modal', function () {
        _$form.find('input[type=text]:first').focus();
    })

    _$formCreate.validate({
      
        rules: {
            NameCategory: {
                required: true,
                maxlength: 64
            }
        },
        messages: {
            NameCategory: {
                required: "Vui lòng nhập tên loại!",
                maxlength: "Tên loại không quá 64 ký tự"
            }
        }
    })
})(jQuery);