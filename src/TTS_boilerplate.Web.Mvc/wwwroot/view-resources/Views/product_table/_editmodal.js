(function ($) {
    var _$modal = $('#ProductEditModal'),
        _$form = _$modal.find('form');
    _productService = abp.services.app.product_table,
        l = abp.localization.getSource('TTS_boilerplate');

    console.log('da vao editproduct');
    console.log('edit_1', _productService)

    // Lấy thông tin culture hiện tại
    var currentCulture = abp.localization.currentCulture;
    console.log('-- Current culture name:', currentCulture.name); // Ví dụ: "vi-VN"
    console.log('-- Display name:', currentCulture.displayName); // Ví dụ: "Vietnamese"
    
    function save() {
        if (!_$form.valid()) {
            return;
        }

        //var product = _$form.serializeFormToObject();
        var formData = new FormData(_$form[0]);

        var cateId = $('select[name="CategoryIdss"]').val();
        //var cateId = parseInt($('select[name="CategoryIdss"]').val());
     
        console.log('-- select category _begin_2:', $('#categorySelect').val());

        console.warn('-- cateId', cateId)
        
            var cateId = parseInt(cateId)
            var selectCategory = parseInt($('#categorySelect').val());
            console.log('--Selected category_update: ', selectCategory);
            formData.append('CategoryId', cateId);
        
        //----
        console.log('formData to send:');
        for (var pair of formData.entries()) {
            console.log(pair[0] + ': ' + pair[1]);
        } 

        //console.log('Id product', productId);

        abp.ui.setBusy(_$modal);
        $.ajax({
            url: abp.appPath + 'Products_table/Update',
            type: 'POST',
            data: formData,
            contentType: false, // Không set contentType để FormData tự xử lý
            processData: false, // Không xử lý dữ liệu để gửi file đúng cách
            success: function (result) {
                console.log('success', result);
                _$modal.modal('hide');
                _$form[0].reset();
                abp.notify.info(l('Cập nhật thành công!'));
                abp.event.trigger('product.edited', formData);

            },
            error: function (e) {
                console.error('Error details:', e);
                console.log('Status:', e.status);
                console.log('Response:', e.responseText);
                abp.notify.error(e.responseJSON && e.responseJSON.message ? e.responseJSON.message : 'An error occurred');

            },
            complete: function () {
                abp.ui.clearBusy(_$modal);
            }
        });
        //---
    }

    $('#price').on('input', function (p) {
        console.log('-- input price', p);
        console.log('-- price value', $(this).val());
    });

    _$form.closest('div.modal-content').find(".save-button").click(function (e) {
        e.preventDefault();
        save();
    });

    _$form.find('input').on('keypress', function (e) {
        if (e.which == 13) {
            e.preventDefault();
            save();
        }
    });
    _$modal.on('shown.bs.modal', function () {
        _$form.find('input[type=text]:first').focus();
    })


    $('#ProductEditForm #imgProduct').on('change', function (event) {
        var reader = new FileReader();

        reader.onload = function (e) {
            $('#ProductEditForm #imagePreview').attr('src', e.target.result).show();
        };

        reader.readAsDataURL(this.files[0]);
    });

    // Reset preview ảnh khi đóng modal create
    $('#ProductEditForm').on('hidden.bs.modal', function () {
        $('#ProductEditForm #imagePreview').attr('src', '#').hide();
        $('#ProductEditForm #imgProduct').val('');
    });


    _$form.validate({
        rules: {
            NameProduct: {
                required: true,
                maxlength: 64
            },
            Price: {
                required: true,
                normalizer: function (value) {
                        if (currentCulture.name === 'vi') {
                            console.log('-- price vn', value.toString())
                            console.log('-- price vn', value)

                            if (value.includes('.')) {
                                // Lưu vị trí con trỏ
                                var cursorPos = this.selectionStart;
                                var dotCount = (value.substring(0, cursorPos).match(/\./g) || []).length;

                                // Thay thế dấu chấm bằng dấu phẩy
                                var newValue = value.replace(/\./g, ',');
                                $(this).val(newValue);

                                // Đặt lại vị trí con trỏ
                                var newCursorPos = cursorPos + (newValue.length - value.length);
                                this.setSelectionRange(newCursorPos, newCursorPos);
                            }

                            // Xử lý định dạng theo tiếng Việt
                            return value.toString().replace(',', '.');
                        } else if (currentCulture.name === 'en') {
                            console.log('-- price en', value.toString())
                            // Xử lý định dạng theo English
                            return value.toString();
                        }
                },
                number: true,
                min: 0.00000000001
                
            },
            DecriptionProduct: {
                required: true,
                maxlength: 256
            },
            CreationDate: {
                required: true,

            },
            ExpirationDate: {
                required: true,

            },
            CategoryIdss: {
                required: true
            }
        },
        messages: {
            NameProduct: {
                required: l("NameProduct_Required"),
                maxlength: l("NameProduct_MaxLength")
            },
            Price: {
                required: l("Price_Required"),
                number: l("Price_Number"),
                min: l("Price_Min")
            },
            DecriptionProduct: {
                required: l("DecriptionProduct_Required"),
                maxlength: l("DecriptionProduct_MaxLength")
            },
            CreationDate: {
                required: l("CreationDate_Required")
            },
            ExpirationDate: {
                required: l("ExpirationDate_Required")
            },
            CategoryIdss: {
                required: l("NameCategory_Required")
            }

        }
    })
})(jQuery);
