(function ($) {
    var _$productServices = abp.services.app.productService
    console.log('-- a', abp.services.app.productService)
    var _$productList = $('#ProductList');
    var _$modal = $('#modalCard'),
        _$valPrice = $('input[name=maxPrice');
    var _$addCartId = '',
       _$buyNowId = '';
    
    var _$idUser = abp.session.userId;
  var currentPageDefault = 1;
    
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
    _pageSize: 10,
    _totalItems: 0,
    _totalPages: 0,
     

        // hàm khởi tạo 
    init: function () {
      console.warn('-- da vao init--')
      console.warn('-- _pageSize --', this._pageSize)
      this.loadProducts(page);
      this.handlerFilter();
     },
    //loadProducts : function(filters, page = 1){
    loadProducts: function (page) {

      
      var self = this; // Lưu context this(this này của productDisplay)
      var userId = _$idUser
      this._currentPage = page || this._currentPage; // Cập nhật trang hiện tại
      var requestData = {
        skipCount: (page - 1) * this._pageSize,
        maxResultCount: this._pageSize,
      };

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
                 })
                 .fail(function (error) {
                     console.error('Lỗi khi tải sản phẩm:', error);
                     abp.notify.error('Có lỗi xảy ra khi tải sản phẩm');
                 })
                 .always(function () {
                     abp.ui.clearBusy();
                 });
          _$productServices.initCart(userId)
            .done(function () {
              console.warn('-- da tao gio hang cho user {0} thanh cong')
            })
            .fail(function () {
              console.warn('Tao gio hang that bai');
            })
        
    },
    renderPagination: function () {
      var $pagination = $('#pagination');
      $pagination.empty();

      if (this._totalItems <= 1) return;

      // thông tin phân trang
      var startItem = (this._currentPage - 1) * this._pageSize + 1
      var endItem = Math.min(startItem + this._pageSize - 1, this._totalItems);


      $pagination.before(`
          <div>
            Hiển thị ${startItem} - ${endItem} của ${this._totalItems} sản phẩm
          </div>
      `);
      // Thêm nút Previous
      var prevDisabled = this._currentPage === 1 ? 'disabled' : '';
      $pagination.append(`
            <li class="page-item ${prevDisabled}">
                <a class="page-link" href="#" data-page="${this._currentPage - 1}"><</a>
            </li>
        `);
      // Tính toán range trang hiển thị
      var startPage = Math.max(1, this._currentPage - 2);
      var endPage = Math.min(this._totalPages, startPage + 4);
      startPage = Math.max(1, endPage - 4);
      // Thêm các trang
      for (var i = startPage; i <= endPage; i++) {
        var active = i == this._currentPage ? 'active' : '';
        $pagination.append(`
                <li class="page-item ${active}">
                    <a class="page-link" href="#" data-page="${i}">${i}</a>
                </li>
            `);
      }
      // Thêm nút Next
      var nextDisabled = this._currentPage === this._totalPages ? 'disabled' : '';
      $pagination.append(`
            <li class="page-item ${nextDisabled}">
                <a class="page-link" href="#" data-page="${this._currentPage + 1}">></a>
            </li>
        `);
      // Bind sự kiện click cho các nút phân trang
      $pagination.find('.page-link').on('click', function (e) {
        e.preventDefault();
        if (!$(this).parent().hasClass('disabled')) {
          var _page = $(this).attr('data-page');
          page = _page;
          //productDisplay.loadProducts(_$currentFilters, page);
          productDisplay.loadProducts( _page);
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
    },
        renderProducts: function (products) {
            _$productList.empty(); // Xóa nội dung cũ trước khi render

            if (!products || products.length === 0) {
                _$productList.html('<div class="col-12 text-center">Không có sản phẩm nào</div>');
                return;
            }
            products.forEach(function (product) {
                var productHtml = `
                    <div class="col-lg-2 col-md-3 col-sm-4" >
                    <div class="card shadow-sm card__product" style="padding:0" data-product-id="${product.id}">
                        <div class="card-body" style="padding:0px;">
                            <div>
                                <img src="${product.productImagePath}" loading="lazy" class="card-img-top card__img" alt="${product.nameProduct}" />
                                ${product.discount ? `<span class="badge rounded-pill position-absolute top-0 end-0 bg-danger text-white">${product.discount}%</span>` : ''}
                            </div>
                            <div class="hi" style="background-color:white">
                                <div class="row">
                                    <div class="col-12 text-start d-flex flex-column">
                                        <h5 class="card-title card__name">${product.nameProduct}</h5>
                                        <p class="text-muted mb-0">${product.nameCategory}</p>
                                        <p class="text-muted card__price" style="margin-bottom:0px">${product.price} VND</p>
                                    </div>
                                </div>
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
    _$productList.on('click', '.card__product', function () {
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
                $('#modalPrice').text(product.price + 'VND');

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

    $('#buyNow').on('click', function () {
        console.log('-- da click buyNow voi id', _$buyNowId)
    });

    $('#addCart').on('click', function () {
        _$idUser = abp.session.userId;
        console.log('Sending data:', {
            idProduct: _$addCartId,
            idUser: _$idUser
        });
        //_$isUser = abp.session.userId;
        abp.ui.setBusy();
        _$productServices.addProductToCart({
            idProduct: _$addCartId,
            idUser: _$idUser
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

    var addCardEvent = (function () {
        return {
            initAddCart: function () {
                this.bindEvent();
            },
            bindEvent: function () {
               
            },
        }
    });




    // event di chuyển thanh giá
    _$rangePrice.on('input', function () {
        const val = $(this).val();
        console.log('-- valPrice', val);
        _$valPrice.val(val);
        console.log('-- valPrice', _$valPrice.val());
    })
    // event khi nhập giá
    _$valPrice.on('input', function () {
        const val = $(this).val();
        console.log('-- valPrice', val);
    })


})(jQuery);
