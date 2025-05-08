(function ($) {
    console.log("index.js has been loaded");


    var _productService = abp.services.app.product_table,
        l = abp.localization.getSource('TTS_boilerplate'),
        _$modal = $('#ProductCreateModal'),
        _$form = _$modal.find('form'),
        _$table = $('#ProductsTable');
    // Lấy thông tin culture hiện tại
    var currentCulture = abp.localization.currentCulture;;

    var _$dropdownFilter = $('.dropdown-item'),
         typeFilter = '',
        filterValue = '',
        startDate = '',
        endDate = '',
        priceFrom = 0,
        priceTo = 0,
        clickFilter = true;




    // Thêm CSS cho các phần filter bị disable
   
    console.log('log 1 ', _productService);
    console.log(abp.services.app);

    var _$productTable = _$table.DataTable({
        paging: true,
        serverSide: true,
        
        // Hiển thị loading khi xử lý
        ordering: true,
        
        listAction: {
            ajaxFunction: _productService.getAllProductCategory,
            inputFilter: function () {
                var _$formObj = $('#ProductsSearchForm').serializeFormToObject(true);
                console.log('-- 1 --', _productService.getAllProductCategory);

                // lấy tt để sapxep
                var draw = _$productTable ? _$productTable.settings()[0].oAjaxData.draw : 1;
                var start = _$productTable ? _$productTable.settings()[0]._iDisplayStart : 0;
                var length = _$productTable ? _$productTable.settings()[0]._iDisplayLength : 10;

                _$formObj.skipCount = start;
                _$formObj.maxResultCount = length;

                // xử lý sắp xếp 
                var order = _$productTable ? _$productTable.order() : '';//[[1, 'asc']];
                console.log('-- order', order);

                if (order && order.length > 0) {
                    var columnIndex = order[0][0];
                    var direction = order[0][1];
                    var columnName = _$productTable ? _$productTable.settings()[0].aoColumns[columnIndex].name : 1;
                    _$formObj.sorting = columnName + ' ' + direction;
                }
                else {
                    _$formObj.sorting = ''; // default
                }
                //--end ---------------
                // xử lý filter ------
                console.log('-- filterValue -- type', filterValue, typeFilter);
                console.log('--startDate_input:', startDate);
                console.log('--endDate_input:', endDate);
                
                _$formObj.startDate = startDate;
                _$formObj.endDate = endDate;
                _$formObj.priceFrom = priceFrom;
                _$formObj.priceTo = priceTo;
                // end xử lý filter---

                _$formObj.maxResultCount = 15;

                console.log('-- Form data:', _$formObj);

                return _$formObj;
            }
        },
        buttons: [
            {
                name: 'refresh',
                text: '<i class=" fas fa-redo-alt"></i>',
                action: function () {
                    priceFrom = 0;
                    priceTo = 0;
                    startDate = '';
                    endDate = '';
                    _$productTable.draw(false)
                }
            }
        ],
        responsive: {
            details: {
                type: 'column'
            }
        },

        columnDefs: [
            {
                targets: 0,
                className: 'control',
                defaultContent: '',
                orderable: false
            },
            {
                targets: 1,
                data: 'nameProduct',
                name: 'NameProduct',
                sortable: true
            },
            {
                targets: 2,
                data: 'descriptionProduct',
                name: 'DesciptionProduct',
                sortable: false
            },
            {
                targets: 3,
                data: 'price',
                name: 'Price',
                sortable: true,
                render: function (data) {
                     //return data + "đ";
                     return Number(data).toLocaleString('vi-VN') + "đ";
                     //return Number(data).toLocaleString('vi-VN') + "đ";
                }
            },
            {
                targets: 4,
                data: 'creationDate',
                name: 'CreationDate',
                sortable: true,
                //render: function (data, type, row) {
                //    return data ? moment(data).format('DD-MM-YYYY') : '';
                //}

                render: function (data, type, row) {
                    return data && moment(data).isValid() ? moment(data).format('YYYY-MM-DD HH:mm') : '';
                }
            },
            {
                targets: 5,
                data: 'expirationDate',
                name: 'ExpirationDate',
                sortable: true,
                render: function (data, type, row) {
                    return data && moment(data).isValid() ? moment(data).format('YYYY-MM-DD HH:mm') : '';
                }
            },

            {
                targets: 6,
                data: 'stock',
                name: 'stock',
                sortable: false,
                render: (data, row, meta) => {
                    return data ? data : 0;
                }
            },
            {
                targets: 7,
                data: 'productImagePath',
                sortable: false,
                render: function (data, type, row, meta) {
                    if (!data) return '';
                    return `<img src="${data}" alt="image" 
                                class=""
                                style="width: 50px; height: 50px; border-radius: 8px; object-fit: cover;"
                            />`;
                }
            },
            {
                targets: 8,
                data: null,
                sortable: false,
                autoWidth: false,
                defaultContent: '',
                render: (data, type, row, meta) => {
                    console.log(row);
                    return [
                        `<div class="d-flex flex-column ml-2">`,

                        abp.auth.hasPermission('Pages.Products_U')
                            ? `<button type="button" style="width:32px; height:28px;" title="Update" class="btn btn-sm bg-secondary edit-product mr-1" data-product-id="${row.id}" data-toggle="modal" data-target="#ProductEditModal">
                                    <i class="fas fa-pencil-alt"></i>
                               </button>`
                            : '',

                        abp.auth.hasPermission('Pages.Products_D')
                            ? `<button type="button" style="width:32px; height:28px;" title="Delete" class="btn btn-sm bg-danger delete-product mt-1" data-product-id="${row.id}" data-product-name="${row.nameProduct}">
                                    <i class="fas fa-trash"></i>
                               </button>`
                            : '',

                        `</div>`
                    ].join('');

                }
            },



        ],
    });

    // Thêm sự kiện tìm kiếm thời gian thực
    $('#searchKeyword').on('input', function () {
        _$productTable.draw(); // Gọi draw() để làm mới bảng với dữ liệu mới
    });

    //-----------------------------------------------
    $(document).on('click', '.edit-product', function (e) {
        var productId = $(this).attr("data-product-id")
        console.log('id', productId)
        e.preventDefault();
        console.log(abp.appPath + 'Products_table/EditProduct?productId=' + productId),
            abp.ajax({
                url: abp.appPath + 'Products_table/EditProduct?productId=' + productId,
                type: 'POST',
                dataType: 'html',
                success: function (content) {
                    if (!content || content.trim() === "") {
                        console.error('Lỗi: Nội dung trả về trống!');
                        return;
                    }
                    //console.log('content', content);
                    $('#ProductEditModal div.modal-content').html(content);
                    console.log('sau html(content)', "");
                    console.log('-- category', $('#categorySelect').val())

                    DaterangerPickerUpdate()
                },
                error: function (e) {
                    console.log('error', e);
                }
            });
    });
    abp.event.on('product.edited', (data) => {
        _$productTable.ajax.reload();
    })
    ////-------------------------------------------------

    ////--------------------------------------------------
    $(document).on('click', '.delete-product', function () {
        var productId = $(this).attr("data-product-id");
        console.log('id_product', productId);

        var productname = $(this).attr("data-product-name");
        console.log('name_product', productname);

        deleteProduct(productId, productname);
    });

    function deleteProduct(productId, productName) {
        abp.message.confirm(
            abp.utils.formatString(
                l('AreYouSureWantToDelete'),
                productName
            ),
            null,
            (isConfirmed) => {
                if (isConfirmed) {
                    _productService.deleteProduct(productId)
                        .done(() => {
                            abp.notify.info(l('SuccessfullyDeleted'));
                            _$productTable.ajax.reload();
                        });
                }
            }
        )
    }

    _$modal.on('shown.bs.modal', function () {
        DaterangerPickerCreate()
    });

    _$form.find(".save-button").on('click', (e) => {
        e.preventDefault();

        if (!_$form.valid()) {
            return;
        }

        //var formData = _$form.serializeFormToObject();

        var formData = new FormData(_$form[0]); // taoj form de gui ca file va data text

        formData.append('Price', parseFloat(formData.Price));

        // Log giá trị trước khi gửi để kiểm tra
        console.log('CreationDate before send:', formData.get("CreationDate"));
        console.log('ExpirationDate before send:', formData.get("ExpirationDate"));
        console.log('product.categoryId ', formData.categoryId)

        //-- append categoryId
        var selectCategory = parseInt($('#categorySelect').val());
        console.log('--Selected category_create: ', selectCategory);
        formData.append('CategoryId', selectCategory);

        console.log('formData to send:');
        for (var pair of formData.entries()) {
            console.log(pair[0] + ': ' + pair[1]);
        }

        console.log('abp.appPath:', abp.appPath);
        abp.ui.setBusy(_$modal);
        $.ajax({

            url: abp.appPath + 'Products_table/CreateProduct',
            type: 'POST',
            data: formData,
            contentType: false, // Không set contentType để FormData tự xử lý
            processData: false, // Không xử lý dữ liệu để gửi file đúng cách
            success: function (result) {
                console.log('success', result);
                _$modal.modal('hide');
                _$form[0].reset();
                abp.notify.info(l('Tạo mới thành công!'));
                _$productTable.ajax.reload();
            },
            error: function (e) {
                console.error('Error details:', e);
                console.log('Status:', e.status);
                abp.notify.error(e.responseJSON.message || 'An error occurred');
            },
            complete: function () {
                abp.ui.clearBusy(_$modal);
            }
        });

        //--------------------------------------------
        //console.log('product to send:', product);
        //if (!formData || Object.keys(formData).length === 0) {
        //    console.error('Product object is empty!');
        //    return;
        //}
        //console.log('product', formData);
        //abp.ui.setBusy(_$modal);

        //_productService.createProduct(formData).done(function () {

        //    _$modal.modal('hide');

        //    _$form[0].reset();

        //    abp.notify.info(l('SavedSuccessfully'));
        //    _$productTable.ajax.reload();
        //}).always(function () {
        //    abp.ui.clearBusy(_$modal);
        //}).fail(function(e){
        //    console.log('catch',e)
        //});
    });
    ////----------------------------------------------------

    _$modal.on('shown.bs.modal', () => {
        console.log('focus input form');
        _$modal.find('input:not([type=hidden]):first').focus();
    }).on('hidden.bs.modal', () => {
        _$form.clearForm();
    });

    $('input[name="category"]').on('change', function () {
        // Lấy danh sách các category đã chọn
        var selectedCategories = [];

        $('input[name="category"]:checked').each(function () {
            selectedCategories.push($(this).val());
        });

        // Cập nhật nội dung label theo tên đã chọn (có thể chọn nhiều)
        $('#nameCategoryLabel').text(selectedCategories.join(', '));
    });

    //---------------------------------------
    $('.btn-search').on('click', (e) => {
        console.log('search click', e)
        _$productTable.ajax.reload();
    });
    $('.txt-search').on('keypress', (e) => {
        if (e.which == 13) {
            console.log('search keypress', e)
            _$productTable.ajax.reload();
            console.log('end search keypress', e)
            return false;
        }
    });
    $(document).on('click', '#btn_filter', function () {
        console.log('btn_filter clicked, triggering btn-search');
        $('.btn-search').trigger('click');
    });

    $('#btn_filter_drop').on('click', function () {
        console.log('-- da bam btn_filter_drop')
        _$productTable.ajax.reload();
    })

    //---------------------------------------


    // Preview ảnh khi chọn ảnh trong createModal
    $('#ProductCreateModal #imgProduct').on('change', function (event) {
        var file_check = this.files[0];
        if (!file_check) return;
        var reader = new FileReader();

        reader.onload = function (e) {
            $('#ProductCreateModal #imagePreview').attr('src', e.target.result).show();
        };

        reader.readAsDataURL(this.files[0]);
    });

    // Reset preview ảnh khi đóng modal create
    $('#ProductCreateModal').on('hidden.bs.modal', function () {
        $('#ProductCreateModal #imagePreview').attr('src', '#').hide();
        $('#ProductCreateModal #imgProduct').val('');
    });

    //----------------------------
    function DaterangerPickerUpdate() {
        $("#creationDate_1").daterangepicker({
            timePicker: true,
            singleDatePicker: true,
            showDropdowns: true,
            locale: {
                format: 'YYYY-MM-DD HH:mm', // Định dạng ngày khớp với giá trị từ server
                applyLabel: 'Áp dụng',
                cancelLabel: 'Hủy',
                daysOfWeek: ['CN', 'T2', 'T3', 'T4', 'T5', 'T6', 'T7'],
                monthNames: [
                    'Tháng 1', 'Tháng 2', 'Tháng 3', 'Tháng 4', 'Tháng 5', 'Tháng 6',
                    'Tháng 7', 'Tháng 8', 'Tháng 9', 'Tháng 10', 'Tháng 11', 'Tháng 12'
                ],
            }
        }).on('apply.daterangepicker', function (ev, picker) {
            var creationDate = picker.startDate; // Moment.js object
            // Kiểm tra nếu expirationDate đã có giá trị và không hợp lệ
            var expirationDateVal = $("#expirationDate_1").val();
            if (expirationDateVal) {
                var expirationDate = moment(expirationDateVal, 'YYYY-MM-DD HH:mm');
                if (expirationDate.isSameOrBefore(creationDate)) {
                    alert("Ngày hết hạn phải sau ngày tạo!");
                    $("#expirationDate_1").val(''); // Xóa giá trị không hợp lệ
                }
            }
        });

        $("#expirationDate_1").daterangepicker({
            timePicker: true,
            singleDatePicker: true,
            showDropdowns: true,
            locale: {
                format: 'YYYY-MM-DD HH:mm', // Định dạng ngày khớp với giá trị từ server
                applyLabel: 'Áp dụng',
                cancelLabel: 'Hủy',
                daysOfWeek: ['CN', 'T2', 'T3', 'T4', 'T5', 'T6', 'T7'],
                monthNames: [
                    'Tháng 1', 'Tháng 2', 'Tháng 3', 'Tháng 4', 'Tháng 5', 'Tháng 6',
                    'Tháng 7', 'Tháng 8', 'Tháng 9', 'Tháng 10', 'Tháng 11', 'Tháng 12'
                ],
            }
        }).on('apply.daterangepicker', function (ev, picker) {
            // 
            var expirationDate = picker.startDate;
            var creationDateVal = $("#creationDate_1").val();

            if (creationDateVal) {
                var creationDate = moment(creationDateVal, 'YYYY-MM-DD HH:mm');
                if (expirationDate.isSameOrBefore(creationDate)) {
                    alert("Ngày hết hạn phải sau ngày sản xuất!");
                    $("#expirationDate_1").val(creationDate); // xoá ngày ko hợp lệ
                    picker.setStartDate(moment());
                }
            }
        });
    }
    function DaterangerPickerCreate() {
        $("#creationDate").daterangepicker({
            timePicker: true,
            singleDatePicker: true,
            showDropdowns: true,
            locale: {
                format: 'YYYY-MM-DD HH:mm', // Định dạng ngày khớp với giá trị từ server
                applyLabel: 'Áp dụng',
                cancelLabel: 'Hủy',
                daysOfWeek: ['CN', 'T2', 'T3', 'T4', 'T5', 'T6', 'T7'],
                monthNames: [
                    'Tháng 1', 'Tháng 2', 'Tháng 3', 'Tháng 4', 'Tháng 5', 'Tháng 6',
                    'Tháng 7', 'Tháng 8', 'Tháng 9', 'Tháng 10', 'Tháng 11', 'Tháng 12'
                ],
            }

        }).on('apply.daterangepicker', function (ev, picker) {
            var creationDate = picker.startDate; // Moment.js object
            // Kiểm tra nếu expirationDate đã có giá trị và không hợp lệ
            var expirationDateVal = $("#expirationDate").val();
            if (expirationDateVal) {
                var expirationDate = moment(expirationDateVal, 'YYYY-MM-DD HH:mm');
                if (expirationDate.isSameOrBefore(creationDate)) {
                    alert("Ngày hết hạn phải sau ngày tạo!");
                    $("#expirationDate").val(''); // Xóa giá trị không hợp lệ
                }
            }
        });

        $("#expirationDate").daterangepicker({
            timePicker: true,
            singleDatePicker: true,
            showDropdowns: true,
            locale: {
                format: 'YYYY-MM-DD HH:mm', // Định dạng ngày khớp với giá trị từ server
                applyLabel: 'Áp dụng',
                cancelLabel: 'Hủy',
                daysOfWeek: ['CN', 'T2', 'T3', 'T4', 'T5', 'T6', 'T7'],
                monthNames: [
                    'Tháng 1', 'Tháng 2', 'Tháng 3', 'Tháng 4', 'Tháng 5', 'Tháng 6',
                    'Tháng 7', 'Tháng 8', 'Tháng 9', 'Tháng 10', 'Tháng 11', 'Tháng 12'
                ],
            }
        }).on('apply.daterangepicker', function (ev, picker) {
            // 
            var expirationDate = picker.startDate;
            var creationDateVal = $("#creationDate").val();

            if (creationDateVal) {
                var creationDate = moment(creationDateVal, 'YYYY-MM-DD HH:mm');
                if (expirationDate.isSameOrBefore(creationDate)) {
                    alert("Ngày hết hạn phải sau ngày sản xuất!");
                    $("#expirationDate").val(''); // xoá ngày ko hợp lệ
                    picker.setStartDate(creationDate);
                }
            }
        });
        //-----------------------
        //$("#creationDate").on('apply.daterangepicker', function (ev, picker) {
        //    var creationDate = picker.startDate;
        //    $("#expirationDate").data('daterangepicker').setMinDate(creationDate);

        //    var expirationDateVal = $("#expirationDate").val();
        //    if (expirationDateVal) {
        //        alert("Ngày hết hạn phải sau ngày tạo!");
        //        $("#expirationDate").val(''); // xoá gtri không hợp lệ
        //    };
        //});

        //$("#expirationDate").on('apply.daterangepicker', function (ev, picker) {
        //    // 
        //    var expirationDate = picker.startDate;
        //    var creationDateVal = $("#creationDate").val();

        //    if (creationDate) {
        //        var creationDate = moment(creationDateVal, 'YYYY-MM-DD HH:mm');
        //        if (expirationDate.isSameOrBefore(creationDate)) {
        //            alert("Ngày hết hạn phải sau ngày sản xuất!");
        //            $("#expirationDate").val(''); // xoá ngày ko hợp lệ
        //            picker.setStartDate(moment());
        //        }
        //    }
        //});
    }

   
    $("#btn_filter_dropdown").on('click', function (f) {
        console.warn('-- da vao btn filter drop');
        f.preventDefault();
        //-----------------------------
        $(".input_price_drop").ionRangeSlider({
            type: "double",
            //grid: true,
            min: 0,
            max: 10000,
            from: 0,
            to: 1000,
            onChange: function (data) {
                priceFrom = data.from;
                priceTo = data.to;

                console.log('-- khoang gia', priceFrom, priceTo);
            }
        });
        //-----------------------------
        $(".filterDate_drop").on('apply.daterangepicker', function (ev, picker) {
            // Ngăn chặn sự kiện bubble up
            //ev.stopPropagation();
            startDate = picker.startDate.format('YYYY-MM-DD HH:mm');
            endDate = picker.endDate.format('YYYY-MM-DD HH:mm');
            console.log('--startDate:', startDate);
            console.log('--endDate:', endDate);
        });

        $(".filterDate_drop").daterangepicker({
            //autoUpdateInput: false,
            showDropdowns: true,
            locale: {
                format: 'YYYY-MM-DD ', // Định dạng ngày khớp với giá trị từ server
                applyLabel: 'Áp dụng',
                cancelLabel: 'Hủy',
                daysOfWeek: ['CN', 'T2', 'T3', 'T4', 'T5', 'T6', 'T7'],
                monthNames: [
                    'Tháng 1', 'Tháng 2', 'Tháng 3', 'Tháng 4', 'Tháng 5', 'Tháng 6',
                    'Tháng 7', 'Tháng 8', 'Tháng 9', 'Tháng 10', 'Tháng 11', 'Tháng 12'
                ],
            }
        }
        ,function (start, end, label) {
            // Ngăn chặn sự kiện bubble up
            event.stopPropagation();
        });
    });
    $('<style>')
        .text(`
            .filter-disabled {
                opacity: 0.5;
                pointer-events: none;
            }
            .filter-enabled {
                opacity: 1;
                pointer-events: auto;
            }
        `)
        .appendTo('head');

    // Xử lý sự kiện checkbox
    $('#checkboxDate, #checkboxPrice').on('change', function () {
        var isDateChecked = $('#checkboxDate').is(':checked');
        var isPriceChecked = $('#checkboxPrice').is(':checked');

        // Xử lý phần filter ngày
        if (isDateChecked) {
            $('.filterDate_drop').closest('div').removeClass('filter-disabled').addClass('filter-enabled');
            console.log()
        } else {
            $('.filterDate_drop').closest('div').removeClass('filter-enabled').addClass('filter-disabled');

            // Reset giá trị date range khi bỏ chọn
            $('.filterDate_drop').val('');
           
            startDate = '';
            endDate = '';
        }

        // Xử lý phần filter giá
        if (isPriceChecked) {
            $('.input_price_drop').closest('div').removeClass('filter-disabled').addClass('filter-enabled');
        } else {
            $('.input_price_drop').closest('div').removeClass('filter-enabled').addClass('filter-disabled');
            // Reset giá trị price range khi bỏ chọn
            //$('.js-range-slider_drop').val('');
            priceFrom = 0;
            priceTo = 0;
        }
    });
    // Khởi tạo trạng thái ban đầu
    $('#checkboxDate, #checkboxPrice').trigger('change');

   
    //-- end filter --
    //---------------------------

    _$form.validate({
        rules: {
            NameProduct: {
                required: true,
                maxlength: 64,
                minlength: 2
            },
            Price: {
                required: true,
                number: true,
                min: 0.00000000001,
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
               
            },
            CategoryIds: {
                required: true
            },
            ProductImagePath: {
                required: true
            }
        },
        messages: {
            NameProduct: {
                //required: "Bạn phải nhập tên sản phẩm!",
                required: l("NameProduct_Required"),
                maxlength: l("NameProduct_MaxLength"),
                minlegth: l("NameProduct_MinLength")
            },
            Price: {
                required: l("Price_Required"),
                number: l("Price_Number"),
                min: l("Price_Min")
            },
            DescriptionProduct: {
                required: l("DescriptionProduct_Required"),
                maxlength: l("DescriptionProduct_MaxLength")
            },
            CreationDate: {
                required: l("CreationDate_Required"),

            },
            ExpirationDate: {
                required: l("ExpirationDate_Required"),

            },
          
            CategoryIds: {
                required: l("NameCategory_Required")
            },
            ProductImagePath: {
                required: l("ProductImagePath_Required")
            }
        }
    })
})(jQuery);

//  Vì #btn_filter được tạo động(thông qua $('#dynamic-input').html(...)), 
//      nên tại thời điểm bạn chạy $('#btn_filter').on('click', ...), phần tử #btn_filter có thể chưa tồn tại trong DOM.
//      Do đó, sự kiện click sẽ không được gắn.
//Giải pháp: Sử dụng event delegation để gắn sự kiện cho các phần tử động.
//      Thay vì gắn trực tiếp cho #btn_filter, bạn nên gắn sự kiện thông qua document hoặc một phần tử cha cố định(ví dụ: #dynamic - input).

//pagelength: 5,
//    lengthMenu: [16, 17, 18],