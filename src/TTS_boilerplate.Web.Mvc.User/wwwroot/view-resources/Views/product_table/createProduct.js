//(function ($){
//    _$form.find(".save-button").on('click', (e) => {
//        e.preventDefault();

//        if (!_$form.valid()) {
//            return;
//        }

//        //var formData = _$form.serializeFormToObject();

//        var formData = new FormData(_$form[0]); // taoj form de gui ca file va data text

//        // Log giá trị trước khi gửi để kiểm tra
//        console.log('CreationDate before send:', formData.get("CreationDate"));
//        console.log('ExpirationDate before send:', formData.get("ExpirationDate"));
//        console.log('product.categoryId 1', formData.categoryId)

//        var _$categoryCheckboxes = _$form[0].querySelectorAll("input[name='category']:checked")
//        console.log(' _$categoryCheckboxes', _$categoryCheckboxes);
//        if (_$categoryCheckboxes) {
//            for (var categoryIndex = 0; categoryIndex < _$categoryCheckboxes.length; categoryIndex++) {
//                var _$categoryCheckbox = $(_$categoryCheckboxes[categoryIndex]);

//                var categoryId = parseInt(_$categoryCheckbox.attr("data-category-id"), 10); // Chuyển thành số nguyên

//                //console.log('cate_checkbob', _$categoryCheckbox.attr("data-category-id"))
//                //console.log('cate_checkbob_2', _$categoryCheckbox.val())
//                formData.categoryId = categoryId;
//                //product.categoryId=categoryId;
//                //product.categoryId.push(categoryId);
//                formData.append('CategoryId', categoryId); // Thêm CategoryId vào formData
//                console.log('formData.categoryId ', categoryId)
//            }
//        }

//        console.log('formData to send:');
//        for (var pair of formData.entries()) {
//            console.log(pair[0] + ': ' + pair[1]);
//        }

//        console.log('abp.appPath:', abp.appPath);
//        abp.ui.setBusy(_$modal);
//        $.ajax({

//            url: abp.appPath + 'Products_table/CreateProduct',
//            type: 'POST',
//            data: formData,
//            contentType: false, // Không set contentType để FormData tự xử lý
//            processData: false, // Không xử lý dữ liệu để gửi file đúng cách
//            success: function (result) {
//                console.log('success', result);
//                _$modal.modal('hide');
//                _$form[0].reset();
//                abp.notify.info(l('Tạo mới thành công!'));
//                _$productTable.ajax.reload();
//            },
//            error: function (e) {
//                console.error('Error details:', e);
//                console.log('Status:', e.status);
//                //console.log('Response:', e.responseText);
//                abp.notify.error(e.responseJSON?.message || 'An error occurred');
//            },
//            complete: function () {
//                abp.ui.clearBusy(_$modal);
//            }
//        });


//    });
//}) (jQuery);