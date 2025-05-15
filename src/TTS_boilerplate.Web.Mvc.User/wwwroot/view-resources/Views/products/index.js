(function ($) {
    var _$productServices = abp.services.app.productService
    var _$orderService = abp.services.app.orders;
    console.log('-- a', abp.services.app.productService)
    var _$productList = $('#ProductList');
    var _$modal = $('#modalCard'),
        _$valPrice = $('input[name=maxPrice');
    var _$addCartId = '',
        _$buyNowId = '';

    var _$idUser = abp.session.userId;
    var _$currentFilters = {
        categories: [],
        minPrice: 0,
        maxPrice: null,
        keySearch: null
    }

    var _$rangePrice = $('#price-range');

    // Khởi tạo khi document ready
    $(document).ready(function () {
        var test = $('#test').attr("data-test")
        console.warn('-- test', test);
        productDisplay.init();
        //initCart();
    });
    var page = 1;
    // Load product lên view 
    var productDisplay = {
        _currentPage: 1,
        _pageSize: 12,
        _totalItems: 0,
        _totalPages: 0,

        // hàm khởi tạo 
        init: function () {
            console.warn('-- da vao init--')
            console.warn('-- _pageSize --', this._pageSize)
            this.loadProducts(page);
            this.handlerFilter();


            const now = new Date();
            var dataOrder = {
                userId: _$idUser,
                nowDate: now,
            }
            $('#buyNow').on('click', function () {
                if (!_$idUser) {
                    console.log("-- userId null, chuyen den dang nhap");
                    window.location.href = '/Account/Login';
                    return;
                }
                var productId = $(this).attr('data-product-id');
                window.location.href = '/Products/InitOrder?productId=' + productId;
            });
        },

        loadProducts: function (page) {

            var self = this; // Lưu context this(this này của productDisplay)
            var userId = _$idUser
            this._currentPage = page || this._currentPage; // Cập nhật trang hiện tại
            var requestData = {
                skipCount: (page - 1) * this._pageSize,
                maxResultCount: this._pageSize,
                categories: _$currentFilters.categories,
                minPrice: _$currentFilters.minPrice,
                maxPrice: _$currentFilters.maxPrice,
                keySearch: _$currentFilters.keySearch
            };
            console.log('-- requestData', requestData);
            abp.ui.setBusy(); // Hiển thị loading
            // Sử dụng abp.services.app để gọi API
            _$productServices.getAll_Product(requestData)
                .done(function (result) {
                    console.warn('-- load thanh cong', result);
                    if (result && result.items) {  // Thêm kiểm tra result
                        self._totalItems = result.totalCount; // Cập nhật totalItems
                        self._totalPages = Math.ceil(result.totalCount / self._pageSize); // Cập nhật totalPages
                        if (result.items.length > 0) {  // vào function này thì nó là this khác 
                            self.renderProducts(result.items);
                            self.renderPagination();
                        } else {
                            _$productList.html('<div class="col-12 text-center">Không có sản phẩm nào phù hợp</div>');
                            $('#pagination').empty();
                            abp.notify.info('Không tìm thấy sản phẩm phù hợp');
                        }
                    }

                    //_$productServices.initCart(userId)
                    //  .done(function () {
                    //    console.warn('-- da tao gio hang cho user {0} thanh cong')
                    //  })
                    //  .fail(function () {
                    //    console.warn('Tao gio hang that bai');
                    //  })
                })
                .fail(function (error) {
                    console.error('Lỗi khi tải sản phẩm:', error);
                    abp.notify.error('Có lỗi xảy ra khi tải sản phẩm');
                })
                .always(function () {
                    abp.ui.clearBusy();
                });
        },
        renderPagination: function () {
            var $pagination = $('#pagination');
            var $container = $('.pagination-container');
            $pagination.empty();

            if (this._totalItems <= 1) return;

            // thông tin phân trang
            var startItem = (this._currentPage - 1) * this._pageSize + 1
            var endItem = Math.min(startItem + this._pageSize - 1, this._totalItems);

            var paginationHtml = `
        <div class="pagination-info mb-2">
            Hiển thị ${startItem} - ${endItem} của ${this._totalItems} sản phẩm
        </div>
        <ul class="pagination justify-content-center" id="pagination">
            <li class="m-1 page-item ${this._currentPage == 1 ? 'disabled' : ''} ">
                <a class="page-link px-3 py-1 rounded-md bg-white border border-gray-300 text-gray-700 hover:bg-gray-50" href="#" data-page="${parseInt(this._currentPage) - 1}">
                    <i class="fas fa-chevron-left"></i>
                </a>
            </li>
    `;
            // Tính toán range trang hiển thị
            var startPage = Math.max(1, this._currentPage - 2);
            var endPage = Math.min(this._totalPages, startPage + 4);
            startPage = Math.max(1, endPage - 4);
            // Thêm các trang
            for (var i = startPage; i <= endPage; i++) {
                var active = i == this._currentPage ? 'active' : '';
                paginationHtml += `
            <li class="page-item ${active} m-1">
                <a class="page-link px-3 py-1 rounded-md border border-gray-300 text-gray-700 hover:bg-gray-50" href="#" data-page="${i}">${i}</a>
            </li>
        `;
            }
            //nút Next
            paginationHtml += `
            <li class="m-1 page-item ${this._currentPage == this._totalPages ? 'disabled' : ''}">
                <a class="page-link px-3 py-1 rounded-md bg-white border border-gray-300 text-gray-700 hover:bg-gray-50" href="#" data-page="${parseInt(this._currentPage) + 1}">
                     <i class="fas fa-chevron-right"></i>
                </a>
            </li>
        </ul>
    `;
            // Thêm toàn bộ HTML vào container
            $container.html(paginationHtml);
            // Bind sự kiện click cho các nút phân trang
            $container.find('.page-link').on('click', function (e) {
                e.preventDefault();
                if (!$(this).parent().hasClass('disabled')) {
                    var _page = $(this).attr('data-page');
                    page = _page;
                    //productDisplay.loadProducts(_$currentFilters, page);
                    productDisplay.loadProducts(_page);
                    // Scroll to top
                    $('html, body').animate({ scrollTop: 0 }, 'slow');
                }
            });
        },

        handlerFilter: function () {
            var self = this;

            // reset về trang 1 khi thay đổi filter 
            function resetToFirstPage() {
                self._currentPage = 1;
                self.loadProducts(_$currentFilters, 1);
            }

            $('input[type = "checkbox"]').on('change', function () {
                var categoryId = $(this).attr('id');
                if ($(this).is(':checked')) {
                    if (!_$currentFilters.categories.includes(categoryId)) {
                        _$currentFilters.categories.push(categoryId);
                    }
                }
                else {
                    _$currentFilters.categories = _$currentFilters.categories.filter(id => id != categoryId);
                }
                console.log('-- categories[]', _$currentFilters.categories);
            });

            // Handle price range input
            _$rangePrice.on('input', function () {
                var val = $(this).val();
                _$valPrice.val(val);
                _$currentFilters.maxPrice = parseInt(val);
                console.log('-- _$currentFilters.maxPrice', _$currentFilters.maxPrice);
            });

            // Handle manual price input
            _$valPrice.on('input', function () {
                var val = $(this).val();
                _$currentFilters.maxPrice = parseInt(val) || 0;
                _$rangePrice.val(val);
                console.log('-- _$currentFilters.maxPrice_valPrice', _$currentFilters.maxPrice);
            });

            // Handle search input
            $('input[name="KeySearch"]').on('input', function () {
                _$currentFilters.keySearch = $(this).val();
                console.log('-- _$currentFilter.keySearch', _$currentFilters.keySearch);
            })

            // Handle filter button click
            $('#btnFilter').on('click', function () {
                self._currentPage = 1; // Reset to first page
                self.loadProducts(1);
            });

            $('#btn_Search').on('click', function () {
                self._currentPage = 1; // Reset to first page
                self.loadProducts(1);
            });

        },
        renderProducts: function (products) {
            _$productList.empty(); // Xóa nội dung cũ trước khi render

            if (!products || products.length === 0) {
                _$productList.html('<div class="col-12 text-center">Không có sản phẩm nào</div>');
                return;
            }
            products.forEach(function (product) {
                var productHtml = `

                <div class="col-lg-2 col-md-3 col-sm-4 mb-3" style="">
                    <div class="bg-white rounded-lg shadow-md overflow-hidden product-card " data-product-id="${product.id}">
                        <div class="relative overflow-hidden">
                            <img src="${product.productImagePath}" 
                                 alt="${product.nameProduct}" 
                                 class="w-full h-48 object-fit product-image transition-transform duration-300">
                        </div>
                        <div class="p-3">
                            <h3 class="text-md font-semibold text-gray-800 mb-1 truncate">${product.nameProduct}</h3>
                            <p class="text-sm text-gray-500 mb-2">${product.nameCategory}</p>
                            <div class="flex items-center justify-between">
                                <span class="text-md font-bold text-blue-600">${product.price.toLocaleString('vi-Vn')}đ</span>
                                <button id="addCart" class="w-8 h-8 flex items-center justify-center border rounded-full text-gray-500 hover:bg-gray-50">
                                   <i class="fas fa-cart-plus"></i>
                                </button>
                            </div>
                        </div>
                    </div>
                </div>
                `;
                _$productList.append(productHtml);
            });
        }
    };

    // hiển thị modal khi bấm vào sản phẩm 
    _$productList.on('click', '.product-card', function () {
        const productId = $(this).data('product-id');

        $('#buyNow').attr('data-product-id', productId);
        $('#addCart').attr('data-product-id', productId);

        _$buyNowId = productId;
        _$addCartId = productId;

        console.warn('-- buynow : addCart', $('#buyNow').attr('data-product-id'), $('#addCart').attr('data-product-id'));
        // Cleanup function
        function cleanup() {
            $('#modalName').text('');
            $('#modalImage').attr('src', '');
            $('#modalDescription').text('');
            $('#modalPrice').text('');

        }
        _$modal.on('hidden.bs.modal', cleanup);

        console.log('-- id product', productId);
        //abp.ui.setBusy($modal);
        _$productServices.getProduct(productId)
            .done(function (product) {
                console.warn('-- product item', product);
                $('#modalName').text(product.nameProduct);
                $('#modalImage').attr('src', product.productImagePath || '/images/no-image.png')
                    .on('error', function () {
                        $(this).attr('src', '/images/no-image.png');
                    });
                $('#modalDescription').text('Mô tả: ' + product.descriptionProduct);
                $('#modalPrice').text(product.price.toLocaleString('vi-Vn') + 'đ');

                // hiển thị modal product 
                _$modal.addClass('active-modal');  // active được tạo trong card.css 
                //_$modal.modal('show');
                console.log('-- da hien modal');
            })
            .fail(function (error) {
                console.error('Lỗi khi tải thông tin sản phẩm:', error);
                abp.notify.error('Không thể tải thông tin sản phẩm. Vui lòng thử lại sau.');
            })
            .always(function () {
                // Ẩn loading
                abp.ui.clearBusy();
            });
    });

    $('#detailProduct').on('click', function () {
        window.location.href = '/Products/Detail_Product?productId=' + _$addCartId;
    });


    $('#addCart').on('click', function (event) {
        // Ngăn chặn sự kiện click lan tỏa lên các phần tử cha
        event.stopPropagation();
        //_$idUser = abp.session.userId;
        if (!_$idUser) {
            console.log("-- userId null, chuyen den dang nhap");
            window.location.href = '/Account/Login';
            return;
        }
        console.log('Sending data:', {
            idProduct: _$addCartId,
            idUser: _$idUser
        });
        //_$isUser = abp.session.userId;
        abp.ui.setBusy();
        _$productServices.addProductToCart({
            idProduct: _$addCartId,
            idUser: _$idUser,
            status: "InCart"
        })
            .done(function (result) {
                console.log('-- thanh cong Add', result)
                abp.notify.info("Đã thêm sản phẩm vào giỏ hàng!")
            })
            .fail(function (error) {
                console.error('Lỗi khi tải sản phẩm:', error);
                abp.notify.error('Có lỗi xảy ra khi tải sản phẩm');
            })
            .always(function () {
                abp.ui.clearBusy();
            });
    });



})(jQuery);
