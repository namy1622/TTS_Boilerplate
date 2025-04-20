(function ($) {
    console.log("-- index.js Category loaded");

    var _categoryService = abp.services.app.category,
        l = abp.localization.getSource('TTS_boilerplate'),
        _$table = $('#CategoryTable'),
        _$modalCreate = $('#CategoryCreateModal'),
        _$formCreate = _$modalCreate.find('form');

    console.log("-- property category", _categoryService);



    var _$categoryTable = _$table.DataTable({
        paging: true,
        serverSide: true,
        ordering: true,
        listAction: {
            ajaxFunction: _categoryService.getAllCategory,
            inputFilter: function () {
                var _$formObj = {};
                var order = _$categoryTable ? _$categoryTable.order() : '';//[[1, 'asc']];
                console.log('-- order', order);

                if (order && order.length > 0) {
                    var columnIndex = order[0][0];
                    var direction = order[0][1];
                    var columnName = _$categoryTable ? _$categoryTable.settings()[0].aoColumns[columnIndex].name : 1;
                    _$formObj.sorting = columnName + ' ' + direction;
                }
                else {
                    _$formObj.sorting = ''; // default
                }
                console.log('-- Form data:', _$formObj);
                return _$formObj;
            }
        },
        buttons: [{
            name: 'refresh',
            text: '<i class="fas fa-redo-alt"></i>',
            action: () => _$categoryTable.draw(false)
        }],
        responsive: {
            details: {
                type: 'column'
            }
        },
        columnDefs: [
            {
                targets: 0,
                className: 'control',
                defaultContent: ''
            },
            {
                targets: 1,
                data: 'id',
                name: 'Id',
                sortable: true,
                defaultContent: '',
                //render: (data, row, meta) => {
                //    console.log(row);
                //}
            },
            {
                targets: 2,
                data: 'nameCategory',
                name: 'NameCategory',
                sortable: true,
                defaultContent: ''
            },
            {
                targets: 3,
                data: 'totalProduct',
                name: 'TotalProduct',
                sortable: true,
                defaultContent: ''
            },
            {
                targets: 4,
                data: null,
                sortable: false,
                autoWidth: false,
                defaultContent: '',
                render: (data, type, row, meta) => {
                    console.log(row);
                    return [
                        
                        //`<button type="button" title="Update" class="btn btn-sm bg-secondary update-category " data-category-id="${row.id}" data-toggle="modal" data-target="#ProductEditModal">`,
                        //`   <i class="fas fa-pencil-alt"></i>`,
                        //'</button>',
                        //`<button type="button" title="Delete" class="btn btn-sm bg-danger ml-2 delete-category" data-category-id="${row.id}" data-category-name="${row.nameCategory}" >`,
                        //`   <i class="fas fa-trash"></i> `,
                        //'</button>'

                        
                        abp.auth.hasPermission('Pages.Categories_U')
                            ?`<button type="button" title="Update" class="btn btn-sm bg-secondary update-category " data-category-id="${row.id}" data-toggle="modal" data-target="#ProductEditModal">
                               <i class="fas fa-pencil-alt"></i>
                             </button>`
                            : '',
                        abp.auth.hasPermission('Pages.Categories_D')
                            ? `<button type="button" title="Delete" class="btn btn-sm bg-danger ml-2 delete-category" data-category-id="${row.id}" data-category-name="${row.nameCategory}" >
                                   <i class="fas fa-trash"></i> 
                                </button>`
                            : ''
                        
                    ].join('');
                }
            }
        ]
    });

    //-- create Category --
    _$formCreate.find(".save-button").on('click', (e) => {
        e.preventDefault();

        if (!_$formCreate.valid()) {
            return;
        }

        var category = _$formCreate.serializeFormToObject();

        console.info('category_create form', category);

        _categoryService.createCategory(category)
            .done(function () {
                _$modalCreate.modal('hide');
                _$formCreate[0].reset();

                abp.notify.info(l('SavedSuccessfully'));

                _$categoryTable.ajax.reload();
            }).always(function () {
                abp.ui.clearBusy(_$modalCreate);
            }).fail(function (e) {
                console.log('catch', e)
            });

    });
    //-----------------

    //-- update category -----
    $(document).on('click', '.update-category', function (e) {
        var categoryId = $(this).attr("data-category-id");
        console.log('--id category', categoryId);

        e.preventDefault();

        console.log('-- path', abp.appPath + 'Category/UpdateCategory?categoryId=' + categoryId);
        abp.ajax({
            
            url: abp.appPath + 'Category/UpdateCategory?categoryId=' + categoryId,
            type: 'POST',
            dataType: 'html',
            success: function (content) {
                if (!content || content.trim() === "") {
                    console.error('Lỗi nội dung trả về trống...');
                    return;
                }
                //console.warn('-- ma update', content);

                var $modalContent = $('#CategoryUpdateModal div.modal-content');
                if ($modalContent.length) {
                    $modalContent.html(content);
                } else {
                    console.error('Không tìm thấy modal-content');
                }

                console.log('Sau html(content', "--")
                $('#CategoryUpdateModal').modal('show');
                console.log('Sau show modal', "--")
            },
            error: function (e) {
                console.log('error update', e)
            }
        });

    });

    abp.event.on('category.updateModalSaved', (data) => {
        _$categoryTable.ajax.reload();
    });             
    //------------------------

    //-- delete category -----
    $(document).on('click', '.delete-category', function () {
        console.warn("-- Ban vua bam Delete");
        var categoryId = $(this).attr("data-category-id");

        var categoryName = $(this).attr("data-category-name");

        console.log("-- id, name category", categoryId, categoryName)

        deleteCategory(categoryId, categoryName);

    });
    function deleteCategory(id, name) {
        abp.message.confirm(
            abp.utils.formatString("Bạn có chắc muốn xóa " + name),
            null,
            (isConfirmed) => {
                if (isConfirmed) {
                    _categoryService.deleteCategory(id)
                        .done(() => {
                            abp.notify.info(l('SuccessfullyDeleted'));
                            _$categoryTable.ajax.reload();
                        });
                }
            }

        )
    }
    
    //------------------------
    


    _$modalCreate.on('shown.bs.modal', () => {
        console.warn('focus input form');
        _$modalCreate.find('input:not([type=hidden]):first').focus();
    }).on('hidden.bs.modal', () => {
        _$formCreate.clearForm();
        console.warn('out input form');
    });

    // khởi tạo validator
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